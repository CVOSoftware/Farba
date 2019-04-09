namespace ColorModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CMY
    {
        private double _C;
        private double _M;
        private double _Y;

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

        public CMY(double c, double m, double y)
        {
            this._C = c;
            this._M = m;
            this._Y = y;
        }
    }
}
