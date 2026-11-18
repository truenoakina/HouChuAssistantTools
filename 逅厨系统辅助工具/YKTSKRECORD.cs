using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class YKTSKRECORD : Form
    {
        int count;
        DataTable dt;

        public YKTSKRECORD()
        {
            InitializeComponent();
        }

        private void YKTSKRECORD_Load(object sender, EventArgs e)
        {
            begindate.Text= DateTime.Now.Date.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            enddate.Text= DateTime.Now.Date.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            string sql=@"select mr.card_id as 卡号,em.emp_fname as 姓名,D.depart_name as 部门,mr.temp_time as 刷卡时间 from MealRecords mr left join Employee em on mr.join_id=em.join_id 
                        LEFT JOIN Departs d on em.depart_id = D.depart_id
                        where temp_time>='"+ begindate.Text+"' and temp_time<= '"+ enddate.Text+ " 23:59:59' and ISNULL(mr.account_kind, 0)= 0 and(mr.flag = 0 or mr.type = 3) and (isnull(mr.is_unknow, 0) = 0 or ISNULL(mr.is_unknow, 0) > 0 and ISNULL(mr.old_is_unknow, 0) = 0) and (mr.type <> 3 or mr.flag = 1) and isnull(mr.is_del, 0)= 0 and 1=@data order by temp_time";
            DataSet ds = SQLHelperYKT.GetDataSet(sql, SQLHelperYKT.MakeParam("data", 1));
            dataGridView1.DataSource = ds.Tables[0];
            dt= ds.Tables[0]; 
            count = ds.Tables[0].Rows.Count;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 300;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            lbl_count.Text = "共" + count.ToString() + "行";

            //设置dataGridView双buffer代码如下，需要引用反射命名空间
            Type dgvType = this.dataGridView1.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGridView1, true, null);
            
        }

        private void btn_out_Click(object sender, EventArgs e)
        {
            string fileExt = ".xlsx";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                ExcelHelper.TableToExcelNomal(fbd.DirectoryPath + @"\" + "食堂消费刷卡记录", dt as DataTable, fileExt);
                MessageBox.Show(fbd.DirectoryPath + @"\" + "食堂消费刷卡记录" + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            }
            else if (fbd.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }
        }
    }
}
