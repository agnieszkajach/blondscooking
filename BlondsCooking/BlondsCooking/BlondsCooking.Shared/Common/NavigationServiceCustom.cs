using System;
using System.Collections.Generic;
using System.Text;
#if WINDOWS_PHONE_APP
using Windows.Phone.UI.Input;
#endif
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using BlondsCooking.Views;
using GalaSoft.MvvmLight.Views;

namespace BlondsCooking.Common
{
    public class NavigationServiceCustom :INavigationService, IDisposable
    {
        private readonly Dictionary<String, Type> framesDictionary = new Dictionary<string, Type>();
        public object CurrentData { get; set; }
#if WINDOWS_PHONE_APP
        public NavigationServiceCustom()
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (CanGoBack())
            {
                GoBack();
                e.Handled = true;
            }
        }
#endif

        public void RegisterPages()
        {
            framesDictionary.Add("Main", typeof(MainPage));
            framesDictionary.Add("Category", typeof(SelectedCategoryView));
            framesDictionary.Add("Recipe", typeof(SelectedRecipeView));
        }

        public void RegisterPage(string key, Type type)
        {
            framesDictionary.Add(key, type);
        }

        public bool CanGoBack()
        {
            var currentFrame = Window.Current.Content as Frame;
            return currentFrame != null && currentFrame.CanGoBack;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string CurrentPageKey
        {
            get { throw new NotImplementedException(); }
        }

        public void GoBack()
        {
            if (!CanGoBack()) return;
            var frame = Window.Current.Content as Frame;
            if (frame!= null) frame.GoBack();
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            var currentFrame = Window.Current.Content as Frame;
            var nextFrameType = framesDictionary[pageKey];
            if (currentFrame != null)
            {
                CurrentData = null;
                currentFrame.Navigate(nextFrameType);
            }
        }

        public void NavigateTo(string pageKey)
        {
            var currentFrame = Window.Current.Content as Frame;
            var nextFrameType = framesDictionary[pageKey];
            if (currentFrame != null)
            {
                CurrentData = null;
                currentFrame.Navigate(nextFrameType);
            }

        }
    }
}
