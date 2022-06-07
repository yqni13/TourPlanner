using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BL.PDFGeneration;
using TourPlanner.BL.Services;
using TourPlanner.Models;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel
    {
        private void SubToMenuEvents()
        {
            Menu.tourExportEvent += (_, arg) =>
            {
                try
                {
                    if (DetailView.DetailSelectedTour != null)
                        TourIO.ExportTour(DetailView.DetailSelectedTour, SaveFileDialog());
                    else
                        MessageBox.Show("For activating the export function, please select a tour first.");
                }
                catch (Exception err)
                {
                    MessageBox.Show("Something went wrong when exporting a file.");
                    logger.Error(err.ToString());
                }
                
            };
            Menu.tourImportEvent += (_, arg) =>
            {
                try
                {
                    TourController.ImportTour(OpenFileDialog());
                }
                catch (Exception err)
                {
                    MessageBox.Show("Something went wrong when importing the file.");
                    logger.Error(err.ToString());
                }                
                UpdateTourList();
            };

            Menu.pdfReportEvent += (_, arg) =>
            {
                try
                {
                    if (DetailView.DetailSelectedTour != null)
                    {
                        Collection<TourLogs> logs = LogController.GetSpecificLogs(DetailView.DetailSelectedTour.ID);
                        TourToPDF.GenerateTourReport(DetailView.DetailSelectedTour, logs);
                    }
                    else
                        MessageBox.Show("Please select a tour to get regarding Report.");
                }
                catch (Exception err)
                {
                    MessageBox.Show("We had a problem generating your specific Report. Please contact customer support.");
                    logger.Error(err.ToString());
                }
            };

            Menu.pdfSummaryEvent += (_, tour) =>
            {
                try
                {
                    Collection<Tour> tours = TourController.GetTours();
                    //Collection<TourLogs> logs = LogController.GetAllLogs();                    
                    TourToPDF.GenerateSummarizeReport(tours);
                }
                catch (Exception err)
                {
                    MessageBox.Show("We had a problem generating your Summary. Please contact customer support.");
                    logger.Error(err.ToString());
                }
            };
        }

        private string OpenFileDialog()
        {
            // From https://wpf-tutorial.com/dialogs/the-openfiledialog/
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            else
            {
                logger.Error("Failed to open File");
                return "";
            }
        }
        private string SaveFileDialog()
        {
            // From https://wpf-tutorial.com/dialogs/the-openfiledialog/
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json | *.json";
            if (saveFileDialog.ShowDialog() == true && saveFileDialog.FileName != null)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                logger.Error("Failed to save File");
                return "";
            }
        }
    }
}
