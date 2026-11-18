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
    public partial class HoursAdjust : Form
    {
        public static HoursAdjust hoursAdjust;
        private decimal sumhoursFrom, sumhoursTo;
        private string idFrom, idTo;
        public HoursAdjust()
        {
            InitializeComponent();
            hoursAdjust = this;
            InitDataGridViewColumns();
        }

        private void HoursAdjust_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM";
            lbl_from.Text = "工作时长超出常规工作时长的日期，点击选择";
            lbl_to.Text = "工作时长不足常规工作时长的日期，点击选择";
            
        }

        private void InitDataGridViewColumns()
        {
            // 禁止自动生成列
            dgv_from.AutoGenerateColumns = false;
            dgv_from.Columns.Clear();
            dgv_to.AutoGenerateColumns = false;
            dgv_to.Columns.Clear();

            // 列1
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "fdate";
            col1.HeaderText = "日期";
            col1.DataPropertyName = "fdate";
            col1.Width = 100;

            // 列2
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "begintime";
            col2.HeaderText = "上班时间";
            col2.DataPropertyName = "begintime";
            col2.Width = 100;

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "endtime";
            col3.HeaderText = "下班时间";
            col3.DataPropertyName = "endtime";
            col3.Width = 100;

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = "sumhours";
            col4.HeaderText = "总工时";
            col4.DataPropertyName = "sumhours";
            col4.Width = 100;

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "regularHours";
            col5.HeaderText = "每日常规工作时长";
            col5.DataPropertyName = "regularHours";
            col5.Width = 120;

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.Name = "remainder";
            col6.HeaderText = "可调整工时";
            col6.DataPropertyName = "remainder";
            col6.Width = 100;

            DataGridViewTextBoxColumn col13 = new DataGridViewTextBoxColumn();
            col13.Name = "id";
            col13.HeaderText = "id";
            col13.DataPropertyName = "id";
            col13.Width = 0;
            // 添加到控件
            dgv_from.Columns.AddRange(col1, col2, col3, col4, col5, col6, col13);
            dgv_from.AllowUserToAddRows = false;
            dgv_from.ReadOnly = true;
            dgv_from.RowHeadersWidth = 30;
            dgv_from.Columns["fdate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_from.Columns["begintime"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_from.Columns["endtime"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_from.Columns["sumhours"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_from.Columns["regularHours"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_from.Columns["remainder"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_from.Columns["id"].SortMode = DataGridViewColumnSortMode.NotSortable;


            DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
            col7.Name = "fdate";
            col7.HeaderText = "日期";
            col7.DataPropertyName = "fdate";
            col7.Width = 100;

            // 列2
            DataGridViewTextBoxColumn col8 = new DataGridViewTextBoxColumn();
            col8.Name = "begintime";
            col8.HeaderText = "上班时间";
            col8.DataPropertyName = "begintime";
            col8.Width = 100;

            DataGridViewTextBoxColumn col9 = new DataGridViewTextBoxColumn();
            col9.Name = "endtime";
            col9.HeaderText = "下班时间";
            col9.DataPropertyName = "endtime";
            col9.Width = 100;

            DataGridViewTextBoxColumn col10 = new DataGridViewTextBoxColumn();
            col10.Name = "sumhours";
            col10.HeaderText = "总工时";
            col10.DataPropertyName = "sumhours";
            col10.Width = 100;

            DataGridViewTextBoxColumn col11 = new DataGridViewTextBoxColumn();
            col11.Name = "regularHours";
            col11.HeaderText = "每日常规工作时长";
            col11.DataPropertyName = "regularHours";
            col11.Width = 120;

            DataGridViewTextBoxColumn col12 = new DataGridViewTextBoxColumn();
            col12.Name = "remainder";
            col12.HeaderText = "可调整工时";
            col12.DataPropertyName = "remainder";
            col12.Width = 100;

            DataGridViewTextBoxColumn col14 = new DataGridViewTextBoxColumn();
            col14.Name = "id";
            col14.HeaderText = "id";
            col14.DataPropertyName = "id";
            col14.Width = 0;

            dgv_to.Columns.AddRange(col7, col8, col9, col10, col11, col12, col14);
            dgv_to.AllowUserToAddRows = false;
            dgv_to.ReadOnly = true;
            dgv_to.RowHeadersWidth = 30;
            dgv_to.Columns["fdate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_to.Columns["begintime"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_to.Columns["endtime"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_to.Columns["sumhours"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_to.Columns["regularHours"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_to.Columns["remainder"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv_to.Columns["id"].SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private void dgv_from_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 & e.ColumnIndex > -1)
            {
                idFrom =dgv_from.Rows[e.RowIndex].Cells[6].Value.ToString();
                sumhoursFrom=decimal.Parse(dgv_from.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
            else
            {
                return;
            }
            //Console.WriteLine(idFrom);
        }

        private void dgv_to_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 & e.ColumnIndex > -1)
            {
                idTo = dgv_to.Rows[e.RowIndex].Cells[6].Value.ToString();
                sumhoursTo = decimal.Parse(dgv_to.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
            else
            {
                return;
            }
            //Console.WriteLine(idTo);
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            if (dgv_from.SelectedCells.Count == 0)
            {
                MsgBox.TipMsgShow(btn_change.Location.X + btn_change.Width + 20, btn_change.Location.Y + btn_change.Height - 10, this.Text, "请选择可调整的数据！", this);
            }
            else if (dgv_to.SelectedCells.Count == 0)
            {
                MsgBox.TipMsgShow(btn_change.Location.X + btn_change.Width + 20, btn_change.Location.Y + btn_change.Height - 10, this.Text, "请选择需要调整的数据！", this);
            }
            else
            {
                HoursAdjustDetail hoursAdjustDetail = new HoursAdjustDetail();
                if (hoursAdjustDetail.ShowDialog() == DialogResult.OK)
                {
                    string sql = @"update ad set ad.sumhours=@sumhours,ad.overtimes=@sumhours-cfg.regularHours from FDD_AttendanceDetail ad inner join 
                               FDD_Config cfg on ad.fdate=cfg.fdate where ad.id=@idFrom";
                    string sql2 = @"update ad set ad.sumhours=@sumhours,ad.days=iif(@sumhours>=cfg.regularHours,1,0),ad.overtimes=@sumhours-cfg.regularHours
                               from FDD_AttendanceDetail ad inner join
                               FDD_Config cfg on ad.fdate=cfg.fdate where ad.id=@idTo";
                    if (SQLHelper.ExecSql(sql,
                            SQLHelper.MakeParam("sumhours", sumhoursFrom - decimal.Parse(HoursAdjustDetail.hoursAdjustDetail.tb_adjustHours.Text)), SQLHelper.MakeParam("idFrom", idFrom)))
                    {
                        dgv_from.Rows[dgv_from.SelectedCells[0].RowIndex].Cells[3].Value = sumhoursFrom - decimal.Parse(HoursAdjustDetail.hoursAdjustDetail.tb_adjustHours.Text);
                        dgv_from.Rows[dgv_from.SelectedCells[0].RowIndex].Cells[5].Value = sumhoursFrom - decimal.Parse(HoursAdjustDetail.hoursAdjustDetail.tb_adjustHours.Text)-
                                                                                           decimal.Parse(dgv_from.Rows[dgv_from.SelectedCells[0].RowIndex].Cells[4].Value.ToString());
                    }
                    if (SQLHelper.ExecSql(sql2,
                            SQLHelper.MakeParam("sumhours", sumhoursTo + decimal.Parse(HoursAdjustDetail.hoursAdjustDetail.tb_adjustHours.Text)), SQLHelper.MakeParam("idTo", idTo)))
                    {
                        dgv_to.Rows[dgv_to.SelectedCells[0].RowIndex].Cells[3].Value = sumhoursTo +decimal.Parse(HoursAdjustDetail.hoursAdjustDetail.tb_adjustHours.Text);
                        dgv_to.Rows[dgv_to.SelectedCells[0].RowIndex].Cells[5].Value = sumhoursTo + decimal.Parse(HoursAdjustDetail.hoursAdjustDetail.tb_adjustHours.Text) -
                                                                                           decimal.Parse(dgv_to.Rows[dgv_to.SelectedCells[0].RowIndex].Cells[4].Value.ToString());
                    }

                    string logtxt = "调整" + lbl_empinfo.Text.Replace("选择的","") + "的总工时，从" + DateTime.Parse(dgv_from.Rows[dgv_from.SelectedCells[0].RowIndex].Cells[0].Value.ToString()).ToShortDateString()
                                   +"调整"+ HoursAdjustDetail.hoursAdjustDetail.tb_adjustHours.Text+"个工时到"+
                                   DateTime.Parse(dgv_to.Rows[dgv_to.SelectedCells[0].RowIndex].Cells[0].Value.ToString()).ToShortDateString();
                    string sql3 = @"insert into FDD_log values (@ftype,@operator,@pcname,@opertime,@cfgdate,@flog)";
                    SQLHelper.ExecSql(sql3,
                        SQLHelper.MakeParam("ftype","工时调整"),
                        SQLHelper.MakeParam("operator", MyVar.LoginName),
                        SQLHelper.MakeParam("pcname", MyVar.LoginComputer),
                        SQLHelper.MakeParam("opertime", DateTime.Now.ToString()),
                        SQLHelper.MakeParam("cfgdate", "9999-12-31"),
                        SQLHelper.MakeParam("flog", logtxt));
                }
            }
        }

       

        private void btn_empchoose_Click(object sender, EventArgs e)
        {
            EmployeeChoose employeeChoose = new EmployeeChoose();
            if (employeeChoose.ShowDialog() == DialogResult.OK)
            {
                lbl_empinfo.Text = EmployeeChoose.employeeChoose.empinfo;
                string sql = @"select ad.fdate,ad.begintime,ad.endtime,ad.sumhours,cfg.regularHours,ad.sumhours-cfg.regularHours as remainder,ad.id from FDD_AttendanceDetail ad
                        inner join FDD_Employee emp on ad.empid=emp.id
                        inner join FDD_config cfg on ad.fdate=cfg.fdate
                        where ad.empid=@empid  and ad.fmark='出勤' and ad.sumhours>cfg.regularHours
                        and year(ad.fdate)=@year and MONTH(ad.fdate)=@month";
                DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("empid", EmployeeChoose.employeeChoose.empid),
                SQLHelper.MakeParam("year", dateTimePicker1.Value.Year), SQLHelper.MakeParam("month", dateTimePicker1.Value.Month));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgv_from.DataSource = ds.Tables[0];
                    dgv_from.ClearSelection();
                    dgv_from.Columns[6].Visible = false;
                }
                else
                {
                    dgv_from.DataSource = null;
                    return;
                }

                sql = @"select ad.fdate,ad.begintime,ad.endtime,ad.sumhours,cfg.regularHours,ad.sumhours-cfg.regularHours as remainder,ad.id from FDD_AttendanceDetail ad
                        inner join FDD_Employee emp on ad.empid=emp.id
                        inner join FDD_config cfg on ad.fdate=cfg.fdate
                        where ad.empid=@empid  and ad.fmark='出勤' and ad.sumhours<cfg.regularHours
                        and year(ad.fdate)=@year and MONTH(ad.fdate)=@month";
                ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("empid", EmployeeChoose.employeeChoose.empid),
                SQLHelper.MakeParam("year", dateTimePicker1.Value.Year), SQLHelper.MakeParam("month", dateTimePicker1.Value.Month));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgv_to.DataSource = ds.Tables[0];
                    dgv_to.ClearSelection();
                    dgv_to.Columns[6].Visible = false;
                }
                else
                {
                    dgv_to.DataSource = null;
                    return;
                }
            }
        }
    }
}
