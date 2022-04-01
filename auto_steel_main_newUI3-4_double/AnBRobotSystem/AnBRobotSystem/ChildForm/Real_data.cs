using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using SQLPublicClass;
using System.Data.Common;
using System.Reflection;
using System.Data.SqlClient;
using AnBRobotSystem.Utlis;

namespace AnBRobotSystem.ChildForm
{
    public partial class Real_data : Sunny.UI.UIPage
    {
        dbTaskHelper dbhelper = new dbTaskHelper();
        DateTime lastd1 = new DateTime();
        public Real_data()
        {
            InitializeComponent();
            
        }

        private void Real_data_Load(object sender, EventArgs e)
        {
            string sqltext = string.Format("SELECT TOP 100 [fishstation] 位置,[carnum] 罐号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 需折铁量,[fall_weight] 实际折铁量,[tb_hight] 净空值,[fall_time] 折铁用时 FROM [AutoSteel].[dbo].[RealTime] where fall_time!='' and fall_weight!='0' and CONVERT(float,i_need_weight)>0 order by startime DESC");
            DataTable dt1 = dbhelper.MultithreadDataTable(sqltext);
            uiDataGridView1.DataSource = dt1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //更新实时数据
                int count = 0;
                DateTime d1 = DateTime.Now;
                string sqltext = string.Format("SELECT TOP 100 [fishstation] 位置,[carnum] 罐号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 需折铁量,[fall_weight] 实际折铁量,[tb_hight] 净空值,[fall_time] 折铁用时 FROM [AutoSteel].[dbo].[RealTime] where fall_time!='' and fall_weight!='0' and CONVERT(float,i_need_weight)>0 order by startime DESC");
                DataTable dt1 = dbhelper.MultithreadDataTable(sqltext);
                uiDataGridView1.DataSource = dt1;

                //拷贝实时表数据到历史表
                if (d1.Day - lastd1.Day != 0)
                {
                    sqltext = string.Format("insert into History SELECT * FROM RealTime where CONVERT(char(10),startime,120)!=CONVERT(char(10),GETDATE(),120)");
                    count = dbhelper.MultithreadExecuteNonQuery(sqltext);

                    sqltext = string.Format("delete from RealTime where CONVERT(char(10),startime,120)!=CONVERT(char(10),GETDATE(),120)");
                    count = dbhelper.MultithreadExecuteNonQuery(sqltext);

                    lastd1 = d1;
                }
            }
            catch
            {

            }
        }

        private void uiDataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }
    }
    }