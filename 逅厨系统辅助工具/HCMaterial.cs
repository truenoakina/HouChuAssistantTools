using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class HCMaterial : Form
    {
        public static HCMaterial hcmaterial;
        public HCMaterial()
        {
            InitializeComponent();
            hcmaterial = this;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string sql = @"select * from hcgoods where kcode like '%" + textBox1.Text.Trim() + "%' or kname like '%"
                + textBox1.Text.Trim() +"%' or tiaoma like '%"+ textBox1.Text.Trim()+ "%' and 1=@data";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                dataGridView1.DataSource = null;
                MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "当前查询条件未查到符合的数据！", this);
                return;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            HCMaterial_add hcmaterial_add = new HCMaterial_add();
            hcmaterial_add.ShowDialog();
        }

        private void HCMaterial_Load(object sender, EventArgs e)
        {
            string sql = @"select * from hcgoods where @data=1";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                dataGridView1.DataSource = null;
                MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "当前查询条件未查到符合的数据！", this);
                return;
            }
            dataGridView1.ReadOnly = true;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 & e.ColumnIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    MyVar.kCode = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                }
                else if (e.RowIndex == -1 || e.ColumnIndex == -1)
                {
                    return;
                }

            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            HCMaterial_update hcmaterial_update = new HCMaterial_update();
            hcmaterial_update.ShowDialog();

        }

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                string sql = @"delete from hcgoods where kcode=@kcode";
                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("kcode", MyVar.kCode));
                MessageBox.Show("删除成功");
                button1_Click(this, new EventArgs());
            }
        }
    }
}
