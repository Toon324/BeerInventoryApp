using BeerInventory.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerInventoryApp.Services
{
    class InventoryService
    {
        private static string ApiUrl = "http://beerinventory20170506104048.azurewebsites.net/api";

        public List<InventoryDetails> GetInventory(string owner, string location = "")
        {
            var client = new GenericRestClient(ApiUrl);

            var request = new GenericRestRequest("/inventory/" + owner);

            if (!string.IsNullOrEmpty(location))
            {
                request.AddQueryParameter("location", location);
            }

            return client.Execute<List<InventoryDetails>>(request);
        }

        public List<BeerEntity> GetBeerDetails(String upc)
        {
            var client = new GenericRestClient(ApiUrl);

            var request = new GenericRestRequest("/beer/" + upc);

            return client.Execute<List<BeerEntity>>(request);
        }

        public BeerEntity GetBeerDetailsByName(String brewery, String beerName)
        {
            var client = new GenericRestClient(ApiUrl);

            var request = new GenericRestRequest("/beer");

            request.AddQueryParameter("brewery", brewery);
            request.AddQueryParameter("beerName", beerName);

            return client.Execute<BeerEntity>(request);
        }
    }
}
