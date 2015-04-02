using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;

namespace BlondsCooking.Services
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message)
        {
            var msg = new MessageDialog(message);
            msg.ShowAsync();
        }
    }
}
