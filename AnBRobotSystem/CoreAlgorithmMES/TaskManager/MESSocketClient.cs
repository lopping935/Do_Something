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
    public class MESSocketClient
    {
        public static Socket socketClient;
        public static void CreateConnect(string IPA,int port)
        {
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(IPA);
            IPEndPoint iep = new IPEndPoint(ip, port);
            socketClient.Connect(iep);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Recv), socketClient);

        }
        public static bool IsSocketConnected()
        {
            Socket socket = socketClient;
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
            try
            {
                while (true)
            {
                Thread.Sleep(1000);
                byte[] arrServerRecMsg = new byte[1024];
                int length = socketClient.Receive(arrServerRecMsg);
                if (length > 0)
                {
                    byte[] buffer = new byte[length];
                    Array.Copy(arrServerRecMsg, buffer, length);
                }
            }
            }
            catch(Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
                Log.addLog(log, LogType.ERROR, ex.StackTrace);
            }
        }
    }
  
}
