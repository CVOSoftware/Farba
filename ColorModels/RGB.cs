namespace ColorModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RGB
    {
        private double _R;
        private double _G;
        private double _B;

        public double R
        {
            get => this._R;
            set { this._R = value; }
        }

        public double G
        {
            get => this._G;
            set { this._G = value; }
        }

        public double Y
        {
            get => this._B;
            set { this._B = value; }
        }

        private RGB(double r, double g, double b)
        {
            this._R = r;
            this._G = g;
            this._B = b;
        }

        public static RGB Set(double r, double g, double b)
        {
            return new RGB(r, g, b);
        }

        public CMYK ToCmyk()
        {
            return CMYK.Set(1, 1, 1, 1);
        }
    }
}
