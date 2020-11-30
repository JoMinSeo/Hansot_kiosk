using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using Hansot_kiosk.view;
using System.Windows;
using System.Windows.Controls;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// Interaction logic for ReadyCtrl.xaml
    /// </summary>
    public partial class ReadyCtrl : UserControl
    {
        public ReadyCtrl()
        {
            InitializeComponent();
        }

        private void OrderStartBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.UIStateManager.Get(UICategory.ORDER);

            if (uc != null)
            {
                App.UIStateManager.Push(uc);
            }
        }

        private void AdminBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.UIStateManager.Get(UICategory.ADMIN);

            if (uc != null)
            {
                App.UIStateManager.Push(uc);
            }
        }
    }
}
