using Hansot_kiosk.Model;
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

namespace Hansot_kiosk.Control.Statistic
{
    /// <summary>
    /// Interaction logic for UserStatisticCtrl.xaml
    /// </summary>
    public partial class UserStatisticCtrl : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public UserStatisticCtrl()
        {
            InitializeComponent();
            App.InitDeleGate += this.init;
        }
        private void init()
        {
            List<int> values = new List<int>();

            foreach(UserModel user in App.Users)
            {
                values.Add(App.Orders.Where(order => order.User_IDX == user.IDX).Sum(order => order.TotalPrice));
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "총 매출",
                    Values = new ChartValues<int>(values)
                }
            };

            //also adding values updates and animates the chart automatically

            Labels = App.Users.Select(user => user.Name).ToArray();

            Formatter = value => value.ToString("N");

            DataContext = this;
        }
    }
}
