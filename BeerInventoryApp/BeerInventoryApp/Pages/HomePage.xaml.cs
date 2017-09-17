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

        private IBeerInventoryApi InventoryApi;

        public IAuthenticate Authenticate;

        public HomePage (IAuthenticate authenticator)
		{
            Authenticate = authenticator;

            InventoryApi = RestService.For<IBeerInventoryApi>(BeerInventoryApi.ApiUrl);

            InitializeComponent();

            nameLabel.Text = "Logged in as: " + authenticator.GetCurrentUser();

            UpdateItems();
		}

        #region Events
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((InventoryDetails)e.SelectedItem);

            await Navigation.PushAsync(new BeerDetails(item.Id));
        }

        private async void Scan_Button_Clicked(object sender, EventArgs e)
        {
            var scannerPage = new CustomScannerPage();
            await Navigation.PushAsync(scannerPage);

            scannerPage.OnUpcResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    ShowNextPage(result);
                });
            };

            scannerPage.OnManualResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    ShowNextPage("", result.Split('|')[0], result.Split('|')[1]);
                });
            };
        }

        private async void ShowNextPage(string upc = "", string brewery = "", string beerName = "")
        {
            List<BeerEntity> foundBeer = new List<BeerEntity>();

            if (!string.IsNullOrEmpty(upc))
            {
                try
                {
                    foundBeer = await InventoryApi.GetBeerByUPC(upc);
                }
                catch (Exception e) { }
            }

            if (!string.IsNullOrEmpty(brewery) && !string.IsNullOrEmpty(beerName))
            {
                try
                {
                    foundBeer = new List<BeerEntity> { await InventoryApi.GetBeerByBreweryAndName(brewery, beerName) };
                }
                catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            if (!foundBeer.Any())
            {
                await Navigation.PushAsync(new AddToDb(upc, brewery, beerName));
            }
            else if (foundBeer.Count > 1)
            {
                await Navigation.PushAsync(new SelectBeerFromListPage(foundBeer));
            }
            else // Only one found
            {
                var beerId = foundBeer.First().Id;
                await Navigation.PushAsync(new BeerDetails(beerId));
            }
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