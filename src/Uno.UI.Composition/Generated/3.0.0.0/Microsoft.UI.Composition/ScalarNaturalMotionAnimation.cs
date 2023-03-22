#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Microsoft.UI.Composition
{
	#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
	[global::Uno.NotImplemented]
	#endif
	public  partial class ScalarNaturalMotionAnimation : global::Microsoft.UI.Composition.NaturalMotionAnimation
	{
		#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
		[global::Uno.NotImplemented("__ANDROID__", "__IOS__", "NET461", "__WASM__", "__SKIA__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public  float InitialVelocity
		{
			get
			{
				throw new global::System.NotImplementedException("The member float ScalarNaturalMotionAnimation.InitialVelocity is not implemented. For more information, visit https://aka.platform.uno/notimplemented?m=float%20ScalarNaturalMotionAnimation.InitialVelocity");
			}
			set
			{
				global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Microsoft.UI.Composition.ScalarNaturalMotionAnimation", "float ScalarNaturalMotionAnimation.InitialVelocity");
			}
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
		[global::Uno.NotImplemented("__ANDROID__", "__IOS__", "NET461", "__WASM__", "__SKIA__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public  float? InitialValue
		{
			get
			{
				throw new global::System.NotImplementedException("The member float? ScalarNaturalMotionAnimation.InitialValue is not implemented. For more information, visit https://aka.platform.uno/notimplemented?m=float%3F%20ScalarNaturalMotionAnimation.InitialValue");
			}
			set
			{
				global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Microsoft.UI.Composition.ScalarNaturalMotionAnimation", "float? ScalarNaturalMotionAnimation.InitialValue");
			}
		}
		#endif
		#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
		[global::Uno.NotImplemented("__ANDROID__", "__IOS__", "NET461", "__WASM__", "__SKIA__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public  float? FinalValue
		{
			get
			{
				throw new global::System.NotImplementedException("The member float? ScalarNaturalMotionAnimation.FinalValue is not implemented. For more information, visit https://aka.platform.uno/notimplemented?m=float%3F%20ScalarNaturalMotionAnimation.FinalValue");
			}
			set
			{
				global::Windows.Foundation.Metadata.ApiInformation.TryRaiseNotImplemented("Microsoft.UI.Composition.ScalarNaturalMotionAnimation", "float? ScalarNaturalMotionAnimation.FinalValue");
			}
		}
		#endif
		// Forced skipping of method Microsoft.UI.Composition.ScalarNaturalMotionAnimation.InitialVelocity.set
		// Forced skipping of method Microsoft.UI.Composition.ScalarNaturalMotionAnimation.FinalValue.get
		// Forced skipping of method Microsoft.UI.Composition.ScalarNaturalMotionAnimation.FinalValue.set
		// Forced skipping of method Microsoft.UI.Composition.ScalarNaturalMotionAnimation.InitialValue.get
		// Forced skipping of method Microsoft.UI.Composition.ScalarNaturalMotionAnimation.InitialValue.set
		// Forced skipping of method Microsoft.UI.Composition.ScalarNaturalMotionAnimation.InitialVelocity.get
	}
}
