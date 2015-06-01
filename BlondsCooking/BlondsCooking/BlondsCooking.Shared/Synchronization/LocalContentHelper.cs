using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using BlondsCooking.Helpers;
using BlondsCooking.Services;

namespace BlondsCooking.Synchronization
{
    public class LocalContentHelper
    {
        private StorageFile file;

        public async Task RunLocalContent()
        {
            if (await CheckForLocalFile())
            {
                await CheckForLocalImages();
            }
        }
        private async Task<bool> CheckForLocalFile()
        {
            var connectionHelper = new ConnectionHelper();
            var isConnected = connectionHelper.IsConnectedToInternet();
            ExceptionDispatchInfo exceptionDispatchInfo = null;
            try
            {
                file = await ApplicationData.Current.LocalFolder.GetFileAsync(App.FileName);
            }
            catch (IOException exception)
            {
                exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
            }
            if (exceptionDispatchInfo == null) return true;
            if (isConnected)
            {
                await AzureTableHelper.DownloadAllRecipes();
                await AzureStorageHelper.DownloadAllImagesFromAzure();
                return false;
            }
            else
            {
                App.InternetNeeded = true;
            }
            return true;
        }

        private async Task CheckForLocalImages()
        {
            var filter = new List<string>() {".png"};
            if (file != null)
            {
                var storageFiles = await ApplicationData.Current.LocalFolder.GetFilesAsync();
                var localImages = storageFiles.Select(x => x.Name).ToList();
                localImages.Remove(file.Name);
                var azureImages = await XmlHelper.GetImages();
                var differences = azureImages.Except(localImages);
                if (differences.Any())
                {
                    await AzureStorageHelper.DownloadAllImagesFromAzure();
                }
            }
        }
    }
}
