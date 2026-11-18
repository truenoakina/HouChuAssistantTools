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
    public partial class SaleData : Form
    {
        public SaleData()
        {
            InitializeComponent();
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            string sql = @"select cus.fname as 学校,cast(sum(ckf.FALLAMOUNT) as decimal(10,0)) as 销售额 from  T_SAL_OUTSTOCKENTRY ckd inner join T_SAL_OUTSTOCK ck on ck.FID=ckd.FID
                    inner join T_SAL_OUTSTOCKENTRY_F ckf on ckd.FENTRYID=ckf.FENTRYID
                    inner join T_BD_CUSTOMER_L cus on cus.FCUSTID=ck.FCUSTOMERID
                    where ck.FDATE=@date and ck.FSALEORGID=516100 group by cus.FNAME
                    union all 
                    select '合计',cast(sum(ckf.FALLAMOUNT) as decimal(10,0)) from  T_SAL_OUTSTOCKENTRY ckd inner join T_SAL_OUTSTOCK ck on ck.FID=ckd.FID
                    inner join T_SAL_OUTSTOCKENTRY_F ckf on ckd.FENTRYID=ckf.FENTRYID where ck.FDATE=@date and ck.FSALEORGID=516100";
            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("@date", dateTimePicker1.Value.ToShortDateString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersWidth = 30;
            dataGridView1.Columns[0].Width = 240;
            dataGridView1.Columns[0].HeaderText = "学校";
            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[1].HeaderText = "销售额";
        }

        private void SaleData_Load(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
