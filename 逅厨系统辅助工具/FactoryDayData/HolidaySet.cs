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
    public partial class HolidaySet : Form
    {
        public static HolidaySet holidaySet;
        
        public HolidaySet()
        {
            InitializeComponent();
            holidaySet = this;
            InitDataGridViewColumns();
        }

        private void HolidaySet_Load(object sender, EventArgs e)
        {
            begindate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            enddate.Value= new DateTime(DateTime.Now.Year, 12, 31);
            lbl_beizhu.Text = "法定节假日正编工时成本计算工式为：正编出勤人数*(三倍薪资基数/三倍工作日基数*3+一倍薪资基数/一倍工作日基数)+正编未出勤人数*(一倍薪资基数/一倍工作日基数)";
            refresh();
        }

        private void InitDataGridViewColumns()
        {
            // 禁止自动生成列
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            

            // 列1
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "fdate";
            col1.HeaderText = "日期";
            col1.DataPropertyName = "fdate";
            col1.Width = 100;

            // 列2
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "ZBCQRS";
            col2.HeaderText = "正编出勤人数";
            col2.DataPropertyName = "ZBCQRS";
            col2.Width = 100;

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "ZBWCQRS";
            col3.HeaderText = "正编未出勤人数";
            col3.DataPropertyName = "ZBWCQRS";
            col3.Width = 120;

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = "Salary_3";
            col4.HeaderText = "三倍薪资基数";
            col4.DataPropertyName = "Salary_3";
            col4.Width = 130;

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "EmpNum_3";
            col5.HeaderText = "三倍工作日基数";
            col5.DataPropertyName = "EmpNum_3";
            col5.Width = 130;

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.Name = "Salary_1";
            col6.HeaderText = "一倍薪资基数";
            col6.DataPropertyName = "Salary_1";
            col6.Width = 130;

            DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
            col7.Name = "EmpNum_1";
            col7.HeaderText = "一倍工作日基数";
            col7.DataPropertyName = "EmpNum_1";
            col7.Width = 130;

            DataGridViewTextBoxColumn col8 = new DataGridViewTextBoxColumn();
            col8.Name = "id";
            col8.HeaderText = "id";
            col8.DataPropertyName = "id";
            col8.Width = 0;
            // 添加到控件
            dataGridView1.Columns.AddRange(col1, col2, col3, col4, col5, col6, col7,col8);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 30;
            dataGridView1.Columns["fdate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["ZBCQRS"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["ZBWCQRS"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["Salary_3"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["EmpNum_3"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["Salary_1"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["EmpNum_1"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Columns[7].Visible = false;  ///隐藏id
        }


        public void refresh()
        {
            string sql = @"select fdate,ZBCQRS,ZBWCQRS,Salary_3,EmpNum_3,Salary_1,EmpNum_1,id from FDD_Holiday where fdate>=@begindate and fdate<=@enddate order by fdate";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("begindate", begindate.Value.Date),
                SQLHelper.MakeParam("enddate", enddate.Value.Date));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                dataGridView1.DataSource = null;
                return;
            }

            
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            HolidayAdd holidayAdd = new HolidayAdd();
            holidayAdd.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= 0 || e.ColumnIndex <= 0)
            {
                return;
            }
            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                DialogResult dr = MessageBox.Show("确认删除节假日“" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "”", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    string sql = @"delete from fdd_Holiday where id=@id";
                    SQLHelper.ExecSql(sql, SQLHelper.MakeParam("id", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[7].Value.ToString()));
                    MsgBox.TipMsgShow(btn_delete.Location.X + btn_delete.Width + 20, btn_delete.Location.Y + btn_delete.Height / 2 - 100, this.Text, "删除成功！", this);
                    refresh();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选中要删除的节假日");
            }
        }
    }
}
