using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace UniversalFileConverter
{
    /// <summary>
    /// Interaction logic for ConvertableControl.xaml
    /// </summary>
    public partial class ConvertibleControl : UserControl
    {
        public readonly string Path;
        public readonly FileType Type;
        public bool IsConverting = false;
        public string OutputFilePath = string.Empty;

        public ConvertibleControl(string path, FileType type)
        {
            InitializeComponent();
            Path = path;
            Type = type;
            SetDefaultIcon();
            string fname = Path[((Path.Contains('\\') ? Path.LastIndexOf('\\') : Path.LastIndexOf('/')) + 1)..];
            if (fname.Length < 45)
            {
                FileName.Content = fname;
            }
            else
            {
                FileName.Content = fname[..20] + "[...]" + fname[^20..];
            }

            if (type == FileType.Unknown)
            {
                ((Panel) OutputFormatBox.Parent).Children.Remove(OutputFormatBox);
            }

            foreach (string format in Converter.FilesFormats[Type])
            {
                OutputFormatBox.Items.Add(format.ToUpper());
            }
            if (OutputFormatBox.Items.Count > 0) 
            { 
                OutputFormatBox.SelectedIndex = 0;
            }
            else 
            {
                (OutputFormatBox.Parent as Panel)?.Children.Remove(OutputFormatBox);
                (ChangeDirectoryButton.Parent as Panel)?.Children.Remove(ChangeDirectoryButton);
            }
            UpdatePath(Converter.ChageExtension(Path, OutputFormatBox.SelectedItem?.ToString()?.ToLower() ?? string.Empty));
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsConverting)
                return;
            dynamic par = Parent;
            (par.Parent.Parent.Parent as ConvertWindow)?.ConvertibleQueue.Remove(this);
            (Parent as StackPanel)?.Children.Remove(this);
        }

        private void OutputFormatBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePath(Converter.ChageExtension(Path, OutputFormatBox.SelectedItem.ToString()?.ToLower() ?? string.Empty));
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                Panel parent = (Panel) Parent;
                foreach (object obj in parent.Children)
                {
                    ConvertibleControl control = (ConvertibleControl) obj;
                    control.OutputFormatBox.SelectedIndex = OutputFormatBox.SelectedIndex;
                    control.UpdatePath(Converter.ChageExtension(
                        control.Path, OutputFormatBox.SelectedItem.ToString()?.ToLower() ?? string.Empty
                    ));
                }
            }
        }

        public void UpdatePath(string path)
        {
            OutputFilePath = path;
            UpdatePath();
        }

        public void UpdatePath()
        {
            if (new FileInfo(OutputFilePath).Exists)
            {
                ReplaceFileBox.Visibility = Visibility.Visible;
            }
            else
            {
                ReplaceFileBox.Visibility = Visibility.Hidden;
            }
        }

        public void SetDefaultIcon() => SetIcon(Type.ToString().ToLower());

        public void SetIcon(string icon) => Icon.Source = App.Images["file_" + icon];

        private void ReplaceFileBox_Click(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                Panel parent = (Panel) Parent;
                foreach (object obj in parent.Children)
                {
                    ConvertibleControl control = (ConvertibleControl) obj;
                    control.ReplaceFileBox.IsChecked = ReplaceFileBox.IsChecked;
                }
            }
        }

        private void ChangeDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new() { FileName = OutputFilePath, Filter = "All files|*.*" };
            if (saveFileDialog.ShowDialog() ?? false)
            {
                UpdatePath(saveFileDialog.FileName);
            }
        }
    }
}
