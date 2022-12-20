
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Vision_Utlis
{
    /// <summary>
    /// sockethelper
    /// </summary>
    ///  
    
public class SocketClient
    {
        private Queue<double> dataQueue = new Queue<double>(100);
        private Thread threadClient = null;
        public Socket socketClient = null;
        public ThreadStart RecvMSG = null;
        public SocketClient(ThreadStart RecvMSG1)
        {
            RecvMSG = RecvMSG1;
        }
        public void CreateConnect(string IPA, int port)
        {
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(IPA);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            threadClient = new Thread(RecvMSG);
            threadClient.IsBackground = true;
            socketClient.Connect(endPoint);
            threadClient.Start();
        }
        public bool IsSocketConnected()
        {
            #region 过程
            bool connectState = true;
            bool blockingState = socketClient.Blocking;
            try
            {
                byte[] tmp = new byte[1];

                socketClient.Blocking = false;
                socketClient.Send(tmp, 0, 0);
                connectState = true; 
            }
            catch (SocketException e)
            {
                if (e.NativeErrorCode.Equals(10035))
                {
                    connectState = true;
                }

                else
                {
                    connectState = false;
                }
            }
            finally
            {
                socketClient.Blocking = blockingState;
            }

            return connectState;
            #endregion
        }
        public void SendData(byte[] buffer)
        {
            socketClient.Send(buffer);
        }
      /*  //重连代码
       *  public void ReconnectMES(SocketClient s)
        {
            string sql = null, str = null;
            bool connectstate = false;
            do
            {

                try
                {


                    s.socketClient.Close();
                    s.socketClient.Dispose();
                    // Client_MES = new SocketClient(ClientREC_MES);
                    s.CreateConnect(MESIP, mesportr);

                    connectstate = true;
                    sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=14", 1, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));
                    tm.MultithreadExecuteNonQuery(sql);

                }
                catch
                {
                    sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=14", 0, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));
                    tm.MultithreadExecuteNonQuery(sql);
                    str = "连接MES系统失败，正在尝试重新连接！";
                    sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                    tm.MultithreadExecuteNonQuery(sql);
                    connectstate = false;
                    Thread.Sleep(5000);
                }
            }
            while (!connectstate);
        }*/
    }
    public class SocketServer
    {

        public  IPEndPoint endPoint;
        public Thread threadWatch = null; //负责监听客户端连接请求的线程；
        public Socket socketWatch = null;
        public ParameterizedThreadStart RecvMSG = null;
        public Dictionary<string, Socket> dict = new Dictionary<string, Socket>();
        public Dictionary<string, Thread> dictThread = new Dictionary<string, Thread>();
        //public Socket sokConnection = null;
        public SocketServer(string host, int port)
        {
            System.Console.WriteLine("server");
            IPAddress ip = IPAddress.Parse(host);
            endPoint = new IPEndPoint(ip, port);
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
        }
        public void StarServer(ParameterizedThreadStart ServerRecvMSG)
        {
            RecvMSG = ServerRecvMSG;
            socketWatch.Bind(endPoint);
            // 设置监听队列的长度；
            socketWatch.Listen(10);
            // 创建负责监听的线程；
            threadWatch = new Thread(WatchConnecting);
            threadWatch.IsBackground = true;
            threadWatch.Start();
        }
        public void WatchConnecting()
        {
            while (true)  // 持续不断的监听客户端的连接请求；
            {
                Socket sokConnection = socketWatch.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；
                dict.Clear();
                dict.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);                
                Thread thr = new Thread(RecvMSG);
                thr.IsBackground = true;
                thr.Start(sokConnection);
            }
        }      
        public void SendToSomeone(byte[] buffer, string ClientEndprot)
        {
            if (buffer.Length <= 0 || string.IsNullOrEmpty(ClientEndprot))
            {
                return;
            }
            dict[ClientEndprot].Send(buffer);
        }

        public void SendToAll(byte[] buffer)
        {

            if (buffer.Length <= 0)
            {
                return;
            }
            foreach (Socket s in dict.Values)
            {
                s.Send(buffer);
            }
        }

     
   

    }
}
