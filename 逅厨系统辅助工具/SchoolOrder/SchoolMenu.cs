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
    public partial class SchoolMenu : Form
    {
        public SchoolMenu()
        {
            InitializeComponent();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            SchoolOrder schoolOrder = new SchoolOrder();
            schoolOrder.ShowDialog();
        }

        private void SchoolMenu_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2;//桌面的宽度的一半减去自身宽的的一半
            this.Top = Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2;
        }

        private void btn_caigouhz_Click(object sender, EventArgs e)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            purchaseOrder.ShowDialog();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            LabelPrint labelPrint = new LabelPrint();
            labelPrint.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaleData saleData = new SaleData();
            saleData.ShowDialog();
        }
    }
}
