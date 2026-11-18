using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class HCcustomer_add : Form
    {
        public HCcustomer_add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (text_code.Text.Trim() != String.Empty & text_name.Text.Trim() != String.Empty)
            {
                string sql = @"select * from Customer where cuscode='" + text_code.Text.Trim() + "' or cusname='" + text_name.Text.Trim() + "' and 1=@data";

                DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("该客户已存在");
                    return;
                }
                else
                {
                    sql = @"insert into Customer ([cuscode],[cusname]) values (@cuscode,@cusname)";
                    SQLHelper.ExecSql(sql, SQLHelper.MakeParam("cuscode", text_code.Text.Trim()), SQLHelper.MakeParam("cusname", text_name.Text.Trim()));
                    MessageBox.Show("添加成功");
                }
            }
            else
            {
                MessageBox.Show("添加内容不能为空，请检查");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
