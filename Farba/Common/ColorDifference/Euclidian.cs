using System;
using System.Windows.Media;
using Farba.Common.ColorDifference.Base;

namespace Farba.Common.ColorDifference
{
    internal class Euclidian : BaseColorDifference
    {
        #region Properties

        private double ApproximationRValue { get; set; }

        private double Delta { get; set; }

        private double DeltaR { get; set; }

        private double DeltaG { get; set; }

        private double DeltaB { get; set; }

        #endregion

        #region Constructor

        public Euclidian(Color one, Color two) : base(one, two)
        {

        }

        #endregion

        #region Implemented BaseColorDifference

        protected override double CalculateAction()
        {
            ApproximationR();
            CalculateDeltaR();
            CalculateDeltaG();
            CalculateDeltaB();
            CalculateDelta();
            return Math.Sqrt(Delta);
        }

        #endregion

        #region Methods

        private void ApproximationR()
        {
            ApproximationRValue = One.R + Two.R / 2.0;
        }

        private void CalculateDeltaR()
        {
            DeltaR = (2.0 + ApproximationRValue / 256) * Math.Pow(Two.R - One.R, 2);
        }

        private void CalculateDeltaG()
        {
            DeltaG = 4.0 * Math.Pow(Two.G - One.G, 2);
        }

        private void CalculateDeltaB()
        {
            DeltaB = (2.0 + (255 - ApproximationRValue) / 256) * Math.Pow(Two.B - One.B, 2);
        }

        private void CalculateDelta()
        {
            Delta = DeltaR + DeltaG + DeltaB;
        }

        #endregion
    }
}
