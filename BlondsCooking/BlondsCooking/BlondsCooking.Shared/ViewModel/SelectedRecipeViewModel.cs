using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlondsCooking.Common;
using BlondsCooking.Helpers;
using BlondsCooking.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace BlondsCooking.ViewModel
{
    public class SelectedRecipeViewModel : ViewModelBase, IViewModel
    {
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

        public SelectedRecipeViewModel()
        {
            //Messenger.Default.Register<MessageToLoadRecipe>(this, LoadSelectedRecipe);    
        }


        public RelayCommand<String> BackCommand
        {
            get { return new RelayCommand<string>(i => Back("SelectedCategory")); }
        }

        private async void Back(String NameOfWindowToNavigateTo)
        {
            Dictionary<String, String> paramsDictionary = new Dictionary<string, string>();
            paramsDictionary.Add("window", NameOfWindowToNavigateTo);
            Messenger.Default.Send(new NavigationMessage("SelectedRecipe", paramsDictionary));
            Messenger.Default.Send(new MessageToGetBackToCategory() {Message = await XmlHelper.GetCategoryByTitle(selectedRecipe)});
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
