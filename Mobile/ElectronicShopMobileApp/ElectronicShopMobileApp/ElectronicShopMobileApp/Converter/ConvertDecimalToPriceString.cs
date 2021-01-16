using System;
using System.Globalization;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertDecimalToPriceString : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.ToString().Length <= 3)
            {
                return value;
            }

            var price = (decimal)value;
            var result = price.ToString("#,##");
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return decimal.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
