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
    public partial class HoursAdjustDetail : Form
    {
        private string fromdate, todate;
        private decimal adjustHours;
        public static HoursAdjustDetail hoursAdjustDetail;
        public HoursAdjustDetail()
        {
            InitializeComponent();
            hoursAdjustDetail = this;
        }

        private void HoursAdjustDetail_Load(object sender, EventArgs e)
        {
            lbl_info.Text = EmployeeChoose.employeeChoose.empinfo;
            fromdate = DateTime.Parse(HoursAdjust.hoursAdjust.dgv_from.Rows[HoursAdjust.hoursAdjust.dgv_from.SelectedCells[0].RowIndex].Cells[0].Value.ToString()).ToShortDateString();
            todate = DateTime.Parse(HoursAdjust.hoursAdjust.dgv_to.Rows[HoursAdjust.hoursAdjust.dgv_to.SelectedCells[0].RowIndex].Cells[0].Value.ToString()).ToShortDateString();
            lbl_info2.Text = "从 "+ fromdate +"(总工时"+ HoursAdjust.hoursAdjust.dgv_from.Rows[HoursAdjust.hoursAdjust.dgv_from.SelectedCells[0].RowIndex].Cells[3].Value.ToString()+ ") \n调整到\n  " + todate 
                +"(总工时" + HoursAdjust.hoursAdjust.dgv_to.Rows[HoursAdjust.hoursAdjust.dgv_to.SelectedCells[0].RowIndex].Cells[3].Value.ToString() + ") "+
                "\n可调整工时：" + HoursAdjust.hoursAdjust.dgv_from.Rows[HoursAdjust.hoursAdjust.dgv_from.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
            tb_adjustHours.Text = "0";
            adjustHours = decimal.Parse(HoursAdjust.hoursAdjust.dgv_from.Rows[HoursAdjust.hoursAdjust.dgv_from.SelectedCells[0].RowIndex].Cells[5].Value.ToString());
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(tb_adjustHours.Text) > adjustHours)
            {
                MsgBox.TipMsgShow(tb_adjustHours.Location.X + tb_adjustHours.Width, tb_adjustHours.Location.Y + tb_adjustHours.Height / 2, this.Text, "不允许超过可调整工时！", this);
            }
            else if (tb_adjustHours.Text.Trim() == "0" || tb_adjustHours.Text.Trim() == "")
            {
                MsgBox.TipMsgShow(tb_adjustHours.Location.X + tb_adjustHours.Width, tb_adjustHours.Location.Y + tb_adjustHours.Height / 2, this.Text, "请填写需要调整的工时！", this);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void tb_adjustHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
