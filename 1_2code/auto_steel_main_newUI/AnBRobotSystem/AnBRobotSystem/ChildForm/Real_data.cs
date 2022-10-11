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
        int hh;


        float IMP_FINISH_need = 0;
        float IMP_FINISH_in = 0;
        float IMP_FINISH_sub = 0;
        int count;
        int sub = 0;
        int full = 0;

        public static string banci = "";
        public static DateTime load = new DateTime();   

        dbTaskHelper dbhelper = new dbTaskHelper();
        DateTime lastd1 = new DateTime();
        DbDataReader dr = null;

        public dbTaskHelper dbhlper = new dbTaskHelper();

        //string sqltext_banci = string.Format("SELECT * FROM [AutoSteel].[dbo].[banci] where time_load!='' and name_class!='0' order by time_load DESC");



        public Real_data()
        {
            InitializeComponent();

        }


        private void Real_data_Load(object sender, EventArgs e)
        {
            sub = 0;
            full = 0;

            //dr = dbhelper.MultithreadDataReader("SELECT * FROM [AutoSteel].[dbo].[banci] where time_load!='' and name_class!='0' order by time_load DESC");
            //if (dr.Read())
            //{
            //    banci = Convert.ToString(dr["name_class"]);
            //    load = (DateTime)dr["time_load"];

            //    //string sqltext_load = string.Format("insert into  [AutoSteel].[dbo].[RealTime] (name_class , time_load ,startime)VALUES ('"+banci+"','"+load+"','"+DateTime.Now+"')");
            //    //dbhlper.MultithreadExecuteNonQuery(sqltext_load);
            //}

            //if (banci == "a")
            //{
            //    //测试静态全局变量banci跳出循环之后是否为局部变量中的值
            //}

            string sqltext = string.Format("SELECT TOP 100 [fishstation] 位置,[carnum] 罐号,[bagnum] 包号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 目标毛重,[fall_weight] 实际毛重,[i_init_weight] 初始重量,[tb_hight] 净空值,[fall_time] 折铁用时,[fall_state] 完成状态 FROM [AutoSteel].[dbo].[RealTime] where fall_time!='' and fall_weight!='0' and CONVERT(float,i_need_weight)>0 order by startime DESC");
            DataTable dt1 = dbhelper.MultithreadDataTable(sqltext);
            uiDataGridView1.DataSource = dt1;

            for (count = 0; count < uiDataGridView1.RowCount - 1; count++)
            {

                DataGridViewRow row = uiDataGridView1.Rows[count];
                IMP_FINISH_need = float.Parse(row.Cells[6].Value.ToString());
                IMP_FINISH_in = float.Parse(row.Cells[7].Value.ToString());
                IMP_FINISH_sub = IMP_FINISH_in - IMP_FINISH_need;
                if ((IMP_FINISH_in - IMP_FINISH_need > 2 || IMP_FINISH_in - IMP_FINISH_need < -2) && IMP_FINISH_in - IMP_FINISH_need < 3 && IMP_FINISH_in - IMP_FINISH_need > -3)
                {
                    uiDataGridView1.Rows[count].DefaultCellStyle.BackColor = Color.DeepPink;
                }
                if (IMP_FINISH_sub < 5)
                {
                    sub++;
                }
                if (IMP_FINISH_in > 280)
                {
                    full++;
                }
                txt_ui_message_real.Text = "投入次数:" + (uiDataGridView1.RowCount - 1) + " 成功次数:" + sub + " 满罐数:" + full + " 投入率: " + Math.Round((float.Parse(sub.ToString()) / float.Parse((uiDataGridView1.RowCount - 1).ToString())) * 100, 1) + " %" + ", 满罐率: " + Math.Round((float.Parse(full.ToString()) / float.Parse((uiDataGridView1.RowCount - 1).ToString())) * 100, 1) + " %" + " ";

            }
            sub = 0;
            full = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {

                sub = 0;
                full = 0;


                //更新实时数据
                int count = 0;
                DateTime d1 = DateTime.Now;
                string sqltext = string.Format("SELECT TOP 100 [fishstation] 位置,[carnum] 罐号,[bagnum] 包号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 目标毛重,[fall_weight] 实际毛重,[i_init_weight] 初始重量,[tb_hight] 净空值,[fall_time] 折铁用时,[fall_state] 完成状态 FROM [AutoSteel].[dbo].[RealTime] where fall_time!='' and fall_weight!='0' and CONVERT(float,i_need_weight)>0 order by startime DESC");
                DataTable dt1 = dbhelper.MultithreadDataTable(sqltext);
                uiDataGridView1.DataSource = dt1;


                for (count = 0; count < uiDataGridView1.RowCount - 1; count++)
                {

                    DataGridViewRow row = uiDataGridView1.Rows[count];
                    IMP_FINISH_need = float.Parse(row.Cells[6].Value.ToString());
                    IMP_FINISH_in = float.Parse(row.Cells[7].Value.ToString());
                    IMP_FINISH_sub = IMP_FINISH_in - IMP_FINISH_need;
                    if ((IMP_FINISH_in - IMP_FINISH_need > 2 || IMP_FINISH_in - IMP_FINISH_need < -2) && IMP_FINISH_in - IMP_FINISH_need < 3 && IMP_FINISH_in - IMP_FINISH_need > -3)
                    {
                        uiDataGridView1.Rows[count].DefaultCellStyle.BackColor = Color.DeepPink;
                    }
                    if (IMP_FINISH_sub < 5)
                    {
                        sub++;
                    }
                    if (IMP_FINISH_in > 280)
                    {
                        full++;
                    }
                    txt_ui_message_real.Text = "投入次数:" + (uiDataGridView1.RowCount - 1) + " 成功次数:" + sub + " 满罐数:" + full + " 投入率: " + Math.Round((float.Parse(sub.ToString()) / float.Parse((uiDataGridView1.RowCount - 1).ToString())) * 100, 1) + " %" + ", 满罐率: " + Math.Round((float.Parse(full.ToString()) / float.Parse((uiDataGridView1.RowCount - 1).ToString())) * 100, 1) + " %" + " ";

                }

                sub = 0;
                full = 0;


                //拷贝实时表数据到历史表
                sqltext = string.Format("SELECT count(*) as count FROM RealTime where CONVERT(char(10),startime,120)!=CONVERT(char(10),GETDATE(),120)");
                DbDataReader dr = null;

                dr = dbhelper.MultithreadDataReader(sqltext);
                while (dr.Read())
                {
                    count = Convert.ToInt32(dr["count"]);
                }
                dr.Close();

                if (count > 0)
                {
                    sqltext = string.Format("insert into History SELECT * FROM RealTime where CONVERT(char(10),startime,120)!=CONVERT(char(10),GETDATE(),120)");
                    count = dbhelper.MultithreadExecuteNonQuery(sqltext);

                    sqltext = string.Format("delete from RealTime where CONVERT(char(10),startime,120)!=CONVERT(char(10),GETDATE(),120)");
                    count = dbhelper.MultithreadExecuteNonQuery(sqltext);

                    lastd1 = d1;

                }

                for (count = 0; count < uiDataGridView1.RowCount; count++)
                {
                    DataGridViewRow row = uiDataGridView1.Rows[count];
                    IMP_FINISH_need = float.Parse(row.Cells[6].Value.ToString());
                    IMP_FINISH_in = float.Parse(row.Cells[7].Value.ToString());
                    if (IMP_FINISH_in - IMP_FINISH_need > 2 || IMP_FINISH_in - IMP_FINISH_need < -2 && IMP_FINISH_in - IMP_FINISH_need < 3 && IMP_FINISH_in - IMP_FINISH_need > -3)
                    {
                        uiDataGridView1.Rows[count].DefaultCellStyle.BackColor = Color.DeepPink;
                    }
                }

            }
            catch(Exception e1)
            {

            }

        }

        private void uiDataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //try
            //{
            //    e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.ToString());
            //}
           

            //try
            //{
            //    for (hh = 0; hh < uiDataGridView1.Rows.Count; hh++)
            //    {
            //        this.uiDataGridView1.Rows[hh].HeaderCell.Value = (hh + 1).ToString();
            //    }

            //}
            //catch(Exception ex)
            //{
            //    //MessageBox.Show(ex.ToString());
            //}



        }

        private void uiDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            for (count = 0; count <= uiDataGridView1.RowCount; count++)
            {
                DataGridViewRow row = uiDataGridView1.Rows[count];
                IMP_FINISH_need = float.Parse(row.Cells[6].Value.ToString());
                IMP_FINISH_in = float.Parse(row.Cells[7].Value.ToString());
                IMP_FINISH_sub = IMP_FINISH_in - IMP_FINISH_need;
                if ((IMP_FINISH_in - IMP_FINISH_need > 2 || IMP_FINISH_in - IMP_FINISH_need < -2) && IMP_FINISH_in - IMP_FINISH_need < 3 && IMP_FINISH_in - IMP_FINISH_need > -3)
                {
                    uiDataGridView1.Rows[count].DefaultCellStyle.BackColor = Color.DeepPink;
                }
            }
        }

        private void Real_data_Initialize(object sender, EventArgs e)
        {

        }

        private void uiDataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                for (hh = 0; hh < uiDataGridView1.Rows.Count; hh++)
                {
                    this.uiDataGridView1.Rows[hh].HeaderCell.Value = (hh + 1).ToString();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

        }
    }
}