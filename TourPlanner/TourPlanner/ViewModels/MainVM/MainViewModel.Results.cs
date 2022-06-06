using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                DetailView.SetSelectedTour(tour);                
                if (tour != null)
                {
                    _isTourSelected = true;
                    LogsView.TourLogCollection = LogController.GetSpecificLogs(tour.ID);
                    DetailView.SetMapPath(tour.MapPath);
                    DetailView.ChildFriendly = DetailView.SetChildFriendliness(tour);
                    //DetailView.SetChildFriendliness(tour);
                    DetailView.Popularity = GeneralController.CalculatePopularity(tour);
                    DetailView.TourWeather = TourController.GetWeather(tour);                    
                }
                else
                {
                    _isTourSelected = false;
                    LogsView.TourLogCollection = null;
                    DetailView.SetMapPath(null);
                    DetailView.TourWeather = null;
                }                
                
            };
            ResultView.OpenAddDialogEvent += (_, arg) =>
            {
                OpenAddDialog();
            };
            ResultView.DeleteTourEvent += (_, tour) =>
            {
                TourController.DeleteTour(tour);
                DetailView.SetSelectedTour(new Tour());
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
