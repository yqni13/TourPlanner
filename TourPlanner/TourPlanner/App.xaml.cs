using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.ViewModels;
using TourPlanner.ViewModels.MainVM;
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
        public TourLogViewModel TourLogViewModel { get; set; }    
        public AddLogViewModel AddLogViewModel { get; set; }

        private void App_OnExecution(object sender, StartupEventArgs e)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json", optional: false, reloadOnChange: true)
                .Build();

            Directory.CreateDirectory(configuration["images:path"]);

            SearchBarViewModel = new SearchBarViewModel();
            TourDataResultsViewModel = new TourDataResultsViewModel();
            TourOverviewViewModel = new TourOverviewViewModel();
            AddTourViewModel = new AddTourViewModel();
            MenuViewModel = new MenuViewModel();
            TourLogViewModel = new TourLogViewModel();
            AddLogViewModel = new AddLogViewModel();

            var window = new MainWindow
            {
                DataContext = new MainViewModel(SearchBarViewModel, TourDataResultsViewModel,TourOverviewViewModel, AddTourViewModel, MenuViewModel, TourLogViewModel, AddLogViewModel),
                TourSearchBar = { DataContext = SearchBarViewModel },
                TourDataResults = { DataContext = TourDataResultsViewModel },
                TourDataDetails = {DataContext = TourOverviewViewModel},
                TourMenu = { DataContext = MenuViewModel },
                TourDataLogs = { DataContext = TourLogViewModel }
            };

            window.Show();
        }
    }
}
