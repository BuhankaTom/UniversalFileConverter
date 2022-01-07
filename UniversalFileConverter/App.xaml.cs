using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows;

namespace UniversalFileConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly Dictionary<string, BitmapImage> Images = new()
        {
            ["start_converting"] = new BitmapImage(new Uri("pack://application:,,,/Graphics/start_converting.png")),
            ["stop_converting"] = new BitmapImage(new Uri("pack://application:,,,/Graphics/stop_converting.png")),
            ["file_video"] = new BitmapImage(new Uri("pack://application:,,,/Graphics/file_video.png")),
            ["file_audio"] = new BitmapImage(new Uri("pack://application:,,,/Graphics/file_audio.png")),
            ["file_image"] = new BitmapImage(new Uri("pack://application:,,,/Graphics/file_image.png")),
            ["file_unknown"] = new BitmapImage(new Uri("pack://application:,,,/Graphics/file_unknown.png")),
            ["file_done"] = new BitmapImage(new Uri("pack://application:,,,/Graphics/file_done.png")),
            ["file_converting"] = new BitmapImage(new Uri("pack://application:,,,/Graphics/file_converting.png")),
            ["file_error"] = new BitmapImage(new Uri("pack://application:,,,/Graphics/file_error.png")),
        };

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new MainWindow().Show();
        }

        public new void Run()
        {
#if DEBUG
            base.Run();
#else
            try
            {
                base.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Got an exception!\n\n{ex}");
            }
#endif
        }

    }
}
