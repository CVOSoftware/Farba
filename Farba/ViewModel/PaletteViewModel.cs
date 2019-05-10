namespace Farba.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Win32;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using Farba.Common;
    using Farba.Common.Enum;
    using Farba.Model;
    using ImageCluster;
    
    class PaletteViewModel : NotifyPropertyChanged
    {
        #region Fields
        private ColorCombination _combination;
        private ObservableCollection<Palette> _palettes;
        private Palette _activePalette;
        private int _imageViewerTab;
        private bool _processState;
        private bool _deleteState;
        private bool _leftArrowState;
        private bool _rightArrowState;
        private string _imageViewerConter;
        #endregion

        #region Commands fields
        private Command _select;
        private Command _process;
        private Command _delete;
        private Command _setup;
        private Command _next;
        private Command _prev;
        private Command _setTile;
        private Command _setList;
        #endregion

        #region Constructors
        public PaletteViewModel()
        {
            _combination = ColorCombination.Tile;
            _palettes = new ObservableCollection<Palette>();
            _activePalette = null;
            _imageViewerTab = 0;
            _processState = false;
            _deleteState = false;
            _leftArrowState = false;
            _rightArrowState = false;
            _imageViewerConter = String.Empty;
            _select = new Command(SelectImage);
            _process = new Command(StartProcess);
            _delete = new Command(DeleteImage);
            _setup = new Command(SetFirstTabImageViewer);
            _next = new Command(NextImage);
            _prev = new Command(PrevImage);
            _setTile = new Command(SwitchTile);
            _setList = new Command(SwitchList);
        }
        #endregion

        #region Properties
        public ColorCombination Combination
        {
            get => _combination;
            set
            {
                _combination = value;
                OnPropertyChanged("Combination");
            }
        }

        public ObservableCollection<Palette> Palettes
        {
            get => _palettes;
            set
            {
                _palettes = value;
                OnPropertyChanged("Palettes");
            }
        }

        public Palette ActivePalette
        {
            get => _activePalette;
            set
            {
                _activePalette = value;
                OnPropertyChanged("ActivePalette");
            }
        }

        public int ImageViewerTab
        {
            get => _imageViewerTab;
            set
            {
                _imageViewerTab= value;
                OnPropertyChanged("ImageViewerTab");
            }
        }
        
        public bool ProcessState
        {
            get => _processState;
            set
            {
                _processState = value;
                OnPropertyChanged("ProcessState");
            }
        }

        public bool DeleteState
        {
            get => _deleteState;
            set
            {
                _deleteState = value;
                OnPropertyChanged("DeleteState");
            }
        }
        
        public bool LeftArrowState
        {
            get => _leftArrowState;
            set
            {
                _leftArrowState = value;
                OnPropertyChanged("LeftArrowState");
            }
        }

        public bool RightArrowState
        {
            get => _rightArrowState;
            set
            {
                _rightArrowState = value;
                OnPropertyChanged("RightArrowState");
            }
        }
        
        public string ImageViewerCounter
        {
            get => _imageViewerConter;
            set
            {
                _imageViewerConter = value;
                OnPropertyChanged("ImageViewerCounter");
            }
        }
        #endregion

        #region Commands properties
        public Command Select => _select;
        public Command Process => _process;
        public Command Delete => _delete;
        public Command Setup => _setup;
        public Command Next => _next;
        public Command Prev => _prev;
        public Command SetTile => _setTile;
        public Command SetList => _setList;
        #endregion

        #region Command methods
        private void SelectImage(object o)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog() == true)
            {
                bool match = true;
                string fileName = openFileDialog.FileName;
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
        
        private void StartProcess(object o)
        {
            List<ClusterColor> clusterColor = Handler.RandomColor();
            _activePalette.Cluster = clusterColor;
            ActivePalette.IsProcess = false;
            ProcessState = ActivePalette.IsProcess;
        }

        private void DeleteImage(object o)
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
        }

        private void NextImage(object o)
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

        private void PrevImage(object o)
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

        private void SetFirstTabImageViewer(object o)
        {
            if (ActivePalette != null)
            {
                ProcessState = ActivePalette.IsProcess;
                ImageViewerTab = 0;
                SwitchArrowState();
                ImageViewerCounterFormat();
            }
        }
        
        private void SwitchTile(object o)
        {
            if (Combination == ColorCombination.List)
            {
                Combination = ColorCombination.Tile;
            }
        }

        private void SwitchList(object o)
        {
            if (Combination == ColorCombination.Tile)
            {
                Combination = ColorCombination.List;
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
        #endregion
    }
}
