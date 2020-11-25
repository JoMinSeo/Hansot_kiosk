using Hansot_kiosk.Common;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        public delegate void InitDel();
        public InitDel DeleGate;
        public MainWindow()
        {
            DeleGate = new InitDel(this.init);

            InitializeComponent();
            initUI();
            StartTimer();

            this.DataContext = this;

            DeleGate();
        }
        private void init()
        {
            if (App.OrderManager.OrderedMenus.Any())
            {
                if (App.UIStateManager.UIStack.Peek() != App.UIStateManager.Get(UICategory.COMPLETE))
                {
                    if (MessageBoxResult.No == MessageBox.Show("주문이 초기화 됩니다. 괜찮으십니까?",
                        "메인화면으로 가기", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                    {
                        return;
                    }
                }
            }
            App.UIStateManager.AllPop();
            App.SQLManager.Init();
            App.OrderManager.Init();
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
            App.UIStateManager.Set(UICategory.READY, readyCtrl);
            App.UIStateManager.Set(UICategory.ORDER, orderCtrl);
            App.UIStateManager.Set(UICategory.PLACE, placeCtrl);
            App.UIStateManager.Set(UICategory.SEATSELECT, seatSelectCtrl);
            App.UIStateManager.Set(UICategory.PAYSELECT, paySelectCtrl);
            App.UIStateManager.Set(UICategory.PAYCREDIT, payCreditCtrl);
            App.UIStateManager.Set(UICategory.PAYCASH, payCashCtrl);
            App.UIStateManager.Set(UICategory.COMPLETE, completeCtrl);
            App.UIStateManager.Set(UICategory.ADMIN, adminCtrl);

            //chris - add user control. please~!!

            App.UIStateManager.Push(readyCtrl);
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleGate();
        }
        #endregion
        #region PropertyChangedEvent
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propetryName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propetryName));
        }
        #endregion
        private void MainWindow_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.F2)
            {
                if(App.UIStateManager.UIStack.Peek() == App.UIStateManager.Get(UICategory.READY))
                {
                    UserControl uc = App.UIStateManager.Get(UICategory.ADMIN);
                    if (uc != null)
                    {
                        App.UIStateManager.Push(uc);
                    }
                }
            }
        }
    }
}
