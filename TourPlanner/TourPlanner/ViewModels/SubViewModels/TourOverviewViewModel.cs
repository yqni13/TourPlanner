using Microsoft.Extensions.Configuration;
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

        private string _image;
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
                if (value == null)
                {
                    _selectedTour = value;
                }
                else
                {
                    _selectedTour = value;
                    MessageBox.Show(_selectedTour.From.ToString());
                    From = _selectedTour.From.ToString();
                    To = _selectedTour.To.ToString();                   
                }                
                OnPropertyChanged();
            }
        }

        private String _from;
        public String From {
            get { return _from; }
            set { _from = value; OnPropertyChanged(); }
        }
        private String _to;
        public String To
        {
            get { return _to; }
            set { _to = value; OnPropertyChanged(); }
        }
        private string _defaultPath; 
        


        public TourOverviewViewModel()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json", optional: false, reloadOnChange: true)
                .Build();

            _defaultPath = $"{Environment.CurrentDirectory}/{configuration["images:path"]}/image_placeholder.jpg";
            _image = _defaultPath;
           
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

        public void SetMapPath(String path)
        {
            MessageBox.Show(path);
            if (path == null || path == "")
            {
                Image = _defaultPath;
                return;
            }
            Image = path;
        }
    }
}
