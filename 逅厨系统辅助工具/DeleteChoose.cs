using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class DeleteChoose : Form
    {
        private bool flag = false;

        public DeleteChoose()
        {
            InitializeComponent();
        }

        private void btn_AllDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定全部删除吗？", "提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string sql = @"delete from APP_CodeCheckRecord20 where 1=@data";
                flag=SQLHelperJD.ExecSql(sql, SQLHelperJD.MakeParam("data", 1));
                if (flag == true)
                {
                    //调用父窗体中的refresh方法
                    Test20.MyMainForm.refresh();
                    MessageBox.Show("删除成功");
                }
                else
                {
                    return;
                }
                
                
            }
            else
            {
               
            }
        }

        private void btn_PartDel_Click(object sender, EventArgs e)
        {
            PartDelete partDelete = new PartDelete();
            //在父窗体中间弹出新窗口
            partDelete.StartPosition = FormStartPosition.Manual;
            partDelete.Location = new Point(Test20.MyMainForm.Location.X + Test20.MyMainForm.Width / 2 - partDelete.Width / 2, Test20.MyMainForm.Location.Y + Test20.MyMainForm.Height / 2 - partDelete.Height / 2);
            if (partDelete.ShowDialog() == DialogResult.OK)
            {
                //获取PartDelete窗体中的日期，进行删除操作
                string date = PartDelete.partDelete.tb_date.Text.Trim().ToString();
                string sql = @"delete from APP_CodeCheckRecord20 where CONVERT(varchar(100),checkdate,23)=@date";
                flag = SQLHelperJD.ExecSql(sql, SQLHelperJD.MakeParam("date", date));
                if (flag == true)
                {
                    //调用父窗体中的refresh方法
                    Test20.MyMainForm.refresh();
                    MessageBox.Show("删除成功");
                }
                else
                {
                    return;
                }
            }
            else
            {

            }

        }
    }
}
