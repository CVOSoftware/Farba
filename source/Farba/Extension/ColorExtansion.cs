using System;
using System.Windows.Media;
using Farba.Common.ColorSpace;

namespace Farba.Extension
{
    internal static class ColorExtension
    {
        public static string HexFormat(this Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public static string RgbFormat(this Color color)
        {
            return $"{color.R}, {color.G}, {color.B}";
        }

        public static SolidColorBrush GetBrash(this Color color)
        {
            return new SolidColorBrush(color);
        }

        public static ColorXYZ ToXyz(this Color color)
        {
            var n_r = NormalizeRGB(color.R);
            var n_g = NormalizeRGB(color.G);
            var n_b = NormalizeRGB(color.B);

            var s_r = ConvertRGBTosRGB(n_r) * 100;
            var s_g = ConvertRGBTosRGB(n_g) * 100;
            var s_b = ConvertRGBTosRGB(n_b) * 100;

            var x = s_r * 0.4124 + s_g * 0.3576 + s_b * 0.1805;
            var y = s_r * 0.2126 + s_g * 0.7152 + s_b * 0.0722;
            var z = s_r * 0.0193 + s_g * 0.1192 + s_b * 0.9505;
            return new ColorXYZ(x, y, z);
        }

        private static double NormalizeRGB(byte value)
        {
            return value / 255.0;
        }

        private static double ConvertRGBTosRGB(double value)
        {
            return value > 0.04045 ? Math.Pow(value + 0.055 / 1.055, 2.2) : value / 12.92;
        }
    }
}
