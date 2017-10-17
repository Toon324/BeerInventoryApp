using BeerInventoryApp.RestInterfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeerInventoryApp.Pages
{
    class SearchPage : ContentPage
    {
        private IAzureSearchApi searchApi = RestService.For<IAzureSearchApi>(AzureSearchApi.ApiUrl);

        public SearchPage()
        {
            var initialResults = searchApi.Search(AzureSearchApi.BeerIndex, "");

            var results = new StackLayout();

            foreach (var result in initialResults.Result.Results)
            {
                results.Children.Add(new StackLayout()
                {
                    Children =
                    {
                        new Label() { Text = result.Id + " | " + result.Name}
                    }
                });
            }

            var searchBar = new SearchBar
            {
                Placeholder = "Enter a search here.."
            };

            searchBar.SearchButtonPressed += SearchBar_SearchButtonPressed;

            searchBar.TextChanged += SearchBar_TextChanged;

            Content = new StackLayout
            {
                Children =
                {
                    results,
                    searchBar
                }
            };
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
