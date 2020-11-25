using Hansot_kiosk.Model;
using Kiosk.UIManager;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// PayCashCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PayCashCtrl : UserControl
    {
        public PayCashCtrl()
        {
            InitializeComponent();
            this.IsVisibleChanged += new DependencyPropertyChangedEventHandler
                             (PayCashCtrl_IsVisibleChanged);

            ((MainWindow)System.Windows.Application.Current.MainWindow).DeleGate += init;
        }

        private void init()
        {
            this.DataContext = App.OrderManager.CurrentOrder;
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            App.UIStateManager.Pop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.UIStateManager.Get(UICategory.COMPLETE);
            if (uc != null)
                App.UIStateManager.Push(uc);
        }

        /// <summary>
        /// usercontrol의 visible이 변경되었을때 textbox에 포커스
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PayCashCtrl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate ()
                {
                    barcodeTb.Focus();
                }));
            }
        }

        /// <summary>
        /// 텍스트박스의 글자가 변경되었을때 텍스트박스의 텍스트를 저장하는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barcodeTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string barcode = barcodeTb.Text;
            UserModel currentUser = App.UserManager.compareName(barcode);

            if (currentUser != null)
            {
                App.OrderManager.CurrentOrder.User_IDX = currentUser.IDX;
                App.OrderManager.CurrentOrder.User_Name = currentUser.Name;

                comfirmBtn.IsEnabled = true;
                barcodeTb.IsEnabled = false;
                return;
            }
        }
    }
}
