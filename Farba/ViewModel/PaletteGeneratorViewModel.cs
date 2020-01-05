using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Farba.ViewModel.Base;
using Farba.Helper;
using Farba.Common.Clusters;
using Farba.Extension;

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

        private bool isNotSelectActivePalette;

        private int imageViewerTab;
        
        private string imageViewerConter;

        private PaletteViewModel activePalette;

        private List<ColorCombinationViewModel> combination;

        private ObservableCollection<PaletteViewModel> palettes;

        #endregion

        #region Constructor

        public PaletteGeneratorViewModel()
        {
            palettes = new ObservableCollection<PaletteViewModel>();
            activePalette = null;
            isNotSelectActivePalette = false;
            imageViewerTab = 0;
            imageViewerConter = string.Empty;
            combination = null;
        }

        #endregion

        #region CommandProperties

        public ICommand SelectImageCommand => RelayCommand.Register(ref selectImageCommand, OnSelectImage);

        public ICommand CreatePaletteCommand => RelayCommand.Register(ref createPaletteCommand, OnCreatePalette, CanCreatePalette);

        public ICommand RemovePaletteCommand => RelayCommand.Register(ref removePaletteCommand, OnRemovePalette, CanRemovePalette);

        public ICommand PrevImageCommand => RelayCommand.Register(ref prevImageCommand, OnPrevImage, CanPrevImage);

        public ICommand NextImageCommand => RelayCommand.Register(ref nextImageCommand, OnNextImage, CanNextImage);

        public ICommand SwitchFirstTabImageViewerCommand => RelayCommand.Register(ref switchFirstTabImageViewerCommand, OnSwitchFirstTabImageViewer);

        #endregion

        #region ViewModelProperties

        public bool IsNotSelectActivePalette
        {
            get => isNotSelectActivePalette;
            set => SetValue(ref isNotSelectActivePalette, value);
        }

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

        public List<ColorCombinationViewModel> Combination
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

                if (IsNotSelectActivePalette == false)
                {
                    IsNotSelectActivePalette = true;
                }
            }
        }
        
        private async void OnCreatePalette(object parameter)
        {
            activePalette.StartProcess();
            await Task.Run(ClusteringProcess);
        }

        private void OnRemovePalette(object parameter)
        {
            var count = palettes.Count;
            if (count > 0)
            {
                var index = palettes.IndexOf(activePalette);
                Palettes.Remove(activePalette);
                count = palettes.Count;
                if (count > 0)
                {
                    if (index == count) index--;
                    ActivePalette = palettes[index];
                }
                ImageViewerCounter = GetCurrentImageCountStringFormat();
            }
            
            if(count == 0)
            {
                IsNotSelectActivePalette = false;
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
            return palettes.Count > 0
                   && ActivePalette.IsProcess;
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

        #region Private

        private void ClusteringProcess()
        {
            using (var kmeans = new Kmeans(activePalette.Image, activePalette.Id))
            {
                var clusters = kmeans.GetClusters();

                UIHelper.UpdateUI(() =>
                {
                    PostClusteringProcess(clusters, kmeans.Id);
                    RelayCommand.RaiseCanExecuteChanged();
                });
            }
        }

        private void PostClusteringProcess(List<ClusterColor> clusters, Guid id)
        {
            foreach (var cluster in clusters)
            {
                cluster.Hex = cluster.Color.HexFormat();
                cluster.Rgb = cluster.Color.RgbFormat();
                cluster.Brush = cluster.Color.GetBrash();
            }

            if (activePalette.Id == id)
            {
                activePalette.Cluster = clusters;
                SetCombination(activePalette);
            }
            else
            {
                foreach (var palette in palettes)
                {
                    if (palette.Id == id)
                    {
                        palette.Cluster = clusters;
                        SetCombination(palette);
                    }
                }
            }
        }

        private void SetCombination(PaletteViewModel palette)
        {
            var combinationList = palette.IsSort ? DirectCombinationProcess(palette) 
                                                 : BackCombinationProcess(palette);

            palette.CombinationCount = combinationList.Count.ToString();
            palette.ColorCombinationList = combinationList;
            palette.PaletteListIsEmpty = true;
            palette.IsProcess = true;
        }

        private bool IsAddPalette(string fileName)
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

        private string GetCurrentImageCountStringFormat()
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

        private List<ColorCombinationViewModel> DirectCombinationProcess(PaletteViewModel palette)
        {
            var combinationList = new List<ColorCombinationViewModel>();

            for (var i = 0; i < palette.Cluster.Count - 1; i++)
            {
                for (var j = i + 1; j < palette.Cluster.Count; j++)
                {
                    var cc = new ColorCombinationViewModel(palette.Cluster[i], palette.Cluster[j], palette.ColorSpaceType);
                    combinationList.Add(cc);
                }
            }

            return combinationList;
        }

        private List<ColorCombinationViewModel> BackCombinationProcess(PaletteViewModel palette)
        {
            var combinationList = new List<ColorCombinationViewModel>();

            for (var i = palette.Cluster.Count - 1; i >= 0; i--)
            {
                for (var j = i - 1; j >= 0; j--)
                {
                    var cc = new ColorCombinationViewModel(palette.Cluster[j], palette.Cluster[i], palette.ColorSpaceType);
                    combinationList.Add(cc);
                }
            }

            return combinationList;
        }

        #endregion
    }
}
