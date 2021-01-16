using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertStarNumberToStarList : IValueConverter
    {  
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var starNumber = (int)value;
            var result = new List<int>();
            for(int i = 1; i <= 5; i++)
            {
                if (starNumber-- > 0)
                {
                    result.Add(1);
                }
                else
                {
                    result.Add(0);
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 1;
        }
    }
}
