using Farba.ViewModel.Base;

namespace Farba.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            PaletteVM = new PalettesViewModel();
            ConverterVM = new ConverterViewModel();
        }

        #region ViewModelProperties

        public PalettesViewModel PaletteVM { get; }

        public ConverterViewModel ConverterVM { get; }

        #endregion
    }
}
