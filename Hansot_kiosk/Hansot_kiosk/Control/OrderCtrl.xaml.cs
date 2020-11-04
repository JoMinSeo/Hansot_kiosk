using Hansot_kiosk.Common;
using Hansot_kiosk.Manager;
using Hansot_kiosk.Model;
using System.Collections.Generic;
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
        private List<MenuModel> menuList;
        #region Init
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
            this.DataContext = App.orderManager;

            menuList = App.sQLManager.SelectMenu();

            lbMenus.ItemsSource = menuList;
            lvOrderdMenus.ItemsSource = App.orderManager.OrderedMenus;
        }
        #endregion
        // 홈버튼 누를 시 메뉴 있다고 메세지 창 띄우기
        #region SelectionChanged
        private void lbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCategory.SelectedIndex == -1)
            {
                return;
            }
            else if (lbCategory.SelectedIndex == 0)
            {
                lbMenus.ItemsSource = menuList;
            }
            else
            {
                ECategory category = (ECategory)lbCategory.SelectedIndex;
                lbMenus.ItemsSource = menuList.Where(x => x.Category == category).ToList();
            }
        }
        private void lbMenus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMenus.SelectedItems.Count < 1)
            {
                return;
            }

            MenuModel model = (MenuModel)lbMenus.SelectedItem;

            MenuModel sameMenu;

            sameMenu = App.orderManager.OrderedMenus.Where(m => m.IDX == model.IDX).FirstOrDefault();

            if (sameMenu == null)
            {
                App.orderManager.OrderedMenus.Add(model);
                menuAmountUp(model);
            }
            else
            {
                menuAmountUp(model);
            }
            lbMenus.UnselectAll(); //선택된것을 해제하는 코드로, 같은 메뉴를 두번 클릭이 가능함
        }
        #endregion
        #region OrderedMenusAmountControl
        private void menuAmountUp(MenuModel menu)
        {
            menu.Amount++;
            App.orderManager.TotalPrice += menu.Price;
        }
        private void btn_AmountUpClick(object sender, RoutedEventArgs e)
        {
            menuAmountUp((sender as Button).DataContext as MenuModel);
        }
        private void btn_AmountDownClick(object sender, RoutedEventArgs e)
        {
            MenuModel senderMenu = (sender as Button).DataContext as MenuModel;
            if (senderMenu != null)
            {
                if (senderMenu.Amount > 1)
                {
                    senderMenu.Amount--;
                    App.orderManager.TotalPrice -= senderMenu.Price;
                }
                else
                {
                    App.orderManager.OrderedMenus.Remove(senderMenu);
                    App.orderManager.TotalPrice -= senderMenu.Price;
                }
            }
        }
        private void btn_MenuDeleteClick(object sender, RoutedEventArgs e)
        {
            MenuModel senderMenu = (sender as Button).DataContext as MenuModel;
            if (senderMenu != null)
            {
                App.orderManager.OrderedMenus.Remove(senderMenu);
                App.orderManager.TotalPrice -= (senderMenu.Price * senderMenu.Amount);
            }
        }
        #endregion
    }
}