using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.UI.ViewModels.TourOverviewMediator
{
    public class AddTourViewModel
    {
        public Tour newTour { get; set; } = new Tour();     
        /*
        public string TourName { get; set; } = "Test";
        public string TourDescription { get; set; } = "Des...";
        public Adress From { get; set; } = new Adress("bla");
        public Adress To { get; set; } = new Adress("bla");

        public String TransportType { get; set; } = "Bike";
        */

        //Subscribe to this in order to get notivied of event 
        public event EventHandler<Tour> AddedTourEvent;
        public event EventHandler CloseAddTourDialogEvent;

        public ICommand AddTourCommand { get; }
        public ICommand CloseDialogCommand { get; }

        public AddTourViewModel()
        {
            newTour.Name = "testTour";
            newTour.Description = "Desc....";
            newTour.From.Street = "Straße";
            newTour.From.Number = "31/5";
            newTour.From.City = "Wien";
            newTour.From.ZibCode = 1200;
            newTour.From.Country = "AT";
            newTour.To.Street = "Straße";
            newTour.To.Number = "31/5";
            newTour.To.City = "Wien";
            newTour.To.ZibCode = 1200;
            newTour.To.Country = "AT";
            //AddTourCommand function raises the ToursChangedEvent event when called            
            AddTourCommand = new RelayCommand((_) =>
            {                
                this.AddedTourEvent?.Invoke(this, newTour);
                this.newTour = new Tour();
            });
            CloseDialogCommand = new RelayCommand((_) =>
            {
                this.CloseAddTourDialogEvent?.Invoke(this, EventArgs.Empty);
            });
        }

        public void Debuggaddcomman()
        {
            MessageBox.Show("testAddCommand");
        }

    }
}
