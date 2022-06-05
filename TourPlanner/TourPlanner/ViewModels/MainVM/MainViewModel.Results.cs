using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Services;
using TourPlanner.Models;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel
    {

        private void SubToResultsViewEvents()
        {
            ResultView.SelectedTourChanged += (_, tour) =>
            {
                DetailView.SelectedTour = tour;                
                if (tour != null)
                {
                    LogsView.TourLogCollection = tour.TourLogs;
                    DetailView.SetMapPath(tour.MapPath);
                }
                else
                {
                    LogsView.TourLogCollection = null;
                    DetailView.SetMapPath(null);
                }                
                
            };
            ResultView.OpenAddDialogEvent += (_, arg) =>
            {
                OpenAddDialog();
            };
            ResultView.DeleteTourEvent += (_, tour) =>
            {
                TourController.DeleteTour(tour);
                DetailView.SelectedTour = new Tour();
                DetailView.SetMapPath(null);
                UpdateTourList();
            };
        }

        private void UpdateTourList()
        {
            Data = TourController.GetTours();
            ResultView.UpdateTours(Data);
        }        
    }
}
