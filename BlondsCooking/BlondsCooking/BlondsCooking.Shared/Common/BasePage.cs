using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using BlondsCooking.ViewModel;
using BlondsCooking.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace BlondsCooking.Common
{
    public class BasePage : Page
    {
        private IViewModel viewModel;
        public BasePage()
        {
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter;
            viewModel = this.DataContext as IViewModel;
            if (viewModel != null) viewModel.LoadData(e.Parameter.ToString());
            base.OnNavigatedTo(e);
        }
    }
}
