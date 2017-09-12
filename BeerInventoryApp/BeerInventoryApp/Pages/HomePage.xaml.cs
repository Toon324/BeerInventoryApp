using BeerInventory.Models;
using BeerInventoryApp.Data;
using BeerInventoryApp.ModalPages;
using BeerInventoryApp.RestInterfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeerInventoryApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        ObservableCollection<Grouping<InventoryDetails, InventoryDetails>> Items { get; set; } = new ObservableCollection<Grouping<InventoryDetails, InventoryDetails>>();

        private int lastResultCount = 0;

        private IBeerInventoryApi InventoryApi = RestService.For<IBeerInventoryApi>(BeerInventoryApi.ApiUrl);

        public IAuthenticate Authenticate;

        public HomePage (IAuthenticate authenticator)
		{
            Authenticate = authenticator;

			InitializeComponent();

            nameLabel.Text = "Logged in as: " + authenticator.GetCurrentUser();

            UpdateItems();
		}

        #region Events
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((InventoryDetails)e.SelectedItem);

            var details = Items.First(x => x.Key.Id == item.Id).GetItems();

            await Navigation.PushAsync(new BeerDetails(details, item.Id));
        }

        private async void Scan_Button_Clicked(object sender, EventArgs e)
        {
            var scannerPage = new CustomScannerPage();
            await Navigation.PushAsync(scannerPage);

            scannerPage.OnUpcResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new AddToDb(result));
                });
            };
        }
        #endregion

        private void UpdateItems()
        {
            if (string.IsNullOrEmpty(Authenticate.GetCurrentUser()))
                return;

            var inventory = InventoryApi.GetInventoryByOwner(App.Authenticator.GetCurrentUser()).Result;

            var sorted = inventory
                .GroupBy(x => x.Id)
                .Select(x => new Grouping<InventoryDetails, InventoryDetails>(x.First(), x));

            Items = new ObservableCollection<Grouping<InventoryDetails, InventoryDetails>>(sorted);

            listView.ItemsSource = Items;
            lastResultCount = Items.Count();
        }

        #region Search
        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var searchTerm = ((SearchBar)sender).Text.ToLower();

            UpdateSearchResults(searchTerm);
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue.ToLower();

            UpdateSearchResults(searchTerm);
        }

        private void UpdateSearchResults(String searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                listView.ItemsSource = Items;
            }

            var results = new ObservableCollection<Grouping<InventoryDetails, InventoryDetails>>(
                Items.Where(x => x.Key.Name.ToLower().Contains(searchTerm) ||
                searchTerm.ToLower().Contains(x.Key.Name)
                ).ToList());

            // TODO: Probably hacky, should rework
            if (results.Count != lastResultCount)
            {
                lastResultCount = results.Count;
                listView.ItemsSource = results;
            }
        }
        #endregion
    }
}