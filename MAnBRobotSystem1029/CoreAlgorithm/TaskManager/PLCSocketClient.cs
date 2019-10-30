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
            
        }
        public static bool IsSocketConnected(Socket socket)
        {
            //Socket socket = socketClient;
            bool connectState = true;
            bool blockingState = socket.Blocking;
            try
            {
                byte[] tmp = new byte[1];

                socket.Blocking = false;
                socket.Send(tmp, 0, 0);
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
                socket.Blocking = blockingState;
            }
            return connectState;
        }
        public static void senddata(byte[] buffer)
        {           
                socketClient.Send(buffer);           
        }
        public static void Recv(object SocketClient)
        {
            
        }
    }
  
}
