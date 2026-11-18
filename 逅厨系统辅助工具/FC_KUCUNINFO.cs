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
    public partial class FC_KUCUNINFO : Form
    {
        string org, yuanliao;
        private DataTable dt,dtyl;
        private SqlDataAdapter sda,sdayl;
        private SqlCommandBuilder scb;
        private SqlConnection conn;
        private int i, startNum;
        private ComboBox cb_yl = new ComboBox();
        private DateTimePicker dtp = new DateTimePicker();


        public FC_KUCUNINFO()
        {
            InitializeComponent();
        }

        private void FC_KUCUNINFO_Load(object sender, EventArgs e)
        {
            conn = SQLHelper.GetConn();
            cb_org.SelectedIndex = 0;
            refresh();
            btn_save.Enabled = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;

            dataGridView1.Columns[2].Width = 160;
            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 120;
            dataGridView1.Columns[7].Width = 120;
            dataGridView1.Columns[8].Width = 160;
            dataGridView1.Columns[8].ReadOnly = true;
            dataGridView1.Columns[2].DefaultCellStyle.NullValue = "--双击选择--";
            BindYL();
            cb_yl.Visible = false;
            cb_yl.SelectedIndexChanged += new EventHandler(cb_yl_SelectedIndexChanged);
            cb_yl.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.Controls.Add(cb_yl);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.TextChanged+= new EventHandler(dtp_TextChange);
            this.dataGridView1.Controls.Add(dtp);
        }

        private void dtp_TextChange(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = dtp.Text.ToString();
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            refresh();
            
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            //直接在datagridview最后一行新增一行，可直接编辑，保存之后同步至数据库，需要在LOAD事件中确定主键id的自增
            DataRow row = null;
            row = dt.NewRow();
            row["FORG"] = cb_org.Text.Trim();
            row["最后更新时间"]= DateTime.Now.ToString();
            ((DataTable)dataGridView1.DataSource).Rows.Add(row);
            btn_save.Enabled = true;
        }

        //创建原料类
        public class Yuanliao
        {
            public string fname;
            public string funit;
        }

        //获取所有原料对象
        public List<Yuanliao> GetYuanliao()
        {
            string sql = "select FNAME,FUNIT from FC_MATERIAL order by fname";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Yuanliao> list = new List<Yuanliao>();
            if(objReader.HasRows)
            {
                while (objReader.Read())
                {
                    Yuanliao yuanliao = new Yuanliao();
                    yuanliao.fname = objReader["FNAME"].ToString();
                    yuanliao.funit = objReader["FUNIT"].ToString();
                    list.Add(yuanliao);
                }
            }
            objReader.Close();
            return list;
        }


        private void BindYL()
        {

            string sql = "select FNAME,FUNIT from FC_MATERIAL where 1=@data order by fname";
            DataSet dsyl = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
            dtyl = dsyl.Tables[0];
            cb_yl.DataSource = dtyl;
            cb_yl.DisplayMember = "FNAME";
            cb_yl.ValueMember = "FNAME";
            cb_yl.SelectedIndex = -1;
            cb_yl.DropDownStyle = ComboBoxStyle.DropDownList;
        //    cb_yl.Items.Clear();
        //    cb_yl.Items.AddRange(GetYuanliao().ToArray());
        //    cb_yl.DisplayMember = "fname";
        //    cb_yl.ValueMember = "fname";
        //    cb_yl.SelectedIndex = -1;
        //    cb_yl.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            cb_yl.Visible = false;
            dtp.Visible = false;
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            cb_yl.Visible = false;
            dtp.Visible = false;
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cb_yl.Visible = false;
            dtp.Visible = false;
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
                if (dgr.Cells[2].Value.ToString().Trim() == "" || dgr.Cells[2].Value.ToString().Trim() == "--双击选择--" || dgr.Cells[2].Value == null || dgr.Cells[4].Value.ToString().Trim() == "0")
                {
                    MessageBox.Show("原料名称或者库存为空，不能保存，请检查");
                    return;
                }
            }


            DialogResult dr = MessageBox.Show("确认保存"+cb_org.Text+"工厂的库存信息吗？", "提示", MessageBoxButtons.OKCancel);
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
            
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.Aquamarine;

            DataGridViewTextBoxEditingControl CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
            CellEdit.SelectAll();
            CellEdit.KeyPress += Cells_KeyPress; //绑定事件
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2|| e.ColumnIndex == 3)
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                if (this.dataGridView1.CurrentCell.Value==DBNull.Value)
                {
                    this.dataGridView1.CurrentCell.Value = 0;
                }
                else
                {
                Decimal oldvalue = Convert.ToDecimal((this.dataGridView1.CurrentCell.Value));
                this.dataGridView1.CurrentCell.Value = Math.Round(oldvalue, 2, MidpointRounding.AwayFromZero);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.ReadOnly == false)
            {
                try
                {
                    if (this.dataGridView1.CurrentCell.ColumnIndex == 6)
                    {
                        Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                        dtp.Left = rect.Left;
                        dtp.Top = rect.Top;
                        dtp.Height = rect.Height;
                        dtp.Width = rect.Width;
                        dtp.Visible = true;
                        //cb_yl.SelectedIndex = -1;
                    }
                    else
                    {
                        dtp.Visible = false;
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

        private void update_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[i].ReadOnly = false;
            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
            dataGridView1.Rows[i].Cells[8].Value= DateTime.Now.ToString();
            btn_save.Enabled = true;
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
                        cb_yl.Left = rect.Left;
                        cb_yl.Top = rect.Top;
                        cb_yl.Height = rect.Height;
                        cb_yl.Width = rect.Width;
                        cb_yl.Visible = true;
                        //cb_yl.SelectedIndex = -1;
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

        private void cb_yl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cb_yl.SelectedIndex.ToString());   
            dataGridView1.CurrentCell.Value = ((ComboBox)sender).Text;
            if (cb_yl.SelectedIndex >= 0)
            {
                dataGridView1.CurrentRow.Cells[3].Value = dtyl.Rows[cb_yl.SelectedIndex][1].ToString();
            }
            else
            {
                dataGridView1.CurrentRow.Cells[3].Value = "";
            }

        }

        public void refresh()
        {
            yuanliao = tb_yl.Text.Trim();
            org = cb_org.Text.Trim();
            string sql = @"select id,FORG,FYLNAME AS 原料,FUNIT AS 单位,FKCQTY as 现有库存量,FZTQTY AS 在途量,FDHDATE AS 预计到货日,FBEIZHU AS 备注,FLASTTIME AS 最后更新时间 from FC_KUCUNINFO 
                           where FYLNAME like '%" + yuanliao + "%' and FORG = '" + org + "' order by FYLNAME";
            sda = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            scb = new SqlCommandBuilder(sda);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows[i].ReadOnly = true;
            }



            sql = @"select max(id) as maxid from FC_KUCUNINFO  where 1=@data";
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
            btn_save.Enabled = false;
        }
    }
}
