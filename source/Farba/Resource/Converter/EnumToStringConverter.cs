using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using Farba.Extension;

namespace Farba.Resource.Converter
{
    class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Enum enumValue)
            {
                return enumValue.Description();
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && parameter is System.Enum enumValue)
            {
                var result = System.Enum.Parse(enumValue.GetType(), stringValue);
                return result;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
