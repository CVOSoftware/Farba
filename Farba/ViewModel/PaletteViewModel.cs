using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using Farba.Common;
using Farba.Helpers;
using Farba.Model;
using ImageCluster;

namespace Farba.ViewModel
{ 
    class PaletteViewModel : BaseViewModel
    {
        #region CommandFields

        private RelayCommand selectImageCommand;

        private RelayCommand createPaletteCommand;

        private RelayCommand removeImageCommand;

        private RelayCommand switchFirstTabImageViewerCommand;

        private RelayCommand nextImageCommand;

        private RelayCommand prevImageCommand;

        #endregion

        #region ViewModelFields

        private ObservableCollection<Palette> _palettes;

        private Palette _activePalette;

        private int _imageViewerTab;

        private bool _processState;

        private bool _deleteState;

        private bool _leftArrowState;

        private bool _rightArrowState;

        private string _imageViewerConter;

        private List<ColorComb> _combination;

        #endregion

        #region Constructor

        public PaletteViewModel()
        {
            _palettes = new ObservableCollection<Palette>();
            _activePalette = null;
            _imageViewerTab = 0;
            _processState = false;
            _deleteState = false;
            _leftArrowState = false;
            _rightArrowState = false;
            _imageViewerConter = string.Empty;
            _combination = null;
        }

        #endregion

        #region CommandProperties

        public ICommand SelectImageCommand => RelayCommand.Register(ref selectImageCommand, OnSelectImage);
        public ICommand CreatePaletteCommand => RelayCommand.Register(ref createPaletteCommand, OnCreatePalette);
        public ICommand RemoveImageCommand => RelayCommand.Register(ref removeImageCommand, OmRemoveImage);
        public ICommand SwitchFirstTabImageViewerCommand => RelayCommand.Register(ref switchFirstTabImageViewerCommand, OnSwitchFirstTabImageViewer);
        public ICommand NextImageCommand => RelayCommand.Register(ref nextImageCommand, OnNextImage);
        public ICommand PrevImageCommand => RelayCommand.Register(ref prevImageCommand, OnPrevImage);
        
        #endregion

        #region ViewModelProperties

        public ObservableCollection<Palette> Palettes
        {
            get => _palettes;
            set
            {
                _palettes = value;
                OnPropertyChanged();
            }
        }

        public Palette ActivePalette
        {
            get => _activePalette;
            set
            {
                _activePalette = value;
                OnPropertyChanged();
            }
        }

        public List<ColorComb> Combination
        {
            get => _combination;
            set
            {
                _combination = value;
                OnPropertyChanged();
            }
        }
        
        public int ImageViewerTab
        {
            get => _imageViewerTab;
            set
            {
                _imageViewerTab= value;
                OnPropertyChanged();
            }
        }
        
        public bool ProcessState
        {
            get => _processState;
            set
            {
                _processState = value;
                OnPropertyChanged();
            }
        }

        public bool DeleteState
        {
            get => _deleteState;
            set
            {
                _deleteState = value;
                OnPropertyChanged();
            }
        }
        
        public bool LeftArrowState
        {
            get => _leftArrowState;
            set
            {
                _leftArrowState = value;
                OnPropertyChanged();
            }
        }

        public bool RightArrowState
        {
            get => _rightArrowState;
            set
            {
                _rightArrowState = value;
                OnPropertyChanged();
            }
        }
        
        public string ImageViewerCounter
        {
            get => _imageViewerConter;
            set
            {
                _imageViewerConter = value;
                OnPropertyChanged();
            }
        }
        
        #endregion

