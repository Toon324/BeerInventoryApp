using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace BeerInventoryApp
{
	public class CustomScannerPage : ContentPage
	{
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;

        public CustomScannerPage () : base()
		{
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(() => {
                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    zxing.IsAnalyzing = false;

                    OnUpcResultEvent(result.Text);
                });
            overlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone up to the barcode",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = zxing.HasTorch,
            };
            overlay.FlashButtonClicked += (sender, e) => {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            var upcEntry = new Entry { Placeholder = "UPC", HorizontalOptions = LayoutOptions.FillAndExpand };
            upcEntry.Completed += (sender, e) =>
            Device.BeginInvokeOnMainThread(() =>
            {
                zxing.IsAnalyzing = false;
                zxing.IsScanning = false;
                zxing.IsEnabled = false;
                OnUpcResultEvent(((Entry)sender).Text);
            });

            var endButton = new Button { Text = "Enter", HorizontalOptions = LayoutOptions.End };
            endButton.Clicked += (sender, e) =>
            Device.BeginInvokeOnMainThread(() =>
            {
                zxing.IsAnalyzing = false;
                zxing.IsScanning = false;
                zxing.IsEnabled = false;
                OnUpcResultEvent(upcEntry.Text);
            });


            var brewerEntry = new Entry { Placeholder = "Brewery Name", HorizontalOptions = LayoutOptions.FillAndExpand, Text = "Left Hand Brewing Company" };
            var beerEntry = new Entry { Placeholder = "Beer Name", HorizontalOptions = LayoutOptions.FillAndExpand, Text = "Milk Stout Nitro" };

            var manualEndButton = new Button { Text = "Enter", HorizontalOptions = LayoutOptions.End };
            manualEndButton.Clicked += (sender, e) =>
            Device.BeginInvokeOnMainThread(() =>
            {
                zxing.IsAnalyzing = false;
                zxing.IsScanning = false;
                zxing.IsEnabled = false;
                OnManualResultEvent(brewerEntry.Text + "|" + beerEntry.Text);
            });


            var content = new StackLayout
            {
                Children = {
                    grid,
                    new StackLayout
                    {
                        Margin = new Thickness(20, 20, 20, 5),
                        Children =
                        {
                            new Label { Text = "Or, manually enter your UPC", FontSize = 20 },
                            new StackLayout {
                                Orientation = StackOrientation.Horizontal,
                                Children = {
                                    upcEntry,
                                    endButton
                                }
                            }
                        }
                    },
                    new StackLayout
                    {
                        Margin = new Thickness(20, 0, 20, 20),
                        Children =
                        {
                            new Label { Text = "Or even just lookup manually by name!", FontSize = 20 },
                            new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                {
                                    brewerEntry,
                                    beerEntry,
                                    manualEndButton
                                }
                            }
                        }
                    }
                }
            };

            Content = content;
		}

        public event UpcResultDelegate OnUpcResult;

        public delegate void UpcResultDelegate(String upc);

        protected void OnUpcResultEvent(String text)
        {
            OnUpcResult(text);
        }

        public event ManualResultDelegate OnManualResult;
        
        public delegate void ManualResultDelegate(String upc);

        protected void OnManualResultEvent(String text)
        {
            OnManualResult(text);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }
    }
}