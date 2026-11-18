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
    public partial class GDHCChangeChoose : Form
    {
        public GDHCChangeChoose()
        {
            InitializeComponent();
        }

        private void GDHCChangeChoose_Load(object sender, EventArgs e)
        {
            // 获取屏幕的宽度和高度
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // 设置窗体的位置
            this.StartPosition = FormStartPosition.Manual;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
        }

        private void btn_woermaDD_Click(object sender, EventArgs e)
        {
            OutOrderGDHC outOrderGDHC = new OutOrderGDHC();
            outOrderGDHC.ShowDialog();
        }

        private void btn_woermaSHD_Click(object sender, EventArgs e)
        {
            MyVar.WemshdType = 0; //单订单
            OutSongHuoDanGDHC outSongHuoDanGDHC = new OutSongHuoDanGDHC();
            outSongHuoDanGDHC.ShowDialog();
            
        }

        private void WEMSHDDDD_Click(object sender, EventArgs e)
        {
            MyVar.WemshdType = 1; //多订单
            OutSongHuoDanGDHCDDD outSongHuoDanGDHCDDD = new OutSongHuoDanGDHCDDD();
            outSongHuoDanGDHCDDD.ShowDialog();
            
        }

        private void ll_WEM1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            WEM1 wEM1 = new WEM1();
            wEM1.StartPosition = FormStartPosition.CenterParent;
            wEM1.ShowDialog();
        }

        private void ll_WEM2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WEM2 wEM2 = new WEM2();
            wEM2.StartPosition = FormStartPosition.CenterParent;
            wEM2.ShowDialog();
        }

        private void ll_WEM3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WEM3 wEM3 = new WEM3();
            wEM3.StartPosition = FormStartPosition.CenterParent;
            wEM3.ShowDialog();
        }

        private void WEMSHDDDD2_Click(object sender, EventArgs e)
        {
            MyVar.WemshdType = 2; //多订单二
            OutSongHuoDanGDHCDDD2 outSongHuoDanGDHCDDD2 = new OutSongHuoDanGDHCDDD2();
            outSongHuoDanGDHCDDD2.ShowDialog();
        }
    }
}
