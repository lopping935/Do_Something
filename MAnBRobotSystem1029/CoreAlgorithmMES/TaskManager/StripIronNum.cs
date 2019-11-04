using System;
using System.Runtime.InteropServices;
using SQLPublicClass;
using System.Data.Common;
using System.Threading;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
namespace CoreAlgorithm.TaskManager
{
   public class StripIronNum
    {
        TasksManager tm;
        INIClass ini = new INIClass(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        static string MESIP = "", localip = "";//400PLC ip
        static int mesportr = 0, localportr = 0;//400PLC端口
        public static object locker = new object();
        public static byte [] ByteReplace(byte[] srcBytes, byte OldByte, byte NewByte)
        {
            for (int i = 0; i < srcBytes.Length ; i++)
            {
                if (srcBytes[i] == OldByte)
                    srcBytes[i] = NewByte;
            }
            return srcBytes;
        }
        public StripIronNum()
        {
            tm = new TasksManager();
        }

        public void do_SendMessage()
        {
            string sql ="", time, MessageHead;
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// DateTime.Now.ToString();
                    string ss = time.Substring(17, 2);
                    #region 一分钟发送心跳包                    
                    if (ss == "59")//ss == "59"
                    {
                        
                            MessageHead = "PRL3000";
                            byte[] sendArrayT = Enumerable.Repeat((byte)0x20, 30).ToArray();
                            byte[] byteArray1T = System.Text.Encoding.ASCII.GetBytes(MessageHead);
                            byte[] byteArray2T = System.Text.Encoding.ASCII.GetBytes(time);
                            sendArrayT[byteArray1T.Length] = 0x7F;
                            sendArrayT[byteArray1T.Length + 1] = 0x26;
                            sendArrayT[byteArray1T.Length + byteArray2T.Length + 2] = 0x7F;
                            sendArrayT[byteArray1T.Length + byteArray2T.Length + 3] = 0x26;
                            Buffer.BlockCopy(byteArray1T, 0, sendArrayT, 0, byteArray1T.Length);
                            Buffer.BlockCopy(byteArray2T, 0, sendArrayT, byteArray1T.Length + 2, byteArray2T.Length);
                            try
                            {
                                MESSocketClient.senddata(sendArrayT);
                            }
                            catch
                            {
                                ReconnectMES();

                            }
                            string str = MessageHead + " &" + time + " &";
                            sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                            tm.MultithreadExecuteNonQuery(sql);
                       
                    }
                    #endregion

                    #region 选择所有贴标完成 但 三级没有应答的 对三级进行反馈
                    #region
                    /*
                    原程序
                    if(ss=="1000")
                    { 
                    sql = string.Format("select MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR from TLabelContent WHERE (IMP_FINISH=31 OR IMP_FINISH=32 OR IMP_FINISH=33) AND L3ACK=0 order by REC_ID ASC");
                    string MACHINE_NO = "";
                    string ID_LOT_PROD = "", ACK = "1", REASON = "", SEQ_SEND = "";
                    Int16 ID_PART_LOT = 0;
                    Int16 NUM_BDL = 0;
                    Int16 SEQ_LEN = 0;
                    Int16 SEQ_OPR = 0;
                     DataTable dt = tm.MultithreadDataTable(sql);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MACHINE_NO = dt.Rows[i]["MACHINE_NO"].ToString();
                        ID_LOT_PROD = dt.Rows[i]["ID_LOT_PROD"].ToString();
                        ID_PART_LOT = Int16.Parse(dt.Rows[i]["ID_PART_LOT"].ToString());
                        NUM_BDL = Int16.Parse(dt.Rows[i]["NUM_BDL"].ToString());
                        SEQ_LEN = Int16.Parse(dt.Rows[i]["SEQ_LEN"].ToString());
                        SEQ_OPR = Int16.Parse(dt.Rows[i]["SEQ_OPR"].ToString());
                        MessageHead = "PRL301A";
                        time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        string str = MessageHead + " &" + MACHINE_NO + " &" + ID_LOT_PROD + " &" + ID_PART_LOT.ToString() + " &" + NUM_BDL.ToString() + " &" + SEQ_LEN.ToString() + " &" + SEQ_OPR.ToString() + " &" + ACK + " &" + REASON + " &" + time + " &" + SEQ_SEND + " &";
                        byte[] sendArray = System.Text.Encoding.ASCII.GetBytes(str);
                        byte OldBytes = 0x20;
                        byte NewBytes = 0x7F;
                        byte[] sendArrayNew = ByteReplace(sendArray, OldBytes, NewBytes);
                        if (sendArrayNew.Length > 0)
                        {
                            MESSocketClient.senddata(sendArrayNew);
                            sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                            tm.MultithreadExecuteNonQuery(sql);
                        }
                    }*/
                    #endregion
                    if (ss == "10020")//根据说明改的
                        {
                            sql = string.Format("select MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST from TLabelContent WHERE (IMP_FINISH=31 OR IMP_FINISH=32 OR IMP_FINISH=33) AND L3ACK=0 order by REC_ID ASC");
                            string MACHINE_NO = "",ID_LOT_PROD = "",  ACK = "1", REASON = "",  IND_FIXED="";
                            Int16 ID_PART_LOT = 0,NUM_BDL = 0,SEQ_LEN = 0,SEQ_OPR = 0,DIM_LEN = 0,NUM_BAR=0,SEQ_LIST=0;
                            double SEQ_SEND=0;
                            DataTable dt = tm.MultithreadDataTable(sql);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                MACHINE_NO = dt.Rows[i]["MACHINE_NO"].ToString();
                                ID_LOT_PROD = dt.Rows[i]["ID_LOT_PROD"].ToString();
                                ID_PART_LOT = Int16.Parse(dt.Rows[i]["ID_PART_LOT"].ToString());
                                NUM_BDL = Int16.Parse(dt.Rows[i]["NUM_BDL"].ToString());
                                SEQ_LEN = Int16.Parse(dt.Rows[i]["SEQ_LEN"].ToString());
                                SEQ_OPR = Int16.Parse(dt.Rows[i]["SEQ_OPR"].ToString());
                                DIM_LEN= Int16.Parse(dt.Rows[i]["DIM_LEN"].ToString());
                                IND_FIXED= dt.Rows[i]["IND_FIXED"].ToString();
                                SEQ_SEND = double.Parse(dt.Rows[i]["SEQ_SEND"].ToString());
                                NUM_BAR= Int16.Parse(dt.Rows[i]["NUM_BAR"].ToString());
                                SEQ_LIST = Int16.Parse(dt.Rows[i]["SEQ_LIST"].ToString());
                                MessageHead = "PRL301A";
                                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                string str = MessageHead + " &" + MACHINE_NO + " &" + ID_LOT_PROD + " &" + ID_PART_LOT.ToString() + " &" + NUM_BDL.ToString() + " &" + SEQ_LEN.ToString() + " &" + SEQ_OPR.ToString() + " &"+DIM_LEN.ToString() + " &" +IND_FIXED+" &"+SEQ_SEND+" &"+NUM_BAR.ToString()+" &"+SEQ_LIST.ToString()+" &"+ ACK + " &" + REASON + " &" + time + " &";
                                byte[] sendArray = System.Text.Encoding.ASCII.GetBytes(str);
                                byte OldBytes = 0x20;
                                byte NewBytes = 0x7F;
                                byte[] sendArrayNew = ByteReplace(sendArray, OldBytes, NewBytes);
                                if (sendArrayNew.Length > 0)
                                {
                                    MESSocketClient.senddata(sendArrayNew);
                                    sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                                    tm.MultithreadExecuteNonQuery(sql);
                                }
                            }

                            #endregion

                            #region 将200条之前的数据更新到历史表 并从当前表中删除
                            double MaxRecID = 0, count = 0;
                    sql = "select count(*) as count from TLabelContent";
                    dt = tm.MultithreadDataTable(sql);
                    for (int i = 0; i < dt.Rows.Count; i++)
                        count = Convert.ToInt32(dt.Rows[i]["count"].ToString());
                    if (count > 200)
                    {
                        sql = "select MAX(REC_ID) AS REC_ID from TLabelContent";
                        dt = tm.MultithreadDataTable(sql);
                        for (int i = 0; i < dt.Rows.Count; i++)
                            MaxRecID = Convert.ToDouble(dt.Rows[i]["REC_ID"].ToString());
                        sql = string.Format("insert into HLabelContent SELECT * FROM TLabelContent WHERE REC_ID<{0}", (MaxRecID - 199));
                        tm.MultithreadExecuteNonQuery(sql);
                        sql = string.Format("DELETE FROM  TLabelContent WHERE REC_ID<{0}", (MaxRecID - 199));
                        tm.MultithreadExecuteNonQuery(sql);
                    }

                       
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                    Log.addLog(log, LogType.ERROR, ex.StackTrace);
                }
        }
        }
      

