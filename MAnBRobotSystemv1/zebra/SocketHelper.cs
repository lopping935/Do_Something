using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
//using log4net;
//using SQLPublicClass;
using System.Reflection;

namespace SocketHelper
{    
    public class SocketClient
    {
        Thread threadClient = null; // 创建用于接收服务端消息的 线程；
        Socket socketClient = null;        
        public byte[] arrServerRecMsg=new byte[1024 * 1024 * 2];

        public int CreateConnect(string IPA, int port)
        {          
            //创建负责通信的Socket
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(IPA);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            threadClient = new Thread(RecMsg);            
            string a=null;
            try
            {
                ShowMsg("与服务器连接中……");
                socketClient.Connect(endPoint);              
                threadClient.Start();
                ShowMsg("与服务器连接成功！！！");
                return 1;
            }
            catch (SocketException se)
            {
                ShowMsg(se.Message);
                return -1;
            }
           
        }

        public  void SendData(string date)
        {
            if (string.IsNullOrEmpty(date))
            { 
                ShowMsg("字符为空");
                return;
            }
            try
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(date);
                socketClient.Send(buffer);
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
                // log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                // Log.addLog(log, LogType.ERROR, ex.Message);
            }
        }
      
        public void RecMsg()
        {            
            while (true)
            {
                //创建一个内存缓冲区，其大小为1024*1024字节  即1M     
                
                byte[] arrMsgRec = new byte[1024 * 1024 * 2];
                int length = -1;
                try
                {
                    length = socketClient.Receive(arrMsgRec); // 接收数据，并返回数据的长度；
                }
                catch (SocketException se)
                {
                    ShowMsg("异常；" + se.Message);
                    return;
                }
                catch (Exception e)
                {
                    ShowMsg("异常：" + e.Message);
                    return;
                }
                if (length>0)
                {
                    Buffer.BlockCopy(arrMsgRec,0,arrServerRecMsg,0,length);
                }
            }
        }
        void ShowMsg(object str)
        {
            Console.WriteLine(str + "\r\n");
            //txtMsg.AppendText(str + "\r\n");
        }
    }
    public class SocketServer
    {
        string Sendmsg = null;
        IPEndPoint endPoint;
        Thread threadWatch = null; // 负责监听客户端连接请求的 线程；
        Socket socketWatch = null;

        Dictionary<string, Socket> dict = new Dictionary<string, Socket>();
        Dictionary<string, Thread> dictThread = new Dictionary<string, Thread>();

        public SocketServer(string host,int port)
        {
            System.Console.WriteLine("server");             
            IPAddress ip = IPAddress.Parse(host);
            endPoint = new IPEndPoint(ip, port);
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);                 
        }
        public void StarServer()
        {
            try
            {
                // 将负责监听的套接字绑定到唯一的ip和端口上；
                socketWatch.Bind(endPoint);
            }
            catch (SocketException se)
            {
                ShowMsg("异常：" + se.Message);
                return;
            }
            // 设置监听队列的长度；
            socketWatch.Listen(10);
            // 创建负责监听的线程；
            threadWatch = new Thread(WatchConnecting);
            threadWatch.IsBackground = true;
            threadWatch.Start();
            ShowMsg("服务器启动监听成功！");
        }
        void WatchConnecting()
        {
            while (true)  // 持续不断的监听客户端的连接请求；
            {
                // 开始监听客户端连接请求，Accept方法会阻断当前的线程；
                Socket sokConnection = socketWatch.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；
                // 想列表控件中添加客户端的IP信息；
                ShowMsg(sokConnection.RemoteEndPoint.ToString());
                // 将与客户端连接的 套接字 对象添加到集合中；
                dict.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);
                ShowMsg("客户端连接成功！");
                Thread thr = new Thread(RecMsg);
                thr.IsBackground = true;
                thr.Start(sokConnection);
                dictThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);  //  将新建的线程 添加 到线程的集合中去。
                //我认为这里线程集合，在.net中应该有对象的线程管理工具类，其在设计上应该远远好于我们自己定义的director
            }
        }
        void RecMsg(object sokConnectionparn)
        {
            Socket sokClient = sokConnectionparn as Socket;
            while (true)
            {
                // 定义一个2M的缓存区；
                byte[] arrMsgRec = new byte[1024 * 1024 * 2];
                // 将接受到的数据存入到输入  arrMsgRec中；
                int length = -1;
                try
                {
                    length = sokClient.Receive(arrMsgRec); // 接收数据，并返回数据的长度；
                }
                catch (SocketException se)
                {
                    ShowMsg("异常：" + se.Message);
                    // 从 通信套接字 集合中删除被中断连接的通信套接字；
                    dict.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从通信线程集合中删除被中断连接的通信线程对象；这样能杀死后台的线程吗？
                    dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从列表中移除被中断的连接IP
                    ShowMsg(sokClient.RemoteEndPoint.ToString());
                    break;
                }
                catch (Exception e)
                {
                    ShowMsg("异常：" + e.Message);
                    // 从 通信套接字 集合中删除被中断连接的通信套接字；
                    dict.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从通信线程集合中删除被中断连接的通信线程对象；
                    dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从列表中移除被中断的连接IP
                    ShowMsg(sokClient.RemoteEndPoint.ToString());
                    break;
                }
                if (arrMsgRec[0] == 0)  // 表示接收到的是数据；
                {
                    string strMsg = System.Text.Encoding.UTF8.GetString(arrMsgRec, 1, length - 1);// 将接受到的字节数据转化成字符串；
                    ShowMsg(strMsg);
                }
               
            }
        }
        private void SendToSomeone(string Sendmsg,string ClientEndprot)
        {
            //构造数据
            if(string.IsNullOrEmpty(Sendmsg)|| string.IsNullOrEmpty(ClientEndprot))
            {
                ShowMsg("message or the client is null");
                return;
     
            }
            string strMsg = "服务器" + "\r\n" + "   -->" + Sendmsg.Trim() + "\r\n";
            byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg); // 将要发送的字符串转换成Utf-8字节数组；
            byte[] arrSendMsg = new byte[arrMsg.Length + 1];
            arrSendMsg[0] = 0; // 表示发送的是消息数据
            Buffer.BlockCopy(arrMsg, 0, arrSendMsg, 1, arrMsg.Length);
            //根据ClientEndprot发送数据      
            dict[ClientEndprot].Send(arrSendMsg);// 解决了 sokConnection是局部变量，不能再本函数中引用的问题；
            ShowMsg(strMsg);     
        }

        private void SendToAll(string Sendmsg)
        {
            
            if (string.IsNullOrEmpty(Sendmsg))
            {
                ShowMsg("message or the client is null");
                return;

            }
            //构造数据
            string strMsg = "服务器" + "\r\n" + "   -->" + Sendmsg.Trim() + "\r\n";
            byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg); // 将要发送的字符串转换成Utf-8字节数组；

            byte[] arrSendMsg = new byte[arrMsg.Length + 1]; // 上次写的时候把这一段给弄掉了，实在是抱歉哈~ 用来标识发送是数据而不是文件，如果没有这一段的客户端就接收不到消息了~~~
            arrSendMsg[0] = 0; // 表示发送的是消息数据
            Buffer.BlockCopy(arrMsg, 0, arrSendMsg, 1, arrMsg.Length);
            //对所有客户发送数据
            foreach (Socket s in dict.Values)
            {
                s.Send(arrMsg);
            }
            ShowMsg(strMsg);
            ShowMsg(" 群发完毕～～～");
        }

        void ShowMsg(string str)
        {
            Console.WriteLine(str + "\r\n");
        }
        
    }
}