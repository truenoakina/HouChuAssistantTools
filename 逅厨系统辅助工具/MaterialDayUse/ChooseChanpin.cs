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
    public partial class ChooseChanpin : Form
    {
        public List<wlgroup> wlgroups;
        public ChooseChanpin()
        {
            InitializeComponent();
        }


        public class wlgroup
        {
            public int id { get; set; }
            public string name { get; set; }
            public int parentid { get; set; }
        }
        private void ChooseChanpin_Load(object sender, EventArgs e)
        {
            // 清除现有数据
            treeView.Nodes.Clear();
            var rootNode = treeView.Nodes.Add("产成品");
            string sql= @"select g.fid,g.FPARENTID,g.FNUMBER+'('+gl.FNAME+')' as fname from T_BD_MATERIALGROUP g inner join T_BD_MATERIALGROUP_L gl on g.FID=gl.FID
                         where g.FFULLPARENTID like '.100145%' and 1=@data";
            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelper.MakeParam("@data", 1));
            DataTable dt = ds.Tables[0];
            


        }

        // 递归创建节点及子节点
        //private TreeNode CreateTreeNode(Department dept, List<Department> allDepartments)
        //{
        //    var node = new TreeNode(dept.Name);
        //    node.Tag = dept.Id;  // 存储ID供后续使用
        //                         // 查找当前节点的所有直接子节点
        //    var children = allDepartments.Where(d => d.ParentId == dept.Id).ToList();

        //    foreach (var child in children)
        //    {
        //        TreeNode childNode = CreateTreeNode(child, allDepartments);
        //        node.Nodes.Add(childNode);
        //    }

        //    return node;
        //}
    }
}
