using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerInventoryApp.Models
{
    public class SearchResultsEntity
    {
        [JsonProperty("@odata.context")]
        public string ContextUrl { get; set; }

        [JsonProperty("value")]
        public List<SearchResult> Results { get; set; }
    }

    public class SearchResult
    {
        [JsonProperty("@search.score")]
        public float SearchScore { get; set; }

        public string RowKey { get; set; }

        public string Key { get; set; }

        public string Id { get; set; }

        public string Brewer { get; set; }

        public string Name { get; set; }
    }
}
