using Kiosk.UIManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UIManager;

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
                             (LoginControl_IsVisibleChanged);
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
        void LoginControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
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
            if (barcodeTb.Text.Equals(""))
            {
                // 함수 종료 
                return;
            }
            String barcode = barcodeTb.Text;
            barcodeTb.Text = "";
            UserControl uc = App.uIStateManager.Get(UICategory.COMPLETE);
            if (uc != null)
                App.uIStateManager.Push(uc);
        }
    }
}
