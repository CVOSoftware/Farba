using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using Farba.Extansion;

namespace Farba.Resources.Converter
{
    class HEXConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var format = ((Color)value).HexFormat();
            return $"HEX: {format}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
