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
    public partial class HolidayAdd : Form
    {

        private Dictionary<TextBox, string> _textBoxInitialValues = new Dictionary<TextBox, string>();
        public HolidayAdd()
        {
            InitializeComponent();
        }

        private void GetAllTextBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                {
                    _textBoxInitialValues[textBox] = textBox.Text;
                }
                else if (control.HasChildren)
                {
                    GetAllTextBoxes(control); // 递归找子控件里的TextBox
                }
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            //判断是否有空值
            foreach (var item in _textBoxInitialValues)
            {
                TextBox textBox = item.Key;
                if (textBox.Text.Trim().Equals(""))
                {
                    MsgBox.TipMsgShow(textBox.Location.X + textBox.Width + 20, textBox.Location.Y + textBox.Height - 10, this.Text, "不能为空！", this);
                    return;
                }

            }

            string sql = @"select * from fdd_Holiday where fdate=@fdate";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
            if (ds.Tables[0].Rows.Count > 0)
            {
                MsgBox.TipMsgShow(btn_ok.Location.X + btn_ok.Width + 20, btn_ok.Location.Y + btn_ok.Height - 10, this.Text, "日期重复，请删除已有日期后再新增", this);
                return;
            }
            else
            { 
                sql = @"insert into fdd_Holiday (fdate,ZBCQRS,ZBWCQRS,Salary_3,EmpNum_3,Salary_1,EmpNum_1) 
                      values (@fdate,@ZBCQRS,@ZBWCQRS,@Salary_3,@EmpNum_3,@Salary_1,@EmpNum_1)";
                if (SQLHelper.ExecSql(sql,
                                     SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date),
                                     SQLHelper.MakeParam("ZBCQRS", tb_ZBCQRS.Text.Trim()),
                                     SQLHelper.MakeParam("ZBWCQRS", tb_ZBWCQRS.Text.Trim()),
                                     SQLHelper.MakeParam("Salary_3", tb_Salary_3.Text.Trim()),
                                     SQLHelper.MakeParam("EmpNum_3", tb_EmpNum_3.Text.Trim()),
                                     SQLHelper.MakeParam("Salary_1", tb_Salary_1.Text.Trim()),
                                     SQLHelper.MakeParam("EmpNum_1", tb_EmpNum_1.Text.Trim())))
                {
                    MsgBox.TipMsgShow(btn_ok.Location.X + btn_ok.Width + 20, btn_ok.Location.Y + btn_ok.Height - 10, this.Text, "添加成功！", this);
                    Properties.Settings.Default.FDD_Salary_3 = decimal.Parse(tb_Salary_3.Text.Trim().ToString());
                    Properties.Settings.Default.FDD_EmpNum_3 = decimal.Parse(tb_EmpNum_3.Text.Trim().ToString());
                    Properties.Settings.Default.FDD_Salary_1 = decimal.Parse(tb_Salary_1.Text.Trim().ToString());
                    Properties.Settings.Default.FDD_EmpNum_1 = decimal.Parse(tb_EmpNum_1.Text.Trim().ToString());
                }
                else
                {
                    MsgBox.TipMsgShow(btn_ok.Location.X + btn_ok.Width + 20, btn_ok.Location.Y + btn_ok.Height - 10, this.Text, "添加失败，请联系管理员！", this);
                }
                HolidaySet.holidaySet.refresh();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_ZBCQRS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = true;
        }

        private void tb_ZBWCQRS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = true;
        }

        private void tb_Salary_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = true;
        }

        private void tb_Salary_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = true;
        }

        private void tb_EmpNum_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_EmpNum_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void HolidayAdd_Load(object sender, EventArgs e)
        {
            GetAllTextBoxes(this);
            tb_Salary_3.Text= Properties.Settings.Default.FDD_Salary_3.ToString();
            tb_EmpNum_3.Text = Properties.Settings.Default.FDD_EmpNum_3.ToString();
            tb_Salary_1.Text = Properties.Settings.Default.FDD_Salary_1.ToString();
            tb_EmpNum_1.Text = Properties.Settings.Default.FDD_EmpNum_1.ToString();
        }
    }
}
