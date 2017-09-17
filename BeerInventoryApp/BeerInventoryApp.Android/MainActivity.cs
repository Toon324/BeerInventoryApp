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
	[Activity (Label = "BeerInventoryApp",
        Icon = "@drawable/icon",
        Theme="@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "beerinventoryapp.android",
        DataHost = "cswendrowski.auth0.com",
        DataPathPrefix = "/android/beerinventoryapp.android/callback")]
    public class MainActivity : FormsApplicationActivity, IAuthenticate
	{
        private Auth0Client client;
        private AuthorizeState authorizeState;

        private bool isAuthenticated = false;
        private string currentUser = "";

        public event EventHandler Authentication;

        public async void Login()
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

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage("ID Token: " + loginResult.IdentityToken.Substring(0, 10));
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

            isAuthenticated = !loginResult.IsError;
            currentUser = loginResult.User.FindFirst("sub").Value;

            OnAuthentication(EventArgs.Empty);
        }

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

            // Initialize the authenticator before loading the app.
            App.Init((IAuthenticate)this);

            LoadApplication (new BeerInventoryApp.App ());
		}

        public void OnAuthentication(EventArgs e)
        {
            Authentication?.Invoke(this, e);
        }
    }
}

