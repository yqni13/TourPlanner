using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Services;
using TourPlanner.Models;
using TourPlanner.ViewComponents;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel
    {
        private void SubToAddDialogLogEvents()
        {
            
            AddLog.AddedTourLogEvent += (_, log) =>
            {
                log.TourID = DetailView.DetailSelectedTour.ID;
                LogController.AddTourLog(log);
                UpdateTourLogs(log.TourID);
                CloseOpenWindow();                
            };
            AddLog.CloseAddLogDialogEvent += (_, arg) =>
            {
                CloseOpenWindow();
            };
        }

        private bool _isTourSelected;

        private void OpenAddLogDialog()
        {
            if (_isTourSelected)
            {
                if (OpenInputWindow == null)
                {
                    OpenInputWindow = new AddLogDialog();
                    OpenInputWindow.DataContext = AddLog;
                    OpenInputWindow.Show();
                }

            }
        }
        
        
    }
}
