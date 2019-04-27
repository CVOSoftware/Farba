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
    
    class PaletteViewModel
    {
        #region Fields
        private PaletteCollection _paletteModel;
        private Command _select;
        private Command _process;
        private Command _delete;
        private Command _setup;
        private Command _setTile;
        private Command _setList;
        #endregion

        #region Constructors
        public PaletteViewModel()
        {
            _paletteModel = new PaletteCollection();
            _select = new Command(SelectAndAdd);
            _process = new Command(StartProcess);
            _delete = new Command(DeletePalette);
            _setup = new Command(SetupSelectImage);
            _setTile = new Command(SwitchTile);
            _setList = new Command(SwitchList);
        }
        #endregion

        #region Properties
        public PaletteCollection PaletteModel => _paletteModel;
        
        public Command Select => _select;

        public Command Process => _process;

        public Command Delete => _delete;

        public Command Setup => _setup;

        public Command SetTile => _setTile;

        public Command SetList => _setList;
        #endregion

        #region CommandMethods
        private void SelectAndAdd(object o)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog() == true)
            {
                bool match = true;
                string fileName = openFileDialog.FileName;
                int count = _paletteModel.Palettes.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (_paletteModel.Palettes[i].FileName == fileName)
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
                    _paletteModel.Palettes.Add(palette);
                    _paletteModel.ActivePalette = palette;
                }
            }
        }
        
        private void StartProcess(object o)
        {
            MessageBox.Show("StartProcess");
        }

        private void DeletePalette(object o)
        {
            int count = _paletteModel.Palettes.Count;
            if (count > 0)
            {
                _paletteModel.Palettes.Remove(_paletteModel.ActivePalette);
                count = _paletteModel.Palettes.Count;
                if (count > 0)
                {
                    _paletteModel.ActivePalette = _paletteModel.Palettes[count - 1];
                }
            }
            else
            {
                _paletteModel.Palettes.Clear();
            }
        }

        private void SetupSelectImage(object o)
        {
            _paletteModel.SelectedTabImagesViewer = 0;
        }

        private void SwitchTile(object o)
        {
            if(_paletteModel.Combination == ColorCombination.List)
            {
                _paletteModel.Combination = ColorCombination.Tile;
            }
        }

        private void SwitchList(object o)
        {
            if (_paletteModel.Combination == ColorCombination.Tile)
            {
                _paletteModel.Combination = ColorCombination.List;
            }
        }
        #endregion
    }
}
