using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logtest;
using Hopchelper;
using System.Threading;
namespace record_data
{
    class Program
    {

        public static opchelper Auto_steel_Client = new opchelper();
        public static OPCItem back_high_speed;
        public static OPCItem back_low_speed;
        public static OPCItem fall_high_speed;
        public static OPCItem fall_low_speed;
        public static OPCItem weight;

        public static object obj=new object ();
        static void Main(string[] args)
        {     
           // Initopc();
            Thread record = new Thread(record_data);
            record.Start();
        }
        static void Initopc()
        {
            try
            {
                if (!Auto_steel_Client.ConnectRemoteServer("127.0.0.1", "kepware.kepserverex.v5"))
                {
                    Console.WriteLine("状态：连接OPC失败！\n");
                    return;
                }
                if (!Auto_steel_Client.CreateGroup())
                {
                    Console.WriteLine("状态：创建OPC组失败！\n");
                    return;
                }
                if (opchelper.KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    string log = "已连接到:" + opchelper.KepServer.ServerName;
                    Console.WriteLine(log + " \r\n");                    
                    Auto_steel_Client.opc_connected = true;
                }
                else
                {
                    //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                    Console.WriteLine("状态：" + opchelper.KepServer.ServerState.ToString() + "\n");
                    Auto_steel_Client.opc_connected = false;
                }
                back_high_speed = Auto_steel_Client.AddItem("back_high_speed");
                back_low_speed = Auto_steel_Client.AddItem("back_low_speed");
                fall_high_speed = Auto_steel_Client.AddItem("fall_high_speed");
                fall_low_speed = Auto_steel_Client.AddItem("fall_low_speed");
                weight = Auto_steel_Client.AddItem("weight");
            }
            catch (Exception err)
            {
                LogHelper.WriteLog("opcerr", err);
            }
        }
        static void record_data()
        {
            while(true)
            {
                Thread.Sleep(1000);
                object b_h_s=Auto_steel_Client.ReadItem(back_high_speed);
                object b_l_s = Auto_steel_Client.ReadItem(back_low_speed);                
                object f_h_s = Auto_steel_Client.ReadItem(fall_high_speed);
                object f_l_s = Auto_steel_Client.ReadItem(fall_low_speed);
                object wight = Auto_steel_Client.ReadItem(weight);
                if((bool)b_h_s== false)//speed=-80hz
                {

                    string log = "-80" + "\",\"" + wight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if((bool)b_l_s== true)//speed=-40hz
                {
                    string log = "-40" + "\",\"" + wight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if((bool)f_h_s == true)////speed=80hz
                {
                    string log = "80" + "\",\"" + wight.ToString();
                    LogHelper.WriteLog(log);
                }
                else if((bool)f_l_s == true)////speed=40hz
                {
                    string log = "40" + "\",\"" + wight.ToString();
                    LogHelper.WriteLog(log);
                }
                else
                {

                }
            }
           
        }


    }
}
