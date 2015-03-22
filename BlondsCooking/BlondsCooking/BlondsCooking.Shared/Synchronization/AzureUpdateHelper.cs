using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Popups;
using BlondsCooking.Helpers;

namespace BlondsCooking.Synchronization
{
    public class AzureUpdateHelper
    {
        public async Task CheckForUpdateOnAzure()
        {
            bool updateNeeded = false;
            var localRecipes = await XmlHelper.GetTitlesOfRecipes();
            var azureRecipes = await AzureTableHelper.DownloadAllTitlesOfRecipes();
            foreach (string title in azureRecipes)
            {
                if (!localRecipes.Contains(title))
                {
                    updateNeeded = true;
                }
            }
            if (updateNeeded)
            {
                await AzureTableHelper.DownloadAllRecipes();
                await AzureStorageHelper.DownloadAllImagesFromAzure();
            }
        }

    }
}
