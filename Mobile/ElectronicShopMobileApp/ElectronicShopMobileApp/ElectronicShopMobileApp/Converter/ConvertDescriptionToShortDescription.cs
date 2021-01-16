using System;
using System.Globalization;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertDescriptionToShortDescription : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value.ToString()).Substring(0, 60) + ".....";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
