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
            this.DataContext = App.orderManager.CurrentOrder;
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            App.uIStateManager.Pop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.uIStateManager.Get(UICategory.COMPLETE);
            if (uc != null)
                App.uIStateManager.Push(uc);
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
            UserModel currentUser = App.userManager.compareName(barcode);
            
            if (currentUser != null)
            {
                App.orderManager.CurrentOrder.User_IDX = currentUser.IDX;
                App.orderManager.CurrentOrder.User_Name = currentUser.Name;

                UserNameLab.Content = currentUser.Name;
                comfirmBtn.IsEnabled = true;
                barcodeTb.IsEnabled = false;
                return;
            }
        }
    }
}
