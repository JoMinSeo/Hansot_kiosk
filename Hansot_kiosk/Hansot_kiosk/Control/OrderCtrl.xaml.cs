using Hansot_kiosk.Common;
using Hansot_kiosk.Manager;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// Interaction logic for OrderCtrl.xaml
    /// </summary>
    public partial class OrderCtrl : UserControl
    {
        private OrderMenuManager OrderMenuManager = new OrderMenuManager();
        public OrderCtrl()
        {
            InitializeComponent();
            this.Loaded += OrderCtrl_Loaded;
        }

        private void OrderCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            init();
        }

        private void init()
        {
            lbMenus.ItemsSource = OrderMenuManager.ListMenu.Where(x => x.Category == Category.meatmeat).ToList();
        }

        private void lbMenus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Menu model = lbMenus.SelectedItem as Model.Menu;
            App.orderManager.SelectedMenus.Add(model);
            lvOrderdMenus.ItemsSource = App.orderManager.SelectedMenus;
        }
    }
}
