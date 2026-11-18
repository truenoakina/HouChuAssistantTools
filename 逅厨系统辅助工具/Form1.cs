using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class Form1 : Form
    {

        private DataTable dt, dt2;
        private bool flag = false;

        private string file = "";
        private string guid = "";   //随机字符串，用于保证转换数据时绑定用户，不被其他用户影响
        private string altermsg = "";//警告信息，警告罗森订单中哪些物料在金蝶中缺少一便码

        ToolTip toolTip = new ToolTip();
        public Form1()
        {
            InitializeComponent();
            try
            {
                GetComputerName();

            }
            catch (Exception)
            {
            }
        }


        /// <summary>
        /// 计算机名
        /// </summary>
        private string computer = "";

        /// <summary>
        /// 返回计算机名
        /// </summary>
        public void GetComputerName()
        {
            //获得主机名
            computer = Dns.GetHostName();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Material material = new Material();
            material.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //r如果计算机名不是txh就不能数据维护
            //if (string.Equals(computer, "txh") == false && string.Equals(computer, "DESKTOP-NV2EBOQ") == false)
            //{
            //   
            //}
            switch (Properties.Settings.Default.defaultfacid)
            {
                case 0:
                    comboBox1.SelectedIndex = 0;
                    break;
                case 1:
                    comboBox1.SelectedIndex = 1;
                    break;
                case 2:
                    comboBox1.SelectedIndex = 2;
                    break;
                case 3:
                    comboBox1.SelectedIndex = 3;
                    break;
            }

            button2.Visible = true;
            dataGridView1.ReadOnly = true;
            code1.Checked = true;
            if (LoginInfo.UserName == "admin")
            {
                button2.Visible = true;
            }
            else
            {
                button2.Visible = false;
            }

            ComboxItem[] jhd = {
                new ComboxItem("广州", "GZ"),
                new ComboxItem("深圳", "SZ"),
                new ComboxItem("厦门", "XM"),
                new ComboxItem("潮汕", "CS")
            };
            cb_jhd.Items.AddRange(jhd);
            cb_jhd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }



        private void btn_txtex_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("请选择销售组织");
                return;
            }



            if (this.comboBox1.SelectedIndex == 0)/// 选择的是佳之家
            {
                MyVar.zzcode = "102";
                MyVar.zzname = "江苏佳之家食品科技有限公司";
                MyVar.salerCode = "HC0033";
                MyVar.salerName = "佳之家文员";
            }
            else if (this.comboBox1.SelectedIndex == 1)///选择的谷之田
            {
                MyVar.zzcode = "103";
                MyVar.zzname = "江阴华联谷之田食品科技有限公司";
                MyVar.salerCode = "HC0034";
                MyVar.salerName = "谷之田文员";
            }
            else if (this.comboBox1.SelectedIndex == 2)///选择的好利森
            {
                MyVar.zzcode = "104";
                MyVar.zzname = "江苏好利森食品科技有限公司";
                MyVar.salerCode = "HC0030";
                MyVar.salerName = "好利森销售";
            }
            else if (this.comboBox1.SelectedIndex == 3)///选择的广东逅厨
            {
                MyVar.zzcode = "107";
                MyVar.zzname = "广东逅厨食品有限公司";
                MyVar.salerCode = "";
                MyVar.salerName = "";

            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            //dialog.Filter = "所有文件(*.*)|*.*";
            dialog.Filter = "txt文件(*.txt)|*.txt";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    //file = dialog.FileName;
                    //textBox1.Text = file;
                    // 新建一个DataTable
                    string fileName = "";
                    DataTable tb = new DataTable();
                    // 添加列
                    DataColumn a = tb.Columns.Add("lscode", typeof(string));
                    DataColumn b = tb.Columns.Add("num", typeof(double));
                    DataColumn c = tb.Columns.Add("price", typeof(double));
                    //取多个文件
                    foreach (string Wenjian in dialog.FileNames)
                    {
                        StreamReader rd = File.OpenText(Wenjian);
                        fileName = fileName + System.IO.Path.GetFileNameWithoutExtension(Wenjian) + ";";
                        string line;
                        // 循环读出文件的每一行
                        while ((line = rd.ReadLine()) != null)
                        {
                            // 拆分出一行的所有用空格分割的数据项
                            string[] values = line.Split(',');
                            // 存入DataTable
                            DataRow r = tb.NewRow();
                            for (int i = 10; i < values.Length; i++)
                            {
                                string s = values[i];
                                Console.WriteLine(values[14]);
                                //判断是否为汉字，不是汉字才添加
                                if (!string.IsNullOrEmpty(s) & i == 12 & !Regex.IsMatch(s, "[\u4e00-\u9fbb]"))
                                {
                                    r["lscode"] = s;
                                }
                                else if (!string.IsNullOrEmpty(s) & i == 16 & !Regex.IsMatch(s, "[\u4e00-\u9fbb]")& values[14].Contains("[L]娃娃菜"))
                                {
                                    r["num"] = s;
                                }
                                else if (!string.IsNullOrEmpty(s) & i == 19 & !Regex.IsMatch(s, "[\u4e00-\u9fbb]") & !values[14].Contains("[L]娃娃菜"))
                                {
                                    r["num"] = s;
                                }
                                else if (!string.IsNullOrEmpty(s) & i == 17 & !Regex.IsMatch(s, "[\u4e00-\u9fbb]"))
                                {
                                    r["price"] = s;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            //空行不加入
                            if (!string.IsNullOrEmpty(r["lscode"].ToString()) && !string.IsNullOrEmpty(r["num"].ToString()) && !string.IsNullOrEmpty(r["price"].ToString()))
                            {
                                tb.Rows.Add(r);
                            }
                        }
                        rd.Close();
                    }
                    textBox1.Text = fileName;
                    dataGridView1.DataSource = tb;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return;
            }
        }

        private void btn_change_Click(object sender, EventArgs e)
        {

            MyVar.jhd = cb_jhd.SelectedIndex == -1 ? "" : ((ComboxItem)cb_jhd.SelectedItem).Text;
            MyVar.jhdId = cb_jhd.SelectedIndex == -1 ? "" : ((ComboxItem)cb_jhd.SelectedItem).Values;
            dt = dataGridView1.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show("未上传文档，请检查");
                return;
            }
            else
            {
                //如果勾选取一便码
                if (code1.CheckState == CheckState.Checked)
                {
                    guid = RandomStr.GetRandomString(20);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sql = @"insert into LSDATA (guid,lscode,num,price) values (@guid,@lscode,@num,@price)";
                        flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid), SQLHelper.MakeParam("lscode", dt.Rows[i]["lscode"].ToString()), SQLHelper.MakeParam("num", dt.Rows[i]["num"].ToString()), SQLHelper.MakeParam("price", dt.Rows[i]["price"].ToString()));
                    }
                    if (flag == true)
                    {
                        //string sql = @"select k.kcode,k.kname,u.code,k.kunit,sum(Convert(decimal(18,0),l.num)) as num,l.price from lsdata l
                        //                left join KINDEEMATERIAL k on k.Lscode=l.lscode 
                        //                left join unit u on u.name=k.kunit where l.guid=@guid group by k.kcode,k.kname,u.code,k.kunit,l.price order by k.kcode";

                        string sql = @"select distinct l.lscode from lsdata l where not exists (select 1 from AIShlhcsvr.dbo.T_BD_MATERIAL m where m.FMNEMONICCODE=l.lscode and m.FUSEORGID in (1,441894))
                                                             and l.guid=@guid";
                        DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("guid", guid));
                        dt2 = ds.Tables[0];
                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                if (i < dt2.Rows.Count - 1)
                                {
                                    altermsg = altermsg + "'"+dt2.Rows[i][0].ToString()+"'" + ",";
                                }
                                else
                                {
                                    altermsg = altermsg + "'" + dt2.Rows[i][0].ToString() + "'";
                                }
                            }
                            sql = @"delete from lsdata where guid=@guid";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                            MessageBox.Show("罗森编码" + altermsg + "在金蝶中没有匹配到商品，请检查系统中该物料的一便码");
                        }
                        else
                        {
                            sql = @"select m.fnumber as kcode,ml.fname as kname,u.fnumber as code,ul.fname as kunit,sum(Convert(decimal(18,0),l.num)) as num,l.price from lsdata l
                                                    left join AIShlhcsvr.dbo.T_BD_MATERIAL m on m.FMNEMONICCODE=l.lscode 
                                                    inner join AIShlhcsvr.dbo.T_BD_MATERIAL_L ml on m.FMATERIALID=ml.FMATERIALID
                                                    inner join AIShlhcsvr.dbo.T_BD_MATERIALBASE mb on m.FMATERIALID=mb.FMATERIALID
                                                    inner join AIShlhcsvr.dbo.t_BD_Unit u on u.FUNITID=mb.FBASEUNITID
                                                    inner join AIShlhcsvr.dbo.t_BD_Unit_L ul on u.FUNITID=ul.FUNITID
                                                    where l.guid=@guid and m.FUSEORGID in (1,441894)
                                                    group by  m.fnumber,ml.fname,u.fnumber,ul.fname,l.price order by m.fnumber";
                            ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("guid", guid));
                            dt2 = ds.Tables[0];

                            string fileExt = ".xlsx";

                            //nowDate = DateTime.Now.ToString("D");
                            //取查询日期段作为文件名称
                            MyVar.saleDate = dateTimePicker1.Value.Year + "/" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Day;
                            string strt = dateTimePicker1.Text;


                            FolderBrowserDialog fbd = new FolderBrowserDialog();
                            if (fbd.ShowDialog(this) == DialogResult.OK)
                            {
                                if (MyVar.zzcode == "107")
                                {
                                    //广东逅厨转换
                                    ExcelHelper.TableToExcelGDHC(fbd.DirectoryPath + @"\" + "罗森订单" + MyVar.jhd + strt, dt2 as DataTable, fileExt);
                                }
                                else
                                {
                                    //江苏逅厨转换
                                    ExcelHelper.TableToExcel(fbd.DirectoryPath + @"\" + "罗森订单" + strt, dt2 as DataTable, fileExt);
                                }
                                MessageBox.Show(fbd.DirectoryPath + @"\" + "罗森订单" + MyVar.jhd + strt + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                sql = @"delete from lsdata where guid=@guid";
                                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                            }
                            else if (fbd.ShowDialog(this) == DialogResult.Cancel)
                            {
                                sql = @"delete from lsdata where guid=@guid";
                                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("上传文档异常，请检查");
                        return;
                    }
                }
                //取2便码的值
                else if (code2.CheckState == CheckState.Checked)
                {
                    guid = RandomStr.GetRandomString(20);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sql = @"insert into LSDATA2 (guid,lscode2,num,price) values (@guid,@lscode,@num,@price)";
                        flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid), SQLHelper.MakeParam("lscode", dt.Rows[i]["lscode"].ToString()), SQLHelper.MakeParam("num", dt.Rows[i]["num"].ToString()), SQLHelper.MakeParam("price", dt.Rows[i]["price"].ToString()));
                    }
                    if (flag == true)
                    {
                        string sql = @"select m.fnumber as kcode,ml.fname as kname,u.fnumber as code,ul.fname as kunit,sum(Convert(decimal(18,0),l.num)) as num,l.price from lsdata l
                                                    left join AIShlhcsvr.dbo.T_BD_MATERIAL m on m.FMNEMONICCODE=l.lscode 
                                                    inner join AIShlhcsvr.dbo.T_BD_MATERIAL_L ml on m.FMATERIALID=ml.FMATERIALID
                                                    inner join AIShlhcsvr.dbo.T_BD_MATERIALBASE mb on m.FMATERIALID=mb.FMATERIALID
                                                    inner join AIShlhcsvr.dbo.t_BD_Unit u on u.FUNITID=mb.FBASEUNITID
                                                    inner join AIShlhcsvr.dbo.t_BD_Unit_L ul on u.FUNITID=ul.FUNITID
                                                    where l.guid=@guid and m.FUSEORGID in (1,441894)
                                                    group by  m.fnumber,ml.fname,u.fnumber,ul.fname,l.price order by m.fnumber";

                        DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("guid", guid));
                        dt2 = ds.Tables[0];

                        string fileExt = ".xlsx";

                        //nowDate = DateTime.Now.ToString("D");
                        //取查询日期段作为文件名称
                        MyVar.saleDate = dateTimePicker1.Value.Year + "/" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Day;
                        string strt = dateTimePicker1.Text;


                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog(this) == DialogResult.OK)
                        {

                            //ExcelHelper.TableToExcel(fbd.SelectedPath + @"\" +exportDate + " " + exportTime + " 备货汇总", dataGridView1.DataSource as DataTable, fileExt);
                            //MessageBox.Show(fbd.SelectedPath + @"\" + exportDate + " " + exportTime + " 备货汇总" + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ExcelHelper.TableToExcel(fbd.DirectoryPath + @"\" + "罗森订单" + strt, dt2 as DataTable, fileExt);
                            MessageBox.Show(fbd.DirectoryPath + @"\" + "罗森订单" + strt + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //sql = @"delete from lsdata2 where guid=@guid";
                            //SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        }
                        else if (fbd.ShowDialog(this) == DialogResult.Cancel)
                        {
                            //sql = @"delete from lsdata2 where guid=@guid";
                            //SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        }

                        sql = @"delete from lsdata2 where guid=@guid";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                    }
                    else
                    {
                        MessageBox.Show("上传文档异常，请检查");
                        return;
                    }
                }
            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //显示行号
            int row = e.Row.Index + 1;
            e.Row.HeaderCell.Value = string.Format("{0}", row);
        }


        //鼠标停留时显示文件内容
        private void textBox1_MouseHover(object sender, EventArgs e)
        {

            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.ToolTipTitle = "罗森订单转换";
            Point showLocation = new Point(textBox1.Location.X, textBox1.Location.Y + textBox1.Height);
            string files = "";
            string[] sArray = Regex.Split(textBox1.Text, ";", RegexOptions.IgnoreCase);
            foreach (string i in sArray)
            {
                files = files + i + "\r\n";
            }
            toolTip.Show(files, this, showLocation, 10000);
        }


        //鼠标移开是关闭显示提示窗口
        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(textBox1);
        }

        private void btn_hide_MouseUp(object sender, MouseEventArgs e)
        {
            //设置隐藏按钮，右击才能显示物料维护功能
            if (e.Button == MouseButtons.Right)
            {
                button2.Visible = true;
            }
        }

        private void code1_CheckedChanged(object sender, EventArgs e)
        {
            if (code1.Checked == true)
            {
                code2.Checked = false;
            }
        }

        private void code2_CheckedChanged(object sender, EventArgs e)
        {
            if (code2.Checked == true)
            {
                code1.Checked = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.defaultfacid = comboBox1.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("请选择销售组织");
                return;
            }

            string file = "";

            if (this.comboBox1.SelectedIndex == 0)/// 选择的是佳之家
            {
                MyVar.zzcode = "102";
                MyVar.zzname = "江苏佳之家食品科技有限公司";
                MyVar.salerCode = "HC0033";
                MyVar.salerName = "佳之家文员";
            }
            else if (this.comboBox1.SelectedIndex == 1)///选择的谷之田
            {
                MyVar.zzcode = "103";
                MyVar.zzname = "江阴华联谷之田食品科技有限公司";
                MyVar.salerCode = "HC0034";
                MyVar.salerName = "谷之田文员";
            }
            else if (this.comboBox1.SelectedIndex == 2)///选择的好利森
            {
                MyVar.zzcode = "104";
                MyVar.zzname = "江苏好利森食品科技有限公司";
                MyVar.salerCode = "HC0030";
                MyVar.salerName = "好利森销售";
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            //dialog.Filter = "所有文件(*.*)|*.*";
            dialog.Filter = "excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.FileName;
                textBox1.Text = System.IO.Path.GetFileNameWithoutExtension(file);
            }
            else
            {
                return;
            }

            dt = ExcelHelper.ExcelToTable(file);
            dataGridView1.DataSource = dt;


        }
    }
}
