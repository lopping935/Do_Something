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
   /* class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("程序启动");
            SocketClient.CreateConnect();
            while (true)
            {
              string inputstring=  Console.ReadLine();
                if(inputstring.Length >0)
                {
                    SocketClient.senddata(inputstring);
                }   
            }
        }

    }*/
    public class SocketClient
    {
       static  Socket socketClient;
        public void CreateConnect(string IPA,int port)
        {
            //创建负责通信的Socket
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(IPA);
            IPEndPoint iep = new IPEndPoint(ip, port);
            socketClient.Connect(iep);
            //调用客户端的接收程序
            ThreadPool.QueueUserWorkItem(new WaitCallback(Recv), socketClient);
            //Console.WriteLine("127.0.0.1:3010 连接成功");
        }

        public static void senddata(string data)
        {
            try
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(data);
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
            Socket connect = SocketClient as Socket;

            while (true)
            {
                //创建一个内存缓冲区，其大小为1024*1024字节  即1M     
                byte[] arrServerRecMsg = new byte[1024];
                try
                {
                    //将接收到的信息存入到内存缓冲区，并返回其字节数组的长度   
                    int length = connect.Receive(arrServerRecMsg);
                    if (length > 0)
                    {
                        byte[] buffer = new byte[length];
                        Array.Copy(arrServerRecMsg, buffer, length);
                        //Console.WriteLine("Client接收到服务器返回信息，信息如下：\r\n客户端：{0} ",  Encoding.UTF8.GetString(buffer));
                      
                    }
                }
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                }

            }
        }
    }
  
}
