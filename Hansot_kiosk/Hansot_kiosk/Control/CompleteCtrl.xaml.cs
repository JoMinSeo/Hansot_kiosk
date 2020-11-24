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

            this.IsVisibleChanged += CompleteCtrl_IsVisibleChanged;

            this.DataContext = App.orderManager.CurrentOrder;
        }
        private void CompleteCtrl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.Visibility == Visibility.Visible)
            {
                App.orderManager.CompleteOrder();
            }
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Init();
        }
    }
}
