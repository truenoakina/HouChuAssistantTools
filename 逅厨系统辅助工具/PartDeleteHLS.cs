using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace 逅厨系统辅助工具
{
    public partial class PartDeleteHLS : Form
    {
        public static PartDeleteHLS partDeletehls;

        public PartDeleteHLS()
        {
            InitializeComponent();
            partDeletehls = this;
            tb_date.Text = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM", DateTimeFormatInfo.InvariantInfo) + "-" + DateTime.Now.ToString("dd", DateTimeFormatInfo.InvariantInfo);//2021-01-01
        }
    }
}
