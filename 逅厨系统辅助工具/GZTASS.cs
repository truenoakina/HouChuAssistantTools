using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class GZTASS : Form
    {
        public GZTASS()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NoBomGy noBomGy = new NoBomGy();
            noBomGy.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BOMGYNAME bomgyname = new BOMGYNAME();
            bomgyname.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UNUnitCheck unUnitCheck = new UNUnitCheck();
            unUnitCheck.ShowDialog();
        
        }
    }
}
