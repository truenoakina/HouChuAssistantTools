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
    public partial class YKTupdate : Form
    {
        public YKTupdate()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void bt_check_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT emp_fname,FingerID FROM Employee where FingerID='' and 1=@data";
            DataSet ds = SQLHelperYKT.GetDataSet(sql, SQLHelperYKT.MakeParam("data", 1));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                dataGridView1.DataSource = null;
                MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "未查到异常人员数据！", this);
                return;
            }
        }

        private void bt_update_Click(object sender, EventArgs e)
        {
            //将没有fingerid的员工update fingerid
            int maxFingerID,fingerID;//fingerid
            int num;   //没有fingerid的员工数
            string join_id;
            string sql= @"select row_number() over(order by join_id) as id,join_id,emp_fname from Employee where FingerID = '' and 1=@data";
            DataSet ds = SQLHelperYKT.GetDataSet(sql, SQLHelperYKT.MakeParam("data", 1));
            num = ds.Tables[0].Rows.Count;
            if (num > 0)
            {
                sql = @"SELECT max(FingerID) as fingerID FROM Employee where 1=@data";
                DataSet ds2 = SQLHelperYKT.GetDataSet(sql, SQLHelperYKT.MakeParam("data", 1));
                maxFingerID = int.Parse(ds2.Tables[0].Rows[0][0].ToString().TrimStart('0'));//将查询到的最大FingerID转化为数字
                for (int i = 0; i < num; i++) {
                    join_id = ds.Tables[0].Rows[i][1].ToString();
                    fingerID = maxFingerID + i + 1;
                    sql = @"update Employee set FingerID=@fingerID where join_id=@join_id";
                    SQLHelperYKT.ExecSql(sql, SQLHelperYKT.MakeParam("fingerID", string.Format("{0:D6}",fingerID)), SQLHelperYKT.MakeParam("join_id", join_id));
                }
                MessageBox.Show("处理完毕，可以写入待下载信息"); 
            }
            else
            {
                MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "未查到异常人员数据,无需处理！", this);
                return;
            }
            
           
            

        }
    }
}
