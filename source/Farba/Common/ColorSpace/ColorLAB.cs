using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farba.Common.ColorSpace
{
    public struct ColorLAB
    {
        public double L { get; }

        public double A { get; }

        public double B { get; }

        public ColorLAB(double l, double a, double b)
        {
            L = l;
            A = a;
            B = b;
        }
    }
}
