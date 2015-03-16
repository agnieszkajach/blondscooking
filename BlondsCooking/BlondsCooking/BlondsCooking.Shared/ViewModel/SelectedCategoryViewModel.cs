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

namespace BlondsCooking.ViewModel
{
    public class SelectedCategoryViewModel : ViewModelBase
    {
        private String _selectedCategory;
        private Recipe _selectedRecipe;
        private ObservableCollection<Recipe> _recipesInSelectedCategory; 

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                RaisePropertyChanged(() => _selectedRecipe);
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

         public SelectedCategoryViewModel()
         {
            Messenger.Default.Register<MessageBetweenViewModels>(this, (m) => LoadSelectedCategory(m));     
         }

         public RelayCommand<String> BackCommand
         {
             get { return new RelayCommand<string>(i => Back("Main")); }
         }

         public RelayCommand<String> OpenRecipeCommand
         {
             get { return new RelayCommand<String>(i => OpenRecipe("SelectedRecipe", i)); }
         }

         private void OpenRecipe(String nameOfWindowToNavigateTo, string recipe)
         {
             Dictionary<String, String> paramsDictionary = new Dictionary<string, string>();
             paramsDictionary.Add("window", nameOfWindowToNavigateTo);
             Messenger.Default.Send(new NavigationMessage("SelectedRecipe", paramsDictionary));
             Messenger.Default.Send<MessageBetweenViewModels>(new MessageBetweenViewModels() { Message = recipe });
         }
         private void Back(String nameOfWindowToNavigateTo)
         {
             Dictionary<String, String> paramsDictionary = new Dictionary<string, string>();
             paramsDictionary.Add("window", nameOfWindowToNavigateTo);
             Messenger.Default.Send(new NavigationMessage("SelectedCategory", paramsDictionary));
         }

         private async void LoadSelectedCategory(MessageBetweenViewModels messageBetweenViewModels)
         {
             _selectedCategory = messageBetweenViewModels.Message;
             var recipes = await XmlHelper.GetRecipesByCategory(_selectedCategory);
             foreach (Recipe recipe in recipes)
             {
                 // TODO images loaded from local storage
                // await AzureStorageHelper.DownloadImageFromAzure(recipe.UrlToImage);
             }
             RecipesInSelectedCategory = new ObservableCollection<Recipe>(recipes);
             LoadImagesForSelectedCategory();
         }

        private void LoadImagesForSelectedCategory()
        {
            foreach (Recipe recipe in RecipesInSelectedCategory)
            {
                recipe.UrlToImage = "ms-appdata:///local/" + recipe.UrlToImage;
            }
            RaisePropertyChanged(() => RecipesInSelectedCategory);
        }

    }
}
