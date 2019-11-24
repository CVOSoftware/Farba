using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using Farba.Extansion;

namespace Farba.Resources.Converters
{
    class RGBConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var format = ((Color)value).RgbFormat();
            return $"RGB: {format}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
