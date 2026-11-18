namespace 逅厨系统辅助工具
{
    partial class CGDJQST
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.reportview = new FastReport.Preview.PreviewControl();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_org = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_year = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_wlname = new System.Windows.Forms.TextBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFnumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblfname = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblguige = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbluname = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.Controls.Add(this.reportview, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_org, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_year, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_wlname, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_OK, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblFnumber, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblfname, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblguige, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbluname, 8, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(939, 545);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // reportview
            // 
            this.reportview.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.reportview.Buttons = FastReport.PreviewButtons.Close;
            this.tableLayoutPanel1.SetColumnSpan(this.reportview, 9);
            this.reportview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportview.Font = new System.Drawing.Font("宋体", 9F);
            this.reportview.Location = new System.Drawing.Point(3, 63);
            this.reportview.Name = "reportview";
            this.reportview.PageOffset = new System.Drawing.Point(10, 10);
            this.reportview.Size = new System.Drawing.Size(934, 486);
            this.reportview.TabIndex = 0;
            this.reportview.ToolbarVisible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "采购组织";
            // 
            // cb_org
            // 
            this.cb_org.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_org.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_org.FormattingEnabled = true;
            this.cb_org.Items.AddRange(new object[] {
            "江苏逅厨供应链管理有限公司",
            "江苏佳之家食品科技有限公司",
            "江阴华联谷之田食品科技有限公司",
            "江苏好利森食品科技有限公司"});
            this.cb_org.Location = new System.Drawing.Point(83, 5);
            this.cb_org.Name = "cb_org";
            this.cb_org.Size = new System.Drawing.Size(194, 20);
            this.cb_org.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "年份";
            // 
            // cb_year
            // 
            this.cb_year.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_year.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_year.FormattingEnabled = true;
            this.cb_year.Items.AddRange(new object[] {
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025"});
            this.cb_year.Location = new System.Drawing.Point(335, 5);
            this.cb_year.Name = "cb_year";
            this.cb_year.Size = new System.Drawing.Size(89, 20);
            this.cb_year.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(443, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "物料名称";
            // 
            // tb_wlname
            // 
            this.tb_wlname.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_wlname.Location = new System.Drawing.Point(513, 4);
            this.tb_wlname.Name = "tb_wlname";
            this.tb_wlname.Size = new System.Drawing.Size(144, 21);
            this.tb_wlname.TabIndex = 6;
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_OK.Location = new System.Drawing.Point(663, 3);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 7;
            this.btn_OK.Text = "物料查询";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "物料编码";
            // 
            // lblFnumber
            // 
            this.lblFnumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFnumber.AutoSize = true;
            this.lblFnumber.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFnumber.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblFnumber.Location = new System.Drawing.Point(153, 39);
            this.lblFnumber.Name = "lblFnumber";
            this.lblFnumber.Size = new System.Drawing.Size(54, 12);
            this.lblFnumber.TabIndex = 10;
            this.lblFnumber.Text = "FNUMBER";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(290, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "名称";
            // 
            // lblfname
            // 
            this.lblfname.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblfname.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblfname, 2);
            this.lblfname.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblfname.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblfname.Location = new System.Drawing.Point(400, 39);
            this.lblfname.Name = "lblfname";
            this.lblfname.Size = new System.Drawing.Size(40, 12);
            this.lblfname.TabIndex = 12;
            this.lblfname.Text = "FNAME";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(558, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "规格型号";
            // 
            // lblguige
            // 
            this.lblguige.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblguige.AutoSize = true;
            this.lblguige.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblguige.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblguige.Location = new System.Drawing.Point(715, 39);
            this.lblguige.Name = "lblguige";
            this.lblguige.Size = new System.Drawing.Size(40, 12);
            this.lblguige.TabIndex = 14;
            this.lblguige.Text = "GUIGE";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(820, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "计价单位";
            // 
            // lbluname
            // 
            this.lbluname.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbluname.AutoSize = true;
            this.lbluname.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbluname.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbluname.Location = new System.Drawing.Point(886, 39);
            this.lbluname.Name = "lbluname";
            this.lbluname.Size = new System.Drawing.Size(40, 12);
            this.lbluname.TabIndex = 16;
            this.lbluname.Text = "UNAME";
            // 
            // CGDJQST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 545);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CGDJQST";
            this.Text = "采购单价趋势图";
            this.Load += new System.EventHandler(this.CGDJQST_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private FastReport.Preview.PreviewControl reportview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_org;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_year;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_wlname;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFnumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblfname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblguige;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbluname;
    }
}