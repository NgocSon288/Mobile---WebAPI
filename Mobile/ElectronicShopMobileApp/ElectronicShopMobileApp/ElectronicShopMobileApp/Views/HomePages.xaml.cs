using System;
using System.Collections.Generic;
using ElectronicShopMobileApp.ViewModels;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Views
{
    public partial class HomePages : ContentPage
    {
        public HomePages()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();

            Device.StartTimer(TimeSpan.FromSeconds(2), (Func<bool>)(() =>
            {
                CarouselViewer.Position = (CarouselViewer.Position + 1) % 11;
                return true;
            }));
        }
    }
}
