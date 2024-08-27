using Microsoft.VisualBasic;
using System;
using System.Buffers;
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
    /// Logika interakcji dla klasy AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public Car newCar {  get; set; }
        public AddWindow()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string model = engineField.Text;

            if (double.TryParse(hpField.Text, out double hp))
            {
                if (double.TryParse(dispField.Text, out double disp))
                {
                    Engine newEngine = new Engine(disp, hp, model);

                    string carModel = modelField.Text;

                    if (int.TryParse(yearField.Text, out int year))
                    {
                        this.newCar = new Car(carModel, newEngine, year);
                        MessageBox.Show("New car added!");
                        DialogResult = true;
                        Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Error: wrong input for year.");
                    }
                }
                else
                {
                    MessageBox.Show("Error: wrong input for displacement.");
                }
            }
            else
            {
                MessageBox.Show("Error: wrong input for horsepower.");
            }


            DialogResult = false;
            Close();
        }
    }
}
