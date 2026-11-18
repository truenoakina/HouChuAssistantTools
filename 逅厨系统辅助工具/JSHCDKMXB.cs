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
    public partial class JSHCDKMXB : Form
    {
        private DataTable dt; ///datagridview1数据来源
        private int colcount;///datagridview列数
        public JSHCDKMXB()
        {
            InitializeComponent();
        }

        private void JSHCDKMXB_Load(object sender, EventArgs e)
        {
            cb_dep.SelectedIndex = 0;
            this.WindowState = FormWindowState.Maximized;
            begindate.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            enddate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            btn_daochu.Enabled = false;
           
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            
            string proc = @"pDayListHouchuName";
            DataSet ds = SQLHelperYKT.GetDataSetPROC(proc, SQLHelperYKT.MakeParam("dep", cb_dep.Text), SQLHelperYKT.MakeParam("name", tb_name.Text),
                SQLHelperYKT.MakeParam("datebegin", begindate.Value.Date), SQLHelperYKT.MakeParam("dateend", enddate.Value.Date), SQLHelperYKT.MakeParam("user_id", "001"));
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                colcount = dt.Columns.Count;
                dt.Columns.RemoveAt(colcount - 1);
                DataView dataview1 = dt.DefaultView;
                dataview1.Sort = "name,sign_date asc";
                dataGridView1.DataSource = dataview1;
                btn_daochu.Enabled = true;

                for (int i = 0; i < colcount; i++)
                {
                    if (i == 0)
                    {
                        dataGridView1.Columns[i].HeaderCell.Value = "姓名";
                    }
                    else if (i == 1)
                    {
                        dataGridView1.Columns[i].HeaderCell.Value = "打卡日期";
                    }
                    else if (i == 2)
                    {
                        dataGridView1.Columns[i].HeaderCell.Value = "打卡次数";
                    }
                    else if (i >= 3 && i < colcount - 1)
                    {
                        dataGridView1.Columns[i].HeaderCell.Value = (i - 2).ToString();
                    }

                }

                //// 设置表头文本对齐方式
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // 设置表头字体样式
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Bold);
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.RowHeadersWidth = 60;


                //设置dataGridView双buffer代码如下，需要引用反射命名空间
                Type dgvType1 = this.dataGridView1.GetType();
                PropertyInfo pi1 = dgvType1.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi1.SetValue(this.dataGridView1, true, null);
            }
            else 
            {
                dataGridView1.DataSource = null;
                btn_daochu.Enabled = false;
                MessageBox.Show("未查询到数据");
            }

            
            


        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void btn_daochu_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                DateTime datetime = DateTime.Now;
                string fileExt = ".xlsx";
                ExcelHelper.TableToExcelNomal(fbd.DirectoryPath + @"\" +cb_dep.Text+ "考勤数据导出时间" + datetime.ToString("yyyy年MM月dd日HH时mm分"), dt as DataTable, fileExt);
                MessageBox.Show(fbd.DirectoryPath + @"\" + cb_dep.Text + "考勤数据导出时间" + datetime.ToString("yyyy年MM月dd日HH时mm分") + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (fbd.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }
        }
    }
}
