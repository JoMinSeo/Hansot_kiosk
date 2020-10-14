using Hansot_kiosk.Common;
using Hansot_kiosk.Manager;
using Hansot_kiosk.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// Interaction logic for OrderCtrl.xaml
    /// </summary>
    public partial class OrderCtrl : UserControl
    {
        private OrderViewModel orderViewModel = new OrderViewModel();
        public OrderCtrl()
        {
            InitializeComponent();
            this.Loaded += OrderCtrl_Loaded;

        }

        private void OrderCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            init();
        }

        private void init()
        {
            orderViewModel.LoadMenu();
            lbMenus.ItemsSource = orderManager.lstMenu.Where(x => x.Category == Category.meatmeat).ToList();
        }
    }
}
