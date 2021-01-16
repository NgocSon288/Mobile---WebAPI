using System;
using System.Collections.Generic;
using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.ViewModels;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Views
{
    public partial class MessagePage : ContentPage
    {
        public MessagePage(string title, string message, Assets.Contains.Page page = Assets.Contains.Page.LOGIN_PAGE)
        {
            InitializeComponent();

            BindingContext = new MessageViewModel(title, message, page);
        }
    }
}
