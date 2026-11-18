using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //获取欲启动进程名
            string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 1)
            {
                MessageBox.Show("逅厨系统辅助工具已运行，请检查！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login login = new Login(args);
            //Application.Run(new MainForm(args));
            if (login.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainForm(args));
            }
        }
    }
}
