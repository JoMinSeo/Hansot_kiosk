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
