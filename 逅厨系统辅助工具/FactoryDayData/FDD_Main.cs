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
    public partial class FDD_Main : Form
    {
        public FDD_Main()
        {
            InitializeComponent();
        }

        private void btn_depinfo_Click(object sender, EventArgs e)
        {
            DepInfo depInfo = new DepInfo();
            depInfo.ShowDialog();
        }

        private void btn_empinfo_Click(object sender, EventArgs e)
        {
            EmployeeInfo employeeInfo = new EmployeeInfo();
            employeeInfo.ShowDialog();
        }

        private void btn_AttendanceConfig_Click(object sender, EventArgs e)
        {
            //从这里进入参数设置，默认myvar.fddConfigDate="1999-01-01"
            MyVar.FDDconfigDate = new DateTime(1999, 1, 1);
            AttendanceConfig attendanceConfig = new AttendanceConfig();
            attendanceConfig.ShowDialog();
        }

        private void btn_AttendanceWrite_Click(object sender, EventArgs e)
        {
            AttendanceWrite attendanceWrite = new AttendanceWrite();
            attendanceWrite.ShowDialog();
        }

        private void FDD_Main_Load(object sender, EventArgs e)
        {
            //string sql = @"select dayHours,regularHours from FDD_Config where 1=@data";
            //DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
            //MyVar.DayHours = decimal.Parse(Properties.Settings.Default.FDD_dayHours.ToString());
            //MyVar.RegularHours = decimal.Parse(Properties.Settings.Default.FDD_regularHours.ToString());
            //MyVar.DiningTime= decimal.Parse(Properties.Settings.Default.FDD_diningTime.ToString());
            //MyVar.IsWuCanKJ = Properties.Settings.Default.FDD_isWuCanKJ;
            //MyVar.IsWanCanKJ= Properties.Settings.Default.FDD_isWanCanKJ;
            //MyVar.WaterPrice= Properties.Settings.Default.FDD_waterPrice;
            //MyVar.ElecPrice = Properties.Settings.Default.FDD_elecPrice;
            //MyVar.NgPrice = Properties.Settings.Default.FDD_ngPrice;
            //MyVar.SteamPrice = Properties.Settings.Default.FDD_steamPrice;
            //MyVar.RegEmpPrice= Properties.Settings.Default.FDD_regEmpPrice;
            //MyVar.TempEmpPrice = Properties.Settings.Default.FDD_tempEmpPrice;
            //MyVar.ManualFixValue= Properties.Settings.Default.FDD_manualFixValue;
        }

        private void btn_energyWrite_Click(object sender, EventArgs e)
        {
            EnergyWrite energyWrite = new EnergyWrite();
            energyWrite.ShowDialog();
        }

        private void btn_holiday_Click(object sender, EventArgs e)
        {
            HolidaySet holidaySet = new HolidaySet();
            holidaySet.ShowDialog();
        }

        private void btn_Attreport_Click(object sender, EventArgs e)
        {
            AttendanceReport attendanceReport = new AttendanceReport();
            attendanceReport.ShowDialog();
        }

        private void btn_saleCostReport_Click(object sender, EventArgs e)
        {
            SaleCostReport saleCostReport = new SaleCostReport();
            saleCostReport.ShowDialog();
        }

        private void btn_hoursAdjust_Click(object sender, EventArgs e)
        {
            HoursAdjust hoursAdjust = new HoursAdjust();
            hoursAdjust.ShowDialog();
        }

        private void btn_log_Click(object sender, EventArgs e)
        {
            LogFind logFind = new LogFind();
            logFind.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DaySummaryData daySummaryData = new DaySummaryData();
            daySummaryData.ShowDialog();
        }
    }
}
