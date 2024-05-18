﻿#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Uno.Foundation.Logging;
using Uno.UI.Dispatching;
using Uno.UI.Hosting;
using Uno.UI.Runtime.Skia.Wpf.Constants;
using Uno.UI.Runtime.Skia.Wpf.Extensions;
using Uno.UI.Runtime.Skia.Wpf.Input;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input;
using Point = System.Windows.Point;
using Rect = Windows.Foundation.Rect;
using WpfControl = System.Windows.Controls.Control;
using WpfMouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Uno.UI.XamlHost.Skia.Wpf;

internal sealed class WpfCorePointerInputSource : IUnoCorePointerInputSource
{
#pragma warning disable CS0067 // Some event are not raised on WPF ... yet!
	public event TypedEventHandler<object, PointerEventArgs>? PointerCaptureLost;
	public event TypedEventHandler<object, PointerEventArgs>? PointerEntered;
	public event TypedEventHandler<object, PointerEventArgs>? PointerExited;
	public event TypedEventHandler<object, PointerEventArgs>? PointerMoved;
	public event TypedEventHandler<object, PointerEventArgs>? PointerPressed;
	public event TypedEventHandler<object, PointerEventArgs>? PointerReleased;
	public event TypedEventHandler<object, PointerEventArgs>? PointerWheelChanged;
	public event TypedEventHandler<object, PointerEventArgs>? PointerCancelled; // Uno Only
#pragma warning restore CS0067

	private readonly WpfControl _hostControl = default!;
	private HwndSource? _hwndSource;
	private PointerEventArgs? _previous;

	public WpfCorePointerInputSource(IXamlRootHost host)
	{
		if (host is null) return;

		if (host is not WpfControl hostControl)
		{
			throw new ArgumentException($"{nameof(host)} must be a WPF Control instance", nameof(host));
		}

		_hostControl = hostControl;

		_hostControl.MouseEnter += HostOnMouseEnter;
		_hostControl.MouseLeave += HostOnMouseLeave;

		_hostControl.StylusMove += HostControlOnStylusMove;
		_hostControl.StylusDown += HostControlOnStylusDown;
		_hostControl.StylusUp += HostControlOnStylusUp;

		_hostControl.MouseMove += HostOnMouseMove;
		_hostControl.MouseDown += HostOnMouseDown;
		_hostControl.MouseUp += HostOnMouseUp;

		_hostControl.LostMouseCapture += HostOnMouseCaptureLost;

		// Hook for native events
		if (_hostControl.IsLoaded)
		{
			HookNative(null, null);
		}
		else
		{
			_hostControl.Loaded += HookNative;
		}

		void HookNative(object? sender, RoutedEventArgs? e)
		{
			_hostControl.Loaded -= HookNative;

			var win = Window.GetWindow(_hostControl);

			var fromDependencyObject = PresentationSource.FromDependencyObject(win);
			_hwndSource = fromDependencyObject as HwndSource;
			_hwndSource?.AddHook(OnWmMessage);
		}
	}

	public bool HasCapture => _hostControl.IsMouseCaptured;

	public CoreCursor PointerCursor
	{
		get => Mouse.OverrideCursor.ToCoreCursor();
		set => Mouse.OverrideCursor = value.ToCursor();
	}

	[NotImplemented]
	public Windows.Foundation.Point PointerPosition { get; }

	public void SetPointerCapture(PointerIdentifier pointer)
		=> _hostControl.CaptureMouse();

	public void SetPointerCapture()
		=> _hostControl.CaptureMouse();

	public void ReleasePointerCapture(PointerIdentifier pointer)
		=> _hostControl.ReleaseMouseCapture();

	public void ReleasePointerCapture()
		=> _hostControl.ReleaseMouseCapture();

	#region Native events
	private void HostOnMouseEvent(InputEventArgs args, TypedEventHandler<object, PointerEventArgs>? @event, [CallerArgumentExpression(nameof(@event))] string eventName = "")
	{
		var current = SynchronizationContext.Current;
		try
		{
			// Make sure WPF doesn't override our own SynchronizationContext.
			if (Windows.UI.Xaml.Application.ApplicationSynchronizationContext is { } syncContext)
			{
				SynchronizationContext.SetSynchronizationContext(syncContext);
			}

			var eventArgs = BuildPointerArgs(args);
			@event?.Invoke(this, eventArgs);
			_previous = eventArgs;
		}
		catch (Exception e)
		{
			this.Log().Error($"Failed to raise {eventName}", e);
		}
		finally
		{
			SynchronizationContext.SetSynchronizationContext(current);
		}
	}
	private void HostOnMouseEnter(object sender, WpfMouseEventArgs args)
	{
		HostOnMouseEvent(args, PointerEntered);
	}

