using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace UniversalFileConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fileDialog = new() { Multiselect = true };
            if (fileDialog.ShowDialog() ?? false)
                ShowConvertWindow(fileDialog.FileNames);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                e.Handled = true;
                ShowConvertWindow(files);
            }
        }

        private void ShowConvertWindow(string[] files)
        {
            new ConvertWindow(files).Show();
            Close();
        }
    }
}
