using System;
using System.Collections.Generic;
using SQLPublicClass;
using System.Data.Common;
using System.Threading;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net.Sockets;
using System.Linq;
using CoreAlgorithmMES;
using System.Text;
using SocketHelper;
namespace CoreAlgorithm.TaskManager
{
   public class StripIronNum
    {
        public static TasksManager tm;
        static string MESIP = "", localip = "";//400PLC ip
        static int  mesportr =0, localportr =0;//400PLC端口

        INIClass ini = new INIClass(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        public static object locker = new object();
        byte OldBytes = 0x20;
        byte NewBytes = 0x7F;
        SocketServer Server_MES = null;
        SocketServer Server_PLC = null;
        public static SocketClient Client_MES = null;
        public static SocketClient Client_PLC = null;
        public messagecls MEShelper = null;
        public static byte [] ByteReplace(byte[] srcBytes, byte OldByte, byte NewByte)
        {
            for (int i = 0; i < srcBytes.Length-1; i++)
            {
                if (srcBytes[i] == OldByte && srcBytes[i+1]==0X26)
                    srcBytes[i] = NewByte;
            }
            return srcBytes;
        }

        public StripIronNum()
        {
            tm = new TasksManager();
            MEShelper = new messagecls();            
            string sql = "SELECT ACQUISITIONCONFIG_ID,DATAACQUISITION_IP,DATAACQUISITION_PORTS,DATAACQUISITION_PORTR FROM ACQUISITIONCONFIG where ACQUISITIONCONFIG_ID=8 or ACQUISITIONCONFIG_ID=9";
            DbDataReader dr = null;
            dr = tm.MultithreadDataReader(sql);
            while (dr.Read())
            {
                if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 8)
                {
                    MESIP = Convert.ToString(dr["DATAACQUISITION_IP"]);
                    mesportr = Convert.ToInt32(dr["DATAACQUISITION_PORTR"]);
                }
                if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 9)
                {
                    localip = Convert.ToString(dr["DATAACQUISITION_IP"]);
                    localportr = Convert.ToInt32(dr["DATAACQUISITION_PORTS"]);
                }
            }

            dr.Close();
        }

