using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnBRobotSystem.ChildForm
{
    public partial class CMessageBox : Form
    {
        public bool m_Flag;
        public CMessageBox()
        {
            m_Flag = false;
            InitializeComponent();
        }

        private void BT_OK_Click(object sender, EventArgs e)
        {
            m_Flag = CHECK_REPLACE.Checked;
            this.Close();
        }

        private void BT_CANCEL_Click(object sender, EventArgs e)
        {
            m_Flag = false;
            this.Close();
        }
    }
}
