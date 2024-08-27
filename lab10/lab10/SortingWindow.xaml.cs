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
using System.Windows.Shapes;

namespace lab10
{
    /// <summary>
    /// Logika interakcji dla klasy SortingWindow.xaml
    /// </summary>
    public partial class SortingWindow : Window
    {

        public bool ascendingOrder { get; set; }
        public int sortingType {  get; set; }

        public SortingWindow()
        {
            InitializeComponent();
        }

        private void sort_Click(object sender, RoutedEventArgs e)
        {
            if (ascendingOrderBut.IsChecked == true)
                ascendingOrder = true;
            else
                ascendingOrder = false;

            if (yearBut.IsChecked == true)
                sortingType = 1; //sorting by year
            else if (modelBut.IsChecked == true)
                sortingType = 2; //sorting by model
            else if (motorBut.IsChecked == true)
                sortingType = 3; //sorting by motor
            else
                sortingType = 1;

            DialogResult = true;
            Close();
        }
    }
}
