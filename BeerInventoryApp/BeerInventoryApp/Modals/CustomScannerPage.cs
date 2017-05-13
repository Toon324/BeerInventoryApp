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

            var endButton = new Button { Text = "Enter", HorizontalOptions = LayoutOptions.End };
            endButton.Clicked += (sender, e) =>
            Device.BeginInvokeOnMainThread(() =>
            {
                zxing.IsAnalyzing = false;
                OnUpcResultEvent(((Button)sender).Text);
            });

            var upcEntry = new Entry { Placeholder = "UPC", HorizontalOptions = LayoutOptions.FillAndExpand };
            upcEntry.Completed += (sender, e) =>
            Device.BeginInvokeOnMainThread(() =>
            {
                zxing.IsAnalyzing = false;
                OnUpcResultEvent(((Entry)sender).Text);
            });

            var content = new StackLayout
            {
                Children = {
                    grid,
                    new StackLayout
                    {
                        Margin = 20,
                        Children =
                        {
                            new Label { Text = "Or, manually enter your UPC" },
                            new StackLayout {
                                Orientation = StackOrientation.Horizontal,
                                Children = {
                                    upcEntry,
                                    endButton
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
    }
}