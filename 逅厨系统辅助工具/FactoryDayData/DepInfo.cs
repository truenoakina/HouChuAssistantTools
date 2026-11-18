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
    public partial class DepInfo : Form
    {
        public static DepInfo depInfo;
        // 全局数据源：所有编辑都操作这个 DataTable
        //private SqlDataAdapter sda;
        //private DataTable dt = new DataTable();
        //private SqlCommandBuilder scb;
        //List<string> factory = new List<string> { "佳之家", "谷之田" };
        //private ComboBox cb_factory = new ComboBox();
        //private SqlConnection conn;
        //private int id=0;

        public DepInfo()
        {
            InitializeComponent();
            depInfo = this;
        }

        private void DepInfo_Load(object sender, EventArgs e)
        {
            refresh();
            dataGridView1.Columns[0].Visible = false;  ///隐藏ID
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 30;

            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[1].HeaderText = "工厂";
            dataGridView1.Columns[2].Width = 220;
            dataGridView1.Columns[2].HeaderText = "部室";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //cb_factory.DataSource = factory;
            //cb_factory.Visible = false;
            //cb_factory.SelectedIndexChanged += new EventHandler(cb_factory_SelectedIndexChanged);
            //this.dataGridView1.Controls.Add(cb_factory);
        }



        void refresh()
        {
            string sql = @"select * from FDD_depinfo where 1=@data";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                dataGridView1.DataSource = null;
                return;
            }
            
        }

        //private void cb_factory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dataGridView1.CurrentCell.Value = ((ComboBox)sender).Text;
        //}

        private void btn_add_Click(object sender, EventArgs e)
        {
            MyVar.Showmode = "add";
            Depdetail depdetail = new Depdetail();
            if (depdetail.ShowDialog() == DialogResult.OK)
            {
                String factory = Depdetail.depdetail.cb_factory.Text.Trim();
                String depname = Depdetail.depdetail.tb_depname.Text.Trim();

                string sql = @"insert into FDD_depinfo values (@factory,@depname)";
                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("factory", factory), SQLHelper.MakeParam("depname", depname));
                MsgBox.TipMsgShow(btn_add.Location.X + btn_add.Width +20, btn_add.Location.Y + btn_add.Height -100, this.Text, "添加成功！", this);
                refresh();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("确认删除部室“" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value.ToString() + "”", "提示", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        string sql = @"select * from FDD_Employee where factory=@factory and depname=@depname";
                        DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value.ToString()),
                            SQLHelper.MakeParam("depname", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value.ToString()));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show("该部门存在员工，不允许删除");
                            return;
                        }
                        else
                        { 
                            sql = @"delete from FDD_depinfo where id=@id";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("id", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                            MsgBox.TipMsgShow(btn_delete.Location.X + btn_delete.Width + 20, btn_delete.Location.Y + btn_delete.Height / 2 - 100, this.Text, "删除成功！", this);
                            refresh();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("请选中要删除的数据");
            }
        }

       

        //private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        //{
        //    cb_factory.Visible = false;
        //}

        //private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        //{
        //    cb_factory.Visible = false;
        //}

        //private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    cb_factory.Visible = false;
        //    dataGridView1.EndEdit();
        //}

        //private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    try
        //    {
        //        if (this.dataGridView1.CurrentCell.ColumnIndex == 1)
        //        {
        //            Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
        //            cb_factory.Left = rect.Left;
        //            cb_factory.Top = rect.Top;
        //            cb_factory.Height = rect.Height;
        //            cb_factory.Width = rect.Width;
        //            cb_factory.Visible = true;
        //            cb_factory.SelectedIndex = -1;
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    MyVar.Showmode = "update";
                    Depdetail depdetail = new Depdetail();
                    if (depdetail.ShowDialog() == DialogResult.OK)
                    {
                        String factory = Depdetail.depdetail.cb_factory.Text.Trim();
                        String depname = Depdetail.depdetail.tb_depname.Text.Trim();

                        string sql = @"update FDD_depinfo set factory=@factory,depname=@depname where id=@id";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("factory", factory), SQLHelper.MakeParam("depname", depname),
                            SQLHelper.MakeParam("id", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                        MsgBox.TipMsgShow(btn_change.Location.X + btn_change.Width + 20, btn_change.Location.Y + btn_change.Height - 100, this.Text, "修改成功！", this);
                        refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("选中要修改的行");
            }
        }
    }
}
