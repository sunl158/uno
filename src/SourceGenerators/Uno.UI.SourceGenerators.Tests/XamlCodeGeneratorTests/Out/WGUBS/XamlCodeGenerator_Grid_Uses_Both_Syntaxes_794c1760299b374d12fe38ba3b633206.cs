﻿// <autogenerated />
#pragma warning disable CS0114
#pragma warning disable CS0108
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Uno.UI;
using Uno.UI.Xaml;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI.Text;
using Uno.Extensions;
using Uno;
using Uno.UI.Helpers;
using Uno.UI.Helpers.Xaml;
using MyProject;

#if __ANDROID__
using _View = Android.Views.View;
#elif __IOS__
using _View = UIKit.UIView;
#elif __MACOS__
using _View = AppKit.NSView;
#else
using _View = Microsoft.UI.Xaml.UIElement;
#endif

namespace Uno.UI.Tests.Windows_UI_XAML_Controls.GridTests.Controls
{
	partial class Grid_Uses_Both_Syntaxes : global::Microsoft.UI.Xaml.Controls.Page
	{
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		private const string __baseUri_prefix_Grid_Uses_Both_Syntaxes_794c1760299b374d12fe38ba3b633206 = "ms-appx:///TestProject/";
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		private const string __baseUri_Grid_Uses_Both_Syntaxes_794c1760299b374d12fe38ba3b633206 = "ms-appx:///TestProject/";
		private global::Microsoft.UI.Xaml.NameScope __nameScope = new global::Microsoft.UI.Xaml.NameScope();
		private void InitializeComponent()
		{
			NameScope.SetNameScope(this, __nameScope);
			var __that = this;
			base.IsParsing = true;
			// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 1:2)
			base.Content = 
			new global::Microsoft.UI.Xaml.Controls.Grid
			{
				IsParsing = true,
				Name = "grid",
				ColumnDefinitions = 
				{
					new global::Microsoft.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Microsoft.UI.Xaml.GridLength(1f, global::Microsoft.UI.Xaml.GridUnitType.Star)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Microsoft.UI.Xaml.GridLength(2f, global::Microsoft.UI.Xaml.GridUnitType.Star)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Microsoft.UI.Xaml.GridLength(1f, global::Microsoft.UI.Xaml.GridUnitType.Auto)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Microsoft.UI.Xaml.GridLength(1f, global::Microsoft.UI.Xaml.GridUnitType.Star)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Microsoft.UI.Xaml.GridLength(300f, global::Microsoft.UI.Xaml.GridUnitType.Pixel)
					}
					,
				}
				,
				RowDefinitions = 
				{
					new global::Microsoft.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Microsoft.UI.Xaml.GridLength(1f, global::Microsoft.UI.Xaml.GridUnitType.Star)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Microsoft.UI.Xaml.GridLength(1f, global::Microsoft.UI.Xaml.GridUnitType.Auto)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Microsoft.UI.Xaml.GridLength(25f, global::Microsoft.UI.Xaml.GridUnitType.Pixel)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Microsoft.UI.Xaml.GridLength(14f, global::Microsoft.UI.Xaml.GridUnitType.Pixel)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Microsoft.UI.Xaml.GridLength(20f, global::Microsoft.UI.Xaml.GridUnitType.Pixel)
					}
					,
				}
				,
				ColumnDefinitions = 
				{
					new global::Microsoft.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Microsoft.UI.Xaml.GridLength(1f, global::Microsoft.UI.Xaml.GridUnitType.Star),
						// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 18:14)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Microsoft.UI.Xaml.GridLength(2f, global::Microsoft.UI.Xaml.GridUnitType.Star),
						// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 19:14)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Microsoft.UI.Xaml.GridLength(1f, global::Microsoft.UI.Xaml.GridUnitType.Auto),
						// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 20:14)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Microsoft.UI.Xaml.GridLength(1f, global::Microsoft.UI.Xaml.GridUnitType.Star),
						// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 21:14)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Microsoft.UI.Xaml.GridLength(300f, global::Microsoft.UI.Xaml.GridUnitType.Pixel),
						// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 22:14)
					}
					,
				}
				,
				RowDefinitions = 
				{
					new global::Microsoft.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Microsoft.UI.Xaml.GridLength(1f, global::Microsoft.UI.Xaml.GridUnitType.Star),
						// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 25:14)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Microsoft.UI.Xaml.GridLength(1f, global::Microsoft.UI.Xaml.GridUnitType.Auto),
						// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 26:14)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Microsoft.UI.Xaml.GridLength(25f, global::Microsoft.UI.Xaml.GridUnitType.Pixel),
						// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 27:14)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Microsoft.UI.Xaml.GridLength(14f, global::Microsoft.UI.Xaml.GridUnitType.Pixel),
						// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 28:14)
					}
					,
					new global::Microsoft.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Microsoft.UI.Xaml.GridLength(20f, global::Microsoft.UI.Xaml.GridUnitType.Pixel),
						// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 29:14)
					}
					,
				}
				,
				// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 11:6)
			}
			.Grid_Uses_Both_Syntaxes_794c1760299b374d12fe38ba3b633206_XamlApply((Grid_Uses_Both_Syntaxes_794c1760299b374d12fe38ba3b633206XamlApplyExtensions.XamlApplyHandler2)(c10 => 
			{
				__nameScope.RegisterName("grid", c10);
				__that.grid = c10;
				// FieldModifier public
				global::Uno.UI.FrameworkElementHelper.SetBaseUri(c10, __baseUri_Grid_Uses_Both_Syntaxes_794c1760299b374d12fe38ba3b633206);
				c10.CreationComplete();
			}
			))
			;
			
			this
			.GenericApply(((c11) => 
			{
				// Source 0\Grid_Uses_Both_Syntaxes.xaml (Line 1:2)
				
				// WARNING Property c11.base does not exist on {http://schemas.microsoft.com/winfx/2006/xaml/presentation}Page, the namespace is http://www.w3.org/XML/1998/namespace. This error was considered irrelevant by the XamlFileGenerator
			}
			))
			.GenericApply(((c12) => 
			{
				// Class Uno.UI.Tests.Windows_UI_XAML_Controls.GridTests.Controls.Grid_Uses_Both_Syntaxes
				global::Uno.UI.FrameworkElementHelper.SetBaseUri(c12, __baseUri_Grid_Uses_Both_Syntaxes_794c1760299b374d12fe38ba3b633206);
				c12.CreationComplete();
			}
			))
			;
			OnInitializeCompleted();

		}
		partial void OnInitializeCompleted();
		private global::Microsoft.UI.Xaml.Data.ElementNameSubject _gridSubject = new global::Microsoft.UI.Xaml.Data.ElementNameSubject();
		public global::Microsoft.UI.Xaml.Controls.Grid grid
		{
			get
			{
				return (global::Microsoft.UI.Xaml.Controls.Grid)_gridSubject.ElementInstance;
			}
			set
			{
				_gridSubject.ElementInstance = value;
			}
		}
	}
}
namespace MyProject
{
	static class Grid_Uses_Both_Syntaxes_794c1760299b374d12fe38ba3b633206XamlApplyExtensions
	{
		public delegate void XamlApplyHandler0(global::Microsoft.UI.Xaml.Controls.ColumnDefinition instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Microsoft.UI.Xaml.Controls.ColumnDefinition Grid_Uses_Both_Syntaxes_794c1760299b374d12fe38ba3b633206_XamlApply(this global::Microsoft.UI.Xaml.Controls.ColumnDefinition instance, XamlApplyHandler0 handler)
		{
			handler(instance);
			return instance;
		}
		public delegate void XamlApplyHandler1(global::Microsoft.UI.Xaml.Controls.RowDefinition instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Microsoft.UI.Xaml.Controls.RowDefinition Grid_Uses_Both_Syntaxes_794c1760299b374d12fe38ba3b633206_XamlApply(this global::Microsoft.UI.Xaml.Controls.RowDefinition instance, XamlApplyHandler1 handler)
		{
			handler(instance);
			return instance;
		}
		public delegate void XamlApplyHandler2(global::Microsoft.UI.Xaml.Controls.Grid instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Microsoft.UI.Xaml.Controls.Grid Grid_Uses_Both_Syntaxes_794c1760299b374d12fe38ba3b633206_XamlApply(this global::Microsoft.UI.Xaml.Controls.Grid instance, XamlApplyHandler2 handler)
		{
			handler(instance);
			return instance;
		}
	}
}
