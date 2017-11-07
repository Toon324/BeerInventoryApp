using BeerStorageLibrary.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerStorageLibrary.Tables
{
    public class UpcTableService : AzureTableService<UpcEntity>
    {
        public UpcTableService() : base("UpcLookup") { }

        public List<UpcEntity> GetByUpc(String upc)
        {
            var results = ExecuteQuery(new TableQuery<UpcEntity>().Where(TableQuery.GenerateFilterCondition("UPC", QueryComparisons.Equal, upc))).Result;

            if (!results.Any())
            {
                return new List<UpcEntity>();
            }

            return results;
        }

        public UpcEntity GetEntity(String type, String id, String upc)
        {
            return GetEntity(type, id + "|" + upc);
        }
    }
}
