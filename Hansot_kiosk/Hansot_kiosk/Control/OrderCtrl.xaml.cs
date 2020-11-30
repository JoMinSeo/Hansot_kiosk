using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
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
            get => _currentCategoryMenuList;
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
            get => _pagingMenuList;
            set
            {
                _pagingMenuList = value;
                lbMenus.ItemsSource = _pagingMenuList;
            }
        }
        private int _currentPage = 1;
        private int currentPage
        {
            get => _currentPage;
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
        private ObservableCollection<string> _categorys = new ObservableCollection<string>();
        public ObservableCollection<string> Categorys
        {
            get => _categorys;
            set => _categorys = value;
        }
        #endregion
        #region Init
        public OrderCtrl()
        {
            InitializeComponent();
            this.Loaded += OrderCtrl_Loaded;

            App.InitDeleGate += init;
        }

        private void OrderCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            init();
        }

        private void init()
        {
            MenuResetBtn.IsEnabled = false;
            OrderBtn.IsEnabled = false;
            this.DataContext = App.OrderManager.CurrentOrder;

            menuList = App.Menus;
            currentCategoryMenuList = menuList;

            lvOrderdMenus.ItemsSource = App.OrderManager.OrderedMenus;

            categoryInit();
        }

        private void categoryInit()
        {
            if (this.Categorys.Count == 0)
            {
                this.Categorys.Add("전체");
                this.Categorys.Add("고기고기");
                this.Categorys.Add("세트");
                this.Categorys.Add("도시락");
                this.Categorys.Add("사이드");
            }
            lbCategory.ItemsSource = this.Categorys;
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

            sameMenu = App.OrderManager.OrderedMenus.Where(m => m.IDX == model.IDX).FirstOrDefault();

            if (sameMenu == null)
            {
                App.OrderManager.OrderedMenus.Add(model);
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
                App.OrderManager.CurrentOrder.TotalPrice += menu.DiscountedPrice;
            }
        }
        private void orderedMenuRemove(MenuModel menu)
        {
            if (menu != null)
            {
                App.OrderManager.OrderedMenus.Remove(menu);
                App.OrderManager.CurrentOrder.TotalPrice -= (menu.DiscountedPrice * menu.Amount);
                menu.Amount = 0;

                if (!App.OrderManager.OrderedMenus.Any())
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
                    App.OrderManager.CurrentOrder.TotalPrice -= senderMenu.DiscountedPrice;
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
            if (App.OrderManager.OrderedMenus.Any())
            {
                if (MessageBoxResult.No == MessageBox.Show("선택된 메뉴가 초기화 됩니다. 괜찮으십니까?",
                        "메뉴 초기화", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    return;
                }
            }
            foreach(var m in App.OrderManager.OrderedMenus)
            {
                m.Amount = 0;
            }
            App.OrderManager.OrderedMenus.Clear();
            App.OrderManager.CurrentOrder.TotalPrice = 0;

            MenuResetBtn.IsEnabled = false;
            OrderBtn.IsEnabled = false;
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.UIStateManager.Get(UICategory.PLACE);
            if (uc != null)
            {
                App.UIStateManager.Push(uc);
            }
        }

        private void PrevCtrlBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.OrderManager.OrderedMenus.Any())
            {
                if (MessageBoxResult.No == MessageBox.Show("주문된 메뉴가 초기화 됩니다. 괜찮으십니까?",
                        "메인화면으로 가기", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    return;
                }
            }
            App.InitDeleGate();
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