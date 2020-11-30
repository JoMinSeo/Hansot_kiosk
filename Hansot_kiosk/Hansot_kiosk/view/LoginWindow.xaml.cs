using Hansot_kiosk.Model;
using System.Windows;

namespace Hansot_kiosk.view
{
    /// <summary>
    /// LoginWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            string id = idTb.Text;
            string pass = passTb.Text;

            TcpModel tcpModel = new TcpModel()
            {
                MSGType = 0,
            };

            if (Properties.Settings.Default.id == id && Properties.Settings.Default.pass == pass)
            {
                if(autoLoginCheck.IsChecked == true)
                {
                    Properties.Settings.Default.isAutoLogin = true;
                    Properties.Settings.Default.Save();
                }
                App.isLogined = true;
                App.TCPManager.PostMessage(tcpModel);
                MessageBox.Show("로그인되었습니다.");
                App.TCPManager.ThreadStart();
                this.Close();
            }
            else
            {
                MessageBox.Show("로그인 실패");
            }
        }
    }
}
