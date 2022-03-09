using System;
using System.Windows.Input;
using WpfSubViewExample.ViewModels.Abstract;

namespace WpfSubViewExample.ViewModels
{
    public class SearchBarViewModel : BaseViewModel
    {
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
            SearchCommand = new SearchCommand((_) =>
            {
                this.SearchTextChanged?.Invoke(this, SearchText);
            });
        }
    }
}
