using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewComponents;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Utility;

namespace TourPlanner.ViewModels.SubViewModels
{
    public class TourOverviewViewModel : BaseViewModel
    {
        public event EventHandler<Tour> ShowTourDataEvent;
        public event EventHandler<String> ShowMapImageEvent;
        public event EventHandler<Tour> SaveChangesEvent;
        public event EventHandler<Tour> CancelChangesEvent;

        public ICommand SaveChangesCommand { get; }
        public ICommand CancelChangesCommand { get; }
        public ICommand OpenAddDialogCommand { get; set; }    
        public ICommand ShowTourDataCommand { get; set; }
        public ICommand ShowMapImageCommand { get; set; }
                
        private string _image = $"{Environment.CurrentDirectory}/Images/image_placeholder.jpg";
        public String Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                From = _selectedTour.From.ToString();
                To = _selectedTour.To.ToString();
                OnPropertyChanged();
            }
        }

        public String From { get; set; }
        public String To { get; set; }


        public TourOverviewViewModel()
        {
            ShowTourDataCommand = new RelayCommand((_) =>
            {
                this.ShowTourDataEvent?.Invoke(this, SelectedTour);
            });
            ShowMapImageCommand = new RelayCommand((_) =>
            {
                this.ShowMapImageEvent?.Invoke(this, Image);
            });
            SaveChangesCommand = new RelayCommand((_) =>
            {
                this.SaveChangesEvent?.Invoke(this, SelectedTour);
            });
            CancelChangesCommand = new RelayCommand((_) =>
            {
                this.CancelChangesEvent?.Invoke(this, SelectedTour);
            });
        }

    }
}
