using System.Windows.Media;

namespace Farba.Extansion
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
