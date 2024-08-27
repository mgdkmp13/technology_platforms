using System.Buffers.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Car> myCars;
        BindingList<Car> myCarsBindingList { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            this.myCars = new List<Car>()
            {
                new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
            };

            myCarsBindingList = new BindingList<Car>(myCars);
            dataGridView1.ItemsSource = myCarsBindingList;

        }





        private void queries_Click(object sender, RoutedEventArgs e)
        {
            exercisesBox.Text = "";
            var elements = from car in myCars
                           where car.Model == "A6"
                           group car by car.Motor.Model == "TDI" ? "diesel" : "petrol" into grouped
                           select new
                           {
                               engineType = grouped.Key,
                               avgHPPL = grouped.Average(car => car.Motor.HorsePower / car.Motor.Displacement)
                           } into result
                           orderby result.avgHPPL descending
                           select result;

            exercisesBox.Text = "Expression Query Syntax results:\n";
            foreach (var e1 in elements)
            {
                exercisesBox.AppendText($"{e1.engineType} : {e1.avgHPPL}\n");
            }

            var elements2 = myCars
                .Where(car => car.Model == "A6")
                .GroupBy(car => car.Motor.Model == "TDI" ? "diesel" : "petrol")
                .Select(g => new
                {
                    engineType = g.Key,
                    avgHPPL = g.Average(car => car.Motor.HorsePower / car.Motor.Displacement)
                })
                .OrderByDescending(result => result.avgHPPL);

            exercisesBox.Text += "\nMethod-Based Query Syntax results:\n";
            foreach (var e2 in elements2)
            {
                exercisesBox.AppendText($"{e2.engineType} : {e2.avgHPPL}\n");
            }
        }

        private void ex2_Click(object sender, RoutedEventArgs e)
        {
            Comparison<Car> arg1 = delegate (Car car1, Car car2)
            {
                return car2.Motor.HorsePower.CompareTo(car1.Motor.HorsePower);
            };
            Predicate<Car> arg2 = delegate (Car car)
            {
                if (car.Motor.Model == "TDI")
                {
                    return true;
                }
                return false;
            };
            Action<Car> arg3 = delegate (Car car)
            {
                MessageBox.Show(car.ToString());
            };

            myCars.Sort(new Comparison<Car>(arg1));
            myCars.FindAll(arg2).ForEach(arg3);
        }

        private void ex3_Click(object sender, RoutedEventArgs e)
        {
            var sortingWindow = new SortingWindow();
            bool? correct = sortingWindow.ShowDialog();
            if (correct == true)
            {
                exercisesBox.Clear();
                exercisesBox.AppendText("MyCars sorted ");

                int sortingType = sortingWindow.sortingType;
                ListSortDirection sortingOrder;
                if (sortingWindow.ascendingOrder)
                {
                    sortingOrder = ListSortDirection.Ascending;
                    exercisesBox.AppendText("ascending\n");
                }
                else
                {
                    sortingOrder = ListSortDirection.Descending;
                    exercisesBox.AppendText("descending\n");

                }
                SortableBindingList<Car> myCarsSorted = new SortableBindingList<Car>(myCars);

                
                if (sortingType == 1)
                {
                    myCarsSorted.SortBy("Year", sortingOrder);
                    exercisesBox.AppendText($"By: Year\n\n");
                }
                else if (sortingType == 2)
                {
                    myCarsSorted.SortBy("Model", sortingOrder);
                    exercisesBox.AppendText($"By: Model\n\n");
                }
                else if (sortingType == 3)
                {
                    myCarsSorted.SortBy("Motor", sortingOrder);
                    exercisesBox.AppendText($"By: Motor\n\n");
                }

                foreach (var car in myCarsSorted)
                {
                    exercisesBox.AppendText(car.ToString() + "\n");
                }


            }
        }

        private void ex3_b_Click(object sender, RoutedEventArgs e)
        {
            var searchWindow = new searchingWindow();
            bool? correct = searchWindow.ShowDialog();
            if (correct == true)
            {
                chooseCars(searchWindow);
            }
        }
        private void ex3_clear_Click(object sender, RoutedEventArgs e)
        {
            dataGridView1.ItemsSource = new BindingList<Car>(myCars);
        }

        private void chooseCars(searchingWindow searchingWindow)
        {
            string keyword = searchingWindow.searchingValue;
            var chosenCars = myCars.Where(car =>
                 string.IsNullOrEmpty(keyword) ||
                car.Model.ToLower().Contains(keyword.ToLower()) ||
                car.Motor.Model.ToLower().Contains(keyword.ToLower()) ||
                car.Year.ToString().Equals(keyword) ||
                car.Motor.Displacement.ToString().Equals(keyword) ||
                car.Motor.HorsePower.ToString().Equals(keyword))
                .ToList();

            dataGridView1.ItemsSource = new BindingList<Car>(chosenCars);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView1.SelectedItem != null)
            {
                Car selectedCar = dataGridView1.SelectedItem as Car;
                if (selectedCar != null)
                {
                    (dataGridView1.ItemsSource as BindingList<Car>).Remove(selectedCar);
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            bool? correct = addWindow.ShowDialog();
            if (correct == true)
            {
                addCar(addWindow);
            }
        }

        private void addCar(AddWindow addWindow)
        {
            Car car = addWindow.newCar;
            if (car!= null)
            {
                (dataGridView1.ItemsSource as BindingList<Car>).Add(car);
            }
        }
        private void edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedItem != null)
            {
                Car selectedCar = dataGridView1.SelectedItem as Car;
                int selectedIndex = dataGridView1.Items.IndexOf(selectedCar);
                EditWindow editWindow = new EditWindow(selectedCar);
                bool? correct = editWindow.ShowDialog();
                if (correct == true)
                {
                    myCarsBindingList[selectedIndex] = selectedCar;
                    dataGridView1.ItemsSource = myCarsBindingList;
                }
            }
        }

    }
}