	private void HostOnMouseLeave(object sender, WpfMouseEventArgs args)
	{
		HostOnMouseEvent(args, PointerExited);
	}


	private void HostControlOnStylusMove(object sender, StylusEventArgs args) => HostOnMouseEvent(args, PointerMoved);
	private void HostControlOnStylusDown(object sender, StylusEventArgs args) => HostOnMouseEvent(args, PointerPressed);
	private void HostControlOnStylusUp(object sender, StylusEventArgs args) => HostOnMouseEvent(args, PointerReleased);


	private void HostOnMouseMove(object sender, WpfMouseEventArgs args)
	{
		if (args.StylusDevice != null)
		{
			return;
		}

		HostOnMouseEvent(args, PointerMoved);
	}

	private void HostOnMouseDown(object sender, MouseButtonEventArgs args)
	{
		if (args.StylusDevice != null)
		{
			return;
		}

		HostOnMouseEvent(args, PointerPressed);
	}

	private void HostOnMouseUp(object sender, MouseButtonEventArgs args)
	{
		if (args.StylusDevice != null)
		{
			return;
		}

		HostOnMouseEvent(args, PointerReleased);
	}

	private void HostOnMouseCaptureLost(object sender, WpfMouseEventArgs args)
	{
		HostOnMouseEvent(args, PointerCaptureLost);
	}

	private IntPtr OnWmMessage(IntPtr hwnd, int msg, IntPtr wparamOriginal, IntPtr lparamOriginal, ref bool handled)
	{
		var wparam = (int)(((long)wparamOriginal) & 0xFFFFFFFF);
		var lparam = (int)(((long)lparamOriginal) & 0xFFFFFFFF);

		static short GetLoWord(int i) => (short)(i & 0xFFFF);
		static short GetHiWord(int i) => (short)(i >> 16);

		switch (msg)
		{
			case Win32Messages.WM_DPICHANGED:
				break;
			case Win32Messages.WM_MOUSEHWHEEL:
			case Win32Messages.WM_MOUSEWHEEL:
				{
					var keys = (MouseModifierKeys)GetLoWord(wparam);

					// Vertical: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-mousewheel
					// Horizontal: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-mousehwheel

					var l = (int)lparam;
					var screenPosition = new System.Windows.Point(GetLoWord(l), GetHiWord(l));
					var wpfPosition = _hostControl.PointFromScreen(screenPosition);
					var position = new Windows.Foundation.Point(wpfPosition.X, wpfPosition.Y);

					var properties = new PointerPointProperties
					{
						IsLeftButtonPressed = keys.HasFlag(MouseModifierKeys.MK_LBUTTON),
						IsMiddleButtonPressed = keys.HasFlag(MouseModifierKeys.MK_MBUTTON),
						IsRightButtonPressed = keys.HasFlag(MouseModifierKeys.MK_RBUTTON),
						IsXButton1Pressed = keys.HasFlag(MouseModifierKeys.MK_XBUTTON1),
						IsXButton2Pressed = keys.HasFlag(MouseModifierKeys.MK_XBUTTON2),
						IsHorizontalMouseWheel = msg == Win32Messages.WM_MOUSEHWHEEL,
						IsPrimary = true,
						IsInRange = true,
						MouseWheelDelta = (int)wparam >> 16
					}.SetUpdateKindFromPrevious(_previous?.CurrentPoint.Properties);

					var modifiers = VirtualKeyModifiers.None;
					if (keys.HasFlag(MouseModifierKeys.MK_SHIFT))
					{
						modifiers |= VirtualKeyModifiers.Shift;
					}
					if (keys.HasFlag(MouseModifierKeys.MK_CONTROL))
					{
						modifiers |= VirtualKeyModifiers.Control;
					}

					var point = new Windows.UI.Input.PointerPoint(
						frameId: FrameIdProvider.GetNextFrameId(),
						timestamp: (ulong)Environment.TickCount,
						device: PointerDevice.For(PointerDeviceType.Mouse),
						pointerId: 1,
						rawPosition: position,
						position: position,
						isInContact: properties.HasPressedButton,
						properties: properties
					);
					var ptArgs = new PointerEventArgs(point, modifiers);

					PointerWheelChanged?.Invoke(this, ptArgs);
					_previous = ptArgs;

					handled = ptArgs.Handled;
					break;
				}
		}

		return IntPtr.Zero;
	}
	#endregion

