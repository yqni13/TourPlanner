using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.UI.ViewModels.AbstractMediator;

namespace TourPlanner.UI.ViewModels.TourOverviewMediator
{
    public class SearchBarViewModel : BaseViewModel
    {
        //Subscribe to this in order to get notivied of event 
        public event EventHandler<string> SearchTextChanged;

        public ICommand SearchCommand { get; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public SearchBarViewModel()
        {
            //SearchCommand function raises the SearchTextChanged event when called            
            SearchCommand = new SearchCommand((_) =>
            {
                this.SearchTextChanged?.Invoke(this, SearchText);
            });
        }
    }
}
