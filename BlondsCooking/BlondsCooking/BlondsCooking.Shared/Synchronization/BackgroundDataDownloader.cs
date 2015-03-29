using System.Threading.Tasks;
using BlondsCooking.Helpers;

namespace BlondsCooking.Synchronization
{
    public class BackgroundDataDownloader
    {
        public async Task Run()
        {
            if (LocalSettingsHelper.CheckIfFirstLaunch())
            {
                await AzureStorageHelper.DownloadAllImagesFromAzure();
                await AzureTableHelper.DownloadAllRecipes();
            }
            else
            {
                await LocalContentHelper.CheckForLocalFile();
            }
        }
    }
}
