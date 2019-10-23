﻿using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using Farba.Common.Extansion;

namespace Farba.Resources.Converters
{
    class BrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Color)value).GetBrash();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
