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
    public partial class SaleCostReport : Form
    {
        private string parentDir = "";  //报表所在目录文件夹名称
        private string noCfgdate;  //没有配置参数的日期
        private string noSumdate;  //没有考勤的日期
        private string noEnergydate;  //没有能耗的日期
        public SaleCostReport()
        {
            InitializeComponent();
        }

        private void SaleCostReport_Load(object sender, EventArgs e)
        {
            parentDir = @".\report\FDD";
            cb_factory.SelectedIndex = 0;
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            noCfgdate = "";
            noSumdate = "";
            noEnergydate = "";
            //没有配置参数的日期
            string sql = @"WITH DateRange AS (
                            SELECT CAST(DATEFROMPARTS(YEAR(@fdate), MONTH(@fdate), 1) AS DATE) AS DateValue
                            UNION ALL
                            SELECT DATEADD(DAY, 1, DateValue) FROM DateRange WHERE DateValue < @fdate
                        )
                        SELECT dr.DateValue
                        FROM DateRange dr
                        LEFT JOIN FDD_Config cfg ON dr.DateValue =cfg.fdate
                        WHERE cfg.fdate IS NULL";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    noCfgdate += row[0].ToString().Replace("0:00:00","") + ",";
                }
                noCfgdate = "以下日期没有配置参数:"+noCfgdate.TrimEnd(',') + "\n"; // 移除末尾的换行符
            }

            //没有录入考勤汇总的日期
            sql = @"WITH DateRange AS (
                            SELECT CAST(DATEFROMPARTS(YEAR(@fdate), MONTH(@fdate), 1) AS DATE) AS DateValue
                            UNION ALL
                            SELECT DATEADD(DAY, 1, DateValue) FROM DateRange WHERE DateValue < @fdate
                        )
                    SELECT dr.DateValue
                    FROM DateRange dr
                    LEFT JOIN FDD_DaySummaryData sd ON dr.DateValue =sd.fdate and sd.factory=@factory
                    WHERE sd.fdate IS NULL";
            ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date),
              SQLHelper.MakeParam("factory", cb_factory.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    noSumdate += row[0].ToString().Replace("0:00:00", "") + ",";
                }
                noSumdate = "以下日期没有录入考勤汇总:" + noSumdate.TrimEnd(',')+"\n"; // 移除末尾的换行符
            }

            //没有录入能耗的日期
            sql = @"WITH DateRange AS (
                            SELECT CAST(DATEFROMPARTS(YEAR(@fdate), MONTH(@fdate), 1) AS DATE) AS DateValue
                            UNION ALL
                            SELECT DATEADD(DAY, 1, DateValue) FROM DateRange WHERE DateValue < @fdate
                        )
                    SELECT dr.DateValue
                    FROM DateRange dr
                    LEFT JOIN FDD_EnergyDetail ed ON dr.DateValue =ed.fdate and ed.factory=@factory
                    WHERE  ed.fdate IS NULL";
            ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date),
              SQLHelper.MakeParam("factory", cb_factory.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    noEnergydate += row[0].ToString().Replace("0:00:00", "") + ",";
                }
                noEnergydate = "以下日期没有录入能耗:"+noEnergydate.TrimEnd(',') + "\n"; // 移除末尾的换行符
            }

            lbl_alarm.Text = noCfgdate + noSumdate + noEnergydate;

            Report report = new Report();
            ///明细
            report.Preview = previewControl1;
            try
            {
                report.Load(parentDir + @"\XiaoShouChengBenFenXiBiao.frx");

            }
            catch (FileNotFoundException ee)
            {
                MessageBox.Show("未找到报表文件,请联系管理员");

            }
            report.SetParameterValue("factory", cb_factory.Text);
            report.SetParameterValue("fdate", dateTimePicker1.Value.Date);
            report.Prepare(true);
            report.Show();

        }

        
    }
}
