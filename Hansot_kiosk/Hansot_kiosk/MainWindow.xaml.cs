using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hansot_kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ProMet.Play();
        }

        private void ProMet_MediaEnded(object sender, RoutedEventArgs e)
        {

        }

        private void ProMet_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void ProMet_MediaOpened(object sender, RoutedEventArgs e)
        {
            this.playTimeSlider.Minimum = 0;

            this.playTimeSlider.Maximum = this.mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProMet.Source == null)

            {

                return;

            }
            this.ProMet.Play();
        }
    }
}
