using BeerStorageLibrary.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerStorageLibrary.Tables
{
    public class InventoryTableService : AzureTableService<InventoryEntity>
    {
        public InventoryTableService() : base("Inventory") { }

        private List<InventoryEntity> GetAvailable(String owner, String location)
        {
            var query = new TableQuery<InventoryEntity>()
               .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, owner))
               .Where(TableQuery.GenerateFilterCondition("Location", QueryComparisons.Equal, location))
               .Where(TableQuery.GenerateFilterConditionForInt("Count", QueryComparisons.GreaterThan, 0));

            return ExecuteQuery(query).Result;
        }

        public List<InventoryEntity> GetAvailable(String owner)
        {
            var query = new TableQuery<InventoryEntity>()
               .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, owner))
               .Where(TableQuery.GenerateFilterConditionForInt("Count", QueryComparisons.GreaterThan, 0));

            return ExecuteQuery(query).Result;
        }

        public List<InventoryEntity> GetAll(String owner)
        {
            var query = new TableQuery<InventoryEntity>()
               .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, owner));

            return ExecuteQuery(query).Result;
        }

        public List<InventoryEntity> GetAll(String owner, String location)
        {
            var query = new TableQuery<InventoryEntity>()
               .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, owner))
               .Where(TableQuery.GenerateFilterCondition("Location", QueryComparisons.Equal, location));

            return ExecuteQuery(query).Result;
        }

        public InventoryEntity Get(String owner, String location, String id)
        {
            var query = new TableQuery<InventoryEntity>()
               .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, owner))
               .Where(TableQuery.GenerateFilterCondition("Location", QueryComparisons.Equal, location))
               .Where(TableQuery.GenerateFilterCondition("Id", QueryComparisons.Equal, id));

            var results = ExecuteQuery(query).Result;

            if (!results.Any())
            {
                return null;
            }

            return results.First();
        }
    }
}
