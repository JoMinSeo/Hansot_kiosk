using Kiosk.UIManager;
using System.Windows;
using System.Windows.Controls;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// PaySelectCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PaySelectCtrl : UserControl
    {
        public PaySelectCtrl()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            lvOrderdMenus.ItemsSource = App.orderManager.OrderedMenus;
            this.DataContext = App.orderManager;
        }

        private void PreviusBtn_Click(object sender, RoutedEventArgs e)
        {
            App.uIStateManager.Pop();
        }

        private void CreditBtn_Click(object sender, RoutedEventArgs e)
        {
            App.orderManager.CurrentOrder.IsCard = true;
            UserControl uc = App.uIStateManager.Get(UICategory.PAYCREDIT);
            if (uc != null)
                App.uIStateManager.Push(uc);
        }

        private void CashBtn_Click(object sender, RoutedEventArgs e)
        {
            App.orderManager.CurrentOrder.IsCard = false;
            UserControl uc = App.uIStateManager.Get(UICategory.PAYCASH);
            if (uc != null)
                App.uIStateManager.Push(uc);
        }
    }
}                                            
