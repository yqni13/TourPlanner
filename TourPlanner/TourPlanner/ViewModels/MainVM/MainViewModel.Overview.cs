using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Services;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel : BaseViewModel
    {
        private void SubToOverviewViewEvents()
        {
            
            DetailView.ShowMapImageEvent += (_, arg) =>
            {
                //DetailView.SelectedTour = ResultView.SelectedTour;
                
                if(DetailView.SelectedTour != null)
                {
                    DetailView.Image = arg;                   
                }
                UpdateTourList();
            };


        }
    }
}
