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
    public partial class FC_ORDER_CHANGE : Form
    {
        int oid,odid;
        string fbillno, forg;
        DateTime fdate;
        private DataTable dt;
        private SqlDataAdapter sda;
        private SqlCommandBuilder scb;
        private SqlConnection conn;
        private ComboBox cb_caiyao = new ComboBox();
        int i;
        bool isChange=false;//判断是否为修改状态下，确定右击菜单能否使用


        public FC_ORDER_CHANGE()
        {
            InitializeComponent();
        }

       

        public FC_ORDER_CHANGE(int foid,int fodid)
        {
            InitializeComponent();
            oid = foid;
            odid = fodid;
        }

        

        private void FC_ORDER_CHANGE_Load(object sender, EventArgs e)
        {
            conn = SQLHelper.GetConn();
            string sql = @"select FBILLNO,FORG,FDATE from FC_order where id=@oid";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("oid", oid));
            fbillno = ds.Tables[0].Rows[0][0].ToString();
            forg= ds.Tables[0].Rows[0][1].ToString();
            fdate= Convert.ToDateTime(ds.Tables[0].Rows[0][2].ToString());
            cb_org.Text = forg;
            lb_billno.Text = fbillno;
            dtp_date.Value = fdate;
            refresh();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].DefaultCellStyle.NullValue = "--双击选择--";
            cb_org.Enabled = false;
            dtp_date.Enabled = false;
            btn_save.Enabled = false;
            isChange = false;
            contextMenuStrip1.Items[0].Enabled = isChange;
            contextMenuStrip1.Items[1].Enabled = isChange;
            dataGridView1.ReadOnly = true;
            BindCAIYAO();
            cb_caiyao.Visible = false;
            cb_caiyao.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            cb_caiyao.SelectedIndexChanged += new EventHandler(cb_caiyao_SelectedIndexChanged);
            this.dataGridView1.Controls.Add(cb_caiyao);

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

        private void refresh()
        {
            string sql = @"select id,oid,FCAIYAO as 菜肴,FQTY as 订单数量 from FC_orderdetail where oid="+oid;
            sda = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            scb = new SqlCommandBuilder(sda);
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            cb_org.Enabled = true;
            dtp_date.Enabled = true;
            btn_change.Enabled= false;
            btn_save.Enabled = true;
            dataGridView1.ReadOnly = false;
            isChange = true;
            contextMenuStrip1.Items[0].Enabled = isChange;
            contextMenuStrip1.Items[1].Enabled = isChange;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.ReadOnly == false)
            {
                try
                {
                    if (this.dataGridView1.CurrentCell.ColumnIndex == 2)
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

        //限制单元格只能输入数字
        private void Cells_KeyPress(object sender, KeyPressEventArgs e) //自定义事件
        {
            if (this.dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
                if (e.KeyChar == '\b') e.Handled = false;
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            
            foreach (DataGridViewRow dgr in dataGridView1.Rows)
            { 
                if (dgr.Cells[2].Value.ToString().Trim() == "" || dgr.Cells[3].Value.ToString().Trim() == ""|| dgr.Cells[2].Value.ToString().Trim() == "--双击选择--")
                {
                    MessageBox.Show("有内容为空，无法保存，请检查");
                    return;
                }
            }

           
                DialogResult dr = MessageBox.Show("确认保存修改的订单数据吗？", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                   //修改表头
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
                    date = dtp_date.Value.ToString("yyyyMMdd");
                    lsh = oid.ToString("0000");
                    billNo = org + date + lsh;
                    string sql = @"update FC_ORDER set FBILLNO=@fbillno,FORG=@org,FDATE=@date where id=@id";
                    SQLHelper.ExecSql(sql, SQLHelper.MakeParam("fbillno", billNo), SQLHelper.MakeParam("org", cb_org.Text), SQLHelper.MakeParam("date", date), SQLHelper.MakeParam("id", oid));
                    lb_billno.Text = billNo;

                    //修改表体
                    sda.Update(dt);
                    refresh();
                    btn_change.Enabled = true;
                    btn_save.Enabled = false;
                    dtp_date.Enabled = false;
                    isChange = false;
                    contextMenuStrip1.Items[0].Enabled = isChange;
                    contextMenuStrip1.Items[1].Enabled = isChange;
                    MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "修改成功！", this);
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

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            cb_caiyao.Visible = false;
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            cb_caiyao.Visible = false;
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cb_caiyao.Visible = false;
        }

        

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
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

        

        private void add_Click(object sender, EventArgs e)
        {
            DataRow row = null;
            row = dt.NewRow();
            row["oid"]=oid;
            ((DataTable)dataGridView1.DataSource).Rows.Add(row);
            btn_save.Enabled = true;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认删除该行数据吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                dataGridView1.Rows.RemoveAt(i);
                sda.Update(dt);
                MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "删除成功！", this);
            }
        }
    }
}
