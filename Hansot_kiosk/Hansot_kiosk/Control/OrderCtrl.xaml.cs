using Hansot_kiosk.Common;
using Hansot_kiosk.Manager;
using Hansot_kiosk.Model;
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
        #region Init
        private MenuManager MenuManager = new MenuManager();
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
            lbMenus.ItemsSource = MenuManager.ListMenu;
            lvOrderdMenus.ItemsSource = App.orderManager.OrderedMenus;
        }
        #endregion
        #region SelectionChanged
        private void lbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCategory.SelectedIndex == -1)
            {
                return;
            }
            else if (lbCategory.SelectedIndex == 0)
            {
                lbMenus.ItemsSource = MenuManager.ListMenu;
            }
            else
            {
                Category category = (Category)lbCategory.SelectedIndex;
                lbMenus.ItemsSource = MenuManager.ListMenu.Where(x => x.Category == category).ToList();
            }
        }
        private void lbMenus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMenus.SelectedItems.Count < 1)
            {
                return;
            }

            MenuModel model = (MenuModel)lbMenus.SelectedItem;
            App.orderManager.OrderedMenus.Add(model);
            lbMenus.UnselectAll(); //선택된것을 해제하는 코드로, 같은 메뉴를 두번 클릭이 가능함
        }
        #endregion
    }
}
