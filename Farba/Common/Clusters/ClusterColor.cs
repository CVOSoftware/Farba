using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Farba.Common.Clusters
{
    public class ClusterColor
    {
        #region Fields
        private Color _color;
        private SolidColorBrush _brush;
        private string _hex;
        private string _rgb;
        private double _percent;
        #endregion

        #region Constructor
        public ClusterColor(byte r, byte g, byte b, double percent)
        {
            _color = Color.FromRgb(r, g, b);
            _brush = new SolidColorBrush(_color);
            _percent = percent;
            HexFormat();
            RgbFormat();
        }
        #endregion

        #region Properties
        public Color Color => _color;

        public SolidColorBrush Brush => _brush;

        public string Hex => _hex;

        public string Rgb => _rgb;


        public double Percent => _percent;
        #endregion

        #region Methods
        private void HexFormat()
        {
            string temp = "#";
            temp += _color.R.ToString("X");
            temp += _color.G.ToString("X");
            temp += _color.B.ToString("X");
            _hex = temp;
        }

        private void RgbFormat()
        {
            _rgb = _color.R + ", " + _color.G + ", " + _color.B;
        }
        #endregion
    }
}
