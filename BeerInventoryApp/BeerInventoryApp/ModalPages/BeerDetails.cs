﻿using BeerInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeerInventoryApp.ModalPages
{
    public class BeerDetails : ContentPage
    {
        int count = 1;
        string location = "None";
        bool shouldAdd = true;
        Button submitButton;
        IEnumerable<InventoryDetails> Details { get; set; }

        public BeerDetails(IEnumerable<InventoryDetails> details)
        {
            Details = details;

            var beer = details.First();

            var locations = details.Select(x => x.Location).Distinct().ToList();
            locations.Add("New Location");

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
                            new Label()
                            {
                                Text = inventory.Count.ToString(),
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }
                        }
                    }
                });
            }

            var addRemoveSwitch = new Switch
            {
                IsToggled = true
            };

            addRemoveSwitch.Toggled += AddRemoveSwitch_Toggled;

            var locationPicker = new Picker
            {
                VerticalOptions = LayoutOptions.Start,
                Title = "Select a Location",
                ItemsSource = locations
            };

            locationPicker.SelectedIndexChanged += LocationPicker_SelectedIndexChanged;

            submitButton = new Button
            {
                VerticalOptions = LayoutOptions.End,
                Text = "Select a Location",
                IsEnabled = false
            };

            submitButton.Clicked += SubmitButton_Clicked;

            var amountEntry = new Entry
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Keyboard = Keyboard.Numeric,
                MinimumWidthRequest = 100,
                WidthRequest = 100,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "1"
            };

            amountEntry.TextChanged += AmountEntry_TextChanged;

            Content = new StackLayout
            {
                Children =
                    {
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.Start,
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
                                    VerticalOptions = LayoutOptions.Center,
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
                                    VerticalOptions = LayoutOptions.Center,
                                    Children =
                                    {
                                        new Label
                                        {
                                            Text = "Style: " + beer.Type
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

                        new StackLayout
                        {
                            Margin = new Thickness(20, 10),
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Children =
                            {
                                new Label
                                {
                                    Text = beer.Description
                                }
                            }
                        },
                        
                        new StackLayout
                        {
                            Margin = 20,
                            Orientation = StackOrientation.Vertical,
                            VerticalOptions = LayoutOptions.End,
                            Children =
                            {
                                inventoryDetails,
                                locationPicker,
                                new StackLayout
                                {
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    Orientation = StackOrientation.Horizontal,
                                    Children =
                                    {
                                        new Label
                                        {
                                            VerticalOptions = LayoutOptions.Center,
                                            Text = "Remove"
                                        },
                                        addRemoveSwitch,
                                        new Label
                                        {
                                            VerticalOptions = LayoutOptions.Center,
                                            Text = "Add"
                                        },
                                        amountEntry
                                    }
                                },
                                submitButton
                            }
                        }
                }
            };
        }

        private void AmountEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                return;
            }

            count = int.Parse(e.NewTextValue);

            UpdateButton();
        }

        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void LocationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;

            location = picker.SelectedItem.ToString();

            UpdateButton();
        }

        private void AddRemoveSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            shouldAdd = e.Value;

            UpdateButton();
        }

        private void UpdateButton()
        {
            if (location == "None" || location == "New Location")
            {
                submitButton.IsEnabled = false;
                submitButton.Text = "Select a Location";
                return;
            }

            submitButton.IsEnabled = true;

            if (shouldAdd)
            {
                submitButton.Text = "Add " + count + " to " + location;
            }
            else
            {
                var amountAtLocation = Details.Where(x => x.Location == location).First().Count;

                if (amountAtLocation < count)
                {
                    submitButton.Text = "Can't Remove " + count + " from " + location;
                    submitButton.IsEnabled = false;
                }
                else
                {
                    submitButton.Text = "Remove " + count + " to " + location;
                }
            }
        }
    }
}
