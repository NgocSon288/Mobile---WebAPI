using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicShopMobileApp.ViewModels;
using Xamarin.Forms; 

namespace ElectronicShopMobileApp.Views
{
    public partial class RegisterAccountPage : ContentPage
    {
        
        public RegisterAccountPage()
        {
            InitializeComponent();

            BindingContext = new RegisterAccountViewModel();
             
        } 
    }
}
