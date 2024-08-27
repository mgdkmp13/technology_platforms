using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
    /// Logika interakcji dla klasy searchingWindow.xaml
    /// </summary>
    public partial class searchingWindow : Window
    {
        public searchingWindow()
        {
            InitializeComponent();
        }

        public string searchingValue { get; private set; }



        private void search_Click(object sender, RoutedEventArgs e)
        {
            searchingValue = searchValue.Text;

            DialogResult = true;
            Close();
        }
    }
}
