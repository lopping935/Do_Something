using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utlis;

namespace agfish_4._0
{
    public partial class data : Form
    {
        public data()
        {
            InitializeComponent();
        }

        public static string constringSelf = "Data Source=.;Initial Catalog=Agfish;User ID=sa; Password=6923263;Enlist=true;Pooling=true;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";

        dbTaskHelper dbhelperNself = new dbTaskHelper();


        private void btn_data_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = dbhelperNself.MultithreadDataTable("select Car_num ,Start_plugin ,Go_starpos ,Alarm ,Alarmtime from [Agfish].[dbo].[aglog] order by Start_plugin desc");         //绑定数据

                listBoxMsg.Items.Add("数据查询成功");
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
            }
            catch (Exception ex)
            {
                listBoxMsg.Items.Add(ex.ToString());
                listBoxMsg.TopIndex = listBoxMsg.Items.Count - 1;
            }


        }

        private void data_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = dbhelperNself.MultithreadDataTable("select Car_num ,Start_plugin ,Go_starpos ,Alarm ,Alarmtime from [Agfish].[dbo].[aglog] order by Start_plugin desc");         //绑定数据
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
