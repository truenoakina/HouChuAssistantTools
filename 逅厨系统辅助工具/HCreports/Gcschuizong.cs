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
    public partial class Gcschuizong : Form
    {
        private static Gcschuizong gcschuizong;
        private string parentDir = "";  //报表所在目录文件夹名称

        public Gcschuizong()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            InitializeComponent();
        }

       

        private void PreviewReport(Report FReport)
        {
            FReport.Show();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Preview = previewControl1;
            try
            {
                report.Load(parentDir + @"\gcschuizong.frx");
            }
            catch (FileNotFoundException ee)
            {
                MessageBox.Show("未找到报表文件，请点击“主页面-其他-更新报表”");

            }
            report.SetParameterValue("scorg", comboBox1.Text);
            report.SetParameterValue("fdate", dateTimePicker1.Text);
            report.Prepare(true);
            report.Show();
        }

        private void Gcschuizong_Load(object sender, EventArgs e)
        {
            parentDir = @".\report";
        }
    }
}
