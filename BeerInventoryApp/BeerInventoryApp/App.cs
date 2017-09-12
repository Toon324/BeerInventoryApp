
using BeerInventoryApp.Pages;
using System;
using Xamarin.Forms;

namespace BeerInventoryApp
{
    public interface IAuthenticate
    {
        void Login();

        bool IsAuthenticated();

        void Logout();

        string GetCurrentUser();

        event EventHandler Authentication;

        void OnAuthentication(EventArgs e);
    }

    public class App : Application
	{
        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        public App ()
		{
			MainPage = new NavigationPage(new BeerInventoryApp.MainPage());

            Authenticator.Authentication += OnAuthentication;
		}

		protected override void OnStart ()
		{
            MainPage = new MainPage();
		}

        private void OnAuthentication(object sender, EventArgs e)
        {
            MainPage = new HomePage(Authenticator);
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
