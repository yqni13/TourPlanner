using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BL.Services;

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
                    TourIO.ExportTour(DetailView.DetailSelectedTour, SaveFileDialog());
                }
                catch
                {
                    MessageBox.Show("Something went wrong when exporting a file");
                }
                
            };
            Menu.tourImportEvent += (_, arg) =>
            {
                TourController.ImportTour(OpenFileDialog());
                UpdateTourList();
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
