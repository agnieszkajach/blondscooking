using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using BlondsCooking.ViewModel;

namespace BlondsCooking.Common
{
    public class BasePage : Page
    {
        public BasePage()
        {

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter;
            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
                navigableViewModel.Activate(e.Parameter.ToString()); 
        }


    }
}
