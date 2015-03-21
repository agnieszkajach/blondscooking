using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Text;
using BlondsCooking.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace BlondsCooking.Helpers
{
    public class AzureTableHelper 
    {
        public static async Task DownloadAllRecipes()
        {
            TableContinuationToken token = null;
            var items = new List<Recipe>();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=blondscooking;AccountKey=UW6vJPdpGPKthyE9N1Tn4gocKiJsM4ZnT+rL9wB8tsDmMxoHj3TGy+w4Swb1a2k6e+UIjSQbYCwx8ohbvuxJrg==");
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("recipes");
            TableQuery<Recipe> query = new TableQuery<Recipe>();
            do
            {
                TableQuerySegment<Recipe> seg = await table.ExecuteQuerySegmentedAsync(query, token);
                token = seg.ContinuationToken;
                items.AddRange(seg);
            } while (token != null);
            await XmlHelper.SaveRecipesToXmlLocally(items);
        }

        public static async Task<IList<string>> DownloadAllTitlesOfRecipes()
        {
            List<string> azureRecipes = new List<string>();
            TableContinuationToken token = null;
            var items = new List<Recipe>();
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=blondscooking;AccountKey=UW6vJPdpGPKthyE9N1Tn4gocKiJsM4ZnT+rL9wB8tsDmMxoHj3TGy+w4Swb1a2k6e+UIjSQbYCwx8ohbvuxJrg==");
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("recipes");
            TableQuery<Recipe> query = new TableQuery<Recipe>();
            do
            {
                TableQuerySegment<Recipe> seg = await table.ExecuteQuerySegmentedAsync(query, token);
                token = seg.ContinuationToken;
                azureRecipes.AddRange(seg.Select(r => r.Title));
            } while (token != null);
            return azureRecipes;
        }
    }
}
