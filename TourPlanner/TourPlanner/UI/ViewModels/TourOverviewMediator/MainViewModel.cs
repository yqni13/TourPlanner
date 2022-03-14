using System;
using TourPlanner.UI.TourSearch;
using TourPlanner.UI.ViewModels.AbstractMediator;

namespace TourPlanner.UI.ViewModels.TourOverviewMediator
{
    public class MainViewModel : BaseViewModel
    {
        private readonly TourOverviewViewModel resultView;
        private readonly ISearch searchEngine;

        public MainViewModel(SearchBarViewModel searchBar, TourOverviewViewModel resultView, ISearch searchEngine)
        {

            searchBar.SearchTextChanged += (_, searchText) =>
            {
                SearchTours(searchText);
            };
            this.resultView = resultView;
            this.searchEngine = searchEngine;
        }

        private void SearchTours(string searchText)
        {
            var results = String.Join("\n", this.searchEngine.TourSearch(searchText));
            this.resultView.DisplayTourDataOverview(results);
        }
    }
}
