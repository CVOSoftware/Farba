using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farba.Common.ColorDifference.Base
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
    }
}
