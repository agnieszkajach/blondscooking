using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace BlondsCooking.ViewModel
{
    public class FullScreenViewModel : ViewModelBase, INavigable
    {
        private INavigationService navigationService;
        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl;}
            set
            {
                _imageUrl = value;
                RaisePropertyChanged(ImageUrl);
            }
        }

        public FullScreenViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void Activate(string parameter)
        {
            ImageUrl = parameter;
        }

        public void Deactivate()
        {
        }
    }
}
