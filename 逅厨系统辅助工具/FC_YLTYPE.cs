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
    public partial class FC_YLTYPE : Form
    {
        public FC_YLTYPE()
        {
            InitializeComponent();
        }

        private void FC_YLTYPE_Load(object sender, EventArgs e)
        {
            string sql = @"select * from FC_YLTYPE where @data=1";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                return;
            }
            dataGridView1.ReadOnly = true;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            FC_YLTYPE_ADD fC_YLTYPE_ADD = new FC_YLTYPE_ADD();
            if (fC_YLTYPE_ADD.ShowDialog() == DialogResult.OK)
            {
                String FTYPE = FC_YLTYPE_ADD.fC_YLTYPE_ADD.textBox1.Text.Trim();
                if (FTYPE != String.Empty)
                {
                    string sql = @"select * from FC_YLTYPE where FTYPE='" + FTYPE + "' and 1=@data";

                    DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("该类别已存在!");
                        return;
                    }
                    else
                    {
                        sql = @"insert into FC_YLTYPE values (@FTYPE)";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("FTYPE", FTYPE));
                        MessageBox.Show("添加成功");
                        sql = @"select * from FC_YLTYPE where @data=1";
                        ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dataGridView1.DataSource = ds.Tables[0];
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else {
                    return;
                }
            }
        }

        
    }
}
