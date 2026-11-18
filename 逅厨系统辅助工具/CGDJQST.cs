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
    public partial class CGDJQST : Form
    {
        public static CGDJQST cGDJQST;

        public string fnumber, fname, guige, uname;

        public CGDJQST()
        {
            InitializeComponent();
            cGDJQST = this;

        }

        private void CGDJQST_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            cb_org.SelectedIndex = 0;
            cb_year.Text = DateTime.Now.Year.ToString();
            lblFnumber.Text = "";
            lblfname.Text = "";
            lblguige.Text = "";
            lbluname.Text = "";

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            CGDJQST_WL cGDJQST_WL = new CGDJQST_WL();
            cGDJQST_WL.orgName = cb_org.Text;
            cGDJQST_WL.year = int.Parse(cb_year.Text.ToString());
            cGDJQST_WL.wlName = tb_wlname.Text;
            if (cGDJQST_WL.ShowDialog(this) == DialogResult.OK)
            {
                lblFnumber.Text = fnumber;
                lblfname.Text = fname;
                lblguige.Text = guige;
                lbluname.Text = uname;
                Report report = new Report();
                report.Load(@".\CGPRICE.frx");
                report.SetParameterValue("orgname", cb_org.Text);
                report.SetParameterValue("year", int.Parse(cb_year.Text.ToString()));
                report.SetParameterValue("wlfnumber", fnumber);
                report.Preview = reportview;
                report.Show();
            }
        }
    }
}
