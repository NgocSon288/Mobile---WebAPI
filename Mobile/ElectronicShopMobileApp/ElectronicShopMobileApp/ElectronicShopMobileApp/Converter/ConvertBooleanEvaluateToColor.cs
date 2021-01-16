﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Converter
{
    public class ConvertBooleanEvaluateToColor : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(bool)value)
                return Color.Transparent;
            else
            {
                return Color.Red;
            } 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}