using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SleepyTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MINUTES_TO_SECONDS = 60;

        private enum TimeUnit
        {
            Minute = 0,
            Hour = 1
        }

        public int[] presets = new[] {15, 30, 60, 120};


        public MainWindow()
        {
            InitializeComponent();
        }

        public void SetSleepTime(float minutes)
        {
            int mili = (int) (minutes * MINUTES_TO_SECONDS);

            AbortSleepTime();

            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = string.Format("/C shutdown /s /t {0}", mili),
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }

        public void AbortSleepTime()
        {
            var abortPrevious = Process.Start(new ProcessStartInfo {
                FileName = "cmd",
                Arguments = "/C shutdown /a",
                UseShellExecute = false,
                CreateNoWindow = true
            });

            abortPrevious.WaitForExit();
        }


        private void preset1Button_Click(object sender, RoutedEventArgs e)
        {
            SetSleepTime(presets[0]);
        }

        private void preset2Button_Click(object sender, RoutedEventArgs e)
        {
            SetSleepTime(presets[1]);
        }

        private void preset3Button_Click(object sender, RoutedEventArgs e)
        {
            SetSleepTime(presets[2]);
        }

        private void preset4Button_Click(object sender, RoutedEventArgs e)
        {
            SetSleepTime(presets[3]);
        }

        private void abortButton_Click(object sender, RoutedEventArgs e)
        {
            AbortSleepTime();
        }

        private void customButton_Click(object sender, RoutedEventArgs e)
        {
            float time = 0;

            if (float.TryParse(customTimeText.Text, out time))
            {
                var timeUnit = (TimeUnit) customTimeUnitCombobox.SelectedIndex;

                if (timeUnit == TimeUnit.Hour)
                {
                    time *= 60;
                }

                SetSleepTime(time);
            }
        }
    }
}
