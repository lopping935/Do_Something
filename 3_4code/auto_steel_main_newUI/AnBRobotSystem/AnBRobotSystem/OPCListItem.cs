using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opc.Ua.Interface.Control;
using Opc.Ua.Interface;
using Opc.Ua;
//using Opc.Ua.ClientTest;
using System.Data.Common;
using SQLPublicClass;
namespace DataManageSystem.ChildForm
{
    class OpcListItem
    {
        IniSqlConfigInfo inisql = new IniSqlConfigInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        public string id = string.Empty;
        public string url = string.Empty;
        public Example exam;
        public MonitoredItemsControl moni;

        public bool m_Connected = false;

        public OpcListItem(string sid, string surl)     
        {
            id = sid;
            url = surl;
            exam = new Example();
            moni = new MonitoredItemsControl();
            exam.m_ServerUrl = "opc.tcp://" + url.Trim();

            ApplicationDescriptionCollection servers = new ApplicationDescriptionCollection();

            exam.FindServers(ref servers);
            exam.m_Server.Connect(servers[0].DiscoveryUrls[0]);

            m_Connected = true;
            moni.Server = exam.m_Server;

            SQLPublicClass.DbHelper db = new SQLPublicClass.DbHelper(inisql.GetConnectionString("SysSQL"));
            string sql = "SELECT [NodeId] FROM [OPC_Tag] where [OPCAcquisitionConfig_id]=" + id.Trim();
            DbDataReader dr = db.ExecuteReader(db.GetSqlStringCommond(string.Format(sql)));
            while (dr.Read())
            {
                moni.addMonitoredItems(dr["NodeId"].ToString());
            }
        }

        public int CloseOpcItem()
        {
            int result;
            try
            {
                result = exam.m_Server.Disconnect();
                if (result == 0)
                {
                    if (moni.Subscription != null)
                    {
                        moni.RemoveSubscription();
                    }
                    m_Connected = false;

                }
                else
                {
                    m_Connected = true;
                }
            }
            catch (Exception e)
            {
                result = -1;
                Console.WriteLine("An exception occured during disconnect: " + e.Message);
            }
            moni.MonitoredItemsList.Items.Clear();

            return result;
        }

        public opcdata FindOpcData(string nodeid)
        {
            opcdata data = new opcdata();
            for (int i = 0; i < moni.MonitoredItemsList.Items.Count; i++)
            {
                if (moni.MonitoredItemsList.Items[i].Text == nodeid.Trim())
                {
                    data.NodeId = moni.MonitoredItemsList.Items[i].Text;
                    data.Value = moni.MonitoredItemsList.Items[i].SubItems[2].Text;
                    data.Quality = moni.MonitoredItemsList.Items[i].SubItems[3].Text;
                    data.Datetime = moni.MonitoredItemsList.Items[i].SubItems[4].Text;
                }
            }
            return data;
            
        }
    }

    public class opcdata 
    {
        private string nodeId;

        public string NodeId
        {
            get { return nodeId; }
            set { nodeId = value; }
        }
        private string quality;

        public string Quality
        {
            get { return quality; }
            set { quality = value; }
        }
        private string datetime;

        public string Datetime
        {
            get { return datetime; }
            set { datetime = value; }
        }
        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
