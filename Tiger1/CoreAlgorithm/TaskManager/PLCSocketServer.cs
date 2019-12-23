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
using System.Data.Common;
namespace CoreAlgorithm.TaskManager
{
    class PLCSocketServer
    {
        static Socket socketServer;
        static Socket socketWatch;
        static TasksManager tm;
        public void CreateSocket(string plcip, int port)
        {
            //当点击开始监听的时候 在服务器端创建一个负责监听IP地址跟端口号的Socket
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //创建端口号对象
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(plcip), port);
            //监听
            socketWatch.Bind(iep);
            tm = new TasksManager();
            socketWatch.Listen(10);//队列排队
            Console.WriteLine("监听Socket创建完成，准备进入监听程序。");
            Thread receiveThread = new Thread(ListenRecall);
            receiveThread.Start();
        }
         private void ListenRecall()
        {
            //等待客户端的链接并创建一个负责通信的Socket
            while (true)
            {
                if (Program.MessageStop == 1)
                    break;
                Thread.Sleep(1000);
                try
                {
                    socketServer = socketWatch.Accept();
                    #region 方法一：启用带参数的线程接收数据
                    Console.WriteLine(socketServer.RemoteEndPoint.ToString() + ":连接成功");
                    //开启一个新线程不停的接受客户端发过来的数据
                    //启动线程池里得一个线程(队列的方式，如线程池暂时没空闲线程，则进入队列排队)
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Recv), socketServer);
                    #endregion
                    #region 方法二：启用带参数的线程接收数据
                    //ParameterizedThreadStart pts = new ParameterizedThreadStart(Recv);
                    //Thread thread = new Thread(pts)
                    //{
                    //    IsBackground = true
                    //};
                    //thread.Start(socketServer);
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }
            }
        }

        public static void Recv(object SocketClient)
        {
            Socket connect = SocketClient as Socket;            
            while (true)
            {
                if (Program.MessageStop == 1)
                    break;
                Thread.Sleep(1000);
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
                        Program.MessageFlg = BitConverter.ToInt16(buffer.Skip(0).Take(2).ToArray(), 0);
                        lock (Program.gllock)
                        {
                            string sql = "";
                            if (Program.MessageFlg ==3)//虎踞成功
                            {                                
                                string ID_LOT_PROD = Encoding.Default.GetString(buffer.Skip(6).Take(9).ToArray());
                                Int16 ID_PART_LOT = BitConverter.ToInt16(buffer.Skip(15).Take(2).ToArray(), 0);
                                double REC_ID = 0;

                                sql = string.Format("select REC_ID from TLabelContent where ID_LOT_PROD='{0}' and ID_PART_LOT={1}", ID_LOT_PROD, ID_PART_LOT);
                                DbDataReader dr = tm.MultithreadDataReader(sql);
                                while (dr.Read())
                                {
                                    if (dr["REC_ID"] != DBNull.Value)
                                        REC_ID = Convert.ToDouble(dr["REC_ID"]);
                                }
                                dr.Close();                              
                                sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID={2}", REC_ID, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), 12);//更新虎踞打捆成功的RECID
                                tm.MultithreadExecuteNonQuery(sql);
                                sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID={2}", 0, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), 11);//更新虎踞打捆成功标志
                                tm.MultithreadExecuteNonQuery(sql);

                                string str = "二级收到：虎踞打捆成功" +ID_LOT_PROD + " " + ID_PART_LOT.ToString();
                                sql = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                                tm.MultithreadExecuteNonQuery(sql);
                            }
                            else if (Program.MessageFlg == 2)//接收到mes数据
                            {
                                string ID_LOT_PROD = Encoding.Default.GetString(buffer.Skip(6).Take(9).ToArray());
                                Int16 ID_PART_LOT = BitConverter.ToInt16(buffer.Skip(15).Take(2).ToArray(), 0);
                                double REC_ID = 0;
                                sql = string.Format("select REC_ID from TLabelContent where ID_LOT_PROD='{0}' and ID_PART_LOT={1}", ID_LOT_PROD, ID_PART_LOT);
                                DbDataReader dr = tm.MultithreadDataReader(sql);
                                while (dr.Read())
                                {
                                    if (dr["REC_ID"] != DBNull.Value)
                                        REC_ID = Convert.ToDouble(dr["REC_ID"]);
                                }
                                dr.Close();
                                sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID={2}", REC_ID, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), 19);//更新虎踞打捆数据标志
                                tm.MultithreadExecuteNonQuery(sql);
                                sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID={2}", 1, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), 18);//更新虎踞接收数据的RECID
                                tm.MultithreadExecuteNonQuery(sql);
                                
                                string str = "二级收到：虎踞接收MES数据成功";
                                sql = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                                tm.MultithreadExecuteNonQuery(sql);
                            }
                            else
                            {
                                string str = "接收PLC数据失败"+Program.MessageFlg.ToString() + " " + Program.PrintNum.ToString();
                                sql = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                                tm.MultithreadExecuteNonQuery(sql);
                            }
                        }

                    }
                    
                 }
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                }
            }
                }
        public static void senddata(byte[] buffer)
        {
            try
            {
                //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(data);
                //buffer[0] = 49;
                socketServer.Send(buffer);
            }
            catch (Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
            }
        }
    }
        }
