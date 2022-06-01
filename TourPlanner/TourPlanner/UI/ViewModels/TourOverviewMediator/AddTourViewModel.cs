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
        public string TourName { get; set; } = "Test";
        public string TourDescription { get; set; } = "Des...";
        public Adress From { get; set; } = new Adress("bla");
        public Adress To { get; set; } = new Adress("bla");

        public String TransportType { get; set; } = "Bike";

        //Subscribe to this in order to get notivied of event 
        public event EventHandler<string> ToursChangedEvent;

        public ICommand AddTourCommand { get; }        

        public AddTourViewModel()
        {
            //AddTourCommand function raises the ToursChangedEvent event when called            
            AddTourCommand = new RelayCommand((_) =>
            {
                TourAccess.AddTour(new Tour(TourName, TourDescription, From, To));
                MessageBox.Show("test");
                this.ToursChangedEvent?.Invoke(this, TourName);
            });
        }

        public void Debuggaddcomman()
        {
            MessageBox.Show("testAddCommand");
        }

    }
}
