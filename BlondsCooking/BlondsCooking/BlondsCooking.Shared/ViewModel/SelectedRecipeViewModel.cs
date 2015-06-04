using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlondsCooking.Common;
using BlondsCooking.Helpers;
using BlondsCooking.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using BlondsCooking.Synchronization;

namespace BlondsCooking.ViewModel
{
    public class SelectedRecipeViewModel : ViewModelBase, INavigable
    {
        private INavigationService navigationService;
        private String selectedRecipe;
        private Recipe _selectedRecipe;
        private string _finishedDownloading;

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                RaisePropertyChanged(() => SelectedRecipe);
            }
        }

        public string FinishedDownloading
        {
            get { return _finishedDownloading; }
            set
            {
                _finishedDownloading = value;
            }
        }

        public SelectedRecipeViewModel(INavigationService navigationService)
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
        

        public RelayCommand<String> BackCommand
        {
            get { return new RelayCommand<string>(i => Back()); }
        }

        public RelayCommand FullScreenCommand
        {
            get { return new RelayCommand(() => navigationService.NavigateTo("FullScreen", _selectedRecipe.UrlToImage)); }
        }

        private async void Back()
        {
            var category = await XmlHelper.GetCategoryByTitle(selectedRecipe);
            navigationService.NavigateTo("Category", category);
        }

        private void LoadImageForSelectedRecipe()
        {
            SelectedRecipe.UrlToImage = App.Path + SelectedRecipe.UrlToImage;
            RaisePropertyChanged(() => SelectedRecipe);
        }

        public async void Activate(string parameter)
        {
            selectedRecipe = parameter;
            SelectedRecipe = await XmlHelper.GetRecipeByTitle(selectedRecipe);
            LoadImageForSelectedRecipe();
        }

        public void Deactivate()
        {
            
        }
    }
}
