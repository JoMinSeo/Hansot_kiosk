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

            App.InitDeleGate += init;
        }

        private void init()
        {
            this.DataContext = App.OrderManager.CurrentOrder;
        }

        private void CompleteCtrl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                App.OrderManager.CompleteOrder();
            }
        }
    }
}
