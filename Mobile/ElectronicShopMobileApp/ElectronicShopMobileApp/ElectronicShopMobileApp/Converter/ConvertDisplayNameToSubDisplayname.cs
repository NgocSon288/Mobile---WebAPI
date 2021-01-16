using System;
using System.Globalization;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertDisplayNameToSubDisplayname : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return value.ToString().Substring(0, 30) + "...";
            }
            catch (Exception ex)
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
