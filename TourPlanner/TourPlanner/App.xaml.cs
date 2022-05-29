using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.UI.TourSearch;
using TourPlanner.UI.ViewModels;
using TourPlanner.UI.ViewModels.TourOverviewMediator;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnExecution(object sender, StartupEventArgs e)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json.config", optional: false, reloadOnChange: true)
                .Build();

            var searchBar = new SearchBarViewModel();
            var searchData = new SearchTourData();
            //var tourOverview = new TourOverviewViewModel();
            var tourOverview = new TourOverviewViewModel();

            var window = new MainWindow
            {
                DataContext = new MainViewModel(searchBar, tourOverview, searchData),
                TourSearchBar = { DataContext = searchBar },
                TourDataResults = { DataContext = tourOverview }
            };

            window.Show();
        }
    }
}
