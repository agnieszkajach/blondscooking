using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using BlondsCooking.Synchronization;
using GalaSoft.MvvmLight.Messaging;

namespace BlondsCooking.Helpers
{
    public class FileStorageHelper
    {
        public static async Task SaveAllImagesLocally(List<IListBlobItem> blobs)
        {
            foreach (CloudBlockBlob item in blobs)
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(item.Name, CreationCollisionOption.ReplaceExisting);
                await item.DownloadToFileAsync(file);
            }
            Messenger.Default.Send(new DownloadingStatusMessage("finished"));
        }

    }
}
