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
    /// Interaction logic for TotalStatisticCtrl.xaml
    /// </summary>
    public partial class TotalStatisticCtrl : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public int TotalPrice { get; set; }

        public TotalStatisticCtrl()
        {
            InitializeComponent();
            App.InitDeleGate += this.init;
        }
        private void init()
        {
            List<int> values = new List<int>();

            int totalDiscountedPrice = 0;

            foreach(OrderedMenuModel orderedMenu in App.OrderedMenus)
            {
                MenuModel menu = App.Menus.Find(x => x.IDX == orderedMenu.MenuIDX);
                totalDiscountedPrice +=(int)Math.Round(orderedMenu.Amount * menu.Price * 
                    (menu.DiscountedPer * 0.01));
            }

            int totalPurePrice = (from order in App.Orders select order).Sum(order => order.TotalPrice);

            TotalPrice = totalPurePrice + totalDiscountedPrice;

            int totalCardPrice = (from order in App.Orders where order.IsCard select order).Sum(order => order.TotalPrice);

            int totalCashPrice = totalPurePrice - totalCardPrice;

            values.Add(TotalPrice);
            values.Add(totalPurePrice);
            values.Add(totalDiscountedPrice);
            values.Add(totalCashPrice);
            values.Add(totalCardPrice);

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "총 매출",
                    Values = new ChartValues<int>(values)
                }
            };

            //also adding values updates and animates the chart automatically

            Labels = new string[] { "총금액", "순수 매출액", "할인금액", "현금", "카드" };

            Formatter = value => value.ToString("N");

            DataContext = this;
        }
    }
}
