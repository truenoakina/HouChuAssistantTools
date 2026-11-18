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
    public partial class FC_PRODUCR_INFO : Form
    {
        public FC_PRODUCR_INFO()
        {
            InitializeComponent();
        }

        private void btn_yltype_Click(object sender, EventArgs e)
        {
            FC_YLTYPE fC_YLTYPE = new FC_YLTYPE();
            fC_YLTYPE.ShowDialog();
        }

        private void btn_supplier_Click(object sender, EventArgs e)
        {
            FC_SUPPLIER fC_SUPPLIER = new FC_SUPPLIER();
            fC_SUPPLIER.ShowDialog();
        }

        private void btn_yuanliao_Click(object sender, EventArgs e)
        {
            FC_YL fC_YL = new FC_YL();
            fC_YL.ShowDialog();
        }

        private void btn_caiyao_Click(object sender, EventArgs e)
        {
            FC_CAIYAO fC_CAIYAO = new FC_CAIYAO();
            fC_CAIYAO.ShowDialog();
        }

        private void btn_caiyaobom_Click(object sender, EventArgs e)
        {
            FC_BOM fC_BOM = new FC_BOM();
            fC_BOM.ShowDialog();
        }

        private void btn_kucun_Click(object sender, EventArgs e)
        {
            FC_KUCUNINFO fC_KUCUNINFO = new FC_KUCUNINFO();
            fC_KUCUNINFO.ShowDialog();
        }

        private void btn_order_Click(object sender, EventArgs e)
        {
            FC_ORDER fC_ORDER = new FC_ORDER();
            fC_ORDER.ShowDialog();
        }

        private void btn_kucunyugu_Click(object sender, EventArgs e)
        {
            FC_KUCUNYUGU fC_KUCUNYUGU = new FC_KUCUNYUGU();
            fC_KUCUNYUGU.ShowDialog();
        }
    }
}
