﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// ContentRoot.h, ContentRoot.cpp

#nullable enable

using System;
using DirectUI;
using Microsoft.UI.Content;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Uno.Disposables;
using Uno.UI.Xaml.Core.Scaling;
using Uno.UI.Xaml.Input;
using Uno.UI.Xaml.Islands;
using Windows.UI;
using static Microsoft/* UWP don't rename */.UI.Xaml.Controls._Tracing;

/*
    +----------------------------------------------------------------------------------+
    |                                      +---------------+                           |
    |                                      | CoreServices  |                           |
    |                                      +-------+-------+                           |
    |                                              |                                   |
    |                                              |                                   |
    |                                        +-----v----------------+                  |
    |                              +---------+ContentRootCoordinator|                  |
    |                              |         +-----------------+----+                  |
    |                              |                           |                       |
    |                              |                           |                       |
    |                       +------v-----+            +--------v---+                   |
    |                       |ContentRoot |            |ContentRoot |"Main Content Root"|
    |                       +--+---------+            +------+-----+                   |
    |                          |                             |                         |
    |      +--------------+    |                             |      +---------------+  |
    |      | FocusManager<----+                             +------> FocusManager |    |
    |      +--------------+    |                             |      +---------------+  |
    |      +--------------+    |                             |      +--------------+   |
    |      | InputManager<----+                             +------> InputManager|     |
    |      +--------------+    |                             |      +--------------+   |
    |      +------------+      |                             |      +-----------+      |
    |      | VisualTree <------+                             +------> VisualTree|      |
    |      +------------+                                           +-----------+      |
    |                                                                                  |
    |                                                                                  |
    +----------------------------------------------------------------------------------+
*/

namespace Uno.UI.Xaml.Core;

/// <summary>
/// Represents the content root of an application window.
/// </summary>
internal partial class ContentRoot
{
	private readonly CoreServices _coreServices;
	private readonly ContentRootEventListener _contentRootEventListener;

	/// <summary>
	/// Initializes a content root.
	/// </summary>
	/// <param name="rootElement">Root element.</param>
	public ContentRoot(ContentRootType type, Color backgroundColor, UIElement? rootElement, CoreServices coreServices)
	{
		_coreServices = coreServices ?? throw new ArgumentNullException(nameof(coreServices));
		Type = type;
		//TODO Uno: Does not match WinUI exactly, additional logic can be ported later.
		VisualTree = new VisualTree(coreServices, backgroundColor, rootElement, this);
		InputManager = new InputManager(this);
		_contentRootEventListener = new ContentRootEventListener(this);
		FocusManager = new FocusManager(this);

		CompositionTarget = new CompositionTarget(this);
		CompositionTarget.Root = ElementCompositionPreview.GetElementVisual(VisualTree.RootElement);
		CompositionTarget.Root.CompositionTarget = CompositionTarget;

		switch (type)
		{
			case ContentRootType.CoreWindow:
				MUX_ASSERT(coreServices.ContentRootCoordinator.CoreWindowContentRoot == null);
				coreServices.ContentRootCoordinator.CoreWindowContentRoot = this;

				FocusAdapter = new FocusManagerCoreWindowAdapter(this);
				FocusManager.SetFocusObserver(new CoreWindowFocusObserver(this));
				break;
			case ContentRootType.XamlIslandRoot:
				FocusAdapter = new FocusManagerXamlIslandAdapter(this);
				FocusManager.SetFocusObserver(new FocusObserver(this));
				break;
			default:
				throw new InvalidOperationException("Unknown content root type.");
		}
	}

	internal CompositionTarget CompositionTarget { get; }

	internal ContentRootType Type { get; }

	/// <summary>
	/// Represents the visual tree associated with this content root.
	/// </summary>
	internal VisualTree VisualTree { get; }

	/// <summary>
	/// Represents the focus manager associated with this content root.
	/// </summary>
	internal FocusManager FocusManager { get; }

	/// <summary>
	/// Represents the input manager associated with this content root.
	/// </summary>
	internal InputManager InputManager { get; }

	/// <summary>
	/// Represents focus adapter.
	/// </summary>
	internal FocusAdapter FocusAdapter { get; }

