using ElectronicShopMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElectronicShopMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        public ProductsPage(int categoryID, string textSearch=null)
        {
            InitializeComponent();
            BindingContext = new ProductsViewModel(categoryID, textSearch);
        }
         

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            GridFilter.TranslationX = DeviceDisplay.MainDisplayInfo.Width;   // width
            GridFilter.Opacity = 0;
            GridFilter.IsVisible = true;

            GridFilter.FadeTo(1, 400);
            GridFilter.TranslateTo(50, 0, 400, Easing.SinInOut);

            StackLayoutBetween.IsVisible = true;

            StackLayoutProducts.IsEnabled = false;
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            GridFilter.TranslationX = 50;
            GridFilter.Opacity = 1;

            StackLayoutBetween.IsVisible = false;

            await GridFilter.TranslateTo(DeviceDisplay.MainDisplayInfo.Width, 0, 900, Easing.SinInOut);  // width
            await GridFilter.FadeTo(0, 400);

            GridFilter.IsVisible = false;

            StackLayoutProducts.IsEnabled = true;
        }
    }
}