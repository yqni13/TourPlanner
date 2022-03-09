using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.UI.ViewModels.AbstractMediator;

namespace TourPlanner.UI.ViewModels
{
    internal class TourOverviewViewModel : BaseViewModel
    {
        private string _result;

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }            
        }

        public void DisplayTourDataOverview(string result)
        {
            Result = result;
        }
    }
}
