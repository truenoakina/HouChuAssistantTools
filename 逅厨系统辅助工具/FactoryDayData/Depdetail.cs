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
    public partial class Depdetail : Form
    {
        public static Depdetail depdetail;
        public Depdetail()
        {
            InitializeComponent();
            depdetail = this;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (tb_depname.Text.Trim() == String.Empty)
            {
                MsgBox.TipMsgShow(tb_depname.Location.X + tb_depname.Width / 2, tb_depname.Location.Y + tb_depname.Height / 2, this.Text, "部室不能为空！", this);
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Depdetail_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Left = Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2;//桌面的宽度的一半减去自身宽的的一半
            this.Top = Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2;
            if (MyVar.Showmode == "add")
            {
                this.Text = "部室新增";
                cb_factory.SelectedIndex = 0;
                tb_depname.Text = "";
            }
            else if (MyVar.Showmode == "update")
            {
                this.Text = "部室修改";
                cb_factory.Text = DepInfo.depInfo.dataGridView1.Rows[DepInfo.depInfo.dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                tb_depname.Text = DepInfo.depInfo.dataGridView1.Rows[DepInfo.depInfo.dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
            }
        }
    }
}
