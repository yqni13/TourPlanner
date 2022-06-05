using System.Collections.ObjectModel;
using System.Windows;
using TourPlanner.BL.Search;
using TourPlanner.Models;
using TourPlanner.BL.Services;
using Microsoft.Win32;
using Example.Log4Net.logging;
using TourPlanner.ViewModels.SubViewModels;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewComponents;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel : BaseViewModel
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();
        public Collection<Tour> Data { get; set; }
           = new Collection<Tour>();
        public Window OpenInputWindow { get; set; } = null;

        //ViewModels
        public SearchBarViewModel SearchBar { get; set; }
        public TourDataResultsViewModel ResultView { get; set; }
        public TourOverviewViewModel DetailView { get; set; }
        public AddTourViewModel AddTour { get; set; }
        public MenuViewModel Menu { get; set; }



        public MainViewModel(SearchBarViewModel searchBar,
                            TourDataResultsViewModel resultView,
                            TourOverviewViewModel detailView,
                            AddTourViewModel addTour,
                            MenuViewModel menu
                            )
        {
            SearchBar = searchBar;
            ResultView = resultView;
            DetailView = detailView;
            AddTour = addTour;
            Menu = menu;


            //subscribe to all the Events from the ViewModels 
            SubscribeToEvents();


            //populate the list of tours in the result view 
            UpdateTourList();
        }

        private void SubscribeToEvents()
        {
            SubToSearchBarEvents();
            SubToResultsViewEvents();
            SubToAddDialogEvents();
            SubToMenuEvents();
        }  

        private void CloseOpenWindow()
        {
            OpenInputWindow.Hide();
            OpenInputWindow = null;
        }        
    }
}
