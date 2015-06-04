using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlondsCooking.Helpers
{
    public class AzureStorageHelper
    {
        public async static Task DownloadAllImagesFromAzure()
        {
            BlobContinuationToken token = null;
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=blondscooking;AccountKey=UW6vJPdpGPKthyE9N1Tn4gocKiJsM4ZnT+rL9wB8tsDmMxoHj3TGy+w4Swb1a2k6e+UIjSQbYCwx8ohbvuxJrg==");

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer containter = blobClient.GetContainerReference("recipes");
            BlobListingDetails blobListingDetails = BlobListingDetails.All;
            List<IListBlobItem> blobs = new List<IListBlobItem>();
            do
            {
                var listingResult =
                    await containter.ListBlobsSegmentedAsync(null, true, blobListingDetails, 10, token, null, null);
                token = listingResult.ContinuationToken;
                blobs.AddRange(listingResult.Results);
            } 
            while (token != null);
            await FileStorageHelper.SaveAllImagesLocally(blobs);
        }
    }
}
