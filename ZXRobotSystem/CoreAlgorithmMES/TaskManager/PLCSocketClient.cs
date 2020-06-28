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
        public static void CreateConnect(string IPA,int port)
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
            Socket connect = SocketClient as Socket;

            while (true)
            {
                Thread.Sleep(1);
                //创建一个内存缓冲区，其大小为1024*1024字节  即1M     
                byte[] arrServerRecMsg = new byte[1024];
                string sql = "";
                try
                {
                    //将接收到的信息存入到内存缓冲区，并返回其字节数组的长度   
                    int length = connect.Receive(arrServerRecMsg);
                    if (length > 0)
                    {
                        byte[] buffer = new byte[length];
                        Array.Copy(arrServerRecMsg, buffer, length); 
                        Int32 SINFinish = BitConverter.ToInt32(buffer.Skip(138).Take(4).ToArray(), 0);
                        //sqlTempUP.Add("UPDATE [EquipmentAttributeValuesRT] SET [AttributeValues_Time]='{1}',[AttributeValues_Value]='{2}' WHERE [EquipmentAttribute_id]={0}");
                        if ((buffer[240] & Convert.ToSByte(2)) != 0)
                        {
                            sql = string.Format("UPDATE TV_D_DETAIL_B14 SET REC_IMP_TIME='{0}',IMP_FINISH=1 WHERE iface_id={1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), SINFinish);
                            tm.MultithreadExecuteNonQuery(sql);
                        }
                        if ((buffer[240] & Convert.ToSByte(1))!=0)
                            Program.ShootA = 1;
                        //Console.WriteLine("Client接收到服务器返回信息，信息如下：\r\n客户端：{0} ",  Encoding.UTF8.GetString(buffer));
                        string str = SINFinish.ToString()+" "+ System.Convert.ToString(buffer[240], 2); ;
                        //sqlTemp.Add("INSERT INTO [EquipmentAttributeValues]([EquipmentAttribute_id],[AttributeValues_Time],[AttributeValues_Value]) VALUES ({0},'{1}','{2}')");
                        sql = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,RECV_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                        tm.MultithreadExecuteNonQuery(sql);
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
