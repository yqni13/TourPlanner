using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels.Utility;

namespace TourPlanner.ViewModels.SubViewModels
{

    public class MenuViewModel : BaseViewModel
    {
        // Define commands and event handlers.
        public ICommand PDFGenerator { get; }
        public ICommand Import { get; }
        public ICommand Export { get; }
        public ICommand QuitApplication { get; }

        public event EventHandler generatePDF;
        public event EventHandler tourImportEvent;
        public event EventHandler tourExportEvent;
        public event EventHandler quitApplication;

        public MenuViewModel()
        {
            PDFGenerator = new RelayCommand((_) =>
            {
                generatePDF?.Invoke(null, EventArgs.Empty);
            });

            Import = new RelayCommand((_) =>
            {
                tourImportEvent?.Invoke(null, EventArgs.Empty);
            });

            Export = new RelayCommand((_) =>
            {
                tourExportEvent?.Invoke(null, EventArgs.Empty);
            });

            QuitApplication = new RelayCommand((_) =>
            {
                quitApplication?.Invoke(null, EventArgs.Empty);
            });
        }
    }
}
