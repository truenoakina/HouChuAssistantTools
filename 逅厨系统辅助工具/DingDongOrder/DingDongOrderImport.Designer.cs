namespace 逅厨系统辅助工具
{
    partial class DingDongOrderImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DingDongOrderImport));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_info = new System.Windows.Forms.Label();
            this.customer = new System.Windows.Forms.Button();
            this.btn_stockCusInfo = new System.Windows.Forms.Button();
            this.btn_change = new System.Windows.Forms.Button();
            this.excelinfo = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lbl_info);
            this.panel1.Controls.Add(this.customer);
            this.panel1.Controls.Add(this.btn_stockCusInfo);
            this.panel1.Controls.Add(this.btn_change);
            this.panel1.Controls.Add(this.excelinfo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(3, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1405, 92);
            this.panel1.TabIndex = 0;
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.ForeColor = System.Drawing.Color.Blue;
            this.lbl_info.Location = new System.Drawing.Point(886, 32);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(55, 15);
            this.lbl_info.TabIndex = 15;
            this.lbl_info.Text = "label1";
            // 
            // customer
            // 
            this.customer.Location = new System.Drawing.Point(779, 11);
            this.customer.Margin = new System.Windows.Forms.Padding(4);
            this.customer.Name = "customer";
            this.customer.Size = new System.Drawing.Size(100, 69);
            this.customer.TabIndex = 14;
            this.customer.Text = "清理缓存";
            this.customer.UseVisualStyleBackColor = true;
            this.customer.Click += new System.EventHandler(this.customer_Click);
            // 
            // btn_stockCusInfo
            // 
            this.btn_stockCusInfo.Location = new System.Drawing.Point(671, 11);
            this.btn_stockCusInfo.Margin = new System.Windows.Forms.Padding(4);
            this.btn_stockCusInfo.Name = "btn_stockCusInfo";
            this.btn_stockCusInfo.Size = new System.Drawing.Size(100, 69);
            this.btn_stockCusInfo.TabIndex = 13;
            this.btn_stockCusInfo.Text = "入库仓对应客户信息";
            this.btn_stockCusInfo.UseVisualStyleBackColor = true;
            this.btn_stockCusInfo.Click += new System.EventHandler(this.btn_stockCusInfo_Click);
            // 
            // btn_change
            // 
            this.btn_change.Location = new System.Drawing.Point(579, 11);
            this.btn_change.Margin = new System.Windows.Forms.Padding(4);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(84, 69);
            this.btn_change.TabIndex = 10;
            this.btn_change.Text = "确认转换";
            this.btn_change.UseVisualStyleBackColor = true;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // excelinfo
            // 
            this.excelinfo.Location = new System.Drawing.Point(217, 9);
            this.excelinfo.Margin = new System.Windows.Forms.Padding(4);
            this.excelinfo.Name = "excelinfo";
            this.excelinfo.ReadOnly = true;
            this.excelinfo.Size = new System.Drawing.Size(352, 73);
            this.excelinfo.TabIndex = 9;
            this.excelinfo.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(125, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "文件名称";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 11);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 69);
            this.button1.TabIndex = 1;
            this.button1.Text = "EXCEL上传\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(3, 101);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1405, 732);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1405, 732);
            this.dataGridView1.TabIndex = 0;
            // 
            // DingDongOrderImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 832);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DingDongOrderImport";
            this.Text = "叮咚订单转换";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DingDongOrderImport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox excelinfo;
        private System.Windows.Forms.Button btn_change;
        private System.Windows.Forms.Button customer;
        private System.Windows.Forms.Button btn_stockCusInfo;
        private System.Windows.Forms.Label lbl_info;
    }
}