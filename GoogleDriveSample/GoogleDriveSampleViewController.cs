using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Auth;
using Newtonsoft.Json;

namespace GoogleDriveSample {
	public partial class GoogleDriveSampleViewController : UIViewController {

		Account myAccount;
		OAuth2Authenticator auth;

		public GoogleDriveSampleViewController (IntPtr handle) : base (handle)
		{
			string clientId = "471493543817-sju0qk7g1m5ca7puc00hahn82n2vgqnr.apps.googleusercontent.com";
			string clientSecret = "HiyhlLM7o8H-mUhlZ5sxAUwc";
			string scope = "https://www.googleapis.com/auth/drive.readonly";

			auth = new OAuth2Authenticator (
				clientId, 
				clientSecret, 
				scope,
				authorizeUrl: new Uri ("https://accounts.google.com/o/oauth2/auth"),
				redirectUrl: new Uri ("http://localhost/"),
				accessTokenUrl: new Uri ("https://accounts.google.com/o/oauth2/token"));

			auth.Completed += (sender, eventArgs) => {
				// We presented the UI, so it's up to us to dimiss it on iOS.
				DismissViewController (true, null);

				if (eventArgs.IsAuthenticated) {
					// Use eventArgs.Account to do wonderful things

					Console.WriteLine("Authenticated!!!!");

					myAccount = eventArgs.Account;

					//save the account in the Acccount Store (Account Store handles storing into the device's keychain)
					AccountStore.Create ().Save (eventArgs.Account, "GoogleDrive");
				} else {
					// The user cancelled
				}
			};

			auth.AllowCancel = true;

			auth.Error += (sender, e) => {
				Console.WriteLine("Authentication Error");
				Console.WriteLine(e.Exception);
				Console.WriteLine(e.Message);
			};
		}

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

			foreach (Account i in AccountStore.Create ().FindAccountsForService ("GoogleDrive"))
			{
				if (i != null)
					myAccount = i;
			}
			bool Busy = false;

			btnReload.TouchUpInside += (sender, e) => {
				// do something. 

				Console.WriteLine("Reload Pressed");

				// Google Drive API: https://developers.google.com/drive/v2/reference/
				// GET https://www.googleapis.com/drive/v2/files
				// maxResults , int  (default 100, optional)
				// q, string  (search string, optional)
				// pageToken, string (optional)
				if (myAccount == null)
				{
					PresentViewController (auth.GetUI (), true, null);
				}

				if (!Busy)
				{
					var parameters = new System.Collections.Generic.Dictionary<string, string>();
					parameters.Add("maxResults", "10");
					var request = new OAuth2Request("GET", new Uri("https://www.googleapis.com/drive/v2/files"), parameters, myAccount); 

					request.GetResponseAsync ().ContinueWith( t => { 
						if (t.IsFaulted)
						{
							Console.WriteLine("Request: Error processing request");
							foreach (Exception ex in t.Exception.InnerExceptions)
							{
								Console.WriteLine(ex.StackTrace);
							}

							Busy = false; 
							myAccount = null;
						}
						var response = t.Result.GetResponseText();
						Console.WriteLine(response);
					
						var results = Newtonsoft.Json.Linq.JObject.Parse(response);
						Console.WriteLine(results.Count);

						Busy = false;
						//return t.Result;
					});
				}
				else {
					Console.WriteLine("Busy... still processing last request");
				}
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (myAccount != null) {
				return;
			}
	
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

