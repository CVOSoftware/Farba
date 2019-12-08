using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Farba.Common.ColorDifference.Base
{
    internal abstract class BaseColorDifference
    {
        protected bool IsNotCalculated { get; set; }

        protected double Calculated { get; set; }

        protected Color One { get; }

        protected Color Two { get; }

        protected BaseColorDifference(Color one, Color two)
        {
            IsNotCalculated = true;
            Calculated = 0;
            One = one;
            Two = two;
        }

        protected abstract double CalculateAction();

        public double CalculateTheDifference()
        {
            if (IsNotCalculated)
            {
                IsNotCalculated = false;
                return CalculateAction();
            }

            return Calculated;
        }

        #region Color converter

        private double NormalizeRGB(byte value)
        {
            return value / 255;
        }

        private double ConvertRGBTosRGB(double value)
        {
            return value > 0.04045 ? Math.Pow(value + 0.055 / 1.055, 2.2) : value / 12.92;
        }

        private double fXYZ(double value)
        {
            return value > 0.008856 ? Math.Pow(value, 1 / 3) : (7.787 * value) + (16 / 116);
        }

        protected ColorXYZ RGBtoXYZ(Color colorRGB)
        {
            var n_r = NormalizeRGB(colorRGB.R);
            var n_g = NormalizeRGB(colorRGB.G);
            var n_b = NormalizeRGB(colorRGB.B);

            var s_r = ConvertRGBTosRGB(n_r) * 100;
            var s_g = ConvertRGBTosRGB(n_g) * 100;
            var s_b = ConvertRGBTosRGB(n_b) * 100;

            var x = s_r * 0.4124 + s_g * 0.3576 + s_b * 0.1805;
            var y = s_r * 0.2126 + s_g * 0.7152 + s_b * 0.0722;
            var z = s_r * 0.0193 + s_g * 0.1192 + s_b * 0.9505;
            return new ColorXYZ(x, y, z);
        }

        protected ColorLAB XYZtoLAB(ColorXYZ colorXYZ)
        {
            var n_x = fXYZ(colorXYZ.X);
            var n_y = fXYZ(colorXYZ.Y);
            var n_z = fXYZ(colorXYZ.Z);

            var l = 116 / n_x - 16;
            var a = 500 * (n_x - n_y);
            var b = 200 * (n_y - n_z);

            return new ColorLAB(l, a, b);
        }

        #endregion
    }
}
