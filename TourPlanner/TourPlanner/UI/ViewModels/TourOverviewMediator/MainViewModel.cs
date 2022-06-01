using System;
using System.Collections.ObjectModel;
using System.Windows;
using TourPlanner.DAL;
using TourPlanner.Models;
using TourPlanner.UI.TourSearch;
using TourPlanner.UI.ViewModels.AbstractMediator;


namespace TourPlanner.UI.ViewModels.TourOverviewMediator
{
    public class MainViewModel : BaseViewModel
    {
        private readonly TourOverviewViewModel resultView;
        private readonly ISearch searchEngine;        

        public Collection<Tour> Data { get; }
           = new Collection<Tour>();

        public MainViewModel(SearchBarViewModel searchBar,
                            TourDataResultsViewModel resultView,
                            TourOverviewViewModel detailView,                             
                            AddTourViewModel addTour,
                            MenuViewModel menu
                            )
        {

            searchBar.SearchTextChanged += (_, searchText) =>
            {
                MessageBox.Show("SeachTextChanged called");
                SearchTours(searchText);                
            };
            //this.resultView = resultView;
            //this.searchEngine = searchEngine;

            addTour.ToursChangedEvent += (_, newTourName) =>
            {
                MessageBox.Show("ToursChangedEvent called");
                resultView.UpdateTours(TourAccess.getTours());
            };
            resultView.SelectedTourChanged += (_, tour) =>
            {
                detailView.SelectedTour = tour;
                MessageBox.Show("New SelectedTour");
            };
            resultView.UpdateTours(TourAccess.getTours());
            
        }

        private void SearchTours(string searchText)
        {
            //var results = String.Join("\n", this.searchEngine.TourSearch(searchText));
            //this.resultView.DisplayTourDataOverview(results);
        }
    }
}
