using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SQLPublicClass;
using System.Data.Common;
using System.Reflection;
using System.Data.SqlClient;
using AnBRobotSystem.Utlis;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;


namespace AnBRobotSystem.ChildForm
{
    
    public partial class ModelSet : Sunny.UI.UIPage
    {


        float IMP_FINISH_need = 0;
        float IMP_FINISH_in = 0;
        float IMP_FINISH_sub = 0;
        int sub = 0;
        int full = 0;
        int count;


        dbTaskHelper db = new dbTaskHelper();
        public ModelSet()
        {
            InitializeComponent();
           
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            //string sqltext = string.Format("SELECT TOP 100 [fishstation] 位置,[carnum] 罐号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 需折铁量,[fall_weight] 实际折铁量,[tb_hight] 净空值,[fall_time] 折铁用时 FROM [AutoSteel].[dbo].[RealTime] order by startime DESC");
            sub = 0;
            full = 0;
            //  string sqlLabelSel = string.Format("select top 100 REC_ID 流水号,merge_sinbar 捆号,gk 技术标准,heat_no 轧制序号,mtrl_no 牌号, spec 规格, wegith 重量, num_no 支数, print_date 日期, classes 班次, sn_no 序号, labelmodel_name 模板名称, print_type 技术标准, insert_date 创建时间, flag 状态, orign_sinbar 原始捆号, time 读取时间,IMP_FINISH 状态信息,REC_CREATE_TIME 状态更新时刻 from HLabelContent WHERE REC_CREATE_TIME>='{0}' and REC_CREATE_TIME<='{1}' order by REC_CREATE_TIME ASC", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            string sqltext = string.Format("SELECT [fishstation] 位置,[carnum] 罐号,[bagnum] 包号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 目标毛重,[fall_weight] 实际毛重,[i_init_weight] 初始重量,[tb_hight] 净空值,[fall_time] 折铁用时,[fall_state] 完成状态 FROM [AutoSteel].[dbo].[History] WHERE [startime]>='{0}' and [startime]<='{1}' and convert(float,i_need_weight)>0 and convert(float,fall_weight)>0 order by startime DESC", uiDatetimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), uiDatetimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                DataTable dt1 = db.MultithreadDataTable(sqltext);
                if (uiDataGridView1.Rows.Count != 0)
                {
                    uiDataGridView1.DataSource = null;
                    uiDataGridView1.Rows.Clear();
                }
                if (dt1.Rows.Count != 0)
                {
                    uiDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    uiDataGridView1.DataSource = dt1;
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
                        if (IMP_FINISH_sub < 5)
                        {
                            sub++;
                        }
                        if (IMP_FINISH_in > 280)
                        {
                            full++;
                        }
                        txt_ui_message_in.Text = "投入次数:" + (uiDataGridView1.RowCount - 1) + " 成功次数:" + sub + " 满罐数:" + full + " 投入率: " + Math.Round((float.Parse(sub.ToString()) / float.Parse((uiDataGridView1.RowCount - 1).ToString())) * 100, 1) + " %" + ", 满罐率: " + Math.Round((float.Parse(full.ToString()) / float.Parse((uiDataGridView1.RowCount - 1).ToString())) * 100, 1) + " %" + " ";

                    }
                    sub = 0;
                    full = 0;

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            //打开文件对话框，导出文件
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog2.Title = "保存文件";
            saveFileDialog2.Filter = "Excel 文件(*.xls)|*.xls|Excel 文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
            saveFileDialog2.FileName = "折铁信息.xls"; //设置默认另存为的名字
            if (this.saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                string txtPath = this.saveFileDialog2.FileName;
                //string sql = "select ID as ID,UserName as 用户名,LoginAccount as 账号,UserPower as 用户权限,Founder as 创建者,Addtime as 创建日期,Activestate as 状态 from UserData";
                string sql = string.Format("SELECT [fishstation] 位置,[carnum] 罐号,[bagnum] 包号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 目标毛重,[fall_weight] 实际毛重,[tb_hight] 净空值,[fall_time] 折铁用时,[fall_state] 完成状态 FROM [AutoSteel].[dbo].[History] WHERE [startime]>='{0}' and [startime]<='{1}' and convert(float,i_need_weight)>0 and convert(float,fall_weight)>0 order by startime DESC", uiDatetimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), uiDatetimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                //string sql = string.Format("SELECT TOP 100 [fishstation] 位置,[carnum] 罐号,[bagnum] 包号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 目标毛重,[fall_weight] 实际毛重,[tb_hight] 净空值,[fall_time] 折铁用时,[fall_state] 完成状态 FROM [AutoSteel].[dbo].[History] where fall_time!='' and fall_weight!='0' and CONVERT(float,i_need_weight)>0 order by startime DESC");
                DataTable dt2 = db.MultithreadDataTable(sql);
                //SqlHelp sqlHelper = new SqlHelp();
                //DataTable dt = sqlHelper.GetDataTableValue(sql);
                NPOIHelper.DataTableToExcel(dt2, txtPath);
            }

        }

        private void uiDataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
    }
}
