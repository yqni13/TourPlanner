using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

        private TourLogs _newLog = new(DateTime.Now);
        public TourLogs NewLog
        {
            get { return _newLog; }
            set { _newLog = value; OnPropertyChanged(); }
        }

        public AddLogViewModel()
        {

            AddLogCommand = new RelayCommand((_) =>
            {
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

        
    }
}
