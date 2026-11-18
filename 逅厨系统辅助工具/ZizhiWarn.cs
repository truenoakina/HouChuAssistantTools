using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class ZizhiWarn : Form
    {

        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);


        //下面是可用的常量,按照不合的动画结果声明本身须要的
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口,该标记可以在迁移转变动画和滑动动画中应用。应用AW_CENTER标记时忽视该标记该标记
        private const int AW_CENTER = 0x0010;//若应用了AW_HIDE标记,则使窗口向内重叠;不然向外扩大
        private const int AW_HIDE = 0x10000;//隐蔽窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口,在应用了AW_HIDE标记后不要应用这个标记
        private const int AW_SLIDE = 0x40000;//应用滑动类型动画结果,默认为迁移转变动画类型,当应用AW_CENTER标记时,这个标记就被忽视
        private const int AW_BLEND = 0x80000;//应用淡入淡出结果


        public ZizhiWarn()
        {
            InitializeComponent();
        }

        private void ZizhiWarn_Load(object sender, EventArgs e)
        {
            lbl_gysyyzz.Text = GQZZINFO.GysyyzzNum.ToString();
            lbl_gysjyxkz.Text = GQZZINFO.GysjyxkzNum.ToString();
            lbl_scsyyzz.Text = GQZZINFO.ScsyyzzNum.ToString();
            lbl_scsscxkz.Text = GQZZINFO.ScsscxkzNum.ToString();
            lbl_xjbg.Text = GQZZINFO.XjbgNum.ToString();

            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_VER_NEGATIVE);

        }

        private void ZizhiWarn_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
            this.Dispose();
        }

        private void llbl_gysyyzz_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GQZZINFO.ChooseNo = 1;
            GQZZDETAIL gQZZDETAIL = new GQZZDETAIL();
            gQZZDETAIL.ShowDialog();
            
        }

        private void llbl_gysjyxkz_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GQZZINFO.ChooseNo = 2;
            GQZZDETAIL gQZZDETAIL = new GQZZDETAIL();
            gQZZDETAIL.ShowDialog();
        }

        private void llbl_scsyyzz_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GQZZINFO.ChooseNo = 3;
            GQZZDETAIL gQZZDETAIL = new GQZZDETAIL();
            gQZZDETAIL.ShowDialog();
        }

        private void llbl_scsscxkz_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GQZZINFO.ChooseNo = 4;
            GQZZDETAIL gQZZDETAIL = new GQZZDETAIL();
            gQZZDETAIL.ShowDialog();
        }

        private void llbl_xjbg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GQZZINFO.ChooseNo = 5;
            GQZZDETAIL gQZZDETAIL = new GQZZDETAIL();
            gQZZDETAIL.ShowDialog();
        }
    }
}
