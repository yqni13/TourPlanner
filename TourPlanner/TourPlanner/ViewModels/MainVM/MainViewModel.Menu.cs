using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Services;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel
    {
        private void SubToMenuEvents()
        {
            Menu.tourExportEvent += (_, arg) =>
            {
                TourIO.ExportTour(DetailView.SelectedTour, FileDialog());
            };
        }
    }
}
