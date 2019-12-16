using System;
using System.Windows.Media;
using Farba.Common.ColorDifference.Base;

namespace Farba.Common.ColorDifference
{
    internal class CIE74 : BaseColorDifference
    {
        public CIE74(Color one, Color two) : base(one, two)
        {

        }

        protected override double CalculateAction()
        {
            var xyzOne = RGBtoXYZ(One);
            var xyzTwo = RGBtoXYZ(Two);
            var labOne = XYZtoLAB(xyzOne);
            var labTwo = XYZtoLAB(xyzTwo);
            var deltaL = Math.Pow(labTwo.L - labOne.L, 2);
            var deltaA = Math.Pow(labTwo.A - labOne.A, 2); 
            var deltaB = Math.Pow(labTwo.B - labOne.B, 2);
            var result = Math.Sqrt(deltaL + deltaA + deltaB);
            return Math.Round(result, 2);
        }
    }
}
