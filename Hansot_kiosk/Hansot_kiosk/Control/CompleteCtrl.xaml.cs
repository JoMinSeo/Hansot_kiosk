using System.Windows;
using System.Windows.Controls;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// CompleteCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CompleteCtrl : UserControl
    {
        public CompleteCtrl()
        {
            InitializeComponent();

            this.DataContext = App.orderManager.CurrentOrder;
        }
        private void CompleteOrder()
        {

            App.orderManager.CompleteOrder();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Init();
        }
    }
}
