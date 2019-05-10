using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ImageCluster
{
    public struct ClusterColor
    {
        #region Fields
        private Color _color;
        private double _percent;
        #endregion

        #region Constructor
        public ClusterColor(byte r, byte g, byte b, double percent)
        {
            _color = Color.FromRgb(r, g, b);
            _percent = percent;
        }
        #endregion

        #region Properties
        public Color Color => _color;

        public double Percent => _percent;
        #endregion
    }
}
