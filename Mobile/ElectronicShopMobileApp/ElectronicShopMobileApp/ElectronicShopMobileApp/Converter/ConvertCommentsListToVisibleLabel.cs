using System;
using System.Collections.Generic;
using System.Globalization;
using ElectronicShopMobileApp.Models;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertCommentsListToVisibleLabel : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = value as List<Comment>;

            if (list == null || list.Count <= 0)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
