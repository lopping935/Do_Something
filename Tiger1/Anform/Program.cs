using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using SQLPublicClass;
using System.Threading;
namespace Anform
{
    class Program
    {
        static SqlHelper sqlhelper = new SqlHelper();
        //static DbHelper db = new DbHelper();
        static void Main(string[] args)
        {

            while(true)
            {
                
                ShowLog();
                
                Console.ReadKey();
                System.Console.Clear();
            }
        }
        public static void ShowLog()
        {
            string sql = "select [REC_CREATE_TIME] ,[CONTENT] from (select top 50 [REC_CREATE_TIME],[CONTENT] from (select [REC_CREATE_TIME],[RECV_CONTENT] as [CONTENT]  from [MESRECVLOG]  union all select [REC_CREATE_TIME],[SEND_CONTENT] as [CONTENT]  from [MESSENDLOG]  union all select[REC_CREATE_TIME],[CONTENT] from [RECVLOG]  union all select [REC_CREATE_TIME],[CONTENT] from [SENDLOG])aa order by [REC_CREATE_TIME] desc )bb order by [REC_CREATE_TIME] asc";
            DataTable dt = sqlhelper.MultithreadDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ComContent = dt.Rows[i]["CONTENT"].ToString();
                ComContent = "信息内容： " + ComContent;
                System.Console.WriteLine(ComContent);
            }

        }
            

    }
}
