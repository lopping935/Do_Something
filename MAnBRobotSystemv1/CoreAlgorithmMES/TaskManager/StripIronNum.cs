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
namespace CoreAlgorithm.TaskManager
{
   public class StripIronNum
    {
        TasksManager tm;
        static string MESIP = "", localip = "";//400PLC ip
        static int  mesportr =0, localportr =0;//400PLC端口
        static double lastmodel = -1;
        INIClass ini = new INIClass(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        public static object locker = new object();
        byte OldBytes = 0x20;
        byte NewBytes = 0x7F;

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
        }

        public void Do_Askmsg()
        {//Flag标识含义;0：无工作  1：三级请求数据  2：确定二级是否已经收到mes数据 3：已接受到数据
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


                    if (a == 1)//向mes请求数据
                    {
                        sql = "update S_TFlag set Flag=2 where ID=1";
                        int b = tm.MultithreadExecuteNonQuery(sql);

                        #region socket发送请求                     

                        string ASK_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        double msgid = MESSocketServer.GetMsgID();
                        string str = "LA21008"+" &"+ASK_TIME+" &"+ msgid+" &"+ ASK_TIME+" $";//MessageHead.Trim() + " &" + TMSTP_SEND + " &" + MACHINE_NO + " &" + ID_LOT_PROD + " &" + ID_PART_LOT.ToString() + " &" + NUM_BDL.ToString() + " &" + SEQ_LEN.ToString() + " &" + SEQ_OPR.ToString() + " &" + ACK.ToString() + " &" + REASON + " &" + TMSTP_SEND + " &" + msgid.ToString() + " &";
                        byte[] sendArray = System.Text.Encoding.Default.GetBytes(str);
                        ByteReplace(sendArray, OldBytes, NewBytes);
                        if (sendArray.Length > 0)
                        {
                            MESSocketClient.senddata(sendArray);
                            sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "发送l2->l3请求数据！" + str);
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

        public void model_change()//模式切换
        {
            try
            {
                lock(Program.obj)
                { 
                    double MAXRECID = 0;// PLANIDNow = 0;                
                    string sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";
                    DbDataReader dr = null;
                    dr = tm.MultithreadDataReader(sql);
                    while (dr.Read())
                    {
                        if (dr["REC_ID"] != DBNull.Value)
                            MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
                    }
                    dr.Close();
                    sql = string.Format("select top 1 MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,REC_ID from TLabelContent  WHERE REC_ID>={0} AND L3ACK=0 and (IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33) order by REC_ID desc", MAXRECID);
                    string MessageHead = "LA21002", MACHINE_NO = "", ID_LOT_PROD="", REASON="";
                    short ID_PART_LOT = 0, NUM_BDL = 0, SEQ_LEN = 0, SEQ_OPR = 0, ACK = 1;
                    string TMSTP_SEND = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    double REC_ID = 0;
                    double msgid = 0;
                    dr = tm.MultithreadDataReader(sql);
                    while (dr.Read())
                    {
                       MACHINE_NO = dr["MACHINE_NO"].ToString();
                       ID_LOT_PROD = dr["ID_LOT_PROD"].ToString();
                       ID_PART_LOT = Int16.Parse(dr["ID_PART_LOT"].ToString());
                       NUM_BDL = Int16.Parse(dr["NUM_BDL"].ToString());
                       SEQ_LEN = Int16.Parse(dr["SEQ_LEN"].ToString());
                       SEQ_OPR = Int16.Parse(dr["SEQ_OPR"].ToString());
                      // REC_ID = double.Parse(dr["REC_ID"].ToString());
                    }
                    msgid = MESSocketServer.GetMsgID();
                    string str = MessageHead.Trim() + " &" + TMSTP_SEND + " &" + MACHINE_NO + " &" + ID_LOT_PROD + " &" + ID_PART_LOT.ToString() + " &" + NUM_BDL.ToString() + " &" + SEQ_LEN.ToString() + " &" + SEQ_OPR.ToString() + " &" +ACK.ToString()+ " &"+REASON+ " &"+TMSTP_SEND+ " &"+ msgid.ToString()+ " &";
                    byte[] sendArray = System.Text.Encoding.Default.GetBytes(str);
                    ByteReplace(sendArray, OldBytes, NewBytes);
                    if (sendArray.Length > 0)
                    {
                        MESSocketClient.senddata(sendArray);
                        sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "发送l2->l3模式切换"+str);
                        tm.MultithreadExecuteNonQuery(sql);
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
                        MessageHead = "LA21000";                      
                        string str = MessageHead + " &" + time + " &"+ time + " &";
                        byte[] sendArrayT = System.Text.Encoding.Default.GetBytes(str);
                        sendArrayT = ByteReplace(sendArrayT, OldBytes, NewBytes);

                        try
                        {
                            MESSocketClient.senddata(sendArrayT);
                        }
                        catch
                        {
                            ReconnectMES();
                        }
                        sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")),"发送心跳数据"+ str);
                        tm.MultithreadExecuteNonQuery(sql);
                    }

                    #endregion
                    bool connectstate = MESSocketClient.IsSocketConnected();
                    if(!connectstate)
                    {
                        ReconnectMES();
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

        static double modelflag = -1;
        public void Send_Result(object objTh)
        {
            
            string sql = "", time, MessageHead;
            DbDataReader dr = null;
            DataTable dt = null;
            while (true)
            {
                Thread.Sleep(1000);
                #region 三级应答反馈 标签结果应答
                try
                {
                    lastmodel = modelflag;
                    #region 模式
                    // PLANIDNow = 0;  
                    sql = string.Format("select * from SYSPARAMETER  where PARAMETER_ID=15");
                    dr = tm.MultithreadDataReader(sql);
                    while (dr.Read())
                    {
                        if (dr["PARAMETER_VALUE"] != DBNull.Value)
                            modelflag = Convert.ToDouble(dr["PARAMETER_VALUE"].ToString());
                    }
                    dr.Close();
                    
                    #endregion
                    if(modelflag==1&& lastmodel==0)
                    {
                        
                        model_change();
                        Thread.Sleep(5000);

                    }
                    if(modelflag==1)//自动模式下上传结果
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
                        sql = string.Format("select top 1 MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,SEQ_SEND,NUM_BAR,SEQ_LIST,LA_BDL_ACT,NO_LICENCE,Name_PROD,Name_STND,ID_HEAT,Name_STLGD,DES_FIPRO_SECTION,ID_CREW_RL,ID_CREW_CK,TMSTP_WEIGH,BAR_CODE,NUM_HEAD,NUM_TAIL,REC_ID,MODELSWACK from TLabelContent WHERE REC_ID>={0} AND L3ACK=0 and (IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33) order by REC_ID desc", MAXRECID);
                        messagecls.LabelData LabelDataASK;
                        string ACK = "1", REASON = "";
                        double msgid = 0,REC_ID=0;
                        string MODELSWACK = "";
                        MessageHead = "LA21001";
                        time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dt = tm.MultithreadDataTable(sql);
                        if(dt.Rows.Count > 0)
                        {
                             MODELSWACK = dt.Rows[0]["MODELSWACK"].ToString();
                        }
                    
                        if (MODELSWACK=="0")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                LabelDataASK.MACHINE_NO = dt.Rows[i]["MACHINE_NO"].ToString();
                                LabelDataASK.ID_LOT_PROD = dt.Rows[i]["ID_LOT_PROD"].ToString();
                                LabelDataASK.ID_PART_LOT = Int16.Parse(dt.Rows[i]["ID_PART_LOT"].ToString());
                                LabelDataASK.NUM_BDL = Int16.Parse(dt.Rows[i]["NUM_BDL"].ToString());
                                LabelDataASK.SEQ_LEN = Int16.Parse(dt.Rows[i]["SEQ_LEN"].ToString());
                                LabelDataASK.SEQ_OPR = Int16.Parse(dt.Rows[i]["SEQ_OPR"].ToString());
                                LabelDataASK.SEQ_SEND = double.Parse(dt.Rows[i]["SEQ_SEND"].ToString());
                                LabelDataASK.NUM_BAR = Int16.Parse(dt.Rows[i]["NUM_BAR"].ToString());
                                LabelDataASK.SEQ_LIST = Int16.Parse(dt.Rows[i]["SEQ_LIST"].ToString());
                                LabelDataASK.LA_BDL_ACT = double.Parse(dt.Rows[i]["LA_BDL_ACT"].ToString());
                                LabelDataASK.NO_LICENCE = dt.Rows[i]["NO_LICENCE"].ToString();
                                LabelDataASK.NAME_PROD = dt.Rows[i]["NAME_PROD"].ToString();//gbk
                                LabelDataASK.NAME_STND = dt.Rows[i]["NAME_STND"].ToString();
                                LabelDataASK.ID_HEAT = dt.Rows[i]["ID_HEAT"].ToString();
                                LabelDataASK.NAME_STLGD = dt.Rows[i]["NAME_STLGD"].ToString();
                                LabelDataASK.DES_FIPRO_SECTION = dt.Rows[i]["DES_FIPRO_SECTION"].ToString();
                                LabelDataASK.ID_CREW_RL = dt.Rows[i]["ID_CREW_RL"].ToString();//gbk
                                LabelDataASK.ID_CREW_CK = dt.Rows[i]["ID_CREW_CK"].ToString();//gbk
                                LabelDataASK.TMSTP_WEIGH = dt.Rows[i]["TMSTP_WEIGH"].ToString();
                                LabelDataASK.BAR_CODE = dt.Rows[i]["BAR_CODE"].ToString();
                                LabelDataASK.NUM_HEAD = Int16.Parse(dt.Rows[i]["NUM_HEAD"].ToString());
                                LabelDataASK.NUM_TAIL = Int16.Parse(dt.Rows[i]["NUM_TAIL"].ToString());
                                REC_ID= double.Parse(dt.Rows[i]["REC_ID"].ToString());
                                msgid = MESSocketServer.GetMsgID();

                                string text1 = MessageHead + " &" + time + " &" + LabelDataASK.MACHINE_NO + " &" + LabelDataASK.ID_LOT_PROD + " &" + LabelDataASK.ID_PART_LOT.ToString() + " &" + LabelDataASK.NUM_BDL.ToString() + " &" + LabelDataASK.SEQ_LEN.ToString() + " &" + LabelDataASK.SEQ_OPR.ToString() + " &" + LabelDataASK.SEQ_SEND.ToString() + " &" + LabelDataASK.NUM_BAR.ToString() + " &" + LabelDataASK.SEQ_LIST.ToString() + " &" + LabelDataASK.LA_BDL_ACT.ToString() + " &" + LabelDataASK.NO_LICENCE + " &" + LabelDataASK.NAME_PROD + " &";
                                string text2 = LabelDataASK.NAME_STND + " &" + LabelDataASK.ID_HEAT + " &" + LabelDataASK.NAME_STLGD + " &" + LabelDataASK.DES_FIPRO_SECTION + " &" + LabelDataASK.ID_CREW_RL + " &" + LabelDataASK.ID_CREW_CK + " &" + LabelDataASK.TMSTP_WEIGH.ToString() + " &" + LabelDataASK.BAR_CODE + " &" + LabelDataASK.NUM_HEAD + " &" + LabelDataASK.NUM_TAIL + " &" + ACK + " &" + REASON + " &" + time + " &" + msgid.ToString() + " &";
                                byte[] sendArray = Encoding.Default.GetBytes(text1 + text2);
                                sendArray = ByteReplace(sendArray, OldBytes, NewBytes);
                                if (sendArray.Length > 0)
                                {
                                    MESSocketClient.senddata(sendArray);
                                    sql = string.Format("UPDATE TLabelContent SET MODELSWACK='{0}',L3ACKTMSTP_SEND='{1}' where REC_ID={2}", "1",DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")),REC_ID);
                                    tm.MultithreadExecuteNonQuery(sql);
                                    sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "发送标签结果应答" + text1);
                                    tm.MultithreadExecuteNonQuery(sql);
                                    Thread.Sleep(5000);
                                }
                            }
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
                        //sql = string.Format("insert into HLabelContent SELECT MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST,LA_BDL_ACT,NO_LICENCE,NAME_PROD,NAME_STND,ID_HEAT,NAME_STLGD,DES_FIPRO_SECTION,ID_CREW_RL,ID_CREW_CK,TMSTP_WEIGH,BAR_CODE,NUM_HEAD,NUM_TAIL,L3TMSTP_SEND,REC_CREATE_TIME FROM TLabelContent WHERE REC_ID<{0}", (MaxRecID - 199));
                        sql = string.Format("insert into HLabelContent SELECT * FROM TLabelContent WHERE REC_ID<{0}", (MaxRecID - 199));
                        tm.MultithreadExecuteNonQuery(sql);
                        sql = string.Format("DELETE FROM  TLabelContent WHERE REC_ID<{0}", (MaxRecID - 199));
                        tm.MultithreadExecuteNonQuery(sql);
                    }
                        #endregion
                }
                catch(Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                    Log.addLog(log, LogType.ERROR, ex.StackTrace);
                }
            }
        }

        public  Double GetMsgID()
        {
            string sql = "";
            Double Id = 0;
            try
            {
                sql = "select PARAMETER_VALUE from SYSPARAMETER where PARAMETER_ID=12";
                DbDataReader dr = tm.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if (dr["PARAMETER_VALUE"] != DBNull.Value)
                        Id = Convert.ToInt64(dr["PARAMETER_VALUE"]) + 1;
                }
                dr.Close();
                sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=10", Id, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));
                tm.MultithreadExecuteNonQuery(sql);
                return Id;
            }
            catch (Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
                return 0;
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

                    //connectstate = MESSocketClient.IsSocketConnected();
                    MESSocketClient.socketClient.Close();
                    MESSocketClient.socketClient.Dispose();
                    MESSocketClient.CreateConnect(MESIP, mesportr);
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

        public void RunSINGenerate()
        {
            try
            {
                bool connectstate = false;
                string sql = "SELECT ACQUISITIONCONFIG_ID,DATAACQUISITION_IP,DATAACQUISITION_PORTS,DATAACQUISITION_PORTR FROM ACQUISITIONCONFIG where ACQUISITIONCONFIG_ID=8 or ACQUISITIONCONFIG_ID=18";
                DbDataReader dr = null;
                dr = tm.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 8)
                    {
                        MESIP = Convert.ToString(dr["DATAACQUISITION_IP"]);
                        mesportr = Convert.ToInt32(dr["DATAACQUISITION_PORTR"]);
                    }
                    if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 18)
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
                        connectstate = MESSocketClient.IsSocketConnected();
                        sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=14", 1, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));
                        tm.MultithreadExecuteNonQuery(sql);
                    }
                    catch
                    {
                        MESSocketClient.socketClient.Close();
                        MESSocketClient.socketClient.Dispose();
                        sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=14", 0, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));
                        tm.MultithreadExecuteNonQuery(sql);
                        string str = "连接MES系统失败，正在尝试重新连接！";
                        sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                        tm.MultithreadExecuteNonQuery(sql);
                        connectstate = false;
                        Thread.Sleep(5000);
                    }

                Thread Heart = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Send_Heartbeat));
                Heart.Start(null);
                Thread Sendresult = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Send_Result));
                Sendresult.Start(null);
                Thread AskMESDate = new Thread(Do_Askmsg);
                AskMESDate.Start();
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