	//TODO Uno: Initialize properly when Access Keys are supported (see #3219)
	/// <summary>
	/// Access key export.
	/// </summary>
	internal AccessKeyExport AccessKeyExport { get; } = new AccessKeyExport();

	internal XamlRoot? XamlRoot => VisualTree.XamlRoot;

	internal XamlIsland? XamlIslandRoot { get; set; }

	private void OnStateChanged()
	{
		switch (Type)
		{
			case ContentRootType.XamlIslandRoot:
				XamlIslandRoot?.OnStateChanged();
				break;
			case ContentRootType.CoreWindow:
				FxCallbacks.DxamlCore_OnCompositionContentStateChangedForUWP();
				break;
			default:
				throw new InvalidOperationException("Unknown ContentRootType");
		}
	}

	private void OnAutomationProviderRequested(
		ContentIsland content,
		ContentIslandAutomationProviderRequestedEventArgs args)
	{
		if (Type == ContentRootType.XamlIslandRoot)
		{
			// XamlislandRoot.OnContentAutomationProviderRequested(content, args));
		}
	}

	private void RegisterCompositionContent(ContentIsland compositionContent)
	{
		_compositionContent = compositionContent;

		void OnStateChangedHandler(ContentIsland content, ContentIslandStateChangedEventArgs args) => OnStateChanged();

		_compositionContent.StateChanged += OnStateChangedHandler;
		_compositionContentStateChangedToken.Disposable = Disposable.Create(() => _compositionContent.StateChanged -= OnStateChangedHandler);

		// Accessibility
		_compositionContent.AutomationProviderRequested += OnAutomationProviderRequested;
		_automationProviderRequestedToken.Disposable = Disposable.Create(() => _compositionContent.AutomationProviderRequested -= OnAutomationProviderRequested);
	}

	private void ResetCompositionContent()
	{
		if (_compositionContent is not null)
		{
			if (_compositionContentStateChangedToken.Disposable is not null)
			{
				_compositionContentStateChangedToken.Disposable = null;
			}

			if (_automationProviderRequestedToken.Disposable is not null)
			{
				_automationProviderRequestedToken.Disposable = null;
			}

			_compositionContent = null;
		}
	}

	internal void SetContentIsland(ContentIsland compositionContent)
	{
		// ContentRoot is re-used through the life time of an application. Hence, CompositionContent can be set multiple time.
		// ResetCompositionContent will make sure to remove handlers and reset previous CompositionContent.
		ResetCompositionContent();
		RegisterCompositionContent(compositionContent);

		if (RootScale.GetRootScaleForContentRoot(this) is { } rootScale) // Check that we still have an active tree
		{
			rootScale.SetContentIsland(compositionContent);
		}
	}

	//TODO Uno: This might need to be adjusted when we have proper lifetime handling
	internal bool IsShuttingDown() => false;

	internal XamlRoot GetOrCreateXamlRoot() => VisualTree.GetOrCreateXamlRoot();

	internal void RaiseXamlRootChanged(ContentRoot.ChangeType changeType)
	{
		AddPendingXamlRootChangedEvent(changeType);
		bool shouldRaiseWindowChangedEvent = (changeType != ContentRoot.ChangeType.Content) ? true : false;
		RaisePendingXamlRootChangedEventIfNeeded(shouldRaiseWindowChangedEvent);
	}

	internal void RaisePendingXamlRootChangedEventIfNeeded(bool shouldRaiseWindowChangedEvent)
	{
		if (_hasPendingChangedEvent)
		{
			_hasPendingChangedEvent = false;
			if (XamlRoot is { } xamlRoot)
			{
				FxCallbacks.XamlRoot_RaiseChanged(xamlRoot);
			}

			// TODO Uno: Port QualifierContext
			//if (shouldRaiseWindowChangedEvent)
			//{
			//	const wf::Size size = m_visualTree.GetSize();
			//	m_visualTree.GetQualifierContext()->OnWindowChanged(
			//		static_cast<XUINT32>(lround(size.Width)),
			//		static_cast<XUINT32>(lround(size.Height)));
			//}
		}
	}

	internal Window? GetOwnerWindow()
	{
		return Type switch
		{
			ContentRootType.CoreWindow => Window.CurrentSafe,
			ContentRootType.XamlIslandRoot when XamlIslandRoot is not null => XamlIslandRoot.OwnerWindow,
			_ => null
		};
	}
}
