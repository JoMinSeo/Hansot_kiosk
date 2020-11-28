using Hansot_kiosk.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// MessageWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MessageWindow : Window
    {
        TcpModel tcpModel = new TcpModel()
        {
            MSGType = 1
        };

        public MessageWindow()
        {
            InitializeComponent();
        }

        private void checkGroup_Checked(object sender, RoutedEventArgs e)
        {
            tcpModel.Group = true;
        }

        private void checkGroup_Unchecked(object sender, RoutedEventArgs e)
        {
            tcpModel.Group = false;
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(messageTB.Text))
            {
                MessageBox.Show("메세지를 입력하여주세요.");
            }
            else
            {
                tcpModel.Content = messageTB.Text;
                App.TcpManager.PostMessage(tcpModel);
            }
        }

    }
}
