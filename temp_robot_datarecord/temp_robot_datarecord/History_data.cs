using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vision_Utlis;

namespace temp_robot_datarecord
{
    public partial class History_data : Form
    {
        public string constring = "Data Source=127.0.0.1;Initial Catalog=temp_robot_datarecord;User ID=sa; Password=6923263;Enlist=true;Pooling=true;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";
        string normal_stop = "";
        public dbTaskHelper db_histary_helper;
        public History_data()
        {
            InitializeComponent();
            db_histary_helper = new dbTaskHelper(constring);
        }
        DataTable dt1;
        private void button1_Click(object sender, EventArgs e)
        {
            
            //string sqltext = string.Format("SELECT [fishstation] 位置,[carnum] 罐号,[bagnum] 包号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 目标毛重,[fall_weight] 实际毛重,[i_init_weight] 初始重量,[tb_hight] 净空值,[fall_time] 折铁用时,[fall_state] 完成状态 FROM [AutoSteel].[dbo].[History] WHERE [startime]>='{0}' and [startime]<='{1}' and convert(float,i_need_weight)>0 and convert(float,fall_weight)>0 order by startime DESC", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            string sqltext = string.Format("SELECT * FROM [temp_robot_datarecord].[dbo].[workrecord] WHERE [rb_on_startpoint]>='{0}' and [rb_on_startpoint]<='{1}'", dateTimePicker1.Value.ToString("yyyy/MM/dd HH:mm:ss"), dateTimePicker2.Value.ToString("yyyy/MM/dd HH:mm:ss"));

            try
            {
                 dt1 = db_histary_helper.MultithreadDataTable(sqltext);
                if (dataGridView1.Rows.Count != 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                }
                if (dt1.Rows.Count != 0)
                {
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.DataSource = dt1;
                   // for (int count = 0; count <= dataGridView1.RowCount; count++)
                   // {

                        //DataGridViewRow row = dataGridView1.Rows[count];
                        //    IMP_FINISH_need = float.Parse(row.Cells[6].Value.ToString());
                        //    IMP_FINISH_in = float.Parse(row.Cells[7].Value.ToString());
                        //    IMP_FINISH_sub = IMP_FINISH_in - IMP_FINISH_need;
                        //    if ((IMP_FINISH_in - IMP_FINISH_need > 2 || IMP_FINISH_in - IMP_FINISH_need < -2) && IMP_FINISH_in - IMP_FINISH_need < 3 && IMP_FINISH_in - IMP_FINISH_need > -3)
                        //    {
                        //        uiDataGridView1.Rows[count].DefaultCellStyle.BackColor = Color.DeepPink;
                        //    }
                        //    if (IMP_FINISH_sub < 5)
                        //    {
                        //        sub++;
                        //    }
                        //    if (IMP_FINISH_in > 280)
                        //    {
                        //        full++;
                        //    }
                        //    txt_ui_message_in.Text = "投入次数:" + (uiDataGridView1.RowCount - 1) + " 成功次数:" + sub + " 满罐数:" + full + " 投入率: " + Math.Round((float.Parse(sub.ToString()) / float.Parse((uiDataGridView1.RowCount - 1).ToString())) * 100, 1) + " %" + ", 满罐率: " + Math.Round((float.Parse(full.ToString()) / float.Parse((uiDataGridView1.RowCount - 1).ToString())) * 100, 1) + " %" + " ";

                        //}
                        //sub = 0;
                        //full = 0;

                   // }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //打开文件对话框，导出文件
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "保存文件";
            saveFileDialog1.Filter = "Excel 文件(*.xls)|*.xls|Excel 文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
            saveFileDialog1.FileName = "测温取样.xls"; //设置默认另存为的名字
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string txtPath = this.saveFileDialog1.FileName;
                //string sql = "select ID as ID,UserName as 用户名,LoginAccount as 账号,UserPower as 用户权限,Founder as 创建者,Addtime as 创建日期,Activestate as 状态 from UserData";
                //string sql = string.Format("SELECT [fishstation] 位置,[carnum] 罐号,[bagnum] 包号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 目标毛重,[fall_weight] 实际毛重,[tb_hight] 净空值,[fall_time] 折铁用时,[fall_state] 完成状态 FROM [AutoSteel].[dbo].[History] WHERE [startime]>='{0}' and [startime]<='{1}' and convert(float,i_need_weight)>0 and convert(float,fall_weight)>0 order by startime DESC", uiDatetimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), uiDatetimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                //string sql = string.Format("SELECT TOP 100 [fishstation] 位置,[carnum] 罐号,[bagnum] 包号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 目标毛重,[fall_weight] 实际毛重,[tb_hight] 净空值,[fall_time] 折铁用时,[fall_state] 完成状态 FROM [AutoSteel].[dbo].[History] where fall_time!='' and fall_weight!='0' and CONVERT(float,i_need_weight)>0 order by startime DESC");
               // DataTable dt2 = db.MultithreadDataTable(sql);
                //SqlHelp sqlHelper = new SqlHelp();
                //DataTable dt = sqlHelper.GetDataTableValue(sql);
                NPOIHelper.DataTableToExcel(dt1, txtPath);
            }
        }
    }
}
