using System;
using System.Collections.Generic;
using SQLPublicClass;
using System.Data.Common;
using System.Threading;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using CoreAlgorithmMES;
namespace CoreAlgorithm.TaskManager
{
   public class StripIronNum
    {
        TasksManager tm;
        static string MESIP = "", localip = "";//400PLC ip
        static int  mesportr =0, localportr =0;//400PLC端口
        INIClass ini = new INIClass(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
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

        public void do_SendMessage(object objTh)
        {
            string sql ="", time, MessageHead;
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
                        sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                        tm.MultithreadExecuteNonQuery(sql);
                    }
                    #endregion
                    #region 三级应答反馈
                    sql = string.Format("select MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,SEQ_SEND,NUM_BAR,SEQ_LIST,LA_BDL_ACT,NO_LICENCE,Name_PROD,Name_STND,ID_HEAT,Name_STLGD,DES_FIPRO_SECTION,ID_CREW_RL,ID_CREW_CK,TMSTP_WEIGH,BAR_CODE,NUM_HEAD,NUM_TAIL,REC_ID from TLabelContent WHERE (IMP_FINISH=31 OR IMP_FINISH=32 OR IMP_FINISH=33) AND L3ACK=0 order by REC_ID ASC");
                    messagecls.LabelData  LabelDataASK;
                    string  ACK = "1", REASON="";
                    double  REC_ID=0;
                    MessageHead = "PRL301A";
                    time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    DataTable dt = tm.MultithreadDataTable(sql);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LabelDataASK.MACHINE_NO = dt.Rows[i]["MACHINE_NO"].ToString();
                        LabelDataASK.ID_LOT_PROD = dt.Rows[i]["ID_LOT_PROD"].ToString();
                        LabelDataASK.ID_PART_LOT = Int16.Parse(dt.Rows[i]["ID_PART_LOT"].ToString());
                        LabelDataASK.NUM_BDL = Int16.Parse(dt.Rows[i]["NUM_BDL"].ToString());
                        LabelDataASK.SEQ_LEN = Int16.Parse(dt.Rows[i]["SEQ_LEN"].ToString());
                        LabelDataASK.SEQ_OPR = Int16.Parse(dt.Rows[i]["SEQ_OPR"].ToString());
                        LabelDataASK.SEQ_SEND = double.Parse(dt.Rows[i]["SEQ_SEND"].ToString());
                        LabelDataASK.NUM_BAR= Int16.Parse(dt.Rows[i]["NUM_BAR"].ToString());
                        LabelDataASK.SEQ_LIST= Int16.Parse(dt.Rows[i]["SEQ_LIST"].ToString());
                        LabelDataASK.LA_BDL_ACT= double.Parse(dt.Rows[i]["LA_BDL_ACT"].ToString());
                        LabelDataASK.NO_LICENCE= dt.Rows[i]["NO_LICENCE"].ToString();
                        LabelDataASK.NAME_PROD= dt.Rows[i]["NAME_PROD"].ToString();
                        LabelDataASK.NAME_STND= dt.Rows[i]["NAME_STND"].ToString();//gbk
                        LabelDataASK.ID_HEAT= dt.Rows[i]["ID_HEAT"].ToString();
                        LabelDataASK.NAME_STLGD= dt.Rows[i]["NAME_STLGD"].ToString();
                        LabelDataASK.DES_FIPRO_SECTION= dt.Rows[i]["DES_FIPRO_SECTION"].ToString();
                        LabelDataASK.ID_CREW_RL=dt.Rows[i]["ID_CREW_RL"].ToString();//gbk
                        LabelDataASK.ID_CREW_CK= dt.Rows[i]["ID_CREW_CK"].ToString();//gbk
                        LabelDataASK.TMSTP_WEIGH=dt.Rows[i]["TMSTP_WEIGH"].ToString();
                        LabelDataASK.BAR_CODE= dt.Rows[i]["BAR_CODE"].ToString();
                        LabelDataASK.NUM_HEAD= Int16.Parse(dt.Rows[i]["NUM_HEAD"].ToString());
                        LabelDataASK.NUM_TAIL= Int16.Parse(dt.Rows[i]["NUM_TAIL"].ToString());
                        REC_ID = double.Parse(dt.Rows[i]["REC_ID"].ToString());
                        
                        string str1 = MessageHead + " &" + LabelDataASK.MACHINE_NO + " &" + LabelDataASK.ID_LOT_PROD + " &" + LabelDataASK.ID_PART_LOT.ToString() + " &" + LabelDataASK.NUM_BDL.ToString() + " &" + LabelDataASK.SEQ_LEN.ToString() + " &" + LabelDataASK.SEQ_OPR.ToString() + " &" + LabelDataASK.SEQ_SEND.ToString()+" &"+ LabelDataASK.NUM_BAR.ToString() + " &" + LabelDataASK.SEQ_LIST.ToString() + " &" + LabelDataASK.LA_BDL_ACT.ToString() + " &" + LabelDataASK.NO_LICENCE + " &" + LabelDataASK.NAME_PROD + " &";
                        string str3 = " &" + LabelDataASK.ID_HEAT+ " &" + LabelDataASK.NAME_STLGD+ " &" + LabelDataASK.DES_FIPRO_SECTION+" &" ;
                        string str6 = " &" + LabelDataASK.TMSTP_WEIGH.ToString() + " &"+ LabelDataASK.BAR_CODE + " &" + LabelDataASK.NUM_HEAD + " &" + LabelDataASK.NUM_TAIL + " &" + ACK + " &" + REASON + " &" + time + " &" + REC_ID.ToString() + " &";
                        byte OldBytes = 0x20;
                        byte NewBytes = 0x7F;
                        byte[] sendArray1 = System.Text.Encoding.ASCII.GetBytes(str1);
                        sendArray1 = ByteReplace(sendArray1, OldBytes, NewBytes);
                        byte[] sendArray2 = System.Text.Encoding.GetEncoding("GBK").GetBytes(LabelDataASK.NAME_STND);
                        byte[] sendArray3 = System.Text.Encoding.ASCII.GetBytes(str3);
                        sendArray3 = ByteReplace(sendArray3, OldBytes, NewBytes);
                        byte[] sendArray4_1 = System.Text.Encoding.GetEncoding("GBK").GetBytes(LabelDataASK.ID_CREW_RL );
                        byte[] sendArray4= Enumerable.Repeat((byte)0x20, sendArray4_1.Length+2).ToArray();
                        Array.Copy(sendArray4_1, sendArray4, sendArray4_1.Length);
                        sendArray4[sendArray4_1.Length] = 0x7F;
                        sendArray4[sendArray4_1.Length + 1] = 0x26;
                        byte[] sendArray5 = System.Text.Encoding.GetEncoding("GBK").GetBytes(LabelDataASK.ID_CREW_CK);
                        byte[] sendArray6 = System.Text.Encoding.ASCII.GetBytes(str6);
                        sendArray6 = ByteReplace(sendArray6, OldBytes, NewBytes);
                        byte[] sendArray= Enumerable.Repeat((byte)0x20, sendArray1.Length + sendArray2.Length + sendArray3.Length + sendArray4.Length + sendArray5.Length + sendArray6.Length).ToArray();
                        //sendArray1.CopyTo(sendArray, 0);
                        //sendArray2.CopyTo(sendArray,sendArray1.Length);
                        //sendArray3.CopyTo(sendArray, sendArray1.Length+ sendArray2.Length);
                        //sendArray4.CopyTo(sendArray, sendArray1.Length + sendArray2.Length+ sendArray3.Length);
                        //sendArray5.CopyTo(sendArray, sendArray1.Length + sendArray2.Length+ sendArray3.Length + sendArray4.Length);
                        //sendArray6.CopyTo(sendArray, sendArray1.Length + sendArray2.Length + sendArray3.Length + sendArray4.Length+sendArray5.Length);
                        List<byte> byteSource = new List<byte>();
                        byteSource.AddRange(sendArray1);
                        byteSource.AddRange(sendArray2);
                        byteSource.AddRange(sendArray3);
                        byteSource.AddRange(sendArray4);
                        byteSource.AddRange(sendArray5);
                        byteSource.AddRange(sendArray6);
                        byte[] data = byteSource.ToArray();

                        if (sendArray.Length > 0)
                        {
                            MESSocketClient.senddata(data);
                            sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str6);
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
                        //sql = string.Format("insert into HLabelContent SELECT MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST,LA_BDL_ACT,NO_LICENCE,NAME_PROD,NAME_STND,ID_HEAT,NAME_STLGD,DES_FIPRO_SECTION,ID_CREW_RL,ID_CREW_CK,TMSTP_WEIGH,BAR_CODE,NUM_HEAD,NUM_TAIL,L3TMSTP_SEND,REC_CREATE_TIME FROM TLabelContent WHERE REC_ID<{0}", (MaxRecID - 199));
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

        public void ReconnectMES()
        {
            string sql = null, str = null;
            bool connectstate = false;
            do
            {

                try
                {

                    connectstate = MESSocketClient.IsSocketConnected();
                    MESSocketClient.socketClient.Close();
                    MESSocketClient.socketClient.Dispose();
                    MESSocketClient.CreateConnect(MESIP, mesportr);


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
                bool connectstate = false;
                string sql = "SELECT ACQUISITIONCONFIG_ID,DATAACQUISITION_IP,DATAACQUISITION_PORTS,DATAACQUISITION_PORTR FROM ACQUISITIONCONFIG where ACQUISITIONCONFIG_ID=8 or ACQUISITIONCONFIG_ID=4";
                DbDataReader dr = null;
                dr = tm.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 8)
                    {
                        MESIP = Convert.ToString(dr["DATAACQUISITION_IP"]);
                        mesportr = Convert.ToInt32(dr["DATAACQUISITION_PORTR"]);
                    }
                    if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 4)
                    {
                        localip = Convert.ToString(dr["DATAACQUISITION_IP"]);
                        localportr = Convert.ToInt32(dr["DATAACQUISITION_PORTS"]);
                    }
                }
                dr.Close();
                MESSocketServer.CreateSocket(localip, localportr);
                do
                {

                    try
                    {
                        MESSocketClient.CreateConnect(MESIP, mesportr);
                        connectstate = MESSocketClient.IsSocketConnected();
                    }
                    catch
                    {
                        MESSocketClient.socketClient.Close();
                        MESSocketClient.socketClient.Dispose();
                        string str = "连接MES系统失败，正在尝试重新连接！";
                        sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                        tm.MultithreadExecuteNonQuery(sql);
                        connectstate = false;
                        Thread.Sleep(5000);
                    }
                }
                while (!connectstate);
                Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(do_SendMessage));
                th.Start(null);
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
