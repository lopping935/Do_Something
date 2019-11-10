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
            //string inputstring = "1"; Console.ReadLine();
            //byte[] buffer1 = System.Text.Encoding.Default.GetBytes(inputstring);
            /*byte[] buffer1 = new byte[1]; //System.Text.Encoding.Default.GetBytes(inputstring);
            buffer1[0] = 49;
            connect.Send(buffer1);*/
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
                        //Console.WriteLine("Socket服务接收数据，信息如下：\r\n客户端：{0} \r\n数  据：{1}", connect.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(buffer));
                        //inputstring = "1"; Console.ReadLine();
                        /*buffer1 = new byte[1]; //System.Text.Encoding.Default.GetBytes(inputstring);
                        buffer1[0] = 49;
                        connect.Send(buffer1);*/
                        Program.MessageFlg = BitConverter.ToInt16(buffer.Skip(0).Take(2).ToArray(), 0);
                        //sqlTempUP.Add("UPDATE [EquipmentAttributeValuesRT] SET [AttributeValues_Time]='{1}',[AttributeValues_Value]='{2}' WHERE [EquipmentAttribute_id]={0}");
                        lock(Program.gllock)
                             { if (Program.MessageFlg == 11 || Program.MessageFlg == 14)
                            {
                                Program.PrintNum = (buffer.Skip(2).Take(1).ToArray())[0];//BitConverter.ToInt16(buffer.Skip(2).Take(2).ToArray(), 0);
                            }
                            string sql = "";
                            if (Program.MessageFlg == 31 || Program.MessageFlg == 32 || Program.MessageFlg == 33)
                            {
                                //string ID_LOT_PROD = BitConverter.ToString(buffer.Skip(4).Take(10).ToArray(), 0);
                                string ID_LOT_PROD = Encoding.ASCII.GetString(buffer.Skip(4).Take(10).ToArray());
                                Int16 ID_PART_LOT = BitConverter.ToInt16(buffer.Skip(14).Take(2).ToArray(), 0);
                                Int16 NUM_BDL = BitConverter.ToInt16(buffer.Skip(16).Take(2).ToArray(), 0);
                                Int16 SEQ_LEN = BitConverter.ToInt16(buffer.Skip(18).Take(2).ToArray(), 0);
                                Int16 SEQ_OPR = BitConverter.ToInt16(buffer.Skip(20).Take(2).ToArray(), 0);
                                sql = string.Format("UPDATE TLabelContent SET REC_IMP_TIME='{0}',IMP_FINISH={1} WHERE ID_LOT_PROD='{2}' and ID_PART_LOT={3} and NUM_BDL={4} and SEQ_LEN={5} and SEQ_OPR={6}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Program.MessageFlg, ID_LOT_PROD, ID_PART_LOT, NUM_BDL, SEQ_LEN, SEQ_OPR);
                                tm.MultithreadExecuteNonQuery(sql);
                                string str = Program.MessageFlg.ToString() + " " + ID_LOT_PROD + " " + ID_PART_LOT.ToString() + " " + NUM_BDL.ToString() + " " + SEQ_LEN.ToString() + " " + SEQ_OPR.ToString();
                                //sqlTemp.Add("INSERT INTO [EquipmentAttributeValues]([EquipmentAttribute_id],[AttributeValues_Time],[AttributeValues_Value]) VALUES ({0},'{1}','{2}')");
                                sql = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                                tm.MultithreadExecuteNonQuery(sql);
                            }
                            else
                            {
                                string str = Program.MessageFlg.ToString() + " " + Program.PrintNum.ToString();
                                //sqlTemp.Add("INSERT INTO [EquipmentAttributeValues]([EquipmentAttribute_id],[AttributeValues_Time],[AttributeValues_Value]) VALUES ({0},'{1}','{2}')");
                                sql = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                                tm.MultithreadExecuteNonQuery(sql);
                            }
                        }
                        //Console.WriteLine("Client接收到服务器返回信息，信息如下：\r\n客户端：{0} ",  Encoding.UTF8.GetString(buffer));
                        
                    }
                }
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                }
                //connect.Send(buffer);
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
