using System.Windows.Media;
using Farba.ViewModel.Base;

namespace Farba.ViewModel
{
    class ColorCombViewModel : BaseViewModel
    {
        #region Constructor

        public ColorCombViewModel(string hexOne, string hexTwo, SolidColorBrush brushOne, SolidColorBrush brushTwo)
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
