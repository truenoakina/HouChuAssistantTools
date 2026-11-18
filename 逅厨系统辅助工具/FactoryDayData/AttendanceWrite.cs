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
    public partial class AttendanceWrite : Form
    {
        private string[] jzjdep;
        private string[] gztdep;
        private int selectRowNum;//输入时选中的行号
        private int empid;//输入时的员工id
        private string empnName;//输入时的员工姓名
        private decimal begintime, endtime;
        /// <summary>
        /// 考勤相关参数，读取参数设置
        /// </summary>
        private Decimal dayHours, regularHours, diningTime,overtimes,sumhours;   //出勤日时长，常规工作时长，用餐时长
        private int days;//出勤天数
        //private bool isWuCanKJ, isWanCanKJ;//是否扣减午餐用时，是否扣减晚餐用时
        public static AttendanceWrite attendanceWrite;
        private DateTime originalDate;//记录datetimepicker选择前的数据

        public AttendanceWrite()
        {
            InitializeComponent();
            attendanceWrite = this;
            // 关键：窗体先捕获键盘
            this.KeyPreview = true;
            // 可选：禁止DGV按Tab跳到单元格编辑，只整行选中
            dataGridView1.StandardTab = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            //dayHours = Properties.Settings.Default.FDD_dayHours;
            //regularHours= Properties.Settings.Default.FDD_regularHours;
            //diningTime= Properties.Settings.Default.FDD_diningTime;
            //isWuCanKJ = Properties.Settings.Default.FDD_isWuCanKJ;
            //isWanCanKJ= Properties.Settings.Default.FDD_isWanCanKJ;
        }

        //屏蔽系统tab键功能，按下tab键是在datagridview中上下依次轮转
        // 全局按键拦截
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 拦截 Tab 键
            if (keyData == Keys.Tab)
            {
                MoveDgvRowDownLoop();
                // 返回true：屏蔽系统原有Tab功能（不跳转焦点）
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// DGV 按Tab向下选行，最后一行回到第一行循环
        /// </summary>
        private void MoveDgvRowDownLoop()
        {
            if (dataGridView1.RowCount == 0) return;

            int currentRowIndex = dataGridView1.CurrentRow?.Index ?? 0;
            int nextRowIndex;

            // 判断是否是最后一行
            if (currentRowIndex >= dataGridView1.RowCount - 1)
            {
                // 最后一行 → 跳到第一行
                nextRowIndex = 0;
            }
            else
            {
                // 正常下一行
                nextRowIndex = currentRowIndex + 1;
            }

            // 选中新行 + 滚动到可视区域
            dataGridView1.ClearSelection();
            dataGridView1.Rows[nextRowIndex].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[nextRowIndex].Cells[1];
            dataGridView1.FirstDisplayedScrollingRowIndex = nextRowIndex;
            lbl_name.Text = dataGridView1.Rows[nextRowIndex].Cells[1].Value.ToString();
            cb_mark.Text = dataGridView1.Rows[nextRowIndex].Cells[3].Value.ToString().Equals("") ? "出勤" : dataGridView1.Rows[nextRowIndex].Cells[3].Value.ToString();
            tb_begintime.Text = dataGridView1.Rows[nextRowIndex].Cells[4].Value.ToString();
            tb_endtime.Text = dataGridView1.Rows[nextRowIndex].Cells[5].Value.ToString();
            cb_kjWucan.Checked = dataGridView1.Rows[nextRowIndex].Cells[6].Value.ToString() == "" ?
                      MyVar.IsWuCanKJ : Convert.ToBoolean(dataGridView1.Rows[nextRowIndex].Cells[6].Value);
            cb_kjWanCan.Checked = dataGridView1.Rows[nextRowIndex].Cells[7].Value.ToString() == "" ?
                      MyVar.IsWanCanKJ : Convert.ToBoolean(dataGridView1.Rows[nextRowIndex].Cells[7].Value);
            lbl_days.Text = dataGridView1.Rows[nextRowIndex].Cells[8].Value.ToString();
            lbl_overtimes.Text = dataGridView1.Rows[nextRowIndex].Cells[9].Value.ToString();
            lbl_sumhours.Text = dataGridView1.Rows[nextRowIndex].Cells[10].Value.ToString();
            if (!cb_mark.Text.Equals("出勤"))
            {
                tb_begintime.Enabled = false;
                tb_endtime.Enabled = false;
            }
            else 
            {
                tb_begintime.Enabled = true;
                tb_endtime.Enabled = true;
                tb_begintime.Focus();
                tb_begintime.SelectAll();
            }
            
            //clearinfo();

            //MessageBox.Show(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value.ToString());
        }
        private void AttendanceWrite_Load(object sender, EventArgs e)
        {
            lbl_beginArrow.Visible = false;
            lbl_endArrow.Visible = false;
            //把佳之家和谷之田的部门提前写入到数组中，方便取数
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
            cb_kjWucan.Checked = MyVar.IsWuCanKJ ? true : false;
            cb_kjWanCan.Checked = MyVar.IsWanCanKJ ? true : false;
            cb_type.SelectedIndex = 0;
            lbl_result.Text = "";
            cb_mark.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now.AddDays(-1).Date;
            originalDate = dateTimePicker1.Value.Date;//取当前日期框日期，如果用户重新选择时不配置参数，回到这个值
            refresh();
            showConfig();
        }

        private void showConfig()
        {
            lbl_config.Text = "当前参数:\n出勤日时长:" + MyVar.DayHours + "\n常规工作时长:" + MyVar.RegularHours
                              + "\n用餐时长" + MyVar.DiningTime + "\n正式工工时成本:"+MyVar.RegEmpPrice
                              + "\n临时工工时成本:" + MyVar.TempEmpPrice;
        }

        void refresh()
        {
            string sql = @"select emp.id,emp.empname,emp.sortnum,ad.fmark,ad.begintime,ad.endtime,
                 ad.isKJWuCan,ad.isKJWanCan,
                 ad.days,ad.overtimes,ad.sumhours
                from FDD_Employee emp 
                left join (select * from FDD_AttendanceDetail where fdate=@fdate) ad on ad.empid=emp.id 
                where emp.factory=@factory and emp.depname=@depname and emp.ftype=@ftype and emp.onjob=1 order by emp.sortnum asc";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", cb_factory.Text),
                SQLHelper.MakeParam("depname", cb_dep.Text), SQLHelper.MakeParam("ftype", cb_type.Text),
                SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                btn_save.Enabled = true;
            }
            else
            {
                dataGridView1.DataSource = null;
                lbl_name.Text = "";
                btn_save.Enabled = false;
                clearinfo();
                return;
            }

            dataGridView1.Columns[2].Visible = false;  ///隐藏sortnum
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 30;

            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[1].HeaderText = "姓名";
            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[3].HeaderText = "说明";
            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[4].HeaderText = "上班时间";
            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[5].HeaderText = "下班时间";
            dataGridView1.Columns[6].Width = 50;
            dataGridView1.Columns[6].HeaderText = "扣减午餐";
            dataGridView1.Columns[7].Width = 50;
            dataGridView1.Columns[7].HeaderText = "扣减晚餐";
            dataGridView1.Columns[8].Width = 50;
            dataGridView1.Columns[8].HeaderText = "出勤天数";
            dataGridView1.Columns[9].Width = 50;
            dataGridView1.Columns[9].HeaderText = "加班时长";
            dataGridView1.Columns[10].Width = 50;
            dataGridView1.Columns[10].HeaderText = "总时长";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            if (dataGridView1.Rows.Count > 0)
            {
                //lbl_result.Text = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[6].Value.ToString();
                dataGridView1.Rows[0].Selected = true;
                lbl_name.Text = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                cb_mark.Text = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                tb_begintime.Text = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
                tb_endtime.Text = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
                cb_kjWucan.Checked = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[6].Value.ToString() == "" ?
                      MyVar.IsWuCanKJ : Convert.ToBoolean(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[6].Value);
                cb_kjWanCan.Checked = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[7].Value.ToString() == "" ?
                      MyVar.IsWanCanKJ : Convert.ToBoolean(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[7].Value);
                lbl_days.Text = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[8].Value.ToString();
                lbl_overtimes.Text = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[9].Value.ToString();
                lbl_sumhours.Text = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[10].Value.ToString();
                tb_begintime.Focus();
                tb_begintime.SelectAll();
            }
            else
            {
                
                clearinfo();
                return;
            }
            //取消点击列排序
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            
        }

        private void cb_factory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_factory.Text == "佳之家")
            {
                cb_dep.DataSource = jzjdep;
            }
            else if (cb_factory.Text == "谷之田")
            {
                cb_dep.DataSource = gztdep;
            }
            refresh();
        }

        private void cb_dep_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
            string sql = @"select * from FDD_Config where fdate=@fdate";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
            if (ds.Tables[0].Rows.Count > 0)
            {
                MyVar.DayHours = decimal.Parse(ds.Tables[0].Rows[0][2].ToString());
                MyVar.RegularHours = decimal.Parse(ds.Tables[0].Rows[0][3].ToString());
                MyVar.DiningTime = decimal.Parse(ds.Tables[0].Rows[0][4].ToString());
                MyVar.IsWuCanKJ = ds.Tables[0].Rows[0][5].ToString() == "True" ? true:false; 
                MyVar.IsWanCanKJ = ds.Tables[0].Rows[0][6].ToString() == "True" ? true : false;
                MyVar.RegEmpPrice = int.Parse(ds.Tables[0].Rows[0][7].ToString());
                MyVar.TempEmpPrice = int.Parse(ds.Tables[0].Rows[0][8].ToString());
                //MyVar.ManualFixValue = int.Parse(ds.Tables[0].Rows[0][9].ToString());
                refresh();
                showConfig();
                originalDate = dateTimePicker1.Value.Date;
            }
            else
            {
                DialogResult dr = MessageBox.Show("该日期未设置参数，请设置", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    MyVar.FDDconfigDate = dateTimePicker1.Value.Date;
                    AttendanceConfig attendanceConfig = new AttendanceConfig();
                    attendanceConfig.ShowDialog();
                    originalDate = dateTimePicker1.Value.Date;
                    refresh();
                    showConfig();
                }
                else 
                {
                    dateTimePicker1.Value = originalDate;
                    return;
                }
                
            }

            
        }


        //清空填写内容
        private void clearinfo()
        {
            lbl_days.Text = "";
            lbl_overtimes.Text = "";
            lbl_sumhours.Text = "";
            tb_begintime.Text = "";
            tb_endtime.Text = "";
            lbl_result.Text = "";
        }
        
        /// <summary>
        /// 计算相关时长
        /// </summary>
        private void calcTime()
        {
            if (cb_mark.SelectedIndex == 0)
            {
                begintime = tb_begintime.Text.Trim().Equals("") ? 0 : decimal.Parse(tb_begintime.Text.Trim());
                endtime = tb_endtime.Text.Trim().Equals("") ? 0 : decimal.Parse(tb_endtime.Text.Trim());
                //计算考勤时间并显示
                days = (endtime - begintime) > MyVar.DayHours ? 1 : 0;
                lbl_days.Text = days.ToString();
                overtimes = (endtime - begintime - (cb_kjWucan.Checked ? MyVar.DiningTime : 0) - (cb_kjWanCan.Checked ? MyVar.DiningTime : 0) - MyVar.RegularHours) > 0 ?
                         endtime - begintime - (cb_kjWucan.Checked ? MyVar.DiningTime : 0) - (cb_kjWanCan.Checked ? MyVar.DiningTime : 0) - MyVar.RegularHours : 0;
                lbl_overtimes.Text = overtimes.ToString();
                sumhours = endtime - begintime - (cb_kjWucan.Checked ? MyVar.DiningTime : 0) - (cb_kjWanCan.Checked ? MyVar.DiningTime : 0);
                lbl_sumhours.Text = sumhours.ToString();
            }
            else
            {
                begintime = 0.00m;
                endtime = 0.00m;
                //计算考勤时间并显示
                days =  0;
                lbl_days.Text = "0";
                overtimes = 0;
                lbl_overtimes.Text = "0";
                sumhours = 0;
                lbl_sumhours.Text = "0";
            }

            
        }
        /// <summary>
        /// 上班时间录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_begintime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!tb_begintime.Text.Trim().Equals(""))
                {
                    calcTime();
                    tb_endtime.Focus();
                    tb_endtime.SelectAll();
                }
                else
                {
                    MsgBox.TipMsgShow(tb_begintime.Location.X + tb_begintime.Width + 20, tb_begintime.Location.Y + tb_begintime.Height - 10, this.Text, "请填写上班时间！", this);
                }
            }
        }

        private void tb_begintime_Leave(object sender, EventArgs e)
        {
            lbl_beginArrow.Visible = false;
            calcTime();
        }

        private void tb_endtime_Leave(object sender, EventArgs e)
        {
            lbl_endArrow.Visible = false;
            calcTime();
        }

        private void cb_mark_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_mark.SelectedIndex == 0)
            {
                tb_begintime.Enabled = true;
                tb_endtime.Enabled = true;
                cb_kjWanCan.Enabled = true;
                cb_kjWucan.Enabled = true;
                clearinfo();
                tb_begintime.Focus();
                tb_begintime.SelectAll();
            }
            else 
            {
                tb_begintime.Enabled = false;
                tb_endtime.Enabled = false;
                cb_kjWanCan.Enabled = false;
                cb_kjWucan.Enabled = false;
                clearinfo();
            }

        }

        private void tb_begintime_Enter(object sender, EventArgs e)
        {
            lbl_beginArrow.Visible = true;
        }

        private void tb_endtime_Enter(object sender, EventArgs e)
        {
            lbl_endArrow.Visible = true;
        }

        private void cb_kjWucan_CheckedChanged(object sender, EventArgs e)
        {
            calcTime();
        }

        private void cb_kjWanCan_CheckedChanged(object sender, EventArgs e)
        {
            calcTime();
        }

       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= 0)
            {
                lbl_name.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                cb_mark.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Equals("")?"出勤": dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                tb_begintime.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                tb_endtime.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                cb_kjWucan.Checked = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()==""?
                     MyVar.IsWuCanKJ:Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                cb_kjWanCan.Checked = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() == "" ?
                     MyVar.IsWanCanKJ:Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                lbl_days.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                lbl_overtimes.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                lbl_sumhours.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                tb_begintime.Focus();
            }
            else if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
        }

        


        /// <summary>
        /// 下班时间录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_endtime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!tb_endtime.Text.Trim().Equals(""))
                {
                    calcTime();
                    btn_save.Focus();
                }
                else
                {
                    MsgBox.TipMsgShow(tb_endtime.Location.X + tb_endtime.Width + 20, tb_endtime.Location.Y + tb_endtime.Height - 10, this.Text, "请填写下班时间！", this);
                }
            }
        }
        private void tb_begintime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (cb_mark.Text=="出勤"&&tb_begintime.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_begintime.Location.X + tb_begintime.Width + 20, tb_begintime.Location.Y + tb_begintime.Height - 10, this.Text, "请填写上班时间！", this);
                return;
            }
            else if (cb_mark.Text == "出勤" && tb_endtime.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_endtime.Location.X + tb_endtime.Width + 20, tb_endtime.Location.Y + tb_endtime.Height - 10, this.Text, "请填写下班时间！", this);
                return;
            }
            else if (cb_mark.Text == "出勤" && decimal.Parse(tb_endtime.Text.Trim().ToString()) <= decimal.Parse(tb_begintime.Text.Trim().ToString()))
            {
                MsgBox.TipMsgShow(tb_endtime.Location.X + tb_endtime.Width + 20, tb_endtime.Location.Y + tb_endtime.Height - 10, this.Text, "下班时间必须大于上班时间！", this);
                return;
            }
            else if (dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value.ToString().Equals(""))
            {
                calcTime();
                string sql = @"insert into FDD_AttendanceDetail values (@empid,@empcode,@factory,@depname,@ftype,@fdate,@fmark,@begintime,@endtime,
                               @isKJWuCan,@isKJWanCan,@days,@overtimes,@sumhours)";
                if (SQLHelper.ExecSql(sql, SQLHelper.MakeParam("empid", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()),
                            SQLHelper.MakeParam("empcode", ""),
                            SQLHelper.MakeParam("empname", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value.ToString()),
                            SQLHelper.MakeParam("factory", cb_factory.Text),
                            SQLHelper.MakeParam("depname", cb_dep.Text),
                            SQLHelper.MakeParam("ftype", cb_type.Text),
                            SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date),
                            SQLHelper.MakeParam("fmark", cb_mark.Text),
                            SQLHelper.MakeParam("begintime", cb_mark.Text.Equals("出勤")? decimal.Parse(tb_begintime.Text.Trim()) : 0.00m),
                            SQLHelper.MakeParam("endtime", cb_mark.Text.Equals("出勤") ? decimal.Parse(tb_endtime.Text.Trim()) : 0.00m),
                            SQLHelper.MakeParam("isKJWuCan", cb_kjWucan.Checked?1:0),
                            SQLHelper.MakeParam("isKJWanCan", cb_kjWanCan.Checked ? 1 : 0),
                            SQLHelper.MakeParam("days", cb_mark.Text.Equals("出勤") ? days : 0),
                            SQLHelper.MakeParam("overtimes", cb_mark.Text.Equals("出勤") ? overtimes : 0),
                            SQLHelper.MakeParam("sumhours", cb_mark.Text.Equals("出勤") ? sumhours : 0)))
                            
                {
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value = cb_mark.Text;
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[4].Value = cb_mark.Text.Equals("出勤") ? tb_begintime.Text:"0.00";
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[5].Value = cb_mark.Text.Equals("出勤") ? tb_endtime.Text:"0.00";
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[6].Value = cb_kjWucan.Checked;
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[7].Value = cb_kjWanCan.Checked;
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[8].Value = cb_mark.Text.Equals("出勤") ? lbl_days.Text:"0";
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[9].Value = cb_mark.Text.Equals("出勤") ? lbl_overtimes.Text: "0.00";
                    dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[10].Value = cb_mark.Text.Equals("出勤") ? lbl_sumhours.Text: "0.00";
                    MsgBox.TipMsgShow(btn_save.Location.X + btn_save.Width + 20, btn_save.Location.Y + btn_save.Height - 10, this.Text, "保存成功！", this);
                }
                else
                {
                    MsgBox.TipMsgShow(btn_save.Location.X + btn_save.Width + 20, btn_save.Location.Y + btn_save.Height - 10, this.Text, "保存失败！", this);
                }
                //refresh();
            }
            //如果不为空，则更新
            else if (!dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value.ToString().Equals(""))
            {
                calcTime();
                DialogResult dr = MessageBox.Show("该员工当日已有考勤数据，是否修改？", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    string sql = @"update FDD_AttendanceDetail set fmark=@fmark,begintime=@begintime,endtime=@endtime,
                                 isKJWuCan=@isKJWuCan,isKJWanCan=@isKJWanCan,days=@days, overtimes=@overtimes, sumhours=@sumhours
                                 where empid=@empid and fdate=@fdate";
                    if (SQLHelper.ExecSql(sql,
                            SQLHelper.MakeParam("fmark", cb_mark.Text),
                            SQLHelper.MakeParam("begintime", cb_mark.Text.Equals("出勤") ? decimal.Parse(tb_begintime.Text.Trim()) : 0.00m),
                            SQLHelper.MakeParam("endtime", cb_mark.Text.Equals("出勤") ? decimal.Parse(tb_endtime.Text.Trim()) : 0.00m),
                            SQLHelper.MakeParam("isKJWuCan", cb_kjWucan.Checked ? 1 : 0),
                            SQLHelper.MakeParam("isKJWanCan", cb_kjWanCan.Checked ? 1 : 0),
                            SQLHelper.MakeParam("days", cb_mark.Text.Equals("出勤") ? days : 0),
                            SQLHelper.MakeParam("overtimes", cb_mark.Text.Equals("出勤") ? overtimes : 0),
                            SQLHelper.MakeParam("sumhours", cb_mark.Text.Equals("出勤") ? sumhours : 0),
                            SQLHelper.MakeParam("empid", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()),
                            SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date))
                        )
                    {
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value = cb_mark.Text;
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[4].Value = cb_mark.Text.Equals("出勤") ? tb_begintime.Text : "0.00";
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[5].Value = cb_mark.Text.Equals("出勤") ? tb_endtime.Text : "0.00";
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[6].Value = cb_kjWucan.Checked;
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[7].Value = cb_kjWanCan.Checked;
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[8].Value = cb_mark.Text.Equals("出勤") ? lbl_days.Text : "0";
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[9].Value = cb_mark.Text.Equals("出勤") ? lbl_overtimes.Text : "0.00";
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[10].Value = cb_mark.Text.Equals("出勤") ? lbl_sumhours.Text : "0.00";
                        MsgBox.TipMsgShow(btn_save.Location.X + btn_save.Width + 20, btn_save.Location.Y + btn_save.Height - 10, this.Text, "更新成功！", this);
                    }
                    else
                    {
                        MsgBox.TipMsgShow(btn_save.Location.X + btn_save.Width + 20, btn_save.Location.Y + btn_save.Height - 10, this.Text, "更新失败！", this);
                    }
                }
            }
        }

        private void tb_endtime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }
    }
}
