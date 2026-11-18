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
    public partial class DaySummaryData : Form
    {
        private string id;
        private bool hasData;//是否有数据
        private DateTime originalDate;//记录datetimepicker选择前的数据

        public DaySummaryData()
        {
            InitializeComponent();
            InitDataGridViewColumns();
        }

        private void DaySummaryData_Load(object sender, EventArgs e)
        {
            dateMonth.Format = DateTimePickerFormat.Custom;
            dateMonth.CustomFormat = "yyyy-MM";
            cb_factory.SelectedIndex = 0;
            
            originalDate = dateTimePicker1.Value.Date;//取当前日期框日期，如果用户重新选择时不配置参数，回到这个值
            refresh();
        }

        private void InitDataGridViewColumns()
        {
            // 禁止自动生成列
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            // 列1
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "factory";
            col1.HeaderText = "工厂";
            col1.DataPropertyName = "factory";
            col1.Width = 100;
            // 列1
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "fdate";
            col2.HeaderText = "日期";
            col2.DataPropertyName = "fdate";
            col2.Width = 120;

            // 列2
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "attEmpNum";
            col3.HeaderText = "当日正式工出勤人数";
            col3.DataPropertyName = "attEmpNum";
            col3.Width = 160;

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = "zshours";
            col4.HeaderText = "正式工工时";
            col4.DataPropertyName = "zshours";
            col4.Width = 120;

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "lshours";
            col5.HeaderText = "临时工工时";
            col5.DataPropertyName = "lshours";
            col5.Width = 120;


            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.Name = "id";
            col6.HeaderText = "id";
            col6.DataPropertyName = "id";
            col6.Width = 0;
            // 添加到控件
            dataGridView1.Columns.AddRange(col1, col2, col3, col4, col5, col6);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 30;
            dataGridView1.Columns["factory"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["fdate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["attEmpNum"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["zshours"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["lshours"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Columns[5].Visible = false;  ///隐藏id
        }



        private void refresh()
        {
            string sql = @"SELECT factory,[fdate],[attEmpNum],[zshours],[lshours],[id]
                          FROM [dbo].[FDD_DaySummaryData] where factory=@factory  
                    and year(fdate)=@year and MONTH(fdate)=@month order by fdate";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", cb_factory.Text),
               SQLHelper.MakeParam("year", dateMonth.Value.Year), SQLHelper.MakeParam("month", dateMonth.Value.Month));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                dataGridView1.DataSource = null;
                
            }

            //载入前一天的明细
            dateTimePicker1.Value = DateTime.Now.AddDays(-1).Date;
            string sql2 = @"select [fdate],[attEmpNum],[zshours],[lshours],[id]
                          FROM [dbo].[FDD_DaySummaryData] where factory=@factory and fdate=@fdate";
            DataSet ds2 = SQLHelper.GetDataSet(sql2, SQLHelper.MakeParam("factory", cb_factory.Text),
            SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
            if (ds2.Tables[0].Rows.Count > 0)
            {
                hasData = true;
                tb_attEmpNum.Text = ds2.Tables[0].Rows[0][1].ToString();
                tb_zshours.Text = ds2.Tables[0].Rows[0][2].ToString();
                tb_lshours.Text = ds2.Tables[0].Rows[0][3].ToString();
                id = ds2.Tables[0].Rows[0][4].ToString();
            }
            else
            {
                hasData = false;
                tb_attEmpNum.Text = "";
                tb_zshours.Text = "";
                tb_lshours.Text = "";
                return;
            }

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (tb_attEmpNum.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_attEmpNum.Location.X + tb_attEmpNum.Width + 20, tb_attEmpNum.Location.Y + tb_attEmpNum.Height - 10, this.Text, "请填写当日出勤人数！", this);
                return;
            }
            else if (tb_zshours.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_zshours.Location.X + tb_zshours.Width + 20, tb_zshours.Location.Y + tb_zshours.Height - 10, this.Text, "请填写正式工工时！", this);
                return;
            }
            else if (tb_lshours.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_lshours.Location.X + tb_lshours.Width + 20, tb_lshours.Location.Y + tb_lshours.Height - 10, this.Text, "请填写临时工工时！", this);
                return;
            }
           

            if (hasData)
            {
                DialogResult dr = MessageBox.Show("该工厂当日已有填报数据，是否修改？", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    string sql = @"update FDD_DaySummaryData set attEmpNum=@attEmpNum,zshours=@zshours,
                                  lshours=@lshours where id=@id";
                    if (SQLHelper.ExecSql(sql,
                                   SQLHelper.MakeParam("attEmpNum", tb_attEmpNum.Text),
                                   SQLHelper.MakeParam("zshours", tb_zshours.Text),
                                   SQLHelper.MakeParam("lshours", tb_lshours.Text),
                                   SQLHelper.MakeParam("id", id)))
                    {
                        MsgBox.TipMsgShow(btn_OK.Location.X + btn_OK.Width + 20, btn_OK.Location.Y + btn_OK.Height - 10, this.Text, "修改成功！", this);
                        refresh();
                    }
                    else
                    {
                        MsgBox.TipMsgShow(btn_OK.Location.X + btn_OK.Width + 20, btn_OK.Location.Y + btn_OK.Height - 10, this.Text, "修改失败，请联系管理员！", this);
                    }
                }

            }
            else
            {
                string sql = @"insert into FDD_DaySummaryData values (@fdate,@factory,@attEmpNum,@zshours,@lshours)";
                if (SQLHelper.ExecSql(sql,
                                   SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date),
                                   SQLHelper.MakeParam("factory", cb_factory.Text),
                                   SQLHelper.MakeParam("attEmpNum", tb_attEmpNum.Text),
                                   SQLHelper.MakeParam("zshours", tb_zshours.Text),
                                   SQLHelper.MakeParam("lshours", tb_lshours.Text)))
                {
                    MsgBox.TipMsgShow(btn_OK.Location.X + btn_OK.Width + 20, btn_OK.Location.Y + btn_OK.Height - 10, this.Text, "保存成功！", this);
                    refresh();
                }
                else
                {
                    MsgBox.TipMsgShow(btn_OK.Location.X + btn_OK.Width + 20, btn_OK.Location.Y + btn_OK.Height - 10, this.Text, "保存失败，请联系管理员！", this);
                }
            }
        }

        private void tb_attEmpNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = true;
        }

        private void tb_allhours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_lshours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }


        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_factory_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string sql = @"select * from FDD_Config where fdate=@fdate";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DialogResult dr = MessageBox.Show("该日期未设置参数，请设置", "提示", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        MyVar.FDDconfigDate = dateTimePicker1.Value.Date;
                        AttendanceConfig attendanceConfig = new AttendanceConfig();
                        attendanceConfig.ShowDialog();
                        originalDate = dateTimePicker1.Value.Date;
                    }
                    else
                    {
                        dateTimePicker1.Value = originalDate;
                        return;
                    }
                }
                else
                {
                    originalDate = dateTimePicker1.Value.Date;
                }
            }

             sql = @"SELECT [fdate],[attEmpNum],[zshours],[lshours],[id]
                          FROM [dbo].[FDD_DaySummaryData] where factory=@factory  
                    and fdate=@fdate";
             ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", cb_factory.Text),
               SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
            if (ds.Tables[0].Rows.Count > 0)
            {
                hasData = true;
                tb_attEmpNum.Text = ds.Tables[0].Rows[0][1].ToString();
                tb_zshours.Text = ds.Tables[0].Rows[0][2].ToString();
                tb_lshours.Text = ds.Tables[0].Rows[0][3].ToString();
                id= ds.Tables[0].Rows[0][4].ToString();
            }
            else
            {
                hasData = false;
                tb_attEmpNum.Text = "";
                tb_zshours.Text = "";
                tb_lshours.Text = "";
                return;
            }
        }

        private void dateMonth_ValueChanged(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
