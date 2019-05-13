namespace Farba.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;
    using Farba.Common;
    using ImageCluster;

    class Palette : NotifyPropertyChanged
    {
        #region Fields
        private bool _isProcess;

        private string _fileName;

        private string _count;

        private BitmapImage _image;

        private List<ClusterColor> _cluster;

        private List<ColorComb> _comb;
        #endregion

        #region Constructors
        public Palette(string fileName, BitmapImage image)
        {
            _isProcess = true;
            _fileName = fileName;
            _image = image;
            _cluster = null;
            _comb = null;
        }
        #endregion

        #region Properties
        public bool IsProcess
        {
            get => _isProcess;
            set
            {
                _isProcess = value;
                OnPropertyChanged("IsProcess");
            }
        }

        public string FileName => _fileName;

        public string Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged("Count");
            }
        }

        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        public List<ClusterColor> Cluster
        {
            get => _cluster;
            set
            {
                _cluster = value;
                OnPropertyChanged("Cluster");
            }
        }

        public List<ColorComb> Comb
        {
            get => _comb;
            set
            {
                _comb = value;
                OnPropertyChanged("Comb");
            }
        }
        #endregion
    }
}
