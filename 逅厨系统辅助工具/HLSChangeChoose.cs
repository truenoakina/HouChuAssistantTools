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
    public partial class HLSChangeChoose : Form
    {
        public HLSChangeChoose()
        {
            InitializeComponent();
        }

        private void HLSChangeChoose_Load(object sender, EventArgs e)
        {
            // 获取屏幕的宽度和高度
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // 设置窗体的位置
            this.StartPosition = FormStartPosition.Manual;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
        }

        private void btn_outOrder_Click(object sender, EventArgs e)
        {
            OutOrderHLS outOrderHLS = new OutOrderHLS();
            outOrderHLS.ShowDialog();
        }

        private void btn_DingDongorder_Click(object sender, EventArgs e)
        {
            DingDongOrderImport dingDongOrderImport = new DingDongOrderImport();
            dingDongOrderImport.ShowDialog();
        }
    }
}
