using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class ChooseType : Form
    {
        public ChooseType()
        {
            InitializeComponent();
            btn69.Visible = false;
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            Test20 test20 = new Test20();
            test20.ShowDialog();
        }

        private void btn69_Click(object sender, EventArgs e)
        {
            MessageBox.Show("该功能未启用");
        }
    }
}
