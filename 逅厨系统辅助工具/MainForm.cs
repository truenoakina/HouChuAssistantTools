using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class MainForm : Form
    {

        private Icon houchu = Properties.Resources.houchu;
        private Icon blank = Properties.Resources.blank;//透明的图标 
        private bool _status = true;
        private bool _isBlink = false;
        //定时器，定时刷新过期资质提醒
        public  System.Timers.Timer t;
        //zizhiwarn窗体类
        ZizhiWarn zizhiWarn;
        //设置页面中是否定时提醒过期资质，及自动刷新时间
        public bool sysIsGQZZWARN;
        public int sysrefreshTime;
        //设置页面中是否定时提醒未入库订单，及自动刷新时间等
        public bool sysIsWRKWARN;
        public int sysWrkRefreshTime;   //未入库订单提醒间隔
        public DateTime notneedWarnDate;
        public int wrkdays;
        public System.Timers.Timer wrkt;//未入库提醒的时间间隔
        //WrkWarn窗体类
        WrkWarn wrkwarn;


        public MainForm(string[] args)
        {

            //if (args.Length < 1)
            //{
            //    MessageBox.Show("请从自动更新程序[HCToolAutoUpdate.exe]启动！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    System.Environment.Exit(0);
            //}
            //else
            //{
            //    if (args[0] != "AutoUpdateStart")
            //    {
            //        MessageBox.Show("请从自动更新程序[HCToolAutoUpdate.exe]启动！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        System.Environment.Exit(0);
            //    }
            //}

            InitializeComponent();


               
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lbl_nowuser.Text = "当前用户:" + LoginInfo.UserName;
            //资质提醒
            int refreshTime = 0;
            refreshTime = int.Parse(Properties.Settings.Default.zizhiRefreshTime.ToString());
            MyVar.Usefuldays = int.Parse(Properties.Settings.Default.usefulDays.ToString());
            t = new System.Timers.Timer(refreshTime * 60 * 1000);//实例化Timer类，设置时间间隔
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件
            t.Elapsed += new System.Timers.ElapsedEventHandler(this.GQZZrefresh);//到达时间的时候执行事件
            if (Properties.Settings.Default.isWarnzizhi == true)
            {  
                t.Start();
            }
            else
            {
                t.Stop();
            }

            //采购订单未入库提醒
            sysWrkRefreshTime = Properties.Settings.Default.wrkRefreshTime;
            MyVar.NotneedWarnDate= Properties.Settings.Default.notneedWarnDate;
            MyVar.Wrkdays = Properties.Settings.Default.wrkdays;
            wrkt = new System.Timers.Timer(sysWrkRefreshTime * 60 * 1000);
            wrkt.AutoReset = true;//设置是执行一次（false）还是一直执行(true)
            wrkt.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件
            wrkt.Elapsed += new System.Timers.ElapsedEventHandler(this.WRKDDrefresh);//到达时间的时候执行事件
            if (Properties.Settings.Default.iswrkWarn == true)
            {
                wrkt.Start();
            }
            else
            {
                wrkt.Stop();
            }
        }



        private void GQZZ()
        {
            ///供应商营业执照过期资质
            string sql = @"select zz.FBILLNO as 资质单编号,ml.FNAME as 物料名称,zzs.fuseorg as 使用组织,gys.FNAME as 供应商,g.F_PAEZ_TEXT3 as 营业执照,g.F_PAEZ_DATE6 as 到期日期,case when datediff(day,getdate(),g.F_PAEZ_DATE6)<=0 then '已到期' else convert(varchar,datediff(day,getdate(),g.F_PAEZ_DATE6)) end as 有效天数,
                            zzs.F_PAEZ_TEXT1 as 备注,case when zz.F_PAEZ_COMBO=1 then '是' else '否' end as 是否下架
                            from PAEZ_t_Cust_Entry100022 zzs inner join PAEZ_t_Cust100004 zz on zz.FID=zzs.FID
                            inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=zzs.F_PAEZ_BASE 
                            inner join t_BD_Supplier_L gys on gys.FSUPPLIERID=zzs.F_PAEZ_BASE1
                            inner join t_BD_Supplier g on g.FSUPPLIERID=zzs.F_PAEZ_BASE1
                            where g.FUSEORGID=1 and zz.FDOCUMENTSTATUS='C' and datediff(day,getdate(),g.F_PAEZ_DATE6)<=@usefuldays";
            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("usefuldays", MyVar.Usefuldays));
            GQZZINFO.Dtgysyyzz = ds.Tables[0];
            GQZZINFO.GysyyzzNum = ds.Tables[0].Rows.Count;

            //供应商经营许可证过期
            sql = @"select zz.FBILLNO as 资质单编号,ml.FNAME as 物料名称,zzs.fuseorg as 使用组织,gys.FNAME as 供应商,
                    g.F_PAEZ_TEXT2 as 经营许可证,g.F_PAEZ_DATE3 as 到期日期,case when datediff(day,getdate(),g.F_PAEZ_DATE3)<=0 then '已到期' else convert(varchar,datediff(day,getdate(),g.F_PAEZ_DATE3)) end as 有效天数,
                    zzs.F_PAEZ_TEXT1 as 备注,case when zz.F_PAEZ_COMBO=1 then '是' else '否' end as 是否下架
                    from PAEZ_t_Cust_Entry100022 zzs inner join PAEZ_t_Cust100004 zz on zz.FID=zzs.FID
                    inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=zzs.F_PAEZ_BASE 
                    inner join t_BD_Supplier_L gys on gys.FSUPPLIERID=zzs.F_PAEZ_BASE1
                    inner join t_BD_Supplier g on g.FSUPPLIERID=zzs.F_PAEZ_BASE1
                    where g.FUSEORGID=1 and zz.FDOCUMENTSTATUS='C' and datediff(day,getdate(),g.F_PAEZ_DATE3)<=@usefuldays";
            ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("usefuldays", MyVar.Usefuldays));
            GQZZINFO.Dtgysjyxkz = ds.Tables[0];
            GQZZINFO.GysjyxkzNum = ds.Tables[0].Rows.Count;

            //生产商营业执照过期
            sql = @"select zz.FBILLNO as 资质单编号,ml.FNAME as 物料名称,zzs.fuseorg as 使用组织,scs.fname as 生产商,sc.F_PAEZ_TEXT1 as 生产营业执照,sc.F_PAEZ_DATE as 到期日期,
                    case when datediff(day,getdate(),sc.F_PAEZ_DATE)<=0 then '已到期' else convert(varchar,datediff(day,getdate(),sc.F_PAEZ_DATE)) end as 有效天数,
                    zzs.F_PAEZ_TEXT1 as 备注,case when zz.F_PAEZ_COMBO=1 then '是' else '否' end as 是否下架
                    from PAEZ_t_Cust_Entry100022 zzs inner join PAEZ_t_Cust100004 zz on zz.FID=zzs.FID
                    inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=zzs.F_PAEZ_BASE 
                    inner join T_BAS_PREBDONE_L scs on scs.FID=zzs.F_PAEZ_Base2
                    inner join T_BAS_PREBDONE sc on sc.FID=zzs.F_PAEZ_Base2
                    where zz.FDOCUMENTSTATUS='C' and datediff(day,getdate(),sc.F_PAEZ_DATE)<=@usefuldays";
            ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("usefuldays", MyVar.Usefuldays));
            GQZZINFO.Dtscsyyzz = ds.Tables[0];
            GQZZINFO.ScsyyzzNum = ds.Tables[0].Rows.Count;

            //生产商生产许可证过期
            sql = @"select zz.FBILLNO as 资质单编号,ml.FNAME as 物料名称,zzs.fuseorg as 使用组织,scs.fname as 生产商,
                    case when datediff(day,getdate(),sc.F_PAEZ_DATE1)<=0 then '已到期' else convert(varchar,datediff(day,getdate(),sc.F_PAEZ_DATE1)) end as 有效天数,
                    zzs.F_PAEZ_TEXT1 as 备注,case when zz.F_PAEZ_COMBO=1 then '是' else '否' end as 是否下架
                    from PAEZ_t_Cust_Entry100022 zzs inner join PAEZ_t_Cust100004 zz on zz.FID=zzs.FID
                    inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=zzs.F_PAEZ_BASE 
                    inner join T_BAS_PREBDONE_L scs on scs.FID=zzs.F_PAEZ_Base2
                    inner join T_BAS_PREBDONE sc on sc.FID=zzs.F_PAEZ_Base2
                    where zz.FDOCUMENTSTATUS='C' and datediff(day,getdate(),sc.F_PAEZ_DATE1)<=@usefuldays";
            ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("@usefuldays",MyVar.Usefuldays));
            GQZZINFO.Dtscsscxkz = ds.Tables[0];
            GQZZINFO.ScsscxkzNum = ds.Tables[0].Rows.Count;


            //型检报告过期
            sql = @"select zz.FBILLNO as 资质单编号,ml.FNAME as 物料名称,zzs.fuseorg as 使用组织,gys.FNAME as 供应商,scs.fname as 生产商,
                    zzs.F_PAEZ_DATE2 as 型检日期,zzs.F_PAEZ_TEXT as 执行标准,floor(zzs.F_PAEZ_DECIMAL) as 有效期,
                  DATEADD(DAY,zzs.F_PAEZ_DECIMAL,zzs.F_PAEZ_DATE2) as 到期日期,
                  case when  datediff(day,getdate(),DATEADD(DAY,zzs.F_PAEZ_DECIMAL,zzs.F_PAEZ_DATE2))<=0 then '已到期' else convert(varchar,datediff(day,getdate(),DATEADD(DAY,zzs.F_PAEZ_DECIMAL,zzs.F_PAEZ_DATE2))) end as 有效天数,
                    zzs.F_PAEZ_TEXT1 as 备注,case when zz.F_PAEZ_COMBO=1 then '是' else '否' end as 是否下架
                    from PAEZ_t_Cust_Entry100022 zzs inner join PAEZ_t_Cust100004 zz on zz.FID=zzs.FID
                    inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=zzs.F_PAEZ_BASE
                    inner join t_BD_Supplier_L gys on gys.FSUPPLIERID=zzs.F_PAEZ_BASE1
                    inner join T_BAS_PREBDONE_L scs on scs.FID=zzs.F_PAEZ_Base2
                    where zz.FDOCUMENTSTATUS='C' and datediff(day,getdate(),DATEADD(DAY,zzs.F_PAEZ_DECIMAL,zzs.F_PAEZ_DATE2))<=@usefuldays";
            ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("usefuldays",MyVar.Usefuldays));
            GQZZINFO.Dtxjbg = ds.Tables[0];
            GQZZINFO.XjbgNum = ds.Tables[0].Rows.Count;
        }

        //查询过期资质
        public void GQZZrefresh(object source, System.Timers.ElapsedEventArgs e)
        {
            GQZZ();
            
            //判断弹出的窗体是否关闭，如没关闭，则激活，否则重新打开窗体
            if (zizhiWarn == null || zizhiWarn.IsDisposed)
            {
                zizhiWarn = new ZizhiWarn();
                zizhiWarn.ShowDialog();
            }
            //else
            //{
            //    zizhiWarn.Activate();
            //}
        }

        private void WRKDD()
        { 
           //江苏逅厨，上海逅厨未入库订单
           string sql= @"select zz.fname as 采购组织,gys.fname as 供应商,d.FBILLNO as 订单号,CONVERT(VARCHAR(10),d.fdate,120) as 订单日期,m.FNUMBER as 物料编码,ml.fname as 物料名称,
                                cast(dd.FQTY as decimal(10,2)) as 订单数量,cast(dgl.FSTOCKINQTY as decimal(10,2)) as 已入库,cast(dgl.FREMAINSTOCKINQTY as decimal(10,2)) as 未入库,ul.fname as 单位
                                from T_PUR_POORDERENTRY dd inner join T_PUR_POORDER d on d.fid=dd.fid
                                inner join T_PUR_POORDERENTRY_R dgl on dgl.FENTRYID=dd.FENTRYID
                                inner join T_BD_MATERIAL m on m.FMATERIALID=dd.FMATERIALID
                                inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=m.FMATERIALID
                                inner join t_BD_Unit_L ul on dd.FUNITID=ul.FUNITID
                                inner join T_ORG_Organizations_L zz on zz.FORGID=d.FPURCHASEORGID
                                inner join t_BD_Supplier_L gys on gys.FSUPPLIERID=d.FSUPPLIERID
                                where DATEDIFF(DAY,d.FDATE,GETDATE())>=@wrkdays-1 and d.FDATE>=@fdate and d.FDocumentStatus='C' and d.FCLOSESTATUS='A'
                                and zz.FNAME not in ('广东逅厨食品有限公司') and gys.fname not in ('江苏逅厨供应链管理有限公司','逅厨(上海)食品科技有限公司') and d.FPURCHASEORGID in (100005,342278)
                                and dgl.FREMAINSTOCKINQTY>0";
            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("wrkdays", MyVar.Wrkdays), SQLHelperJD.MakeParam("fdate", MyVar.NotneedWarnDate));
            WrkDdInfo.Dthouchuwrk = ds.Tables[0];
            WrkDdInfo.Houchuwrk= ds.Tables[0].Rows.Count;

            //佳之家未入库订单
            sql = @"select zz.fname as 采购组织,gys.fname as 供应商,d.FBILLNO as 订单号,CONVERT(VARCHAR(10),d.fdate,120) as 订单日期,m.FNUMBER as 物料编码,ml.fname as 物料名称,
                                cast(dd.FQTY as decimal(10,2)) as 订单数量,cast(dgl.FSTOCKINQTY as decimal(10,2)) as 已入库,cast(dgl.FREMAINSTOCKINQTY as decimal(10,2)) as 未入库,ul.fname as 单位
                                from T_PUR_POORDERENTRY dd inner join T_PUR_POORDER d on d.fid=dd.fid
                                inner join T_PUR_POORDERENTRY_R dgl on dgl.FENTRYID=dd.FENTRYID
                                inner join T_BD_MATERIAL m on m.FMATERIALID=dd.FMATERIALID
                                inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=m.FMATERIALID
                                inner join t_BD_Unit_L ul on dd.FUNITID=ul.FUNITID
                                inner join T_ORG_Organizations_L zz on zz.FORGID=d.FPURCHASEORGID
                                inner join t_BD_Supplier_L gys on gys.FSUPPLIERID=d.FSUPPLIERID
                                where DATEDIFF(DAY,d.FDATE,GETDATE())>=@wrkdays-1 and d.FDATE>=@fdate and d.FDocumentStatus='C' and d.FCLOSESTATUS='A'
                                and zz.FNAME not in ('广东逅厨食品有限公司') and gys.fname not in ('江苏逅厨供应链管理有限公司','逅厨(上海)食品科技有限公司') and d.FPURCHASEORGID=100006
                                and dgl.FREMAINSTOCKINQTY>0";
            ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("wrkdays", MyVar.Wrkdays), SQLHelperJD.MakeParam("fdate", MyVar.NotneedWarnDate));
            WrkDdInfo.Dtjzjwrk = ds.Tables[0];
            WrkDdInfo.Jzjwrk = ds.Tables[0].Rows.Count;

            //谷之田未入库订单
            sql = @"select zz.fname as 采购组织,gys.fname as 供应商,d.FBILLNO as 订单号,CONVERT(VARCHAR(10),d.fdate,120) as 订单日期,m.FNUMBER as 物料编码,ml.fname as 物料名称,
                                cast(dd.FQTY as decimal(10,2)) as 订单数量,cast(dgl.FSTOCKINQTY as decimal(10,2)) as 已入库,cast(dgl.FREMAINSTOCKINQTY as decimal(10,2)) as 未入库,ul.fname as 单位
                                from T_PUR_POORDERENTRY dd inner join T_PUR_POORDER d on d.fid=dd.fid
                                inner join T_PUR_POORDERENTRY_R dgl on dgl.FENTRYID=dd.FENTRYID
                                inner join T_BD_MATERIAL m on m.FMATERIALID=dd.FMATERIALID
                                inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=m.FMATERIALID
                                inner join t_BD_Unit_L ul on dd.FUNITID=ul.FUNITID
                                inner join T_ORG_Organizations_L zz on zz.FORGID=d.FPURCHASEORGID
                                inner join t_BD_Supplier_L gys on gys.FSUPPLIERID=d.FSUPPLIERID
                                where DATEDIFF(DAY,d.FDATE,GETDATE())>=@wrkdays-1 and d.FDATE>=@fdate and d.FDocumentStatus='C' and d.FCLOSESTATUS='A'
                                and zz.FNAME not in ('广东逅厨食品有限公司') and gys.fname not in ('江苏逅厨供应链管理有限公司','逅厨(上海)食品科技有限公司') and d.FPURCHASEORGID=100007
                                and dgl.FREMAINSTOCKINQTY>0";
            ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("wrkdays", MyVar.Wrkdays), SQLHelperJD.MakeParam("fdate", MyVar.NotneedWarnDate));
            WrkDdInfo.Dtgztwrk = ds.Tables[0];
            WrkDdInfo.Gztwrk = ds.Tables[0].Rows.Count;

            //好利森未入库订单
            sql = @"select zz.fname as 采购组织,gys.fname as 供应商,d.FBILLNO as 订单号,CONVERT(VARCHAR(10),d.fdate,120) as 订单日期,m.FNUMBER as 物料编码,ml.fname as 物料名称,
                                cast(dd.FQTY as decimal(10,2)) as 订单数量,cast(dgl.FSTOCKINQTY as decimal(10,2)) as 已入库,cast(dgl.FREMAINSTOCKINQTY as decimal(10,2)) as 未入库,ul.fname as 单位
                                from T_PUR_POORDERENTRY dd inner join T_PUR_POORDER d on d.fid=dd.fid
                                inner join T_PUR_POORDERENTRY_R dgl on dgl.FENTRYID=dd.FENTRYID
                                inner join T_BD_MATERIAL m on m.FMATERIALID=dd.FMATERIALID
                                inner join T_BD_MATERIAL_L ml on ml.FMATERIALID=m.FMATERIALID
                                inner join t_BD_Unit_L ul on dd.FUNITID=ul.FUNITID
                                inner join T_ORG_Organizations_L zz on zz.FORGID=d.FPURCHASEORGID
                                inner join t_BD_Supplier_L gys on gys.FSUPPLIERID=d.FSUPPLIERID
                                where DATEDIFF(DAY,d.FDATE,GETDATE())>=@wrkdays-1 and d.FDATE>=@fdate and d.FDocumentStatus='C' and d.FCLOSESTATUS='A'
                                and zz.FNAME not in ('广东逅厨食品有限公司') and gys.fname not in ('江苏逅厨供应链管理有限公司','逅厨(上海)食品科技有限公司') and d.FPURCHASEORGID=286780
                                and dgl.FREMAINSTOCKINQTY>0";
            ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("wrkdays", MyVar.Wrkdays), SQLHelperJD.MakeParam("fdate", MyVar.NotneedWarnDate));
            WrkDdInfo.Dthlswrk = ds.Tables[0];
            WrkDdInfo.Hlswrk = ds.Tables[0].Rows.Count;
        }

        //查询未入库订单
        public void WRKDDrefresh(object source, System.Timers.ElapsedEventArgs e)
        {
            WRKDD();

            //判断弹出的窗体是否关闭，如没关闭，则激活，否则重新打开窗体
            if (wrkwarn == null || wrkwarn.IsDisposed)
            {
                wrkwarn = new WrkWarn();
                wrkwarn.ShowDialog();
            }
            //else
            //{
            //    wrkwarn.Activate();
            //}  //注释掉，会引起线程错误
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OutOrderOrgChoose outOrderOrgChoose = new OutOrderOrgChoose();
            outOrderOrgChoose.ShowDialog();

        }

        private void BarcodeTest_Click(object sender, EventArgs e)
        {
            //ChooseType chooseType = new ChooseType();
            //chooseType.Show();
            Test20 test20 = new Test20();
            test20.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GZTASS gztAss = new GZTASS();
            gztAss.ShowDialog();
        }

        private void KQSJZH_Click(object sender, EventArgs e)
        {
            HRKQSJFORMAT hrkgsjformat = new HRKQSJFORMAT();
            hrkgsjformat.ShowDialog();
        }

        private void LSDDZH_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void WBDDZH_Click(object sender, EventArgs e)
        {
            OutOrder outOrder = new OutOrder();
            outOrder.ShowDialog();
        }

        private void TXMCS_Click(object sender, EventArgs e)
        {
            Test20 test20 = new Test20();
            test20.ShowDialog();
        }

        private void GZTFZCX_Click(object sender, EventArgs e)
        {
            GZTASS gztAss = new GZTASS();
            gztAss.ShowDialog();
        }

        private void SCZLD_Click(object sender, EventArgs e)
        {
            SCZLDMAIN sczldmain = new SCZLDMAIN();
            sczldmain.ShowDialog();
        }

        private void YJBOMSC_Click(object sender, EventArgs e)
        {
            Level1BOM level1BOM = new Level1BOM();
            level1BOM.ShowDialog();
        }

        private void YKTUPDATE_Click(object sender, EventArgs e)
        {
            YKTupdate yktupdate = new YKTupdate();
            yktupdate.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;           //取消关闭操作 表现为不关闭窗体  
                this.Hide();               //隐藏窗体  
            }
        }

        private void show_Click(object sender, EventArgs e)
        {
            this.Show();                                //窗体显示  
            this.WindowState = FormWindowState.Normal;  //窗体状态默认大小  
            this.Activate();
        }

        private void hide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            //点击"是(YES)"退出程序  
            if (MessageBox.Show("确定要退出程序?", "安全提示",
        System.Windows.Forms.MessageBoxButtons.YesNo,
        System.Windows.Forms.MessageBoxIcon.Warning)
        == System.Windows.Forms.DialogResult.Yes)
            {
                notifyIcon1.Visible = false;   //设置图标不可见  
                this.Close();                  //关闭窗体  
                this.Dispose();                //释放资源  
                Application.Exit();            //关闭应用程序窗体  
            }
        }


        /// <summary>
        /// 单击图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //点击鼠标"左键"发生  
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;                        //窗体可见  
                this.WindowState = FormWindowState.Normal;  //窗体默认大小  
                this.notifyIcon1.Visible = true;            //设置图标可见  
            }
        }

        private void blink_Click(object sender, EventArgs e)
        {
            if (_isBlink == false)
            {
                _isBlink = true;
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                _isBlink = false;
                timer1.Stop();
            }
            notifyIcon1.Visible = true;
            notifyIcon1.Icon = houchu;
            //ZizhiWarn zizhiWarn = new ZizhiWarn();
            //zizhiWarn.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_status)
                notifyIcon1.Icon = houchu;
            else
                notifyIcon1.Icon = blank;

            _status = !_status;
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FC_PRODUCR_INFO fC_PRODUCR_INFO = new FC_PRODUCR_INFO();
            fC_PRODUCR_INFO.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
                 
        }

        private void 采购单价趋势图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CGDJQST cGDJQST = new CGDJQST();
            cGDJQST.ShowDialog();
           
        }

        //设置页面之后的反馈
        private void btn_set_Click(object sender, EventArgs e)
        {
            SystemSet systemSet = new SystemSet();
            if (systemSet.ShowDialog(this)== DialogResult.OK)
            {
                //资质提醒
                if (sysIsGQZZWARN == true)
                {
                    try
                    {
                        t.Interval = sysrefreshTime * 60 * 1000;
                        t.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                else if (sysIsGQZZWARN == false)
                {
                    try
                    {
                        t.Stop();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

                //未入库提醒
                if (sysIsWRKWARN == true)
                {
                    try
                    {
                        wrkt.Interval = sysWrkRefreshTime * 60 * 1000;
                        wrkt.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                else if (sysIsWRKWARN == false)
                {
                    try
                    {
                        wrkt.Stop();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }

            }
            else
            {
                return;
            }
            
        }

        private void GQZZCX_Click(object sender, EventArgs e)
        {
            GQZZ();
            GQZZDETAIL gQZZDETAIL = new GQZZDETAIL();
            gQZZDETAIL.Show();
        }

       

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.isWarnzizhi == true)
            {
                //如果设置了过期资质自动提醒，则第一次登陆时自动弹出提醒
                GQZZ();

                //判断弹出的窗体是否关闭，如没关闭，则激活，否则重新打开窗体
                if (zizhiWarn == null || zizhiWarn.IsDisposed)
                {
                    zizhiWarn = new ZizhiWarn();
                    zizhiWarn.Show();
                }
                else
                {
                    zizhiWarn.Activate();
                }
            }

            if (Properties.Settings.Default.iswrkWarn == true)
            {
                //如果设置了订单未入库自动提醒，则第一次登陆时自动弹出提醒
                WRKDD();

                //判断弹出的窗体是否关闭，如没关闭，则激活，否则重新打开窗体
                if (wrkwarn == null || wrkwarn.IsDisposed)
                {
                    wrkwarn = new WrkWarn();
                    wrkwarn.Show();
                }
                else
                {
                    wrkwarn.Activate();
                }
            }
            else
            { }
        }

        private void btn_changepassword_Click(object sender, EventArgs e)
        {
            ChangePassWord changePassWord = new ChangePassWord();
            changePassWord.ShowDialog();
        }

        private void about_Click(object sender, EventArgs e)
        {

        }

        private void YKTSKRECORD_Click(object sender, EventArgs e)
        {
            YKTSKRECORD yKTSKRECORD = new YKTSKRECORD();
            yKTSKRECORD.ShowDialog();
        }

        private void btn_hcreport_Click(object sender, EventArgs e)
        {
            HCReports hcreports = new HCReports();
            hcreports.ShowDialog();
        }

        private void refreshreport_Click(object sender, EventArgs e)
        {
            RefreshReport refreshReport = new RefreshReport();
            refreshReport.ShowDialog();
        }

        private void SCcalendarform_Click(object sender, EventArgs e)
        {
            CalendarForm calendarForm = new CalendarForm();
            calendarForm.Show();
        }

        private void TXMCSHLS_Click(object sender, EventArgs e)
        {
            Test20HLS test20hls = new Test20HLS();
            test20hls.ShowDialog();
        }

        private void cgwrkcx_Click(object sender, EventArgs e)
        {
            WrkDdInfo.Cgzz = 0;//从功能页面进入
            WrkDetail wrkDetail = new WrkDetail();
            wrkDetail.ShowDialog();
        }

        private void JSHCDKMXB_Click(object sender, EventArgs e)
        {
            JSHCDKMXB jSHCDKMXB = new JSHCDKMXB();
            jSHCDKMXB.ShowDialog();
        }

        private void MaterialDayUse_Click(object sender, EventArgs e)
        {
            MaterialDayUse materialDayUse = new MaterialDayUse();
            materialDayUse.ShowDialog();
        }

        private void btn_school_Click(object sender, EventArgs e)
        {
            SchoolMenu schoolMenu = new SchoolMenu();
            schoolMenu.ShowDialog();
        }

        private void btn_FactoryDayData_Click(object sender, EventArgs e)
        {
            AttendanceConfig attendanceConfig = new AttendanceConfig();
            if (attendanceConfig.ShowDialog() == DialogResult.OK)
            {
                FDD_Main fDD_Main = new FDD_Main();
                fDD_Main.ShowDialog();
            }
        }

        private void SMCPCCJYBGPLDY_Click(object sender, EventArgs e)
        {
            SNCK_InspectionReport sNCK_InspectionReport = new SNCK_InspectionReport();
            sNCK_InspectionReport.ShowDialog();
        }
    }
}
