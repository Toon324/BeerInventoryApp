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

        public MainPage()
        {
            InitializeComponent();

            var inventory = InventoryService.GetInventory("Cody");

            foreach (var beer in inventory)
            {
                Items.Add(beer);
            }

            listView.ItemsSource = Items;
        }


        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var name = e.SelectedItem.ToString();

            editor.Text = name;
        }
    }
}
