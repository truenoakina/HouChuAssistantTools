using FastReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class PurchaseOrder : Form
    {
        private string parentDir = "";  //报表所在目录文件夹名称
        public PurchaseOrder()
        {
            InitializeComponent();
        }

        private void PurchaseOrder_Load(object sender, EventArgs e)
        {
            parentDir = @".\report\school";
            begindate.Value = DateTime.Now;
            enddate.MinDate = begindate.Value;
            enddate.MaxDate = begindate.Value.AddDays(6);
            enddate.Value = begindate.Value.AddDays(6);
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Preview = previewControl1;
            try
            {
                report.Load(parentDir + @"\schoolcghz.frx");
            }
            catch (FileNotFoundException ee)
            {
                MessageBox.Show("未找到报表文件，请联系管理员”");

            }
            report.SetParameterValue("begindate", begindate.Text);
            report.SetParameterValue("enddate", enddate.Text);
            report.Prepare(true);
            report.Show();
        }

        private void begindate_ValueChanged(object sender, EventArgs e)
        {
            enddate.MaxDate = begindate.Value.AddDays(6);
            enddate.MinDate = begindate.Value;
            enddate.Value = begindate.Value.AddDays(6);
        }
    }
}
