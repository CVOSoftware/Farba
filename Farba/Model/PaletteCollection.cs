namespace Farba.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Farba.Common;
    using Farba.Common.Enum;

    class PaletteCollection : NotifyPropertyChanged
    {
        #region Fields
        private ColorCombination _combination;

        private ObservableCollection<Palette> _palettes;

        private Palette _activePalette;

        private int _selectTabImagesViewer;
        #endregion

        #region Constructors
        public PaletteCollection()
        {
            _combination = ColorCombination.Tile;
            _palettes = new ObservableCollection<Palette>();
            _selectTabImagesViewer = 0;
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

        public int SelectedTabImagesViewer
        {
            get => _selectTabImagesViewer;
            set
            {
                _selectTabImagesViewer = value;
                OnPropertyChanged("SelectedTabImagesViewer");
            }
        }
        #endregion
    }
}
