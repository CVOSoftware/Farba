using Farba.Common;

namespace Farba.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            PaletteVM = new PaletteViewModel();
            ConverterVM = new ConverterViewModel();
        }

        #region ViewModelProperties

        public PaletteViewModel PaletteVM { get; }

        public ConverterViewModel ConverterVM { get; }

        #endregion
    }
}
