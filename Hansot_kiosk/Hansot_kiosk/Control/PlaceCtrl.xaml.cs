using Kiosk.UIManager;
using System.Windows;
using System.Windows.Controls;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// PlaceCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlaceCtrl : UserControl
    {
        public PlaceCtrl()
        {
            InitializeComponent();
        }

        private void ShopMealBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.uIStateManager.Get(UICategory.SEATSELECT);
            if (uc != null)
                App.uIStateManager.Push(uc);
        }

        private void TakeOutBtn_Click(object sender, RoutedEventArgs e)
        {
            App.orderManager.CurrentOrder.Seat = 0;
            UserControl uc = App.uIStateManager.Get(UICategory.PAYSELECT);
            if (uc != null)
                App.uIStateManager.Push(uc);
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            App.uIStateManager.Pop();
        }
    }
}
