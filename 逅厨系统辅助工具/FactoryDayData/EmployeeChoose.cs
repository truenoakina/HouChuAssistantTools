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
    public partial class EmployeeChoose : Form
    {
        public static EmployeeChoose employeeChoose;

        private string[] jzjdep;
        private string[] gztdep;
        public int empid;
        public string empinfo;

        public EmployeeChoose()
        {
            InitializeComponent();
            employeeChoose = this;
        }

        private void EmployeeInfo_Load(object sender, EventArgs e)
        {
            string sql = @"select emp.id,emp.empname,emp.factory,emp.depname,emp.ftype from FDD_AttendanceDetail ad
                        inner join FDD_Employee emp on ad.empid=emp.id
                        inner join FDD_config cfg on ad.fdate=cfg.fdate
                        where  ad.sumhours<cfg.regularHours  and ad.fmark='出勤' and year(ad.fdate)=@year and MONTH(ad.fdate)=@month";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("year", HoursAdjust.hoursAdjust.dateTimePicker1.Value.Year),
                SQLHelper.MakeParam("month", HoursAdjust.hoursAdjust.dateTimePicker1.Value.Month));
                
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                label1.Text = "双击姓名选择";
            }
            else
            {
                dataGridView1.DataSource = null;
                label1.Text = "没有符合要求的数据";
                return;
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 30;

            dataGridView1.Columns[0].Width = 300;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[1].HeaderText = "姓名";
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[2].HeaderText = "工厂";
            dataGridView1.Columns[3].Width = 300;
            dataGridView1.Columns[3].HeaderText = "部室";
            dataGridView1.Columns[4].Width = 300;
            dataGridView1.Columns[4].HeaderText = "类别";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

       
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 & e.ColumnIndex > -1)
            {
                empid= (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                empinfo="选择的员工 ID:"+ dataGridView1.Rows[e.RowIndex].Cells[0].Value+","+ dataGridView1.Rows[e.RowIndex].Cells[1].Value+","
                     + dataGridView1.Rows[e.RowIndex].Cells[2].Value
                     + ","+ dataGridView1.Rows[e.RowIndex].Cells[3].Value + ","+ dataGridView1.Rows[e.RowIndex].Cells[4].Value;
                DialogResult = DialogResult.OK;
            }
            else
            {
                return;
            }
        }

      

       
       

       

       



    }
}
