using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Auth0.OidcClient;
using IdentityModel.OidcClient;
using Android.Content;

namespace BeerInventoryApp.Droid
{
	[Activity (Label = "BeerInventoryApp", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "com.xamarin.sample.beerinventoryapp",
        DataHost = "cswendrowski.auth0.com",
        DataPathPrefix = "/android/com.xamarin.sample.beerinventoryapp/callback")]
    public class MainActivity : FormsAppCompatActivity, IAuthenticate
	{
        private Auth0Client client;
        private AuthorizeState authorizeState;

        private bool isAuthenticated = false;
        private string currentUser = "";

        public async void Login()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                client = new Auth0Client(new Auth0ClientOptions
                {
                    Domain = "cswendrowski.auth0.com",
                    ClientId = "UMWesaiJJhwcssKPWNgpi5OJPUWwDiJk",
                    Activity = this
                });

                authorizeState = await client.PrepareLoginAsync();

                var uri = Android.Net.Uri.Parse(authorizeState.StartUrl);

                var intent = new Intent(Intent.ActionView, uri);
                intent.AddFlags(ActivityFlags.NoHistory);
                StartActivity(intent);

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

        }

        public bool IsAuthenticated()
        {
            return isAuthenticated;
        }

        public void Logout()
        {
            isAuthenticated = false;
        }

        public string GetCurrentUser()
        {
            return currentUser;
        }

        protected override async void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            var loginResult = await client.ProcessResponseAsync(intent.DataString, authorizeState);

            isAuthenticated = !loginResult.IsError;
            currentUser = loginResult.User.FindFirst("sub").Value;
        }

        protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

            // Initialize the authenticator before loading the app.
            App.Init((IAuthenticate)this);

            LoadApplication (new BeerInventoryApp.App ());
		}
	}
}

