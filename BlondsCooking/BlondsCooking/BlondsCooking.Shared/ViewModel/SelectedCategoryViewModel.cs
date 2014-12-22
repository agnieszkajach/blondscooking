using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlondsCooking.Model;
using GalaSoft.MvvmLight;

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

    }
}
