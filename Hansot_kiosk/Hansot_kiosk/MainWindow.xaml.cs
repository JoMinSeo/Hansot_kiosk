using Hansot_kiosk.Common;
using Hansot_kiosk.Manager;
using Hansot_kiosk.Model;
using Hansot_kiosk.view;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Hansot_kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        readonly BrushConverter converter = new BrushConverter();
        Brush red = null;
        Brush green = null;

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

            App.InitDeleGate();
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
            App.InitDeleGate();
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
            Properties.Settings.Default.totalProgramTime++;
            Properties.Settings.Default.Save();
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
            App.UIStateManager.Set(UICategory.STATISTIC, statisticCtrl);

            //chris - add user control. please~!!

            App.UIStateManager.Push(readyCtrl);
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            init();
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
            if (e.Key == System.Windows.Input.Key.F2)
            {
                if (App.UIStateManager.UIStack.Peek() == App.UIStateManager.Get(UICategory.READY))
                {
                    UserControl uc = App.UIStateManager.Get(UICategory.ADMIN);
                    if (uc != null)
                    {
                        App.UIStateManager.Push(uc);
                    }
                }
            }
        }

        private void messageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow messageWindow = new MessageWindow();
            messageWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            red = (Brush)converter.ConvertFromString("#B83226");
            green = (Brush)converter.ConvertFromString("#50915B");

            TcpModel tcpModel = new TcpModel()
            {
                MSGType = 0,
            };

            if (Properties.Settings.Default.isAutoLogin == true)
            {
                App.isLogined = true;
                App.TCPManager.PostMessage(tcpModel);
                App.TCPManager.ThreadStart();
                connectedTime.Text = App.TCPManager.isConnection ? "최근 접속 시간: " + DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초") : "";
                MessageBox.Show("자동로그인 되었습니다.");
            }
            else if (App.isLogined == false)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
            else
            {
                MessageBox.Show("이미 로그인되어있습니다.");
            }

            connectionThreadRun();
        }

        private void connectionThreadRun()
        {
            Thread networkThread = new Thread(new ThreadStart(connectionStateObserver))
            {
                IsBackground = true
            };

            networkThread.Start();
        }

        private void connectionStateObserver()
        {
            while (true)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    serverConnected.Text = App.TCPManager.isConnection ? "서버와 연결 되어있습니다." : "서버와 연결 되어있지 않습니다.";
                    connectionBtn.IsEnabled = !App.TCPManager.isConnection;
                    serverConnectionPanel.Background = App.TCPManager.isConnection ? green : red;
                }));
            }
        }

        private void reConnectConnection(object sender, RoutedEventArgs e)
        {
            TcpModel tcpModel = new TcpModel()
            {
                MSGType = 0

            };

            string response = App.TCPManager.PostMessage(tcpModel);

            if (response == "200")
            {
                App.TCPManager.ThreadStart();
                App.TCPManager.isConnection = true;
                connectedTime.Text = App.TCPManager.isConnection ? "최근 접속 시간: " + DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초") : "";
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            App.TCPManager.ThreadClose();
        }
    }
}
