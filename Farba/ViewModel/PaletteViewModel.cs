using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Farba.ViewModel.Base;
using Farba.Common.Clusters;

namespace Farba.ViewModel
{
    class PaletteViewModel : BaseViewModel
    {
        #region Fields

        private string colorCombination;

        private BitmapImage image;

        private List<ClusterColor> cluster;

        private List<ColorCombViewModel> comb;

        #endregion

        #region Constructor

        public PaletteViewModel(string fileName, BitmapImage image)
        {
            IsProcess = true;
            FileName = fileName;
            Image = image;
            cluster = null;
            comb = null;
        }

        #endregion

        #region Properties

        public bool IsProcess { get; set; }

        public string FileName { get; }

        public string ColorCombination
        {
            get => colorCombination;
            set => SetValue(ref colorCombination, value);
        }

        public BitmapImage Image { get; }

        public List<ClusterColor> Cluster
        {
            get => cluster;
            set => SetValue(ref cluster, value);
        }

        public List<ColorCombViewModel> Comb
        {
            get => comb;
            set => SetValue(ref comb, value);
        }

        #endregion
    }
}
