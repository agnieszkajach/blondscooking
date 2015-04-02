using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace BlondsCooking.Services
{
    public class DialogService : IDialogService
    {
        public async Task ShowMessage(string message)
        {
            var msg = new MessageDialog(message);
            await msg.ShowAsync();
        }
    }
}
