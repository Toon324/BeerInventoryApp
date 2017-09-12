using Auth0.OidcClient;
using BeerInventory.Models;
using BeerInventoryApp.Data;
using BeerInventoryApp.ModalPages;
using BeerInventoryApp.Pages;
using BeerInventoryApp.RestInterfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace BeerInventoryApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void Login_Button_Clicked(object sender, EventArgs e)
        {
            App.Authenticator.Login();
        }

    }  
}
