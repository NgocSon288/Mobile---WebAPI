using System;
using System.Collections.Generic;
using ElectronicShopMobileApp.ViewModels;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Views
{
    public partial class CustomerPage : ContentPage
    {
        public CustomerPage()
        {
            InitializeComponent();
            BindingContext = new CustomerViewModel();
        }
    }
}
