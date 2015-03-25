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

namespace BlondsCooking.ViewModel
{
    public class SelectedRecipeViewModel : ViewModelBase, IViewModel
    {
        private INavigationService navigationService;
        private String selectedRecipe;
        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                RaisePropertyChanged(() => SelectedRecipe);
            }
        }

        public SelectedRecipeViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService; 
        }


        public RelayCommand<String> BackCommand
        {
            get { return new RelayCommand<string>(i => Back("SelectedCategory")); }
        }

        private async void Back(String NameOfWindowToNavigateTo)
        {
            var category = await XmlHelper.GetCategoryByTitle(selectedRecipe);
            navigationService.NavigateTo("Category", category);
        }

        private void LoadImageForSelectedRecipe()
        {
            SelectedRecipe.UrlToImage = App.Path + SelectedRecipe.UrlToImage;
            RaisePropertyChanged(() => SelectedRecipe);
        }

        public async void LoadData(string data)
        {
            selectedRecipe = data;
            SelectedRecipe = await XmlHelper.GetRecipeByTitle(selectedRecipe);
            LoadImageForSelectedRecipe();
        }
    }
}
