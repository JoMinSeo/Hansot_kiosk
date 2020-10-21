using Hansot_kiosk.Common;
using Hansot_kiosk.Manager;
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
            lbMenus.ItemsSource = OrderMenuManager.ListMenu;
            lvOrderdMenus.ItemsSource = App.orderManager.orderedMenus;
        }
        private void lbMenus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMenus.SelectedItems.Count < 1)
            {
                return;
            }

            Model.MenuModel model = lbMenus.SelectedItem as Model.MenuModel;
            App.orderManager.orderedMenus.Add(model);
        }  
        private void lbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCategory.SelectedIndex == -1)
            {
                return;
            }
            else if (lbCategory.SelectedIndex == 0)
            {
                lbMenus.ItemsSource = OrderMenuManager.ListMenu;
            }
            else
            {
                Category category = (Category)lbCategory.SelectedIndex;
                lbMenus.ItemsSource = OrderMenuManager.ListMenu.Where(x => x.Category == category).ToList();
            }
        }
    }
}
