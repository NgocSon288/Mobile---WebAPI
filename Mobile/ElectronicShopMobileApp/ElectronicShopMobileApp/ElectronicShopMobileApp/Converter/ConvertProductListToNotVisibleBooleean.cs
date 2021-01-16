using System;
using System.Collections.ObjectModel;
using System.Globalization;
using ElectronicShopMobileApp.Models;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertProductListToNotVisibleBooleean : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var products = value as ObservableCollection<Product>;

            if (products == null || products.Count <= 0)
            {
                return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
