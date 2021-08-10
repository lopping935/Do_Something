using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AnBRobotSystem.Utlis
{
    class Auto_steel_helper
    {
        public static void mainlog(string model, string strmsg)
        {
            ListViewItem iteme1 = new ListViewItem(DateTime.Now.ToString());
            iteme1.SubItems.Add(model);
            iteme1.SubItems.Add(strmsg);
            AnBRobotSystem.MdiParent.form.listView1.Items.Insert(0, iteme1);
        }
    }
}
