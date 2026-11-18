using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    class TabManager
    {
        /// <summary>
        /// 外部引用注意是否会被其它人修改
        /// </summary>
        public TabControl TabControl { get; set; }

        /// <summary>
        /// 初始化，添加TabControl的引用
        /// </summary>
        /// <param name="control"></param>
        public void Init(TabControl control)
        {
            TabControl = control;
        }

        /// <summary>
        /// 当主界面加载后调用
        /// </summary>
        public void MainWindow_Loadd()
        {
            Add();
        }

        /// <summary>
        /// 末尾添加
        /// </summary>
        public void Add()
        {
            // 声明一个字符串变量，用于生成新增选项卡的名称
            string Title = "新增选项卡 " + (TabControl.TabCount + 1).ToString();
            string name= "新增选项卡 " + (TabControl.TabCount + 1).ToString();
            

            // 使用TabControl控件的TabPages 属性的Add方法添加新的选项卡
            TabControl.TabPages.Add(CreateTabPage(name,Title, (Form)Assembly.GetExecutingAssembly().CreateInstance("WS.Note.EditForm")));
            MessageBox.Show("现有" + TabControl.TabCount + "个选项卡");  //获取选项卡个数
        }

        /// <summary>
        /// 末尾添加一个指定标题的TabPage
        /// </summary>
        /// <param name="title">TabPage的标题</param>
        //public void Add(string title)
        //{
        //    Add(title, "WS.Note.EditForm");
        //}

        /// <summary>
        /// 在选项卡列表末尾添加一个TabPage，并填充一个Form
        /// </summary>
        /// <param name="title"></param>
        /// <param name="FormClassName"></param>
        //public void Add(string title, string FormClassName)
        //{
        //    Add(title, (Form)Assembly.GetExecutingAssembly().CreateInstance(FormClassName));
        //}

        /// <summary>
        /// Tabs末尾追加填充Form的TabPage
        /// </summary>
        /// <param name="title"></param>
        /// <param name="form"></param>
        public void Add(string name,string title, Form form)
        {
            TabControl.TabPages.Add(CreateTabPage(name,title, form));
        }

        /// <summary>
        /// 将以Form填充的TabPage添加到Tabs的指定位置
        /// 注1：如果索引为负数，将从末尾向前索引
        /// 注2：如果索引超限，将做取模操作
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="form">Form窗口</param>
        /// <param name="position">添加的位置（插入模式）</param>
        public void Add(string name,string title, Form form, int position)
        {
            // 获取自然数索引（从0开始）
            int index = (position % TabControl.TabCount + TabControl.TabCount) % TabControl.TabCount;

            // 使用TabControl控件的TabPages 属性的Add方法添加新的选项卡
            TabControl.TabPages.Insert(index, CreateTabPage(name,title, form));
        }

        /// <summary>
        /// 删除当前选中的选项卡
        /// </summary>
        public void Remove()
        {
            // 删除时判断是否还存在TabPage
            if (TabControl.SelectedIndex > -1)
            {
                //使用TabControl控件的TabPages属性的Remove方法移除指定的选项卡
                int removeIndex = TabControl.SelectedIndex;
                TabControl.TabPages.Remove(TabControl.SelectedTab);
            }
        }

        /// <summary>
        /// 交换两个索引所在TabPage的位置
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void Swap(int index1, int index2)
        {
            // 参数检查 - 索引是否超限 -TODO 取模操作避免超限
            if (index1 < 0 || index1 >= TabControl.TabPages.Count || index1 < 0 || index1 >= TabControl.TabPages.Count)
            {
                throw new IndexOutOfRangeException("Tab Page Index out of the Range");
            }
            // 交换TabPage
            TabPage page = TabControl.TabPages[index1];
            TabControl.TabPages.RemoveAt(index1);
            TabControl.TabPages.Insert(index2, page);
        }

        /// <summary>
        /// 生成一个新的TabPage，用Form填充
        /// </summary>
        /// <param name="title">Tab Page Title</param>
        /// <param name="form">Form</param>
        /// <returns></returns>
        public static TabPage CreateTabPage(string name,string title, Form form)
        {
            TabPage page = new TabPage
            {
                Name = name,
                Text = title,
                Tag = form.GetType().FullName
            };
            //设置窗体没有边框 加入到选项卡中  
            form.FormBorderStyle = FormBorderStyle.None;
            form.TopLevel = false;
            form.Parent = page;  // 将新生成的Form添加到选择的TabPage里面去
            form.ControlBox = false;
            form.Dock = DockStyle.Fill;
            form.Show();

            return page;
        }
    }
}
