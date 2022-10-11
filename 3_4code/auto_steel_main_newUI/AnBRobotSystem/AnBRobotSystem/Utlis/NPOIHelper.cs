using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace AnBRobotSystem
{
    class NPOIHelper
    {
        public class ExcelUtility
        {
            /// <summary>
            /// 将excel导入到datatable
            /// </summary>
            /// <param name="filePath">excel路径</param>
            /// <param name="isColumnName">第一行是否是列名</param>
            /// <returns>返回datatable</returns>
            public static DataTable ExcelToDataTable(string filePath, bool isColumnName)
            {
                DataTable dataTable = null;
                FileStream fs = null;
                DataColumn column = null;
                DataRow dataRow = null;
                IWorkbook workbook = null;
                ISheet sheet = null;
                IRow row = null;
                ICell cell = null;
                int startRow = 0;
                try
                {
                    using (fs = File.OpenRead(filePath))
                    {
                        // 版本后缀控制
                        if (filePath.IndexOf(".xlsx") > 0)
                            workbook = new XSSFWorkbook(fs);
                        // 版本后缀控制
                        else if (filePath.IndexOf(".xls") > 0)
                            workbook = new HSSFWorkbook(fs);

                        if (workbook != null)
                        {
                            sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet
                            dataTable = new DataTable();
                            if (sheet != null)
                            {
                                int rowCount = sheet.LastRowNum;//总行数
                                if (rowCount > 0)
                                {
                                    IRow firstRow = sheet.GetRow(0);//第一行
                                    int cellCount = firstRow.LastCellNum;//列数

                                    //构建datatable的列
                                    if (isColumnName)
                                    {
                                        startRow = 1;//如果第一行是列名，则从第二行开始读取
                                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                        {
                                            cell = firstRow.GetCell(i);
                                            if (cell != null)
                                            {
                                                if (cell.StringCellValue != null)
                                                {
                                                    column = new DataColumn(cell.StringCellValue);
                                                    dataTable.Columns.Add(column);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                        {
                                            column = new DataColumn("column" + (i + 1));
                                            dataTable.Columns.Add(column);
                                        }
                                    }

                                    //填充行
                                    for (int i = startRow; i <= rowCount; ++i)
                                    {
                                        row = sheet.GetRow(i);
                                        if (row == null) continue;

                                        dataRow = dataTable.NewRow();
                                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                                        {
                                            cell = row.GetCell(j);
                                            if (cell == null)
                                            {
                                                dataRow[j] = "";
                                            }
                                            else
                                            {
                                                //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)
                                                switch (cell.CellType)
                                                {
                                                    case CellType.Blank:
                                                        dataRow[j] = "";
                                                        break;
                                                    case CellType.Numeric:
                                                        short format = cell.CellStyle.DataFormat;
                                                        //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理
                                                        if (format == 14 || format == 31 || format == 57 || format == 58)
                                                            dataRow[j] = cell.DateCellValue;
                                                        else
                                                            dataRow[j] = cell.NumericCellValue;
                                                        break;
                                                    case CellType.String:
                                                        dataRow[j] = cell.StringCellValue;
                                                        break;
                                                }
                                            }
                                        }
                                        dataTable.Rows.Add(dataRow);
                                    }
                                }
                            }
                        }
                    }
                    return dataTable;
                }
                catch (Exception)
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                    return null;
                }
            }
        }
        public static bool DataTableToExcel(DataTable dt, string txtPath)
        {
            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet0");//创建一个名称为Sheet0的表
                    int rowCount = dt.Rows.Count;//行数
                    int columnCount = dt.Columns.Count;//列数

                    //设置列头
                    row = sheet.CreateRow(0);//excel第一行设为列头
                    for (int c = 0; c < columnCount; c++)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(dt.Columns[c].ColumnName);
                    }

                    //设置每行每列的单元格,
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                    using (fs = File.OpenWrite(txtPath))
                    {
                        workbook.Write(fs);//向打开的这个xls文件中写入数据
                        result = true;
                    }
                }
                MessageBox.Show("导出成功");
                return result;
            }
            catch (Exception)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return false;
            }

        }
        //private void uiButton2_Click(object sender, EventArgs e)
        //{
        //    //打开文件对话框，导出文件
        //    //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        //    saveFileDialog2.Title = "保存文件";
        //    saveFileDialog2.Filter = "Excel 文件(*.xls)|*.xls|Excel 文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
        //    saveFileDialog2.FileName = "折铁信息.xls"; //设置默认另存为的名字
        //    if (this.saveFileDialog2.ShowDialog() == DialogResult.OK)
        //    {
        //        string txtPath = this.saveFileDialog2.FileName;
        //        //string sql = "select ID as ID,UserName as 用户名,LoginAccount as 账号,UserPower as 用户权限,Founder as 创建者,Addtime as 创建日期,Activestate as 状态 from UserData";
        //        string sql = string.Format("SELECT [fishstation] 位置,[carnum] 罐号,[bagnum] 包号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 目标毛重,[fall_weight] 实际毛重,[tb_hight] 净空值,[fall_time] 折铁用时,[fall_state] 完成状态 FROM [AutoSteel].[dbo].[History] WHERE [startime]>='{0}' and [startime]<='{1}' and convert(float,i_need_weight)>0 and convert(float,fall_weight)>0 order by startime DESC", uiDatetimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), uiDatetimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));

        //        //string sql = string.Format("SELECT TOP 100 [fishstation] 位置,[carnum] 罐号,[bagnum] 包号,[is_full] 罐状态,[startime] 开始倾倒,[stoptime] 结束倾倒,[i_need_weight] 目标毛重,[fall_weight] 实际毛重,[tb_hight] 净空值,[fall_time] 折铁用时,[fall_state] 完成状态 FROM [AutoSteel].[dbo].[History] where fall_time!='' and fall_weight!='0' and CONVERT(float,i_need_weight)>0 order by startime DESC");
        //        DataTable dt2 = db.MultithreadDataTable(sql);
        //        //SqlHelp sqlHelper = new SqlHelp();
        //        //DataTable dt = sqlHelper.GetDataTableValue(sql);
        //        NPOIHelper.DataTableToExcel(dt2, txtPath);
        //    }

        //}
    }
}
