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
                        Program.MessageFlg = BitConverter.ToInt16(buffer.Skip(0).Take(2).ToArray(), 0);
                        lock (Program.gllock)
                        {
                            if (Program.MessageFlg == 11 || Program.MessageFlg == 14)
                            {
                                Program.PrintNum = (buffer.Skip(2).Take(1).ToArray())[0];
                            }
                            string sql = "";
                            if (Program.MessageFlg == 31 || Program.MessageFlg == 32 || Program.MessageFlg == 33)
                            {
                                string ID_LOT_PROD = Encoding.ASCII.GetString(buffer.Skip(4).Take(10).ToArray());
                                Int16 ID_PART_LOT = BitConverter.ToInt16(buffer.Skip(14).Take(2).ToArray(), 0);
                                Int16 NUM_BDL = BitConverter.ToInt16(buffer.Skip(16).Take(2).ToArray(), 0);
                                Int16 SEQ_LEN = BitConverter.ToInt16(buffer.Skip(18).Take(2).ToArray(), 0);
                                Int16 SEQ_OPR = BitConverter.ToInt16(buffer.Skip(20).Take(2).ToArray(), 0);
                                sql = string.Format("UPDATE TLabelContent SET REC_IMP_TIME='{0}',IMP_FINISH={1} WHERE ID_LOT_PROD='{2}' and ID_PART_LOT={3} and NUM_BDL={4} and SEQ_LEN={5} and SEQ_OPR={6}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Program.MessageFlg, ID_LOT_PROD, ID_PART_LOT, NUM_BDL, SEQ_LEN, SEQ_OPR);
                                tm.MultithreadExecuteNonQuery(sql);
                                string str = Program.MessageFlg.ToString() + " " + ID_LOT_PROD + " " + ID_PART_LOT.ToString() + " " + NUM_BDL.ToString() + " " + SEQ_LEN.ToString() + " " + SEQ_OPR.ToString();
                                sql = string.Format("INSERT INTO RECVLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                                tm.MultithreadExecuteNonQuery(sql);
                            }
                            else
                            {
                                string str = Program.MessageFlg.ToString() + " " + Program.PrintNum.ToString();
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
    }
}
