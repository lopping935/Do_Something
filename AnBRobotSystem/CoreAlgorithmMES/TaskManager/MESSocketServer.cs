using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using log4net;
using SQLPublicClass;
using System.Reflection;
using System.Data.Common;
using CoreAlgorithmMES;
namespace CoreAlgorithm.TaskManager
{   
    class MESSocketServer
    {
        [DllImport("kernel32.dll")]
    private static extern bool SetLocalTime(ref SYSTEMTIME time);
        [StructLayout(LayoutKind.Sequential)]
        
    private struct SYSTEMTIME
    {
        public ushort year;
        public ushort month;
        public ushort dayOfWeek;
        public ushort day;
        public ushort hour;
        public ushort minute;
        public ushort second;
        public ushort milliseconds;
    }
        
        
        public static bool SetDate(DateTime dt)
        {
            SYSTEMTIME st;

            st.year = (ushort)dt.Year;
            st.month = (ushort)dt.Month;
            st.dayOfWeek = (ushort)dt.DayOfWeek;
            st.day = (ushort)dt.Day;
            st.hour = (ushort)dt.Hour;
            st.minute = (ushort)dt.Minute;
            st.second = (ushort)dt.Second;
            st.milliseconds = (ushort)dt.Millisecond;
            bool rt = SetLocalTime(ref st);
            return rt;
        }
        static Socket socketServer;
        static Socket socketWatch;
        static TasksManager tm;
        
        public enum EncodingType { UTF7, UTF8, UTF32, Unicode, BigEndianUnicode, ASCII, GB2312, GBK,ISO8859 };
        public static string GetString(byte[] myByte, EncodingType encodingType)
        {
            string str = null;
            switch (encodingType)
            {
                //将要加密的字符串转换为指定编码的字节数组
                case EncodingType.UTF7:
                    str = Encoding.UTF7.GetString(myByte);
                    break;
                case EncodingType.UTF8:
                    str = Encoding.UTF8.GetString(myByte);
                    break;
                case EncodingType.UTF32:
                    str = Encoding.UTF32.GetString(myByte);
                    break;
                case EncodingType.Unicode:
                    str = Encoding.Unicode.GetString(myByte);
                    break;
                case EncodingType.BigEndianUnicode:
                    str = Encoding.BigEndianUnicode.GetString(myByte);
                    break;
                case EncodingType.ASCII:
                    str = Encoding.ASCII.GetString(myByte);
                   // str = Encoding.;
                    break;
                case EncodingType.GB2312:
                    str = Encoding.Default.GetString(myByte);
                    break;
                case EncodingType.GBK:
                    str = System.Text.Encoding.GetEncoding("GBK").GetString(myByte);
                    break;
                case EncodingType.ISO8859:
                    str = System.Text.Encoding.GetEncoding("ISO8859-1").GetString(myByte);
                    break;
            }
            return str;
        }

         
        // <summary>  
        /// 定位指定的 System.Byte[] 在此实例中的第一个匹配项的索引。  
        /// </summary>  
        /// <param name="srcBytes">源数组</param>  
        /// <param name="searchBytes">查找的数组</param>  
        /// <returns>返回的索引位置；否则返回值为 -1。</returns>  
        public static List<byte[]> nByteIndexOf(byte[] srcBytes, byte[] searchBytes)
        {
            List<byte[]> HeadIndex = new List<byte[]>();
            int starindex = 0;
            for (int i = 0; i < srcBytes.Length; i++)//- searchBytes.Length- searchBytes.Length
            {
                if (srcBytes[i] == searchBytes[0])
                {
                    if (searchBytes.Length == 1)
                    {
                        if (starindex == 0)
                        {
                            HeadIndex.Add(srcBytes.Skip(i).Take(i - starindex).ToArray());
                            starindex = i;
                        }
                        else
                        {
                            HeadIndex.Add(srcBytes.Skip(i).Take(i - starindex).ToArray());
                            starindex = i + searchBytes.Length;
                        }

                    }
                    else
                    {
                        bool flag = true;
                        for (int j = 1; j < searchBytes.Length; j++)
                        {
                            if (srcBytes[i + j] != searchBytes[j])
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            if (starindex == 0)
                            {
                                HeadIndex.Add(srcBytes.Skip(starindex).Take(i - starindex).ToArray());
                                starindex = i;
                            }
                            else
                            {
                                HeadIndex.Add(srcBytes.Skip(starindex + searchBytes.Length).Take(i - starindex - searchBytes.Length).ToArray());
                                starindex = i;
                            }
                        }
                    }
                }
            }
            return HeadIndex;
        }
        
