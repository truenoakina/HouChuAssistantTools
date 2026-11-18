namespace 逅厨系统辅助工具
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.BarcodeTest = new System.Windows.Forms.Button();
            this.menuFunc = new System.Windows.Forms.MenuStrip();
            this.FuncList = new System.Windows.Forms.ToolStripMenuItem();
            this.LSDDZH = new System.Windows.Forms.ToolStripMenuItem();
            this.WBDDZH = new System.Windows.Forms.ToolStripMenuItem();
            this.TXMCS = new System.Windows.Forms.ToolStripMenuItem();
            this.TXMCSHLS = new System.Windows.Forms.ToolStripMenuItem();
            this.GZTFZCX = new System.Windows.Forms.ToolStripMenuItem();
            this.BOMXGHLCX = new System.Windows.Forms.ToolStripMenuItem();
            this.KQSJZH = new System.Windows.Forms.ToolStripMenuItem();
            this.SCZLD = new System.Windows.Forms.ToolStripMenuItem();
            this.YJBOMSC = new System.Windows.Forms.ToolStripMenuItem();
            this.YKTUPDATE = new System.Windows.Forms.ToolStripMenuItem();
            this.YKTSKRECORD = new System.Windows.Forms.ToolStripMenuItem();
            this.CGDJQST = new System.Windows.Forms.ToolStripMenuItem();
            this.GQZZCX = new System.Windows.Forms.ToolStripMenuItem();
            this.SCcalendarform = new System.Windows.Forms.ToolStripMenuItem();
            this.cgwrkcx = new System.Windows.Forms.ToolStripMenuItem();
            this.JSHCDKMXB = new System.Windows.Forms.ToolStripMenuItem();
            this.zkIfaceManage = new System.Windows.Forms.ToolStripMenuItem();
            this.MaterialDayUse = new System.Windows.Forms.ToolStripMenuItem();
            this.SMCPCCJYBGPLDY = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_set = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_changePW = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_refreshreport = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_changepassword = new System.Windows.Forms.ToolStripMenuItem();
            this.about = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.show = new System.Windows.Forms.ToolStripMenuItem();
            this.hide = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.lbl_nowuser = new System.Windows.Forms.Label();
            this.btn_hcreport = new System.Windows.Forms.Button();
            this.btn_school = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_FactoryDayData = new System.Windows.Forms.Button();
            this.menuFunc.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.BorderSize = 3;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(80, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "罗森订单转换";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Highlight;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button2.FlatAppearance.BorderSize = 3;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(80, 107);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "外部订单转换";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BarcodeTest
            // 
            this.BarcodeTest.BackColor = System.Drawing.SystemColors.Highlight;
            this.BarcodeTest.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.BarcodeTest.FlatAppearance.BorderSize = 3;
            this.BarcodeTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BarcodeTest.ForeColor = System.Drawing.Color.White;
            this.BarcodeTest.Location = new System.Drawing.Point(80, 179);
            this.BarcodeTest.Name = "BarcodeTest";
            this.BarcodeTest.Size = new System.Drawing.Size(177, 40);
            this.BarcodeTest.TabIndex = 2;
            this.BarcodeTest.Text = "条形码测试";
            this.BarcodeTest.UseVisualStyleBackColor = false;
            this.BarcodeTest.Click += new System.EventHandler(this.BarcodeTest_Click);
            // 
            // menuFunc
            // 
            this.menuFunc.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuFunc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FuncList,
            this.btn_set,
            this.btn_changePW});
            this.menuFunc.Location = new System.Drawing.Point(0, 0);
            this.menuFunc.Name = "menuFunc";
            this.menuFunc.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuFunc.Size = new System.Drawing.Size(354, 25);
            this.menuFunc.TabIndex = 4;
            this.menuFunc.Text = "功能列表";
            // 
            // FuncList
            // 
            this.FuncList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LSDDZH,
            this.WBDDZH,
            this.TXMCS,
            this.TXMCSHLS,
            this.GZTFZCX,
            this.BOMXGHLCX,
            this.KQSJZH,
            this.SCZLD,
            this.YJBOMSC,
            this.YKTUPDATE,
            this.YKTSKRECORD,
            this.CGDJQST,
            this.GQZZCX,
            this.SCcalendarform,
            this.cgwrkcx,
            this.JSHCDKMXB,
            this.zkIfaceManage,
            this.MaterialDayUse,
            this.SMCPCCJYBGPLDY});
            this.FuncList.Name = "FuncList";
            this.FuncList.Size = new System.Drawing.Size(68, 21);
            this.FuncList.Text = "功能列表";
            // 
            // LSDDZH
            // 
            this.LSDDZH.Name = "LSDDZH";
            this.LSDDZH.Size = new System.Drawing.Size(253, 22);
            this.LSDDZH.Text = "罗森订单转换";
            this.LSDDZH.Click += new System.EventHandler(this.LSDDZH_Click);
            // 
            // WBDDZH
            // 
            this.WBDDZH.Name = "WBDDZH";
            this.WBDDZH.Size = new System.Drawing.Size(253, 22);
            this.WBDDZH.Text = "外部订单转换";
            this.WBDDZH.Click += new System.EventHandler(this.WBDDZH_Click);
            // 
            // TXMCS
            // 
            this.TXMCS.Name = "TXMCS";
            this.TXMCS.Size = new System.Drawing.Size(253, 22);
            this.TXMCS.Text = "条形码测试";
            this.TXMCS.Click += new System.EventHandler(this.TXMCS_Click);
            // 
            // TXMCSHLS
            // 
            this.TXMCSHLS.Name = "TXMCSHLS";
            this.TXMCSHLS.Size = new System.Drawing.Size(253, 22);
            this.TXMCSHLS.Text = "条形码测试(好利森)";
            this.TXMCSHLS.Click += new System.EventHandler(this.TXMCSHLS_Click);
            // 
            // GZTFZCX
            // 
            this.GZTFZCX.Name = "GZTFZCX";
            this.GZTFZCX.Size = new System.Drawing.Size(253, 22);
            this.GZTFZCX.Text = "谷之田辅助查询";
            this.GZTFZCX.Click += new System.EventHandler(this.GZTFZCX_Click);
            // 
            // BOMXGHLCX
            // 
            this.BOMXGHLCX.Name = "BOMXGHLCX";
            this.BOMXGHLCX.Size = new System.Drawing.Size(253, 22);
            this.BOMXGHLCX.Text = "BOM修改记录查询";
            // 
            // KQSJZH
            // 
            this.KQSJZH.Name = "KQSJZH";
            this.KQSJZH.Size = new System.Drawing.Size(253, 22);
            this.KQSJZH.Text = "考勤数据转换(好利森)";
            this.KQSJZH.Click += new System.EventHandler(this.KQSJZH_Click);
            // 
            // SCZLD
            // 
            this.SCZLD.Name = "SCZLD";
            this.SCZLD.Size = new System.Drawing.Size(253, 22);
            this.SCZLD.Text = "生产指令单";
            this.SCZLD.Click += new System.EventHandler(this.SCZLD_Click);
            // 
            // YJBOMSC
            // 
            this.YJBOMSC.Name = "YJBOMSC";
            this.YJBOMSC.Size = new System.Drawing.Size(253, 22);
            this.YJBOMSC.Text = "一阶BOM导入模板生成";
            this.YJBOMSC.Click += new System.EventHandler(this.YJBOMSC_Click);
            // 
            // YKTUPDATE
            // 
            this.YKTUPDATE.Name = "YKTUPDATE";
            this.YKTUPDATE.Size = new System.Drawing.Size(253, 22);
            this.YKTUPDATE.Text = "一卡通待写入员工异常处理";
            this.YKTUPDATE.Click += new System.EventHandler(this.YKTUPDATE_Click);
            // 
            // YKTSKRECORD
            // 
            this.YKTSKRECORD.Name = "YKTSKRECORD";
            this.YKTSKRECORD.Size = new System.Drawing.Size(253, 22);
            this.YKTSKRECORD.Text = "食堂消费刷卡记录";
            this.YKTSKRECORD.Click += new System.EventHandler(this.YKTSKRECORD_Click);
            // 
            // CGDJQST
            // 
            this.CGDJQST.Name = "CGDJQST";
            this.CGDJQST.Size = new System.Drawing.Size(253, 22);
            this.CGDJQST.Text = "采购单价趋势图";
            this.CGDJQST.Click += new System.EventHandler(this.采购单价趋势图ToolStripMenuItem_Click);
            // 
            // GQZZCX
            // 
            this.GQZZCX.Name = "GQZZCX";
            this.GQZZCX.Size = new System.Drawing.Size(253, 22);
            this.GQZZCX.Text = "过期资质查询";
            this.GQZZCX.Click += new System.EventHandler(this.GQZZCX_Click);
            // 
            // SCcalendarform
            // 
            this.SCcalendarform.Name = "SCcalendarform";
            this.SCcalendarform.Size = new System.Drawing.Size(253, 22);
            this.SCcalendarform.Text = "生产每日数据";
            this.SCcalendarform.Click += new System.EventHandler(this.SCcalendarform_Click);
            // 
            // cgwrkcx
            // 
            this.cgwrkcx.Name = "cgwrkcx";
            this.cgwrkcx.Size = new System.Drawing.Size(253, 22);
            this.cgwrkcx.Text = "采购订单未入库查询";
            this.cgwrkcx.Click += new System.EventHandler(this.cgwrkcx_Click);
            // 
            // JSHCDKMXB
            // 
            this.JSHCDKMXB.Name = "JSHCDKMXB";
            this.JSHCDKMXB.Size = new System.Drawing.Size(253, 22);
            this.JSHCDKMXB.Text = "打卡明细表(江苏逅厨)";
            this.JSHCDKMXB.Click += new System.EventHandler(this.JSHCDKMXB_Click);
            // 
            // zkIfaceManage
            // 
            this.zkIfaceManage.Name = "zkIfaceManage";
            this.zkIfaceManage.Size = new System.Drawing.Size(253, 22);
            this.zkIfaceManage.Text = "中控考勤机管理";
            // 
            // MaterialDayUse
            // 
            this.MaterialDayUse.Name = "MaterialDayUse";
            this.MaterialDayUse.Size = new System.Drawing.Size(253, 22);
            this.MaterialDayUse.Text = "原料每日使用复核";
            this.MaterialDayUse.Click += new System.EventHandler(this.MaterialDayUse_Click);
            // 
            // SMCPCCJYBGPLDY
            // 
            this.SMCPCCJYBGPLDY.Name = "SMCPCCJYBGPLDY";
            this.SMCPCCJYBGPLDY.Size = new System.Drawing.Size(253, 22);
            this.SMCPCCJYBGPLDY.Text = "SNCK产品出厂检验报告批量打印";
            this.SMCPCCJYBGPLDY.Click += new System.EventHandler(this.SMCPCCJYBGPLDY_Click);
            // 
            // btn_set
            // 
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(44, 21);
            this.btn_set.Text = "设置";
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // btn_changePW
            // 
            this.btn_changePW.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_refreshreport,
            this.btn_changepassword,
            this.about});
            this.btn_changePW.Name = "btn_changePW";
            this.btn_changePW.Size = new System.Drawing.Size(44, 21);
            this.btn_changePW.Text = "其他";
            // 
            // btn_refreshreport
            // 
            this.btn_refreshreport.Name = "btn_refreshreport";
            this.btn_refreshreport.Size = new System.Drawing.Size(124, 22);
            this.btn_refreshreport.Text = "更新报表";
            this.btn_refreshreport.Click += new System.EventHandler(this.refreshreport_Click);
            // 
            // btn_changepassword
            // 
            this.btn_changepassword.Name = "btn_changepassword";
            this.btn_changepassword.Size = new System.Drawing.Size(124, 22);
            this.btn_changepassword.Text = "修改密码";
            this.btn_changepassword.Click += new System.EventHandler(this.btn_changepassword_Click);
            // 
            // about
            // 
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(124, 22);
            this.about.Text = "关于";
            this.about.Click += new System.EventHandler(this.about_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 507);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "ver5.6";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "111";
            this.notifyIcon1.BalloonTipTitle = "111";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "逅厨系统辅助工具";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.show,
            this.hide,
            this.exit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // show
            // 
            this.show.Name = "show";
            this.show.Size = new System.Drawing.Size(124, 22);
            this.show.Text = "显示窗体";
            this.show.Click += new System.EventHandler(this.show_Click);
            // 
            // hide
            // 
            this.hide.Name = "hide";
            this.hide.Size = new System.Drawing.Size(124, 22);
            this.hide.Text = "隐藏窗体";
            this.hide.Click += new System.EventHandler(this.hide_Click);
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(124, 22);
            this.exit.Text = "退出";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Highlight;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button4.FlatAppearance.BorderSize = 3;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(263, 274);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 40);
            this.button4.TabIndex = 6;
            this.button4.Text = "团餐订单原料信息";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lbl_nowuser
            // 
            this.lbl_nowuser.AutoSize = true;
            this.lbl_nowuser.Location = new System.Drawing.Point(-2, 507);
            this.lbl_nowuser.Name = "lbl_nowuser";
            this.lbl_nowuser.Size = new System.Drawing.Size(53, 12);
            this.lbl_nowuser.TabIndex = 8;
            this.lbl_nowuser.Text = "username";
            // 
            // btn_hcreport
            // 
            this.btn_hcreport.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_hcreport.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btn_hcreport.FlatAppearance.BorderSize = 3;
            this.btn_hcreport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_hcreport.ForeColor = System.Drawing.Color.White;
            this.btn_hcreport.Location = new System.Drawing.Point(80, 247);
            this.btn_hcreport.Name = "btn_hcreport";
            this.btn_hcreport.Size = new System.Drawing.Size(177, 40);
            this.btn_hcreport.TabIndex = 9;
            this.btn_hcreport.Text = "逅 厨 报 表";
            this.btn_hcreport.UseVisualStyleBackColor = false;
            this.btn_hcreport.Click += new System.EventHandler(this.btn_hcreport_Click);
            // 
            // btn_school
            // 
            this.btn_school.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_school.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btn_school.FlatAppearance.BorderSize = 3;
            this.btn_school.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_school.ForeColor = System.Drawing.Color.White;
            this.btn_school.Location = new System.Drawing.Point(80, 315);
            this.btn_school.Name = "btn_school";
            this.btn_school.Size = new System.Drawing.Size(177, 40);
            this.btn_school.TabIndex = 10;
            this.btn_school.Text = "学校订单";
            this.btn_school.UseVisualStyleBackColor = false;
            this.btn_school.Click += new System.EventHandler(this.btn_school_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Highlight;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button3.FlatAppearance.BorderSize = 3;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(80, 384);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(177, 40);
            this.button3.TabIndex = 11;
            this.button3.Text = "广东逅厨";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // btn_FactoryDayData
            // 
            this.btn_FactoryDayData.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_FactoryDayData.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btn_FactoryDayData.FlatAppearance.BorderSize = 3;
            this.btn_FactoryDayData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_FactoryDayData.ForeColor = System.Drawing.Color.White;
            this.btn_FactoryDayData.Location = new System.Drawing.Point(80, 451);
            this.btn_FactoryDayData.Name = "btn_FactoryDayData";
            this.btn_FactoryDayData.Size = new System.Drawing.Size(177, 40);
            this.btn_FactoryDayData.TabIndex = 12;
            this.btn_FactoryDayData.Text = "生 产 数 据";
            this.btn_FactoryDayData.UseVisualStyleBackColor = false;
            this.btn_FactoryDayData.Click += new System.EventHandler(this.btn_FactoryDayData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(354, 530);
            this.Controls.Add(this.btn_FactoryDayData);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_school);
            this.Controls.Add(this.btn_hcreport);
            this.Controls.Add(this.lbl_nowuser);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BarcodeTest);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuFunc);
            this.MainMenuStrip = this.menuFunc;
            this.Name = "MainForm";
            this.Text = "逅厨系统辅助工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuFunc.ResumeLayout(false);
            this.menuFunc.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button BarcodeTest;
        private System.Windows.Forms.MenuStrip menuFunc;
        private System.Windows.Forms.ToolStripMenuItem FuncList;
        private System.Windows.Forms.ToolStripMenuItem LSDDZH;
        private System.Windows.Forms.ToolStripMenuItem WBDDZH;
        private System.Windows.Forms.ToolStripMenuItem TXMCS;
        private System.Windows.Forms.ToolStripMenuItem GZTFZCX;
        private System.Windows.Forms.ToolStripMenuItem BOMXGHLCX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem KQSJZH;
        private System.Windows.Forms.ToolStripMenuItem SCZLD;
        private System.Windows.Forms.ToolStripMenuItem YJBOMSC;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem show;
        private System.Windows.Forms.ToolStripMenuItem hide;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem YKTUPDATE;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolStripMenuItem CGDJQST;
        private System.Windows.Forms.ToolStripMenuItem btn_set;
        private System.Windows.Forms.ToolStripMenuItem GQZZCX;
        private System.Windows.Forms.ToolStripMenuItem btn_changePW;
        private System.Windows.Forms.Label lbl_nowuser;
        private System.Windows.Forms.ToolStripMenuItem btn_changepassword;
        private System.Windows.Forms.ToolStripMenuItem about;
        private System.Windows.Forms.ToolStripMenuItem YKTSKRECORD;
        private System.Windows.Forms.Button btn_hcreport;
        private System.Windows.Forms.ToolStripMenuItem btn_refreshreport;
        private System.Windows.Forms.ToolStripMenuItem SCcalendarform;
        private System.Windows.Forms.ToolStripMenuItem TXMCSHLS;
        private System.Windows.Forms.ToolStripMenuItem cgwrkcx;
        private System.Windows.Forms.ToolStripMenuItem JSHCDKMXB;
        private System.Windows.Forms.ToolStripMenuItem zkIfaceManage;
        private System.Windows.Forms.ToolStripMenuItem MaterialDayUse;
        private System.Windows.Forms.Button btn_school;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btn_FactoryDayData;
        private System.Windows.Forms.ToolStripMenuItem SMCPCCJYBGPLDY;
    }
}