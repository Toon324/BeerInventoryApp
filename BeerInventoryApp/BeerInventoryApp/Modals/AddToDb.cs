using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeerInventoryApp
{
	public class AddToDb : ContentPage
	{
		public AddToDb (string upc = "")
		{
			Content = new StackLayout {
                Margin = 20,
				Children = {
					new Label { Text = "Welcome to Xamarin Forms!" },
                    new Entry { Placeholder = "UPC", Text = upc },
                    new Entry { Placeholder = "Name of Brewery"},
                    new Entry { Placeholder = "Name of Beer"}
				}
			};
		}
	}
}