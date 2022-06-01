﻿using System;
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

namespace TourPlanner.UI.ViewComponents
{
    /// <summary>
    /// Interaction logic for TourDataResults.xaml
    /// </summary>
    public partial class TourDataResults : UserControl
    {
        public TourDataResults()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddTourDialog();            
            window.Show();
        }
    }
}
