using System;
using System.Collections.ObjectModel;
using System.Windows;
using TourPlanner.DAL;
using TourPlanner.BL.Search;
using TourPlanner.Models;
using TourPlanner.UI.TourSearch;
using TourPlanner.UI.ViewComponents;
using TourPlanner.UI.ViewModels.AbstractMediator;
using TourPlanner.BL.Services;
using Microsoft.Win32;
using Example.Log4Net.logging;

namespace TourPlanner.UI.ViewModels.TourOverviewMediator
{
    public class MainViewModel : BaseViewModel
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
            this.SearchBar = searchBar;
            this.ResultView = resultView;
            this.DetailView = detailView;
            this.AddTour = addTour;
            this.Menu = menu;

            //subscribe to all the Events from the ViewModels 
            SubscribeToEvents();


            //populate the list of tours in the result view 
            UpdateTourList();
        }

        private void SubscribeToEvents()
        {
            //@todo swap everything to BL
            ResultView.SelectedTourChanged += (_, tour) =>
            {
                DetailView.SelectedTour = tour;                
            };
            ResultView.OpenAddDialogEvent += (_, arg) =>
            {
                OpenAddDialog();
            };
            ResultView.DeleteTourEvent += (_, tour) =>
            {
                TourController.DeleteTour(tour);
                UpdateTourList();
            };
            SearchBar.SearchTextChanged += (_, searchText) =>
            {                
                SearchTours(searchText);
            };
            AddTour.AddedTourEvent += (_, tour) =>
            {
                TourController.AddTour(tour);
                UpdateTourList();
                CloseOpenWindow();                              
            };
            AddTour.CloseAddTourDialogEvent += (_, arg) =>
            {
                CloseOpenWindow();
            };
            Menu.tourExportEvent += (_, arg) =>
            {
                TourIO.ExportTour(DetailView.SelectedTour, )
            };            
        }

        private void SearchTours(string searchText)
        {
            ResultView.UpdateTours(SearchEngine.searchTours(Data, searchText));
        }

        private void OpenAddDialog()
        {
            if(OpenInputWindow == null)
            {
                OpenInputWindow = new AddTourDialog();
                OpenInputWindow.DataContext = this.AddTour;
                OpenInputWindow.Show();
            }                     
        }

        private void CloseOpenWindow()
        {
            OpenInputWindow.Hide();
            OpenInputWindow = null;
        }

        private void UpdateTourList()
        {
            this.Data = TourController.GetTours();
            ResultView.UpdateTours(this.Data);
        }

        private String FileDialog()
        {
            // From https://wpf-tutorial.com/dialogs/the-openfiledialog/
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            else
            {
                logger.Error("Failed to open File");
                return "";
            }            
        }
    }
}
