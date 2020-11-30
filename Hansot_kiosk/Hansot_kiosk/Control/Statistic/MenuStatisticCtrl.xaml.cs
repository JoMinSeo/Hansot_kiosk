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
    /// Interaction logic for MenuStatisticCtrl.xaml
    /// </summary>
    public partial class MenuStatisticCtrl : UserControl
    {
        public List<MenuModel> menus;
        public MenuStatisticCtrl()
        {
            InitializeComponent();
            App.InitDeleGate += this.init;
        }
        private void init()
        {
            menus = new List<MenuModel>(App.Menus);
            menus.ForEach(menu => menu.TotalPrice = App.OrderedMenus.Where(orderedMenu => orderedMenu.MenuIDX == menu.IDX).Sum(
                orderedMenu => orderedMenu.Amount * menu.DiscountedPrice));
        }
    }
}
