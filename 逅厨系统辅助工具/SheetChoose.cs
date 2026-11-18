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
    public partial class SheetChoose : Form
    {

        public static SheetChoose sheetChoose;//静态实例化对象
        public SheetChoose()
        {
            InitializeComponent();
            sheetChoose = this;
        }

        private void SheetChoose_Load(object sender, EventArgs e)
        {
            if (MyVar.WemshdType == 0)
            {
                cb_sheets.DataSource = OutSongHuoDanGDHC.outSongHuoDanGDHC.sheets;
            }
            else if (MyVar.WemshdType == 1)
            {
                cb_sheets.DataSource = OutSongHuoDanGDHCDDD.outSongHuoDanGDHCDDD.sheets;
            }
            else if (MyVar.WemshdType == 2)
            {
                cb_sheets.DataSource = OutSongHuoDanGDHCDDD2.outSongHuoDanGDHCDDD2.sheets;
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            MyVar.Sheetname = cb_sheets.SelectedItem.ToString();
        }
    }
}
