namespace ColorModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CMYK
    {
        private double _C;
        private double _M;
        private double _Y;
        private double _K;

        public double C
        {
            get => this._C;
            set { this._C = value; }
        }

        public double M
        {
            get => this._M;
            set { this._M = value; }
        }

        public double Y
        {
            get => this._Y;
            set { this._Y = value; }
        }

        public double K
        {
            get => this._K;
            set { this._K = value; }
        }

        private CMYK(double c, double m, double y, double k)
        {
            this._C = c;
            this._M = m;
            this._Y = y;
            this._K = k;
        }

        public static CMYK Set(double c, double m, double y, double k)
        {
            return new CMYK(c, m, y, k);
        }

        public RGB ToRgb()
        {
            double r = (1 - this._C) * (1 - this._K),
                   g = (1 - this._M) * (1 - this._K),
                   b = (1 - this._Y) * (1 - this._K);
            return RGB.Set(r, g, b);
        }
    }
}
