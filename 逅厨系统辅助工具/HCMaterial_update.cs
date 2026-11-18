using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class HCMaterial_update : Form
    {
        public HCMaterial_update()
        {
            InitializeComponent();
        }

        private void HCMaterial_update_Load(object sender, EventArgs e)
        {
            string sql = @"select * from hcgoods where kcode=@kcode";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("kcode", MyVar.kCode));
            text_kcode.Text = ds.Tables[0].Rows[0]["kcode"].ToString();
            text_kname.Text = ds.Tables[0].Rows[0]["kname"].ToString();
            text_tiaoma.Text = ds.Tables[0].Rows[0]["tiaoma"].ToString();
            text_unit.Text = ds.Tables[0].Rows[0]["unit"].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认修改吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                string sql = @"update hcgoods set kcode=@kcode,kname=@kname,tiaoma=@tiaoma,unit=@unit where kcode=@kcode_old";
                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("kcode", text_kcode.Text.Trim()), SQLHelper.MakeParam("kname", text_kname.Text.Trim()),
                    SQLHelper.MakeParam("tiaoma", text_tiaoma.Text.Trim()), SQLHelper.MakeParam("unit", text_unit.Text.Trim()),
                    SQLHelper.MakeParam("kcode_old", MyVar.kCode));
                MessageBox.Show("修改成功");

            }
        }

        private void HCMaterial_update_FormClosed(object sender, FormClosedEventArgs e)
        {
            HCMaterial.hcmaterial.button1_Click(this, new EventArgs());
        }
    }
}
