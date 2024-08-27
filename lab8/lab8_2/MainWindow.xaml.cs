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
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;
using System.Reflection;

namespace lab8_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private TreeViewItem rootItem;
        private string rootPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog() { Description = "Select directory to open" };

            dlg.ShowDialog();
            string selectedPath = dlg.SelectedPath;
            this.rootPath = selectedPath;

            if (selectedPath != null)
            {
                TreeViewItem rootItem = new TreeViewItem();
                rootItem.Header = System.IO.Path.GetFileName(selectedPath);
                rootItem.Tag = selectedPath;
                rootItem.Style = (Style)FindResource("DirectoryStyle");
                AddContextMenu(rootItem);

                this.rootItem = rootItem;

                PopulateTreeView(selectedPath, rootItem);

                AddContextMenuToTreeItems(rootItem);
                treeView1.Items.Add(rootItem);
            }

        }

        private void PopulateTreeView(string directoryPath, TreeViewItem parentNode)
        {
            foreach (string file in Directory.GetFiles(directoryPath))
            {
                string fileName = System.IO.Path.GetFileName(file);
                TreeViewItem fileItem = new TreeViewItem();
                fileItem.Header = fileName;
                fileItem.Tag = file;
                AddContextMenuToTreeItems(fileItem);
                parentNode.Items.Add(fileItem);
            }

            foreach (string subdirectory in Directory.GetDirectories(directoryPath))
            {
                string directoryName = System.IO.Path.GetFileName(subdirectory);
                TreeViewItem directoryItem = new TreeViewItem();
                directoryItem.Header = directoryName;
                directoryItem.Tag = subdirectory;
                directoryItem.Style = (Style)FindResource("DirectoryStyle");
                AddContextMenuToTreeItems(directoryItem);
                parentNode.Items.Add(directoryItem);

                PopulateTreeView(subdirectory, directoryItem);
            }
        }


        private void treeView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            displayFile();
        }

        private void displayFile()
        {
            TreeViewItem selectedItem = treeView1.SelectedItem as TreeViewItem;
            if (selectedItem != null)
            {
                string path = selectedItem.Tag as string;
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    OpenedFile.Text = File.ReadAllText(path);
                }
            }
        }


        void AddContextMenu(TreeViewItem item)
        {
            ContextMenu contextMenuFile = (ContextMenu)this.Resources["TreeViewContextMenuFile"];
            ContextMenu contextMenuDirectory = (ContextMenu)this.Resources["TreeViewContextMenuDirectory"];

            string itemPath = item.Tag as string;
            if (!string.IsNullOrEmpty(itemPath))
            {
                if (File.Exists(itemPath))
                {
                    item.MouseRightButtonDown += (sender, e) =>
                    {
                        if (e.RightButton == MouseButtonState.Released)
                        {
                            item.Focus();
                            contextMenuFile.IsOpen = true;
                        }
                    };
                    item.ContextMenu = contextMenuFile;
                }
                else if (Directory.Exists(itemPath))
                {
                    item.MouseRightButtonDown += (sender, e) =>
                    {
                        if (e.RightButton == MouseButtonState.Released)
                        {
                            item.Focus();
                            contextMenuDirectory.IsOpen = true;
                        }
                    };
                    item.ContextMenu = contextMenuDirectory;
                }
            }

        }

        void AddContextMenuToTreeItems(TreeViewItem parentNode)
        {
            foreach (TreeViewItem item in parentNode.Items)
            {
                AddContextMenu(item);
                AddContextMenuToTreeItems(item);
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                TreeViewItem selectedItem = treeView1.SelectedItem as TreeViewItem;

                if (selectedItem != null)
                {
                    string itemPath = selectedItem.Tag as string;

                    if (!string.IsNullOrEmpty(itemPath))
                    {
                        if (selectedItem.Parent is ItemsControl parent)
                        {
                            parent.Items.Remove(selectedItem);
                        }

                        if (Directory.Exists(itemPath))
                        {
                            DeleteDirectory(itemPath);
                        }
                        else if (File.Exists(itemPath))
                        {
                            try
                            {
                                if ((File.GetAttributes(itemPath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                                {
                                    File.SetAttributes(itemPath, File.GetAttributes(itemPath) & ~FileAttributes.ReadOnly);
                                }

                                File.Delete(itemPath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error: cannot delete! {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: cannot delete!");
                    }
                }
            }
        }



        private void DeleteDirectory(string path)
        {
            try
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string subdirectory in Directory.GetDirectories(path))
                {
                    DeleteDirectory(subdirectory);
                }

                Directory.Delete(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: cannot delete! {ex.Message}");
            }
        }



        private void CreateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                var treeViewItem = ((ContextMenu)((MenuItem)sender).Parent).PlacementTarget as TreeViewItem;
                var createWindow = new CreateForm();
                createWindow.ShowDialog();

                if (createWindow.fileName != null)
                {
                    var path = treeViewItem.Tag;
                    var newFilePath = System.IO.Path.Combine((string)path, createWindow.fileName);

                    if (createWindow.Directory)
                        Directory.CreateDirectory(newFilePath);
                    else if (!File.Exists(newFilePath))
                    {
                        File.Create(newFilePath);
                    }

                    FileAttributes attr = 0;
                    if (createWindow.archive)
                        attr |= FileAttributes.Archive;
                    if (createWindow.readOnly)
                        attr |= FileAttributes.ReadOnly;
                    if (createWindow.hidden)
                        attr |= FileAttributes.Hidden;
                    if (createWindow.systemInfo)
                        attr |= FileAttributes.System;

                    File.SetAttributes(newFilePath, attr);

                    var item = new TreeViewItem()
                    {
                        Header = createWindow.fileName,
                        Tag = path
                    };

                    AddContextMenu(treeViewItem);
                    treeViewItem.Items.Add(item);

                    ReloadBranch(treeViewItem);
                }
            }
        }

        private void ReloadBranch(TreeViewItem parentNode)
        {
            parentNode.Items.Clear();

            PopulateTreeView((string)parentNode.Tag, parentNode);
            AddContextMenuToTreeItems(parentNode);
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            displayFile();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateStatusBar_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                if (treeView1.SelectedItem is TreeViewItem selectedItem)
                {
                    string itemPath = selectedItem.Tag as string;

                    FileInfo fsi = new FileInfo(itemPath);
                    char read = (fsi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly ? 'r' : '-';
                    char archive = (fsi.Attributes & FileAttributes.Archive) == FileAttributes.Archive ? 'a' : '-';
                    char hidden = (fsi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ? 'h' : '-';
                    char system = (fsi.Attributes & FileAttributes.System) == FileAttributes.System ? 's' : '-';

                    string rahs = $"{read}{archive}{hidden}{system}";

                    rahsInfo.Text = rahs;
                }
            }
        }
    }
}