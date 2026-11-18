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
    public partial class FC_ORDER : Form
    {
        int ddid; //订单ID
        string billno;

        public FC_ORDER()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            FC_ORDER_ADD fC_ORDER_ADD = new FC_ORDER_ADD();
            fC_ORDER_ADD.ShowDialog();
        }

        private void refresh() {
            string sql = @"select o.id,od.id,o.FDATE,o.FBILLNO,od.fcaiyao,od.fqty from FC_order o inner join FC_orderdetail od on o.ID=od.OID where o.forg=@forg 
               and o.fdate>=@begindate and o.fdate<=@enddate order by o.FBILLNO";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("forg", cb_org.Text), SQLHelper.MakeParam("begindate", begindate.Value.Date.ToShortDateString()), SQLHelper.MakeParam("enddate", enddate.Value.Date.ToShortDateString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].DataPropertyName = ds.Tables[0].Columns[0].ToString();
                dataGridView1.Columns[1].DataPropertyName = ds.Tables[0].Columns[1].ToString();
                dataGridView1.Columns[2].DataPropertyName = ds.Tables[0].Columns[2].ToString();
                dataGridView1.Columns[3].DataPropertyName = ds.Tables[0].Columns[3].ToString();
                dataGridView1.Columns[4].DataPropertyName = ds.Tables[0].Columns[4].ToString();
                dataGridView1.Columns[5].DataPropertyName = ds.Tables[0].Columns[5].ToString();
            }

        }

        private void FC_ORDER_Load(object sender, EventArgs e)
        {
            cb_org.SelectedIndex = 0;
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 140;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width =100;
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (dataGridView1.CurrentCell.RowIndex >= 0)
            {
                int foid, fodid;//foid主表id，fodid 子表id
                foid = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value);
                fodid = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value);
                FC_ORDER_CHANGE fC_ORDER_CHANGE = new FC_ORDER_CHANGE(foid,fodid);
                fC_ORDER_CHANGE.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                //MessageBox.Show(e.RowIndex.ToString() + e.ColumnIndex.ToString());
                if (e.RowIndex > -1 & e.ColumnIndex == -1)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                    //dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    ddid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    billno = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                }
                else
                {
                    return;
                }

            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认删除编号为‘"+billno+"’的销售订单吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                string sql = @"delete from FC_ORDER where id=@id";
                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("id", ddid));
                sql= @"delete from FC_ORDERDETAIL where oid=@oid";
                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("oid", ddid));
                MessageBox.Show("删除成功");
                refresh();
            }
        }

       
    }
    
}
