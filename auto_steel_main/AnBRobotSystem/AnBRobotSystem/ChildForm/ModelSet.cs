﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SQLPublicClass;
using System.Data.Common;
using System.Reflection;
using System.Data.SqlClient;

namespace AnBRobotSystem.ChildForm
{

    public partial class ModelSet : Form
    {
     
        public ModelSet()
        {
            InitializeComponent();
           
        }

       /* private void ModelSet_Load(object sender, EventArgs e)
        {
        }

        

        

        private void button2_Click(object sender, EventArgs e)
        {
           // string sqlLabelSel = string.Format("select REC_ID 流水号,MACHINE_NO 打包机组号,ID_LOT_PROD 生产批号,ID_PART_LOT 分批号,NUM_BDL 捆号, SEQ_LEN 长度顺序号, SEQ_OPR 操作顺序号, DIM_LEN 米长, IND_FIXED 定尺标志, SEQ_SEND 下发顺序号, NUM_BAR 捆内支数, SEQ_LIST 排列序号, LA_BDL_ACT 重量, NO_LICENCE 许可证号, NAME_PROD 产品名称, NAME_STND 执行标准, ID_HEAT 熔炼号, NAME_STLGD 钢牌号, DES_FIPRO_SECTION 断面规格描述, ID_CREW_RL 轧制班别, ID_CREW_CK 检查班别, TMSTP_WEIGH 生产日期, BAR_CODE 条码内容, NUM_HEAD 头签个数, NUM_TAIL 尾签个数,L3TMSTP_SEND MES发送时刻,IMP_FINISH 状态信息,REC_IMP_TIME 状态更新时刻,REC_CREATE_TIME 记录创建时刻 from HLabelContent WHERE REC_CREATE_TIME>='{0}' and REC_CREATE_TIME<='{1}' order by REC_CREATE_TIME ASC", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            string sqlLabelSel = string.Format("select top 100 REC_ID 流水号,merge_sinbar 捆号,gk 技术标准,heat_no 轧制序号,mtrl_no 牌号, spec 规格, wegith 重量, num_no 支数, print_date 日期, classes 班次, sn_no 序号, labelmodel_name 模板名称, print_type 技术标准, insert_date 创建时间, flag 状态, orign_sinbar 原始捆号, time 读取时间,IMP_FINISH 状态信息,REC_CREATE_TIME 状态更新时刻 from HLabelContent WHERE REC_CREATE_TIME>='{0}' and REC_CREATE_TIME<='{1}' order by REC_CREATE_TIME ASC", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
               DataTable dt1 = db.ExecuteDataTable(db.GetSqlStringCommond(sqlLabelSel)); 
            
            if (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
            }
            if (dt1.Rows.Count != 0)
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.DataSource = dt1;
            }
            }
            catch (Exception ex)
            {

            }

        }

        private void button_count_Click(object sender, EventArgs e)
        {
            //if(dataGridView1.RowCount>0)
            //{
            //    textBox_count.Text = (dataGridView1.RowCount-1).ToString();
            //}
            //else
            //{
            //    textBox_count.Text = "0";
            //}
            

            
        }*/
    }
}
