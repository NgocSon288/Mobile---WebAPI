using System;
using System.Globalization;
using ElectronicShopMobileApp.Assets.Font;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertBitStarNumberToStarIcon : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return FontIcons.StarMinusOutline; 
            return (int)value == 1 ? FontIcons.Star : FontIcons.StarOutline;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
