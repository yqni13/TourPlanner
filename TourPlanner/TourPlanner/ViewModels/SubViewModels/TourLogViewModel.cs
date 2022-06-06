using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Utility;

namespace TourPlanner.ViewModels.SubViewModels
{
    public class TourLogViewModel : BaseViewModel
    {
        public event EventHandler OpenAddDialogEvent;
        public event EventHandler<TourLogs> DeleteTourLogsEvent;
        public event EventHandler<TourLogs> SelectTourLogsEvent;
        public event EventHandler<TourLogs> ModifyTourLogsEvent;

        public ICommand OpenAddDialogCommand { get; }
        public ICommand DeleteTourLogsCommand { get; }
        public ICommand ModifyTourLogsCommand { get; }

        private List<TourLogs> _tourLogCollection;
        public List<TourLogs> TourLogCollection
        {
            get { return _tourLogCollection; }
            set
            {
                _tourLogCollection = value;
                OnPropertyChanged();
            }
        }

        private Collection<TourLogs> _logData;
        public Collection<TourLogs> LogData
        {
            get => _logData;
            set
            {
                _logData = value;
                OnPropertyChanged();
            }
        }

        public TourLogViewModel()
        {
            OpenAddDialogCommand = new RelayCommand((_) =>
            {
                // (this, EventArgs.Empty) => 2nd parameter if Eventhandler (line 19) is empty or gives specific datatype
                this.OpenAddDialogEvent?.Invoke(this, EventArgs.Empty);
            });
            DeleteTourLogsCommand = new RelayCommand((_) =>
            {
                this.DeleteTourLogsEvent?.Invoke(this, SelectedTourLog);
            });
            ModifyTourLogsCommand = new RelayCommand((_) =>
            {
                this.ModifyTourLogsEvent?.Invoke(this, SelectedTourLog);
            });
        }

        private TourLogs _selectedTourLog;
        public TourLogs SelectedTourLog
        {
            get => _selectedTourLog;
            set
            {
                if(_selectedTourLog != value)
                {
                    _selectedTourLog = value;
                    this.SelectTourLogsEvent?.Invoke(this, SelectedTourLog);
                    OnPropertyChanged();
                    // TODO: further methods?
                }
            }
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

        public void UpdateLogs(Collection<TourLogs> logs)
        {
            this.LogData = logs;
        }
    }
}
