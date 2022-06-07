using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BL.Services;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel
    {
        private void SubToOverviewViewEvents()
        {
            
            DetailView.ShowMapImageEvent += (_, arg) =>
            {
                //DetailView.SelectedTour = ResultView.SelectedTour;
                
                if(DetailView.DetailSelectedTour != null)
                {
                    DetailView.Image = arg;                   
                }
                UpdateTourList();
            };
            DetailView.SaveChangesEvent += (_, tour) =>
            {
                //DetailView.SelectedTour = ResultView.SelectedTour;
                if(tour == null)
                {
                    MessageBox.Show("No tour selected!");
                }
                else
                {
                    TourController.UpdateTour(tour);
                }                            
                UpdateTourList();
            };
            DetailView.CancelChangesEvent += (_, arg) =>
            {                
                UpdateTourList();
            };


        }
    }
}
