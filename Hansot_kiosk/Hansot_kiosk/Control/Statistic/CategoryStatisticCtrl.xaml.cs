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
    /// Interaction logic for CategoryStatisticCtrl.xaml
    /// </summary>
    public partial class CategoryStatisticCtrl : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public CategoryStatisticCtrl()
        {
            InitializeComponent();

            App.InitDeleGate += this.init;
        }
        private void init()
        {
            int[] values = new int[5];

            values[0] = (App.Orders.Sum(order => order.TotalPrice));

            foreach(OrderedMenuModel orderedMenu in App.OrderedMenus)
            {
                MenuModel menu = App.Menus.Find(x => x.IDX == orderedMenu.MenuIDX);
                values[(int)menu.Category] += (int)Math.Round(orderedMenu.Amount * menu.Price *
                    ((100-menu.DiscountedPer) * 0.01));
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

            Labels = new string[] { "전부", "고기고기", "세트", "도시락", "사이드" };

            Formatter = value => value.ToString("N");

            DataContext = this;
        }
    }
}
