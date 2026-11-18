using FastReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class Test111 : Form
    {
        public Test111()
        {
            InitializeComponent();
        }

        private void Test111_Load(object sender, EventArgs e)
        {
            string parentDir = @".\report";
            Report report = new Report();
            report.Preview = previewControl1;
            report.Load(parentDir + @"\GztBushiCl.frx");
            report.SetParameterValue("fdate", "2023-09-27");
            report.Prepare(true);
            report.Show();
        }
    }
}
