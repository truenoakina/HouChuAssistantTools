using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class HCMaterial_add : Form
    {
        public HCMaterial_add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (text_kcode.Text.Trim() != String.Empty & text_name.Text.Trim() != String.Empty & text_tiaoma.Text.Trim() != String.Empty
                 & text_unit.Text.Trim() != String.Empty)
            {
                string sql = @"select * from hcgoods where kcode='" + text_kcode.Text.Trim() + "' or kname='" + text_name.Text.Trim() 
                    + "' or tiaoma='"+ text_tiaoma.Text.Trim() + "' and 1=@data";
                DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("该金蝶编码或者名称或者条码已存在");
                    return;
                }
                else
                {
                    sql = @"insert into hcgoods values (@kcode,@kname,@unit,@tiaoma)";
                    SQLHelper.ExecSql(sql, SQLHelper.MakeParam("kcode", text_kcode.Text.Trim()), SQLHelper.MakeParam("kname", text_name.Text.Trim()),
                        SQLHelper.MakeParam("unit", text_unit.Text.Trim()),SQLHelper.MakeParam("tiaoma", text_tiaoma.Text.Trim()));
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
