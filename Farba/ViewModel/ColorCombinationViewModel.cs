using System.Windows.Media;
using Farba.ViewModel.Base;

namespace Farba.ViewModel
{
    class ColorCombinationViewModel : BaseViewModel
    {
        #region ViewModelFields

        private string hexOne;

        private string hexTwo;

        private SolidColorBrush brushOne;

        private SolidColorBrush brushTwo;

        #endregion

        #region Constructor

        public ColorCombinationViewModel(string hexOne, string hexTwo, SolidColorBrush brushOne, SolidColorBrush brushTwo)
        {
            this.hexOne = hexOne;
            this.hexTwo = hexTwo;
            this.brushOne = brushOne;
            this.brushTwo = brushTwo;
        }

        #endregion

        #region ViewModelProperties

        public string HexOne
        {
            get => hexOne;
            set => SetValue(ref hexOne, value);
        }

        public string HexTwo
        {
            get => hexTwo;
            set => SetValue(ref hexTwo, value);
        }

        public SolidColorBrush BrushOne
        {
            get => brushOne;
            set => SetValue(ref brushOne, value);
        }

        public SolidColorBrush BrushTwo
        {
            get => brushTwo;
            set => SetValue(ref brushTwo, value);
        }

        #endregion

        #region Public

        public void ReverseHex()
        {
            var temp = HexOne;
            HexOne = HexTwo;
            HexTwo = temp;
        }

        public void ReverseBrush()
        {
            var temp = BrushOne;
            BrushOne = BrushTwo;
            BrushTwo = temp;
        }

        #endregion
    }
}
