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
    public partial class FC_BOM : Form
    {
        string yuanliao, caiyao;
        private DataTable dt;
        private SqlDataAdapter sda;
        private SqlCommandBuilder scb;
        private SqlConnection conn;
        private int i, startNum;
        private ComboBox cb_yl = new ComboBox();
        private ComboBox cb_caiyao = new ComboBox();


        public FC_BOM()
        {
            InitializeComponent();
        }

        private void FC_BOM_Load(object sender, EventArgs e)
        {

            conn = SQLHelper.GetConn();
            refresh();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].Visible = false;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[1].DefaultCellStyle.NullValue = "--双击选择--";
            dataGridView1.Columns[2].DefaultCellStyle.NullValue = "--双击选择--";

            BindCAIYAO();
            BindYL();
            cb_yl.Visible = false;
            cb_caiyao.Visible = false;
            cb_yl.SelectedIndexChanged += new EventHandler(cb_yl_SelectedIndexChanged);
            cb_caiyao.SelectedIndexChanged += new EventHandler(cb_caiyao_SelectedIndexChanged);
            cb_caiyao.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            cb_yl.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);/*添加事件*/

            this.dataGridView1.Controls.Add(cb_yl);
            this.dataGridView1.Controls.Add(cb_caiyao);
        }

        private void BindYL()
        {

            string sql = "select FNAME,FUNIT from FC_MATERIAL order by fname";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            List<string> fname = new List<string>();
            while (reader.Read())
            {
                fname.Add(reader["FNAME"].ToString());
            }
            reader.Close();
            cb_yl.DataSource = fname;
            cb_yl.DisplayMember = "FNAME";
            cb_yl.SelectedIndex = -1;
            cb_yl.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BindCAIYAO()
        {

            string sql = "select FNAME from FC_CAIYAO order by fname";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            List<string> fname = new List<string>();
            while (reader.Read())
            {
                fname.Add(reader["FNAME"].ToString());
            }
            reader.Close();
            cb_caiyao.DataSource = fname;
            cb_caiyao.DisplayMember = "FNAME";
            cb_caiyao.SelectedIndex = -1;
            cb_caiyao.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cb_yl_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = ((ComboBox)sender).Text;

        }

        private void cb_caiyao_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = ((ComboBox)sender).Text;

        }





        private void btn_add_Click(object sender, EventArgs e)
        {
            //直接在datagridview最后一行新增一行，可直接编辑，保存之后同步至数据库，需要在LOAD事件中确定主键id的自增
            DataRow row = null;
            row = dt.NewRow();
            ((DataTable)dataGridView1.DataSource).Rows.Add(row);
            
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("确认保存修改的数据吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                sda.Update(dt);
                refresh();
                btn_add.Enabled = true;
                MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "保存成功！", this);
            }
        }


        //限制单元格只能输入数字
        private void Cells_KeyPress(object sender, KeyPressEventArgs e) //自定义事件
        {
            if ((this.dataGridView1.CurrentCell.ColumnIndex == 4 || this.dataGridView1.CurrentCell.ColumnIndex == 5))
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
                if (e.KeyChar == '\b') e.Handled = false;
                if (e.KeyChar == '.') e.Handled = false;
            }
            else if (this.dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
                if (e.KeyChar == '\b') e.Handled = false;
            }
        }

        

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            cb_yl.Visible = false;
            cb_caiyao.Visible = false;
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            cb_yl.Visible = false;
            cb_caiyao.Visible = false;
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cb_yl.Visible = false;
            cb_caiyao.Visible = false;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认删除该行数据吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                dataGridView1.Rows.RemoveAt(i);
                sda.Update(dt);
                btn_add.Enabled = true;
                MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "删除成功！", this);
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

        

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            //{
            //    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
            //    {
            //        dataGridView1.Rows[e.RowIndex].ErrorText = "内容不能为空";
            //        btn_save.Enabled = false;
            //        e.Cancel = true;
                    
            //    }
            //    else
            //    {
            //        dataGridView1.Rows[e.RowIndex].ErrorText = null;
            //        btn_save.Enabled = true;
            //    }
            //}
        }

        

        //限制单元格只能输入数字
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.Aquamarine;

            DataGridViewTextBoxEditingControl  CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
            CellEdit.SelectAll();
            CellEdit.KeyPress += Cells_KeyPress; //绑定事件

        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == ""|| dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "--双击选择--")
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "内容不能为空";
                    btn_save.Enabled = false;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = null;
                    btn_save.Enabled = true;
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                Decimal oldvalue = Convert.ToDecimal(this.dataGridView1.CurrentCell.Value);
                this.dataGridView1.CurrentCell.Value = Math.Round(oldvalue, 2, MidpointRounding.AwayFromZero);
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.ReadOnly == false)
            {
                try
                {
                    if (this.dataGridView1.CurrentCell.ColumnIndex == 1)
                    {
                        Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                        cb_yl.Left = rect.Left;
                        cb_yl.Top = rect.Top;
                        cb_yl.Height = rect.Height;
                        cb_yl.Width = rect.Width;
                        cb_yl.Visible = true;
                        //cb_yl.SelectedIndex = -1;
                    }
                    else if (this.dataGridView1.CurrentCell.ColumnIndex == 2)
                    {
                        Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                        cb_caiyao.Left = rect.Left;
                        cb_caiyao.Top = rect.Top;
                        cb_caiyao.Height = rect.Height;
                        cb_caiyao.Width = rect.Width;
                        cb_caiyao.Visible = true;
                        //cb_caiyao.SelectedIndex = -1;
                    }
                }
                catch
                {
                }
            }
            else {
                return;
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[i].ReadOnly = false;
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh() {
            yuanliao = tb_yl.Text.Trim();
            caiyao=tb_caiyao.Text.Trim();
            string sql = @"select id,FYUANLIAO AS 原料,FCAIYAO as 菜肴,FYLNUM AS 组装块数,FYLWEIGHT_J AS 净料克重,FYLWEIGHT_M as 净料毛重 from FC_CAIYAOBOM 
                           where FCAIYAO like '%"+ caiyao+"%' and FYUANLIAO like '%"+yuanliao+"%' order by FYUANLIAO,FCAIYAO";
            sda = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            scb = new SqlCommandBuilder(sda);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows[i].ReadOnly = true;
            }

            sql = @"select FYUANLIAO,FCAIYAO,COUNT(1) FROM FC_CAIYAOBOM where 1=@data group by FYUANLIAO,FCAIYAO having COUNT(*)>=2";
            DataSet ds2= SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
            if (ds2.Tables[0].Rows.Count > 0)
            {
                error.Text = "警告:存在重复的原料菜肴组合，会影响库存用量计算，请检查！";
            }
            else
            {
                error.Text = "";
            }


            sql = @"select max(id) as maxid from FC_CAIYAOBOM  where 1=@data";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
            //设置新增行是的id值，设定初始值startnum和步增值1
            if (ds.Tables[0].Rows[0][0].ToString() == "")
            {
                startNum = 1;
            }
            else
            {
                startNum = (int)ds.Tables[0].Rows[0][0] + 1;
            }
            dt.Columns["id"].AutoIncrement = true;
            dt.Columns[0].AutoIncrementSeed = startNum;
            dt.Columns[0].AutoIncrementStep = 1;


        }
    }
}
