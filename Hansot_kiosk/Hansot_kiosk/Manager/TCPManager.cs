using Hansot_kiosk.Control;
using Hansot_kiosk.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public bool isSend = false;
        private byte[] sendData = new byte[MAX_LEN];
        private byte[] receiveData = new byte[MAX_LEN];
        private byte[] response = new byte[MAX_LEN];
        public string getResponse = string.Empty;

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
                tcpClient = new TcpClient();
                var result = tcpClient.BeginConnect(ServerIP.serverIP, ServerIP.port, null, null);
                result.AsyncWaitHandle.WaitOne(3000, true);

                networkStream = tcpClient.GetStream();
                networkStream.Write(sendData, 0, sendData.Length);

                tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                int readData = networkStream.Read(this.response, 0, this.response.Length);
                getResponse = Encoding.UTF8.GetString(response, 0, readData);

                if (getResponse == "200")
                {
                    this.isConnection = true;
                }
                return getResponse;
            }
            catch (Exception e)
            {
                this.isConnection = false;
                MessageBox.Show("서버와 연결이 끊겨있습니다.");
            }

            return null;
        }

        public async void GetMessage()
        {
            try
            {
                NetworkStream stream = tcpClient.GetStream();
                string response = "";
                int data = 0;

                while (true)
                {
                    try
                    {
                        isSend = false;
                        data = await stream.ReadAsync(receiveData, 0, receiveData.Length);
                        response = Encoding.UTF8.GetString(receiveData, 0, data);

                        if (!isSend)
                        {
                            MessageBox.Show(response);
                            if (response.IndexOf("총매출액") > -1)
                            {
                                StatisticCtrl statisticCtrl = (StatisticCtrl)App.UIStateManager.Get(Common.UICategory.STATISTIC);
                                
                                string totalPrice = "오늘까지의 총매출액은 " + statisticCtrl.TotalStatisticCtrl.TotalPrice + "원 입니다.";

                                TcpModel tcpModel = new TcpModel()
                                {
                                    MSGType = 1,
                                    Content = totalPrice
                                };

                                PostMessage(tcpModel);
                                break;
                            }
                        }
                        else
                        {
                            isConnection = false;
                            MessageBox.Show("Server Connection Failed");
                            tcpClient.Close();
                            networkStream.Close();
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Server Connection Failed");
                        isConnection = false;
                        break;
                    }
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public void threadStart()
        {
            if (this.isConnection)
            {
                this.tcpThread = new Thread(new ThreadStart(GetMessage));
                tcpThread.Start();
            }
        }

        public void threadEnd()
        {
            if (tcpThread != null)
            {
                tcpThread.Abort();
            }
        }
    }
}
