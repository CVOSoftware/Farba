using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Farba.ViewModel.Base;
using Farba.Common.Clusters;
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

        public ColorCombinationType ColorCombinationType
        {
            get => colorCombinationType;
            set => SetValue(ref colorCombinationType, value);
        }

        public BitmapImage Image { get; }

        public List<ClusterColor> Cluster
        {
            get => cluster;
            set => SetValue(ref cluster, value);
        }

        public List<ColorCombinationViewModel> ColorCombinationList
        {
            get => colorCombinatinList;
            set => SetValue(ref colorCombinatinList, value);
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

        private bool CanReverseColor(object param)
        {
            return ColorCombinationList != null;
        }

        #endregion
    }
}
