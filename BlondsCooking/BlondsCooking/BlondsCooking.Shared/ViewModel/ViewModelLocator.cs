/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:BlondsCooking"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/
#if WINDOWS_PHONE_APP
using Windows.Phone.UI.Input;
#endif

using BlondsCooking.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;

namespace BlondsCooking.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            var dialogService = this.createDialogService();
            SimpleIoc.Default.Register<Services.IDialogService>(() => dialogService);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SelectedCategoryViewModel>();
            SimpleIoc.Default.Register<SelectedRecipeViewModel>();
        }

        private Services.IDialogService createDialogService()
        {
            var dialogService = new Services.DialogService();
            return dialogService;
        }

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("Main", typeof(MainPage));
            navigationService.Configure("Category", typeof(SelectedCategoryView));
            navigationService.Configure("Recipe", typeof(SelectedRecipeView));
            return navigationService;
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public SelectedCategoryViewModel SelectedCategory
        {
            get { return ServiceLocator.Current.GetInstance<SelectedCategoryViewModel>(); }
        }

        public SelectedRecipeViewModel SelectedRecipe
        {
            get { return ServiceLocator.Current.GetInstance<SelectedRecipeViewModel>(); }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}