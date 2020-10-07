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
