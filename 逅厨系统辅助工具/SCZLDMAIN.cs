using FastReport;
using FastReport.Data;
using FastReport.Design;
using FastReport.Utils;
using FastReport.Wizards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class SCZLDMAIN : Form
    {
        List<String> checkNodeList = new List<String>(); //选中的节点集合

        private string parent = "";     //选中节点的父节点名称
        private string parentDir = "";  //报表所在目录文件夹名称
        public static string nodeName = ""; //右击节点是记录的nodename，用于更新报表使用

        /// <summary>
        /// app程序路径和指令单路径，
        /// </summary>
        private static string varFilePath = null;
        private static string appPath = null;


        public SCZLDMAIN()
        {
            InitializeComponent();
            //在treeview节点中显示勾选框
            this.treeView1.CheckBoxes = true;
            this.treeView1.ShowLines = false;
            this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;

            this.treeView1.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView_DrawNode);
            //应用程序所在的目录
            appPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        //隐藏的实现
        private const int TVIF_STATE = 0x8;
        private const int TVIS_STATEIMAGEMASK = 0xF000;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETITEM = TV_FIRST + 63;
        private void HideCheckBox(TreeView tvw, TreeNode node)
        {

            TVITEM tvi = new TVITEM();

            tvi.hItem = node.Handle;

            tvi.mask = TVIF_STATE;

            tvi.stateMask = TVIS_STATEIMAGEMASK;

            tvi.state = 0;

            SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);

        }

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]

        private struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage; public int cChildren; public IntPtr lParam;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]

        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TVITEM lParam);

        //实现委托事件
        private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //隐藏节点前的checkbox
            //if (e.Node.ImageIndex == 6)
            //    HideCheckBox(this.treeView1, e.Node);
            //e.DrawDefault = true;
            if (e.Node.Level < 1)    //如果是根节点，不显示勾选框
                HideCheckBox(this.treeView1, e.Node);
            e.DrawDefault = true;
        }


        //设置子节点状态
        private void SetChildNodeCheckedState(TreeNode currNode, bool isCheckedOrNot)
        {
            if (currNode.Nodes == null) return; //没有子节点返回
            foreach (TreeNode tmpNode in currNode.Nodes)
            {
                tmpNode.Checked = isCheckedOrNot;
                SetChildNodeCheckedState(tmpNode, isCheckedOrNot);
            }
        }


        /*
         * 1、不选中一个节点，则其所有的子节点都不被选中。

            2、选中一个节点，则其所有的子节点都被选中。

            3、当一个节点的所有子节点都没有被选中时，该节点也没有被选中。

            4、当一个节点的所有子节点中有一个被选中时，则该节点也被选中。
          */

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //通过鼠标或者键盘触发事件，防止修改节点的Checked状态时候再次进入
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                SetChildNodeCheckedState(e.Node, e.Node.Checked);
                SetParentNodeCheckedState(e.Node, e.Node.Checked);
            }

        }

        //设置父节点状态
        private void SetParentNodeCheckedState(TreeNode currNode, bool isCheckedOrNot)
        {
            if (currNode.Parent == null) return; //没有父节点返回
            if (isCheckedOrNot) //如果当前节点被选中，则设置所有父节点都被选中
            {
                currNode.Parent.Checked = isCheckedOrNot;
                SetParentNodeCheckedState(currNode.Parent, isCheckedOrNot);
            }
            else //如果当前节点没有被选中，则当其父节点的子节点有一个被选中时，父节点被选中，否则父节点不被选中
            {
                bool checkedFlag = false;
                foreach (TreeNode tmpNode in currNode.Parent.Nodes)
                {
                    if (tmpNode.Checked)
                    {
                        checkedFlag = true;
                        break;
                    }
                }
                currNode.Parent.Checked = checkedFlag;
                SetParentNodeCheckedState(currNode.Parent, checkedFlag);
            }
        }

        //指定目录中的frx集合
        public List<String> fileList(string path)
        {
            List<String> list = new List<String>();

            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileSystemInfo fsi in dir.GetFileSystemInfos())
            {
                if (fsi is FileInfo)
                {
                    FileInfo fi = (FileInfo)fsi;
                    string s = System.IO.Path.GetExtension(fi.FullName);
                    string name = System.IO.Path.GetFileNameWithoutExtension(fi.FullName);
                    if (s == ".frx")
                    {
                        list.Add(name);
                    }
                }
            }
            return list;
        }


        private void SCZLDMAIN_Load(object sender, EventArgs e)
        {
            //在佳之家根节点下动态增加报表节点
            TreeNode tnsjzj = treeView1.Nodes[0].Nodes[0];
            foreach (var item in fileList(@".\SCZLD\佳之家生产指令单"))
            {
                tnsjzj.Nodes.Add(item);
            }
            TreeNode tnsgzt = treeView1.Nodes[0].Nodes[1];
            foreach (var item in fileList(@".\SCZLD\谷之田生产指令单"))
            {
                tnsgzt.Nodes.Add(item);
            }
            TreeNode tnshls = treeView1.Nodes[0].Nodes[2];
            foreach (var item in fileList(@".\SCZLD\好利森生产指令单"))
            {
                tnshls.Nodes.Add(item);
            }
        }

        //更新报表节点，更新服务器报表之后使用
        private void refreshTreeNodes()
        {
            treeView1.Nodes[0].Nodes[0].Nodes.Clear();
            treeView1.Nodes[0].Nodes[1].Nodes.Clear();
            treeView1.Nodes[0].Nodes[2].Nodes.Clear();
            //在佳之家根节点下动态增加报表节点
            TreeNode tnsjzj = treeView1.Nodes[0].Nodes[0];
            foreach (var item in fileList(@".\SCZLD\佳之家生产指令单"))
            {
                tnsjzj.Nodes.Add(item);
            }
            TreeNode tnsgzt = treeView1.Nodes[0].Nodes[1];
            foreach (var item in fileList(@".\SCZLD\谷之田生产指令单"))
            {
                tnsgzt.Nodes.Add(item);
            }
            TreeNode tnshls = treeView1.Nodes[0].Nodes[2];
            foreach (var item in fileList(@".\SCZLD\好利森生产指令单"))
            {
                tnshls.Nodes.Add(item);
            }

        }



        #region "递归TreeView，把所有被选中的节点ID记录到ArrayList中"
        ///递归TreeView，把所有被选中的节点ID记录到ArrayList中
        private void RecursiveTree(TreeView tree)
        {
            TreeNodeCollection nodes = tree.Nodes;
            foreach (TreeNode n in nodes)
            {
                RecursiveNodes(n);
                AddCheckedID(n);
            }

        }

        ///递归TreeView的节点，把所有被选中的节点ID记录到ArrayList中
        private void RecursiveNodes(TreeNode node)
        {

            foreach (TreeNode tn in node.Nodes)
            {
                RecursiveNodes(tn);
                AddCheckedID(tn);
            }

        }

        ///把所有被选中的节点ID和记录到ArrayList中
        private void AddCheckedID(TreeNode node)
        {

            if (node.Checked)
            {
                if (node.Level != 2)
                {

                }
                else
                {
                    checkNodeList.Add(node.Text);
                    parent = node.Parent.Text;
                }
            }
        }
        #endregion

        private void btn_show_Click(object sender, EventArgs e)
        {
            checkNodeList.Clear();
            RecursiveTree(this.treeView1);
            if (checkNodeList.Count == 0)
            {
                MessageBox.Show("未选中任何报表");
            }
            else
            {
                if (parent == "佳之家")
                {
                    parentDir = @".\SCZLD\佳之家生产指令单";
                }
                else
                {
                    MessageBox.Show("该条件不是您选择指令单的筛选条件，请确认！");
                    return;
                }
            

                Report report = new Report();
                foreach (string file in checkNodeList)
                {
                    report.Load(parentDir + @"\" + file + ".frx");
                    report.SetParameterValue("date", dateTimePicker1.Text);
                    report.Prepare(true);
                }
                FastReport.EnvironmentSettings settings = new EnvironmentSettings();  ///显示进度条
                settings.ReportSettings.ShowProgress = true;
                report.ShowPrepared();
                report.Dispose();
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            checkNodeList.Clear();
            RecursiveTree(this.treeView1);
            if (checkNodeList.Count == 0)
            {
                MessageBox.Show("未选中任何报表");
            }
            else
            {
                if (parent == "佳之家")
                {
                    parentDir = @".\佳之家生产指令单";
                }

                Report report = new Report();
                foreach (string file in checkNodeList)
                {
                    report.Load(parentDir + @"\" + file + ".frx");
                    report.SetParameterValue("date", dateTimePicker1.Text);
                    report.Prepare(true);
                }
                report.PrintSettings.ShowDialog = true;
                report.Print();
                report.Dispose();
            }
        }


        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    treeView1.SelectedNode = CurrentNode;//选中这个节点
                    nodeName = CurrentNode.Name;
                    if (nodeName == "JZJ" || nodeName == "GZT" || nodeName == "HLS")
                    {
                        CurrentNode.ContextMenuStrip = ContextMenuStrip1;
                    }
                }
            }
        }

        //下载完报表文件之后更新treeview，先删除本地报表文件，再下载
        private void refresh_Click(object sender, EventArgs e)
        {
            
            DialogResult dr = MessageBox.Show("确认更新报表？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                string sczldFilePath = "";
                varFilePath = appPath; //程序主目录路径

                if (SCZLDMAIN.nodeName == "JZJ")
                {
                    sczldFilePath = @"佳之家生产指令单\";
                }
                else if (SCZLDMAIN.nodeName == "GZT")
                {
                    sczldFilePath = @"谷之田生产指令单\";
                }
                else if (SCZLDMAIN.nodeName == "HLS")
                {
                    sczldFilePath = @"好利森生产指令单\";
                }
                //指令单报表配置文件所在文件夹目录
                varFilePath = varFilePath + @"SCZLD\" + sczldFilePath;
                DirectoryInfo zldFolder = new DirectoryInfo(varFilePath);
                FileInfo[] zldInfos = zldFolder.GetFiles();
                foreach (FileInfo zldInfo in zldInfos)
                {
                    string s = System.IO.Path.GetExtension(zldInfo.FullName);
                    if (s == ".frx")
                    {
                        File.Delete(zldInfo.FullName);
                     }
                }
                SCZLDUpdate sczldUpdate = new SCZLDUpdate();
                sczldUpdate.ShowDialog();
                refreshTreeNodes();
            }
        }

       
    }
}
