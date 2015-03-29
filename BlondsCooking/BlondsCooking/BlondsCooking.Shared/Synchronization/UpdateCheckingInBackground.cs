using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Background;

namespace BlondsCooking.Synchronization
{
    public class UpdateCheckingInBackground : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            AzureUpdateHelper azureUpdateHelper = new AzureUpdateHelper();
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            await azureUpdateHelper.CheckForUpdateOnAzure();
            deferral.Complete();
        }
    }
}
