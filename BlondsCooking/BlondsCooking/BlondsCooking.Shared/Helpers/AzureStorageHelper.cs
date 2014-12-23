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
        public async static Task<string> DownloadImageFromAzure(string name)
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=blondscooking;AccountKey=UW6vJPdpGPKthyE9N1Tn4gocKiJsM4ZnT+rL9wB8tsDmMxoHj3TGy+w4Swb1a2k6e+UIjSQbYCwx8ohbvuxJrg==");

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer containter = blobClient.GetContainerReference("recipes");
            CloudBlockBlob image = containter.GetBlockBlobReference(name);
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            await image.DownloadToFileAsync(file);
            return file.Name;
        }
    }
}
