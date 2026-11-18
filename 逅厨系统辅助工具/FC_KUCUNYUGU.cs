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
    public partial class FC_KUCUNYUGU : Form
    {
        string org;
        DateTime begindate,enddate;
        

        public FC_KUCUNYUGU()
        {
            InitializeComponent();
        }

        private void FC_KUCUNYUGU_Load(object sender, EventArgs e)
        {
            cb_org.SelectedIndex = 0;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;  //设置列标题不换行
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddDays(10);
            this.dataGridView2.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;  //设置列标题不换行
           
        }

        //添加行号
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }


        //设置datetimepick2的最大日期为datetimepick1的10天后
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddDays(9);
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            string proc = "DDYLXUQIUMX";
            org = cb_org.Text;
            begindate = dateTimePicker1.Value.Date;
            enddate = dateTimePicker2.Value.Date;
            DataSet ds = SQLHelper.GetDataSetPROC(proc, SQLHelper.MakeParam("@org", org), SQLHelper.MakeParam("@begindate", begindate), SQLHelper.MakeParam("@enddate", enddate));
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

            string proc2 = "TCKUCUNYUGU";
            ds = SQLHelper.GetDataSetPROC(proc2, SQLHelper.MakeParam("@org", org), SQLHelper.MakeParam("@begindate", begindate), SQLHelper.MakeParam("@enddate", enddate));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView2.DataSource = ds.Tables[0];
                for (int i = 0; i < this.dataGridView2.Columns.Count; i++)
                {
                    //将每一列都调整为自动适应模式
                    this.dataGridView2.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                }
                dataGridView2.AllowUserToAddRows = false;
            }
            else
            {
                dataGridView2.DataSource = null;
                MsgBox.TipMsgShow(dataGridView2.Location.X + dataGridView2.Width / 2, dataGridView2.Location.Y + dataGridView2.Height / 2, this.Text, "当前查询条件未查到符合的数据！", this);
                return;
            }

        }
    }
}
