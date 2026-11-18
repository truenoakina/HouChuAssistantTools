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
    public partial class FC_ORDER_ADD : Form
    {
        private DataTable dt;
        private SqlDataAdapter sda;
        private SqlCommandBuilder scb;
        private SqlConnection conn;
        private ComboBox cb_caiyao = new ComboBox();
        int i;
        

        public FC_ORDER_ADD()
        {
            InitializeComponent();
        }

        private void FC_ORDER_ADD_Load(object sender, EventArgs e)
        {
            cb_org.SelectedIndex = 0;
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[0].DefaultCellStyle.NullValue = "--双击选择--";
            dataGridView1.AllowUserToAddRows = false;
            BindCAIYAO();
            cb_caiyao.Visible = false;
            cb_caiyao.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            cb_caiyao.SelectedIndexChanged += new EventHandler(cb_caiyao_SelectedIndexChanged);
            this.dataGridView1.Controls.Add(cb_caiyao);
            btn_save.Enabled = false;
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

        private void cb_caiyao_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = ((ComboBox)sender).Text;

        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            cb_caiyao.Visible = false;
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cb_caiyao.Visible = false;
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            cb_caiyao.Visible = false;
        }


        //限制单元格只能输入数字
        private void Cells_KeyPress(object sender, KeyPressEventArgs e) //自定义事件
        {
            if (this.dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
                if (e.KeyChar == '\b') e.Handled = false;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.ReadOnly == false)
            {
                try
                {
                    if (this.dataGridView1.CurrentCell.ColumnIndex == 0)
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
            else
            {
                return;
            }
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "" || dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "--双击选择--")
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

        private void btn_rowadd_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = "--双击选择--";
            dataGridView1.Rows[index].Cells[1].Value = "0";
            btn_save.Enabled = false;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(i);
            if (dataGridView1.Rows.Count > 0)
            {
                btn_save.Enabled = true;
            }
            else
            {
                btn_save.Enabled = false;
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

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.Aquamarine;

            DataGridViewTextBoxEditingControl CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
            CellEdit.SelectAll();
            CellEdit.KeyPress += Cells_KeyPress; //绑定事件
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            
            foreach (DataGridViewRow dgr in dataGridView1.Rows)
            {
                if (dgr.Cells[0].Value.ToString().Trim() == ""|| dgr.Cells[0].Value.ToString().Trim() == "--双击选择--"|| dgr.Cells[1].Value==null|| dgr.Cells[1].Value.ToString().Trim() == "0")
                {
                    MessageBox.Show("有内容为空，无法保存，请检查");
                    return;
                    
                }
            }

           DialogResult dr = MessageBox.Show("确认保存", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    int id;//order主表中的主键ID
                    string org, date, lsh, billNo;//订单单据号,org+date+lsh流水号
                    org = "";
                    if (cb_org.SelectedIndex == 0)
                    {
                        org = "JY";
                    }
                    else if (cb_org.SelectedIndex == 1)
                    {
                        org = "NJ";
                    }
                    date = dateTimePicker1.Value.ToString("yyyyMMdd");

                    string sql = @"select max(id) as maxid from FC_order where 1=@data";
                    DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
                    //设置order主表中的id
                    if (ds.Tables[0].Rows[0][0].ToString() == "")
                    {
                        id = 1;
                    }
                    else
                    {
                        id = (int)ds.Tables[0].Rows[0][0] + 1;
                    }
                    lsh = id.ToString("0000");
                    billNo = org + date + lsh;
                    sql = @"insert FC_order values (@id,@FBILLNO,@FORG,@FDATE)";
                    bool exec = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("id", id), SQLHelper.MakeParam("FBILLNO", billNo), SQLHelper.MakeParam("FORG", cb_org.Text)
                       , SQLHelper.MakeParam("FDATE", dateTimePicker1.Value));
                    if (exec)
                    {
                        for (i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            sql = @"insert FC_orderdetail values (@oid,@caiyao,@qty)";
                            exec = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("oid", id), SQLHelper.MakeParam("caiyao", dataGridView1.Rows[i].Cells[0].Value.ToString()), SQLHelper.MakeParam("qty", Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value.ToString())));

                        }
                        if (exec)
                        {
                            MessageBox.Show("保存成功");
                            this.Close();
                        }
                    }
                }
            
        }
    }
}
