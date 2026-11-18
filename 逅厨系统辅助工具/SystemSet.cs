using Microsoft.Win32;
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
    public partial class SystemSet : Form
    {
        public static SystemSet systemSet;
        SystemSetValuesChangedListener listener;

        public SystemSet()
        {
            InitializeComponent();
           
        }


        private void SystemSet_Load(object sender2, EventArgs e)
        {
            tb_time.Text = Properties.Settings.Default.zizhiRefreshTime.ToString();
            tb_usefuldays.Text = Properties.Settings.Default.usefulDays.ToString();
            //设置资质是否提醒和刷新时间
            if (Properties.Settings.Default.isWarnzizhi == true)
            {
                cb_isWarnzizhi.Checked = true;
                label1.Enabled = true;
                tb_time.Enabled = true;
            }
            else if(Properties.Settings.Default.isWarnzizhi == false)
            {
                cb_isWarnzizhi.Checked = false;
                label1.Enabled = false;
                tb_time.Enabled = false;
            }

            if (Properties.Settings.Default.isautorun == true)
            {
                cb_autorun.Checked = true;
            }
            else if (Properties.Settings.Default.isautorun == false)
            {
                cb_autorun.Checked = false;
            }

            //采购订单提醒相关设置
            if (Properties.Settings.Default.iswrkWarn == true)
            {
                cb_isWarnWrk.Checked = true;
                wrkneedwarddate.Enabled = true;
                wrkneedwarddays.Enabled = true;
                tb_wrkrefreshtime.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
                label6.Enabled = true;
                label7.Enabled = true;
            }
            else if (Properties.Settings.Default.iswrkWarn == false)
            {
                cb_isWarnWrk.Checked = false;
                wrkneedwarddate.Enabled = false;
                wrkneedwarddays.Enabled = false;
                tb_wrkrefreshtime.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;
            }

            wrkneedwarddays.Text = Properties.Settings.Default.wrkdays.ToString();
            wrkneedwarddate.Value = Properties.Settings.Default.notneedWarnDate;
            tb_wrkrefreshtime.Text = Properties.Settings.Default.wrkRefreshTime.ToString();

            btn_save.Enabled = false;

            ///监控是否有值变动，如有变动则保存按钮改为可用
            listener = new SystemSetValuesChangedListener(this);
            listener.ModifedChanged += (sender, arg) => btn_save.Enabled = true;
        }



        private void btn_save_Click(object sender, EventArgs e)
        {
            
                MainForm mainForm = (MainForm)this.Owner;
                if (cb_isWarnzizhi.Checked == true&& int.Parse(tb_time.Text.Trim().ToString()) > 0)
                {
                    Properties.Settings.Default.isWarnzizhi = true;
                    Properties.Settings.Default.zizhiRefreshTime = int.Parse(tb_time.Text.Trim().ToString());
                    Properties.Settings.Default.Save();
                    //MessageBox.Show("保存成功！");
                    mainForm.sysIsGQZZWARN = true;
                    mainForm.sysrefreshTime = int.Parse(tb_time.Text.Trim().ToString());
                    
                }
                else if(cb_isWarnzizhi.Checked == false)
                {
                    Properties.Settings.Default.isWarnzizhi = false;
                    Properties.Settings.Default.Save();
                    //MessageBox.Show("保存成功！");
                    mainForm.sysIsGQZZWARN = false;
                    
                }
                else if(cb_isWarnzizhi.Checked == true && int.Parse(tb_time.Text.Trim().ToString()) == 0)
                { 
                    MessageBox.Show("刷新时间必须大于0");
                    return;
                }

            //MessageBox.Show(System.Environment.CurrentDirectory);
            //程序路径
            string dir = System.Environment.CurrentDirectory;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);//打开注册表子项
            if (key == null)//如果该项不存在的话，则创建该子项
            {
                key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            }
            //保存是否自启动
            if (cb_autorun.Checked == true)
            {
                Properties.Settings.Default.isautorun = true;
                Properties.Settings.Default.Save();
                try
                {
                    key.SetValue("HCsystem", dir + @"\逅厨系统辅助工具.exe");//设置为开机启动
                    key.Close();
                }
                catch
                {
                    
                }
            }
            else if (cb_autorun.Checked == false)
            {
                Properties.Settings.Default.isautorun = false;
                Properties.Settings.Default.Save();
                try
                {
                    key.DeleteValue("HCsystem");//取消开机启动
                    key.Close();
                }
                catch
                {
                    
                }
            }

            if (!tb_usefuldays.Text.Equals(""))
            {
                Properties.Settings.Default.usefulDays = int.Parse(tb_usefuldays.Text.ToString());
                Properties.Settings.Default.Save();
                MyVar.Usefuldays = int.Parse(tb_usefuldays.Text.ToString());
            }
            else
            {
                MessageBox.Show("请填写提前天数");
                return;
            }

            ///采购未入库提醒相关
            if (cb_isWarnWrk.Checked == true && int.Parse(tb_wrkrefreshtime.Text.Trim().ToString()) > 0)
            {
                Properties.Settings.Default.iswrkWarn = true;
                Properties.Settings.Default.wrkdays = int.Parse(wrkneedwarddays.Text.Trim().ToString());
                Properties.Settings.Default.notneedWarnDate = wrkneedwarddate.Value;
                Properties.Settings.Default.wrkRefreshTime= int.Parse(tb_wrkrefreshtime.Text.Trim().ToString());
                Properties.Settings.Default.Save();
                mainForm.sysIsWRKWARN = true;
                mainForm.sysWrkRefreshTime = int.Parse(tb_wrkrefreshtime.Text.Trim().ToString());
                mainForm.wrkdays= int.Parse(wrkneedwarddays.Text.Trim().ToString());
                mainForm.notneedWarnDate= wrkneedwarddate.Value;
            }
            else if (cb_isWarnWrk.Checked == false)
            {
                Properties.Settings.Default.iswrkWarn = false;
                Properties.Settings.Default.Save();
                mainForm.sysIsWRKWARN = false;
            }
            else if (cb_isWarnWrk.Checked == true && int.Parse(tb_wrkrefreshtime.Text.Trim().ToString()) == 0)
            {
                MessageBox.Show("未入库提醒刷新时间必须大于0");
                return;
            }

            MessageBox.Show("保存成功！");
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_isWarnzizhi_CheckedChanged(object sender, EventArgs e)
        {
            
            if (cb_isWarnzizhi.Checked == true)
            {
                tb_time.Enabled = true;
                label1.Enabled = true;
            }
            else {
                tb_time.Enabled = false;
                label1.Enabled = false;
            }
        }

        private void tb_time_KeyPress(object sender, KeyPressEventArgs e)
        {
            //刷新时间只允许输入正数
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
                if (e.KeyChar == '\b') e.Handled = false;
                if (e.KeyChar == '.') e.Handled = true;
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = true;
        }

        private void wrkneedwarddays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = true;
        }

      

        private void cb_isWarnWrk_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_isWarnWrk.Checked == true)
            {
                wrkneedwarddate.Enabled = true;
                wrkneedwarddays.Enabled = true;
                tb_wrkrefreshtime.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
                label6.Enabled = true;
                label7.Enabled = true;
            }
            else
            {
                wrkneedwarddate.Enabled = false;
                wrkneedwarddays.Enabled = false;
                tb_wrkrefreshtime.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;
            }
        }

        private void tb_wrkrefreshtime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = true;
        }
    }
}
