using System;
using System.Collections.Generic;
using BlondsCooking.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

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
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            
        }

        public RelayCommand<String> OpenMuffinWindowCoomand
        {
            get { return new RelayCommand<String>(i => OpenWindow("muffin")); }
        }

        public RelayCommand<String> OpenCookieWindowCoomand
        {
            get { return new RelayCommand<String>(i => OpenWindow("cookie")); }
        }

        public RelayCommand<String> OpenCakeWindowCoomand
        {
            get { return new RelayCommand<String>(i => OpenWindow("cake")); }
        }

        public RelayCommand<String> OpenDinnerWindowCoomand
        {
            get { return new RelayCommand<String>(i => OpenWindow("dinner")); }
        }

        public RelayCommand<String> OpenPancakeWindowCoomand
        {
            get { return new RelayCommand<String>(i => OpenWindow("pancake")); }
        }

        public RelayCommand<String> OpenShakeWindowCoomand
        {
            get { return new RelayCommand<String>(i => OpenWindow("shake")); }
        }

        private void OpenWindow(String nameOfWindowsToNavigateTo)
        {
            var paramsDictionary = new Dictionary<string, string>
            {
                {"window", "SelectedCategory"},
                {"category", nameOfWindowsToNavigateTo}
            };
            Messenger.Default.Send(new NavigationMessage("SelectedCategory", paramsDictionary));
            Messenger.Default.Send(new MessageToGetBackToCategory() { Message = nameOfWindowsToNavigateTo });

        }
    }
}