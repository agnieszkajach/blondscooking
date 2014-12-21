using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Messaging;

namespace BlondsCooking.Common
{
    public class NavigationMessage : NotificationMessage
    {
        public string PageName
        {
            get { return base.Notification; }
        }

        public Dictionary<string, string> QueryStringParams { get; private set; }

        public NavigationMessage(string pageName) : base(pageName) { }

        public NavigationMessage(string pageName, Dictionary<string, string> queryStringParams) : this(pageName)
        {
            this.QueryStringParams = queryStringParams;
        }
    }
}
