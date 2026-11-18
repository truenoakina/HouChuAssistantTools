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
    public partial class Level1BOM : Form
    {

        private DataTable dt;
        private String FName;

        public Level1BOM()
        {
            
            InitializeComponent();
        }

        private void Level1BOM_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString().Trim() == "")
            {
                return;
            }
            string proc = "BOMdiceng2";
            FName = textBox1.Text.ToString();
            DataSet ds = SQLHelperJD.GetDataSetPROC(proc, SQLHelperJD.MakeParam("@FNAME", FName));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                {
                    //将每一列都调整为自动适应模式
                    this.dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                }
                dataGridView1.AllowUserToAddRows = false;
            }
            else
            {
                dataGridView1.DataSource = null;
                MsgBox.TipMsgShow(dataGridView1.Location.X + dataGridView1.Width / 2, dataGridView1.Location.Y + dataGridView1.Height / 2, this.Text, "当前查询条件未查到符合的数据！", this);
                return;
            }


        }

        private void btn_out_Click(object sender, EventArgs e)
        {
            dt = dataGridView1.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有要导出的数据，请检查！");
                return;
            }
            else
            {
                string fileExt = ".xlsx";
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    ExcelHelper.TableToExcelBOM(fbd.DirectoryPath + @"\" + FName + "一阶BOM", dt as DataTable, fileExt);
                    MessageBox.Show(fbd.DirectoryPath + @"\" + FName + "一阶BOM" + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (fbd.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }
            }
        }
    }
}
