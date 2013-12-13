// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace GoogleDriveSample
{
	[Register ("GoogleDriveSampleViewController")]
	partial class GoogleDriveSampleViewController
	{
		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UIButton btnReload { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UIView myUIView { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextView txtOutput { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnReload != null) {
				btnReload.Dispose ();
				btnReload = null;
			}

			if (txtOutput != null) {
				txtOutput.Dispose ();
				txtOutput = null;
			}

			if (myUIView != null) {
				myUIView.Dispose ();
				myUIView = null;
			}
		}
	}
}
