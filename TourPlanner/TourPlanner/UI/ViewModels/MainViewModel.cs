using System;
using System.Collections.ObjectModel;
using System.Windows;
using TourPlanner.DAL;
using TourPlanner.Models;
using TourPlanner.UI.TourSearch;
using TourPlanner.UI.ViewComponents;
using TourPlanner.UI.ViewModels.AbstractMediator;


namespace TourPlanner.UI.ViewModels.TourOverviewMediator
{
    public class MainViewModel : BaseViewModel
    {
        private readonly TourOverviewViewModel resultView;
        private readonly ISearch searchEngine;        

        public Collection<Tour> Data { get; set; }
           = new Collection<Tour>();

        public Window OpenInputWindow { get; set; }

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
            this.Data = TourAccess.getTours();
            resultView.UpdateTours(this.Data);            
        }

        private void SubscribeToEvents()
        {
            ResultView.SelectedTourChanged += (_, tour) =>
            {
                DetailView.SelectedTour = tour;
                if (tour == null)
                {
                    MessageBox.Show("no tour selected");
                }
                else
                {
                    MessageBox.Show("New SelectedTour");
                }
                
            };
            ResultView.OpenAddDialogEvent += (_, arg) =>
            {
                OpenAddDialog();
            };
            ResultView.DeleteTourEvent += (_, tour) =>
            {
                MessageBox.Show("Delete Tour with UUID " + tour.ID.ToString());
                TourAccess.DeleteTour(tour.ID);
                this.Data = TourAccess.getTours();
                ResultView.UpdateTours(this.Data);
            };
            SearchBar.SearchTextChanged += (_, searchText) =>
            {
                MessageBox.Show("SeachTextChanged called");
                SearchTours(searchText);
            };
            AddTour.AddedTourEvent += (_, tour) =>
            {                
                
                TourAccess.AddTour(tour);
                this.Data = TourAccess.getTours();                
                ResultView.UpdateTours(this.Data);
                CloseOpenWindow();
                              
            };
            AddTour.CloseAddTourDialogEvent += (_, arg) =>
            {
                CloseOpenWindow();
            };



            
        }

        private void SearchTours(string searchText)
        {
            //var results = String.Join("\n", this.searchEngine.TourSearch(searchText));
            //this.resultView.DisplayTourDataOverview(results);
        }

        private void OpenAddDialog()
        {
            OpenInputWindow = new AddTourDialog();
            OpenInputWindow.DataContext = this.AddTour;
            OpenInputWindow.Show();            
        }

        private void CloseOpenWindow()
        {
            OpenInputWindow.Hide();   
        }
    }
}
