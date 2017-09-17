using BeerInventory.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerInventoryApp.RestInterfaces
{
    public interface IBeerInventoryApi
    {
        [Get("/beer")]
        Task<List<BeerEntity>> GetAllBeer();

        [Get("/beer")]
        Task<BeerEntity> GetBeerById(string id);

        [Get("/upc")]
        Task<List<BeerEntity>> GetBeerByUPC([AliasAs("id")] string upc);

        [Get("/beer")]
        Task<BeerEntity> GetBeerByBreweryAndName(string brewery, string beerName);

        [Get("/inventory/{owner}")]
        Task<List<InventoryDetails>> GetInventoryByOwner(string owner);

        [Get("/inventory/{owner}")]
        Task<List<InventoryDetails>> GetInventoryByOwnerAndLocation(string owner, string location);

        [Put("/beer")]
        Task AddBeerToDB([AliasAs("brewer")] string brewery, string beerName, [AliasAs("id")] string upc);

        [Post("/inventory")]
        Task AddInventory([AliasAs("id")] string beerId, [AliasAs("user")] string owner, string location, int count);
    }

    public static class BeerInventoryApi
    {
        public static string ApiUrl = "http://beerinventory20170506104048.azurewebsites.net/api";
    }
}

