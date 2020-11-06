using Kiosk.UIManager;
using System.Windows;
using System.Windows.Controls;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// SeatSelectCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SeatSelectCtrl : UserControl
    {
        public SeatSelectCtrl()
        {
            InitializeComponent();
        }

        private void PreviusBtn_Click(object sender, RoutedEventArgs e)
        {
            App.uIStateManager.Pop();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.uIStateManager.Get(UICategory.PAYSELECT);
            if (uc != null)
                App.uIStateManager.Push(uc);
        }
    }
}
