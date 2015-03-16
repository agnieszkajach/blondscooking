using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace BlondsCooking.Helpers
{
    public class FileStorageHelper
    {
        public static async Task SaveAllImagesLocally(List<IListBlobItem> blobs)
        {
            foreach (CloudBlockBlob item in blobs)
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(item.Name, CreationCollisionOption.ReplaceExisting);
                item.DownloadToFileAsync(file);
            }
        }
    }
}
