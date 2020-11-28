using Hansot_kiosk.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        byte[] response = new byte[MAX_LEN];

        NetworkStream networkStream = null;
        TcpClient tcpClient = null;

        private Thread tcpThread = null;
        public bool isConnection = false;

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
                tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                this.isConnection = true;

                Int32 readData = networkStream.Read(this.response, 0, this.response.Length);
                string getResponse = Encoding.UTF8.GetString(response, 0, readData);
                return getResponse;
            }
            catch(Exception e)
            {
                this.isConnection = false;
                MessageBox.Show("서버와 연결이 끊겨있습니다.");
            }

            return null;
        }

        //public void threadStart()
        //{
        //    if (this.isConnection)
        //    {
        //        this.tcpThread = new Thread(new ThreadStart(receiveMessage));
        //        tcpThread.Start();
        //    }
        //}

        //public void threadEnd()
        //{
        //    if (tcpThread != null)
        //    {
        //        tcpThread.Abort();
        //    }
        //}
    }
}
