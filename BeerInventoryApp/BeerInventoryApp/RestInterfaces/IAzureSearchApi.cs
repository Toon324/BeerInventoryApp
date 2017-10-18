using BeerInventoryApp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerInventoryApp.RestInterfaces
{
    public interface IAzureSearchApi
    {
        [Get("/{index}/docs?search={search}&api-version=2016-09-01")]
        Task<SearchResultsEntity> Search(string index, string search);

        [Get("/{index}/docs/suggest?search={search}&api-version=2016-09-01")]
        Task<SuggestionResultsEntity> Suggest(string index, string suggesterName, string search);
    }

    public static class AzureSearchApi
    {
        public const string ApiUrl = "https://beerinventory.search.windows.net/indexes";

        public const string BeerIndex = "beerindex";
    }
}
