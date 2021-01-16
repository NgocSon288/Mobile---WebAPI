using ElectronicShopMobileApp.Models;
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
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage(int ID)
        {
            InitializeComponent();
            BindingContext = new ProductDetailViewModel(ID);


            Device.StartTimer(TimeSpan.FromSeconds(2), (Func<bool>)(() =>
            {
                CarouselViewer.Position = (CarouselViewer.Position + 1) % 4;
                return true;
            }));
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Button btn = (sender as Button);
            switch (btn.Text == "Thêm vào giỏ")
            {
                case true:
                    ButtonAddToCart.IsVisible = true;
                    ButtonBuyNow.IsVisible = false;
                    break;
                default:
                    ButtonAddToCart.IsVisible = false;
                    ButtonBuyNow.IsVisible = true;
                    break;
            }

            GridFilter.TranslationY = DeviceDisplay.MainDisplayInfo.Height;
            GridFilter.TranslationX = 0;
            GridFilter.Opacity = 0;
            GridFilter.IsVisible = true;

            GridFilter.FadeTo(1, 400);

            await GridFilter.TranslateTo(0, 0, 400, Easing.SinInOut);

            StackLayoutBetween.IsVisible = true;

        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            GridFilter.TranslationX = 0;
            GridFilter.Opacity = 1;

            StackLayoutBetween.IsVisible = false;

            GridFilter.FadeTo(0, 400);
            await GridFilter.TranslateTo(0, DeviceDisplay.MainDisplayInfo.Height, 400, Easing.SinInOut);

            GridFilter.IsVisible = false;

            StackLayoutProductDetail.IsEnabled = true;
        }

    }
}