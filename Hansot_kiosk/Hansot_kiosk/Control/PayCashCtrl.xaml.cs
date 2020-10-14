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
    /// PayCashCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PayCashCtrl : UserControl
    {
        public PayCashCtrl()
        {
            InitializeComponent();
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
    }
}