	#region Convert helpers
	private PointerEventArgs BuildPointerArgs(InputEventArgs args)
	{
		if (args is null)
		{
			throw new ArgumentNullException(nameof(args));
		}

		Point position;
		PointerPointProperties properties;

		uint pointerId;
		if (args is WpfMouseEventArgs mouseEventArgs)
		{
			pointerId = 1;
			position = mouseEventArgs.GetPosition(_hostControl);
			properties = new()
			{
				IsLeftButtonPressed = mouseEventArgs.LeftButton == MouseButtonState.Pressed,
				IsMiddleButtonPressed = mouseEventArgs.MiddleButton == MouseButtonState.Pressed,
				IsRightButtonPressed = mouseEventArgs.RightButton == MouseButtonState.Pressed,
				IsXButton1Pressed = mouseEventArgs.XButton1 == MouseButtonState.Pressed,
				IsXButton2Pressed = mouseEventArgs.XButton2 == MouseButtonState.Pressed,
				IsPrimary = true,
				IsInRange = true
			};
		}
		else if (args is StylusEventArgs stylusEventArgs)
		{
			pointerId = (uint)stylusEventArgs.StylusDevice.Id;
			position = stylusEventArgs.GetPosition(_hostControl);

			properties = new()
			{
				IsLeftButtonPressed = true,
				IsPrimary = true,
				IsInRange = !stylusEventArgs.InAir,
			};

			var stylusPointCollection = stylusEventArgs.GetStylusPoints(_hostControl);
			if (stylusPointCollection.Count > 0)
			{
				var stylusPoint = stylusPointCollection[0];

				properties.Pressure = stylusPoint.PressureFactor;

				if (stylusPoint.HasProperty(StylusPointProperties.Width) && stylusPoint.HasProperty(StylusPointProperties.Height))
				{
					var width = stylusPoint.GetPropertyValue(StylusPointProperties.Width);
					var height = stylusPoint.GetPropertyValue(StylusPointProperties.Height);

					// Consider enable the ContactRectRaw property.
					//properties.ContactRectRaw = new Rect(position.X, position.Y, width, height);
					properties.ContactRect = new Rect(position.X, position.Y, width, height);
				}

				if (stylusPoint.HasProperty(StylusPointProperties.XTiltOrientation))
				{
					var xTilt = stylusPoint.GetPropertyValue(StylusPointProperties.XTiltOrientation);
					properties.XTilt = xTilt;
				}

				if (stylusPoint.HasProperty(StylusPointProperties.YTiltOrientation))
				{
					var yTilt = stylusPoint.GetPropertyValue(StylusPointProperties.YTiltOrientation);
					properties.YTilt = yTilt;
				}
			}
		}
		else
		{
			throw new ArgumentException();
		}

		properties = properties.SetUpdateKindFromPrevious(_previous?.CurrentPoint.Properties);
		var modifiers = GetKeyModifiers();
		var point = new PointerPoint(
			frameId: FrameIdProvider.GetNextFrameId(),
			timestamp: (ulong)(args.Timestamp * TimeSpan.TicksPerMillisecond),
			device: GetPointerDevice(args),
			pointerId: pointerId,
			rawPosition: new Windows.Foundation.Point(position.X, position.Y),
			position: new Windows.Foundation.Point(position.X, position.Y),
			isInContact: properties.HasPressedButton,
			properties: properties
		);

		return new PointerEventArgs(point, modifiers);
	}

	private static PointerDevice GetPointerDevice(InputEventArgs args)
	{
		if (args is null)
		{
			throw new ArgumentNullException(nameof(args));
		}

		if (args is StylusEventArgs stylusEventArgs)
		{
			if (stylusEventArgs.StylusDevice.TabletDevice?.Type == TabletDeviceType.Touch)
			{
				return PointerDevice.For(PointerDeviceType.Touch);
			}
			else
			{
				return PointerDevice.For(PointerDeviceType.Pen);
			}
		}
		else
		{
			return PointerDevice.For(PointerDeviceType.Mouse);
		}
	}

	private static VirtualKeyModifiers GetKeyModifiers()
	{
		VirtualKeyModifiers modifiers = VirtualKeyModifiers.None;
		if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
		{
			modifiers |= VirtualKeyModifiers.Shift;
		}

		if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
		{
			modifiers |= VirtualKeyModifiers.Control;
		}

		if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
		{
			modifiers |= VirtualKeyModifiers.Menu;
		}

		if (Keyboard.IsKeyDown(Key.LWin) || Keyboard.IsKeyDown(Key.RWin))
		{
			modifiers |= VirtualKeyModifiers.Windows;
		}

		return modifiers;
	}
	#endregion
}
