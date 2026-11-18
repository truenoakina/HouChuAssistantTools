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
    public partial class GDHCReportMain : Form
    {
        // 存储所有已打开的页面（Key:节点标识, Value:标签页）
        private readonly Dictionary<string, TabPage> openTabs = new Dictionary<string, TabPage>();
        private Color activeTabColor = ColorTranslator.FromHtml("#E0F0FF");
        private Color inactiveTabColor = ColorTranslator.FromHtml("#F0F0F0");
        private string org;//生产组织
        private Point nodeLocation;//第一个节点的位置
        private string tabKey;//选中原料节点时的tag
        private string tabTitle;//选中原料节点时的名称
        public GDHCReportMain()
        {
            InitializeComponent();
            InitializeTreeView();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // 设置默认标签提示
            defaultLabel.Text = "请从左侧树形菜单中选择功能";
            defaultLabel.Dock = DockStyle.Fill;
            defaultLabel.TextAlign = ContentAlignment.MiddleCenter;
            defaultLabel.Font = new Font("微软雅黑", 12, FontStyle.Italic);
            defaultLabel.ForeColor = Color.Gray;
        }

        private void InitializeTreeView()
        {
            // 设置树形控件属性
            treeView.Dock = DockStyle.Fill;
            treeView.AfterSelect += treeView_AfterSelect;

            // 添加演示数据
            PopulateTreeWithSampleData();
            // 获取第一个节点的位置
            TreeNode firstNode = treeView.Nodes[0];
            nodeLocation = treeView.PointToScreen(firstNode.Bounds.Location);

            // 添加上下文菜单（用于维护分组）
            //treeView.ContextMenuStrip = new ContextMenuStrip();
            //treeView.ContextMenuStrip.Items.Add("添加分组", null, (s, e) => AddNewGroup());
            //treeView.ContextMenuStrip.Items.Add("添加子项", null, (s, e) => AddNewChildItem());
            //treeView.ContextMenuStrip.Items.Add("重命名", null, (s, e) => RenameSelectedNode());
            //treeView.ContextMenuStrip.Items.Add("-");
            //treeView.ContextMenuStrip.Items.Add("删除", null, (s, e) => RemoveSelectedNode());
            

        }

        private void PopulateTreeWithSampleData()
        {
            // 清除现有数据
            treeView.Nodes.Clear();
            // 创建根节点
            var ShengChanNode = treeView.Nodes.Add("生产");

            //// 系统管理分组
            //var systemNode = rootNode.Nodes.Add("系统管理");
            //systemNode.ImageIndex = 0;
            //systemNode.Tag = "sys_config";
            //systemNode.Nodes.Add("基本配置").Tag = "sys_basic";
            //systemNode.Nodes.Add("权限设置").Tag = "sys_permission";
            ShengChanNode.Nodes.Add("配料间(按锅)").Tag = "peiliapjian_pot";



            // 展开所有节点
            ShengChanNode.ExpandAll();
            
        }
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null) return;

            tabKey = e.Node.Tag.ToString();
            tabTitle = e.Node.Text;
            // 如果页面已打开则切换到该页面，否则创建新页面
            if (openTabs.ContainsKey(tabKey))
            {
                tabControl.SelectedTab = openTabs[tabKey];
            }
            else
            {
                CreateNewTabPage(tabKey, tabTitle);
            }

            UpdateTabHeadersVisuals();
        }

        private void CreateNewTabPage(string key, string title)
        {
            // 隐藏默认标签
            defaultLabel.Visible = false;
            // 创建新标签页
            var tabPage = new TabPage();
            tabPage.Tag = tabKey;
            tabPage.Text = tabTitle;
            tabControl.TabPages.Add(tabPage);
            tabControl.SelectedTab = tabPage;

            // 创建内容面板
            var contentPanel = new Panel { Dock = DockStyle.Fill };

            
            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                Name = "mainTableLayout",
                Dock = DockStyle.Fill,
                BackColor = SystemColors.ControlLight,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single  // 显示单元格边框
            };
            // 设置为2行7列
            tableLayout.ColumnCount = 7;
            tableLayout.RowCount = 2;
            // 设置列宽：前6列固定宽度，最后一列自动
            int[] columnWidths = { 80, 100, 120, 90, 90, 90, 0 }; // 0表示自动列
            for (int i = 0; i < tableLayout.ColumnCount; i++)
            {
                if (i == tableLayout.ColumnCount - 1) // 最后一列
                    tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                else
                    tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, columnWidths[i]));
            }

            // 设置行高：第一行固定高度，第二行填充剩余空间
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));  // 第一行高度
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // 第二行占满剩余空间

            // 第一行控件
            // 第一列：Label
            Label wlname = new Label
            {
                Text = title,
                Anchor = AnchorStyles.None,
                AutoSize = true
            };
            tableLayout.Controls.Add(wlname, 0, 0);
            // 第二列：label
            Label fdate = new Label
            {
                Text = "日期",
                Anchor = AnchorStyles.None,
                AutoSize = true
            };
            tableLayout.Controls.Add(fdate, 1, 0);
            // 第三列：日期控件
            DateTimePicker datePicker = new DateTimePicker
            {
                Anchor = AnchorStyles.Left,
                Format = DateTimePickerFormat.Short,
                Margin = new Padding(3, 10, 3, 10)
            };
            tableLayout.Controls.Add(datePicker, 2, 0);

            // 第四、五、六列：按钮
            string[] buttonTexts = { "查询", "产品维护", "实际用量维护" };
            for (int i = 3; i <= 5; i++) // 列索引 3,4,5
            {
                Button colButton = new Button
                {
                    Text = buttonTexts[i - 3],
                    Anchor = AnchorStyles.None,
                    Margin = new Padding(3)
                };

                switch (i) 
                {
                    case 3:
                        colButton.Click+= new EventHandler(this.chaxun_Click);
                        break;
                    case 4:
                        colButton.Click += new EventHandler(this.chanpin_Click);
                        break;
                    case 5:
                        colButton.Click += new EventHandler(this.sjyl_Click);
                        break;
                }

                tableLayout.Controls.Add(colButton, i, 0);

            }


            // 第二行：DataGridView（跨7列）
            FastReport.Preview.PreviewControl previewControl = new FastReport.Preview.PreviewControl
            {
                Name = "preview",
                Dock = DockStyle.Fill,
                //BackgroundColor = SystemColors.Window,
                //RowHeadersVisible = false,
                //ReadOnly = true,
                //AllowUserToAddRows = false
            };
            // 设置列跨度为7
            tableLayout.SetColumnSpan(previewControl, 7);
            tableLayout.Controls.Add(previewControl, 0, 1);

            contentPanel.Controls.Add(tableLayout);
            tabPage.Controls.Add(contentPanel);

            // 创建自定义标签头
            var tabHeader = CreateTabHeader(tabPage, title);
            tabsContainer.Controls.Add(tabHeader);

            // 添加到打开的标签字典
            openTabs.Add(key, tabPage);

            // 更新标签头视觉效果
            UpdateTabHeadersVisuals();
        }

        //查询
        private void chaxun_Click(object sender, EventArgs e)
        {
            
        }

        //产品维护
        private void chanpin_Click(object sender, EventArgs e)
        {
            YlToChanpin ylToChanpin = new YlToChanpin();
            ylToChanpin.SetTitle(tabTitle,tabKey);
            ylToChanpin.ShowDialog();
        }

        //实际用量维护
        private void sjyl_Click(object sender, EventArgs e)
        {

        }

        private void AddSampleControls(Panel parent, string title)
        {
            // 示例内容
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("微软雅黑", 16, FontStyle.Bold),
                Location = new Point(30, 20),
                AutoSize = true
            };

            var btnDemo = new Button
            {
                Text = "示例按钮",
                Location = new Point(30, 70),
                Size = new Size(120, 35),
                Font = new Font("微软雅黑", 9),
                BackColor = Color.LightSteelBlue
            };

            var dataView = new DataGridView
            {
                Location = new Point(30, 120),
                Size = new Size(400, 200),
                BackgroundColor = SystemColors.Window,
                RowHeadersVisible = false
            };
            dataView.Columns.Add("ID", "ID");
            dataView.Columns.Add("Name", "名称");
            dataView.Columns.Add("Status", "状态");
            dataView.Rows.Add("001", "示例项目1", "正常");
            dataView.Rows.Add("002", "示例项目2", "警告");

            parent.Controls.Add(lblTitle);
            parent.Controls.Add(btnDemo);
            parent.Controls.Add(dataView);
        }

        private void TabHeader_Click(object sender, EventArgs e)
        {
            // 切换标签页
            var tabPage = (sender as Control)?.Tag as TabPage;
            if (tabPage != null)
            {
                tabControl.SelectedTab = tabPage;
                //选中是重新定义原料名称和tag
                tabTitle = tabControl.SelectedTab.Text;
                tabKey = tabControl.SelectedTab.Tag.ToString();
                UpdateTabHeadersVisuals();
            }
        }
        private void UpdateTabHeadersVisuals()
        {
            // 没有打开的标签页时显示默认标签
            if (tabControl.TabPages.Count == 0)
            {
                defaultLabel.Visible = true;
                return;
            }

            // 更新所有标签头的颜色
            foreach (Panel header in tabsContainer.Controls)
            {
                var tabPage = header.Tag as TabPage;
                header.BackColor = (tabPage == tabControl.SelectedTab)
                    ? activeTabColor
                    : inactiveTabColor;
            }
        }

        private Panel CreateTabHeader(TabPage tabPage, string title)
        {
            // 创建标签头容器
            var tabHeader = new Panel
            {
                Tag = tabPage,
                Size = new Size(160, 35),
                Margin = new Padding(0, 0, 1, 0),
                BackColor = inactiveTabColor,
                Cursor = Cursors.Hand
            };

            // 标题标签
            var lblTitle = new Label
            {
                Text = title,
                Location = new Point(10, 8),
                AutoSize = true,
                Font = new Font("微软雅黑", 9),
                ForeColor = Color.Black,
                Tag = tabPage
            };
            lblTitle.Click += TabHeader_Click;

            // 关闭按钮
            var btnClose = new Button
            {
                Text = "×",
                Size = new Size(25, 25),
                Location = new Point(135, 5),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微软雅黑", 12),
                ForeColor = Color.Gray,
                BackColor = Color.Transparent,
                Tag = tabPage,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#FF8080");
            btnClose.Click += CloseTab_Click;

            // 添加控制到标签头
            tabHeader.Controls.Add(lblTitle);
            tabHeader.Controls.Add(btnClose);

            // 添加点击事件
            tabHeader.Click += TabHeader_Click;

            return tabHeader;
        }

        private void CloseTab_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var tabPage = btn?.Tag as TabPage;

            if (tabPage == null) return;
            // 从tabControl移除
            tabControl.TabPages.Remove(tabPage);

            // 从容器移除标签头
            foreach (Panel header in tabsContainer.Controls)
            {
                if (header.Tag == tabPage)
                {
                    tabsContainer.Controls.Remove(header);
                    break;
                }
            }

            // 从字典中移除
            var keyToRemove = "";
            foreach (var kvp in openTabs)
            {
                if (kvp.Value == tabPage)
                {
                    keyToRemove = kvp.Key;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(keyToRemove))
                openTabs.Remove(keyToRemove);

            // 更新标签页视觉效果
            UpdateTabHeadersVisuals();
        }

        #region 树形分组维护功能
        private void AddNewGroup()
        {
            if (treeView.SelectedNode?.Parent == null) return;

            var newNode = treeView.SelectedNode.Parent.Nodes.Add("新分组");
            newNode.ImageIndex = 3;
            newNode.Tag = $"new_group_{Guid.NewGuid().ToString("N").Substring(0, 6)}";
            treeView.SelectedNode = newNode;
        }
        private void AddNewChildItem()
        {
            if (treeView.SelectedNode == null ) return;
            if(treeView.SelectedNode.Text == "佳之家" || treeView.SelectedNode.Text == "谷之田")
            {
                switch (treeView.SelectedNode.Text)
                {
                    case "佳之家":
                        org = "JZJ";
                        break;
                    case "谷之田":
                        org = "GZT";
                        break;
                    default:
                        org = "null";
                        break;
                }
                using (NodeAdd nodeAdd = new NodeAdd())
                {
                    if (nodeAdd.ShowDialog() == DialogResult.OK)
                    {
                        var newNode = treeView.SelectedNode.Nodes.Add(nodeAdd.Wlname);
                        newNode.Tag = $"{org}_{Guid.NewGuid().ToString("N").Substring(0, 6)}";
                        treeView.SelectedNode.Expand();
                        string sql = @"insert DayUse_material values (@org,@wlname,@tag)";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("org", org), SQLHelper.MakeParam("wlname", nodeAdd.Wlname), SQLHelper.MakeParam("tag", newNode.Tag.ToString()));
                        MsgBox.TipMsgShow(nodeLocation.X  + 80, nodeLocation.Y, this.Text, "添加成功！", this);
                    }
                }
            
            //treeView.SelectedNode = newNode;
            }
        }
        private void RenameSelectedNode()
        {
            if (treeView.SelectedNode == null) return;

            var node = treeView.SelectedNode;
            node.BeginEdit();

            // 结束编辑后更新标签页标题（如果已打开）
            treeView.AfterLabelEdit += (s, e) =>
            {
                if (e.Label != null)
                {
                    UpdateTabTitle(node.Tag?.ToString(), e.Label);
                }
            };
        }
        private void RemoveSelectedNode()
        {
            if (treeView.SelectedNode == null) return;

            var node = treeView.SelectedNode;
            var tag = node.Tag?.ToString();

            // 关闭相关的标签页
            if (tag != null && openTabs.ContainsKey(tag))
            {
                CloseTab_Click(new Button { Tag = openTabs[tag] }, EventArgs.Empty);
            }

            node.Remove();
        }
        private void UpdateTabTitle(string tagKey, string newTitle)
        {
            if (string.IsNullOrEmpty(tagKey) || !openTabs.ContainsKey(tagKey))
                return;

            // 更新标签页的自定义标题
            var tabPage = openTabs[tagKey];
            foreach (Panel header in tabsContainer.Controls)
            {
                if (header.Tag == tabPage)
                {
                    foreach (Control c in header.Controls)
                    {
                        if (c is Label label)
                        {
                            label.Text = newTitle;
                            break;
                        }
                    }
                    break;
                }
            }
        }
        #endregion
    }
}
