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

namespace TourPlanner.ViewModels
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
                TourIO.ExportTour(DetailView.SelectedTour, FileDialog());
            };
        }

        private void SearchTours(string searchText)
        {
            ResultView.UpdateTours(SearchEngine.searchTours(Data, searchText));
        }

        private void OpenAddDialog()
        {
            if (OpenInputWindow == null)
            {
                OpenInputWindow = new AddTourDialog();
                OpenInputWindow.DataContext = AddTour;
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
            Data = TourController.GetTours();
            ResultView.UpdateTours(Data);
        }

        private string FileDialog()
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
