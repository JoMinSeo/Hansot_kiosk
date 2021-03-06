﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Hansot_kiosk.Converter
{
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = (value is bool) ? (bool)value : false;
            return flag ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
