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
    public partial class FC_CAIYAO_ADD : Form
    {
        public static FC_CAIYAO_ADD fC_CAIYAO_ADD;

        public FC_CAIYAO_ADD()
        {
            InitializeComponent();
            fC_CAIYAO_ADD = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tmp;
            if (tb_name.Text.Trim() == String.Empty || tb_weight.Text.Trim() == String.Empty)
            {
                MsgBox.TipMsgShow(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2, this.Text, "名称和克重不能为空！", this);
                return;
            }
            else
            {
                if (!int.TryParse(tb_weight.Text, out tmp))
                {
                    MsgBox.TipMsgShow(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2, this.Text, "克重请输入数字!", this);
                    return;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void FC_CAIYAO_ADD_Load(object sender, EventArgs e)
        {
            cb_isinorder.SelectedIndex = cb_isinorder.Items.IndexOf("是");
        }
    }
}
