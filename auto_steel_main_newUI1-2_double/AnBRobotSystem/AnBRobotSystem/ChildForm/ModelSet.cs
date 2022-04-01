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

namespace AnBRobotSystem.ChildForm
{
    
    public partial class ModelSet : Sunny.UI.UIPage
    {
        dbTaskHelper db = new dbTaskHelper();
        public ModelSet()
        {
            InitializeComponent();
           
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            //string sqltext = string.Format("SELECT TOP 100 [fishstation] 位置,[carnum] 罐号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 需折铁量,[fall_weight] 实际折铁量,[tb_hight] 净空值,[fall_time] 折铁用时 FROM [AutoSteel].[dbo].[RealTime] order by startime DESC");

            //  string sqlLabelSel = string.Format("select top 100 REC_ID 流水号,merge_sinbar 捆号,gk 技术标准,heat_no 轧制序号,mtrl_no 牌号, spec 规格, wegith 重量, num_no 支数, print_date 日期, classes 班次, sn_no 序号, labelmodel_name 模板名称, print_type 技术标准, insert_date 创建时间, flag 状态, orign_sinbar 原始捆号, time 读取时间,IMP_FINISH 状态信息,REC_CREATE_TIME 状态更新时刻 from HLabelContent WHERE REC_CREATE_TIME>='{0}' and REC_CREATE_TIME<='{1}' order by REC_CREATE_TIME ASC", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            string sqltext = string.Format("SELECT [fishstation] 位置,[carnum] 罐号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 需折铁量,[fall_weight] 实际折铁量,[tb_hight] 净空值,[fall_time] 折铁用时 FROM [AutoSteel].[dbo].[History] WHERE [startime]>='{0}' and [startime]<='{1}' and convert(float,i_need_weight)>0 and convert(float,fall_weight)>0 order by startime DESC", uiDatetimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), uiDatetimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));

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
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
