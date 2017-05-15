using BeerInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeerInventoryApp.ModalPages
{
	public class BeerDetails : ContentPage
	{
		public BeerDetails (IEnumerable<InventoryDetails> details)
		{
            var beer = details.First();

            var inventoryDetails = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center
            };

            foreach (var inventory in details)
            {
                inventoryDetails.Children.Add(new Frame()
                {
                    Content = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                        Children =
                        {
                            new Label() {
                                Text = inventory.Location,
                                FontAttributes = FontAttributes.Bold,
                                FontSize = 20,
                                HorizontalOptions = LayoutOptions.Center
                            },
                            new StackLayout()
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                {
                                    new Button()
                                    {
                                        Text = "-",
                                        FontSize = 20,
                                        WidthRequest = 50,
                                        HeightRequest = 50
                                    },
                                    new Label()
                                    {
                                        Text = inventory.Count.ToString(),
                                        FontAttributes = FontAttributes.Bold,
                                        HorizontalOptions = LayoutOptions.Center,
                                        VerticalOptions = LayoutOptions.Center
                                    },
                                    new Button()
                                    {
                                        Text = "+",
                                        FontSize = 20,
                                        WidthRequest = 50,
                                        HeightRequest = 50
                                    }
                                }
                            }
                        }
                    }
                });
            }

            Content = new StackLayout {
                Children = {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Margin = 20,
                        Children =
                        {
                            new Image
                            {
                                Source = beer.LabelUrl
                            },
                            new StackLayout
                            {
                                Margin = new Thickness(10, 20),
                                Children =
                                {
                                    new Label
                                    {
                                        Text = beer.Brewer
                                    },
                                    new Label
                                    {
                                        Text = beer.Name
                                    },
                                    new Label
                                    {
                                        Text = beer.ABV.ToString() + "%"
                                    }
                                }
                            },
                            new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        Text = beer.Type
                                    },
                                    new Label
                                    {
                                        Text = beer.Availablity
                                    },
                                    new Label
                                    {
                                        Text = beer.Glass
                                    }
                                }
                            }
                        }
                    },
                    new Label
                    {
                        Margin = new Thickness(20, 10),
                        Text = beer.Description
                    },
                    inventoryDetails
                }
			};
		}
	}
}
