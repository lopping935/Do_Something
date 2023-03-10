using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using SQLPublicClass;
using System.Reflection;
namespace CoreAlgorithm.TaskManager
{
    class YFHelper
    {
        public struct LabelData
        {
            public string merge_sinbar;
            public string gk;
            public string heat_no;
            public string mtrl_no;
            public string spec;
            public int wegith;
            public int num_no;
            public string print_date;
            public string classes;
        };
        public void Recv(object SocketClient)
        {
            TasksManager tm = new TasksManager();
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
                        
                        lock (Program.gllock)
                        {
                            Program.MessageFlg = BitConverter.ToInt16(buffer.Skip(0).Take(2).ToArray(), 0);
                            if (Program.MessageFlg == 11)
                            {           
                                Program.PrintNum = buffer.Skip(2).Take(1).ToArray()[0];
                                string sqltext = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "收到PLC请求打印信号" + Program.MessageFlg.ToString() + " " + Program.PrintNum);
                                tm.MultithreadExecuteNonQuery(sqltext);
                            }
                            string sql = "";
                            if (Program.MessageFlg == 31 || Program.MessageFlg == 32 || Program.MessageFlg == 33)
                            {
                                double  REC_ID = double.Parse(Encoding.ASCII.GetString(buffer.Skip(4).Take(12).ToArray()));
                                string iface_id = Encoding.ASCII.GetString(buffer.Skip(16).Take(12).ToArray());
                                sql = string.Format("UPDATE TLabelContent SET REC_CREATE_TIME='{0}',IMP_FINISH={1} WHERE REC_ID={2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Program.MessageFlg, REC_ID);
                                tm.MultithreadExecuteNonQuery(sql);
                                string str = "收到PLC焊接完成信号"+Program.MessageFlg.ToString() + " " + iface_id;
                                sql = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                                tm.MultithreadExecuteNonQuery(sql);

                                KeyValuePair<string, Socket> kvp = CommMaster.PLC_Server.dict.FirstOrDefault();
                                byte[] markok = BitConverter.GetBytes(35);
                                byte[] sendArray = Enumerable.Repeat((byte)0x0, length).ToArray();
                                Array.Copy(buffer, sendArray, length);
                                Buffer.BlockCopy(markok, 0, sendArray, 0,markok.Length);
                                CommMaster.PLC_Server.SendToSomeone(sendArray, kvp.Key);
                                str = "发送到PLC完成回复" + " " + 35.ToString()+ " " + iface_id;
                                sql = string.Format("INSERT INTO SENDLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                                tm.MultithreadExecuteNonQuery(sql);
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    string str = "收到未定义信号" + Program.MessageFlg.ToString();
                    string sql = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                    tm.MultithreadExecuteNonQuery(sql);
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                }
            }
        }
    }
}
