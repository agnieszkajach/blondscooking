using System;
using BlondsCooking.Model;
using GalaSoft.MvvmLight;

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
    }
}
