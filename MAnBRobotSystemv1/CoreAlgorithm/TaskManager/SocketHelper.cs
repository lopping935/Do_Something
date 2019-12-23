
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace SocketHelper
{
    /// <summary>
    /// sockethelper
    /// </summary>
    public class SocketClient
    {
        private Thread threadClient = null;
        public Socket socketClient = null;
        public ThreadStart RecvMSG = null;
        public SocketClient(ThreadStart RecvMSG1)
        {
            RecvMSG = RecvMSG1;
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void CreateConnect(string IPA, int port)
        {
            IPAddress ip = IPAddress.Parse(IPA);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            threadClient = new Thread(RecvMSG);
            socketClient.Connect(endPoint);
            threadClient.Start();
        }
        public bool IsSocketConnected()
        {
            #region remarks
            /********************************************************************************************
             * 当Socket.Conneted为false时， 如果您需要确定连接的当前状态，请进行非阻塞、零字节的 Send 调用。
             * 如果该调用成功返回或引发 WAEWOULDBLOCK 错误代码 (10035)，则该套接字仍然处于连接状态； 
             * 否则，该套接字不再处于连接状态。
             * Depending on http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.connected.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2
            ********************************************************************************************/
            #endregion
            #region 过程
            bool connectState = true;
            bool blockingState = socketClient.Blocking;
            try
            {
                byte[] tmp = new byte[1];

                socketClient.Blocking = false;
                socketClient.Send(tmp, 0, 0);
                connectState = true; //若Send错误会跳去执行catch体，而不会执行其try体里其之后的代码
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
    }
    public class SocketServer
    {

        public  IPEndPoint endPoint;
        public Thread threadWatch = null; //负责监听客户端连接请求的线程；
        public Socket socketWatch = null;
        public ThreadStart RecvMSG = null;
        public Dictionary<string, Socket> dict = new Dictionary<string, Socket>();
        public Dictionary<string, Thread> dictThread = new Dictionary<string, Thread>();

        public SocketServer(string host, int port)
        {
            System.Console.WriteLine("server");
            IPAddress ip = IPAddress.Parse(host);
            endPoint = new IPEndPoint(ip, port);
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
        }
        public void StarServer(ThreadStart ServerRecvMSG)
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
                // 开始监听客户端连接请求，Accept方法会阻断当前的线程；
                Socket sokConnection = socketWatch.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；
                // 将与客户端连接的 套接字 对象添加到集合中；
                dict.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);
                Thread thr = new Thread(RecvMSG);
                thr.IsBackground = true;
                thr.Start(sokConnection);
                dictThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);  //  将新建的线程 添加 到线程的集合中去。
                //我认为这里线程集合，在.net中应该有对象的线程管理工具类，其在设计上应该远远好于我们自己定义的director
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
