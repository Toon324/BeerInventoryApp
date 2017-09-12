using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeerInventoryApp
{
	public class AddToDb : ContentPage
	{
		public AddToDb (string upc = "", string brewery = "", string beer = "")
		{
			Content = new StackLayout {
                Margin = 20,
				Children = {
					new Label { Text = "Beer not found in our system, please submit it so we can approve it. Thanks!" },
                    new Entry { Placeholder = "UPC", Text = upc },
                    new Entry { Placeholder = "Name of Brewery", Text = brewery},
                    new Entry { Placeholder = "Name of Beer", Text = beer}
				}
			};
		}
	}
}