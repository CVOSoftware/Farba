using System;
using System.Windows.Media;
using Farba.Common.ColorDifference.Base;
using Farba.Extansion;

namespace Farba.Common.ColorDifference
{
    internal class CIE94 : BaseColorDifference
    {
        private double Kl = 1;

        private double K1 = 0.045;

        private double K2 = 0.015;

        public CIE94(Color one, Color two) : base(one, two)
        {

        }

        protected override double CalculateAction()
        {
            var labOne = One.ToXyz().ToLab();
            var labTwo = Two.ToXyz().ToLab();
            var oneC = Math.Sqrt(labOne.A * labOne.A + labOne.B * labOne.B);
            var twoC = Math.Sqrt(labTwo.A * labTwo.A + labTwo.B * labTwo.B);
            var deltaA = labTwo.A - labOne.A;
            var deltaB = labTwo.B - labOne.B;
            var deltaL = labTwo.L - labOne.L;
            var deltaC = twoC - oneC;
            var deltaH = Math.Sqrt(deltaA * deltaA + deltaB * deltaB - deltaC * deltaC);
            var tempL = Math.Pow(deltaL / Kl, 2);
            var tempC = Math.Pow(deltaC / 1 + K1 * oneC, 2);
            var tempH = Math.Pow(deltaH / 1 + K2, 2);
            var result = Math.Sqrt(tempL + tempC + tempH);
            return Math.Round(result, 2);
        }
    }
}
