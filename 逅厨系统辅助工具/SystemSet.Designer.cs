namespace 逅厨系统辅助工具
{
    partial class SystemSet
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
            this.cb_isWarnzizhi = new System.Windows.Forms.CheckBox();
            this.tb_time = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_autorun = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_usefuldays = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_wrkrefreshtime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.wrkneedwarddate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_isWarnWrk = new System.Windows.Forms.CheckBox();
            this.wrkneedwarddays = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_isWarnzizhi
            // 
            this.cb_isWarnzizhi.AutoSize = true;
            this.cb_isWarnzizhi.Location = new System.Drawing.Point(6, 32);
            this.cb_isWarnzizhi.Name = "cb_isWarnzizhi";
            this.cb_isWarnzizhi.Size = new System.Drawing.Size(144, 16);
            this.cb_isWarnzizhi.TabIndex = 0;
            this.cb_isWarnzizhi.Text = "是否提醒过期资质信息";
            this.cb_isWarnzizhi.UseVisualStyleBackColor = true;
            this.cb_isWarnzizhi.CheckedChanged += new System.EventHandler(this.cb_isWarnzizhi_CheckedChanged);
            // 
            // tb_time
            // 
            this.tb_time.Location = new System.Drawing.Point(171, 57);
            this.tb_time.Name = "tb_time";
            this.tb_time.Size = new System.Drawing.Size(100, 21);
            this.tb_time.TabIndex = 1;
            this.tb_time.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_time_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "过期资质提醒刷新时间(分钟)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cb_isWarnzizhi);
            this.groupBox1.Controls.Add(this.tb_time);
            this.groupBox1.Location = new System.Drawing.Point(3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 85);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "过期资质提醒";
            // 
            // btn_save
            // 
            this.btn_save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_save.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.Location = new System.Drawing.Point(61, 345);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 4;
            this.btn_save.Text = "保  存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.Location = new System.Drawing.Point(201, 345);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "取  消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_autorun);
            this.groupBox2.Location = new System.Drawing.Point(3, 283);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 46);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "程序开机启动";
            // 
            // cb_autorun
            // 
            this.cb_autorun.AutoSize = true;
            this.cb_autorun.Location = new System.Drawing.Point(6, 20);
            this.cb_autorun.Name = "cb_autorun";
            this.cb_autorun.Size = new System.Drawing.Size(96, 16);
            this.cb_autorun.TabIndex = 0;
            this.cb_autorun.Text = "是否开机自启";
            this.cb_autorun.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tb_usefuldays);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(4, 96);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(342, 46);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "过期资质过滤条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "天";
            // 
            // tb_usefuldays
            // 
            this.tb_usefuldays.Location = new System.Drawing.Point(65, 18);
            this.tb_usefuldays.Name = "tb_usefuldays";
            this.tb_usefuldays.Size = new System.Drawing.Size(68, 21);
            this.tb_usefuldays.TabIndex = 1;
            this.tb_usefuldays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "提前天数";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tb_wrkrefreshtime);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.wrkneedwarddate);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cb_isWarnWrk);
            this.groupBox4.Controls.Add(this.wrkneedwarddays);
            this.groupBox4.Location = new System.Drawing.Point(4, 148);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(342, 129);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "采购订单未入库提醒";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "未入库提醒刷新时间(分钟)";
            // 
            // tb_wrkrefreshtime
            // 
            this.tb_wrkrefreshtime.Location = new System.Drawing.Point(168, 104);
            this.tb_wrkrefreshtime.Name = "tb_wrkrefreshtime";
            this.tb_wrkrefreshtime.Size = new System.Drawing.Size(100, 21);
            this.tb_wrkrefreshtime.TabIndex = 6;
            this.tb_wrkrefreshtime.Text = "60";
            this.tb_wrkrefreshtime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_wrkrefreshtime_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "前的采购订单不提醒";
            // 
            // wrkneedwarddate
            // 
            this.wrkneedwarddate.Location = new System.Drawing.Point(6, 48);
            this.wrkneedwarddate.Name = "wrkneedwarddate";
            this.wrkneedwarddate.Size = new System.Drawing.Size(102, 21);
            this.wrkneedwarddate.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "天后未入库需要提醒";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "订单日期";
            // 
            // cb_isWarnWrk
            // 
            this.cb_isWarnWrk.AutoSize = true;
            this.cb_isWarnWrk.Location = new System.Drawing.Point(6, 24);
            this.cb_isWarnWrk.Name = "cb_isWarnWrk";
            this.cb_isWarnWrk.Size = new System.Drawing.Size(180, 16);
            this.cb_isWarnWrk.TabIndex = 0;
            this.cb_isWarnWrk.Text = "是否提醒采购订单未入库信息";
            this.cb_isWarnWrk.UseVisualStyleBackColor = true;
            this.cb_isWarnWrk.CheckedChanged += new System.EventHandler(this.cb_isWarnWrk_CheckedChanged);
            // 
            // wrkneedwarddays
            // 
            this.wrkneedwarddays.Location = new System.Drawing.Point(63, 78);
            this.wrkneedwarddays.Name = "wrkneedwarddays";
            this.wrkneedwarddays.Size = new System.Drawing.Size(39, 21);
            this.wrkneedwarddays.TabIndex = 1;
            this.wrkneedwarddays.Text = "4";
            this.wrkneedwarddays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.wrkneedwarddays_KeyPress);
            // 
            // SystemSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 394);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "SystemSet";
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.SystemSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_isWarnzizhi;
        private System.Windows.Forms.TextBox tb_time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cb_autorun;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_usefuldays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cb_isWarnWrk;
        private System.Windows.Forms.TextBox wrkneedwarddays;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker wrkneedwarddate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_wrkrefreshtime;
    }
}