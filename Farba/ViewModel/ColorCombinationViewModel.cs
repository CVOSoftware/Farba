using System.Windows.Media;
using Farba.Common.Clusters;
using Farba.Enum;
using Farba.ViewModel.Base;
using Farba.Extension;

namespace Farba.ViewModel
{
    class ColorCombinationViewModel : BaseViewModel
    {
        #region ViewModelFields

        private double difference;

        private string colorSpaceOne;

        private string colorSpaceTwo;

        private SolidColorBrush brushOne;

        private SolidColorBrush brushTwo;

        #endregion

        #region Constructor

        public ColorCombinationViewModel(ClusterColor one, ClusterColor two, ColorSpaceType type)
        {
            brushOne = one.Brush;
            brushTwo = two.Brush;
            ColorOne = one.Color;
            ColorTwo = two.Color;
            SetColorSpaceText(type);
        }

        #endregion

        #region ViewModelProperties

        public double Difference
        {
            get => difference;
            set => SetValue(ref difference, value);
        }

        public string ColorSpaceOne
        {
            get => colorSpaceOne;
            set => SetValue(ref colorSpaceOne, value);
        }

        public string ColorSpaceTwo
        {
            get => colorSpaceTwo;
            set => SetValue(ref colorSpaceTwo, value);
        }

        public Color ColorOne { get; }

        public Color ColorTwo { get; }

        public SolidColorBrush BrushOne
        {
            get => brushOne;
            private set => SetValue(ref brushOne, value);
        }

        public SolidColorBrush BrushTwo
        {
            get => brushTwo;
            private set => SetValue(ref brushTwo, value);
        }

        #endregion

        #region Public

        public void ReverseHex()
        {
            var temp = ColorSpaceOne;
            ColorSpaceOne = ColorSpaceTwo;
            ColorSpaceTwo = temp;
        }

        public void ReverseBrush()
        {
            var temp = BrushOne;
            BrushOne = BrushTwo;
            BrushTwo = temp;
        }

        public void SetColorSpaceText(ColorSpaceType type)
        {
            switch (type)
            {
                case ColorSpaceType.HEX:
                    ColorSpaceOne = ColorOne.HexFormat();
                    ColorSpaceTwo = ColorTwo.HexFormat();
                    break;
                case ColorSpaceType.RGB:
                    ColorSpaceOne = ColorOne.RgbFormat();
                    ColorSpaceTwo = ColorTwo.RgbFormat();
                    break;
            }
        }

        #endregion
    }
}
