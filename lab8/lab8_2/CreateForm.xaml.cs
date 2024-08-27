using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media ;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab8_2
{
    /// <summary>
    /// Logika interakcji dla klasy CreateForm.xaml
    /// </summary>
    public partial class CreateForm : Window
    {
        public string fileName { get; private set; }
        public bool Directory { get; private set; }
        public bool readOnly { get; private set; }
        public bool archive { get; private set; }
        public bool hidden { get; private set; }
        public bool systemInfo { get; private set; }

        public CreateForm()
        {
            InitializeComponent();
        }

        private void CreateItem_Click(object sender, RoutedEventArgs e)
        {

            var name = ElementName.Text;
            Directory = DirectoryRadioButton.IsChecked.Value;
            if (!Directory)
            {
                if (Regex.Match(name, @"^[A-Za-z0-9_~\-]{1,8}\.(txt|php|html)$").Success == false)
                {
                    System.Windows.Forms.MessageBox.Show("Nieprawidłowa nazwa pliku!", "Błąd", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
                    return;
                }
            }

            fileName = System.IO.Path.GetFileName(name.Trim());
           
            readOnly = ReadOnly.IsChecked.Value;
            archive = Archive.IsChecked.Value;
            hidden = Hidden.IsChecked.Value;
            systemInfo = SystemInfo.IsChecked.Value;

            this.Close();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
