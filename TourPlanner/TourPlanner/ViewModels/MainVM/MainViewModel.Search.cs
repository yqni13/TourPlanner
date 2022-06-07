using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Search;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel
    {
        private void SubToSearchBarEvents()
        {
            SearchBar.SearchTextChanged += (_, searchText) =>
            {
                SearchTours(searchText);
            };
        }
        private void SearchTours(string searchText)
        {
            ResultView.UpdateTours(SearchEngine.searchTours(Data, searchText));
        }
    }
}
