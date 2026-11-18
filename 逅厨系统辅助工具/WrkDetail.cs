using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class WrkDetail : Form
    {
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
        private DataTable dt; ///datagridview1数据来源

        public WrkDetail()
        {
            InitializeComponent();
        }

        private void WrkDetail_Load(object sender, EventArgs e)
        {
            lbl_ps.Text = "注:只显示未入库数量>0，未关闭的订单";
            this.WindowState = FormWindowState.Maximized;
            begindate.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            enddate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            //从弹出页面进入
            if (WrkDdInfo.Cgzz > 0)
            { 
                if (WrkDdInfo.Cgzz == 1)
                {
                    cb_zgcc.SelectedIndex = 0;
                    dt = WrkDdInfo.Dthouchuwrk;
                }
                else if (WrkDdInfo.Cgzz == 2)
                {
                    cb_zgcc.SelectedIndex = 2;
                    dt = WrkDdInfo.Dtjzjwrk;
                }
                else if (WrkDdInfo.Cgzz == 3)
                {
                    cb_zgcc.SelectedIndex = 3;
                    dt = WrkDdInfo.Dtgztwrk;
                }
                else if (WrkDdInfo.Cgzz == 4)
                {
                    cb_zgcc.SelectedIndex = 4;
                    dt = WrkDdInfo.Dthlswrk;
                }
                dt.DefaultView.Sort = "订单日期 asc ";
                dt = dt.DefaultView.ToTable();
                if (dt.Rows.Count > 0)
                {
                    PageSorter();
                }
                else
                {
                    return;
                }
            }
            //从首页功能页面进入
            else if (WrkDdInfo.Cgzz == 0)
            {
                cb_zgcc.SelectedIndex = 0;
            }
            
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.RowHeadersWidth = 60;


            //// 设置表头文本对齐方式
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // 设置表头字体样式
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            // 设置表头文本颜色
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Red;

            

            //设置dataGridView双buffer代码如下，需要引用反射命名空间
            Type dgvType1 = this.dataGridView1.GetType();
            PropertyInfo pi1 = dgvType1.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi1.SetValue(this.dataGridView1, true, null);

            this.btn_EndPage.Click += Ctrl_Click;
            this.btn_FirstPage.Click += Ctrl_Click;
            this.btn_LastPage.Click += Ctrl_Click;
            this.btn_NextPage.Click += Ctrl_Click;
        }

       

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
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
            string billno="";  //行的单号用于变色用
            int colortype = 1; //行背景颜色，1.黄色，2.红色
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
            this.lbl_now.Text = currentPage.ToString() + " / " + pageCount.ToString();//当前页
            this.lbl_Rows.Text =  recordCount.ToString() + " 行";//总记录数

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.Yellow; // 设置为你想要的颜色

            ///把临时table的数据插入到datagridview控件里面。
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < dtTemp.Columns.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = dtTemp.Rows[i][j].ToString();
                }
                //根据订单号不同的行内容交替变换背景颜色
                if (!billno.Equals(dataGridView1.Rows[i].Cells[2].Value.ToString())&&colortype==1)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    billno = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    colortype = 2;
                }
                else if (!billno.Equals(dataGridView1.Rows[i].Cells[2].Value.ToString()) && colortype == 2)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    billno = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    colortype = 1;
                }
                else if(billno.Equals(dataGridView1.Rows[i].Cells[2].Value.ToString()))
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = dataGridView1.Rows[i - 1].DefaultCellStyle.BackColor;
                   
                }
            }

        }

        private void btn_chaxun_Click(object sender, EventArgs e)
        {
            
            string sql = @"select zz.fname as 采购组织,gys.fname as 供应商,d.FBILLNO as 订单号,CONVERT(VARCHAR(10),d.fdate,120) as 订单日期,m.FNUMBER as 物料编码,ml.fname as 物料名称,
                                cast(dd.FQTY as decimal(10,2)) as 订单数量,cast(dgl.FSTOCKINQTY as decimal(10,2)) as 已入库,cast(dgl.FREMAINSTOCKINQTY as decimal(10,2)) as 未入库,ul.fname as 单位
                                from T_PUR_POORDERENTRY dd inner join T_PUR_POORDER d on d.fid=dd.fid
                                inner join T_PUR_POORDERENTRY_R dgl on dgl.FENTRYID=dd.FENTRYID
                                inner join T_BD_MATERIAL m on m.FMATERIALID=dd.FMATERIALID
                                inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=m.FMATERIALID
                                inner join t_BD_Unit_L ul on dd.FUNITID=ul.FUNITID
                                inner join T_ORG_Organizations_L zz on zz.FORGID=d.FPURCHASEORGID
                                inner join t_BD_Supplier_L gys on gys.FSUPPLIERID=d.FSUPPLIERID
                                where DATEDIFF(DAY,d.FDATE,GETDATE())>=@wrkdays-1 and d.FDATE>=@begindate and d.fdate<=@enddate and gys.fname like '%"+tb_gys.Text+@"%' and d.FDocumentStatus='C' and d.FCLOSESTATUS='A'
                                and gys.fname not in ('江苏逅厨供应链管理有限公司','逅厨(上海)食品科技有限公司') and zz.fname=@cgzz
                                and dgl.FREMAINSTOCKINQTY>0 order by d.fdate";
            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("wrkdays", MyVar.Wrkdays), SQLHelperJD.MakeParam("begindate", begindate.Value.Date), SQLHelperJD.MakeParam("enddate", enddate.Value.Date)
                                       , SQLHelperJD.MakeParam("cgzz", cb_zgcc.Text));

            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                PageSorter();
            }
            else
            {
                dataGridView1.Rows.Clear();
                this.lbl_now.Text = " / ";//当前页
                this.lbl_Rows.Text =  "0行";//总记录数
            }
        }
    }
}
