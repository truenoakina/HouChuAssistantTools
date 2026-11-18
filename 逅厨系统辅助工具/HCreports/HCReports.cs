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
    public partial class HCReports : Form
    {
        //private Gcscdetail gcscdetail;
        //private Gcschuizong gcschuizong;
        //private GztBushiCL gztbushicl;
        private string nodename, oldnodename;
        private int iLastTabPageIndex; //关闭时的当前选项卡
        

        //设定按钮的大小：
        const int CLOSE_SIZE = 16;

        public HCReports()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer |
                              ControlStyles.UserPaint |
                              ControlStyles.AllPaintingInWmPaint, true);
        }

        //会导致fastreport内嵌报表报错
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
        //        return cp;
        //    }
        //}

        private void HCReports_Load(object sender, EventArgs e)
        {
            //splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
            this.tabControl1.TabPages.Clear();
            this.tabControl1.Padding = new System.Drawing.Point(CLOSE_SIZE, CLOSE_SIZE);
            this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem += new DrawItemEventHandler(this.tabControl1_DrawItem);
            treeView1.SelectedNode = null;

        }


        //在tabControl的MouseDown事件中添加关闭的动作，
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X, y = e.Y;

                //计算关闭区域  
                Rectangle myTabRect = this.tabControl1.GetTabRect(this.tabControl1.SelectedIndex);

                myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE-2), 2);
                myTabRect.Width = CLOSE_SIZE;
                myTabRect.Height = CLOSE_SIZE;

                //如果鼠标在区域内就关闭选项卡  
                bool isClose = x > myTabRect.X && x < myTabRect.Right
                 && y > myTabRect.Y && y < myTabRect.Bottom;

                if (isClose == true)
                {
                    if (tabControl1.TabPages.Count == 1)
                    {
                        this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);
                    }
                    else if (tabControl1.TabPages.Count > 1)
                    {
                        this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);
                        tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                    }
                }
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            #region 重绘标签头=======================
            SolidBrush back;
            SolidBrush white;
            if (e.Index == tabControl1.SelectedIndex)//当前Tab page页的样式
            {
                //背景颜色
                back = new SolidBrush(/*Color.FromArgb(45, 45, 48)*/Color.DarkBlue);
                //字体颜色
                white = new SolidBrush(Color.White);
            }
            else//其余Tab page页的样式
            {
                //背景颜色
                back = new SolidBrush(Color.SeaShell);
                //字体颜色
                white = new SolidBrush(Color.Blue);
            }
            StringFormat sf = new StringFormat()
            {
                //文本水平/垂直居中对齐
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            };

            //绑定选项卡
            Rectangle rec = tabControl1.GetTabRect(e.Index);
            //设置选项卡背景
            e.Graphics.FillRectangle(back, rec);

            //设置选项卡字体及颜色
            e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, new Font("微软雅黑", 8, FontStyle.Bold), white, rec, sf);
            #endregion

            //选项卡标题右侧增加关闭按钮
            TabPage thisTab = tabControl1.TabPages[e.Index];
            //Draw Close button
            if (e.State == DrawItemState.Selected)
            {
                Point closeLoc = new Point(12, 2);
                e.Graphics.DrawRectangle(Pens.LightBlue, e.Bounds.Right - closeLoc.X, e.Bounds.Top + closeLoc.Y, 10, 15);
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds.Right - closeLoc.X, e.Bounds.Top + closeLoc.Y, 10, 15);
                e.Graphics.DrawString("X", e.Font, Brushes.Black, e.Bounds.Right - (closeLoc.X), e.Bounds.Top + closeLoc.Y);
                // Draw String of Caption
                //e.Graphics.DrawString("", e.Font, Brushes.Black, e.Bounds.Left + 28, e.Bounds.Top + 4);
                // Add Icon to Front
                e.DrawFocusRectangle();
            }
            else
            {
                Point closeLoc = new Point(12, 2);
                e.Graphics.DrawRectangle(Pens.Black, e.Bounds.Right - closeLoc.X, e.Bounds.Top + closeLoc.Y, 10, 12);
                e.Graphics.FillRectangle(Brushes.Black, e.Bounds.Right - closeLoc.X, e.Bounds.Top + closeLoc.Y, 10, 12);
                e.Graphics.DrawString("X", e.Font, Brushes.White, e.Bounds.Right - (closeLoc.X), e.Bounds.Top + closeLoc.Y);
                // Draw String of Caption
                //e.Graphics.DrawString("", e.Font, Brushes.Black, e.Bounds.Left + 28, e.Bounds.Top + 4);
                // Add Icon to Front
                e.DrawFocusRectangle();
            }
        }

        

        private void tabControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TabManager tabmanager = new TabManager();
            tabmanager.TabControl = tabControl1;
            TreeView tr = (TreeView)sender;
            if (!tr.HasChildren)
            {
                nodename = tr.SelectedNode.Name;
                switch (nodename)
                {
                    case "gcscdetail":
                        Gcscdetail gcscdetail = new Gcscdetail();
                        tabmanager.Add(nodename, "每日产量明细", gcscdetail);
                        tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                        treeView1.SelectedNode = null;
                        break;
                    case "gcschuizong":
                        //splitContainer1.Panel2.Controls.Clear();
                        //Gcschuizong gcschuizong = new Gcschuizong();
                        //this.IsMdiContainer = true;//指定为父窗体
                        //gcschuizong.MdiParent = this;//指定子窗体的父窗体
                        //gcschuizong.Parent = this.splitContainer1.Panel2;//指定父容器
                        //gcschuizong.Show();
                        //oldnodename = nodename;
                        Gcschuizong gcschuizong = new Gcschuizong();
                        tabmanager.Add(nodename, "每日产量汇总", gcschuizong);
                        tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                        treeView1.SelectedNode = null;
                        break;
                    case "gztbushicl":
                        GztBushiCL gztbushicl = new GztBushiCL();
                        tabmanager.Add(nodename, "谷之田各部室产量", gztbushicl);
                        tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                        treeView1.SelectedNode = null;
                        break;
                }

            }
            else
            {
                return;
            }
        }

       



    }
}
