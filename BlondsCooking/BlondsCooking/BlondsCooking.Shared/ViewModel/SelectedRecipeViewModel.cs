using System;
using System.Collections.Generic;
using BlondsCooking.Common;
using BlondsCooking.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace BlondsCooking.ViewModel
{
    public class SelectedRecipeViewModel : ViewModelBase
    {
        private String selectedRecipe;
        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                RaisePropertyChanged(() => _selectedRecipe);
            }
        }

        public SelectedRecipeViewModel()
        {
            Messenger.Default.Register<MessageBetweenViewModels>(this, (m) => LoadSelectedRecipe(m));    
        }

        private void LoadSelectedRecipe(MessageBetweenViewModels messageBetweenViewModels)
        {
            selectedRecipe = messageBetweenViewModels.Message;
        }

        public RelayCommand<String> BackCommand
        {
            get { return new RelayCommand<string>(i => Back("SelectedCategory")); }
        }

        private void Back(String NameOfWindowToNavigateTo)
        {
            Dictionary<String, String> paramsDictionary = new Dictionary<string, string>();
            paramsDictionary.Add("window", NameOfWindowToNavigateTo);
            Messenger.Default.Send(new NavigationMessage("SelectedRecipe", paramsDictionary));
            //Messenger.Default.Send(new MessageBetweenViewModels() {Message = xmlHelper.GetCategoryFromTitle(selectedRecipe)});
        }
    }
}
