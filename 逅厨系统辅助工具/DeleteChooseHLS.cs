using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class DeleteChooseHLS : Form
    {
        private bool flag = false;

        public DeleteChooseHLS()
        {
            InitializeComponent();
        }

        private void btn_AllDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定全部删除吗？", "提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string sql = @"delete from APP_CodeCheckRecord20HLS where 1=@data";
                flag=SQLHelper.ExecSql(sql, SQLHelper.MakeParam("data", 1));
                if (flag == true)
                {
                    //调用父窗体中的refresh方法
                    Test20HLS.MyMainForm.refresh();
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
            PartDeleteHLS partDeletehls = new PartDeleteHLS();
            //在父窗体中间弹出新窗口
            partDeletehls.StartPosition = FormStartPosition.Manual;
            partDeletehls.Location = new Point(Test20HLS.MyMainForm.Location.X + Test20HLS.MyMainForm.Width / 2 - partDeletehls.Width / 2, Test20HLS.MyMainForm.Location.Y + Test20HLS.MyMainForm.Height / 2 - partDeletehls.Height / 2);
            if (partDeletehls.ShowDialog() == DialogResult.OK)
            {
                //获取PartDelete窗体中的日期，进行删除操作
                string date = PartDeleteHLS.partDeletehls.tb_date.Text.Trim().ToString();
                string sql = @"delete from APP_CodeCheckRecord20HLS where CONVERT(varchar(100),checkdate,23)=@date";
                flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("date", date));
                if (flag == true)
                {
                    //调用父窗体中的refresh方法
                    Test20HLS.MyMainForm.refresh();
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
