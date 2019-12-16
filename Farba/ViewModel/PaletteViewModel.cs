using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Farba.ViewModel.Base;
using Farba.Common.Clusters;
using Farba.Common.ColorDifference;
using Farba.Common.ColorDifference.Base;
using Farba.Enum;

namespace Farba.ViewModel
{
    class PaletteViewModel : BaseViewModel
    {
        #region CommandFields

        private RelayCommand reverseColorCommand;

        #endregion

        #region ViewModelFields

        private bool isProcess;

        private string colorCombination;

        private string selectColorDifferenceType;

        private ColorCombinationType colorCombinationType;

        private List<ClusterColor> cluster;

        private List<ColorCombinationViewModel> colorCombinatinList;

        #endregion

        #region Constructor

        public PaletteViewModel(string fileName, BitmapImage image)
        {
            isProcess = true;
            FileName = fileName;
            colorCombinationType = ColorCombinationType.Square;
            Image = image;
            cluster = null;
            colorCombinatinList = null;
            ColorDifferenceCollection = new List<string>
            {
                "CIE76",
                "CIE94"
            };
            SelectColorDifferenceType = ColorDifferenceCollection.First();
        }

        #endregion

        #region CommandProperties

        public ICommand ReverseColorCommand => RelayCommand.Register(ref reverseColorCommand, OnReverseColor, CanReverseColor);

        #endregion

        #region ViewModelProperties

        public bool IsProcess
        {
            get => isProcess;
            set => SetValue(ref isProcess, value);
        }

        public string FileName { get; }

        public string CombinationCount
        {
            get => colorCombination;
            set => SetValue(ref colorCombination, value);
        }

        public string SelectColorDifferenceType
        {
            get => selectColorDifferenceType;
            set
            {
                if (SetValue(ref selectColorDifferenceType, value)
                    && ColorCombinationList != null)
                {
                    SetColorDifferenceValue();
                }
            }
        }

        public ColorCombinationType ColorCombinationType
        {
            get => colorCombinationType;
            set => SetValue(ref colorCombinationType, value);
        }


        public BitmapImage Image { get; }

        public List<string> ColorDifferenceCollection { get; }

        public List<ClusterColor> Cluster
        {
            get => cluster;
            set => SetValue(ref cluster, value);
        }

        public List<ColorCombinationViewModel> ColorCombinationList
        {
            get => colorCombinatinList;
            set
            {
                if (SetValue(ref colorCombinatinList, value))
                {
                    SetColorDifferenceValue();
                }
            }
        }

        #endregion

        #region CommandExecuteMethod

        private void OnReverseColor(object param)
        {
            foreach (var colorCombinationItem in ColorCombinationList)
            {
                colorCombinationItem.ReverseHex();
                colorCombinationItem.ReverseBrush();
            }
        }

        #endregion

        #region CommandCanExecuteMethods

        private bool CanReverseColor(object param)
        {
            return ColorCombinationList != null;
        }

        #endregion

        #region Private

        private BaseColorDifference GetColorDiffAlgorithm(Color colorOne, Color colorTwo)
        {
            switch (SelectColorDifferenceType)
            {
                case "CIE76":
                    return new CIE76(colorOne, colorTwo);
                case "CIE94":
                    return new CIE94(colorOne, colorTwo);
            }

            return default;
        }

        private void SetColorDifferenceValue()
        {
            foreach (var colorComb in ColorCombinationList)
            {
                var colorDifferenceAlgorithm = GetColorDiffAlgorithm(colorComb.ColorOne, colorComb.ColorTwo);
                colorComb.Difference = colorDifferenceAlgorithm.CalculateTheDifference();
            }
        }

        #endregion
    }
}
