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
        public Real_data()
        {
            InitializeComponent();
            
        }

        private void Real_data_Load(object sender, EventArgs e)
        {
            string sqltext = string.Format("SELECT TOP 1000 [ID] ID,[fishstation] 位置,[carnum] 罐号,[is_full] 罐状态,[train_in_times] 火车到时,[startime] 开始倾倒,[stoptime] 结束倾倒,[Tare_Weight] 毛重,[f_full_weight] 满罐重量,[f_has_weight] 当前罐重,[i_need_weight] 需折铁量,[tempture] 温度,[fall_time] 折铁用时,[fall_weight] 实际折铁量 FROM [AutoSteel].[dbo].[RealTime]");
            DataTable dt1= dbhelper.MultithreadDataTable(sqltext);
            uiDataGridView1.DataSource = dt1;
        }
    }
    }