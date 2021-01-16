using System;
using System.Globalization;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertSourceToOpacity : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ImageSource)value == null ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
