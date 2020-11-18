using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using log4net;
using bincour_sharp;
namespace SocketHelper
{
    class SocketData
    {        
        public void Recv(object SocketClient)
        {
            
            Socket connect = SocketClient as Socket;
            while (true)
            {              
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
                        
                        lock (Program.gllock)
                        {
                           Program.MessageFlg = BitConverter.ToInt16(buffer.Skip(0).Take(2).ToArray(), 0);
                           Program.Message_angle = BitConverter.ToDouble(buffer.Skip(2).Take(8).ToArray(), 0);
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void do_SendMessage()
        {
            byte[] sendArray = Enumerable.Repeat((byte)0x0, 16).ToArray();
            //构造发送数组
            byte[] byteArray1 = BitConverter.GetBytes(Form1.open_flag);
            Buffer.BlockCopy(byteArray1, 0, sendArray, 0, byteArray1.Length);
            //发送数据
            KeyValuePair<string, Socket> kvp = Form1.PLC_Server.dict.FirstOrDefault();
            Form1.PLC_Server.SendToSomeone(sendArray, kvp.Key);
        }
    }
}



//锁定义
//public static object obj = new object();
//public static object gllock = new object();
//public static Int16 MessageFlg = 0;
//public static Int16 MessageStop = 0;

//数据解析
//Program.PrintNum = buffer.Skip(2).Take(1).ToArray()[0];
//string iface_id = Encoding.ASCII.GetString(buffer.Skip(16).Take(12).ToArray());
//KeyValuePair<string, Socket> kvp = Form1.PLC_Server.dict.FirstOrDefault();
//byte[] markok = BitConverter.GetBytes(35);
//byte[] sendArray = Enumerable.Repeat((byte)0x0, length).ToArray();
//Array.Copy(buffer, sendArray, length);
//Buffer.BlockCopy(markok, 0, sendArray, 0, markok.Length);
//Form1.PLC_Server.SendToSomeone(sendArray, kvp.Key);
