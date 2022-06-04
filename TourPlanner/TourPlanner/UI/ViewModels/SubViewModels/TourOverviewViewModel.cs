using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.UI.ViewComponents;
using TourPlanner.UI.ViewModels.AbstractMediator;

namespace TourPlanner.UI.ViewModels.SubViewModels
{
    public class TourOverviewViewModel : BaseViewModel
    {
        private string _result;        

        public ICommand OpenAddDialogCommand { get; }        

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;  
                OnPropertyChanged();
            }
        }

        public TourOverviewViewModel()
        {
        }

        public bool IsResultEmpty()
        {
            if (string.IsNullOrEmpty(Result))
                return true;
            // Purpose of method serves only for Unit Test at the moment.
            return false;
        }

        public void DisplayTourDataOverview(string resultText)
        {
            Result = resultText;            
        }
    }
}
