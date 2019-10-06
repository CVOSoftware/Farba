using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Farba.Model
{
    class ColorComb
    {
        #region Constructor

        public ColorComb(string hexOne, string hexTwo, SolidColorBrush brushOne, SolidColorBrush brushTwo)
        {
            HexOne = hexOne;
            HexTwo = hexTwo;
            BrushOne = brushOne;
            BrushTwo = brushTwo;
        }

        #endregion

        #region Properties

        public string HexOne { get; }

        public string HexTwo { get; }

        public SolidColorBrush BrushOne { get; }

        public SolidColorBrush BrushTwo { get; }

        #endregion
    }
}
