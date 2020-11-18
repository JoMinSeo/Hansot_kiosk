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
        #region property
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

                CategoryPrevBtn.IsEnabled = false;
                if (currentCategoryMenuList.Count> pagingNum)
                {
                    CategoryNextBtn.IsEnabled = true;
                }
                else
                {
                    CategoryNextBtn.IsEnabled = false;
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
                CategoryPrevBtn.IsEnabled = false;
                CategoryNextBtn.IsEnabled = false;

                paging();

                if (currentPage > 1)
                {
                    CategoryPrevBtn.IsEnabled = true;
                }
                if (currentCategoryMenuList.Count - (currentPage * pagingNum) > 0)
                {
                    CategoryNextBtn.IsEnabled = true;
                }
            }
        }
        #endregion
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
            MenuResetBtn.IsEnabled = false;
            OrderBtn.IsEnabled = false;
            this.DataContext = App.orderManager;

            menuList = App.sQLManager.SelectAllMenus();
            currentCategoryMenuList = menuList;

            lvOrderdMenus.ItemsSource = App.orderManager.OrderedMenus;

            lbCategory.SelectedIndex = 0;
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

            MenuResetBtn.IsEnabled = true;
            OrderBtn.IsEnabled = true;
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
                    MenuResetBtn.IsEnabled = false;
                    OrderBtn.IsEnabled = false;
                }
            }
        }
        private void AmountUpBtn_Click(object sender, RoutedEventArgs e)
        {
            menuAmountUp((sender as Button).DataContext as MenuModel);
        }
        private void AmountDownBtn_Click(object sender, RoutedEventArgs e)
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
        private void MenuDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            orderedMenuRemove((sender as Button).DataContext as MenuModel);
        }
        #endregion
        #region BtnClickEvent
        private void MenuResetBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach(var m in App.orderManager.OrderedMenus)
            {
                m.Amount = 0;
            }
            App.orderManager.OrderedMenus.Clear();
            App.orderManager.TotalPrice = 0;

            MenuResetBtn.IsEnabled = false;
            OrderBtn.IsEnabled = false;
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.uIStateManager.Get(UICategory.PLACE);
            if (uc != null)
            {
                App.uIStateManager.Push(uc);
            }
        }

        private void PrevCtrlBtn_Click(object sender, RoutedEventArgs e)
        {
            App.uIStateManager.Pop();
        }

        private void CategoryPrevBtn_Click(object sender, RoutedEventArgs e)
        {
            currentPage--;
        }

        private void CategoryNextBtn_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
        }
        #endregion
    }
}