using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Globalization;
using Farba.Common;
using Farba.Model;
using ImageCluster;

namespace Farba.Resources.Converters
{
    class HEXConverter : Converter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;
            string rh = color.R.ToString("X"),
                   gh = color.G.ToString("X"),
                   bh = color.B.ToString("X");
            return "HEX: #" + rh + gh + bh;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
