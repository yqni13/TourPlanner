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
using TourPlanner.BL.Services;
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

        public ICommand SaveChangesCommand { get; set; }
        public ICommand CancelChangesCommand { get; set; }
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

        private Tour _detailselectedTour;
        public Tour DetailSelectedTour
        {
            get => _detailselectedTour;
            set
            {
                if (value == null)
                {
                    _detailselectedTour = value;
                    From = "";
                    To = "";
                    ChildFriendly = "";
                    PopularityProperty = 0;
                }
                else
                {
                    _detailselectedTour = value;                    
                    From = _detailselectedTour.From.ToString();
                    To = _detailselectedTour.To.ToString();
                    ChildFriendly = SetChildFriendliness(_detailselectedTour).ToString();
                    PopularityProperty = GeneralController.CalculatePopularity(_detailselectedTour);
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

        private string _childFriendly;
        public string ChildFriendly
        {
            get => _childFriendly;
            set { _childFriendly = value; OnPropertyChanged(); }
        }

        private int _popularityProperty;
        public int PopularityProperty
        {
            get => _popularityProperty;
            set { _popularityProperty = value; OnPropertyChanged(); }
        }

        private Weather _tourweather;
        public Weather TourWeather {
            get { return _tourweather; }
            set { _tourweather = value; OnPropertyChanged(); } 
        }

        public TourOverviewViewModel()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json", optional: false, reloadOnChange: true)
                .Build();

            _defaultPath = $"{Environment.CurrentDirectory}/{configuration["images:path"]}/image_placeholder.jpg";
            _image = _defaultPath;
           
            ShowTourDataCommand = new RelayCommand((_) =>
            {
                this.ShowTourDataEvent?.Invoke(this, DetailSelectedTour);
            });
            ShowMapImageCommand = new RelayCommand((_) =>
            {
                this.ShowMapImageEvent?.Invoke(this, Image);
            });
            SaveChangesCommand = new RelayCommand((_) =>
            {
                this.SaveChangesEvent?.Invoke(this, DetailSelectedTour);
            });
            CancelChangesCommand = new RelayCommand((_) =>
            {
                this.CancelChangesEvent?.Invoke(this, DetailSelectedTour);
            });
        }

        public void SetMapPath(String path)
        {            
            if (path == null || path == "")
            {
                Image = _defaultPath;
                return;
            }
            Image = path;
        }

        public void SetSelectedTour(Tour tour)
        {
            this.DetailSelectedTour = tour;                  
        }
        
        public int Popularity { get; set; }

        
        public string SetChildFriendliness(Tour tour)
        {
            if (GeneralController.CalculateChildFriendly(tour))
                return "true";
            else
                return "false";
        }
    }
}
