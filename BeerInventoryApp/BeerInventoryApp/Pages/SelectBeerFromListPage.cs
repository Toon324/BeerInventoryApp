using System;
using System.Collections.Generic;
using System.Text;
using BeerInventory.Models;
using Xamarin.Forms;

namespace BeerInventoryApp.Pages
{
    class SelectBeerFromListPage: ContentPage
    {
        private List<BeerEntity> foundBeer;

        public SelectBeerFromListPage(List<BeerEntity> foundBeer)
        {
            this.foundBeer = foundBeer;

            var beerOptions = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };

            foreach (var beer in foundBeer)
            {
                beerOptions.Children.Add(new Frame()
                {
                    Content = new StackLayout()
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = beer.Brewer + " | " + beer.Name
                            }
                        }
                    }
                });
            }

            beerOptions.Children.Add(new Frame()
            {
                Content = new StackLayout()
                {
                    Children =
                        {
                            new Label
                            {
                                Text = "My beer isn't on this list, I would like to submit it as a new one"
                            }
                        }
                }
            });

            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "We found multiple beers, please select the one you want"
                    },
                    beerOptions
                }
            };
        }
    }
}
