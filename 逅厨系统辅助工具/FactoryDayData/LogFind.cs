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
    public partial class LogFind : Form
    {
        public LogFind()
        {
            InitializeComponent();
        }

        private void LogFind_Load(object sender, EventArgs e)
        {
            cb_operType.SelectedIndex = 0;
            // 禁止自动生成列
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            

            // 列1
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "ftype";
            col1.HeaderText = "操作类型";
            col1.DataPropertyName = "ftype";
            col1.Width = 100;

            // 列2
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "operator";
            col2.HeaderText = "操作员";
            col2.DataPropertyName = "operator";
            col2.Width = 100;

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "pcname";
            col3.HeaderText = "计算机名称";
            col3.DataPropertyName = "pcname";
            col3.Width = 100;

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = "opertime";
            col4.HeaderText = "操作时间";
            col4.DataPropertyName = "opertime";
            col4.Width = 120;

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "cfgdate";
            col5.HeaderText = "参数日期";
            col5.DataPropertyName = "cfgdate";
            col5.Width = 100;

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.Name = "flog";
            col6.HeaderText = "日志";
            col6.DataPropertyName = "flog";
            col6.Width = 1000;


            // 添加到控件
            dataGridView1.Columns.AddRange(col1, col2, col3, col4, col5, col6);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 30;
            dataGridView1.Columns["ftype"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["operator"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["pcname"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["opertime"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["cfgdate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["flog"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            string sql = @"select ftype,operator,pcname,opertime,cfgdate,flog from FDD_log where 
                            convert(date,opertime)>=@begintime and convert(date,opertime)<=@endtime and ftype=@ftype";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("begintime", begindate.Value.Date),
                SQLHelper.MakeParam("endtime", enddate.Value.Date), SQLHelper.MakeParam("ftype", cb_operType.Text));
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
    }
}
