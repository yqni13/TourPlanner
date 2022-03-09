using System;
using WpfSubViewExample.SearchEngine.Abstract;
using WpfSubViewExample.ViewModels.Abstract;

namespace WpfSubViewExample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ResultViewModel resultView;
        private readonly ISearchEngine searchEngine;

        public MainViewModel(SearchBarViewModel searchBar, ResultViewModel resultView, ISearchEngine searchEngine)
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
            var results = String.Join("\n", this.searchEngine.SearchFor(searchText));
            this.resultView.DisplayResults(results);
        }
    }
}
