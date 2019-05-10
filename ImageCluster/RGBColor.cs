using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCluster
{
    public class RGBColor
    {
        #region Fields
        public byte _r;
        public byte _g;
        public byte _b;
        #endregion

        #region Constructor
        public RGBColor(byte r, byte g, byte b)
        {
            _r = r;
            _g = g;
            _b = b;
        }
        #endregion

        #region Properties
        public byte R => _r;
        public byte G => _g;
        public byte B => _b;
        #endregion
    }
}
