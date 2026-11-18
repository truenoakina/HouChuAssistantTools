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
    public partial class GQZZDETAIL : Form
    {
        public GQZZDETAIL()
        {
            InitializeComponent();
        }

        private void GQZZDETAIL_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GQZZINFO.Dtgysyyzz;
            dataGridView2.DataSource = GQZZINFO.Dtgysjyxkz;
            dataGridView3.DataSource = GQZZINFO.Dtscsyyzz;
            dataGridView4.DataSource = GQZZINFO.Dtscsscxkz;
            dataGridView5.DataSource = GQZZINFO.Dtxjbg;

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.RowHeadersWidth = 60;
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView2.RowHeadersWidth = 60;
            dataGridView3.ReadOnly = true;
            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView3.RowHeadersWidth = 60;
            dataGridView4.ReadOnly = true;
            dataGridView4.AllowUserToAddRows = false;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView4.RowHeadersWidth = 60;
            dataGridView5.ReadOnly = true;
            dataGridView5.AllowUserToAddRows = false;
            dataGridView5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView5.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView5.RowHeadersWidth = 60;

            //设置dataGridView双buffer代码如下，需要引用反射命名空间
            Type dgvType1 = this.dataGridView1.GetType();
            PropertyInfo pi1 = dgvType1.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi1.SetValue(this.dataGridView1, true, null);

            Type dgvType2 = this.dataGridView2.GetType();
            PropertyInfo pi2 = dgvType2.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi2.SetValue(this.dataGridView2, true, null);

            Type dgvType3 = this.dataGridView3.GetType();
            PropertyInfo pi3 = dgvType3.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi3.SetValue(this.dataGridView3, true, null);

            Type dgvType4 = this.dataGridView4.GetType();
            PropertyInfo pi4 = dgvType4.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi4.SetValue(this.dataGridView4, true, null);

            Type dgvType5 = this.dataGridView5.GetType();
            PropertyInfo pi5 = dgvType5.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi5.SetValue(this.dataGridView5, true, null);

            if (GQZZINFO.ChooseNo == 1)
            {
                this.tabControl1.SelectedTab = this.tabPage1;
            }
            else if (GQZZINFO.ChooseNo == 2)
            {
                this.tabControl1.SelectedTab = this.tabPage2;
            }
            else if (GQZZINFO.ChooseNo == 3)
            {
                this.tabControl1.SelectedTab = this.tabPage3;
            }
            else if (GQZZINFO.ChooseNo == 4)
            {
                this.tabControl1.SelectedTab = this.tabPage4;
            }
            else if (GQZZINFO.ChooseNo == 5)
            {
                this.tabControl1.SelectedTab = this.tabPage5;
            }

            this.WindowState = FormWindowState.Maximized;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                  ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = "是否下架 = '是'";
                  ((DataTable)dataGridView2.DataSource).DefaultView.RowFilter = "是否下架 = '是'";
                  ((DataTable)dataGridView3.DataSource).DefaultView.RowFilter = "是否下架 = '是'";
                  ((DataTable)dataGridView4.DataSource).DefaultView.RowFilter = "是否下架 = '是'";
                  ((DataTable)dataGridView5.DataSource).DefaultView.RowFilter = "是否下架 = '是'";
                   break;
                case 2:
                    ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = "是否下架 = '否'";
                    ((DataTable)dataGridView2.DataSource).DefaultView.RowFilter = "是否下架 = '否'";
                    ((DataTable)dataGridView3.DataSource).DefaultView.RowFilter = "是否下架 = '否'";
                    ((DataTable)dataGridView4.DataSource).DefaultView.RowFilter = "是否下架 = '否'";
                    ((DataTable)dataGridView5.DataSource).DefaultView.RowFilter = "是否下架 = '否'";
                    break;
                default:
                    ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = null;
                    ((DataTable)dataGridView2.DataSource).DefaultView.RowFilter = null;
                    ((DataTable)dataGridView3.DataSource).DefaultView.RowFilter = null;
                    ((DataTable)dataGridView4.DataSource).DefaultView.RowFilter = null;
                    ((DataTable)dataGridView5.DataSource).DefaultView.RowFilter = null;
                    break;

            }
            
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void dataGridView2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void dataGridView3_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void dataGridView4_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void dataGridView5_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }
    }
}
