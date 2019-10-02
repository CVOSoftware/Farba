using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
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

        private RelayCommand removePaletteCommand;

        private RelayCommand switchFirstTabImageViewerCommand;

        private RelayCommand prevImageCommand;

        private RelayCommand nextImageCommand;

        #endregion

        #region ViewModelFields

        private ObservableCollection<Palette> _palettes;

        private Palette _activePalette;

        private int _imageViewerTab;
        
        private string _imageViewerConter;

        private List<ColorComb> _combination;

        #endregion

        #region Constructor

        public PaletteViewModel()
        {
            _palettes = new ObservableCollection<Palette>();
            _activePalette = null;
            _imageViewerTab = 0;
            _imageViewerConter = string.Empty;
            _combination = null;
        }

        #endregion

        #region CommandProperties

        public ICommand SelectImageCommand => RelayCommand.Register(ref selectImageCommand, OnSelectImage);

        public ICommand CreatePaletteCommand => RelayCommand.Register(ref createPaletteCommand, OnCreatePalette, CanCreatePalette);

        public ICommand RemovePaletteCommand => RelayCommand.Register(ref removePaletteCommand, OmRemovePalette, CanRemovePalette);

        public ICommand PrevImageCommand => RelayCommand.Register(ref prevImageCommand, OnPrevImage, CanPrevImage);

        public ICommand NextImageCommand => RelayCommand.Register(ref nextImageCommand, OnNextImage, CanNextImage);

        public ICommand SwitchFirstTabImageViewerCommand => RelayCommand.Register(ref switchFirstTabImageViewerCommand, OnSwitchFirstTabImageViewer);

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

        private void OnSelectImage(object parameter)
        {
            var fileName = DialogWindowHelper.FileDialog(FileFilter.Images);
            if(fileName != string.Empty 
               && IsCreatePalette(fileName, _palettes))
            {
                var uri = new Uri(fileName);
                var image = new BitmapImage(uri);
                var palette = new Palette(fileName, image);
                Palettes.Add(palette);
                ActivePalette = palette;
                ImageViewerCounter = GetCurrentImageCountStringFormat(palette, _palettes);
            }
        }
        
        private void OnCreatePalette(object parameter)
        {
            List<ClusterColor> clusterColor = Handler.RandomColor(5);
            _activePalette.Cluster = clusterColor;
            ActivePalette.IsProcess = false;
            SetCombination();
        }

        private void OmRemovePalette(object parameter)
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
                }
                ImageViewerCounter = GetCurrentImageCountStringFormat(_activePalette, _palettes);
            }
        }

        private void OnNextImage(object parameter)
        {
            int count = _palettes.Count,
                index = _palettes.IndexOf(ActivePalette);
            if (count > 1 && index != count - 1)
            {
                ActivePalette = _palettes[index + 1];
            }
            ImageViewerCounter = GetCurrentImageCountStringFormat(_activePalette, _palettes);
        }

        private void OnPrevImage(object parameter)
        {
            int count = _palettes.Count,
                index = _palettes.IndexOf(ActivePalette);
            if(count > 1 && index != 0)
            {
                ActivePalette = _palettes[index - 1];
            }
            ImageViewerCounter = GetCurrentImageCountStringFormat(_activePalette, _palettes);
        }

        private void OnSwitchFirstTabImageViewer(object parameter)
        {
            if (ActivePalette != null)
            {
                ImageViewerTab = 0;
                ImageViewerCounter = GetCurrentImageCountStringFormat(_activePalette, _palettes);
            }
        }

        #endregion

        #region CommandCanExecuteMethods

        private bool CanCreatePalette(object parameter)
        {
            return _activePalette != null
                   && _activePalette.IsProcess == true; 
        }

        private bool CanRemovePalette(object parameter)
        {
            return _palettes.Count > 0;
        }

        private bool CanPrevImage(object parameter)
        {
            return _palettes.Count > 1
                   && _palettes[0] != _activePalette;
        }

        private bool CanNextImage(object parameter)
        {
            var palettesCount = _palettes.Count;

            return palettesCount > 1
                   && _palettes[palettesCount - 1] != _activePalette;
        }

        #endregion

        #region ViewModelHelperMethods

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

        #region ViewModelStaticHelperMethods

        private static bool IsCreatePalette(string fileName, ObservableCollection<Palette> palettes)
        {
            var isFile = true;
            var palettesCount = palettes.Count;
            if (palettesCount > 0)
            {
                for (int i = 0; i < palettesCount; i++)
                {
                    if (palettes[i].FileName == fileName)
                    {
                        isFile = false;
                        break;
                    }
                }
            }
            return isFile;
        }

        private static string GetCurrentImageCountStringFormat(Palette current, ObservableCollection<Palette> palettes)
        {
            var count = palettes.Count;
            
            if(count == 1)
            {
                return $"{count}";
            }
            else if(count > 1)
            {
                var index = palettes.IndexOf(current) + 1;
                return $"{index}/{count}";
            }

            return string.Empty;
        }

        #endregion
    }
}
