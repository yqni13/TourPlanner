﻿using Microsoft.Extensions.Configuration;
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
        public SearchBarViewModel SearchBarViewModel { get; set; }
        public TourDataResultsViewModel TourDataResultsViewModel { get; set; }
        public TourOverviewViewModel TourOverviewViewModel { get; set; }
        public AddTourViewModel AddTourViewModel { get; set; }

        private void App_OnExecution(object sender, StartupEventArgs e)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json.config", optional: false, reloadOnChange: true)
                .Build();

            
            var searchData = new SearchTourData();

            SearchBarViewModel = new SearchBarViewModel();
            TourDataResultsViewModel = new TourDataResultsViewModel();
            TourOverviewViewModel = new TourOverviewViewModel();
            AddTourViewModel = new AddTourViewModel();

            var window = new MainWindow
            {
                DataContext = new MainViewModel(SearchBarViewModel, TourDataResultsViewModel,TourOverviewViewModel, AddTourViewModel),
                TourSearchBar = { DataContext = SearchBarViewModel },
                TourDataResults = { DataContext = TourDataResultsViewModel },
                TourDataDetails = {DataContext = TourOverviewViewModel}
            };

            window.Show();
        }
    }
}
