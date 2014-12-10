using System;
using System.Collections.Generic;
using System.Text;

namespace BlondsCooking.Model
{
    public class Category
    {
        private List<Recipe> _listOfRecipes;

        public Category()
        {
            _listOfRecipes = new List<Recipe>();
        }
    }
}
