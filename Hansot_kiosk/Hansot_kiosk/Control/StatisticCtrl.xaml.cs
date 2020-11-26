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
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public StatisticCtrl()
        {
            InitializeComponent();

            List<int> totalAmountList = new List<int>();

            for (int i = 0; i < App.Menus.Count; i++)
            {
                totalAmountList.Add((from orderedMenu in App.OrderedMenus where orderedMenu.MenuIDX == i select orderedMenu.Amount).Sum());
            }
            int[] totalAmountArr = totalAmountList.ToArray();

            int[] totalPriceArr = new int[totalAmountArr.Length];

            for(int i=0; i<totalAmountArr.Length; i++)
            {
                totalPriceArr[i] = totalAmountArr[i] * (App.Menus.Find(menu => menu.IDX == (i+1)).DiscountedPrice);
            }
            

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "총 매출",
                    Values = new ChartValues<int>(totalPriceArr)
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "총 수량",
                Values = new ChartValues<int>(totalAmountArr)
            }); ;

            //also adding values updates and animates the chart automatically

            List<string> menuNames = new List<string>();
            App.Menus.ForEach(menu => menuNames.Add(menu.Name));

            Labels = menuNames.ToArray();

            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        private void PrevCtrlBtn_Click(object sender, RoutedEventArgs e)
        {
            App.UIStateManager.Pop();
        }
    }
}
