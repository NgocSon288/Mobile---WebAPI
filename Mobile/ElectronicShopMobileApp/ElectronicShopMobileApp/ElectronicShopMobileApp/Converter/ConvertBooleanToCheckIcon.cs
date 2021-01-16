using System;
using System.Globalization;
using ElectronicShopMobileApp.Assets.Font;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertBooleanToCheckIcon : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? FontIcons.CheckboxMarked : FontIcons.CheckboxBlankOutline;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
