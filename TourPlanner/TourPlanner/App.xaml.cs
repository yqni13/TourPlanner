using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.ViewModels;
using TourPlanner.ViewModels.SubViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public SearchBarViewModel SearchBarViewModel { get; set; }
        public TourDataResultsViewModel TourDataResultsViewModel { get; set; }
        public TourOverviewViewModel TourOverviewViewModel { get; set; }
        public AddTourViewModel AddTourViewModel { get; set; }
        public MenuViewModel MenuViewModel {get;set;}

        private void App_OnExecution(object sender, StartupEventArgs e)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json", optional: false, reloadOnChange: true)
                .Build();

            SearchBarViewModel = new SearchBarViewModel();
            TourDataResultsViewModel = new TourDataResultsViewModel();
            TourOverviewViewModel = new TourOverviewViewModel();
            AddTourViewModel = new AddTourViewModel();
            MenuViewModel = new MenuViewModel();

            var window = new MainWindow
            {
                DataContext = new MainViewModel(SearchBarViewModel, TourDataResultsViewModel,TourOverviewViewModel, AddTourViewModel, MenuViewModel),
                TourSearchBar = { DataContext = SearchBarViewModel },
                TourDataResults = { DataContext = TourDataResultsViewModel },
                TourDataDetails = {DataContext = TourOverviewViewModel},
                TourMenu = { DataContext = MenuViewModel }
            };

            window.Show();
        }
    }
}
