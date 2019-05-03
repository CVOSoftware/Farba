namespace Farba.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;
    using Farba.Common;

    class Palette : NotifyPropertyChanged
    {
        #region Fields
        private bool _isProcess;

        private string _fileName;

        private BitmapImage _image;
        #endregion

        #region Constructors
        public Palette(string fileName, BitmapImage image)
        {
            _isProcess = true;
            _fileName = fileName;
            _image = image;
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

        public BitmapImage Image => _image;
        #endregion
    }
}
