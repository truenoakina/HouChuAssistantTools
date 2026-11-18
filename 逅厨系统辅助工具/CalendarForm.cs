using DevComponents.DotNetBar.Schedule;
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
    public partial class CalendarForm : Form
    {
        public CalendarForm()
        {
            InitializeComponent();
        }

        private void CalendarForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnday_Click(object sender, EventArgs e)
        {
            calendarView1.SelectedView = eCalendarView.Day;
        }

        private void btnmonth_Click(object sender, EventArgs e)
        {
            calendarView1.SelectedView = eCalendarView.Month;
        }

        private void btnyear_Click(object sender, EventArgs e)
        {
            calendarView1.SelectedView = eCalendarView.Year;
        }

        
    }
}
