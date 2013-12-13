using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Auth;

namespace GoogleDriveSample {
	public partial class GoogleDriveSampleViewController : UIViewController {
		public GoogleDriveSampleViewController (IntPtr handle) : base (handle)
		{
		}

//		My Client Secret : 
//		CLIENT ID
//		319527390597-vn18tmvmd0topph53ou9dg23ang81vq3.apps.googleusercontent.com
//		CLIENT SECRET
//		9jBrMozLZ2JsqOM-IZRmCGGx
// 		Redirect URI
//		urn:ietf:wg:oauth:2.0:oob
//		http://localhost

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();




			btnReload.TouchUpInside += (sender, e) => {
				// do something. 

				Console.WriteLine("Reload Pressed");

				// Connect to it? 
				// Log in ? OAUTH?
				// Read Folders under the Root Folder
				// Display Folders
			};
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			var auth = new OAuth2Authenticator (
				clientId: "319527390597-vn18tmvmd0topph53ou9dg23ang81vq3.apps.googleusercontent.com",
				scope: "",
				authorizeUrl: new Uri ("https://accounts.google.com/o/oauth2/auth"),
				redirectUrl: new Uri ("urn:ietf:wg:oauth:2.0:oob"));

			auth.Completed += (sender, eventArgs) => {
				// We presented the UI, so it's up to us to dimiss it on iOS.
				DismissViewController (true, null);

				if (eventArgs.IsAuthenticated) {
					// label
					Console.WriteLine("Authenticated!!!!");
					// Use eventArgs.Account to do wonderful things
				} else {
					// The user cancelled
				}
			};

			PresentViewController (auth.GetUI (), true, null);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

