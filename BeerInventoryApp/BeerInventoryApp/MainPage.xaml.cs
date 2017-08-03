using BeerInventory.Models;
using BeerInventoryApp.Data;
using BeerInventoryApp.ModalPages;
using BeerInventoryApp.Services;
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
        ObservableCollection<Grouping<InventoryDetails, InventoryDetails>> Items { get; set; } = new ObservableCollection<Grouping<InventoryDetails, InventoryDetails>>();

        private String name = "Cody";

        private int lastResultCount = 0;

        public MainPage()
        {
            InitializeComponent();

            UpdateItems();
        }


        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((InventoryDetails)e.SelectedItem);

            var details = Items.First(x => x.Key.Id == item.Id).GetItems();

            await Navigation.PushAsync(new BeerDetails(details, name, item.Id));
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = e.NewTextValue;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            name = this.FindByName<Entry>("Name_Entry").Text;
            UpdateItems();
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

        private void Entry_Completed(object sender, EventArgs e)
        {
            name = ((Entry)sender).Text;
            UpdateItems();
        }

        private void UpdateItems()
        {
            if (string.IsNullOrEmpty(name))
                return;

            var inventory = InventoryService.GetInventory(name);

            var sorted = inventory
                .GroupBy(x => x.Id)
                .Select(x => new Grouping<InventoryDetails, InventoryDetails>(x.First(), x));

            Items = new ObservableCollection<Grouping<InventoryDetails, InventoryDetails>>(sorted);

            listView.ItemsSource = Items;
            lastResultCount = Items.Count();
        }

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
    }
}
