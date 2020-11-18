using Hansot_kiosk.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hansot_kiosk.Manager
{
    public class TCPManager
    {
        private const int MAX_LEN = 4096;
        private bool isSend = false;
        private byte[] sendData = new byte[MAX_LEN];
        private byte[] receiveData = new byte[MAX_LEN];

        NetworkStream networkStream = null;
        TcpClient tcpClient = null;

        public string PostMessage(TcpModel tcpModel)
        {
            string json = JsonConvert.SerializeObject(tcpModel);
            this.sendData = Encoding.UTF8.GetBytes(json);

            try
            {
                if(tcpClient == null) // 연결이 안되어 있을 경우
                {
                    tcpClient = new TcpClient(ServerIP.serverIP, ServerIP.port);
                    networkStream = tcpClient.GetStream();
                }

                networkStream.Write(sendData, 0, sendData.Length);
                byte[] response = new byte[MAX_LEN];
                Int32 readData = networkStream.Read(response, 0, response.Length);

                String getResponse = Encoding.UTF8.GetString(response, 0, readData);
                tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                return getResponse;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return null;
        }
    }
}
