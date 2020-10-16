using Kiosk.UIManager;
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

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.uIStateManager.Get(UICategory.ORDER);
            if(uc != null)
                App.uIStateManager.Push(uc);
        }
    }
}
