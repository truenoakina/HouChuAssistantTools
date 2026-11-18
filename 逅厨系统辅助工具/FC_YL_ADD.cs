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
    public partial class FC_YL_ADD : Form
    {
        public static FC_YL_ADD fC_YL_ADD;

        public FC_YL_ADD()
        {
            InitializeComponent();
            fC_YL_ADD = this;
            this.cb_supplier.TextChanged += new EventHandler(cb_supplier_TextChanged);
        }

        private void FC_YL_ADD_Load(object sender, EventArgs e)
        {
            BindLeibie();
            BindSupplier();
        }

        private void BindLeibie()
        {
            string sql = "select FTYPE from FC_YLTYPE";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            List<string> ftype = new List<string>();
            while (reader.Read())
            {
                ftype.Add(reader["FTYPE"].ToString());
            }
            reader.Close();
            cb_leibie.DataSource = ftype;
            cb_leibie.DisplayMember = "FTYPE";
            cb_leibie.SelectedIndex = -1;
            cb_leibie.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BindSupplier()
        {
            string sql = "select FSUPPLIERNAME from FC_SUPPLIER";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            List<string> fsupplier= new List<string>();
            while (reader.Read())
            {
                fsupplier.Add(reader["FSUPPLIERNAME"].ToString());
            }
            reader.Close();
            cb_supplier.DataSource = fsupplier;
            cb_supplier.DisplayMember = "FSUPPLIERNAME";
            cb_supplier.SelectedIndex = -1;
            cb_supplier.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cb_supplier_TextChanged(object sender, EventArgs e)
        {
            //string search = cb_supplier.Text.Trim();
            //if (string.IsNullOrWhiteSpace(search)) {
            //    return;
            //}
            //cb_supplier.Items.Clear();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cb_leibie.Text.Trim() == String.Empty || tb_name.Text.Trim() == String.Empty)
            {
                MsgBox.TipMsgShow(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2, this.Text, "类别和名称不能为空！", this);
                return;
            }
            else
            {
                string sql = @"select * from FC_MATERIAL where FNAME='" + tb_name.Text.Trim() + "' and 1=@data";

                DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MsgBox.TipMsgShow(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2, this.Text, "原料名称重复，请检查！", this);
                    return;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
