using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Farba.Resources.Converter
{
    internal class AdaptiveWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var width = (double)value - 10;
            return (width / 5) - 26;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
