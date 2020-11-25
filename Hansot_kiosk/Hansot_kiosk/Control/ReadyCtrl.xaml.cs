using Hansot_kiosk.Model;
using Hansot_kiosk.view;
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
            UserControl uc = App.UIStateManager.Get(UICategory.ORDER);
            if(uc != null)
                App.UIStateManager.Push(uc);
        }

        private void ReadyControl_Loaded(object sender, RoutedEventArgs e)
        {
            TcpModel tcpModel = new TcpModel()
            {
                MSGType = 0,
            };

            if (Properties.Settings.Default.isAutoLogin == true)
            {
                App.isLogined = true;
                //Properties.Settings.Default.isAutoLogin = false;
                //Properties.Settings.Default.Save();
                App.TcpManager.PostMessage(tcpModel);
                MessageBox.Show("자동로그인 되었습니다.");
            }else if (App.isLogined == false)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
            else
            {
                MessageBox.Show("이미 로그인되어있습니다.");
            }
        }
    }
}
