using System;
using System.Collections.Generic;
using ElectronicShopMobileApp.ViewModels;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Views
{
    public partial class ComfirmEmailPage : ContentPage
    { 

        public ComfirmEmailPage(string userName, string passWord, string fullName, string phoneNumber, string email, string address, byte[] avatarRegister)
        { 
            InitializeComponent();

            BindingContext = new ComfirmEmailViewModel(userName, passWord, fullName, phoneNumber, email, address, avatarRegister);
        }

        void txt1_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (txt1.Text.Length >= 1)
                txt2.Focus();
        }

        void txt2_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (txt2.Text.Length >= 1)
                txt3.Focus();
        }

        void txt3_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (txt3.Text.Length >= 1)
                txt4.Focus();
        }
         
    }
}
