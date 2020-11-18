using Kiosk.UIManager;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Hansot_kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private void init()
        {
            App.uIStateManager.AllPop();
            App.orderManager.init();
            orderCtrl.init();
        }
        public MainWindow()
        {
            InitializeComponent();
            initUI();
            StartTimer();
        }
        #region TimeControl
        private void StartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            dClock.Content = String.Format("{0:yyyy년 MM월 dd일 tt hh시 mm분 ss초}", DateTime.Now);
        }
        #endregion

        #region UIControl
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
            if (App.orderManager.OrderedMenus.Any())
            {
                if (MessageBoxResult.Yes == MessageBox.Show("주문이 초기화 됩니다. 괜찮으십니까?",
                "메인화면으로 가기", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    init();
                }
                return;
            }
            init();
        }
        #endregion
    }
}
