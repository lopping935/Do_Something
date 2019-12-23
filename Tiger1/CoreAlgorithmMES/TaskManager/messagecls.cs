﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using CoreAlgorithm.TaskManager;
using SQLPublicClass;
using System.Data.Common;
using System.Reflection;
namespace CoreAlgorithmMES
{
    public class messagecls
    {
        static TasksManager tm;
        public enum EncodingType { UTF7, UTF8, UTF32, Unicode, BigEndianUnicode, ASCII, GB2312, GBK, ISO8859 ,defaul};
        public struct LabelData
        {
            public string MACHINE_NO;// 打包机组号
            public string ID_TIME;
            public string ID_LOT_PROD;//生产批号
            public Int16 ID_PART_LOT; //分批号            
            public double DIM_LEN; //米长
            public string IND_FIXED;// 定尺标志
            public double SEQ_SEND;// 下发顺序号
            public Int16 NUM_BAR;// 捆内支数
            public double SEQ_LIST;// 排列序号
            public double WT_BDL_ACT;// 重量           
            public string TMSTP_SEND;// 发送时间
            public string ID_PERSON;//下发人
        };
        public messagecls()
        {
            tm = new TasksManager();
        }
        public static Double GetMsgID(int ID)
        {
            string sql = "";
            Double Id = 0;
            try
            {
                sql = string.Format("select PARAMETER_VALUE from SYSPARAMETER where PARAMETER_ID={0}", ID);
                DbDataReader dr = tm.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if (dr["PARAMETER_VALUE"] != DBNull.Value)
                        Id = Convert.ToInt64(dr["PARAMETER_VALUE"]);
                }
                dr.Close();
                sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID={2}", 0, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), ID);
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
        public static Double GetMsgID()
        {
            string sql = "";
            Double Id = 0;
            try
            {
                sql = "select PARAMETER_VALUE from SYSPARAMETER where PARAMETER_ID=9";
                DbDataReader dr = tm.MultithreadDataReader(sql);
                while (dr.Read())
                {
                    if (dr["PARAMETER_VALUE"] != DBNull.Value)
                        Id = Convert.ToInt64(dr["PARAMETER_VALUE"]) + 1;
                }
                dr.Close();
                sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=12", Id, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));
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
        public void RecMES(object SocketClient)
        {

            byte OldBytes = 0x20;
            byte NewBytes = 0x7F;
            Socket connect = SocketClient as Socket;
            messagecls msq = new messagecls();
            string sql = "";
            while (true)
            {
                //Thread.Sleep(1000);
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

                        byte[] searchBytes = new byte[] { 0x7F, 0x26 };
                        List<byte[]> HeadIndex = new List<byte[]>();
                        HeadIndex = nByteIndexOf(buffer, searchBytes);
                        string MessageFlg = GetString(HeadIndex[0], EncodingType.GB2312);//System.Text.Encoding.GB2312.GetString(buffer.Skip(0).Take(HeadIndex + 1).ToArray());



                        if (MessageFlg == "21BD000")//心跳信息，校准时间
                        {
                            string MesTime = GetString(HeadIndex[1], EncodingType.GB2312);//System.Text.Encoding.ASCII.GetString(buffer.Skip(0).Take(HeadIndex + 1).ToArray());
                            string str = MessageFlg.ToString() + " " + MesTime;
                            sql = string.Format("INSERT INTO MESRECVLOG(REC_CREATE_TIME,RECV_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), str);
                            tm.MultithreadExecuteNonQuery(sql);
                        }
                        if (MessageFlg == "21BD001")//打捆指令
                        {
                            messagecls.LabelData LabelDataRecv;
                            string reson = "";
                            Int16 ACK = 0;
                            double ProductIDA = 0;
                            string str = "";
                            Double msgid = 0;
                            #region l3->l2接收数据标签
                            try
                            {
                                LabelDataRecv.ID_TIME = GetString(HeadIndex[1], EncodingType.GB2312);
                                LabelDataRecv.MACHINE_NO = GetString(HeadIndex[2], EncodingType.GB2312);
                                LabelDataRecv.ID_LOT_PROD = GetString(HeadIndex[3], EncodingType.GB2312);
                                LabelDataRecv.ID_PART_LOT = Convert.ToInt16(GetString(HeadIndex[4], EncodingType.GB2312));                                
                                LabelDataRecv.DIM_LEN = Convert.ToDouble(GetString(HeadIndex[5], EncodingType.GB2312));
                                LabelDataRecv.IND_FIXED = GetString(HeadIndex[6], EncodingType.GB2312);
                                LabelDataRecv.SEQ_SEND = Convert.ToDouble(GetString(HeadIndex[7], EncodingType.GB2312));
                                LabelDataRecv.NUM_BAR = Convert.ToInt16(GetString(HeadIndex[8], EncodingType.GB2312));
                                LabelDataRecv.SEQ_LIST = Convert.ToDouble(GetString(HeadIndex[9], EncodingType.GB2312));                                
                                LabelDataRecv.TMSTP_SEND = GetString(HeadIndex[10], EncodingType.GB2312);
                                LabelDataRecv.ID_PERSON= GetString(HeadIndex[11], EncodingType.GB2312);
                                ACK = 1;
                                reson = "";
                                try
                                {
                                    sql = "select PARAMETER_VALUE from SYSPARAMETER where PARAMETER_ID=10";
                                    DbDataReader dr = tm.MultithreadDataReader(sql);

                                    while (dr.Read())
                                    {
                                        if (dr["PARAMETER_VALUE"] != DBNull.Value)
                                            ProductIDA = Convert.ToDouble(dr["PARAMETER_VALUE"]) + 1;
                                    }
                                    dr.Close();
                                    sql = string.Format("insert into TLabelContent(MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST,TMSTP_SEND,ID_PERSON,REC_ID,REC_IMP_TIME,IMP_FINISH) values('{0}','{1}',{2},{3},{4},{5},{6},'{7}',{8},{9},{10},{11},'{12}')", LabelDataRecv.MACHINE_NO, LabelDataRecv.ID_LOT_PROD, LabelDataRecv.ID_PART_LOT, LabelDataRecv.DIM_LEN, LabelDataRecv.IND_FIXED, LabelDataRecv.SEQ_SEND, LabelDataRecv.NUM_BAR, LabelDataRecv.SEQ_LIST,LabelDataRecv.TMSTP_SEND, LabelDataRecv.ID_PERSON, ProductIDA, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0);
                                    tm.MultithreadExecuteNonQuery(sql);
                                }
                                catch
                                {
                                    ACK = 0;
                                    reson = "打捆指令储数据有误，可能存在重复数据";
                                    #region l2->l3标签数据应答失败回复
                                    //反馈接收数据标签信息 
                                    msgid = GetMsgID();
                                    string MessageHead = "BD2101A" + " &" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    byte[] sendArray1 = Enumerable.Repeat((byte)0x20, length - 19 - 8).ToArray(); //
                                    Array.Copy(buffer, sendArray1, length - 19 - 8);
                                    byte[] byteArray1 = StripIronNum.ByteReplace(Encoding.Default.GetBytes(MessageHead), OldBytes, NewBytes);//应答头
                                    Buffer.BlockCopy(byteArray1, 0, sendArray1, 0, byteArray1.Length);


                                    string appendmsg = ACK.ToString() + " &" + reson + " &" + msgid.ToString() + " &" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " &";
                                    byte[] sendArray2 = StripIronNum.ByteReplace(Encoding.Default.GetBytes(appendmsg), OldBytes, NewBytes);

                                    byte[] sendArray = Enumerable.Repeat((byte)0x20, sendArray1.Length + sendArray2.Length).ToArray();
                                    Array.Copy(sendArray1, sendArray, sendArray1.Length);
                                    Buffer.BlockCopy(sendArray2, 0, sendArray, sendArray1.Length, sendArray2.Length);

                                    StripIronNum.Client_MES.SendData(sendArray);
                                    string strsend = MessageHead + appendmsg;
                                    string sqlsend = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "回复标签数据" + strsend);
                                    tm.MultithreadExecuteNonQuery(sqlsend);
                                    #endregion
                                }
                            }
                            catch
                            {
                                ACK = 0;
                                reson = "打捆数据解析有误，请查验数据格式";
                                #region l2->l3标签数据应答失败回复
                                //反馈接收数据标签信息 
                                msgid = GetMsgID();
                                string MessageHead = "BD2101A" + " &" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                byte[] sendArray1 = Enumerable.Repeat((byte)0x20, length - 19 - 8).ToArray(); //
                                Array.Copy(buffer, sendArray1, length - 19 - 8);
                                byte[] byteArray1 = StripIronNum.ByteReplace(Encoding.Default.GetBytes(MessageHead), OldBytes, NewBytes);//应答头
                                Buffer.BlockCopy(byteArray1, 0, sendArray1, 0, byteArray1.Length);


                                string appendmsg = ACK.ToString() + " &" + reson + " &" + msgid.ToString() + " &" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " &";
                                byte[] sendArray2 = StripIronNum.ByteReplace(Encoding.Default.GetBytes(appendmsg), OldBytes, NewBytes);

                                byte[] sendArray = Enumerable.Repeat((byte)0x20, sendArray1.Length + sendArray2.Length).ToArray();
                                Array.Copy(sendArray1, sendArray, sendArray1.Length);
                                Buffer.BlockCopy(sendArray2, 0, sendArray, sendArray1.Length, sendArray2.Length);

                                StripIronNum.Client_MES.SendData(sendArray);
                                string strsend = MessageHead + appendmsg;
                                string sqlsend = string.Format("INSERT INTO MESSENDLOG(REC_CREATE_TIME,SEND_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "回复标签数据" + strsend);
                                tm.MultithreadExecuteNonQuery(sqlsend);
                                #endregion
                            }
                            sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=10", ProductIDA, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));//更新recid
                            tm.MultithreadExecuteNonQuery(sql);
                            sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=7", 1, DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));//更新MES打捆指令数据
                            tm.MultithreadExecuteNonQuery(sql);
                            str = MessageFlg.ToString() + " " + LabelDataRecv.MACHINE_NO + " " + LabelDataRecv.ID_LOT_PROD + " " + LabelDataRecv.ID_PART_LOT.ToString() + LabelDataRecv.DIM_LEN.ToString() + " " + LabelDataRecv.IND_FIXED + " " + LabelDataRecv.SEQ_SEND.ToString() + " " + LabelDataRecv.NUM_BAR.ToString() + " " + LabelDataRecv.SEQ_LIST.ToString() + " " + LabelDataRecv.TMSTP_SEND + " " + LabelDataRecv.ID_PERSON.ToString();
                            sql = string.Format("INSERT INTO MESRECVLOG(REC_CREATE_TIME,RECV_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), "收到标签数据" + str);
                            tm.MultithreadExecuteNonQuery(sql);
                            #endregion

                        }                      
                        if (MessageFlg == "")
                        {
                            string text = GetString(buffer, EncodingType.GB2312);
                            sql = string.Format("INSERT INTO MESRECVLOG(REC_CREATE_TIME,RECV_CONTENT) VALUES ('{0}','{1}')", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")), text);
                            tm.MultithreadExecuteNonQuery(sql);
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