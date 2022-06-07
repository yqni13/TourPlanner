using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public event EventHandler<TourLogs> SaveTourLogsChangeEvent;

        public ICommand OpenAddDialogCommand { get; }
        public ICommand DeleteTourLogsCommand { get; }
        public ICommand SaveTourLogsChangeCommand { get; }

        private Collection<TourLogs> _tourLogCollection;
        public Collection<TourLogs> TourLogCollection
        {
            get { return _tourLogCollection; }
            set
            {
                _tourLogCollection = value;                
                OnPropertyChanged();
            }
        }        

        public TourLogViewModel()
        {
            OpenAddDialogCommand = new RelayCommand((_) =>
            {                
                this.OpenAddDialogEvent?.Invoke(this, EventArgs.Empty);
            });
            DeleteTourLogsCommand = new RelayCommand((_) =>
            {
                this.DeleteTourLogsEvent?.Invoke(this, SelectedTourLog);
            });
            SaveTourLogsChangeCommand = new RelayCommand((_) =>
            {
                this.SaveTourLogsChangeEvent?.Invoke(this, SelectedTourLog);
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
                }
            }
        }

        public void UpdateLogs(Collection<TourLogs> logs)
        {
            this.TourLogCollection = logs;
        }
    }
}
