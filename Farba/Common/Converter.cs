using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Farba.Common
{
    public abstract class Converter : MarkupExtension, IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
