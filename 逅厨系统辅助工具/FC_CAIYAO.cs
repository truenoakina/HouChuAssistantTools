using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class FC_CAIYAO : Form
    {
        private DataTable dt;
        private SqlDataAdapter sda;
        private SqlCommandBuilder scb;
        private ComboBox cb_leibie = new ComboBox();
        private SqlConnection conn;
        private int i;

        public FC_CAIYAO()
        {
            InitializeComponent();
        }


        private void FC_CAIYAO_Load(object sender, EventArgs e)
        {
            conn = SQLHelper.GetConn();
            refresh();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            

        }

        private void refresh()
        {
            sda = new SqlDataAdapter("select id,FNAME as 菜肴名称,FWEIGHT as 菜品克重,FIS_INORDER AS 是否参与订货 from FC_CAIYAO order by id", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            scb = new SqlCommandBuilder(sda);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            FC_CAIYAO_ADD fC_CAIYAO_ADD = new FC_CAIYAO_ADD();
            if (fC_CAIYAO_ADD.ShowDialog() == DialogResult.OK)
            {
                String name = FC_CAIYAO_ADD.fC_CAIYAO_ADD.tb_name.Text.Trim();
                String weight = FC_CAIYAO_ADD.fC_CAIYAO_ADD.tb_weight.Text.Trim();
                String isinorder = FC_CAIYAO_ADD.fC_CAIYAO_ADD.cb_isinorder.Text.Trim();
                

                string sql = @"insert into FC_CAIYAO values (@FNAME,@FWEIGHT,@FIS_INORDER)";
                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("FNAME", name), SQLHelper.MakeParam("FWEIGHT", weight), SQLHelper.MakeParam("FIS_INORDER", isinorder));
                MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "添加成功！", this);
                refresh();
               

            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                dataGridView1.Rows.RemoveAt(i);
                sda.Update(dt);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                //MessageBox.Show(e.RowIndex.ToString() + e.ColumnIndex.ToString());
                if (e.RowIndex > -1 & e.ColumnIndex == -1)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                    //dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    i = e.RowIndex;
                }
                else
                {
                    return;
                }

            }
        }
    }
}
