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
        public static OPCServer KepServer ;
        public static OPCGroups KepGroups;
        public static OPCGroup KepGroup;
        public OPCItems KepItems;
        public Boolean opc_connected;
        public opchelper()
        {
            KepServer = new OPCServer();
        }
        /// <summary>
        /// 连接server
        /// </summary>
        public bool ConnectRemoteServer(string remoteServerIP, string remoteServerName)
        {
            try
            {
                KepServer.Connect(remoteServerName, remoteServerIP);//实例化kepserver对象

            }
            catch(Exception e)
            {
                opc_connected=false;
                return false;
            }
            opc_connected= true;
            return true;
        }
        /// <summary>
        /// 创建组
        /// </summary>
        public bool CreateGroup()
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
        /// <summary>
        /// 添加item
        /// </summary>
        public OPCItem AddItem(string tagname)
        {
            OPCItem myKepItem = null;
            string itemname;
            itemname = "ANFish.AFish300." + tagname;
            myKepItem = KepItems.AddItem(itemname, 1);
            return myKepItem;
        }
        /// <summary>
        /// 断开server
        /// </summary>
        private void service_disconnect_Click(object sender, EventArgs e)
        {
            if (!opc_connected)
            {
                return;
            }
            KepServer.Disconnect();
            opc_connected = false;

        }
        /// <summary>
        /// 读数据
        /// </summary>
        public object ReadItem(OPCItem myKepItem)
        {
            object myValue = 0, myQuality = 0, myTimeStamp = 0;
            myKepItem.Read((short)OPCDataSource.OPCDevice, out myValue, out myQuality, out myTimeStamp);
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
               // LogHelper.WriteLog("curd write to db40", err);
            }

        }
    }
}
