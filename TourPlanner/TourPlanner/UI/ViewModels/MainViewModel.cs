using System;
using System.Windows;
using TourPlanner.UI.TourSearch;
using TourPlanner.UI.ViewModels.AbstractMediator;

namespace TourPlanner.UI.ViewModels.TourOverviewMediator
{
    public class MainViewModel : BaseViewModel
    {
        private readonly TourOverviewViewModel _resultView;
        private readonly ISearch _searchEngine;
        public MenuViewModel MenuView { get; set; }

        public MainViewModel(MenuViewModel menu, SearchBarViewModel searchBar, TourOverviewViewModel resultView, ISearch searchEngine)
        {

            searchBar.SearchTextChanged += (_, searchText) =>
            {
                SearchTours(searchText);                
            };
            this._resultView = resultView;
            this._searchEngine = searchEngine;
            MenuView = menu;

            MenuSetUp();
        }

        private void SearchTours(string searchText)
        {
            var results = String.Join("\n", this._searchEngine.TourSearch(searchText));
            this._resultView.DisplayTourDataOverview(results);
        }

        private void MenuSetUp()
        {
            QuitApplicationOption();
        }

        private void QuitApplicationOption()
        {
            MenuView.quitApplication += (sender, e) =>
            {
                try
                {
                    Environment.Exit(0);                   
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            };
        }
    }
}
