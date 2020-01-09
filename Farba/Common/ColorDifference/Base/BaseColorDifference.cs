using System.Windows.Media;

namespace Farba.Common.ColorDifference.Base
{
    public abstract class BaseColorDifference
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
    }
}
