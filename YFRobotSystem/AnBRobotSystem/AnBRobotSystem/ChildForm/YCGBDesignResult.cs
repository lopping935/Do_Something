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


namespace AnBRobotSystem.ChildForm
{
    public partial class YCGBDesignResult : Form
    {
        IniSqlConfigInfo inisql = new IniSqlConfigInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini");
        DbHelper db;
        int count = 0;
        List<string> sqlList_cb = new List<string>();
        List<string> sqlList_dw = new List<string>();
        List<string> sqlList_Update = new List<string>();
        List<string> RuleList_Update = new List<string>();
        List<string> sqlList_Insert = new List<string>();
        #region 私有变量
        

        public YCGBDesignResult()
        {
            InitializeComponent();
            db = new DbHelper(inisql.GetConnectionString("SysSQL"));      
        }
        #endregion
        string sqlLabelSel = "";
        private void YCGBDesignResult_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=.;Initial Catalog=YFDBBRobotData;User ID=sa; Password=6923263;Enlist=true;Pooling=true;Connection Timeout=30;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000");
            DataTable dt1 = new DataTable("MyDT");
            DbDataReader dr = null;
            string sql = "";
            double MAXRECID = -1000, modelflag=0;// PLANIDNow = 0;  
            sql = "update [YFDBBRobotData].[dbo].[TLabelContent] set rownumberf=row2 from (select ROW_NUMBER() over(order by merge_sinbar asc)row2, REC_ID from[YFDBBRobotData].[dbo].[TLabelContent])DETAIL_B14 where[TLabelContent].REC_ID = DETAIL_B14.REC_ID";
            db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            sql = "select MAX(rownumberf) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";            
            dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            while (dr.Read())
            {
                if (dr["REC_ID"] != DBNull.Value)
                MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
            }
            dr.Close();
            if(MAXRECID!=-1000)
            { 
                try
                {
                    sqlLabelSel = string.Format("select top 100 REC_ID 流水号,merge_sinbar 合并号,gk 技术标准,heat_no 轧制序号,mtrl_no 牌号, spec 规格, wegith 重量, num_no 支数, print_date 日期, classes 班次, sn_no 序号, labelmodel_name 模板名称, print_type 技术标准, insert_date 创建时间, flag 状态, orign_sinbar 原始捆号, time 读取时间,IMP_FINISH 状态信息,REC_CREATE_TIME 状态更新时刻 from TLabelContent WHERE rownumberf>={0} order by rownumberf ASC", MAXRECID - 20);
                    dt1 = db.ExecuteDataTable(db.GetSqlStringCommond(sqlLabelSel));
                }
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    Log.addLog(log, LogType.ERROR, ex.Message);
                }
                if (dataGridView1.Rows.Count != 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                }
                if (dt1.Rows.Count != 0)
                {
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.DataSource = dt1;
                    //dt1.Rows.Add(dataGridView1.Rows[dataGridView1.Rows.Count - 1]);

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (double.Parse(dt1.Rows[i][0].ToString()) == MAXRECID)
                        {
                            count = i;//计算多选出来的数量
                            break;
                        }
                    }

                }
            }
            else
            {
                sqlLabelSel = string.Format("select top 100 REC_ID 流水号,merge_sinbar 捆号,gk 技术标准,heat_no 轧制序号,mtrl_no 牌号, spec 规格, wegith 重量, num_no 支数, print_date 日期, classes 班次, sn_no 序号, labelmodel_name 模板名称, print_type 技术标准, insert_date 创建时间, flag 状态, orign_sinbar 原始捆号, time 读取时间,IMP_FINISH 状态信息,REC_IMP_TIME 状态更新时刻 from TLabelContent WHERE rownumberf>={0} order by rownumberf ASC", MAXRECID);
                try
                {
                    dt1 = db.ExecuteDataTable(db.GetSqlStringCommond(sqlLabelSel));
                }
                catch (Exception ex)
                {
                    //log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    //Log.addLog(log, LogType.ERROR, ex.Message);
                }
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.DataSource = dt1;                
                //dataGridView1.Rows[dataGridView1.Rows.Count-1].DefaultCellStyle.BackColor = Color.Red;
            }
           
        }

        private void MenuItem_Relation_Click(object sender, EventArgs e)
        {
            
        }
        int intindex = 0;
        SqlDataAdapter adapter;
        SqlConnection conn;
        private DataTable dbconn(string strSql)
        {
            this.adapter = new SqlDataAdapter(strSql, conn);
            DataTable dtSelect = new DataTable();
            int mt = this.adapter.Fill(dtSelect);
            return dtSelect;
        }
        private Boolean dbUpdate(string strSql)
        {            
            try
            {
                if(intindex!=0)
                { 
                    DataTable dtUpdate = new DataTable();
                    dtUpdate = this.dbconn(strSql);
                    dtUpdate.Rows.Clear();
                    DataTable dtShow = new DataTable();
                    dtShow = (DataTable)this.dataGridView1.DataSource;
                    dtUpdate.ImportRow(dtShow.Rows[intindex]);
                    SqlCommandBuilder CommandBuilder;
                    CommandBuilder = new SqlCommandBuilder(this.adapter);
                    this.adapter.Update(dtUpdate);
                    dtUpdate.AcceptChanges();
                    return true;
                }
                else
                {
                    
                    return false;
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
           
        }
        private Boolean SetValue(string strSql)
        {
            try
            {
                DataTable dtUpdate = new DataTable();
                dtUpdate = this.dbconn(strSql);
                dtUpdate.Rows.Clear();
                DataTable dtShow = new DataTable();
                if (intindex == 0)
                {
                    dataGridView1.Rows[intindex].Cells["状态信息"].Value = 31;
                    dtShow = (DataTable)this.dataGridView1.DataSource;
                    dtUpdate.ImportRow(dtShow.Rows[intindex]);
                }
                else
                {
                    dataGridView1.Rows[intindex - 1].Cells["状态信息"].Value = 31;
                    dtShow = (DataTable)this.dataGridView1.DataSource;
                    dtUpdate.ImportRow(dtShow.Rows[intindex - 1]);
                }
                SqlCommandBuilder CommandBuilder;
                CommandBuilder = new SqlCommandBuilder(this.adapter);
                this.adapter.Update(dtUpdate);
                dtUpdate.AcceptChanges();

                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
            
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)//删除选中行
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择一行信息！");
                return;
            }
            
            string sql = "";
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                if (dataGridView1.SelectedRows[i].Cells[0].Value.ToString() == "" )
                {
                    MessageBox.Show("请选择相应的行！");
                    return;
                }
                else if(dataGridView1.SelectedRows[i].Cells[0].Value.ToString() == "0")
                {
                    MessageBox.Show("该行为保留行，不能被删除！");
                    return;
                }
                else
                {
                    sql = sql + string.Format("delete from TLabelContent where REC_ID='{0}' and REC_ID!=0;", Convert.ToDouble(dataGridView1.SelectedRows[i].Cells[0].Value.ToString()));
                }
                   
            }
            int ret1 = db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            if (ret1>0)
                MessageBox.Show("删除成功！");
            SqlDataAdapter sda = new SqlDataAdapter(sqlLabelSel, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            //dataGridView1.RowHeadersVisible = false;

        }
       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            intindex = e.RowIndex;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)//更新数据
        {
            try
            { 
                if (dbUpdate(sqlLabelSel))
                    MessageBox.Show("修改成功！");
                else
                    MessageBox.Show("该条数据不允许修改！");
                SqlDataAdapter sda = new SqlDataAdapter(sqlLabelSel, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                //dataGridView1.RowHeadersVisible = ture;
                YCGBDesignResult_Load(null, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show("数据更新有误！",ex.Message.ToString());
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)//插入新数据
        {
            try
            {
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.Rows.Count - 2];

                string sql = string.Format("SET IDENTITY_INSERT TLabelContent ON;insert into TLabelContent(REC_ID,merge_sinbar ,gk ,heat_no ,mtrl_no , spec , wegith , num_no , print_date , classes , sn_no, labelmodel_name, print_type, insert_date, flag, orign_sinbar, time,IMP_FINISH,REC_IMP_TIME,rownumberf) values({0},'{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',{17},'{18}',{19});SET IDENTITY_INSERT TLabelContent OFF", Convert.ToDouble(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString(), Convert.ToInt32(row.Cells[6].Value.ToString()), Convert.ToInt32(row.Cells[7].Value.ToString()), row.Cells[8].Value.ToString(), row.Cells[9].Value.ToString(), row.Cells[10].Value.ToString(), row.Cells[11].Value.ToString(), row.Cells[12].Value.ToString(), row.Cells[13].Value.ToString(), row.Cells[14].Value.ToString(), row.Cells[15].Value.ToString(), row.Cells[16].Value.ToString(), Convert.ToInt32( row.Cells[17].Value.ToString()),DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),0);
                int ret1 = db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
                if (ret1 > 0)
                {
                    sql = "select PARAMETER_VALUE from SYSPARAMETER where PARAMETER_ID=10";
                    DbDataReader dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
                    double ProductIDA = 0;
                    while (dr.Read())
                    {
                        if (dr["PARAMETER_VALUE"] != DBNull.Value)
                            ProductIDA = Convert.ToDouble(dr["PARAMETER_VALUE"]) ;
                    }
                    if(Convert.ToDouble(row.Cells[0].Value.ToString())>ProductIDA)
                    {
                        sql = string.Format("UPDATE SYSPARAMETER SET PARAMETER_VALUE={0},PARAMETER_TIME='{1}' where PARAMETER_ID=10", Convert.ToDouble(row.Cells[0].Value.ToString()), DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss")));
                        db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
                    }                
                    MessageBox.Show("插入成功！");
                }
                else
                    MessageBox.Show("插入失败！");
            
                SqlDataAdapter sda = new SqlDataAdapter(sqlLabelSel, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                //dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                string a=ex.Message;
                string b = ex.StackTrace;
                MessageBox.Show("插入失败,插入数据有误！");
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                int countn = 0;
                int IMP_FINISH = 0;
                if (dataGridView1.RowCount > count + 2)
                {
                    countn = count + 2;
                }
                else
                {
                    countn = dataGridView1.RowCount - 1;

                }

                for (int i = 0; i < countn; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];                    
                    IMP_FINISH = int.Parse(row.Cells[17].Value.ToString());                  
                    if (IMP_FINISH == 31 )
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
                    if (IMP_FINISH == 32)
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    if (IMP_FINISH == 0 && (i < (countn - 1)))
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    if (IMP_FINISH == 0 && (i == (countn - 1)))
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }
            catch
            {
                MessageBox.Show("表格重绘发生错误！");
            }
        }

        private DataRow rowCopy;
        private void ToolStripMenuItem5_Click(object sender, EventArgs e)//复制数据
        {
            Clipboard.Clear();//清空剪切板内容
            Clipboard.SetDataObject(this.dataGridView1.GetClipboardContent());
            MessageBox.Show("已复制到剪贴板！");
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)//粘贴数据
        {
            dataGridView1.AllowUserToAddRows = true;
            try
            {

                // 获取剪切板的内容，并按行分割   
                string pasteText = Clipboard.GetText();
                if (string.IsNullOrEmpty(pasteText))
                    return;
                int tnum = 0;//多少个单元格
                int nnum = 0;//多少行
                //获得当前剪贴板内容的行、列数
                for (int i = 0; i < pasteText.Length; i++)
                {
                    if (pasteText.Substring(i, 1) == "\t")
                    {
                        tnum++;
                    }
                    if (pasteText.Substring(i, 1) == "\n")
                    {
                        nnum++;
                    }
                }
                Object[,] data;
                //粘贴板上的数据来自于EXCEL时，每行末都有\n，在DATAGRIDVIEW内复制时，最后一行末没有\n
                if (pasteText.Substring(pasteText.Length - 1, 1) == "\n")
                {
                    nnum = nnum - 1;

                }

                tnum = tnum / (nnum + 1);
                data = new object[nnum + 1, tnum + 1];//定义一个二维数组

                String rowstr;
                rowstr = "";
                //MessageBox.Show(pasteText.IndexOf("B").ToString());
                //对数组赋值
                for (int i = 0; i < (nnum + 1); i++)
                {
                    for (int colIndex = 0; colIndex < (tnum + 1); colIndex++)
                    {
                        //一行中的最后一列
                        if (colIndex == tnum && pasteText.IndexOf("\r") != -1)
                        {
                            rowstr = pasteText.Substring(0, pasteText.IndexOf("\r"));
                        }
                        //最后一行的最后一列
                        if (colIndex == tnum && pasteText.IndexOf("\r") == -1)
                        {
                            rowstr = pasteText.Substring(0);
                        }
                        //其他行列
                        //pasteText = pasteText.Substring(pasteText.IndexOf("\t") + 1);
                        if (colIndex != tnum)
                        {
                            rowstr = pasteText.Substring(0, pasteText.IndexOf("\t"));
                            pasteText = pasteText.Substring(pasteText.IndexOf("\t") + 1);
                        }
                        data[i, colIndex] = rowstr;
                    }
                    //截取下一行数据
                    pasteText = pasteText.Substring(pasteText.IndexOf("\n") + 1);

                }
                //获取当前选中单元格所在的列序号
                int curntindex = 0;// dataGridView1.CurrentRow.Cells.IndexOf(dataGridView1.CurrentCell);
                //获取获取当前选中单元格所在的行序号
                int rowindex = dataGridView1.CurrentRow.Index;

                //MessageBox.Show(curntindex.ToString ());
                for (int j = 0; j < (nnum + 1); j++)
                {
                    for (int colIndex = 0; colIndex < (tnum + 1); colIndex++)
                    {
                        if(colIndex + 1<20)
                        {
                            
                            if (data[j, 0]=="")
                            {    
                                
                                dataGridView1.Rows[j + rowindex].Cells[colIndex + curntindex].Value = data[j, colIndex + 1];

                            }                                
                            else
                            {
                                dataGridView1.Rows[j + rowindex].Cells[colIndex + curntindex].Value = data[j, colIndex];
                            }
                                
                        }
                        
                    }
                    dataGridView1.Rows[j + rowindex].Cells[17].Value = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //dataGridView1.Rows[dataGridView1.Rows.Count - 2].Clone(rowCopy);
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (timer1.Enabled == true)
                toolStripMenuItem2.Text = "停止自动刷新";
            if (timer1.Enabled == false)
                toolStripMenuItem2.Text = "恢复自动刷新";
            string sqltext = ";update [YFDBBRobotData].[dbo].[TLabelContent] set rownumberf=row2 from (select ROW_NUMBER() over(order by heat_no asc)row2, REC_ID from[YFDBBRobotData].[dbo].[TLabelContent])DETAIL_B14 where[TLabelContent].REC_ID = DETAIL_B14.REC_ID";


            YCGBDesignResult_Load(null, null);
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            if (timer1.Enabled == true && toolStripMenuItem2.Text == "停止自动刷新")
            { timer1.Enabled = false;
                toolStripMenuItem2.Text = "恢复自动刷新"; }
            else//if (timer1.Enabled == false && toolStripMenuItem2.Text == "恢复自动刷新")
            { timer1.Enabled = true;
                toolStripMenuItem2.Text = "停止自动刷新"; }
            YCGBDesignResult_Load(null, null);
        }
        private void Datemodel_Click(object sender, EventArgs e)
        {
            int modelflag = 0;
            try
            {
                string sql = string.Format("select * from SYSPARAMETER  where PARAMETER_ID=15");                
                if (SetValue(sqlLabelSel))
                {
                    MessageBox.Show("数据起点设置成功！");
                }
                else
                {
                    MessageBox.Show("数据起点设置失败！");
                }
                SqlDataAdapter sda = new SqlDataAdapter(sqlLabelSel, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                YCGBDesignResult_Load(null, null);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("数据起点设置失败！", ex.Message.ToString());
            }
        }     
    }
    }