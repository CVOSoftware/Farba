using System.Windows;
using Farba.View;
using Farba.ViewModel;

namespace Farba
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var viewModel = new MainViewModel();
            var view = new MainWindow();
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
