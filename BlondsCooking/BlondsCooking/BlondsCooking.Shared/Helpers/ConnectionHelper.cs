using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace BlondsCooking.Helpers
{
    public class ConnectionHelper
    {
        public bool IsConnectedToInternet()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            var isConnected = (profile != null &&
                               profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
            return isConnected;
        }
    }
}
