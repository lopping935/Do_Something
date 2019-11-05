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
        int intindex = 0;
        SqlDataAdapter adapter;
        SqlConnection conn;
        string sqlLabelSel = "";
        int count = 0;
        public YCGBDesignResult()
        {
            InitializeComponent();
            db = new DbHelper(inisql.GetConnectionString("SysSQL"));
            conn = new SqlConnection("Data Source=.;Initial Catalog=MDBAnBRobotData;User ID=sa; Password=6923263;Enlist=true;Pooling=true;Connection Timeout=30;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000");
        }
                      

        private void YCGBDesignResult_Load(object sender, EventArgs e)
        {
            double MAXRECID = 0;// PLANIDNow = 0;  

            DataTable dt1 = new DataTable("MyDT");
            string sql = "select MAX(REC_ID) AS REC_ID from TLabelContent WHERE IMP_FINISH=31 or IMP_FINISH=32 or IMP_FINISH=33";
            DbDataReader dr = null;
            dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
            while (dr.Read())
            {
                if (dr["REC_ID"] != DBNull.Value)
                MAXRECID = Convert.ToDouble(dr["REC_ID"].ToString());
            }
            dr.Close();
            sqlLabelSel = string.Format("select top 100 REC_ID 流水号,MACHINE_NO 打包机组号,ID_LOT_PROD 生产批号,ID_PART_LOT 分批号,NUM_BDL 捆号, SEQ_LEN 长度顺序号, SEQ_OPR 操作顺序号, DIM_LEN 米长, IND_FIXED 定尺标志, SEQ_SEND 下发顺序号, NUM_BAR 捆内支数, SEQ_LIST 排列序号, LA_BDL_ACT 重量, NO_LICENCE 许可证号, NAME_PROD 产品名称, NAME_STND 执行标准, ID_HEAT 熔炼号, NAME_STLGD 钢牌号, DES_FIPRO_SECTION 断面规格描述, ID_CREW_RL 轧制班别, ID_CREW_CK 检查班别, TMSTP_WEIGH 生产日期, BAR_CODE 条码内容, NUM_HEAD 头签个数, NUM_TAIL 尾签个数,L3TMSTP_SEND MES发送时刻,IMP_FINISH 状态信息,REC_IMP_TIME 状态更新时刻 from TLabelContent WHERE REC_ID>={0} order by REC_ID ASC", MAXRECID - 2);
            try
            {
                dt1 = db.ExecuteDataTable(db.GetSqlStringCommond(sqlLabelSel));
            }
            catch (Exception ex)
            {
                //log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                //Log.addLog(log, LogType.ERROR, ex.Message);
            }
            if (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
            }
            if (dt1.Rows.Count != 0)//has record,maybe is all didn't used date,maybe all used;when you repain the datagirdview,it would has error
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.DataSource = dt1;
                for(int i=0;i<dt1.Rows.Count;i++)
                {
                    if (double.Parse(dt1.Rows[i][0].ToString()) == MAXRECID)
                    {
                        count = i;
                        break;
                    }
                }
            }
            else//the table has 0 record
            {
               // MessageBox.Show("当前数据为空，请检查数据！");
                //return;
                
                sqlLabelSel = string.Format("select top 30  REC_ID 流水号,MACHINE_NO	打包机组号,ID_LOT_PROD	生产批号,ID_PART_LOT	分批号,NUM_BDL	捆号, SEQ_LEN    长度顺序号, SEQ_OPR   操作顺序号, DIM_LEN   米长, IND_FIXED    定尺标志, SEQ_SEND   下发顺序号, NUM_BAR   捆内支数, SEQ_LIST   排列序号, LA_BDL_ACT 重量, NO_LICENCE   许可证号, NAME_PROD  产品名称, NAME_STND  执行标准, ID_HEAT    熔炼号, NAME_STLGD  钢牌号, DES_FIPRO_SECTION   断面规格描述, ID_CREW_RL   轧制班别, ID_CREW_CK 检查班别, TMSTP_WEIGH    生产日期, BAR_CODE   条码内容, NUM_HEAD 头签个数, NUM_TAIL 尾签个数,L3TMSTP_SEND MES发送时刻,IMP_FINISH 状态信息,REC_IMP_TIME 状态更新时刻 from TLabelContent WHERE REC_ID>={0} order by ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR ASC", MAXRECID);
                try { dt1 = db.ExecuteDataTable(db.GetSqlStringCommond(sqlLabelSel)); }
                catch (Exception ex)
                {
                    //log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString() + "::" + MethodBase.GetCurrentMethod().ToString());
                    //Log.addLog(log, LogType.ERROR, ex.Message);
                }
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.DataSource = dt1;
                
                dataGridView1.Rows[dataGridView1.Rows.Count-1].DefaultCellStyle.BackColor = Color.Red;
            }

        }

        private void MenuItem_Relation_Click(object sender, EventArgs e)
        {
            
        }
        
        private DataTable dbconn(string strSql)
        {
            this.adapter = new SqlDataAdapter(strSql, conn);
            DataTable dtSelect = new DataTable();
            int mt = this.adapter.Fill(dtSelect);
            return dtSelect;
        }
        private Boolean dbUpdate(string strSql)
        {
            DataTable dtUpdate = new DataTable();
            dtUpdate = this.dbconn(strSql);
            dtUpdate.Rows.Clear();
            DataTable dtShow = new DataTable();
            dtShow = (DataTable)this.dataGridView1.DataSource;
            dtUpdate.ImportRow(dtShow.Rows[intindex]);
            try
            {
                SqlCommandBuilder CommandBuilder;
                CommandBuilder = new SqlCommandBuilder(this.adapter);
                this.adapter.Update(dtUpdate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
            dtUpdate.AcceptChanges();
            return true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)//删除选中数据
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择一行信息！");
                return;
            }
            
            string sql = "";
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                sql = sql+ string.Format("delete from TLabelContent where ID_LOT_PROD='{0}' and ID_PART_LOT={1} and NUM_BDL={2} and SEQ_LEN={3} and SEQ_OPR={4};", dataGridView1.SelectedRows[i].Cells[2].Value.ToString(), Convert.ToInt16(dataGridView1.SelectedRows[i].Cells[3].Value.ToString()), Convert.ToInt16(dataGridView1.SelectedRows[i].Cells[4].Value.ToString()), Convert.ToInt16(dataGridView1.SelectedRows[i].Cells[5].Value.ToString()), Convert.ToInt16(dataGridView1.SelectedRows[i].Cells[6].Value.ToString()));
                }
            int ret1 = db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
            if (ret1>0)
                MessageBox.Show("删除成功！");
            SqlDataAdapter sda = new SqlDataAdapter(sqlLabelSel, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.RowHeadersVisible = false;

        }       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            intindex = e.RowIndex;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)//更新数据
        {
            try { 
            if (dbUpdate(sqlLabelSel))
                MessageBox.Show("修改成功！");
            SqlDataAdapter sda = new SqlDataAdapter(sqlLabelSel, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
           
            dataGridView1.RowHeadersVisible = false;
            }
            catch(Exception ex)
            {
                
                MessageBox.Show("数据更新有误！",ex.Message.ToString());
            }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)//插入数据
        {
            try
            {
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.Rows.Count - 2];
                string sql = string.Format("insert into HLabelContent(MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST,LA_BDL_ACT,NO_LICENCE,NAME_PROD,NAME_STND,ID_HEAT,NAME_STLGD,DES_FIPRO_SECTION,ID_CREW_RL,ID_CREW_CK,TMSTP_WEIGH,BAR_CODE,NUM_HEAD,NUM_TAIL,L3TMSTP_SEND,IMP_FINISH,REC_ID,REC_CREATE_TIME) values('{0}','{1}',{2},{3},{4},{5},{6},'{7}',{8},{9},{10},{11},'{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22},{23},'{24}',{25},{26},'{27}')", row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), Convert.ToInt16(row.Cells[3].Value.ToString()), Convert.ToInt16(row.Cells[4].Value.ToString()), Convert.ToInt16(row.Cells[5].Value.ToString()), Convert.ToInt16(row.Cells[6].Value.ToString()), Convert.ToInt16(row.Cells[7].Value.ToString()), row.Cells[8].Value.ToString(), Convert.ToInt32(row.Cells[9].Value.ToString()), Convert.ToInt16(row.Cells[10].Value.ToString()), Convert.ToInt16(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), row.Cells[13].Value.ToString(), row.Cells[14].Value.ToString(), row.Cells[15].Value.ToString(), row.Cells[16].Value.ToString(), row.Cells[17].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), row.Cells[20].Value.ToString(), row.Cells[21].Value.ToString(), row.Cells[22].Value.ToString(), Convert.ToInt16(row.Cells[23].Value.ToString()), Convert.ToInt16(row.Cells[24].Value.ToString()), row.Cells[25].Value.ToString(), Convert.ToInt16(row.Cells[26].Value.ToString()), Convert.ToDouble(row.Cells[0].Value.ToString()), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
                sql = string.Format("insert into TLabelContent(MACHINE_NO,ID_LOT_PROD,ID_PART_LOT,NUM_BDL,SEQ_LEN,SEQ_OPR,DIM_LEN,IND_FIXED,SEQ_SEND,NUM_BAR,SEQ_LIST,LA_BDL_ACT,NO_LICENCE,NAME_PROD,NAME_STND,ID_HEAT,NAME_STLGD,DES_FIPRO_SECTION,ID_CREW_RL,ID_CREW_CK,TMSTP_WEIGH,BAR_CODE,NUM_HEAD,NUM_TAIL,L3TMSTP_SEND,IMP_FINISH,REC_ID,REC_CREATE_TIME) values('{0}','{1}',{2},{3},{4},{5},{6},'{7}',{8},{9},{10},{11},'{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22},{23},'{24}',{25},{26},'{27}')", row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), Convert.ToInt16(row.Cells[3].Value.ToString()), Convert.ToInt16(row.Cells[4].Value.ToString()), Convert.ToInt16(row.Cells[5].Value.ToString()), Convert.ToInt16(row.Cells[6].Value.ToString()), Convert.ToInt16(row.Cells[7].Value.ToString()), row.Cells[8].Value.ToString(), Convert.ToInt32(row.Cells[9].Value.ToString()), Convert.ToInt16(row.Cells[10].Value.ToString()), Convert.ToInt16(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), row.Cells[13].Value.ToString(), row.Cells[14].Value.ToString(), row.Cells[15].Value.ToString(), row.Cells[16].Value.ToString(), row.Cells[17].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), row.Cells[20].Value.ToString(), row.Cells[21].Value.ToString(), row.Cells[22].Value.ToString(), Convert.ToInt16(row.Cells[23].Value.ToString()), Convert.ToInt16(row.Cells[24].Value.ToString()), row.Cells[25].Value.ToString(), Convert.ToInt16(row.Cells[26].Value.ToString()), Convert.ToDouble(row.Cells[0].Value.ToString()), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int ret1 = db.ExecuteNonQuery(db.GetSqlStringCommond(sql));
                if (ret1 > 0)
                {
                    sql = "select PARAMETER_VALUE from SYSPARAMETER where PARAMETER_ID=10";
                    DbDataReader dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
                    double ProductIDA = 0;
                    while (dr.Read())
                    {
                        if (dr["PARAMETER_VALUE"] != DBNull.Value)
                            ProductIDA = Convert.ToDouble(dr["PARAMETER_VALUE"]);
                    }
                    if (Convert.ToDouble(row.Cells[0].Value.ToString()) > ProductIDA)
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
                dataGridView1.RowHeadersVisible = false;
            }
            catch
            {
                MessageBox.Show("插入失败,可能存在重复数据！");
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int countn = 0;
            int IMP_FINISH = 0;
            if (dataGridView1.RowCount>count+2)
            {
                countn = count+2;
            }
             else
            {
                countn = dataGridView1.RowCount-1;

            }

            for (int i = 0; i < countn; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];

                //string sql = string.Format("SELECT IMP_FINISH FROM TLabelContent where ID_LOT_PROD='{0}' and ID_PART_LOT={1} and NUM_BDL={2} and SEQ_LEN={3} and SEQ_OPR={4};", row.Cells[2].Value.ToString(), Convert.ToInt16(row.Cells[3].Value.ToString()), Convert.ToInt16(row.Cells[4].Value.ToString()), Convert.ToInt16(row.Cells[5].Value.ToString()), Convert.ToInt16(row.Cells[6].Value.ToString()));
                //DbDataReader dr = null;
                IMP_FINISH = int.Parse( row.Cells[26].Value.ToString());
                //dr = db.ExecuteReader(db.GetSqlStringCommond(sql));
                //while (dr.Read())
                //{
                //    if (dr["IMP_FINISH"] != DBNull.Value)
                //        IMP_FINISH = Convert.ToInt32(dr["IMP_FINISH"].ToString());
                //    else

                //    {
                //        MessageBox.Show("数据为空！");
                //        return;
                //    }
                //}
                //dr.Close();
                if (IMP_FINISH == 31 || IMP_FINISH == 32 || IMP_FINISH == 33)
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
                if (IMP_FINISH == 0 && (i < (countn - 1)))
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                if (IMP_FINISH == 0 && (i == (countn - 1)))
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
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
                int tnum = 0;
                int nnum = 0;
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
                        if(colIndex + 1<28)
                        { if(data[j, 0]=="")
                                dataGridView1.Rows[j + rowindex].Cells[colIndex + curntindex].Value = data[j, colIndex + 1];
                        else
                                dataGridView1.Rows[j + rowindex].Cells[colIndex + curntindex].Value = data[j, colIndex];
                        }
                        
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //dataGridView1.Rows[dataGridView1.Rows.Count - 2].Clone(rowCopy);
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            YCGBDesignResult_Load(null, null);
            if (timer1.Enabled == true)
                toolStripMenuItem2.Text = "停止自动刷新";
            if (timer1.Enabled == false)
                toolStripMenuItem2.Text = "恢复自动刷新";
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)//自动更新开关
        {
            if (timer1.Enabled == true && toolStripMenuItem2.Text == "停止自动刷新")
            { timer1.Enabled = false;
                toolStripMenuItem2.Text = "恢复自动刷新"; }
            else//if (timer1.Enabled == false && toolStripMenuItem2.Text == "恢复自动刷新")
            { timer1.Enabled = true;
                toolStripMenuItem2.Text = "停止自动刷新"; }
        }
    }
    }