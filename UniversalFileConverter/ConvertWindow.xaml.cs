using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace UniversalFileConverter
{
    /// <summary>
    /// Interaction logic for ConvertWindow.xaml
    /// </summary>
    public partial class ConvertWindow : Window
    {
        /// <summary>
        /// Queue for available files to convert 
        /// </summary>
        public readonly List<ConvertibleControl> ConvertibleQueue = new();
        /// <summary>
        /// List of active converting processes 
        /// </summary>
        private readonly List<(ConvertibleControl, Process)> processes = new();
        /// <summary>
        /// Timer for updating the converting process 
        /// </summary>
        private readonly DispatcherTimer updateTimer;
        private bool isConverting = false;
        private int maxProcesses = 5;

        public ConvertWindow(string[] files)
        {
            InitializeComponent();

            foreach (string file in files)
            {
                ConvertibleControl control = new(file, Converter.GetFileType(file));
                ConvertibleQueue.Add(control);
                ConvertiblesStackPanel.Children.Add(control);
            }
            ConvertibleQueue.Reverse();

            updateTimer = new();
            updateTimer.Tick += new EventHandler(ConvertablesChecker);
            updateTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            updateTimer.Start();
        }

        private void MaxProcessesValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out int _);
        }

        private void MaxProcesses_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(MaxProcesses.Text, out int res) || res > 25)
            {
                MaxProcesses.Clear();
                MaxProcesses.AppendText("25");
            }
        }

        private void StartOrStopButton_Click(object? sender, RoutedEventArgs? e)
        {
            if (isConverting) 
            {
                ((Image) StartOrStopButton.Content).Source = App.Images["start_converting"];
                CheckFinishedProcesses();
                foreach ((ConvertibleControl conv, Process proc) in processes)
                {
                    proc.Kill();
                    conv.IsConverting = false;
                    conv.SetDefaultIcon();
                    conv.UpdatePath();
                }
                processes.Clear();
                isConverting = false;
            } 
            else if (ConvertibleQueue.Count > 0)
            {
                ((Image) StartOrStopButton.Content).Source = App.Images["stop_converting"];
                maxProcesses = int.TryParse(MaxProcesses.Text, out int res) ? res : 5;
                ConvertationProgressBar.Maximum = ConvertibleQueue.Count;
                isConverting = true;
            }
        }

        private void ConvertablesChecker(object? sender, EventArgs e)
        {
            if (!isConverting) return;
            CheckFinishedProcesses();
            while ((ConvertibleQueue.Count >= maxProcesses && processes.Count < maxProcesses) ||
                   (ConvertibleQueue.Count < maxProcesses && processes.Count < ConvertibleQueue.Count))
            {
                for (int index = ConvertibleQueue.Count - 1; index >= 0; index--)
                {
                    ConvertibleControl conv = ConvertibleQueue[index];
                    if (conv.IsConverting)
                        continue;
                    if (conv.Type == FileType.Unknown)
                    {
                        ConvertiblesStackPanel.Children.Remove(conv);
                        ConvertibleQueue.Remove(conv);
                        continue;
                    }
                    conv.IsConverting = true;
                    conv.SetIcon("converting");
                    processes.Add((
                        conv, 
                        Converter.Convert(conv.Path, conv.OutputFilePath ?? string.Empty, conv.ReplaceFileBox.IsChecked ?? false)
                    ));
                    if (processes.Count > maxProcesses || processes.Count > ConvertibleQueue.Count)
                        break;
                }
            }
            ConvertationProgressBar.Value = ConvertationProgressBar.Maximum - ConvertibleQueue.Count;
            if (ConvertibleQueue.Count == 0)
            {
                StartOrStopButton_Click(null, null);
            }
        }

        private void CheckFinishedProcesses()
        {
            for (int index = processes.Count - 1; index >= 0; index--)
            {
                (ConvertibleControl conv, Process proc) = processes[index];
                if (proc.HasExited)
                {
                    if (proc.ExitCode == 0)
                    {
                        conv.SetIcon("done");
                    }
                    else
                    {
                        conv.SetIcon("error");
                    }
                    conv.IsConverting = false;
                    processes.Remove((conv, proc));
                    ConvertibleQueue.Remove(conv);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            updateTimer.Stop();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
