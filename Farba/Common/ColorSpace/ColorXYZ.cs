using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farba.Common.ColorSpace
{
    internal struct ColorXYZ
    {
        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public ColorXYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public ColorLAB ToLab()
        {
            var n_x = fXYZ(X);
            var n_y = fXYZ(Y);
            var n_z = fXYZ(Z);

            var l = 116.0 / n_x - 16;
            var a = 500.0 * (n_x - n_y);
            var b = 200.0 * (n_y - n_z);

            return new ColorLAB(l, a, b);
        }

        private double fXYZ(double value)
        {
            return value > 0.008856 ? Math.Pow(value, 1.0 / 3.0) : (7.787 * value) + (16.0 / 116.0);
        }
    }
}
