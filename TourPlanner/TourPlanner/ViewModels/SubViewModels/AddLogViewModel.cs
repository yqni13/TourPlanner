using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BL.Services;
using TourPlanner.Models;
using TourPlanner.Models.Enums;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Utility;

namespace TourPlanner.ViewModels.SubViewModels
{
    public class AddLogViewModel : BaseViewModel
    {
        public event EventHandler<TourLogs> AddedTourLogEvent;
        public event EventHandler CloseAddLogDialogEvent;

        public ICommand AddLogCommand { get; }
        public ICommand CloseDialogCommand { get; }

        public List<String> DifficultyDropdown { get; set; } = new List<String>
        {
            "Warmup", "Easy", "Moderate", "Hard", "Expert"
        };

        public List<String> RatingDropdown { get; set; } = new List<String>
        {
            "Worst", "Bad", "Weak", "Improveable", "Moderate", "Advancement", "Good",
            "Excellent", "Satisfying", "Perfect"
        };
        
        public string Difficulty { get; set; }
        public string Rating { get; set; }

        private TourLogs _newLog = new();
        public TourLogs NewLog
        {
            get { return _newLog; }
            set
            {
                _newLog = value;
                //_newLog.Timestamp = TimeOfLogCreation;
                OnPropertyChanged();
            }
        }

        public AddLogViewModel()
        {

            AddLogCommand = new RelayCommand((_) =>
            {
                //NewLog.Timestamp = TimeOfLogCreation;
                NewLog.Difficulty = LogController.GetETourDifficultyEnumeration(Difficulty);
                NewLog.Rating = LogController.GetETourRatingEnumeration(Rating);
                
                if (ValidateInput())
                {
                    this.AddedTourLogEvent?.Invoke(this, NewLog);
                    this.NewLog = new TourLogs();
                }
            });
            CloseDialogCommand = new RelayCommand((_) =>
            {
                this.CloseAddLogDialogEvent?.Invoke(this, EventArgs.Empty);
            });
        }

        public bool ValidateInput()
        {
            if (NewLog.TourID.ToString() == "" ||                
                NewLog.Comment == "" ||
                NewLog.Difficulty.ToString() == "" ||
                NewLog.TotalTime.ToString() == "" ||
                NewLog.Rating.ToString() == ""                
                )
            {
                MessageBox.Show("The fields marked with a * can not be empty");
                return false;
            }
            return true;
        }

        private DateTime _timeOfLogCreation;
        public DateTime TimeOfLogCreation
        {
            get { return _timeOfLogCreation; }
            set
            {                
                _timeOfLogCreation = DateTime.Now;
            }
        }
    }
}
