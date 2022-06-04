using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.UI.ViewComponents;
using TourPlanner.UI.ViewModels.AbstractMediator;

namespace TourPlanner.UI.ViewModels.TourOverviewMediator
{
    public class TourDataResultsViewModel : BaseViewModel
    {
        //Subscribe to this in order to get notivied of event 
        public event EventHandler OpenAddDialogEvent;
        public event EventHandler<Tour> SelectedTourChanged;
        public event EventHandler<Tour> DeleteTourEvent;

        public ICommand OpenAddDialogCommand { get; }
        public ICommand DeleteTourCommand { get; }

        private Collection<Tour> _data;
        public Collection<Tour> Data
        {
            get => _data;
            set
            {
                _data = value;
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
                this.SelectedTourChanged?.Invoke(this, SelectedTour);
                OnPropertyChanged();
            }
        }
        public TourDataResultsViewModel()
        {                   
            OpenAddDialogCommand = new RelayCommand((_) =>
            {                
                this.OpenAddDialogEvent?.Invoke(this, EventArgs.Empty);
            });
            DeleteTourCommand = new RelayCommand((_) =>
            {
                this.DeleteTourEvent?.Invoke(this, SelectedTour);
            });
        }

        public void UpdateTours(Collection<Tour> tours)
        {            
            this.Data = tours;                 
            OnPropertyChanged();
        }

    }
}
