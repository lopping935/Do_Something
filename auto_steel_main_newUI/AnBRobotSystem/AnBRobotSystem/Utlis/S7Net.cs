using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;

namespace AnBRobotSystem.Utlis
{
    class S7Net
    {
        
        public Plc plc300;
         public S7Net(string plcip)
        {
            plc300 = new Plc(CpuType.S7300, plcip, 0, 2);  //创建PLC实例
            plc300.Open();
        }


        public void PlcOpen()
        {
            plc300.Open();
        } //连接  

        public void PlcClose()
        {
            plc300.Close();
        } //断开



        public bool PlcIsConnected()
        {
            return plc300.IsConnected;
        }  //是否连接
        public bool PlcBoolRead(string address)
        {
            return (bool)plc300.Read(address);
        }  //读bool值 DBx.DBXy

        public byte PlcByteRead(string address)
        {
            return (byte)plc300.Read(address);
        }  //读byte值 DBx.DBBy
        public ushort PlcIntRead(string address)
        {
             return ((ushort)plc300.Read(address));
        }  //读int值 DBx.DBWy
        public double PlcRealRead(string address)
        {
            return ((uint)plc300.Read(address)).ConvertToFloat();
        }  //读real值 DBx.DBDy

        public void PlcIntWrite(int data,string address)
        {
            plc300.Write(address, data);
        }    //写int

        public void PlcRealWrite(float data, string address)
        {

            plc300.Write(address, Conversion.ConvertToUInt(data));

        }  //写real

        public string PlcStrRead(int DB, int address, int lenth)
        {
            string ecode = (string)plc300.Read(DataType.DataBlock, DB, address, VarType.String, lenth-2).ToString();
            //string ecode = (string)plc300.Read(address).ToString();
            return ecode;
        }  //读string  DB块，地址，string长度
    }
}
