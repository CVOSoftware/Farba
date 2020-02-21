using Farba.ViewModel.Base;

namespace Farba.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            PaletteGeneratorVM = new PaletteGeneratorViewModel();
            PaletteListVM = new PaletteListViewModel();
        }

        #region ViewModelProperties

        public PaletteGeneratorViewModel PaletteGeneratorVM { get; }

        public PaletteListViewModel PaletteListVM { get; }

        #endregion
    }
}
