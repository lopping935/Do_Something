using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logtest;
using Hopchelper;
using System.Threading;
namespace record_data
{
    class Program
    {

        public static ZT_record ZT=new ZT_record();
        static void Main(string[] args)
        {
            ZT.open_plc();
            Thread record = new Thread(ZT.record_data);
            record.Start();
        }
  



    }
}