        public static List<int> ByteIndexOf(byte[] srcBytes, byte[] searchBytes)
        {
            List<int> HeadIndex = new List<int>();
            for (int i = 0; i < srcBytes.Length- searchBytes.Length; i++)
            {
                if (srcBytes[i] == searchBytes[0])
                {
                    if (searchBytes.Length == 1) { HeadIndex.Add(i); }
                    bool flag = true;
                    for (int j = 1; j < searchBytes.Length; j++)
                    {
                        if (srcBytes[i + j] != searchBytes[j])
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag) { HeadIndex.Add(i); }
                }
            }
            return HeadIndex;
        }
        public static void CreateSocket(string mesip, int port)
        {
            //当点击开始监听的时候 在服务器端创建一个负责监听IP地址跟端口号的Socket
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //创建端口号对象
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(mesip), port);
            //监听
            socketWatch.Bind(iep);
            tm = new TasksManager();
            socketWatch.Listen(10);//队列排队
            Console.WriteLine("监听Socket创建完成，准备进入监听程序。");
            Thread receiveThread = new Thread(ListenRecall);
            receiveThread.Start();
        }
        static private void ListenRecall()
        {
            //等待客户端的链接并创建一个负责通信的Socket
            while (true)
            {
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
            messagecls msq = new messagecls();
            string sql = "";
            while (true)
            {
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
                        string text = GetString(buffer, EncodingType.UTF8);
                        byte[] searchBytes = new byte[] { 0x7F, 0x26 };
                        List<byte[]> HeadIndex = new List<byte[]>();
                        HeadIndex = nByteIndexOf(buffer, searchBytes);
                        string MessageFlg= GetString(HeadIndex[0], EncodingType.GB2312);//System.Text.Encoding.ASCII.GetString(buffer.Skip(0).Take(HeadIndex + 1).ToArray());
                        
                       

                        if (MessageFlg == "21LA000")//心跳信息，校准时间
                        {
                            string MesTime = GetString(HeadIndex[1], EncodingType.ASCII);//System.Text.Encoding.ASCII.GetString(buffer.Skip(0).Take(HeadIndex + 1).ToArray());
                            bool ret = SetDate(Convert.ToDateTime(MesTime));
                            string str = MessageFlg.ToString() + " " + MesTime;
                            sql = string.Format("INSERT INTO MESRECVLOG(REC_CREATE_TIME,RECV_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                            tm.MultithreadExecuteNonQuery(sql);
                        }
                        #region l3->l2接收数据标签
                        if (MessageFlg == "21LA001")//接收标签数据信息并反馈
                        {
                            
                            messagecls.LabelData LabelDataRecv;
                            LabelDataRecv.ID_TIME = GetString(HeadIndex[1], EncodingType.GB2312);
                            LabelDataRecv.MACHINE_NO = GetString(HeadIndex[2], EncodingType.GB2312);                           
                            LabelDataRecv.ID_LOT_PROD = GetString(HeadIndex[3], EncodingType.GB2312);
                            LabelDataRecv.ID_PART_LOT = Convert.ToInt16(GetString(HeadIndex[4], EncodingType.GB2312));
                            LabelDataRecv.NUM_BDL = Convert.ToInt16(GetString(HeadIndex[5], EncodingType.GB2312));
                            LabelDataRecv.SEQ_LEN = Convert.ToInt16(GetString(HeadIndex[6], EncodingType.GB2312));
                            LabelDataRecv.SEQ_OPR = Convert.ToInt16(GetString(HeadIndex[7], EncodingType.GB2312));
                            LabelDataRecv.DIM_LEN = Convert.ToDouble(GetString(HeadIndex[8], EncodingType.GB2312));
                            LabelDataRecv.IND_FIXED = GetString(HeadIndex[9], EncodingType.GB2312);
                            LabelDataRecv.SEQ_SEND = Convert.ToDouble(GetString(HeadIndex[10], EncodingType.GB2312));
                            LabelDataRecv.NUM_BAR = Convert.ToInt16(GetString(HeadIndex[11], EncodingType.GB2312));
                            LabelDataRecv.SEQ_LIST = Convert.ToInt16(GetString(HeadIndex[12], EncodingType.GB2312));
                            LabelDataRecv.LA_BDL_ACT = Convert.ToDouble(GetString(HeadIndex[13], EncodingType.GB2312));
                            LabelDataRecv.NO_LICENCE = GetString(HeadIndex[14], EncodingType.GB2312);
                            LabelDataRecv.NAME_PROD = GetString(HeadIndex[15], EncodingType.GB2312);//GB2312
                            LabelDataRecv.NAME_STND = GetString(HeadIndex[16], EncodingType.GB2312);
                            LabelDataRecv.ID_HEAT = GetString(HeadIndex[17], EncodingType.GB2312);
                            LabelDataRecv.NAME_STLGD = GetString(HeadIndex[18], EncodingType.GB2312);//GB2312
                            LabelDataRecv.DES_FIPRO_SECTION = GetString(HeadIndex[19], EncodingType.GB2312);
                            LabelDataRecv.ID_CREW_RL = GetString(HeadIndex[20], EncodingType.GB2312);//GB2312
                            LabelDataRecv.ID_CREW_CK = GetString(HeadIndex[21], EncodingType.GB2312);//GB2312
                            LabelDataRecv.TMSTP_WEIGH = GetString(HeadIndex[22], EncodingType.GB2312);
                            LabelDataRecv.BAR_CODE = GetString(HeadIndex[23], EncodingType.GB2312);
                            LabelDataRecv.NUM_HEAD = Convert.ToInt16(GetString(HeadIndex[24], EncodingType.GB2312));
                            LabelDataRecv.NUM_TAIL = Convert.ToInt16(GetString(HeadIndex[25], EncodingType.GB2312));
                            LabelDataRecv.TMSTP_SEND = GetString(HeadIndex[26], EncodingType.GB2312);
                            sql = "select PARAMETER_VALUE from SYSPARAMETER where PARAMETER_ID=10";
                            DbDataReader dr = tm.MultithreadDataReader(sql);
                            double ProductIDA = 0;
                            while (dr.Read())
                            {
                                if (dr["PARAMETER_VALUE"] != DBNull.Value)
                                    ProductIDA = Convert.ToDouble(dr["PARAMETER_VALUE"]) + 1;
                            }
                            dr.Close();

                            sql = string.Format("insert into HLabelContent(MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST,LA_BDL_ACT,NO_LICENCE,NAME_PROD,NAME_STND,ID_HEAT,NAME_STLGD,DES_FIPRO_SECTION,ID_CREW_RL,ID_CREW_CK,TMSTP_WEIGH,BAR_CODE,NUM_HEAD,NUM_TAIL,L3TMSTP_SEND,REC_ID,REC_CREATE_TIME) values('{0}','{1}',{2},{3},{4},{5},{6},'{7}',{8},{9},{10},{11},'{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22},{23},'{24}',{25},'{26}')", LabelDataRecv.MACHINE_NO, LabelDataRecv.ID_LOT_PROD, LabelDataRecv.ID_PART_LOT, LabelDataRecv.NUM_BDL, LabelDataRecv.SEQ_LEN, LabelDataRecv.SEQ_OPR, LabelDataRecv.DIM_LEN, LabelDataRecv.IND_FIXED, LabelDataRecv.SEQ_SEND, LabelDataRecv.NUM_BAR, LabelDataRecv.SEQ_LIST, LabelDataRecv.LA_BDL_ACT, LabelDataRecv.NO_LICENCE, LabelDataRecv.NAME_PROD, LabelDataRecv.NAME_STND, LabelDataRecv.ID_HEAT, LabelDataRecv.NAME_STLGD, LabelDataRecv.DES_FIPRO_SECTION, LabelDataRecv.ID_CREW_RL, LabelDataRecv.ID_CREW_CK, LabelDataRecv.TMSTP_WEIGH, LabelDataRecv.BAR_CODE, LabelDataRecv.NUM_HEAD, LabelDataRecv.NUM_TAIL, LabelDataRecv.TMSTP_SEND, ProductIDA, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            tm.MultithreadExecuteNonQuery(sql);
                            sql = string.Format("insert into TLabelContent(MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST,LA_BDL_ACT,NO_LICENCE,NAME_PROD,NAME_STND,ID_HEAT,NAME_STLGD,DES_FIPRO_SECTION,ID_CREW_RL,ID_CREW_CK,TMSTP_WEIGH,BAR_CODE,NUM_HEAD,NUM_TAIL,L3TMSTP_SEND,REC_ID,REC_CREATE_TIME) values('{0}','{1}',{2},{3},{4},{5},{6},'{7}',{8},{9},{10},{11},'{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22},{23},'{24}',{25},'{26}')", LabelDataRecv.MACHINE_NO, LabelDataRecv.ID_LOT_PROD, LabelDataRecv.ID_PART_LOT, LabelDataRecv.NUM_BDL, LabelDataRecv.SEQ_LEN, LabelDataRecv.SEQ_OPR, LabelDataRecv.DIM_LEN, LabelDataRecv.IND_FIXED, LabelDataRecv.SEQ_SEND, LabelDataRecv.NUM_BAR, LabelDataRecv.SEQ_LIST, LabelDataRecv.LA_BDL_ACT, LabelDataRecv.NO_LICENCE, LabelDataRecv.NAME_PROD, LabelDataRecv.NAME_STND, LabelDataRecv.ID_HEAT, LabelDataRecv.NAME_STLGD, LabelDataRecv.DES_FIPRO_SECTION, LabelDataRecv.ID_CREW_RL, LabelDataRecv.ID_CREW_CK, LabelDataRecv.TMSTP_WEIGH, LabelDataRecv.BAR_CODE, LabelDataRecv.NUM_HEAD, LabelDataRecv.NUM_TAIL, LabelDataRecv.TMSTP_SEND, ProductIDA, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            tm.MultithreadExecuteNonQuery(sql);
                            sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=10", ProductIDA, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));
                            tm.MultithreadExecuteNonQuery(sql);
                            string str = MessageFlg.ToString() + " " + LabelDataRecv.MACHINE_NO + " " + LabelDataRecv.ID_LOT_PROD + " " + LabelDataRecv.ID_PART_LOT.ToString() + " " + LabelDataRecv.NUM_BDL.ToString() + " " + LabelDataRecv.SEQ_LEN.ToString() + " " + LabelDataRecv.SEQ_OPR.ToString() + " " + LabelDataRecv.DIM_LEN.ToString() + " " + LabelDataRecv.IND_FIXED + " " + LabelDataRecv.SEQ_SEND.ToString() + " " + LabelDataRecv.NUM_BAR.ToString() + " " + LabelDataRecv.SEQ_LIST.ToString() + " " + LabelDataRecv.LA_BDL_ACT.ToString() + " " + LabelDataRecv.NO_LICENCE + " " + LabelDataRecv.NAME_PROD + " " + LabelDataRecv.NAME_STND + " " + LabelDataRecv.ID_HEAT + " " + LabelDataRecv.NAME_STLGD + " " + LabelDataRecv.DES_FIPRO_SECTION + " " + LabelDataRecv.ID_CREW_RL + " " + LabelDataRecv.ID_CREW_CK + " " + LabelDataRecv.TMSTP_WEIGH + " " + LabelDataRecv.BAR_CODE + " " + LabelDataRecv.NUM_HEAD.ToString() + " " + LabelDataRecv.NUM_TAIL.ToString() + " " + LabelDataRecv.TMSTP_SEND;
                            sql = string.Format("INSERT INTO MESRECVLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                            tm.MultithreadExecuteNonQuery(sql);
                            #endregion
                            #region l2->l3标签数据应答
                            //反馈接收数据标签信息                        
                            string MessageHead = "LA2101A";
                            byte OldBytes = 0x20;
                            byte NewBytes = 0x7F;
                            byte[] sendArray1 = Enumerable.Repeat((byte)0x20, length-19-2).ToArray(); //
                            Array.Copy(buffer, sendArray1, length-19-2);
                            byte[] byteArray1 = Encoding.ASCII.GetBytes(MessageHead);//应答头
                            Buffer.BlockCopy(byteArray1, 0, sendArray1, 0, byteArray1.Length);

                            string appendmsg = "1 &" + " &" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" &"+ ProductIDA.ToString()+ " &";                            
                            byte[] sendArray2 = StripIronNum.ByteReplace(Encoding.ASCII.GetBytes(appendmsg), OldBytes, NewBytes);

                            byte[] sendArray = Enumerable.Repeat((byte)0x20, sendArray1.Length+sendArray2.Length).ToArray();
                            Array.Copy(sendArray1, sendArray, sendArray1.Length);
                            Buffer.BlockCopy(sendArray2, 0, sendArray, sendArray1.Length, sendArray2.Length);

                            MESSocketClient.senddata(sendArray);
                            string strsend = MessageHead + " &" + str + "1" + " &" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " &";
                            string sqlsend = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), strsend);
                            tm.MultithreadExecuteNonQuery(sqlsend);
                            #endregion
                        }
                        if (MessageFlg == "21LA01A")//l3->l2标签结果应答
                        {
                            string ID_TIME= GetString(HeadIndex[1], EncodingType.ASCII);
                            string MACHINE_NO = GetString(HeadIndex[2], EncodingType.ASCII);
                            string ID_LOT_PROD = GetString(HeadIndex[3], EncodingType.ASCII);
                            short ID_PART_LOT = Convert.ToInt16(GetString(HeadIndex[4], EncodingType.ASCII));
                            short NUM_BDL = Convert.ToInt16(GetString(HeadIndex[5], EncodingType.ASCII));
                            short SEQ_LEN = Convert.ToInt16(GetString(HeadIndex[6], EncodingType.ASCII));
                            short SEQ_OPR = Convert.ToInt16(GetString(HeadIndex[7], EncodingType.ASCII));
                            double SEQ_SEND= Convert.ToDouble(GetString(HeadIndex[8], EncodingType.ASCII));
                            double SEQ_L2 = Convert.ToDouble(GetString(HeadIndex[9], EncodingType.ASCII));
                            short ACK = Convert.ToInt16(GetString(HeadIndex[10], EncodingType.ASCII));
                            string REASON = GetString(HeadIndex[11], EncodingType.ASCII);
                            string TMSTP_SEND = GetString(HeadIndex[12], EncodingType.ASCII);
                            sql = string.Format("UPDATE TLabelContent SET MODELSWTMSTP_SEND='{0}',MODELSWACK={1},MODELSWREASON='{2}' WHERE ID_LOT_PROD='{3}' and ID_PART_LOT={4} and NUM_BDL={5} and SEQ_LEN={6} and SEQ_OPR={7} and SEQ_SEND={8} and SEQ_L2={9}", TMSTP_SEND, ACK, REASON, ID_LOT_PROD, ID_PART_LOT, NUM_BDL, SEQ_LEN, SEQ_OPR, SEQ_SEND, SEQ_L2);
                            tm.MultithreadExecuteNonQuery(sql);
                            string str = MessageFlg.ToString() + " " + ACK + " " + REASON + " " + TMSTP_SEND + " " + ID_LOT_PROD + " " + ID_PART_LOT.ToString() + " " + NUM_BDL.ToString() + " " + SEQ_LEN.ToString() + " " + SEQ_OPR.ToString()+SEQ_SEND.ToString()+SEQ_L2.ToString();
                            sql = string.Format("INSERT INTO MESRECVLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                            tm.MultithreadExecuteNonQuery(sql);
                        }
                        if(MessageFlg == "21LA02A")
                        {
                            string ID_TIME= GetString(HeadIndex[1], EncodingType.ASCII);
                            string MACHINE_NO = GetString(HeadIndex[2], EncodingType.ASCII);
                            string ID_LOT_PROD = GetString(HeadIndex[3], EncodingType.ASCII);
                            short ID_PART_LOT = Convert.ToInt16(GetString(HeadIndex[4], EncodingType.ASCII));
                            short NUM_BDL = Convert.ToInt16(GetString(HeadIndex[5], EncodingType.ASCII));
                            short SEQ_LEN = Convert.ToInt16(GetString(HeadIndex[6], EncodingType.ASCII));
                            short SEQ_OPR = Convert.ToInt16(GetString(HeadIndex[7], EncodingType.ASCII));
                            double SEQ_L2 = Convert.ToDouble(GetString(HeadIndex[8], EncodingType.ASCII));
                            short ACK = Convert.ToInt16(GetString(HeadIndex[9], EncodingType.ASCII));
                            string REASON = GetString(HeadIndex[10], EncodingType.ASCII);
                            string TMSTP_SEND = GetString(HeadIndex[11], EncodingType.ASCII);
                            sql = string.Format("UPDATE TLabelContent SET MODELSWTMSTP_SEND='{0}',MODELSWACK={1},MODELSWREASON='{2}' WHERE ID_LOT_PROD='{3}' and ID_PART_LOT={4} and NUM_BDL={5} and SEQ_LEN={6} and SEQ_OPR={7} and SEQ_L2={8}", TMSTP_SEND, ACK, REASON, ID_LOT_PROD, ID_PART_LOT, NUM_BDL, SEQ_LEN, SEQ_OPR, SEQ_L2);
                            tm.MultithreadExecuteNonQuery(sql);
                            string str = MessageFlg.ToString() + " " + ACK + " " + REASON + " " + TMSTP_SEND + " " + ID_LOT_PROD + " " + ID_PART_LOT.ToString() + " " + NUM_BDL.ToString() + " " + SEQ_LEN.ToString() + " " + SEQ_OPR.ToString() + " " + SEQ_L2.ToString();
                            sql = string.Format("INSERT INTO MESRECVLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")),"l3->l2模式切换"+ str);
                            tm.MultithreadExecuteNonQuery(sql);
                        }
                        //if(MessageFlg!= "21LA000"&& MessageFlg != "21LA001" && MessageFlg != "21LA01A" && MessageFlg != "21LA02A")
                        //{
                        //    sql = string.Format("INSERT INTO MESRECVLOG(REC_CREATE_TIME,RECV_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "收到三级非法数据");
                        //    tm.MultithreadExecuteNonQuery(sql);
                        //}

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
