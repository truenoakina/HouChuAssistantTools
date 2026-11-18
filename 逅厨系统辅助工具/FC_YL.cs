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
    public partial class FC_YL : Form
    {
        private DataTable dt;
        private SqlDataAdapter sda;
        private SqlCommandBuilder scb;
        private ComboBox cb_leibie = new ComboBox();
        private SqlConnection conn;
        private int i;

        public FC_YL()
        {
            InitializeComponent();
            
        }

        private void FC_YL_Load(object sender, EventArgs e)
        {
            int startNum;
            conn = SQLHelper.GetConn();
            refresh();
            //设置新增行的最大主键
            if (dt.Rows.Count > 0)
            {
                 startNum = (int)dt.Rows[dt.Rows.Count - 1][0] + 1;
            }
            else {
                 startNum = 1;
            }
            dt.Columns["id"].AutoIncrement = true;
            dt.Columns[0].AutoIncrementSeed = startNum;
            dt.Columns[0].AutoIncrementStep = 1;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            
            

            BindLeibie();
            cb_leibie.Visible = false;
            cb_leibie.SelectedIndexChanged += new EventHandler(cb_leibie_SelectedIndexChanged);
            this.dataGridView1.Controls.Add(cb_leibie);

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            
            sda.Update(dt);
            dataGridView1.DataSource = dt;
        }

        private void BindLeibie()
        {
            //DataTable dtLeibie = new DataTable();
            //dtLeibie.Columns.Add("value");
            //DataRow drLeibie;
            //drLeibie = dtLeibie.NewRow();
            //drLeibie[0] = "包材";
            //dtLeibie.Rows.Add(drLeibie);
            //drLeibie = dtLeibie.NewRow();
            //drLeibie[0] = "原料";
            //dtLeibie.Rows.Add(drLeibie);
            //cb_leibie.DisplayMember = "value";
            string sql = "select FTYPE from FC_YLTYPE";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            List<string> ftype = new List<string>();
            while (reader.Read()) {
                ftype.Add(reader["FTYPE"].ToString());
            }
            reader.Close();
            cb_leibie.DataSource = ftype;
            cb_leibie.DisplayMember = "FTYPE";
            cb_leibie.SelectedIndex = -1;
            cb_leibie.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        
        private void cb_leibie_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = ((ComboBox)sender).Text;
            //dataGridView1.CurrentCell.Tag = ((ComboBox)sender).Text;
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            cb_leibie.Visible = false;
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cb_leibie.Visible = false;
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            cb_leibie.Visible = false;
            dataGridView1.EndEdit();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            //直接在datagridview最后一行新增一行，可直接编辑，保存之后同步至数据库，需要在LOAD事件中确定主键id的自增
            // DataRow row = null;
            // row = dt.NewRow();
            //((DataTable)dataGridView1.DataSource).Rows.Add(row);
            FC_YL_ADD fC_YL_ADD = new FC_YL_ADD();
            if (fC_YL_ADD.ShowDialog() == DialogResult.OK)
            {
                String yltype = FC_YL_ADD.fC_YL_ADD.cb_leibie.Text.Trim();
                String ylname = FC_YL_ADD.fC_YL_ADD.tb_name.Text.Trim();
                String ylguige = FC_YL_ADD.fC_YL_ADD.tb_guige.Text.Trim();
                String ylsupplier = FC_YL_ADD.fC_YL_ADD.cb_supplier.Text.Trim();
                String ylunit = FC_YL_ADD.fC_YL_ADD.tb_unit.Text.Trim();
                String ylbeizhu = FC_YL_ADD.fC_YL_ADD.tb_beizhu.Text.Trim();
               
                string sql = @"insert into FC_MATERIAL values (@FTYPE,@FNAME,@FSPECIFICATION,@FSUPPLIER,@FUNIT,@FBEIZHU)";
                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("FTYPE", yltype), SQLHelper.MakeParam("FNAME", ylname), SQLHelper.MakeParam("FSPECIFICATION", ylguige)
                    , SQLHelper.MakeParam("FSUPPLIER", ylsupplier), SQLHelper.MakeParam("FUNIT", ylunit), SQLHelper.MakeParam("FBEIZHU", ylbeizhu));
                MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "添加成功！", this);
                refresh();
                        
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
                    contestmenustrip.Show(MousePosition.X, MousePosition.Y);
                    i = e.RowIndex;
                }
                else 
                {
                    return;
                }

            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认删除该行数据吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                dataGridView1.Rows.RemoveAt(i);
                sda.Update(dt);
            }
        }

       
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    cb_leibie.Left = rect.Left;
                    cb_leibie.Top = rect.Top;
                    cb_leibie.Height = rect.Height;
                    cb_leibie.Width = rect.Width;
                    cb_leibie.Visible = true;
                    cb_leibie.SelectedIndex = -1;
                }
                else
                {

                }
            }
            catch
            {
            }
        }

        private void refresh() {
            sda = new SqlDataAdapter("select id,FTYPE as 类别,FNAME as 名称,FSPECIFICATION as 规格,FSUPPLIER as 供应商,FUNIT as 单位,FBEIZHU as 备注 from FC_MATERIAL order by id", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            scb = new SqlCommandBuilder(sda);
        }
    }
}
