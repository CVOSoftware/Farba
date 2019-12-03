using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Farba.Common.Clusters;
using Farba.Common.ColorDifference;
using Farba.Common.ColorDifference.Base;
using Farba.ViewModel.Base;

namespace Farba.ViewModel
{
    class ColorCombinationViewModel : BaseViewModel
    {
        #region ViewModelFields

        private double difference;

        private string hexOne;

        private string hexTwo;

        private SolidColorBrush brushOne;

        private SolidColorBrush brushTwo;

        #endregion

        #region Constructor

        public ColorCombinationViewModel(ClusterColor one, ClusterColor two)
        {
            hexOne = one.Hex;
            hexTwo = two.Hex;
            brushOne = one.Brush;
            brushTwo = two.Brush;
        }

        #endregion

        #region ViewModelProperties

        public double Difference
        {
            get => difference;
            private set => SetValue(ref difference, value);
        }

        public string HexOne
        {
            get => hexOne;
            private set => SetValue(ref hexOne, value);
        }

        public string HexTwo
        {
            get => hexTwo;
            private set => SetValue(ref hexTwo, value);
        }

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
