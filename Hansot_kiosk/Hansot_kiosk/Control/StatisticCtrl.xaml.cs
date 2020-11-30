using LiveCharts;
using LiveCharts.Wpf;
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

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// Interaction logic for StatisticCtrl.xaml
    /// </summary>
    public partial class StatisticCtrl : UserControl
    {
        private List<UserControl> controls = new List<UserControl>();
        private List<Button> btnss = new List<Button>();
        public StatisticCtrl()
        {
            InitializeComponent();

            controls.Add(TotalStatisticCtrl);
            controls.Add(CategoryStatisticCtrl);
            controls.Add(UserStatisticCtrl);
            controls.Add(menuStatisticCtrl);

            btnss.Add(TotalStatisticCtrlBtn);
            btnss.Add(CategoryStatisticCtrlBtn);
            btnss.Add(MenuStatisticCtrlBtn);

            TotalStatisticCtrlBtn.IsEnabled = false;
        }

        private void PrevCtrlBtn_Click(object sender, RoutedEventArgs e)
        {
            App.UIStateManager.Pop();
        }

        private void TotalStatisticCtrlBtn_Click(object sender, RoutedEventArgs e)
        {
            controls.ForEach(control => control.Visibility = Visibility.Collapsed);
            TotalStatisticCtrl.Visibility = Visibility.Visible;
        }

        private void CategoryStatisticCtrlBtn_Click(object sender, RoutedEventArgs e)
        {
            controls.ForEach(control => control.Visibility = Visibility.Collapsed);
            CategoryStatisticCtrl.Visibility = Visibility.Visible;
        }

        private void UserStatisticCtrlBtn_Click(object sender, RoutedEventArgs e)
        {
            controls.ForEach(control => control.Visibility = Visibility.Collapsed);
            UserStatisticCtrl.Visibility = Visibility.Visible;
        }

        private void MenuStatisticCtrlBtn_Click(object sender, RoutedEventArgs e)
        {
            controls.ForEach(control => control.Visibility = Visibility.Collapsed);
            menuStatisticCtrl.Visibility = Visibility.Visible;
        }
    }
}
