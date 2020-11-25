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
            UserControl uc = App.UIStateManager.Get(UICategory.SEATSELECT);
            if (uc != null)
                App.UIStateManager.Push(uc);
        }

        private void TakeOutBtn_Click(object sender, RoutedEventArgs e)
        {
            App.OrderManager.CurrentOrder.Seat_IDX = 0;
            UserControl uc = App.UIStateManager.Get(UICategory.PAYSELECT);
            if (uc != null)
                App.UIStateManager.Push(uc);
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            App.UIStateManager.Pop();
        }
    }
}
