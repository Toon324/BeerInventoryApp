using Auth0.OidcClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BeerInventoryApp.UWP
{
    public sealed partial class MainPage: IAuthenticate
    {
        public MainPage()
        {
            this.InitializeComponent();

            BeerInventoryApp.App.Init(this);

            LoadApplication(new BeerInventoryApp.App());
        }

        #region authentication

        private Auth0Client client;

        private bool isAuthenticated = false;

        private string currentUser = "";

        public event EventHandler Authentication;

        public async void Login()
        {
            client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = "cswendrowski.auth0.com",
                ClientId = "UMWesaiJJhwcssKPWNgpi5OJPUWwDiJk"
            });

            var loginResult = await client.LoginAsync();

            isAuthenticated = !loginResult.IsError;
            currentUser = loginResult.User.FindFirst("sub").Value;

            OnAuthentication(EventArgs.Empty);
        }

        public string GetCurrentUser()
        {
            return currentUser;
        }

        public bool IsAuthenticated()
        {
            return isAuthenticated;
        }

        public void Logout()
        {
            isAuthenticated = false;
        }

        public void OnAuthentication(EventArgs e)
        {
            Authentication?.Invoke(this, e);
        }
    }
    #endregion
}
