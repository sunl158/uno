#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Microsoft.Web.WebView2.Core
{
	#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
	[global::Uno.NotImplemented]
	#endif
	public  partial class CoreWebView2AcceleratorKeyPressedEventArgs 
	{
		#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
		[global::Uno.NotImplemented("__ANDROID__", "__IOS__", "NET461", "__WASM__", "__SKIA__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public  bool Handled
		{
			get
			{
				throw new global::System.NotImplementedException("The member bool CoreWebView2AcceleratorKeyPressedEventArgs.Handled is not implemented. For more information, visit https://aka.platform.uno/notimplemented?m=bool%20CoreWebView2AcceleratorKeyPressedEventArgs.Handled");
			}
			set
			{
				global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Microsoft.Web.WebView2.Core.CoreWebView2AcceleratorKeyPressedEventArgs", "bool CoreWebView2AcceleratorKeyPressedEventArgs.Handled");
			}
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
		[global::Uno.NotImplemented("__ANDROID__", "__IOS__", "NET461", "__WASM__", "__SKIA__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public  global::Microsoft.Web.WebView2.Core.CoreWebView2KeyEventKind KeyEventKind
		{
			get
			{
				throw new global::System.NotImplementedException("The member CoreWebView2KeyEventKind CoreWebView2AcceleratorKeyPressedEventArgs.KeyEventKind is not implemented. For more information, visit https://aka.platform.uno/notimplemented?m=CoreWebView2KeyEventKind%20CoreWebView2AcceleratorKeyPressedEventArgs.KeyEventKind");
			}
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
		[global::Uno.NotImplemented("__ANDROID__", "__IOS__", "NET461", "__WASM__", "__SKIA__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public  int KeyEventLParam
		{
			get
			{
				throw new global::System.NotImplementedException("The member int CoreWebView2AcceleratorKeyPressedEventArgs.KeyEventLParam is not implemented. For more information, visit https://aka.platform.uno/notimplemented?m=int%20CoreWebView2AcceleratorKeyPressedEventArgs.KeyEventLParam");
			}
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
		[global::Uno.NotImplemented("__ANDROID__", "__IOS__", "NET461", "__WASM__", "__SKIA__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public  global::Microsoft.Web.WebView2.Core.CoreWebView2PhysicalKeyStatus PhysicalKeyStatus
		{
			get
			{
				throw new global::System.NotImplementedException("The member CoreWebView2PhysicalKeyStatus CoreWebView2AcceleratorKeyPressedEventArgs.PhysicalKeyStatus is not implemented. For more information, visit https://aka.platform.uno/notimplemented?m=CoreWebView2PhysicalKeyStatus%20CoreWebView2AcceleratorKeyPressedEventArgs.PhysicalKeyStatus");
			}
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
		[global::Uno.NotImplemented("__ANDROID__", "__IOS__", "NET461", "__WASM__", "__SKIA__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public  uint VirtualKey
		{
			get
			{
				throw new global::System.NotImplementedException("The member uint CoreWebView2AcceleratorKeyPressedEventArgs.VirtualKey is not implemented. For more information, visit https://aka.platform.uno/notimplemented?m=uint%20CoreWebView2AcceleratorKeyPressedEventArgs.VirtualKey");
			}
		}
		#endif
		// Forced skipping of method Microsoft.Web.WebView2.Core.CoreWebView2AcceleratorKeyPressedEventArgs.KeyEventKind.get
		// Forced skipping of method Microsoft.Web.WebView2.Core.CoreWebView2AcceleratorKeyPressedEventArgs.VirtualKey.get
		// Forced skipping of method Microsoft.Web.WebView2.Core.CoreWebView2AcceleratorKeyPressedEventArgs.KeyEventLParam.get
		// Forced skipping of method Microsoft.Web.WebView2.Core.CoreWebView2AcceleratorKeyPressedEventArgs.PhysicalKeyStatus.get
		// Forced skipping of method Microsoft.Web.WebView2.Core.CoreWebView2AcceleratorKeyPressedEventArgs.Handled.get
		// Forced skipping of method Microsoft.Web.WebView2.Core.CoreWebView2AcceleratorKeyPressedEventArgs.Handled.set
	}
}
