using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BL.Services;
using TourPlanner.ViewComponents;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel
    {
        private void SubToAddDialogEvents()
        {
            AddTour.AddedTourEvent += (_, tour) =>
            {
                try
                {
                    TourController.AddTour(tour);
                    UpdateTourList();
                    CloseOpenWindow();
                }
                catch (Exception)
                {
                    MessageBox.Show("We were unable to find this route. Please check your input.");
                }
            };
            AddTour.CloseAddTourDialogEvent += (_, arg) =>
            {
                CloseOpenWindow();
            };

        }

        private void OpenAddDialog()
        {
            if (OpenInputWindow == null)
            {
                OpenInputWindow = new AddTourDialog();
                OpenInputWindow.DataContext = AddTour;
                OpenInputWindow.Show();
            }
        }
    }
}
