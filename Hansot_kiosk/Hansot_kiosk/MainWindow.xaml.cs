using Hansot_kiosk.Control;
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
using System.Windows.Threading;

namespace Hansot_kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            initUI();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            dClock.Text = DateTime.Now.ToString();
        }

        private void initUI()
        {
            App.uIStateManager.Set(UICategory.HOME, readyCtrl);
            App.uIStateManager.Set(UICategory.ORDER, orderCtrl);
            App.uIStateManager.Set(UICategory.PLACE, placeCtrl);
            App.uIStateManager.Set(UICategory.SEATSELECT, seatSelectCtrl);
            App.uIStateManager.Set(UICategory.PAYSELECT, paySelectCtrl);
            App.uIStateManager.Set(UICategory.PAYCREDIT, payCreditCtrl);
            App.uIStateManager.Set(UICategory.PAYCASH, payCashCtrl);
            App.uIStateManager.Set(UICategory.COMPLETE, completeCtrl);

            //chris - add user control. please~!!

            App.uIStateManager.Push(readyCtrl);
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            App.uIStateManager.AllPop();
        }
    }
}
