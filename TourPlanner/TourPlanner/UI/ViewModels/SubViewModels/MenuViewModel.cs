using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.UI.ViewModels.AbstractMediator;

namespace TourPlanner.UI.ViewModels
{

    public class MenuViewModel : BaseViewModel
    {
        // Define commands and event handlers.
        public ICommand PDFGenerator;
        public ICommand Import;
        public ICommand Export;
        public ICommand QuitApplication;

        public event EventHandler generatePDF;
        public event EventHandler tourImport;
        public event EventHandler tourExport;
        public event EventHandler quitApplication;

        public MenuViewModel()
        {
            PDFGenerator = new RelayCommand((_) =>
            {
                generatePDF?.Invoke(null, EventArgs.Empty);
            });

            Import = new RelayCommand((_) =>
            {
                tourImport?.Invoke(null, EventArgs.Empty);
            });

            Export = new RelayCommand((_) =>
            {
                tourExport?.Invoke(null, EventArgs.Empty);
            });

            QuitApplication = new RelayCommand((_) =>
            {
                quitApplication?.Invoke(null, EventArgs.Empty);
            });
        }
    }
}
