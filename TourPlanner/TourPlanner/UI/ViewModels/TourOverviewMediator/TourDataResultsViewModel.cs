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

        public ObservableCollection<Tour> Data { get; set; }
           = new ObservableCollection<Tour>();

        public ICommand OpenAddDialogCommand { get; }        

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                this.SelectedTourChanged?.Invoke(this, SelectedTour);
                MessageBox.Show(SelectedTour.Name);
                OnPropertyChanged();
            }
        }
        public TourDataResultsViewModel()
        {                   
            OpenAddDialogCommand = new RelayCommand((_) =>
            {
                Trace.WriteLine("Add");                
                /*
                AddTourDialog addTourDialog = new AddTourDialog();
                addTourDialog.Show();*/
            });
        }

        public void UpdateTours(Collection<Tour> tours)
        {            
            this.Data = new ObservableCollection<Tour>(tours);
        }

    }
}
