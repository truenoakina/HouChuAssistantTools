using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class LabelPrint : Form
    {
        List<int> indexZT = new List<int>(); //祝塘
        List<int> indexQY = new List<int>();//青阳
        List<int> indexHZ = new List<int>();//华姿
        List<int> indexJYZZ = new List<int>();//江阴中专
        List<int> indexChoose = new List<int>();//选中的学校
        Button btn = new Button();
        Button[] btns = new Button[5];
        private DataTable dt;


        private string school;
        private string group;
        private string wlname;
        private string unit;
        private string qty;
        private string type;
        private string dangkou;
        private string billno;
        private bool isprintAll;//是否全部打印
        private string schoolName;//批量打印弹出确认时用

        public LabelPrint()
        {
            InitializeComponent();
            btns = new Button[] { btn_all, btn_zhutang, btn_qingyang, btn_huazi, btn_jyzz };
        }

        private void LabelPrint_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.CalendarFont = new Font("微软雅黑", 30.0f);
            this.dateTimePicker1.CalendarForeColor = System.Drawing.Color.FromArgb(255, 0, 115, 0);
            dateTimePicker1.Value = DateTime.Now;
            lbl_date.Text = DateTime.Now.ToLongDateString();
            btn_all.Enabled = false;
            btn_zhutang.Enabled = false;
            btn_qingyang.Enabled = false;
            btn_huazi.Enabled = false;
            btn_jyzz.Enabled = false;
            refresh();
           
        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            indexZT.Clear();
            indexQY.Clear();
            indexHZ.Clear();
            indexJYZZ.Clear();
            lbl_date.Text = dateTimePicker1.Value.ToLongDateString();
            refresh();
        }

        private void refresh()
        {
            string sql = @"select o.FBILLNO,cus.FNAME as cname,fzl.FNAME,m.fnumber,ml.FNAME,ROUND(cast(od.FSTOCKQTY as decimal(10,2)), 2) as kcqty,ul.FNAME as kcunit,ftype.FDATAVALUE as ftype,dangkou.FDATAVALUE as dangkou from 
                            T_SAL_ORDERENTRY od inner join T_SAL_ORDER o on o.FID=od.FID
                            inner join T_BD_MATERIAL m on m.FMATERIALID=od.FMATERIALID
                            inner join T_BD_MATERIAL_l ml on od.FMATERIALID=ml.FMATERIALID
                            inner join T_BD_CUSTOMER_L cus on cus.FCUSTID=o.FCUSTID
                            inner join t_BD_Unit_L ul on od.FSTOCKUNITID=ul.FUNITID
                            inner join T_BD_MATERIALGROUP_L fzl on m.FMATERIALGROUP=fzl.FID
                            left join T_BAS_ASSISTANTDATAENTRY_L ftype on ftype.FENTRYID=od.F_PAEZ_ASSISTANT1
                            left join T_BAS_ASSISTANTDATAENTRY_L dangkou on dangkou.FENTRYID=od.F_ZZSZ_DANGKOU
                            where o.FDATE=@fdate and o.FSALEORGID=516100 and o.FDOCUMENTSTATUS='C'
                            order by cus.FNAME,o.FBILLNO,ftype.FDATAVALUE,dangkou.FDATAVALUE,fzl.FNAME,m.FNUMBER";
            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("@fdate", dateTimePicker1.Value.Date));
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.dataGridView1.DataSource = dt;
                btn_all.Enabled = true;
                //默认打印第一行的数据
                billno = dt.Rows[0][0].ToString();
                school = dt.Rows[0][1].ToString();
                group = dt.Rows[0][2].ToString();
                wlname = dt.Rows[0][4].ToString();
                qty = dt.Rows[0][5].ToString();
                unit = dt.Rows[0][6].ToString();
                type = dt.Rows[0][7].ToString();
                dangkou = dt.Rows[0][8].ToString();
                isprintAll = true;
            }
            else
            {
                dataGridView1.DataSource = null;
                btn_all.Enabled = false;
                btn_zhutang.Enabled = false;
                btn_qingyang.Enabled = false;
                btn_huazi.Enabled = false;
                btn_jyzz.Enabled = false;
                foreach (Button btn in btns)
                {
                   
                    btn.BackColor = SystemColors.Control;
                    btn.ForeColor = Color.Black;
                    
                }
                return;
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersWidth = 60;

            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[0].HeaderText = "订单号";
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[1].HeaderText = "客户";
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[2].HeaderText = "物料分组";
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[3].HeaderText = "编码";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[4].HeaderText = "品名";
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[5].HeaderText = "数量";
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[6].HeaderText = "单位";
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[7].HeaderText = "类型";
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[8].HeaderText = "档口";


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 30;
            }
            

            //设置dataGridView双buffer代码如下，需要引用反射命名空间
            //防止数据多时滚动时卡顿
            Type dgvType = this.dataGridView1.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGridView1, true, null);

            //将所有数据的行号根据客户区分到数组
            foreach (DataGridViewRow row in dataGridView1.Rows)
            { 
                switch(row.Cells[1].Value?.ToString())
                {
                    case "江阴市祝塘中学":
                        indexZT.Add(row.Index);
                        break;
                    case "江阴市青阳中学":
                        indexQY.Add(row.Index);
                        break;
                    case "江阴市华姿中等专业学校":
                        indexHZ.Add(row.Index);
                        break;
                    case "江苏省江阴中等专业学校徐霞客校区":
                        indexJYZZ.Add(row.Index);
                        break;
                }
            }

            if (indexZT.Count > 0) { btn_zhutang.Enabled = true; } else { btn_zhutang.Enabled = false; }
            if (indexQY.Count > 0) { btn_qingyang.Enabled = true; } else { btn_qingyang.Enabled = false; }
            if (indexHZ.Count > 0) { btn_huazi.Enabled = true; } else { btn_huazi.Enabled = false; }
            if (indexJYZZ.Count > 0) { btn_jyzz.Enabled = true; } else { btn_jyzz.Enabled = false; }

        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void btn_zhutang_Click(object sender, EventArgs e)
        {
            //btnChange(btn_zhutang);
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = "cname = '江阴市祝塘中学'";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 30;
            }
            indexChoose = indexZT;
            schoolName = "祝塘中学";
            isprintAll = false;
        }

        private void btn_qingyang_Click(object sender, EventArgs e)
        {
            //btnChange(btn_qingyang);
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = "cname = '江阴市青阳中学'";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 30;
            }
            indexChoose = indexQY;
            schoolName = "青阳中学";
            isprintAll = false;
        }

        private void btn_huazi_Click(object sender, EventArgs e)
        {
            //btnChange(btn_huazi);
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = "cname = '江阴市华姿中等专业学校'";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 30;
            }
            indexChoose = indexHZ;
            schoolName = "华姿中专";
            isprintAll = false;
        }

        private void btn_jyzz_Click(object sender, EventArgs e)
        {
            //btnChange(btn_jyzz);
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = "cname = '江苏省江阴中等专业学校徐霞客校区'";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 30;
            }
            indexChoose = indexJYZZ;
            schoolName = "江阴中专";
            isprintAll = false;
        }

        private void btn_all_Click(object sender, EventArgs e)
        {
            //btnChange(btn_all);
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = null;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 30;
            }
            isprintAll = true;
        }

        private void btnChange(Button sender)
        {
            foreach (Button btn in btns)
            {
                if (btn.Name == sender.Name)
                {
                    btn.BackColor = Color.DeepSkyBlue;
                    btn.ForeColor = Color.White;
                    schoolName = btn.Text;
                }
                else
                {
                    btn.BackColor = SystemColors.Control;
                    btn.ForeColor = Color.Black;
                }
            }


        }

        private void btn_plprint_Click(object sender, EventArgs e)
        {
            if (isprintAll)
            {
                DialogResult dr = MessageBox.Show("确定打印所有标签？", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        billno = dt.Rows[0][0].ToString();
                        school = dt.Rows[0][1].ToString();
                        group = dt.Rows[0][2].ToString();
                        wlname = dt.Rows[0][4].ToString();
                        qty = dt.Rows[0][5].ToString();
                        unit = dt.Rows[0][6].ToString();
                        type = dt.Rows[0][7].ToString();
                        dangkou = dt.Rows[0][8].ToString();
                        PrintDocument pd = new PrintDocument();
                        pd.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
                        string defaultName = pd.PrinterSettings.PrinterName;//默认打印机名

                        pd.DefaultPageSettings.PrinterSettings.PrinterName = defaultName;       //打印机名称
                                                                                                //pd.DefaultPageSettings.Landscape = true;  //设置横向打印，不设置默认是纵向的
                        pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                        pd.Print();
                    }
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("确定打印“"+schoolName+"”的所有标签？", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < indexChoose.Count; i++)
                    {
                        billno = dt.Rows[0][0].ToString();
                        school = dt.Rows[0][1].ToString();
                        group = dt.Rows[0][2].ToString();
                        wlname = dt.Rows[0][4].ToString();
                        qty = dt.Rows[0][5].ToString();
                        unit = dt.Rows[0][6].ToString();
                        type = dt.Rows[0][7].ToString();
                        dangkou = dt.Rows[0][8].ToString();
                        PrintDocument pd = new PrintDocument();
                        pd.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
                        string defaultName = pd.PrinterSettings.PrinterName;//默认打印机名

                        pd.DefaultPageSettings.PrinterSettings.PrinterName = defaultName;       //打印机名称
                                                                                                //pd.DefaultPageSettings.Landscape = true;  //设置横向打印，不设置默认是纵向的
                        pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                        pd.Print();
                    }
                }
            }

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            string defaultName = pd.PrinterSettings.PrinterName;//默认打印机名

            pd.DefaultPageSettings.PrinterSettings.PrinterName = defaultName;       //打印机名称
                                                                                    //pd.DefaultPageSettings.Landscape = true;  //设置横向打印，不设置默认是纵向的
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();
            pd.Print();
        }

        /// <summary>
        /// 食材管理票常规打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font bigFont = new Font("微软雅黑", 14, System.Drawing.FontStyle.Bold);//大  
            Font middleFont = new Font("微软雅黑", 11, System.Drawing.FontStyle.Bold);//小    
            Font smallFont = new Font("微软雅黑", 10, System.Drawing.FontStyle.Bold);//小         

            System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.Black);//画刷           
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);           //线条颜色         
            try
            {
                //e.Graphics.DrawRectangle(pen, 6, 7, 222, 186);
                e.Graphics.DrawString("订单号:" + billno, smallFont, brush, new System.Drawing.Point(5, 5));
                e.Graphics.DrawString("日期:"+dateTimePicker1.Value.ToLongDateString(), smallFont, brush, new System.Drawing.Point(5, 30));
                e.Graphics.DrawString(school, smallFont, brush, new System.Drawing.Point(5,55));
                e.Graphics.DrawString("类别:"+type, smallFont, brush, new System.Drawing.Point(5, 80));
                e.Graphics.DrawString("档口:" + dangkou, smallFont, brush, new System.Drawing.Point(80, 80));
                e.Graphics.DrawString("物料分组:" + group, smallFont, brush, new System.Drawing.Point(5, 105));
                e.Graphics.DrawString("品名:"+wlname, smallFont, brush, new System.Drawing.Point(5, 130));
                e.Graphics.DrawString("应发数量:"+qty, smallFont, brush, new System.Drawing.Point(5, 155));
                e.Graphics.DrawString( unit, smallFont, brush, new System.Drawing.Point(120, 155));
                e.Graphics.DrawString("实发数量:", smallFont, brush, new System.Drawing.Point(5, 180));
                e.Graphics.DrawString(unit, smallFont, brush, new System.Drawing.Point(120, 180));
                e.Graphics.DrawString("框数:", smallFont, brush, new System.Drawing.Point(5, 205));
                e.Graphics.DrawString("袋数:", smallFont, brush, new System.Drawing.Point(100, 205));

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            else
            {
                billno= dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                school = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                group= dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                wlname = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                qty= dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                unit = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                type= dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                dangkou = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

            }
        }
    }
}
