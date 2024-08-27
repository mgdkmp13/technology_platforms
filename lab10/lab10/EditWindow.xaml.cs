using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Logika interakcji dla klasy EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public Car editCar {  get; set; }
        public EditWindow(Car car)
        {
            editCar = car;
            InitializeComponent();
        }


        private void edit_Click(object sender, RoutedEventArgs e)
        {
            string model = engineField.Text;

            if (double.TryParse(hpField.Text, out double hp))
            {
                editCar.Motor.HorsePower = hp;
            }

            if (double.TryParse(dispField.Text, out double disp))
            {
                editCar.Motor.Displacement = disp;
            }
            string carModel = modelField.Text;
            if (!string.IsNullOrEmpty(carModel))
            {
                editCar.Model = carModel;
            }
            if (int.TryParse(yearField.Text, out int year))
            {
                this.editCar.Year = year;
            }

            DialogResult = true;
            Close();
        }
    }
}
