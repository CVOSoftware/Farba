using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Farba.Common;
using ImageCluster;

namespace Farba.Model
{
    class Palette : BaseViewModel
    {
        #region Fields

        private string colorCombination;

        private BitmapImage image;

        private List<ClusterColor> cluster;

        private List<ColorComb> comb;

        #endregion

        #region Constructor

        public Palette(string fileName, BitmapImage image)
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
            set
            {
                colorCombination = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage Image { get; }

        public List<ClusterColor> Cluster
        {
            get => cluster;
            set
            {
                cluster = value;
                OnPropertyChanged();
            }
        }

        public List<ColorComb> Comb
        {
            get => comb;
            set
            {
                comb = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
