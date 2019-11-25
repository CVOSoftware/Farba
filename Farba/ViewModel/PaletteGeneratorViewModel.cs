using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Farba.ViewModel.Base;
using Farba.Helper;
using Farba.Common.Clusters;

namespace Farba.ViewModel
{ 
    class PaletteGeneratorViewModel : BaseViewModel
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

        public PaletteGeneratorViewModel()
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
               && IsAddPalette(fileName))
            {
                var uri = new Uri(fileName);
                var image = new BitmapImage(uri);
                var palette = new PaletteViewModel(fileName, image);
                Palettes.Add(palette);
                ActivePalette = palette;
                ImageViewerCounter = GetCurrentImageCountStringFormat();
            }
        }
        
        private void OnCreatePalette(object parameter)
        {
            using (var kmeans = new Kmeans(activePalette.Image))
            {
                activePalette.Cluster = kmeans.GetClusters();
                activePalette.IsProcess = false;
                SetCombination();
            }
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
                ImageViewerCounter = GetCurrentImageCountStringFormat();
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
            ImageViewerCounter = GetCurrentImageCountStringFormat();
        }

        private void OnPrevImage(object parameter)
        {
            int count = palettes.Count,
                index = palettes.IndexOf(activePalette);
            if(count > 1 && index != 0)
            {
                ActivePalette = palettes[index - 1];
            }
            ImageViewerCounter = GetCurrentImageCountStringFormat();
        }

        private void OnSwitchFirstTabImageViewer(object parameter)
        {
            if (activePalette != null)
            {
                ImageViewerTab = 0;
                ImageViewerCounter = GetCurrentImageCountStringFormat();
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

        #region Protected

        protected void SetCombination()
        {
            var length = activePalette.Cluster.Count;
            var combList = new List<ColorCombViewModel>();

            for (var i = 0; i < length - 1; i++)
            {
                for (var j = i + 1; j < length; j++)
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

            var count = MathHelper.CombinationCount(length, 2);

            activePalette.ColorCombination = count != -1 ? count.ToString() : String.Empty;
            activePalette.Comb = combList;
        }

        protected bool IsAddPalette(string fileName)
        {
            var palettesCount = palettes.Count;

            if (palettesCount == 0)
            {
                return true;
            }

            for (var i = 0; i < palettesCount; i++)
            {
                if (palettes[i].FileName != fileName)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        protected string GetCurrentImageCountStringFormat()
        {
            var palettesCount = palettes.Count;

            switch (palettesCount)
            {
                case 0:
                    return string.Empty;
                case 1:
                    return palettesCount.ToString();
                default:
                {
                    var current = palettes.IndexOf(activePalette) + 1;

                    return $"{current}/{palettesCount}";
                }
            }
        }

        #endregion
    }
}
