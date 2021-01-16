using System;
using System.Collections.ObjectModel;
using System.Globalization;
using ElectronicShopMobileApp.Models.SQLiteModels;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertCartItemListToVisibleBooleean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value <= 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 1;
        }
    }
}
