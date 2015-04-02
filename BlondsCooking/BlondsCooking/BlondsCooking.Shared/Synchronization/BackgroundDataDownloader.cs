using System.Threading.Tasks;
using Windows.UI.Popups;
using BlondsCooking.Helpers;
using BlondsCooking.Services;

namespace BlondsCooking.Synchronization
{
    public class BackgroundDataDownloader
    {
        public async Task Run()
        {
            await AzureTableHelper.DownloadAllRecipes();
            await AzureStorageHelper.DownloadAllImagesFromAzure();         
        }
    }
}
