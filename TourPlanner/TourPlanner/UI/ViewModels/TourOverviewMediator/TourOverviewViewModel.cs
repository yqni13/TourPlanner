using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.UI.ViewModels.AbstractMediator;

namespace TourPlanner.UI.ViewModels
{
    public class TourOverviewViewModel : BaseViewModel
    {
        private string _result;
        private string _configexample;

        public TourOverviewViewModel(string path) 
        {
            _configexample = path;
        }

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged();
            }
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
            //Result = _configexample;
        }
    }
}
