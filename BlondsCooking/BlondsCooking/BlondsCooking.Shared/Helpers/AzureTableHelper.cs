using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlondsCooking.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace BlondsCooking.Helpers
{
    public class AzureTableHelper 
    {
        public static async Task<IList<Recipe>> DownloadRecipesFromTableByCategory(string category)
        {
            var items = new List<Recipe>();
            TableContinuationToken token = null;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=blondscooking;AccountKey=UW6vJPdpGPKthyE9N1Tn4gocKiJsM4ZnT+rL9wB8tsDmMxoHj3TGy+w4Swb1a2k6e+UIjSQbYCwx8ohbvuxJrg==");
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("recipes");
            TableQuery<Recipe> query = new TableQuery<Recipe>().Where(TableQuery.GenerateFilterCondition("Category", QueryComparisons.Equal, category));
            do
            {
                TableQuerySegment<Recipe> seg = await table.ExecuteQuerySegmentedAsync(query, token);
                token = seg.ContinuationToken;
                items.AddRange(seg);
            } while (token != null );
            return items;
        }

        public static async Task<IList<Recipe>> DownloadRecipesFromTableByTitle(string title)
        {
            var items = new List<Recipe>();
            TableContinuationToken token = null;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=blondscooking;AccountKey=UW6vJPdpGPKthyE9N1Tn4gocKiJsM4ZnT+rL9wB8tsDmMxoHj3TGy+w4Swb1a2k6e+UIjSQbYCwx8ohbvuxJrg==");
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("recipes");
            TableQuery<Recipe> query = new TableQuery<Recipe>().Where(TableQuery.GenerateFilterCondition("Title", QueryComparisons.Equal, title));
            do
            {
                TableQuerySegment<Recipe> seg = await table.ExecuteQuerySegmentedAsync(query, token);
                token = seg.ContinuationToken;
                items.AddRange(seg);
            } while (token != null);
            return items;
        }

        
    }
}
