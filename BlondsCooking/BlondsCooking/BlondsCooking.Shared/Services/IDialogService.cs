using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlondsCooking.Services
{
    public interface IDialogService
    {
        Task ShowMessage(string message);
    }
}
