using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerInventoryApp.Models
{
    public class SuggestionResultsEntity
    {
        [JsonProperty("@search.coverage")]
        public float SearchCoverage { get; set; }

        [JsonProperty("value")]
        public List<dynamic> Results { get; set; }
    }

}
