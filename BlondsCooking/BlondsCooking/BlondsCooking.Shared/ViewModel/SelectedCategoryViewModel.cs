using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    public class SelectedCategoryViewModel : ViewModelBase, IViewModel
    {
        private INavigationService navigationService;
        private String _selectedCategory;
        private Recipe _selectedRecipe;
        private ObservableCollection<Recipe> _recipesInSelectedCategory; 

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

        public SelectedCategoryViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

         public RelayCommand BackCommand
         {
             get { return new RelayCommand(() => navigationService.NavigateTo("Main")); }
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


        public async void LoadData(string data)
        {
            SelectedCategory = data;
            var recipes = await XmlHelper.GetRecipesByCategory(SelectedCategory);
            LoadImagesForSelectedCategory(recipes);
        }
    }
}
