using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class Test20HLS : Form
    {
        /*Barcode 测试的条形码，RP，判断当前年为闰年还是平年
         code 商品编码，dateCode日期码，timeCode条形码中获取的时间码
         name商品名称spec规格，price价格,days保质期天数
         enddate根据测试日期及保质期天数判断保质期日期
         DD根据enddate推算出的DD值
         checkCode验证码
             */
        private string BarCode,RP,code,dateCode,timeCode,name,spec,price,days,endDate,DD,checkCode,code69;

       

        private string timeCode2;  //物料信息中获取的时间码
        private DateTime endDate2;////根据测试日期及保质期天数判断保质期日期
        private string endDate2str;//enddate转化为字符串月日month-day
        private bool flag = false;
        private DataTable dt; ///datagridview1数据来源

        /// <summary>
        /// 每页记录数
        /// </summary>
        private int pageSize = 100;

        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;

        /// <summary>
        /// 总页数
        /// </summary>
        private int pageCount = 0;

        /// <summary>
        /// 当前页
        /// </summary>
        private int currentPage = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        public static Test20HLS MyMainForm = null;

        public Test20HLS()
        {
            InitializeComponent();
            MyMainForm = this;
        }


        private void Test20_Load(object sender, EventArgs e)
        {
            timelbl.Text = DateTime.Now.ToString("yyyy-MM-dd");

            string sql = @"select CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode from APP_CodeCheckRecord20HLS where CheckDate>DateAdd(day,@days,getdate())";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("days", -15));
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            { 
              refresh();
            }

            //设置dataGridView双buffer代码如下，需要引用反射命名空间
            Type dgvType = this.dataGridView1.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGridView1, true, null);

            this.btn_EndPage.Click += Ctrl_Click;
            this.btn_FirstPage.Click += Ctrl_Click;
            this.btn_LastPage.Click += Ctrl_Click;
            this.btn_NextPage.Click += Ctrl_Click;


            //以下代码会导致刷新数据过慢，取消，固定列宽
            //dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            //dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;//行首自适应宽度
            
        }

        private void Ctrl_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == this.btn_EndPage.Text)
            {
                if (currentPage == pageCount)
                { return; }
                currentPage = pageCount;
                LoadPage();
            }
            else if (btn.Text == this.btn_FirstPage.Text)
            {
                if (currentPage == 1)
                { return; }
                currentPage = 1;
                LoadPage();
            }
            else if (btn.Text == this.btn_LastPage.Text)
            {
                if (currentPage == 1)
                { return; }
                currentPage--;
                LoadPage();
            }
            else if (btn.Text == this.btn_NextPage.Text)
            {
                if (currentPage == pageCount)
                { return; }
                currentPage++;
                LoadPage();
            }
            else
            {
                MessageBox.Show("error");
            }


        }

        private void dgrclear()
        {
            dataGridView1.Rows.Clear();
        }


        //添加行号
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
                //条码输入框按下回车键
                if (e.KeyValue == 13)
                {
                if (textBox1.Text.Trim().Length==13)//判断字符串长度是否是13
                {
                    BarCode = textBox1.Text;
                    code = BarCode.Substring(2, 6);
                    code69 = BarCode.Substring(9,3);//一鸣产品的产品码
                    dateCode = BarCode.Substring(8, 2);
                    timeCode = BarCode.Substring(10, 2);
                    checkCode= BarCode.Substring(0, 12);//验证码
                    if (textBox1.Text.Substring(0, 2) == "20")//首两位是20，继续判断
                    {
                        //由3-6位判断物料名称，9-10位判断日期码，11-12位判断时间码
                        //通过商品码查询
                        string sql = @"select ml.FNAME,ml.FSPECIFICATION, mb.f_paez_text6,convert(int,ms.FEXPPERIOD) as days,convert(decimal(18,4),mb.F_PAEZ_DECIMAL9) as price from T_BD_MATERIALBASE mb inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=mb.FMATERIALID
                                        inner join T_BD_MATERIAL m on m.FMATERIALID=ml.FMATERIALID
                                        inner join T_BD_MATERIALSTOCK ms on ms.FMATERIALID=m.FMATERIALID
                                        where  m.FMNEMONICCODE='" + code + "' and mb.FCATEGORYID=241 and m.FUSEORGID=1 and 1=@data";
                        DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("data", 1));
                        if (ds.Tables[0].Rows.Count > 0) //通过商品码查询到商品，继续判断
                        {
                            //判断闰年平年
                            if (DateTime.Now.Year % 4 == 0 && DateTime.Now.Year % 100 != 0 || DateTime.Now.Year % 400 == 0)
                            {
                                RP = "R";
                            }
                            else
                            {
                                RP = "P";
                            }
                            name = ds.Tables[0].Rows[0][0].ToString();
                            spec = ds.Tables[0].Rows[0][1].ToString();
                            timeCode2 = ds.Tables[0].Rows[0][2].ToString();
                            days = ds.Tables[0].Rows[0][3].ToString();
                            price = ds.Tables[0].Rows[0][4].ToString();
                            //根据测试日期及保质期天数判断保质期日期
                            endDate2 = DateTime.Now.AddDays(double.Parse(days) - 1);
                            endDate2str = endDate2.Month.ToString() + "-" + endDate2.Day.ToString();//3-13
                            //根据endDate2str中的日期计算出DD值
                            sql = @"select DD from APP_DDTABLE where vdate=@vdate and rp=@RP";
                            ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("vdate", endDate2str), SQLHelper.MakeParam("RP", RP));
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DD = ds.Tables[0].Rows[0][0].ToString();
                            }
                            else
                            {
                                MessageBox.Show("DD表异常，请检查");
                            }
                            if (DD.Equals(dateCode) == true)//如果根据保质期推算出的DD值与条码中的日期码相同，则正确
                            {
                                //判断时间码
                                if (timeCode.Equals(timeCode2) == true)
                                {
                                    //如果时间码一致，则验证正确
                                    sql = @"insert into APP_CodeCheckRecord20HLS (CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode) values (@checkdate,@barcode,@code,@name,@spec,@price,@days,@enddate,@timecode,@checkcode,'')";
                                    flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("checkdate", DateTime.Now.ToString()), SQLHelper.MakeParam("barcode", BarCode), SQLHelper.MakeParam("code", code),
                                                SQLHelper.MakeParam("name", name), SQLHelper.MakeParam("spec", spec), SQLHelper.MakeParam("price", price), SQLHelper.MakeParam("days", days),
                                                SQLHelper.MakeParam("enddate", endDate2.ToShortDateString()), SQLHelper.MakeParam("timecode", timeCode2), SQLHelper.MakeParam("checkcode", checkCode));
                                    if (flag == true)
                                    {
                                        //refresh();
                                        //CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode
                                        DataRow dr = dt.NewRow();
                                        dr["checkdate"] = DateTime.Now.ToString();
                                        dr["Barcode"] = BarCode;
                                        dr["Code"] = code;
                                        dr["Name"] = name;
                                        dr["Spec"] = spec;
                                        dr["Price"] = price;
                                        dr["days"] = days;
                                        dr["Enddate"] = endDate2.ToShortDateString();
                                        dr["TimeCode"] = timeCode2;
                                        dr["CheckCode"] = checkCode;
                                        dr["ResultCode"] = "";
                                        dt.Rows.Add(dr);
                                        refresh();
                                    }
                                    else
                                    {
                                        MessageBox.Show("插入数据异常，请检查！");
                                        return;
                                    }
                                }
                                else
                                {
                                    //如果时间码不一致
                                    sql = @"insert into APP_CodeCheckRecord20HLS (CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode) values (@checkdate,@barcode,@code,@name,@spec,@price,@days,@enddate,@timecode,@checkcode,'*** 时间码异常 ***')";
                                    flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("checkdate", DateTime.Now.ToString()), SQLHelper.MakeParam("barcode", BarCode), SQLHelper.MakeParam("code", code),
                                                SQLHelper.MakeParam("name", name), SQLHelper.MakeParam("spec", spec), SQLHelper.MakeParam("price", price), SQLHelper.MakeParam("days", days),
                                                SQLHelper.MakeParam("enddate", endDate2.ToShortDateString()), SQLHelper.MakeParam("timecode", timeCode2), SQLHelper.MakeParam("checkcode", checkCode));
                                    if (flag == true)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["checkdate"] = DateTime.Now.ToString();
                                        dr["Barcode"] = BarCode;
                                        dr["Code"] = code;
                                        dr["Name"] = name;
                                        dr["Spec"] = spec;
                                        dr["Price"] = price;
                                        dr["days"] = days;
                                        dr["Enddate"] = endDate2.ToShortDateString();
                                        dr["TimeCode"] = timeCode2;
                                        dr["CheckCode"] = checkCode;
                                        dr["ResultCode"] = "*** 时间码异常 ***";
                                        dt.Rows.Add(dr);
                                        refresh();
                                    }
                                    else
                                    {
                                        MessageBox.Show("插入数据异常，请检查！");
                                        return;
                                    }
                                }

                            }
                            else
                            {
                                //如果根据保质期推算出的DD值与条码中的日期码不相同，则报异常
                                sql = @"insert into APP_CodeCheckRecord20HLS (CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode) values (@checkdate,@barcode,@code,@name,@spec,@price,@days,@enddate,@timecode,@checkcode,'*** 日期码异常 ***')";
                                flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("checkdate", DateTime.Now.ToString()), SQLHelper.MakeParam("barcode", BarCode), SQLHelper.MakeParam("code", code),
                                            SQLHelper.MakeParam("name", name), SQLHelper.MakeParam("spec", spec), SQLHelper.MakeParam("price", price), SQLHelper.MakeParam("days", days),
                                            SQLHelper.MakeParam("enddate", endDate2.ToShortDateString()), SQLHelper.MakeParam("timecode", timeCode2), SQLHelper.MakeParam("checkcode", checkCode));
                                if (flag == true)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["checkdate"] = DateTime.Now.ToString();
                                    dr["Barcode"] = BarCode;
                                    dr["Code"] = code;
                                    dr["Name"] = name;
                                    dr["Spec"] = spec;
                                    dr["Price"] = price;
                                    dr["days"] = days;
                                    dr["Enddate"] = endDate2.ToShortDateString();
                                    dr["TimeCode"] = timeCode2;
                                    dr["CheckCode"] = checkCode;
                                    dr["ResultCode"] = "*** 日期码异常 ***";
                                    dt.Rows.Add(dr);
                                    refresh();
                                }
                                else
                                {
                                    MessageBox.Show("插入数据异常，请检查！");
                                    return;
                                }
                            }


                        }
                        
                        else
                        {  //通过商品码未查询到商品
                            sql = @"insert into APP_CodeCheckRecord20HLS (CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode) values (@checkdate,@barcode,@code,'','','','','','','','*** 商品码异常 ***')";
                            flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("checkdate", DateTime.Now.ToString()), SQLHelper.MakeParam("barcode", BarCode), SQLHelper.MakeParam("code", code));
                            if (flag == true)
                            {
                                DataRow dr = dt.NewRow();
                                dr["checkdate"] = DateTime.Now.ToString();
                                dr["Barcode"] = BarCode;
                                dr["Code"] = code;
                                dr["Name"] = "";
                                dr["Spec"] = "";
                                dr["Price"] = "";
                                dr["days"] = "";
                                dr["Enddate"] = "";
                                dr["TimeCode"] = "";
                                dr["CheckCode"] = "";
                                dr["ResultCode"] = "*** 商品码异常 ***";
                                dt.Rows.Add(dr);
                                refresh();
                            }
                            else
                            {
                                MessageBox.Show("插入数据异常，请检查！");
                                return;
                            }
                        }
                    }
                    else if (textBox1.Text.Substring(0, 2) == "69")//首两位是69，继续判断
                    {
                        string sql = @"select ml.FNAME,m.F_PAEZ_TEXT1,mb.f_paez_text6,convert(int,ms.FEXPPERIOD) as days,convert(decimal(18,4),mb.F_PAEZ_DECIMAL9) as price from T_BD_MATERIALBASE mb inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=mb.FMATERIALID
                                        inner join T_BD_MATERIAL m on m.FMATERIALID=ml.FMATERIALID
                                        inner join T_BD_MATERIALSTOCK ms on ms.FMATERIALID=m.FMATERIALID
                                        where  m.FMNEMONICCODE='" + code69 + "' and mb.FCATEGORYID=241 and m.FUSEORGID=1 and 1=@data";
                        DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("data", 1));
                        if (ds.Tables[0].Rows.Count > 0) //通过商品码查询到商品，继续判断
                        {
                            name = ds.Tables[0].Rows[0][1].ToString();
                            days= ds.Tables[0].Rows[0][3].ToString();
                            endDate2 = DateTime.Now.AddDays(double.Parse(days) - 1);
                            sql = @"insert into APP_CodeCheckRecord20HLS (CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode) values (@checkdate,@barcode,@code,@name,@spec,@price,@days,@enddate,@timecode,@checkcode,'')";
                            flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("checkdate", DateTime.Now.ToString()), SQLHelper.MakeParam("barcode", BarCode), SQLHelper.MakeParam("code", code69),
                                        SQLHelper.MakeParam("name", name), SQLHelper.MakeParam("spec", ""), SQLHelper.MakeParam("price", ""), SQLHelper.MakeParam("days", days),
                                        SQLHelper.MakeParam("enddate", endDate2.ToShortDateString()), SQLHelper.MakeParam("timecode", ""), SQLHelper.MakeParam("checkcode", checkCode));
                            if (flag == true)
                            {
                                //refresh();
                                //CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode
                                DataRow dr = dt.NewRow();
                                dr["checkdate"] = DateTime.Now.ToString();
                                dr["Barcode"] = BarCode;
                                dr["Code"] = code69;
                                dr["Name"] = name;
                                dr["Spec"] = spec;
                                dr["Price"] = "";
                                dr["days"] = days;
                                dr["Enddate"] = endDate2.ToShortDateString();
                                dr["TimeCode"] = "";
                                dr["CheckCode"] = checkCode;
                                dr["ResultCode"] = "";
                                dt.Rows.Add(dr);
                                refresh();
                            }
                            else
                            {
                                MessageBox.Show("插入数据异常，请检查！");
                                return;
                            }

                        }
                        else
                        {  //通过商品码未查询到商品
                            sql = @"insert into APP_CodeCheckRecord20HLS (CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode) values (@checkdate,@barcode,@code,'','','','','','','','*** 商品码异常 ***')";
                            flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("checkdate", DateTime.Now.ToString()), SQLHelper.MakeParam("barcode", BarCode), SQLHelper.MakeParam("code", code));
                            if (flag == true)
                            {
                                DataRow dr = dt.NewRow();
                                dr["checkdate"] = DateTime.Now.ToString();
                                dr["Barcode"] = BarCode;
                                dr["Code"] = code69;
                                dr["Name"] = "";
                                dr["Spec"] = "";
                                dr["Price"] = "";
                                dr["days"] = "";
                                dr["Enddate"] = "";
                                dr["TimeCode"] = "";
                                dr["CheckCode"] = "";
                                dr["ResultCode"] = "*** 商品码异常 ***";
                                dt.Rows.Add(dr);
                                refresh();
                            }
                            else
                            {
                                MessageBox.Show("插入数据异常，请检查！");
                                return;
                            }
                        }
                    }
                    else
                    {   
                        ///首两位不是20，则提示前两码异常，并记录
                        string sql = @"insert into APP_CodeCheckRecord20HLS (CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode) values (@checkdate,@barcode,@code,'','','','','','','','*** 前两码异常 ***')";
                        flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("checkdate", DateTime.Now.ToString()), SQLHelper.MakeParam("barcode", BarCode), SQLHelper.MakeParam("code", code));
                        if (flag == true)
                        {
                            DataRow dr = dt.NewRow();
                            dr["checkdate"] = DateTime.Now.ToString();
                            dr["Barcode"] = BarCode;
                            dr["Code"] = code;
                            dr["Name"] = "";
                            dr["Spec"] = "";
                            dr["Price"] = "";
                            dr["days"] = "";
                            dr["Enddate"] = "";
                            dr["TimeCode"] = "";
                            dr["CheckCode"] = "";
                            dr["ResultCode"] = "*** 前两码异常 ***";
                            dt.Rows.Add(dr);
                            refresh();

                        }
                        else
                        {
                            MessageBox.Show("插入数据异常，请检查！");
                            return;
                        }

                    }
                }
                else {
                    MessageBox.Show("条码长度不为13，请检查");
                }

                textBox1.Text = "";

            }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        private void PageSorter()
        {
            dataGridView1.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.Name = "";
                dgvc.DataPropertyName = "";
                dgvc.HeaderText = col.ToString();
                dataGridView1.Columns.Add(dgvc);
            }
            recordCount = dt.Rows.Count;     //记录总行数
            pageCount = (recordCount / pageSize);
            if ((recordCount % pageSize) > 0)
            {
                pageCount++;
            }
            //默认第一页
            currentPage = 1;
            LoadPage();//调用加载数据的方法
        }

        private void LoadPage()
        {
            if (currentPage < 1) currentPage = 1;
            if (currentPage > pageCount) currentPage = pageCount;

            int beginRecord;    //开始指针
            int endRecord;      //结束指针
            DataTable dtTemp;
            dtTemp = dt.Clone();

            beginRecord = pageSize * (currentPage - 1);
            if (currentPage == 1) beginRecord = 0;
            endRecord = pageSize * currentPage;

            if (currentPage == pageCount) endRecord = recordCount;
            for (int i = beginRecord; i < endRecord; i++)
            {
                dtTemp.ImportRow(dt.Rows[i]);
            }

            dataGridView1.Rows.Clear();
            this.lb_NowPage.Text = "当前页: " + currentPage.ToString() + " / " + pageCount.ToString();//当前页
            this.Lb_TotalRows.Text = "总行数: " + recordCount.ToString() + " 行";//总记录数



            //把临时table的数据插入到datagridview控件里面。
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < dtTemp.Columns.Count; j++)
                    dataGridView1.Rows[i].Cells[j].Value = dtTemp.Rows[i][j].ToString();
            }

            
        }


        public void refresh() {
            //string sql = @"select CheckDate,Barcode,Code,Name,Spec,Price,days,Enddate,TimeCode,CheckCode,ResultCode from APP_CodeCheckRecord20HLS where CheckDate>DateAdd(day,@days,getdate())";
            //DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("days", -15));
            //dt = ds.Tables[0];
            //dt.DefaultView.Sort = "CheckDate desc ";
            //dt = dt.DefaultView.ToTable();

            dt.DefaultView.Sort = "CheckDate desc ";
            dt = dt.DefaultView.ToTable();

            PageSorter();

            dataGridView1.RowHeadersWidth = 60;
            dataGridView1.Columns[0].HeaderCell.Value = "测试日期";
            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].HeaderCell.Value = "商品条形码";
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].HeaderCell.Value = "产品编号";
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].HeaderCell.Value = "产品名称";
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].HeaderCell.Value = "产品规格";
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].HeaderCell.Value = "销售单价";
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].HeaderCell.Value = "保质天数";
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].HeaderCell.Value = "保质期";
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[8].HeaderCell.Value = "时间码";
            dataGridView1.Columns[8].Width = 80;
            dataGridView1.Columns[9].HeaderCell.Value = "验证码";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].HeaderCell.Value = "检查码";
            dataGridView1.Columns[10].Width = 110;

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[10].DefaultCellStyle.ForeColor = Color.Red;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[10].Value.ToString().Equals("") == false)
                {
                    if (dataGridView1.Rows[i].Cells[10].Value.ToString().Substring(4, 3).Equals("时间码") == true)
                    {
                        //如果时间码异常，则时间码值为红色
                        dataGridView1.Rows[i].Cells[8].Style.ForeColor = Color.Red;
                    }
                    else if (dataGridView1.Rows[i].Cells[10].Value.ToString().Substring(4, 3).Equals("商品码") == true)
                    {
                        //如果商品码异常，则商品码值为红色
                        dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                    }
                    else if (dataGridView1.Rows[i].Cells[10].Value.ToString().Substring(4, 3).Equals("日期码") == true)
                    {
                        //如果日期码异常，则保质期为红色
                        dataGridView1.Rows[i].Cells[7].Style.ForeColor = Color.Red;
                    }
                }
                else
                {

                }
            }

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            DeleteChooseHLS deleteChoosehls  = new DeleteChooseHLS();
            //在父窗体中间弹出新窗口
            deleteChoosehls.StartPosition = FormStartPosition.Manual;
            deleteChoosehls.Location = new Point(Test20HLS.MyMainForm.Location.X + Test20HLS.MyMainForm.Width / 2 - deleteChoosehls.Width / 2, Test20HLS.MyMainForm.Location.Y + Test20HLS.MyMainForm.Height / 2 - deleteChoosehls.Height / 2);
            deleteChoosehls.ShowDialog();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
