using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BeerStorageLibrary.Tables
{
    public abstract class AzureTableService<T> where T : TableEntity, new()
    {
        private string TableName { get; set; }

        protected CloudTable Table { get; set; }

        public AzureTableService(String tableName)
        {
            TableName = tableName;
            var storageAccount = CloudStorageAccount.Parse("");

            var table = storageAccount.CreateCloudTableClient().GetTableReference(TableName);

            table.CreateIfNotExistsAsync();

            Table = table;
        }

        public T GetEntity(string partitionKey, string rowKey)
        {
            try
            {
                var result = Table.ExecuteAsync(TableOperation.Retrieve<T>(partitionKey, rowKey)).Result;

                return ((T)result.Result);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                return null;
            }
        }

        public async Task<List<T>> ExecuteQuery(TableQuery<T> query)
        {
            TableContinuationToken continuationToken = null;
            var results = new List<T>();

            do
            {
                var queryResults = await Table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);
            }
            while (continuationToken != null);

            return results;
        }

        public void AddOrUpdate(T entity)
        {
            try
            {
                Table.ExecuteAsync(TableOperation.InsertOrReplace(entity));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
            }
        }
    }
}
