
namespace 逅厨系统辅助工具
{
    partial class FDD_Main
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
            this.btn_depinfo = new System.Windows.Forms.Button();
            this.btn_empinfo = new System.Windows.Forms.Button();
            this.btn_AttendanceConfig = new System.Windows.Forms.Button();
            this.btn_AttendanceWrite = new System.Windows.Forms.Button();
            this.btn_energyWrite = new System.Windows.Forms.Button();
            this.btn_Attreport = new System.Windows.Forms.Button();
            this.btn_holiday = new System.Windows.Forms.Button();
            this.btn_saleCostReport = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_log = new System.Windows.Forms.Button();
            this.btn_hoursAdjust = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_depinfo
            // 
            this.btn_depinfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_depinfo.Enabled = false;
            this.btn_depinfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_depinfo.Location = new System.Drawing.Point(12, 51);
            this.btn_depinfo.Name = "btn_depinfo";
            this.btn_depinfo.Size = new System.Drawing.Size(126, 42);
            this.btn_depinfo.TabIndex = 0;
            this.btn_depinfo.Text = "部门信息";
            this.btn_depinfo.UseVisualStyleBackColor = true;
            this.btn_depinfo.Click += new System.EventHandler(this.btn_depinfo_Click);
            // 
            // btn_empinfo
            // 
            this.btn_empinfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_empinfo.Enabled = false;
            this.btn_empinfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_empinfo.Location = new System.Drawing.Point(12, 99);
            this.btn_empinfo.Name = "btn_empinfo";
            this.btn_empinfo.Size = new System.Drawing.Size(126, 42);
            this.btn_empinfo.TabIndex = 1;
            this.btn_empinfo.Text = "员工信息";
            this.btn_empinfo.UseVisualStyleBackColor = true;
            this.btn_empinfo.Click += new System.EventHandler(this.btn_empinfo_Click);
            // 
            // btn_AttendanceConfig
            // 
            this.btn_AttendanceConfig.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_AttendanceConfig.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_AttendanceConfig.Location = new System.Drawing.Point(12, 195);
            this.btn_AttendanceConfig.Name = "btn_AttendanceConfig";
            this.btn_AttendanceConfig.Size = new System.Drawing.Size(126, 42);
            this.btn_AttendanceConfig.TabIndex = 2;
            this.btn_AttendanceConfig.Text = "参数设置";
            this.btn_AttendanceConfig.UseVisualStyleBackColor = true;
            this.btn_AttendanceConfig.Click += new System.EventHandler(this.btn_AttendanceConfig_Click);
            // 
            // btn_AttendanceWrite
            // 
            this.btn_AttendanceWrite.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_AttendanceWrite.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_AttendanceWrite.Location = new System.Drawing.Point(162, 195);
            this.btn_AttendanceWrite.Name = "btn_AttendanceWrite";
            this.btn_AttendanceWrite.Size = new System.Drawing.Size(126, 42);
            this.btn_AttendanceWrite.TabIndex = 3;
            this.btn_AttendanceWrite.Text = "考勤录入";
            this.btn_AttendanceWrite.UseVisualStyleBackColor = true;
            this.btn_AttendanceWrite.Visible = false;
            this.btn_AttendanceWrite.Click += new System.EventHandler(this.btn_AttendanceWrite_Click);
            // 
            // btn_energyWrite
            // 
            this.btn_energyWrite.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_energyWrite.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_energyWrite.Location = new System.Drawing.Point(162, 51);
            this.btn_energyWrite.Name = "btn_energyWrite";
            this.btn_energyWrite.Size = new System.Drawing.Size(126, 42);
            this.btn_energyWrite.TabIndex = 4;
            this.btn_energyWrite.Text = "能耗录入";
            this.btn_energyWrite.UseVisualStyleBackColor = true;
            this.btn_energyWrite.Click += new System.EventHandler(this.btn_energyWrite_Click);
            // 
            // btn_Attreport
            // 
            this.btn_Attreport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Attreport.Enabled = false;
            this.btn_Attreport.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Attreport.Location = new System.Drawing.Point(312, 51);
            this.btn_Attreport.Name = "btn_Attreport";
            this.btn_Attreport.Size = new System.Drawing.Size(126, 42);
            this.btn_Attreport.TabIndex = 5;
            this.btn_Attreport.Text = "考勤报表";
            this.btn_Attreport.UseVisualStyleBackColor = true;
            this.btn_Attreport.Click += new System.EventHandler(this.btn_Attreport_Click);
            // 
            // btn_holiday
            // 
            this.btn_holiday.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_holiday.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_holiday.Location = new System.Drawing.Point(12, 147);
            this.btn_holiday.Name = "btn_holiday";
            this.btn_holiday.Size = new System.Drawing.Size(126, 42);
            this.btn_holiday.TabIndex = 6;
            this.btn_holiday.Text = "法定节假日设置";
            this.btn_holiday.UseVisualStyleBackColor = true;
            this.btn_holiday.Click += new System.EventHandler(this.btn_holiday_Click);
            // 
            // btn_saleCostReport
            // 
            this.btn_saleCostReport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_saleCostReport.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_saleCostReport.Location = new System.Drawing.Point(312, 99);
            this.btn_saleCostReport.Name = "btn_saleCostReport";
            this.btn_saleCostReport.Size = new System.Drawing.Size(126, 42);
            this.btn_saleCostReport.TabIndex = 7;
            this.btn_saleCostReport.Text = "销售成本分析表";
            this.btn_saleCostReport.UseVisualStyleBackColor = true;
            this.btn_saleCostReport.Click += new System.EventHandler(this.btn_saleCostReport_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_depinfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_saleCostReport, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_empinfo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_Attreport, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_holiday, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_energyWrite, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_AttendanceConfig, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_log, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btn_hoursAdjust, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.btn_AttendanceWrite, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(583, 416);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(338, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 22);
            this.label3.TabIndex = 10;
            this.label3.Text = "报表查询";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(188, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 22);
            this.label2.TabIndex = 9;
            this.label2.Text = "数据录入";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(38, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 22);
            this.label1.TabIndex = 8;
            this.label1.Text = "基础资料";
            // 
            // btn_log
            // 
            this.btn_log.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_log.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_log.Location = new System.Drawing.Point(12, 243);
            this.btn_log.Name = "btn_log";
            this.btn_log.Size = new System.Drawing.Size(126, 42);
            this.btn_log.TabIndex = 12;
            this.btn_log.Text = "日志查询";
            this.btn_log.UseVisualStyleBackColor = true;
            this.btn_log.Click += new System.EventHandler(this.btn_log_Click);
            // 
            // btn_hoursAdjust
            // 
            this.btn_hoursAdjust.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_hoursAdjust.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_hoursAdjust.Location = new System.Drawing.Point(162, 243);
            this.btn_hoursAdjust.Name = "btn_hoursAdjust";
            this.btn_hoursAdjust.Size = new System.Drawing.Size(126, 42);
            this.btn_hoursAdjust.TabIndex = 11;
            this.btn_hoursAdjust.Text = "员工工时调整";
            this.btn_hoursAdjust.UseVisualStyleBackColor = true;
            this.btn_hoursAdjust.Visible = false;
            this.btn_hoursAdjust.Click += new System.EventHandler(this.btn_hoursAdjust_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(162, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 42);
            this.button1.TabIndex = 13;
            this.button1.Text = "汇总数据录入 ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FDD_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 416);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FDD_Main";
            this.Text = "生产数据录入及报表";
            this.Load += new System.EventHandler(this.FDD_Main_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_depinfo;
        private System.Windows.Forms.Button btn_empinfo;
        private System.Windows.Forms.Button btn_AttendanceConfig;
        private System.Windows.Forms.Button btn_AttendanceWrite;
        private System.Windows.Forms.Button btn_energyWrite;
        private System.Windows.Forms.Button btn_Attreport;
        private System.Windows.Forms.Button btn_holiday;
        private System.Windows.Forms.Button btn_saleCostReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_hoursAdjust;
        private System.Windows.Forms.Button btn_log;
        private System.Windows.Forms.Button button1;
    }
}