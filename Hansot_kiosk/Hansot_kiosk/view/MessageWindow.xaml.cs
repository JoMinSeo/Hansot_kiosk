using Hansot_kiosk.Model;
using System.Windows;

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
                App.TCPManager.PostMessage(tcpModel);
                App.TCPManager.IsSend = true;
            }
        }
    }
}
