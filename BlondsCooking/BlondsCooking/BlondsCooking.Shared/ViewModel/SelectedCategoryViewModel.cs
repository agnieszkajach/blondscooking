using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Core;
using Windows.UI.Popups;
using BlondsCooking.Common;
using BlondsCooking.Helpers;
using BlondsCooking.Model;
using BlondsCooking.Synchronization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace BlondsCooking.ViewModel
{
    public class SelectedCategoryViewModel : ViewModelBase, INavigable
    {
        private INavigationService navigationService;
        private String _selectedCategory;
        private Recipe _selectedRecipe;
        private ObservableCollection<Recipe> _recipesInSelectedCategory;
        private String _urlToImageOfCategory;
        private readonly Services.IDialogService dialogService;
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

        public string SelectedCategory
        {
            get
            {   return _selectedCategory;   }
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(() => SelectedCategory);
            }
        }

        public ObservableCollection<Recipe> RecipesInSelectedCategory
        {
            get { return _recipesInSelectedCategory; }
            set
            {
                _recipesInSelectedCategory = value;
                RaisePropertyChanged(() => RecipesInSelectedCategory);
            }
        }

        public string UrlToImageOfCategory
        {
            get { return _urlToImageOfCategory;}
            set
            {
                _urlToImageOfCategory = value;
                RaisePropertyChanged(() => UrlToImageOfCategory);
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

        public SelectedCategoryViewModel(INavigationService navigationService, Services.IDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
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

        public RelayCommand BackCommand
         {
             get { return new RelayCommand(() => navigationService.NavigateTo("Main", " ")); }
         }

         public RelayCommand<String> OpenRecipeCommand
         {
             get { return new RelayCommand<String>(i => navigationService.NavigateTo("Recipe", i)); }
         }


        private void LoadImagesForSelectedCategory(IList<Recipe> recipes)
        {
            foreach (Recipe recipe in recipes)
            {
                recipe.UrlToImage = App.Path + recipe.UrlToImage;
            }
            RecipesInSelectedCategory = new ObservableCollection<Recipe>(recipes);
        }


        public async void Activate(string parameter)
        {
            ExceptionDispatchInfo capturedException = null;
            UrlToImageOfCategory = "../Assets/Layout/" + parameter +".png";
            SelectedCategory = parameter;
            try
            {
                var recipes = await XmlHelper.GetRecipesByCategory(SelectedCategory);
                LoadImagesForSelectedCategory(recipes);
            }
            catch (FileNotFoundException ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);               
            }
            if (App.InternetNeeded)
            {
                await dialogService.ShowMessage(
                    "Hey, you need to connect to Internet to get all that delicious stuff ♥");
            }
            else
            {
                if (capturedException != null)
                {
                    await dialogService.ShowMessage("Please be patient. We are downloading delicious stuff espacially for You ♥");
                }
            }
            
        }

        public void Deactivate()
        {
           
        }

    }
}
