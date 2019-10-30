using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using zebra;
//using zebra.Properties;
namespace AnBRobotSystem.ChildForm
{
    public partial class print_view : Form
    {
        public print_view(Bitmap img1)
        {
            InitializeComponent();
            Bitmap img2 = new Bitmap(img1);
            pictureBox1.Image = img2;
            
        }
       
    }
}
