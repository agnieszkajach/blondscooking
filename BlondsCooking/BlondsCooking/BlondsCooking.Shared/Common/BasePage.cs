using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using BlondsCooking.Views;
using GalaSoft.MvvmLight.Messaging;

namespace BlondsCooking.Common
{
    public class BasePage : Page
    {
        private Dictionary<String, Type> listOfWindows = new Dictionary<string, Type>();
        public BasePage()
        {
            listOfWindows.Add("Main", typeof(MainPage));
            listOfWindows.Add("SelectedCategory", typeof(SelectedCategoryView));
            listOfWindows.Add("SelectedRecipe", typeof(SelectedRecipeView));
            Messenger.Default.Register<NavigationMessage>(this, NavigateToPage);
        }

        protected void NavigateToPage(NavigationMessage message)
        {
            String nameOfWindowToNavigateTo;
            Type window;
            String details;
            message.QueryStringParams.TryGetValue("window", out nameOfWindowToNavigateTo);
            listOfWindows.TryGetValue(nameOfWindowToNavigateTo, out window);
            message.QueryStringParams.TryGetValue("category", out details);
            Frame.Navigate(window, details);
        }
    }
}
