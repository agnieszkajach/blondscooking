using System;
using System.Collections.Generic;
using System.Text;

namespace BlondsCooking.Helpers
{
    public class LocalSettingsHelper
    {
        public static bool CheckIfFirstLaunch()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("FirstLaunch"))
            {
                return false;
            }
            else
            {
                localSettings.Values["FirstLaunch"] = "true";
                return true;
            }
        }
    }
}
