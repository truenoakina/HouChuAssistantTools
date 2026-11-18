using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class Material_add : Form
    {
        public Material_add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (text_kcode.Text.Trim() != String.Empty & text_lcode.Text.Trim() != String.Empty & text_kname.Text.Trim() != String.Empty
                  & text_unit.Text.Trim() != String.Empty)
            {
                string sql = @"select * from KINDEEMATERIAL where kcode='"+ text_kcode.Text.Trim()+"' or lscode='"+ text_lcode.Text.Trim()+  "' and 1=@data";
                DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
                //string sql = @"select * from KINDEEMATERIAL where kcode=@kcode or lscode=@lscode";
                //(SQLHelper.GetCount(sql, SQLHelper.MakeParam("kcode", text_kcode.Text.Trim()), SQLHelper.MakeParam("lscode", text_lcode.Text.Trim())) > 0)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("该金蝶编码或者罗森编码已存在");
                    return;
                }
                else
                {
                    sql = @"insert into KINDEEMATERIAL values (@kcode,@lscode,@lscode2,@kname,@unit)";
                    SQLHelper.ExecSql(sql, SQLHelper.MakeParam("kcode", text_kcode.Text.Trim()), SQLHelper.MakeParam("lscode", text_lcode.Text.Trim()), SQLHelper.MakeParam("lscode2", txt_2BM.Text.Trim()),
                        SQLHelper.MakeParam("kname", text_kname.Text.Trim()), SQLHelper.MakeParam("unit", text_unit.Text.Trim()));
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
