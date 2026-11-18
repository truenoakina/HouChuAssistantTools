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
    public partial class OutOrderOrgChoose : Form
    {
        public OutOrderOrgChoose()
        {
            InitializeComponent();
        }

        private void OutOrderOrgChoose_Load(object sender, EventArgs e)
        {
            // 获取屏幕的宽度和高度
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // 设置窗体的位置
            this.StartPosition = FormStartPosition.Manual;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
        }

        private void btn_jiangyin_Click(object sender, EventArgs e)
        {
            OutOrder outOrder = new OutOrder();
            outOrder.ShowDialog();
        }

        private void btn_nanjing_Click(object sender, EventArgs e)
        {
            HLSChangeChoose hLSChangeChoose = new HLSChangeChoose();
            hLSChangeChoose.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GDHCChangeChoose gDHCChangeChoose = new GDHCChangeChoose();
            gDHCChangeChoose.ShowDialog();
        }
    }
}