        public void Do_Askmsg()
        {//Flag标识含义;0：无工作  1：三级请求数据  2：本地向三级发送数据 3：已接受到数据
            string sql = "";

            while (true)
            {
               Thread.Sleep(100);
                try
                {
                    int a = 0;
                    DbDataReader dr = null;                                    
                    sql = "select top 1 *  from S_TFlag";
                    dr = tm.MultithreadDataReader(sql);                       
                    while (dr.Read())
                    {
                        a = int.Parse(dr["Flag"].ToString());
                    }
                    dr.Close();                
                  
               
                    if (a == 1)//向ms请求数据
                    {
                        sql = "update S_TFlag set Flag=2 where ID=1";
                        int b = tm.MultithreadExecuteNonQuery(sql);

                        #region socket发送请求 
                        byte[] searchBytes = new byte[] { 0x7F, 0x26 };
                        byte[] sendArray = Enumerable.Repeat((byte)0x20, 33).ToArray();
                        string MACHINE_NO = "CH01";
                        string ASK_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        int ASK_SQ = 0;
                        byte[] byteArray1 = System.Text.Encoding.ASCII.GetBytes(MACHINE_NO);
                        byte[] byteArray2 = System.Text.Encoding.ASCII.GetBytes(ASK_TIME);
                        byte[] byteArray3 = BitConverter.GetBytes(ASK_SQ);
                        Buffer.BlockCopy(byteArray1, 0, sendArray, 0, byteArray1.Length);
                        Buffer.BlockCopy(searchBytes, 0, sendArray, 4, 2);
                        Buffer.BlockCopy(byteArray2, 0, sendArray, 6, byteArray2.Length);
                        Buffer.BlockCopy(searchBytes, 0, sendArray, 25, 2);
                        Buffer.BlockCopy(byteArray3, 0, sendArray, 27, byteArray3.Length);
                        Buffer.BlockCopy(searchBytes, 0, sendArray, 31, 2);
                        if (sendArray.Length > 0)
                        {
                            string str = MACHINE_NO + ASK_TIME + ASK_SQ.ToString();
                            MESSocketClient.senddata(sendArray);
                            sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                            tm.MultithreadExecuteNonQuery(sql);
                        }
                        Thread.Sleep(100);
                        #endregion
                    }
                    
                    if (a == 2)
                    {
                        double MAXRECID = 0;// PLANIDNow = 0;                
                        sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";
                        dr = tm.MultithreadDataReader(sql);
                        while (dr.Read())
                        {
                            if (dr["REC_ID"] != DBNull.Value)
                                MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
                        }
                        dr.Close();

                        int count = 0;
                        sql = string.Format("select count(*) as count from TLabelContent WHERE REC_ID>{0} AND IMP_FINISH=0", MAXRECID);
                        dr = tm.MultithreadDataReader(sql);
                        while (dr.Read())
                        {
                            count = int.Parse(dr["count"].ToString());
                        }
                        dr.Close();
                        if (count >= 1)
                        {
                            sql = "update S_TFlag set Flag=3 where ID=1";
                            int b = tm.MultithreadExecuteNonQuery(sql);
                        }
                    }

                }
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                    Log.addLog(log, LogType.ERROR, ex.StackTrace);
                }
            }
        }

        public void ReconnectMES()
        {
            string sql = null, str = null;
            bool connectstate = false;
            do
            {

                try
                {

                    
                    MESSocketClient.socketClient.Close();
                    MESSocketClient.socketClient.Dispose();
                    MESSocketClient.CreateConnect(MESIP, mesportr);
                    connectstate = MESSocketClient.IsSocketConnected();
                    str = "MES系统重新连接成功！";
                    sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                    tm.MultithreadExecuteNonQuery(sql);



                }
                catch
                {
                    str = "连接MES系统失败，正在尝试重新连接！";
                    sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                    tm.MultithreadExecuteNonQuery(sql);
                    connectstate = false;
                    Thread.Sleep(5000);
                }
            }
            while (!connectstate);
        }

        public void RunSINGenerate()
        {
            try
            {
                string sql = "SELECT ACQUISITIONCONFIG_ID,DATAACQUISITION_IP,DATAACQUISITION_PORTS,DATAACQUISITION_PORTR FROM ACQUISITIONCONFIG where ACQUISITIONCONFIG_ID=8 or ACQUISITIONCONFIG_ID=23";
                DbDataReader dr = null;
                dr = tm.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 8)
                    {
                        MESIP = Convert.ToString(dr["DATAACQUISITION_IP"]);
                        mesportr = Convert.ToInt16(dr["DATAACQUISITION_PORTS"]);
                    }
                    if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 23)
                    {
                        localip = Convert.ToString(dr["DATAACQUISITION_IP"]);
                        localportr = Convert.ToInt32(dr["DATAACQUISITION_PORTS"]);
                    }
                }
                dr.Close();

                MESSocketServer.CreateSocket(localip, localportr);
                try
                {
                    MESSocketClient.CreateConnect(MESIP, mesportr);
                }
                catch
                {
                    string str = "连接MES系统失败！";
                    sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                    tm.MultithreadExecuteNonQuery(sql);
                }
                Thread SendToMes = new Thread(do_SendMessage);
                SendToMes.Start();
                Thread AskMESDate = new Thread(Do_Askmsg);
                AskMESDate.Start();
                
                
            }
            catch (Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
                Log.addLog(log, LogType.ERROR, ex.StackTrace);
            }

        }
   
   }


   
}
