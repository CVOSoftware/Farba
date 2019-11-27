using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using Farba.Enum;

namespace Farba.Resource.Converter
{
    class ColorCombinationTypeConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is ColorCombinationType type && type.Equals(parameter) ? true : DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool flag ? parameter : DependencyProperty.UnsetValue;
        }
    }
}