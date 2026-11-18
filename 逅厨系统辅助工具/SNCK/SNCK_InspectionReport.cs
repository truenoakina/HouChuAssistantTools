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
    public partial class SNCK_InspectionReport : Form
    {
        private string fdate, fname, fggxh, fqty, fcyqty, jyy, shy;
        private string parentDir = "";  //报表所在目录文件夹名称

        public SNCK_InspectionReport()
        {
            InitializeComponent();
            InitDataGridViewColumns();
        }

        

        private void InitDataGridViewColumns()
        {
            // 禁止自动生成列
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            // 列1
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "fdate";
            col1.HeaderText = "订单日期";
            col1.DataPropertyName = "fdate";
            col1.Width = 100;
            // 列1
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "fname";
            col2.HeaderText = "商品名称";
            col2.DataPropertyName = "fname";
            col2.Width = 160;

            // 列2
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "fggxh";
            col3.HeaderText = "规格型号";
            col3.DataPropertyName = "fggxh";
            col3.Width = 120;

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = "fqty";
            col4.HeaderText = "生产量";
            col4.DataPropertyName = "fqty";
            col4.Width = 120;

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "fcyqty";
            col5.HeaderText = "抽样量";
            col5.DataPropertyName = "fcyqty";
            col5.Width = 120;


            // 添加到控件
            dataGridView1.Columns.AddRange(col1, col2, col3, col4, col5);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.Columns["fdate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["fname"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["fggxh"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["fqty"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["fcyqty"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //dataGridView1.Columns[5].Visible = false;  ///隐藏id
            //整行选中,单选
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        

        private void refresh()
        {
            string sql = @"select  convert(varchar(100),t.fdate,102) as fdate,ml.fname,ml.FSPECIFICATION as fggxh,
                cast(t.fqty as int) as fqty,
                case when t.fqty<3 then 0
                     when t.fqty>=3 and t.fqty<=69 then 3
                     when t.fqty>=70 and t.fqty<=89 then 4
                    when t.fqty>=90 and t.fqty<=109 then 5
                    when t.fqty>=110 and t.fqty<=129 then 6
                    when t.fqty>=130 and t.fqty<=149 then 7
                    when t.fqty>=150 and t.fqty<=169 then 8
                    when t.fqty>=170 and t.fqty<=189 then 9
                    when t.fqty>=190 and t.fqty<=209 then 10
                    when t.fqty>=210 and t.fqty<=229 then 11
                    when t.fqty>=230 and t.fqty<=299 then 12
                    when t.fqty>=300 and t.fqty<=329 then 14
                    when t.fqty>=330 then 15 else 9999 end as fcyqty
                from 
                (select d.FDATE,dd.FMATERIALID,sum(dd.FQTY) as fqty from T_SAL_ORDERENTRY dd inner join T_SAL_ORDER d on d.fid=dd.fid
                inner join T_BD_CUSTOMER_L cus on d.FCUSTID=cus.FCUSTID
                where cus.FNAME='山姆' and  d.FDATE=@fdate and d.FSALEORGID=593793
                group by d.FDATE,dd.FMATERIALID) t
                inner join t_bd_material_l ml on t.FMATERIALID=ml.FMATERIALID";
            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("fdate", dateTimePicker1.Value.Date));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                //添加序号
                //for (int j = 0; j < this.dataGridView1.Rows.Count; j++)
                //{ dataGridView1.Rows[j].HeaderCell.Value = j + 1; }
                dataGridView1.Rows[0].Selected=true;
                fdate = dataGridView1.Rows[0].Cells[0].Value.ToString();
                fname = dataGridView1.Rows[0].Cells[1].Value.ToString();
                fggxh = dataGridView1.Rows[0].Cells[2].Value.ToString();
                fqty = dataGridView1.Rows[0].Cells[3].Value.ToString();
                fcyqty = dataGridView1.Rows[0].Cells[4].Value.ToString();
                lbl_printinfo.Text = "";
            }
            else
            {
                dataGridView1.DataSource = null;
                lbl_printinfo.Text = "没有找到符合要求的数据，请确认金蝶中是否已录入订单";
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void SNCK_InspectionReport_Load(object sender, EventArgs e)
        {
            tb_jyy.Text = Properties.Settings.Default.SNCK_JYY;
            tb_shy.Text= Properties.Settings.Default.SNCK_SHY;
            dateTimePicker1.Value = DateTime.Now.Date.AddDays(1);
            parentDir = @".\report";
            refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= 0)
            {
                fdate = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                fname= dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                fggxh= dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                fqty = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                fcyqty = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            else if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
        }

        private void btn_RowPrint_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SNCK_JYY = tb_jyy.Text;
            Properties.Settings.Default.SNCK_SHY = tb_shy.Text;
            Properties.Settings.Default.Save();
            PrinterChoose printerChoose = new PrinterChoose();
            if (printerChoose.ShowDialog() == DialogResult.OK)
            {
                Report report = new Report();
                try
                {
                    report.Load(parentDir + @"\SNCKcpccjybgd.frx");
                }
                catch (FileNotFoundException ee)
                {
                    MessageBox.Show("未找到报表文件,请联系管理员");

                }
                report.PrintSettings.Printer = PrinterChoose.printerChoose.cb_printers.Text; // 指定打印机
                report.SetParameterValue("fdate", fdate);
                report.SetParameterValue("fname", fname);
                report.SetParameterValue("fggxh", fggxh);
                report.SetParameterValue("fqty", fqty);
                report.SetParameterValue("fcyqty", fcyqty);
                report.SetParameterValue("jyy", tb_jyy.Text.Trim());
                report.SetParameterValue("shy", tb_shy.Text.Trim());
                report.PrintSettings.ShowDialog = false; // 不弹出打印对话框
                report.Print(); // 直接打印
            }
            
        }

        private void btn_mutliPrint_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SNCK_JYY = tb_jyy.Text;
            Properties.Settings.Default.SNCK_SHY = tb_shy.Text;
            Properties.Settings.Default.Save();
            PrinterChoose printerChoose = new PrinterChoose();
            if (printerChoose.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    fdate = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    fname = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    fggxh = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    fqty = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    fcyqty = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    Report report = new Report();
                    try
                    {
                        report.Load(parentDir + @"\SNCKcpccjybgd.frx");
                    }
                    catch (FileNotFoundException ee)
                    {
                        MessageBox.Show("未找到报表文件,请联系管理员");

                    }
                    lbl_printinfo.Text = "共" + dataGridView1.Rows.Count + "张,正在打印第" + (i + 1) + "张";
                    report.PrintSettings.Printer = PrinterChoose.printerChoose.cb_printers.Text; // 指定打印机
                    report.SetParameterValue("fdate", fdate);
                    report.SetParameterValue("fname", fname);
                    report.SetParameterValue("fggxh", fggxh);
                    report.SetParameterValue("fqty", fqty);
                    report.SetParameterValue("fcyqty", fcyqty);
                    report.SetParameterValue("jyy", tb_jyy.Text.Trim());
                    report.SetParameterValue("shy", tb_shy.Text.Trim());
                    report.PrintSettings.ShowDialog = false; // 不弹出打印对话框
                    report.Print(); // 直接打印
                }
                lbl_printinfo.Text = "";
            }
        }
    }
}
