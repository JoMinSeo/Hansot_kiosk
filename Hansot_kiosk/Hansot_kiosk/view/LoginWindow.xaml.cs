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
using System.Windows.Shapes;

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

            if(Properties.Settings.Default.id == id && Properties.Settings.Default.pass == pass)
            {
                if(autoLoginCheck.IsChecked == true)
                {
                    Properties.Settings.Default.isAutoLogin = true;
                    Properties.Settings.Default.Save();
                }
                App.isLogined = true;
                MessageBox.Show("로그인되었습니다.");

                this.Close();
            }
            else
            {
                MessageBox.Show("로그인 실패");
            }
        }
    }
}
