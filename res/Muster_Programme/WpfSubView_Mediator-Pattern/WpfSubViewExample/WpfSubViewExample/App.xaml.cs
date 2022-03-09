using System.Windows;
using WpfSubViewExample.SearchEngine;
using WpfSubViewExample.ViewModels;

namespace WpfSubViewExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var searchBarViewModel = new SearchBarViewModel();
            var searchEngine = new StandardSearchEngine();
            var resultViewModel = new ResultViewModel();

            var wnd = new MainWindow
            {
                DataContext = new MainViewModel(searchBarViewModel, resultViewModel, searchEngine),
                SearchBar = { DataContext = searchBarViewModel },
                ResultView = { DataContext = resultViewModel }
            };

            wnd.Show();
        }
    }
}
