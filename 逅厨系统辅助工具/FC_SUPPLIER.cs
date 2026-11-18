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
    public partial class FC_SUPPLIER : Form
    {
        public DataTable dt;
        public SqlDataAdapter sda;
        public SqlCommandBuilder scb;


        public FC_SUPPLIER()
        {
            InitializeComponent();
        }

        private void FC_SUPPLIER_Load(object sender, EventArgs e)
        {
            SqlConnection conn = SQLHelper.GetConn();
            sda = new SqlDataAdapter("select id,FSUPPLIERNAME as 供应商,FBEIZHU as 备注 from FC_SUPPLIER", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].Visible = false;
            scb = new SqlCommandBuilder(sda);

            //string sql = @"select * from FC_SUPPLIER where @data=1";
            //DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    dataGridView1.AllowUserToAddRows = false;
            //    dataGridView1.DataSource = ds.Tables[0];
            //}
            //else
            //{
            //    return;
            //}
            //dataGridView1.ReadOnly = true;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            FC_SUPPLIER_ADD fC_SUPPLIER_ADD = new FC_SUPPLIER_ADD();
            if (fC_SUPPLIER_ADD.ShowDialog() == DialogResult.OK)
            {
                String FSUPPLIERNAME = FC_SUPPLIER_ADD.fC_SUPPLIER_ADD.text_name.Text.Trim();
                String FBEIZHU = FC_SUPPLIER_ADD.fC_SUPPLIER_ADD.text_beizhu.Text.Trim();
                if (FSUPPLIERNAME != String.Empty)
                {
                    string sql = @"select * from FC_SUPPLIER where FSUPPLIERNAME='" + FSUPPLIERNAME + "' and 1=@data";

                    DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("该供应商已存在!");
                        return;
                    }
                    else
                    {
                        sql = @"insert into FC_SUPPLIER values (@FSUPPLIERNAME,@FBEIZHU)";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("FSUPPLIERNAME", FSUPPLIERNAME), SQLHelper.MakeParam("FBEIZHU", FBEIZHU));
                        MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "添加成功！", this);
                        sql = @"select id,FSUPPLIERNAME as 供应商,FBEIZHU as 备注 from FC_SUPPLIER where @data=1";
                        ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dt = new DataTable();
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                            scb = new SqlCommandBuilder(sda);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }

       
    }
}
