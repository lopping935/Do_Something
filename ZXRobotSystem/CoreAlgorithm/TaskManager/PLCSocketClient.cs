using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using log4net;
using SQLPublicClass;
using System.Reflection;

namespace CoreAlgorithm.TaskManager
{
    public class PLCSocketClient
    {
       static  Socket socketClient;
       static TasksManager tm;
        public  void CreateConnect(string IPA,int port)
        {
            //创建负责通信的Socket
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(IPA);
            IPEndPoint iep = new IPEndPoint(ip, port);
            tm = new TasksManager();
            socketClient.Connect(iep);
            //调用客户端的接收程序
            ThreadPool.QueueUserWorkItem(new WaitCallback(Recv), socketClient);
            //Console.WriteLine("127.0.0.1:3010 连接成功");
        }

        public static void senddata(byte[] buffer)
        {
            try
            {
                //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(data);
                //buffer[0] = 49;
                socketClient.Send(buffer);
            }
            catch(Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
            }
        }
        public static void Recv(object SocketClient)
        {
            
        }
    }
  
}
