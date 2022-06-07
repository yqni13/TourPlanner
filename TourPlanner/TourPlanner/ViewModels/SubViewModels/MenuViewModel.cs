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

    public class MenuViewModel : BaseViewModel
    {
        // Define commands and event handlers.
        public ICommand PDFReportCommand { get; }
        public ICommand PDFSummaryCommand { get; }
        public ICommand Import { get; }
        public ICommand Export { get; }

        public event EventHandler pdfReportEvent;
        public event EventHandler<Tour> pdfSummaryEvent;
        public event EventHandler tourImportEvent;
        public event EventHandler tourExportEvent;

        public MenuViewModel()
        {
            PDFReportCommand = new RelayCommand((_) =>
            {
                pdfReportEvent?.Invoke(null, EventArgs.Empty);
            });

            PDFSummaryCommand = new RelayCommand((_) =>
            {
                // Will get Collection<TourLogs> via Controller
                pdfSummaryEvent?.Invoke(this, PDFSelectedTour);
            });

            Import = new RelayCommand((_) =>
            {
                tourImportEvent?.Invoke(null, EventArgs.Empty);
            });

            Export = new RelayCommand((_) =>
            {
                tourExportEvent?.Invoke(null, EventArgs.Empty);
            });
        }        

        private Tour _pdfSelectedTour;
        public Tour PDFSelectedTour
        {
            get => _pdfSelectedTour;
            set { _pdfSelectedTour = value; OnPropertyChanged(); }
        }
    }
}
