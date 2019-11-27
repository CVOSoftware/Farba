using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Farba.Resource.Converter
{
    internal class AdaptiveHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var width = (double)value;
            return width + width / 3.5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
