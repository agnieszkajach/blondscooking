using System;
using System.Collections.Generic;
using BlondsCooking.Common;
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
    public class MainViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
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

    }
}