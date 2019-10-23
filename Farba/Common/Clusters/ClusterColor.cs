using System.Windows.Media;
using Farba.Common.Extansion;

namespace Farba.Common.Clusters
{
    public class ClusterColor
    {
        #region Const

        private const int ALPHA = 255;

        #endregion

        #region Constructor

        public ClusterColor(int r, int g, int b)
        {
            Color = Color.FromArgb(ALPHA, (byte)r, (byte)g, (byte)b);
            Brush = Color.GetBrash();
            Hex = Color.HexFormat();
            Rgb = Color.RgbFormat();
        }

        #endregion

        #region Properties

        public Color Color { get; }

        public SolidColorBrush Brush { get; }

        public string Hex { get; }

        public string Rgb { get; }

        #endregion
    }
}
