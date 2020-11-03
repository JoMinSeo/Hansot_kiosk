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
        List<MenuModel> menuList;
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
                Category category = (Category)lbCategory.SelectedIndex;
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
                model.Amount = 1;
                App.orderManager.OrderedMenus.Add(model);
            }
            else
            {
                sameMenu.Amount += 1;
            }
            lbMenus.UnselectAll(); //선택된것을 해제하는 코드로, 같은 메뉴를 두번 클릭이 가능함
        }
        #endregion
    }
}
