using Kiosk.UIManager;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Hansot_kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DateTime _currentDateTime = DateTime.Now;
        public DateTime CurrentDateTime
        {
            get => _currentDateTime;
            set
            {
                _currentDateTime = value;
                OnPropertyChanged(nameof(CurrentDateTime));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            initUI();
            StartTimer();

            this.DataContext = this;
        }
        public void Init()
        {
            App.uIStateManager.AllPop();
            App.orderManager.Init();
            App.sQLManager.Init();
            orderCtrl.init();
            seatSelectCtrl.init();
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
            CurrentDateTime = DateTime.Now;
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
            App.uIStateManager.Set(UICategory.ADMIN, adminCtrl);

            //chris - add user control. please~!!

            App.uIStateManager.Push(readyCtrl);
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.orderManager.OrderedMenus.Any())
            {
                if (App.orderManager.CurrentOrder.OrderedTime.CompareTo(default(DateTime)) == 0)
                {
                    if (MessageBoxResult.Yes == MessageBox.Show("주문이 초기화 됩니다. 괜찮으십니까?",
                "메인화면으로 가기", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                    {
                        Init();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            Init();
        }
        #endregion
        #region PropertyChangedEvent
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propetryName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propetryName));
        }
        #endregion

    }
}
