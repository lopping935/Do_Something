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
                    sql = string.Format("select MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,SEQ_SEND,REC_ID from TLabelContent WHERE (IMP_FINISH=31 OR IMP_FINISH=32 OR IMP_FINISH=33) AND L3ACK=0 order by REC_ID ASC");
                    string MACHINE_NO = "";
                    string ID_LOT_PROD = "", ACK = "1", REASON = "";
                    Int16 ID_PART_LOT = 0;
                    Int16 NUM_BDL = 0;
                    Int16 SEQ_LEN = 0;
                    Int16 SEQ_OPR = 0;
                    double SEQ_SEND = 0, REC_ID=0;
                    DataTable dt = tm.MultithreadDataTable(sql);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MACHINE_NO = dt.Rows[i]["MACHINE_NO"].ToString();
                        ID_LOT_PROD = dt.Rows[i]["ID_LOT_PROD"].ToString();
                        ID_PART_LOT = Int16.Parse(dt.Rows[i]["ID_PART_LOT"].ToString());
                        NUM_BDL = Int16.Parse(dt.Rows[i]["NUM_BDL"].ToString());
                        SEQ_LEN = Int16.Parse(dt.Rows[i]["SEQ_LEN"].ToString());
                        SEQ_OPR = Int16.Parse(dt.Rows[i]["SEQ_OPR"].ToString());
                        SEQ_SEND = double.Parse(dt.Rows[i]["SEQ_SEND"].ToString());
                        REC_ID= double.Parse(dt.Rows[i]["REC_ID"].ToString());

                        MessageHead = "PRL301A";
                        time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        string str = MessageHead + " &" + MACHINE_NO + " &" + ID_LOT_PROD + " &" + ID_PART_LOT.ToString() + " &" + NUM_BDL.ToString() + " &" + SEQ_LEN.ToString() + " &" + SEQ_OPR.ToString() + " &" +SEQ_SEND.ToString()+" &"+ ACK + " &" + REASON + " &" + time + " &" + REC_ID.ToString() + " &";
                        byte[] sendArray = System.Text.Encoding.ASCII.GetBytes(str);
                        byte OldBytes = 0x20;
                        byte NewBytes = 0x7F;
                        byte[] sendArrayNew = ByteReplace(sendArray, OldBytes, NewBytes);
                        if (sendArrayNew.Length > 0)
                        {
                            MESSocketClient.senddata(sendArrayNew);
                            sql = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
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
    
        public void Send_SignsMessage()
        {try
            { 
            string sql = "select MAX(iface_id) AS iface_id from TV_D_DETAIL_B14 WHERE IMP_FINISH=1";
                        int Maxiface_id = 0;
        DataTable dt = tm.MultithreadDataTable(sql);
                        for (int i = 0; i<dt.Rows.Count; i++)
                        {
                            Maxiface_id = Convert.ToInt32(dt.Rows[i]["iface_id"].ToString());
                            }
sql = string.Format("select top 1 [FUN_NO],[STEEL_CODE_DESC],[SPEC_CP_DESC],[iface_id],[NUM],[LENGTH],[NET_WEIGHT],[LotNo],[XH],[HT_NO],[SCBZ],[MFL_DESC],[ProTime],[ItemPrint],[TaskNo] from TV_D_DETAIL_B14 WHERE IMP_FINISH!=1 AND iface_id>{0} order by iface_id ASC", Maxiface_id);
    //sql = "select top 2 SlabNO from TSlabNO WHERE PEN_FINISH!=1 order by REC_ID ASC";
    string FUN_NO = "";
    string STEEL_CODE_DESC = "";
    string SPEC_CP_DESC = "";
    Int32 iface_id = 0;
    Int16 NUM = 0;
    string LENGTH = "";
    float NET_WEIGHT = 0;
    string LotNo = "";
    string XH = "";
    string HT_NO = "";
    string SCBZ = "";
    string MFL_DESC = "";
    string ProTime = "";
    string ItemPrint = "";
    Int16 IItemPrint = 0;
    string TaskNo = "";
    dt  = tm.MultithreadDataTable(sql);
    for (int i = 0; i<dt.Rows.Count; i++)
    {
    FUN_NO = dt.Rows[i]["FUN_NO"].ToString();
    STEEL_CODE_DESC = dt.Rows[i]["STEEL_CODE_DESC"].ToString();
    SPEC_CP_DESC = dt.Rows[i]["SPEC_CP_DESC"].ToString();
    iface_id = Int32.Parse(dt.Rows[i]["iface_id"].ToString());
    NUM = Int16.Parse(dt.Rows[i]["NUM"].ToString());
    NET_WEIGHT = float.Parse(dt.Rows[i]["NET_WEIGHT"].ToString())*1000;
    LENGTH = dt.Rows[i]["LENGTH"].ToString();
    LotNo = dt.Rows[i]["LotNo"].ToString();
    XH = dt.Rows[i]["XH"].ToString();
    HT_NO = dt.Rows[i]["HT_NO"].ToString();
    SCBZ = dt.Rows[i]["SCBZ"].ToString();
    MFL_DESC = dt.Rows[i]["MFL_DESC"].ToString();
    ProTime = dt.Rows[i]["ProTime"].ToString();
    ItemPrint = dt.Rows[i]["ItemPrint"].ToString();
    TaskNo = dt.Rows[i]["TaskNo"].ToString();
}
                        switch (ItemPrint)
                        {
                            case "":
                                IItemPrint = 1;
                                break;
                            case "DEFORMED BAR":
                                IItemPrint = 2;
                                break;
                            case "HOT ROLLED ALLOY STEEL DEFORMED BAR":
                                IItemPrint = 3;
                                break;
                            case "HOT ROLLED RIBBED STEEL BAR":
                                IItemPrint = 4;
                                break;
                            case "钢筋混凝土用钢筋":
                                IItemPrint = 5;
                                break;
                            case "钢筋混凝土用热轧带肋钢筋":
                                IItemPrint = 6;
                                break;
                            case "预应力混凝土用螺纹钢筋":
                                IItemPrint = 7;
                                break;
                            default:
                                IItemPrint = 0;
                                break;
                        }
                        //Int16 alpha = 1000;
Int16 INET_WEIGHT = (Int16)( NET_WEIGHT) ;
byte[] sendArray = Enumerable.Repeat((byte)0x20, 241).ToArray();
byte[] byteArray1 = BitConverter.GetBytes(IItemPrint);
byte[] byteArray2 = System.Text.Encoding.ASCII.GetBytes(SCBZ+"/"+STEEL_CODE_DESC);
byte[] byteArray3 = System.Text.Encoding.ASCII.GetBytes(HT_NO);
byte[] byteArray4 = System.Text.Encoding.ASCII.GetBytes(FUN_NO);
byte[] byteArray5 = System.Text.Encoding.ASCII.GetBytes(SPEC_CP_DESC+"X"+ LENGTH);
byte[] byteArray6 = BitConverter.GetBytes(NUM);
byte[] byteArray7 = System.Text.Encoding.ASCII.GetBytes(ProTime + "/" +DateTime.Now.Hour.ToString());
byte[] byteArray8 = BitConverter.GetBytes(INET_WEIGHT);
byte[] byteArray9 = BitConverter.GetBytes(iface_id);
string bstr = "00000010";
sendArray[0] = Convert.ToByte(bstr, 2);
string bstr1 = "00000000";
sendArray[240] = Convert.ToByte(bstr1, 2);
                        Buffer.BlockCopy(byteArray1, 0, sendArray, 2, byteArray1.Length);
                        Buffer.BlockCopy(byteArray2, 0, sendArray, 4, byteArray2.Length);
                        Buffer.BlockCopy(byteArray3, 0, sendArray, 36, byteArray3.Length);
                        Buffer.BlockCopy(byteArray4, 0, sendArray, 68, byteArray4.Length);
                        Buffer.BlockCopy(byteArray5, 0, sendArray, 90, byteArray5.Length);
                        Buffer.BlockCopy(byteArray6, 0, sendArray, 112, byteArray6.Length);
                        Buffer.BlockCopy(byteArray7, 0, sendArray, 114, byteArray7.Length);
                        Buffer.BlockCopy(byteArray8, 0, sendArray, 136, byteArray8.Length);
                        Buffer.BlockCopy(byteArray9, 0, sendArray, 138, byteArray9.Length);
                if (sendArray.Length > 0)
                {
                    //SocketClient.senddata(PDResult);连接相机恢复
                    MESSocketClient.senddata(sendArray);
                    string str = bstr + " " + SCBZ + "/" + STEEL_CODE_DESC + " " + HT_NO + " " + FUN_NO + " " + SPEC_CP_DESC + "X" + LENGTH + " " + NUM.ToString() + " " + ProTime + "/" + DateTime.Now.Hour.ToString() + " " + INET_WEIGHT.ToString() + " "+iface_id.ToString()+" "+ bstr1;
                    //sqlTemp.Add("INSERT INTO [EquipmentAttributeValues]([EquipmentAttribute_id],[AttributeValues_Time],[AttributeValues_Value]) VALUES ({0},'{1}','{2}')");
                    sql = string.Format("INSERT INTO SENDLOG(REC_CREATE_TIME,CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                    tm.MultithreadExecuteNonQuery(sql);
                }
            
}
            catch (Exception ex)
            {
                log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                Log.addLog(log, LogType.ERROR, ex.Message);
                Log.addLog(log, LogType.ERROR, ex.StackTrace);
            }//sendArray[38] = 50;(byte)'1';
            
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
                        mesportr = Convert.ToInt16(dr["DATAACQUISITION_PORTRS"]);
                    }
                    if (Convert.ToInt16(dr["ACQUISITIONCONFIG_ID"]) == 4)
                    {
                        localip = Convert.ToString(dr["DATAACQUISITION_IP"]);
                        localportr = Convert.ToInt32(dr["DATAACQUISITION_PORTR"]);
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
