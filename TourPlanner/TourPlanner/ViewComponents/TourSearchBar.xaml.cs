using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TourPlanner.ViewComponents
{
    /// <summary>
    /// Interaction logic for TourSearchBar.xaml
    /// </summary>
    public partial class TourSearchBar : UserControl
    {
        public TourSearchBar()
        {
            InitializeComponent();
        }

        private void ChangeHint(object sender, TextChangedEventArgs e)
        {
            if (SearchInputBox.Text.Length == 0)
                SearchBoxInputHint.Visibility = Visibility.Visible;
            else
                SearchBoxInputHint.Visibility = Visibility.Hidden;
        }
    }
}
