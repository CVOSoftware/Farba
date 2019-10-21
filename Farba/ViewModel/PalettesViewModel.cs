using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Farba.ViewModel.Base;
using Farba.Common.Helpers;
using Farba.ViewModel;
using Farba.Common.Clusters;

namespace Farba.ViewModel
{ 
    class PalettesViewModel : BaseViewModel
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

        private ObservableCollection<PaletteViewModel> palettes;

        private PaletteViewModel activePalette;

        private int imageViewerTab;
        
        private string imageViewerConter;

        private List<ColorCombViewModel> combination;

        #endregion

        #region Constructor

        public PalettesViewModel()
        {
            palettes = new ObservableCollection<PaletteViewModel>();
            activePalette = null;
            imageViewerTab = 0;
            imageViewerConter = string.Empty;
            combination = null;
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

        public ObservableCollection<PaletteViewModel> Palettes
        {
            get => palettes;
            set => SetValue(ref palettes, value);
        }

        public PaletteViewModel ActivePalette
        {
            get => activePalette;
            set => SetValue(ref activePalette, value);
        }

        public List<ColorCombViewModel> Combination
        {
            get => combination;
            set => SetValue(ref combination, value);
        }
        
        public int ImageViewerTab
        {
            get => imageViewerTab;
            set => SetValue(ref imageViewerTab, value);
        }
        
        public string ImageViewerCounter
        {
            get => imageViewerConter;
            set => SetValue(ref imageViewerConter, value);
        }
        
        #endregion

        #region CommandExecuteMethods

        private void OnSelectImage(object parameter)
        {
            var fileName = DialogWindowHelper.FileDialog(FileFilter.Images);
            if(fileName != string.Empty 
               && IsCreatePalette(fileName, palettes))
            {
                var uri = new Uri(fileName);
                var image = new BitmapImage(uri);
                var palette = new PaletteViewModel(fileName, image);
                Palettes.Add(palette);
                ActivePalette = palette;
                ImageViewerCounter = GetCurrentImageCountStringFormat(palette, palettes);
            }
        }
        
        private void OnCreatePalette(object parameter)
        {
            List<ClusterColor> clusterColor = Handler.RandomColor(5);
            activePalette.Cluster = clusterColor;
            activePalette.IsProcess = false;
            SetCombination();
        }

        private void OmRemovePalette(object parameter)
        {
            int count = palettes.Count;
            if (count > 0)
            {
                int index = palettes.IndexOf(activePalette);
                Palettes.Remove(activePalette);
                count = palettes.Count;
                if (count > 0)
                {
                    if (index == count) index--;
                    ActivePalette = palettes[index];
                }
                ImageViewerCounter = GetCurrentImageCountStringFormat(activePalette, palettes);
            }
        }

        private void OnNextImage(object parameter)
        {
            int count = palettes.Count,
                index = palettes.IndexOf(activePalette);
            if (count > 1 && index != count - 1)
            {
                ActivePalette = palettes[index + 1];
            }
            ImageViewerCounter = GetCurrentImageCountStringFormat(activePalette, palettes);
        }

        private void OnPrevImage(object parameter)
        {
            int count = palettes.Count,
                index = palettes.IndexOf(activePalette);
            if(count > 1 && index != 0)
            {
                ActivePalette = palettes[index - 1];
            }
            ImageViewerCounter = GetCurrentImageCountStringFormat(activePalette, palettes);
        }

        private void OnSwitchFirstTabImageViewer(object parameter)
        {
            if (activePalette != null)
            {
                ImageViewerTab = 0;
                ImageViewerCounter = GetCurrentImageCountStringFormat(activePalette, palettes);
            }
        }

        #endregion

        #region CommandCanExecuteMethods

        private bool CanCreatePalette(object parameter)
        {
            return activePalette != null
                   && activePalette.IsProcess == true; 
        }

        private bool CanRemovePalette(object parameter)
        {
            return palettes.Count > 0;
        }

        private bool CanPrevImage(object parameter)
        {
            return palettes.Count > 1
                   && palettes[0] != activePalette;
        }

        private bool CanNextImage(object parameter)
        {
            var palettesCount = palettes.Count;

            return palettesCount > 1
                   && palettes[palettesCount - 1] != activePalette;
        }

        #endregion

        #region ViewModelHelperMethods

        private void SetCombination()
        {
            int length = activePalette.Cluster.Count;
            List<ColorCombViewModel> combList = new List<ColorCombViewModel>();
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    ColorCombViewModel cc = new ColorCombViewModel(
                        activePalette.Cluster[i].Hex,
                        activePalette.Cluster[j].Hex,
                        activePalette.Cluster[i].Brush,
                        activePalette.Cluster[j].Brush
                    );
                    combList.Add(cc);
                }
            }
            int count = CombinationCount(length, 2);
            activePalette.ColorCombination = count != -1 ? count.ToString() : String.Empty;
            activePalette.Comb = combList;
        }

        public int Factorial(int n)
        {
            if(n < 0) return 0;
            else if(n == 0) return 1;
            else return n * Factorial(n - 1);
        }

        public int CombinationCount(int n, int k)
        {
            return k > 0 && k <= n ? Factorial(n) / (Factorial(n - k) * Factorial(k)) : -1;
        }

        #endregion

        #region ViewModelStaticHelperMethods

        private static bool IsCreatePalette(string fileName, ObservableCollection<PaletteViewModel> palettes)
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

        private static string GetCurrentImageCountStringFormat(PaletteViewModel current, ObservableCollection<PaletteViewModel> palettes)
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
