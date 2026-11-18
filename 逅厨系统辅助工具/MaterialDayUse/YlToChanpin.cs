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
    public partial class YlToChanpin : Form
    {
        //原料名称
        private string wlname;
        public string Wlname { get => wlname; set => wlname = value; }
        //原料tag
        private string wltag;
        public string Wltag { get => wltag; set => wltag = value; }
        public YlToChanpin()
        {
            InitializeComponent();
        }

        public void SetTitle(string title,string tag)
        {
            this.Text = title;
            Wlname = title;
            Wltag = tag;
        }

        private void chooseChanpin_Click(object sender, EventArgs e)
        {
            ChooseChanpin chooseChanpin = new ChooseChanpin();
            chooseChanpin.ShowDialog();
        }
    }
}
