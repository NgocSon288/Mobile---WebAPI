using System;
using System.Collections.Generic;
using ElectronicShopMobileApp.ViewModels;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Views
{
    public partial class ModifyProfilePage : ContentPage
    {
        public ModifyProfilePage()
        {
            InitializeComponent();
            BindingContext = new ModifyProfileViewModel();
        }
    }
}
