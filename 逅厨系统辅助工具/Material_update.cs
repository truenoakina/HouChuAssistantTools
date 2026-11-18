using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class Material_update : Form
    {
        public Material_update()
        {
            InitializeComponent();
        }

        private void Material_update_Load(object sender, EventArgs e)
        {
            string sql = @"select * from KINDEEMATERIAL where kcode=@kcode";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("kcode", MyVar.kCode));
            text_kcode.Text = ds.Tables[0].Rows[0]["kcode"].ToString();
            text_lcode.Text = ds.Tables[0].Rows[0]["lscode"].ToString();
            txt_2BM.Text= ds.Tables[0].Rows[0]["lscode2"].ToString();
            text_kname.Text = ds.Tables[0].Rows[0]["kname"].ToString();
            text_unit.Text = ds.Tables[0].Rows[0]["kunit"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认修改吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                string sql = @"update KINDEEMATERIAL set kcode=@kcode,lscode=@lscode,lscode2=@lscode2,kname=@kname,kunit=@kunit where kcode=@kcode_old";
                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("kcode", text_kcode.Text.Trim()), SQLHelper.MakeParam("lscode", text_lcode.Text.Trim()),
                    SQLHelper.MakeParam("lscode2", txt_2BM.Text.Trim()), SQLHelper.MakeParam("kname", text_kname.Text.Trim()), SQLHelper.MakeParam("kunit", text_unit.Text.Trim()),
                    SQLHelper.MakeParam("kcode_old", MyVar.kCode));
                MessageBox.Show("修改成功");

            }
        }

        private void Material_update_FormClosed(object sender, FormClosedEventArgs e)
        {
            Material.material.button1_Click(this, new EventArgs());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
