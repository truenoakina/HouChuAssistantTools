using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;



namespace 逅厨系统辅助工具
{
    public partial class HRKQSJFORMAT : Form
    {
        /// <summary>
        /// 提示文件是非被占用
        /// </summary>
        /// <param name="lpPathName"></param>
        /// <param name="iReadWrite"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public readonly IntPtr HFILE_ERROR = new IntPtr(-1);


        DataTable KQtb2 = new DataTable("kqtb2");////最终转换完毕的考勤记录datatable

        public HRKQSJFORMAT()
        {
            InitializeComponent();
            label1.Text = "转换进度";
            label2.Text = "转换进度";
            btn_save.Enabled = false;
        }

       
        private void btn_File_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            //dialog.Filter = "所有文件(*.*)|*.*";
            dialog.Filter = "excel文件(*.xlsx,*.xls)|*.xlsx;*.xls";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ///判断文件是非打开
                IntPtr vHandle = _lopen(dialog.FileName, OF_READWRITE | OF_SHARE_DENY_NONE);
                if (vHandle == HFILE_ERROR)
                {
                    MessageBox.Show("文件被占用！请关闭后再操作");
                    return;
                }else
                {
                    CloseHandle(vHandle);
                    try
                    {
                        KQtb2 = new DataTable("kqtb2");
                        this.progressBar1.Value = 0;
                        this.progressBar2.Value = 0;
                        label1.Text = "转换进度";
                        label2.Text = "转换进度";
                        ///进行第一次转换，去除打卡间隔为1分钟的打卡时间，并且将打卡时间拆分成日期和时间
                        //将excel导入至datatable
                        DataTable tb=NPOIOprateExcel.ExcelToDataTable(dialog.FileName,true);
                        //string a = tb.Rows.Count.ToString();
                        //ExcelHelper.TableToExcel(tb, @"D:/人力资源/江阴南京考勤表格/aaa.xlsx");
                        ////时间差
                        //double diff = DateDiff(Convert.ToDateTime(tb.Rows[6][2]), Convert.ToDateTime(tb.Rows[7][2]));
                        //MessageBox.Show(diff.ToString());
                        //将原始表格优化，去掉间隔时间短的考勤记录
                        DataTable KQtb = new DataTable("kqtb");
                        DataColumn dc = null;
                        dc = KQtb.Columns.Add("ID", Type.GetType("System.Int32"));
                        dc.AutoIncrement = true;//自动增加
                        dc.AutoIncrementSeed = 1;//起始为1
                        dc.AutoIncrementStep = 1;//步长为1
                        dc.AllowDBNull = false;//
                        dc = KQtb.Columns.Add("Name", Type.GetType("System.String"));
                        dc = KQtb.Columns.Add("Org", Type.GetType("System.String"));
                        dc = KQtb.Columns.Add("KQdate", Type.GetType("System.String"));
                        dc = KQtb.Columns.Add("KQtime", Type.GetType("System.String"));
                        DataRow newRow;
                        int tbrows = tb.Rows.Count;///导入的表格数据行数
                        this.progressBar1.Maximum = tbrows;///进度条1的最高行数
                        string kqtimeold = "2021-05-01 08:20:04";
                        string kqtime = "";
                        for (int i = 0; i < tbrows; i++)
                        {
                            kqtime = tb.Rows[i][2].ToString();
                            this.progressBar1.Minimum = 0;
                            double diff = DateDiff(Convert.ToDateTime(kqtimeold), Convert.ToDateTime(kqtime));
                            
                            if (diff > 60)
                            {
                                newRow = KQtb.NewRow();
                                newRow["Name"] = tb.Rows[i][0];
                                newRow["Org"] = tb.Rows[i][1];
                                newRow["KQdate"] = Convert.ToDateTime(tb.Rows[i][2]).ToShortDateString();
                                newRow["KQtime"] = Convert.ToDateTime(tb.Rows[i][2]).ToShortTimeString();
                                KQtb.Rows.Add(newRow);
                                kqtimeold = tb.Rows[i][2].ToString();
                            }
                            else
                            {
                                continue;
                            }
                            this.progressBar1.Value = i;
                            label1.Text = "转换进度:   " + i.ToString() + "   /  总行数" + tbrows.ToString();
                            Application.DoEvents();
                            
                        }
                        //保证进度条走完。暂时不明为什么startbyte不等于allbyte
                        this.progressBar1.Value = tbrows;
                        label1.Text = "转换进度:   " + tbrows.ToString() + "   /  总行数" + tbrows.ToString() + " 转换完毕";


                        //进行二次转换，将相同日期，不同打卡时间的放在一行中
                        
                        if (KQtb.Rows.Count > 0)
                        {
                            int k = 0;///同一天的考勤次数
                            int j = -1;///KQtb2的行数
                            
                            DataColumn dc2 = null;
                            dc2 = KQtb2.Columns.Add("Name", Type.GetType("System.String"));
                            dc2 = KQtb2.Columns.Add("Org", Type.GetType("System.String"));
                            dc2 = KQtb2.Columns.Add("KQdate", Type.GetType("System.String"));
                            dc2 = KQtb2.Columns.Add("KQtime1", Type.GetType("System.String"));
                            dc2 = KQtb2.Columns.Add("KQtime2", Type.GetType("System.String"));
                            dc2 = KQtb2.Columns.Add("KQtime3", Type.GetType("System.String"));
                            dc2 = KQtb2.Columns.Add("KQtime4", Type.GetType("System.String"));
                            dc2 = KQtb2.Columns.Add("KQtime5", Type.GetType("System.String"));
                            dc2 = KQtb2.Columns.Add("KQtime6", Type.GetType("System.String"));
                            dc2 = KQtb2.Columns.Add("KQtime7", Type.GetType("System.String"));
                            DataRow newRow2;
                            int KQtbRows= KQtb.Rows.Count;///第一次转换完的表格数据行数
                            this.progressBar2.Maximum = KQtbRows;///进度条2的最高行数
                            string nameold = "";
                            string dateold = "";
                            for (int i = 0; i < KQtbRows; i++)
                            {
                                string name = KQtb.Rows[i][1].ToString();
                                string date= KQtb.Rows[i][3].ToString();
                                string time= KQtb.Rows[i][4].ToString();
                                this.progressBar2.Minimum = 0;
                                if (name != nameold)
                                {
                                    newRow2 = KQtb2.NewRow();
                                    newRow2["Name"] = KQtb.Rows[i][1];
                                    newRow2["Org"] = KQtb.Rows[i][2];
                                    newRow2["KQDate"] = KQtb.Rows[i][3];
                                    newRow2["KQtime1"] = KQtb.Rows[i][4];
                                    newRow2["KQtime2"] = "";
                                    newRow2["KQtime3"] = "";
                                    newRow2["KQtime4"] = "";
                                    newRow2["KQtime5"] = "";
                                    newRow2["KQtime6"] = "";
                                    newRow2["KQtime7"] = "";
                                    KQtb2.Rows.Add(newRow2);
                                    nameold = name;
                                    dateold = date;
                                    j++;
                                    k = 1;
                                }
                                else if (name == nameold & date == dateold) ////名字一样，日期一样
                                {
                                    k++;
                                    if (k == 2)
                                    {
                                        KQtb2.Rows[j]["KQtime2"] = KQtb.Rows[i][4].ToString();
                                    }
                                    else if (k == 3)
                                    {
                                        KQtb2.Rows[j]["KQtime3"] = KQtb.Rows[i][4].ToString();
                                    }
                                    else if (k == 4)
                                    {
                                        KQtb2.Rows[j]["KQtime4"] = KQtb.Rows[i][4].ToString();
                                    }
                                    else if (k == 5)
                                    {
                                        KQtb2.Rows[j]["KQtime5"] = KQtb.Rows[i][4].ToString();
                                    }
                                    else if (k == 6)
                                    {
                                        KQtb2.Rows[j]["KQtime6"] = KQtb.Rows[i][4].ToString();
                                    }
                                    else if (k == 7)
                                    {
                                        KQtb2.Rows[j]["KQtime7"] = KQtb.Rows[i][4].ToString();
                                    }
                                }
                                else if (name == nameold & date != dateold) ////名字一样，日期不一样
                                {
                                    newRow2 = KQtb2.NewRow();
                                    newRow2["Name"] = KQtb.Rows[i][1];
                                    newRow2["Org"] = KQtb.Rows[i][2];
                                    newRow2["KQDate"] = KQtb.Rows[i][3];
                                    newRow2["KQtime1"] = KQtb.Rows[i][4];
                                    newRow2["KQtime2"] = "";
                                    newRow2["KQtime3"] = "";
                                    newRow2["KQtime4"] = "";
                                    newRow2["KQtime5"] = "";
                                    newRow2["KQtime6"] = "";
                                    newRow2["KQtime7"] = "";
                                    KQtb2.Rows.Add(newRow2);
                                    nameold = name;
                                    dateold = date;
                                    j++;
                                    k = 1;
                                }
                                this.progressBar2.Value = i;
                                label2.Text = "转换进度:   " + i.ToString() + "   /  总行数" + KQtbRows.ToString();
                                Application.DoEvents();
                            }
                            //保证进度条走完。暂时不明为什么startbyte不等于allbyte
                            this.progressBar2.Value = KQtbRows;
                            label2.Text = "转换进度:   " + KQtbRows.ToString() + "   /  总行数" + KQtbRows.ToString()+" 转换完毕,可导出保存";
                            btn_save.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("无转换数据");
                        }
                    

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("上传文档不符合要求，请检查");
                        return;
                    }
                }
            }
            else
            {
                return;
            }
        }


        /// 已重载.计算两个日期的时间间隔,返回的是时间间隔的日期差的绝对值.
        /// </summary>
        /// <param name="DateTime1">第一个日期和时间</param>
        /// <param name="DateTime2">第二个日期和时间</param>
        /// <returns></returns>
        private double DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            double dateDiff = 0;
            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                //dateDiff = ts.Days.ToString() + "天"
                //        + ts.Hours.ToString() + "小时"
                //        + ts.Minutes.ToString() + "分钟"
                //        + ts.Seconds.ToString() + "秒";
                ///时间间隔秒数
                dateDiff = ts.TotalSeconds;
            }
            catch
            {

            }
            return dateDiff;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string fileExt = ".xlsx";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                ExcelHelper.TableToExcel(KQtb2, fbd.DirectoryPath + @"\" + "考勤数据" + DateTime.Now.ToString("yyyy-MM-dd") + fileExt);
                MessageBox.Show(fbd.DirectoryPath + @"\" + "考勤数据" + DateTime.Now.ToString("yyyy-MM-dd") + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (fbd.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
