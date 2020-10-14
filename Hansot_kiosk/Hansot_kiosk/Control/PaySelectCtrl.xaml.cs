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
        }

        private void PreviusBtn_Click(object sender, RoutedEventArgs e)
        {
            App.uIStateManager.Pop();
        }

        private void CreditBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.uIStateManager.Get(UICategory.PAYCREDIT);
            if (uc != null)
                App.uIStateManager.Push(uc);
        }

        private void CashBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.uIStateManager.Get(UICategory.PAYCASH);
            if (uc != null)
                App.uIStateManager.Push(uc);
        }

    }
}
