using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Farba.Common.ColorDifference.Base;

namespace Farba.Common.ColorDifference
{
    internal class CIEDE2000 : BaseColorDifference
    {
        public CIEDE2000(Color one, Color two) : base(one, two)
        {

        }

        protected override double CalculateAction()
        {
            return default;
        }
    }
}
