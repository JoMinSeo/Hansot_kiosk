using Hansot_kiosk.Common;
using Hansot_kiosk.Manager;
using Hansot_kiosk.Model;
using Kiosk.UIManager;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int pagingNum = 9;
        private List<MenuModel> menuList;
        private List<MenuModel> _currentCategoryMenuList;
        private List<MenuModel> currentCategoryMenuList
        {
            get
            {
                return _currentCategoryMenuList;
            }
            set
            {
                _currentCategoryMenuList = value;
                _currentPage = 1;

                paging();

                btn_CategoryPrev.IsEnabled = false;
                if (currentCategoryMenuList.Count> pagingNum)
                {
                    btn_CategoryNext.IsEnabled = true;
                }
                else
                {
                    btn_CategoryNext.IsEnabled = false;
                }
            }
        }


        private ObservableCollection<MenuModel> _pagingMenuList;
        private ObservableCollection<MenuModel> pagingMenuList
        {
            get { return _pagingMenuList; }
            set
            {
                _pagingMenuList = value;
                lbMenus.ItemsSource = _pagingMenuList;
            }
        }
        private int _currentPage = 1;
        private int currentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                btn_CategoryPrev.IsEnabled = false;
                btn_CategoryNext.IsEnabled = false;

                paging();

                if (currentPage > 1)
                {
                    btn_CategoryPrev.IsEnabled = true;
                }
                if (currentCategoryMenuList.Count - (currentPage * pagingNum) > 0)
                {
                    btn_CategoryNext.IsEnabled = true;
                }
            }
        }

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

        public void init()
        {
            btn_MenuReset.IsEnabled = false;
            btn_Order.IsEnabled = false;
            this.DataContext = App.orderManager;

            menuList = App.sQLManager.SelectMenu();
            currentCategoryMenuList = menuList;

            lbMenus.ItemsSource = pagingMenuList;

            lvOrderdMenus.ItemsSource = App.orderManager.OrderedMenus;
        }
        #endregion
        private void paging()
        {
            if (currentCategoryMenuList.Count - (currentPage * pagingNum - pagingNum) < pagingNum && currentCategoryMenuList.Count - (currentPage * pagingNum - pagingNum) > 0)
            {
                pagingMenuList = new ObservableCollection<MenuModel>(currentCategoryMenuList.GetRange(
                    currentPage * pagingNum - pagingNum, 
                    currentCategoryMenuList.Count - (currentPage * pagingNum - pagingNum)).ToList());
            }
            else if (currentCategoryMenuList.Count - (currentPage * pagingNum - pagingNum) >= pagingNum)
            {
                pagingMenuList = new ObservableCollection<MenuModel>(currentCategoryMenuList.GetRange(
                    currentPage * pagingNum - pagingNum, pagingNum).ToList());
            }
        }
        #region SelectionChanged
        private void lbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPage = 1;
            if (lbCategory.SelectedIndex == -1)
            {
                return;
            }
            else if (lbCategory.SelectedIndex == 0)
            {
                currentCategoryMenuList = menuList;
            }
            else
            {
                ECategory category = (ECategory)lbCategory.SelectedIndex;

                currentCategoryMenuList = menuList.Where(x => x.Category == category).ToList();
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

            btn_MenuReset.IsEnabled = true;
            btn_Order.IsEnabled = true;
        }
        #endregion
        #region OrderedMenusAmountControl
        private void menuAmountUp(MenuModel menu)
        {
            if (menu != null)
            {
                menu.Amount++;
                App.orderManager.TotalPrice += menu.Price;
            }
        }
        private void orderedMenuRemove(MenuModel menu)
        {
            if (menu != null)
            {
                App.orderManager.OrderedMenus.Remove(menu);
                App.orderManager.TotalPrice -= (menu.Price * menu.Amount);
                menu.Amount = 0;

                if (!App.orderManager.OrderedMenus.Any())
                {
                    btn_MenuReset.IsEnabled = false;
                    btn_Order.IsEnabled = false;
                }
            }
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
                    orderedMenuRemove(senderMenu);
                }
            }
        }
        private void btn_MenuDeleteClick(object sender, RoutedEventArgs e)
        {
            orderedMenuRemove((sender as Button).DataContext as MenuModel);
        }
        #endregion
        private void btn_MenuReset_Click(object sender, RoutedEventArgs e)
        {
            foreach(var m in App.orderManager.OrderedMenus)
            {
                m.Amount = 0;
            }
            App.orderManager.OrderedMenus.Clear();
            App.orderManager.TotalPrice = 0;

            btn_MenuReset.IsEnabled = false;
            btn_Order.IsEnabled = false;
        }

        private void btn_Order_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.uIStateManager.Get(UICategory.PLACE);
            if (uc != null)
            {
                App.uIStateManager.Push(uc);
            }
        }

        private void btn_PrevCtrl_Click(object sender, RoutedEventArgs e)
        {
            App.uIStateManager.Pop();
        }

        private void btn_CategoryPrev_Click(object sender, RoutedEventArgs e)
        {
            currentPage--;
        }

        private void btn_CategoryNext_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
        }
    }
}