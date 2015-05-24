using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace BlondsCooking.Synchronization
{
    public class UpdateCheckingInBackground
    {
        public async Task Run()
        {
            AzureUpdateHelper azureUpdateHelper = new AzureUpdateHelper();
            await azureUpdateHelper.CheckForUpdateOnAzure();
        }
    }
}
