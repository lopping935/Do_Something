using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCAutomation;
namespace AGFish
{
    class opchelper
    {
        //opc定义
        public static OPCServer KepServer = new OPCServer();
        public static OPCGroups KepGroups;
        public static OPCGroup KepGroup;
        public OPCItems KepItems;
        public bool opc_connected = false;
        OPCItem item_printflag;//请求打印
        OPCItem item_print_autoflag;//手自动标志位
        public static OPCItem item_chanpin_name;//产品名称
        public static OPCItem item_paihao;//执行标准/牌号
        public static OPCItem item_hetonghao;//合同号
        public static OPCItem item_luhao;//炉号
        public static OPCItem item_guige;//规格
        public static OPCItem item_changdu;//长度
        public static OPCItem item_zhahao;//轧号
        public static OPCItem item_kunhao;//捆号
        public static OPCItem item_zhishu;//支数
        private void service_connection_Click(object sender, EventArgs e)
        {
            Connect_Server();
        }
        private void Connect_Server()//连接服务器
        {
            try
            {

                if (!ConnectRemoteServer("127.0.0.1", "kepware.kepserverex.v4"))
                {
                    return;
                }

                if (!CreateGroup())
                {
                    return;
                }
                if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {

                    //txt_message.AppendText("已连接到:" + KepServer.ServerName + "\n");

                    //service_connection.BackColor = Color.LightGreen;
                    //service_connection.Enabled = false;
                    opc_connected = true;
                }
                else
                {
                    //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                    //txt_message.AppendText("状态：" + KepServer.ServerState.ToString() + "\n");

                    //service_connection.BackColor = Color.Red;
                    opc_connected = false;
                }

                item_printflag = AddItem("print_flag");//请求打印
                item_chanpin_name = AddItem("chanpin_name");//产品名称
                item_paihao = AddItem("paihao");//执行标准牌号
                item_hetonghao = AddItem("hetonghao");//合同号
                item_luhao = AddItem("luhao");//炉号
                item_zhahao = AddItem("zhahao");//轧号
                item_kunhao = AddItem("kunhao");//捆号
                item_print_autoflag = AddItem("print_autoflag");
                item_guige = AddItem("guige");//规格
                item_changdu = AddItem("changdu");//长度
                item_zhishu = AddItem("zhishu");//支数
                
            }
            catch (Exception err)
            {
                //txt_message.AppendText("初始化出错：" + err.Message + "\n");

            }
        }
        private bool ConnectRemoteServer(string remoteServerIP, string remoteServerName)
        {
            try
            {
                KepServer.Connect(remoteServerName, remoteServerIP);//实例化kepserver对象

            }
            catch (Exception err)
            {
               // txt_message.AppendText("连接远程服务器出现错误：" + err.Message + "\n");

                return false;
            }
            return true;
        }
        /// <summary>
        /// 创建组
        /// </summary>
        private bool CreateGroup()
        {
            try
            {
                KepGroups = KepServer.OPCGroups;
                KepGroup = KepGroups.Add("group1");
                KepServer.OPCGroups.DefaultGroupIsActive = true;
                KepServer.OPCGroups.DefaultGroupDeadband = 0;
                KepGroup.UpdateRate = 1000;
                KepGroup.IsActive = true;
                KepGroup.IsSubscribed = true;

                KepItems = KepGroup.OPCItems;

            }
            catch (Exception err)
            {
                //MessageBox.Show("创建组出现错误：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               // txt_message.AppendText("创建组出现错误：" + err.Message + "\n");

                return false;
            }
            return true;
        }

        private OPCItem AddItem(string tagname)
        {

            OPCItem myKepItem = null;
            string itemname;
            try
            {
                itemname = "HB.PLC." + tagname;
                myKepItem = KepItems.AddItem(itemname, 1);

                return myKepItem;
            }
            catch (Exception err)
            {
                //MessageBox.Show("添加Item错误！" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               // txt_message.AppendText("添加Item错误！" + err.Message + "\n");

            }
            return myKepItem;
        }

        private void service_disconnect_Click(object sender, EventArgs e)
        {
            //timer_readwrite.Enabled = false;
            if (!opc_connected)
            {
                return;
            }

            KepServer.Disconnect();

            //myTimer.Elapsed -= new System.Timers.ElapsedEventHandler(timer_read_write);

            opc_connected = false;
            if (!opc_connected)
            {
               // txt_message.AppendText("已断开服务器" + "\n");
                //service_disconnect.BackColor = Color.Red;


            }
            //service_connection.Enabled = true;
        }


        /// <summary>
        /// 读数据
        /// </summary>
        public object ReadItem(OPCItem myKepItem)
        {
            object myValue = 0, myQuality = 0, myTimeStamp = 0;
            try
            {
                myKepItem.Read((short)OPCDataSource.OPCDevice, out myValue, out myQuality, out myTimeStamp);
            }
            catch (Exception err)
            {
              // txt_message.AppendText("读取" + myKepItem + "错误！" + err.Message + "\n");

                return "错误";
            }
            return myValue;
        }
        /// <summary>
        /// 写数据
        /// </summary>
        public void WriteItem(string myValue, OPCItem myKepItem)
        {

            try
            {
                Array Serverhandles, MyErrors;
                object[] valueTemp = new object[] { "", myValue };
                int[] temp = new int[2];
                temp[0] = 0;
                temp[1] = myKepItem.ServerHandle;

                Serverhandles = (Array)temp;
                KepGroup.SyncWrite(1, Serverhandles, valueTemp, out MyErrors);
            }
            catch (Exception err)
            {
                //txt_message.AppendText("写入" + myKepItem + "错误！" + err.Message + "\n");

            }

        }
        public static void static_WriteItem(string myValue, OPCItem myKepItem)
        {

            try
            {
                Array Serverhandles, MyErrors;
                object[] valueTemp = new object[] { "", myValue };
                int[] temp = new int[2];
                temp[0] = 0;
                temp[1] = myKepItem.ServerHandle;

                Serverhandles = (Array)temp;
                KepGroup.SyncWrite(1, Serverhandles, valueTemp, out MyErrors);
            }
            catch (Exception err)
            {
                //LogHelper.WriteLog("curd write to db40", err);
            }

        }
    }
}