        #region CommandExecuteMethods
        private void OnSelectImage(object o)
        {
            var fileName = DialogWindowHelper.FileDialog(FileFilter.Images);
            if(fileName != string.Empty)
            {
                bool match = true;
                int count = _palettes.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (_palettes[i].FileName == fileName)
                        {
                            match = false;
                            break;
                        }
                    }
                }
                if (match)
                {
                    Uri uri = new Uri(fileName);
                    BitmapImage image = new BitmapImage(uri);
                    Palette palette = new Palette(fileName, image);
                    Palettes.Add(palette);
                    ActivePalette = palette;
                    DeleteState = true;
                    ImageViewerCounterFormat();
                }
            }
            ProcessState = true;
            SwitchArrowState();
        }
        
        private void OnCreatePalette(object o)
        {
            List<ClusterColor> clusterColor = Handler.RandomColor(5);
            _activePalette.Cluster = clusterColor;
            ActivePalette.IsProcess = false;
            ProcessState = ActivePalette.IsProcess;
            SetCombination();
        }

        private void OmRemoveImage(object o)
        {
            int count = _palettes.Count;
            if (count > 0)
            {
                int index = _palettes.IndexOf(_activePalette);
                Palettes.Remove(ActivePalette);
                count = _palettes.Count;
                if (count > 0)
                {
                    if (index == count) index--;
                    ActivePalette = _palettes[index];
                    ProcessState = ActivePalette.IsProcess;
                }
                ImageViewerCounterFormat();
            }
            if (count == 0)
            {
                ProcessState = false;
                DeleteState = false;
            }
            SwitchArrowState();
            GC.Collect();
        }

        private void OnNextImage(object o)
        {
            int count = _palettes.Count,
                index = _palettes.IndexOf(ActivePalette);
            if (count > 1 && index != count - 1)
            {
                ActivePalette = _palettes[index + 1];
                ProcessState = ActivePalette.IsProcess;
            }
            SwitchArrowState();
            ImageViewerCounterFormat();
        }

        private void OnPrevImage(object o)
        {
            int count = _palettes.Count,
                index = _palettes.IndexOf(ActivePalette);
            if(count > 1 && index != 0)
            {
                ActivePalette = _palettes[index - 1];
                ProcessState = ActivePalette.IsProcess;
            }
            SwitchArrowState();
            ImageViewerCounterFormat();
        }

        private void OnSwitchFirstTabImageViewer(object o)
        {
            if (ActivePalette != null)
            {
                ProcessState = ActivePalette.IsProcess;
                ImageViewerTab = 0;
                SwitchArrowState();
                ImageViewerCounterFormat();
            }
        }
        
        #endregion

        #region Methods

        private void SwitchArrowState()
        {
            int count = _palettes.Count,
                index = _palettes.IndexOf(_activePalette);
            if(count > 1)
            {
                LeftArrowState = index == 0 ? false : true;
                RightArrowState = index == count - 1 ? false : true;
            }
            else
            {
                LeftArrowState = false;
                RightArrowState = false;
            }
        }

        private void ImageViewerCounterFormat()
        {
            int count = _palettes.Count,
                index = _palettes.IndexOf(_activePalette) + 1;
            string format = String.Empty;
            if(count == 1)
            {
                format = "1";
            }
            else if (count > 1)
            {
                format = index + "/" + count;
            }
            ImageViewerCounter = format;
        }

        private void SetCombination()
        {
            int length = _activePalette.Cluster.Count;
            List<ColorComb> combList = new List<ColorComb>();
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    ColorComb cc = new ColorComb(
                        _activePalette.Cluster[i].Hex,
                        _activePalette.Cluster[j].Hex,
                        _activePalette.Cluster[i].Brush,
                        _activePalette.Cluster[j].Brush
                    );
                    combList.Add(cc);
                }
            }
            int count = CombinationCount(length, 2);
            _activePalette.Count = count != -1 ? count.ToString() : String.Empty;
            _activePalette.Comb = combList;
        }

        public int Factorial(int n)
        {
            if(n < 0) return 0;
            else if(n == 0) return 1;
            else return n * Factorial(n - 1);
        }

        public int CombinationCount(int n, int k)
        {
            if(k > 0 && k <= n)
            {
                return Factorial(n) / (Factorial(n - k) * Factorial(k));
            }
            return -1;
        }
        
        #endregion
    }
}
