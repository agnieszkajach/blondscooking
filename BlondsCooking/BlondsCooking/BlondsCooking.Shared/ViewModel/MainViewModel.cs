using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Background;
using Windows.UI.Core;
using Windows.UI.WebUI;
using BlondsCooking.Common;
using BlondsCooking.Synchronization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace BlondsCooking.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, INavigable
    {
        private INavigationService navigationService;
        private string _finishedDownloading;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Messenger.Default.Register<DownloadingStatusMessage>(this, HandleFinishedDownloading);
            this.FinishedDownloading = App.FinishedDownloading;
        }

        private void HandleFinishedDownloading(DownloadingStatusMessage message)
        {
            if (message.Status == "finished")
            {
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    FinishedDownloading = "Collapsed";
                    RaisePropertyChanged(() => FinishedDownloading);
                }
            );
            }
            
        }

        public string FinishedDownloading
        {
            get { return _finishedDownloading;}
            set
            {
                _finishedDownloading = value;
            }
        }

        public RelayCommand OpenMuffinWindowCoomand
        {
            get { return new RelayCommand(() =>
            {
                navigationService.NavigateTo("Category", "muffin");
            }); }
        }

        public RelayCommand OpenCookieWindowCoomand
        {
            get { return new RelayCommand(() =>
            {
                navigationService.NavigateTo("Category", "cookie");
            }); }
        }

        public RelayCommand OpenCakeWindowCoomand
        {
            get { return new RelayCommand(() =>
            {
                navigationService.NavigateTo("Category", "cake");
            }); }
        }

        public RelayCommand OpenDinnerWindowCoomand
        {
            get { return new RelayCommand(() =>
            {
                navigationService.NavigateTo("Category", "dinner");
            }); }
        }

        public RelayCommand OpenPancakeWindowCoomand
        {
            get { return new RelayCommand(() =>
            {
                navigationService.NavigateTo("Category", "pancake");
            }); }
        }

        public RelayCommand OpenShakeWindowCoomand
        {
            get { return new RelayCommand(() =>
            {
                navigationService.NavigateTo("Category", "shake");
            });
            }
        }

        public void Activate(string parameter)
        {
        }

        public void Deactivate()
        {
            
        }
    }
}