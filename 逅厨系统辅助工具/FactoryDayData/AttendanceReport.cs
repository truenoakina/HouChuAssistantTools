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
    public partial class AttendanceReport : Form
    {
        private string[] jzjdep;
        private string[] gztdep;
        private string parentDir = "";  //报表所在目录文件夹名称
        public AttendanceReport()
        {
            InitializeComponent();
        }

        private void AttendanceReport_Load(object sender, EventArgs e)
        {
            parentDir = @".\report\FDD";
            string sql = @"select depname from FDD_depinfo where factory=@factory";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", "佳之家"));
            if (ds.Tables[0].Rows.Count > 0)
            {
                jzjdep = ds.Tables[0].AsEnumerable().Select(r => r["depname"].ToString()).ToArray(); ;
            }
            else
            {
                return;
            }
            ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", "谷之田"));
            if (ds.Tables[0].Rows.Count > 0)
            {
                gztdep = ds.Tables[0].AsEnumerable().Select(r => r["depname"].ToString()).ToArray(); ;
            }
            else
            {
                return;
            }

            cb_factory.Text = "佳之家";
            if (jzjdep.Count() > 0)
            {
                cb_dep.DataSource = jzjdep;
                cb_dep.SelectedIndex = 0;
            }
            cb_type.SelectedIndex = 0;
            
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            ///明细
            report.Preview = previewdetail;
            try
            {
                report.Load(parentDir + @"\Attendance.frx");
                
            }
            catch (FileNotFoundException ee)
            {
                MessageBox.Show("未找到报表文件,请联系管理员");

            }
            report.SetParameterValue("factory", cb_factory.Text);
            report.SetParameterValue("depname", cb_dep.Text);
            report.SetParameterValue("ftype", cb_type.Text);
            report.SetParameterValue("fdate", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            report.Prepare(true);
            report.Show();

            
        }

        private void cb_factory_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cb_factory.Text == "佳之家")
            {
                cb_dep.DataSource = jzjdep;
            }
            else if (cb_factory.Text == "谷之田")
            {
                cb_dep.DataSource = gztdep;
            }
        }
    }
}