        public void Send_Heartbeat(object objTh)
        {
            string sql = "", time, MessageHead;
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    #region 发送心跳
                    time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// DateTime.Now.ToString();
                    string ss = time.Substring(17, 2);
                    if (ss == "59")
                    {
                        MessageHead = "BD21000";                      
                        string str = MessageHead + " &" + time + " &"+ time + " &";
                        byte[] sendArrayT = System.Text.Encoding.Default.GetBytes(str);
                        sendArrayT = ByteReplace(sendArrayT, OldBytes, NewBytes);

                        try
                        {
                            Client_MES.SendData(sendArrayT);
                        }
                        catch
                        {
                            ReconnectMES(Client_MES);
                        }
                        sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")),"发送心跳数据"+ str);
                        tm.MultithreadExecuteNonQuery(sql);
                    }

                    #endregion
                   
                    if(!Client_MES.socketClient.Connected)
                    {
                        ReconnectMES(Client_MES);
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
        public void Send_Result(object objTh)
        {           
            string sql = "", time, MessageHead;
            DbDataReader dr = null;
            DataTable dt = null;
            while (true)
            {
                Thread.Sleep(1000);
                
                try
                {
                    if (messagecls.GetMsgID(11,1) == 1)//plc打捆结果
                    {
                        double MAXRECID = messagecls.GetMsgID(12);// PLANIDNow = 0;                
                        messagecls.LabelData LabelDataASK;
                        double REC_ID = 0, SEQ_L2 = 0;
                        MessageHead = "BD21001";
                        time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sql = string.Format("select MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST,WT_BDL_ACT from TLabelContent WHERE REC_ID={0} ", MAXRECID);
                        dt = tm.MultithreadDataTable(sql);
                        
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            LabelDataASK.MACHINE_NO = dt.Rows[i]["MACHINE_NO"].ToString();
                            LabelDataASK.ID_LOT_PROD = dt.Rows[i]["ID_LOT_PROD"].ToString();
                            LabelDataASK.ID_PART_LOT = Int16.Parse(dt.Rows[i]["ID_PART_LOT"].ToString());
                            LabelDataASK.DIM_LEN = double.Parse(dt.Rows[i]["DIM_LEN"].ToString());
                            LabelDataASK.IND_FIXED = dt.Rows[i]["IND_FIXED"].ToString();
                            LabelDataASK.SEQ_SEND = double.Parse(dt.Rows[i]["SEQ_SEND"].ToString());
                            LabelDataASK.NUM_BAR = Int16.Parse(dt.Rows[i]["NUM_BAR"].ToString());
                            LabelDataASK.SEQ_LIST = double.Parse(dt.Rows[i]["SEQ_LIST"].ToString());
                            LabelDataASK.WT_BDL_ACT = double.Parse(dt.Rows[i]["WT_BDL_ACT"].ToString());

                            REC_ID = MAXRECID;
                            SEQ_L2 = messagecls.GetMsgID();
                            string text1 = MessageHead + " &" + time + " &" + LabelDataASK.MACHINE_NO + " &" + LabelDataASK.ID_LOT_PROD + " &" + LabelDataASK.ID_PART_LOT.ToString() + " &" + LabelDataASK.DIM_LEN.ToString() + " &" + LabelDataASK.IND_FIXED + " &" + LabelDataASK.SEQ_SEND.ToString() + " &" + LabelDataASK.NUM_BAR.ToString() + " &" + LabelDataASK.SEQ_LIST.ToString() + " &" + LabelDataASK.WT_BDL_ACT.ToString() + " &" + SEQ_L2.ToString() + " &" + time + " &";
                            byte[] sendArray = Encoding.Default.GetBytes(text1);
                            sendArray = ByteReplace(sendArray, OldBytes, NewBytes);
                            if (sendArray.Length > 0)
                            {
                                Client_MES.SendData(sendArray);
                                //sql = string.Format("UPDATE TLabelContent SET IMP_FINISH=0,REC_IMP_TIME='{0}' where REC_ID={1}", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), REC_ID);
                                //tm.MultithreadExecuteNonQuery(sql);
                                sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "L2发送：二级向MES发送打捆结果" + text1);
                                tm.MultithreadExecuteNonQuery(sql);
                            }
                        }
                    }
                    if (messagecls.GetMsgID(18,1) == 1)//打捆指令下发成功应答
                    {
                        double MAXRECID = messagecls.GetMsgID(19);// 虎踞接收打捆数据的REC_ID             
                        messagecls.LabelData LabelDataASK;
                        double REC_ID = 0, SEQ_L2 = 0;
                        MessageHead = "BD2101A";
                        time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sql = string.Format("select MACHINE_NO,NAME_PROD,WT_AVG_LEN_PROD,DES_FIPRO_SECTION,NAME_STLGD,ID_LOT_PROD,ID_PART_LOT,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST,L3ACK from TLabelContent WHERE REC_ID={0} ", MAXRECID);
                        dt = tm.MultithreadDataTable(sql);
                        
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            LabelDataASK.MACHINE_NO = dt.Rows[i]["MACHINE_NO"].ToString();
                            LabelDataASK.NAME_PROD= dt.Rows[i]["NAME_PROD"].ToString();
                            LabelDataASK.WT_AVG_LEN_PROD= double.Parse(dt.Rows[i]["WT_AVG_LEN_PROD"].ToString());
                            LabelDataASK.DES_FIPRO_SECTION= dt.Rows[i]["DES_FIPRO_SECTION"].ToString();
                            LabelDataASK.NAME_STLGD= dt.Rows[i]["NAME_STLGD"].ToString();
                            LabelDataASK.ID_LOT_PROD = dt.Rows[i]["ID_LOT_PROD"].ToString();
                            LabelDataASK.ID_PART_LOT = Int16.Parse(dt.Rows[i]["ID_PART_LOT"].ToString());
                            LabelDataASK.DIM_LEN = double.Parse(dt.Rows[i]["DIM_LEN"].ToString());
                            LabelDataASK.IND_FIXED = dt.Rows[i]["IND_FIXED"].ToString();
                            LabelDataASK.SEQ_SEND = double.Parse(dt.Rows[i]["SEQ_SEND"].ToString());
                            LabelDataASK.NUM_BAR = Int16.Parse(dt.Rows[i]["NUM_BAR"].ToString());
                            LabelDataASK.SEQ_LIST = double.Parse(dt.Rows[i]["SEQ_LIST"].ToString());
                            short ACK = Int16.Parse(dt.Rows[i]["L3ACK"].ToString());

                            REC_ID = MAXRECID;
                            SEQ_L2 = messagecls.GetMsgID();
                            string text1 = MessageHead + " &" + time + " &" + LabelDataASK.MACHINE_NO + " &" + LabelDataASK.NAME_PROD+" &" +LabelDataASK.WT_AVG_LEN_PROD +" &" + LabelDataASK.DES_FIPRO_SECTION+" &"+LabelDataASK.NAME_STLGD+ " &" + LabelDataASK.ID_LOT_PROD + " &" + LabelDataASK.ID_PART_LOT.ToString() + " &" + LabelDataASK.DIM_LEN.ToString() + " &" + LabelDataASK.IND_FIXED + " &" + LabelDataASK.SEQ_SEND.ToString() + " &" + LabelDataASK.NUM_BAR.ToString() + " &" + LabelDataASK.SEQ_LIST.ToString() + " &" + 1.ToString()  +" &" +""+" &"+ SEQ_L2.ToString() + " &" + time + " &";
                            byte[] sendArray = Encoding.Default.GetBytes(text1);
                            sendArray = ByteReplace(sendArray, OldBytes, NewBytes);
                            if (sendArray.Length > 0)
                            {
                                Client_MES.SendData(sendArray);
                                sql = string.Format("UPDATE TLabelContent SET L3ACK=1,REC_IMP_TIME='{0}' where REC_ID={1}", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), REC_ID);
                                tm.MultithreadExecuteNonQuery(sql);
                                sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "L2发送：TOMES发送打捆指令应答" + text1);
                                tm.MultithreadExecuteNonQuery(sql);
                            }
                        }
                    }
                    if (messagecls.GetMsgID(18,2) == 2)//打捆指令下发失败应答
                    {
                        double MAXRECID = messagecls.GetMsgID(19);// 虎踞接收打捆数据的REC_ID             
                        messagecls.LabelData LabelDataASK;
                        double REC_ID = 0, SEQ_L2 = 0;
                        MessageHead = "BD2101A";
                        time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sql = string.Format("select MACHINE_NO,NAME_PROD,WT_AVG_LEN_PROD,DES_FIPRO_SECTION,NAME_STLGD,ID_LOT_PROD,ID_PART_LOT,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST from TLabelContent WHERE REC_ID={0} ", MAXRECID);
                        dt = tm.MultithreadDataTable(sql);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            LabelDataASK.MACHINE_NO = dt.Rows[i]["MACHINE_NO"].ToString();
                            LabelDataASK.NAME_PROD = dt.Rows[i]["NAME_PROD"].ToString();
                            LabelDataASK.WT_AVG_LEN_PROD = double.Parse(dt.Rows[i]["WT_AVG_LEN_PROD"].ToString());
                            LabelDataASK.DES_FIPRO_SECTION = dt.Rows[i]["DES_FIPRO_SECTION"].ToString();
                            LabelDataASK.NAME_STLGD = dt.Rows[i]["NAME_STLGD"].ToString();
                            LabelDataASK.ID_LOT_PROD = dt.Rows[i]["ID_LOT_PROD"].ToString();
                            LabelDataASK.ID_PART_LOT = Int16.Parse(dt.Rows[i]["ID_PART_LOT"].ToString());
                            LabelDataASK.DIM_LEN = double.Parse(dt.Rows[i]["DIM_LEN"].ToString());
                            LabelDataASK.IND_FIXED = dt.Rows[i]["IND_FIXED"].ToString();
                            LabelDataASK.SEQ_SEND = double.Parse(dt.Rows[i]["SEQ_SEND"].ToString());
                            LabelDataASK.NUM_BAR = Int16.Parse(dt.Rows[i]["NUM_BAR"].ToString());
                            LabelDataASK.SEQ_LIST = double.Parse(dt.Rows[i]["SEQ_LIST"].ToString());
                            //LabelDataASK.WT_BDL_ACT = double.Parse(dt.Rows[i]["WT_BDL_ACT"].ToString());


                            REC_ID = MAXRECID;
                            SEQ_L2 = messagecls.GetMsgID();
                            string text1 = MessageHead + " &" + time + " &" + LabelDataASK.MACHINE_NO + " &" + LabelDataASK.NAME_PROD + " &" + LabelDataASK.WT_AVG_LEN_PROD + " &" + LabelDataASK.DES_FIPRO_SECTION + " &" + LabelDataASK.NAME_STLGD + " &" + LabelDataASK.ID_LOT_PROD + " &" + LabelDataASK.ID_PART_LOT.ToString() + " &" + LabelDataASK.DIM_LEN.ToString() + " &" + LabelDataASK.IND_FIXED + " &" + LabelDataASK.SEQ_SEND.ToString() + " &" + LabelDataASK.NUM_BAR.ToString() + " &" + LabelDataASK.SEQ_LIST.ToString() + " &" + 0.ToString() + " &" + "PLC工作忙！" + " &" + SEQ_L2.ToString() + " &" + time + " &";
                            byte[] sendArray = Encoding.Default.GetBytes(text1);
                            sendArray = ByteReplace(sendArray, OldBytes, NewBytes);
                            if (sendArray.Length > 0)
                            {
                                Client_MES.SendData(sendArray);
                                sql = string.Format("UPDATE TLabelContent SET L3ACK=1,REC_IMP_TIME='{0}' where REC_ID={1}", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), REC_ID);
                                tm.MultithreadExecuteNonQuery(sql);
                                sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "L2发送：TOMES发送打捆指令应答" + text1);
                                tm.MultithreadExecuteNonQuery(sql);
                            }
                        }
                    }
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
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                    Log.addLog(log, LogType.ERROR, ex.StackTrace);
                }
            }
        }
        
        public void ReconnectMES(SocketClient s)
        {
            string sql = null, str = null;
            bool connectstate = false;
            do
            {

                try
                {


                    s.socketClient.Close();
                    s.socketClient.Dispose();
                    s.CreateConnect(MESIP, mesportr);
                    connectstate = true;
                    sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=14", 1, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));
                    tm.MultithreadExecuteNonQuery(sql);

                }
                catch
                {
                    sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=14", 0, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));
                    tm.MultithreadExecuteNonQuery(sql);
                    str = "连接MES系统失败，正在尝试重新连接！";
                    sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                    tm.MultithreadExecuteNonQuery(sql);
                    connectstate = false;
                    Thread.Sleep(5000);
                }
            }
            while (!connectstate);
        }
        void ClientREC_MES()
        { }
        public void RunSINGenerate()
        {
            try
            {
                
                try
                {
                    Server_MES = new SocketServer(localip, localportr);
                    Server_MES.StarServer(MEShelper.RecMES);
                    Client_MES = new SocketClient(ClientREC_MES);                                       
                    Client_MES.CreateConnect(MESIP, mesportr);

                }
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                    Log.addLog(log, LogType.ERROR, ex.StackTrace);
                }                              
                Thread Heart = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Send_Heartbeat));
                Heart.Start(null);
                Thread Sendresult = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Send_Result));
                Sendresult.Start(null);
            }
            catch (Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
                Log.addLog(log, LogType.ERROR, ex.StackTrace);
            }
         return;
        }
   }


  
}
