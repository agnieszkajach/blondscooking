using System;
using System.Collections.Generic;
using System.Text;

namespace BlondsCooking.Synchronization
{
    public class DownloadingStatusMessage
    {
        private string _status;

        public string Status
        {
            get { return _status;}
            set { _status = value; }
        }
        public DownloadingStatusMessage(string status)
        {
            _status = status;
        }
    }
}
