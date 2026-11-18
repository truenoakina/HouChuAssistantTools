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
    public partial class EmployeeDetail : Form
    {
        public static EmployeeDetail employeeDetail;
        private string[] jzjdep;
        private string[] gztdep;
        
        public EmployeeDetail()
        {
            InitializeComponent();
            employeeDetail = this;
        }

        private void EmployeeDetail_Load(object sender, EventArgs e)
        {
            //把佳之家和谷之田的部门提前写入到数组中，方便取数
            string sql = @"select depname from FDD_depinfo where factory=@factory";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", "佳之家"));
            if (ds.Tables[0].Rows.Count > 0)
            {
                jzjdep=ds.Tables[0].AsEnumerable().Select(r => r["depname"].ToString()).ToArray(); ;
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

            this.ControlBox = false;
            this.Left = Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2;//桌面的宽度的一半减去自身宽的的一半
            this.Top = Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2;
            if (MyVar.Showmode == "add")
            {
                this.Text = "新增员工";
                cb_factory.SelectedIndex = EmployeeInfo.employeeInfo.cb_factory.SelectedIndex;
                cb_dep.SelectedIndex = EmployeeInfo.employeeInfo.cb_dep.SelectedIndex;
                cb_type.SelectedIndex = EmployeeInfo.employeeInfo.cb_type.SelectedIndex;
                cb_onjob.SelectedIndex = 0;
            }
            else if (MyVar.Showmode == "update")
            {
                this.Text = "员工信息修改";
                tb_empname.Text= EmployeeInfo.employeeInfo.dataGridView1.Rows[EmployeeInfo.employeeInfo.dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                cb_factory.Text = EmployeeInfo.employeeInfo.dataGridView1.Rows[EmployeeInfo.employeeInfo.dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                cb_dep.Text = EmployeeInfo.employeeInfo.dataGridView1.Rows[EmployeeInfo.employeeInfo.dataGridView1.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
                cb_type.Text= EmployeeInfo.employeeInfo.dataGridView1.Rows[EmployeeInfo.employeeInfo.dataGridView1.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
                cb_onjob.SelectedIndex = EmployeeInfo.employeeInfo.dataGridView1.Rows[EmployeeInfo.employeeInfo.dataGridView1.SelectedCells[0].RowIndex].Cells[6].Value.ToString() == "在职" ? 0 : 1;
            }


        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (tb_empname.Text.Trim() == String.Empty)
            {
                MsgBox.TipMsgShow(tb_empname.Location.X + tb_empname.Width / 2, tb_empname.Location.Y + tb_empname.Height / 2, this.Text, "姓名不能为空！", this);
                return;
            }
            else if (cb_factory.Text.Trim() == String.Empty)
            {
                MsgBox.TipMsgShow(cb_factory.Location.X + cb_factory.Width / 2, cb_factory.Location.Y + cb_factory.Height / 2, this.Text, "请选择工厂！", this);
                return;
            }
            else if (cb_dep.Text.Trim() == String.Empty)
            {
                MsgBox.TipMsgShow(cb_dep.Location.X + cb_dep.Width / 2, cb_dep.Location.Y + cb_dep.Height / 2, this.Text, "请选择部门！", this);
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
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
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
