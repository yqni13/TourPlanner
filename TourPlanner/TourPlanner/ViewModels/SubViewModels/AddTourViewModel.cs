using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.DAL;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Utility;

namespace TourPlanner.ViewModels.SubViewModels
{
    public class AddTourViewModel : BaseViewModel
    {
        private Tour _newTour = new Tour();
        public Tour newTour {
            get { return _newTour; }
            set { _newTour = value; OnPropertyChanged(); } 
        }        
              
        public List<String> transporttypes { get; set; } = new List<String>{ "fastest", "shortest", "pedestrian", "bicycle" };
        public List<String> maptypes { get; set; } = new List<String> { "dark", "light", "map", "hyb", "sat"};

        //Subscribe to this in order to get notivied of event 
        public event EventHandler<Tour> AddedTourEvent;
        public event EventHandler CloseAddTourDialogEvent;

        public ICommand AddTourCommand { get; }
        public ICommand CloseDialogCommand { get; }

        public AddTourViewModel()
        {
            /*newTour.Name = "testTour";
            newTour.Description = "Desc....";
            newTour.From.Street = "Kaschlgasse";
            newTour.From.Number = "1";
            newTour.From.City = "Wien";
            newTour.From.ZibCode = 1200;
            newTour.From.Country = "AT";
            newTour.To.Street = "Gonzagagasse";
            newTour.To.Number = "1";
            newTour.To.City = "Wien";
            newTour.To.ZibCode = 1010;
            newTour.To.Country = "AT";*/
            //AddTourCommand function raises the ToursChangedEvent event when called            
            AddTourCommand = new RelayCommand((_) =>
            {
                if (ValidateInput())
                {
                    this.AddedTourEvent?.Invoke(this, newTour);
                    this.newTour = new Tour();
                }                
            });
            CloseDialogCommand = new RelayCommand((_) =>
            {
                this.CloseAddTourDialogEvent?.Invoke(this, EventArgs.Empty);
            });
        }
        public bool ValidateInput()
        {
            if (newTour.Name =="" ||
                newTour.Description == "" ||
                newTour.From.Street == "" ||
                newTour.From.Number == "" ||
                newTour.From.ZibCode <= 0 ||
                newTour.To.Street == "" ||
                newTour.To.Number == "" ||
                newTour.To.ZibCode <= 0                
                )
            {
                MessageBox.Show("The fields marked with a * can not be empty");
                return false;
            }
            return true;            
        }

    }
}
