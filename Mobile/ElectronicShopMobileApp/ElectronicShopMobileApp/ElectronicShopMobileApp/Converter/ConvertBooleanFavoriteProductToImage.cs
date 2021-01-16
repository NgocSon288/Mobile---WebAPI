using System;
using System.Globalization;
using ElectronicShopMobileApp.Assets.Font;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertBooleanFavoriteProductToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return FontIcons.Heart; 
            }
            return FontIcons.HeartOutline; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
