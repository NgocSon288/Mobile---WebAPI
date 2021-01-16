using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ElectronicShopMobileApp.Models;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertProductListToProductListRight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            ObservableCollection<Product> products = value as ObservableCollection<Product>;
            var d = 0;

            return products.Where(product => d++ % 2 != 0);
        }
         
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
