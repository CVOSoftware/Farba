using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Farba.Common.Extansion
{
    internal static class ColorExtansion
    {
        public static string HexFormat(this Color color)
        {
            return $"#{color.R.ToString("X2")}{color.G.ToString("X2")}{color.B.ToString("X2")}";
        }

        public static string RgbFormat(this Color color)
        {
            return $"{color.R}, {color.G}, {color.B}";
        }

        public static SolidColorBrush GetBrash(this Color color)
        {
            return new SolidColorBrush(color);
        }
    }
}
