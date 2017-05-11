using BeerInventory.Models;
using BeerInventoryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeerInventoryApp
{
	public partial class MainPage : ContentPage
	{
        ObservableCollection<InventoryDetails> Items { get; set; } = new ObservableCollection<InventoryDetails>();

        InventoryService InventoryService { get; set; } = new InventoryService();

        private String name = "Cody";

        private int lastResultCount = 0;

        public MainPage()
        {
            InitializeComponent();

            UpdateItems();
        }


        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var name = e.SelectedItem.ToString();

            editor.Text = name;
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

            var newList = new ObservableCollection<InventoryDetails>();

            foreach (var beer in inventory)
            {
                newList.Add(beer);
            }

            Items = newList;

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

            var results = new ObservableCollection<InventoryDetails>(Items.Where(x => x.Name.ToLower().Contains(searchTerm) || searchTerm.ToLower().Contains(x.Name)).ToList());

            // TODO: Probably hacky, should rework
            if (results.Count != lastResultCount)
            {
                lastResultCount = results.Count;
                listView.ItemsSource = results;
            }
        }
    }
}
