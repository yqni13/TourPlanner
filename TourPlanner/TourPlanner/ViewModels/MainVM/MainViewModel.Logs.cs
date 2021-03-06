using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BL.Services;
using TourPlanner.Models;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel
    {
        private void SubToLogsViewEvent()
        {
            LogsView.SelectTourLogsEvent += (_, logs) =>
            {
                // TODO
            };
            LogsView.OpenAddDialogEvent += (_, arg) =>
            {
                OpenAddLogDialog();
            };
            LogsView.DeleteTourLogsEvent += (_, logs) =>
            {
                if (LogsView.SelectedTourLog != null)
                {
                    LogController.DeleteTourLog(logs);
                    LogsView.SelectedTourLog = new TourLogs();
                    UpdateTourLogs(logs.TourID);
                }
                else
                    MessageBox.Show("No log selected!");
            };
        }

        private void UpdateTourLogs(Guid tourFK)
        {
            LogData = LogController.GetSpecificLogs(tourFK);
            LogsView.UpdateLogs(LogData);
        }
    }
}
