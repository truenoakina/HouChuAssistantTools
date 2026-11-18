using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class PrinterChoose : Form
    {
        public static PrinterChoose printerChoose;
        

        public PrinterChoose()
        {
            InitializeComponent();
            printerChoose = this;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void PrinterChoose_Load(object sender, EventArgs e)
        {
            cb_printers.DropDownStyle = ComboBoxStyle.DropDownList;
            List<string> printers = new List<string>();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }
            cb_printers.DataSource = printers;
            PrintDocument pd = new PrintDocument();
            cb_printers.Text = pd.PrinterSettings.PrinterName;
        }
    }
}
