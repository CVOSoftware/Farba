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
        #region Fields
        private string _hexOne;
        private string _hexTwo;
        private SolidColorBrush _brushOne;
        private SolidColorBrush _brushTwo;
        #endregion

        #region Constructor
        public ColorComb(string hexOne, string hexTwo, SolidColorBrush brushOne, SolidColorBrush brushTwo)
        {
            this._hexOne = hexOne;
            this._hexTwo = hexTwo;
            this._brushOne = brushOne;
            this._brushTwo = brushTwo;
        }
        #endregion

        #region Properties
        public string HexOne => _hexOne;
        public string HexTwo => _hexTwo;
        public SolidColorBrush BrushOne => _brushOne;
        public SolidColorBrush BrushTwo => _brushTwo;
        #endregion
    }
}
