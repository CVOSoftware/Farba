using System.Windows.Media;
using Farba.Extension;

namespace Farba.Common.Clusters
{
    public class ClusterColor
    {
        #region Const

        private const int ALPHA = 255;

        #endregion

        #region Constructor

        public ClusterColor(double percent, int r, int g, int b)
        {
            Percent = percent;
            Color = Color.FromArgb(ALPHA, (byte)r, (byte)g, (byte)b);
            Brush = Color.GetBrash();
            Hex = Color.HexFormat();
            Rgb = Color.RgbFormat();
        }

        #endregion

        #region Properties

        public double Percent { get; }

        public Color Color { get; }

        public SolidColorBrush Brush { get; }

        public string Hex { get; }

        public string Rgb { get; }

        #endregion
    }
}
