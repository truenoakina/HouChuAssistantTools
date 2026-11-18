namespace 逅厨系统辅助工具
{
    partial class FC_PRODUCR_INFO
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
            this.btn_yltype = new System.Windows.Forms.Button();
            this.btn_yuanliao = new System.Windows.Forms.Button();
            this.btn_supplier = new System.Windows.Forms.Button();
            this.btn_caiyao = new System.Windows.Forms.Button();
            this.btn_caiyaobom = new System.Windows.Forms.Button();
            this.btn_order = new System.Windows.Forms.Button();
            this.btn_kucun = new System.Windows.Forms.Button();
            this.btn_kucunyugu = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.Controls.Add(this.btn_yltype, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_yuanliao, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_supplier, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_caiyao, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_caiyaobom, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_order, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_kucun, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_kucunyugu, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(363, 323);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_yltype
            // 
            this.btn_yltype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_yltype.Location = new System.Drawing.Point(3, 3);
            this.btn_yltype.Name = "btn_yltype";
            this.btn_yltype.Size = new System.Drawing.Size(172, 54);
            this.btn_yltype.TabIndex = 0;
            this.btn_yltype.Text = "原料类别";
            this.btn_yltype.UseVisualStyleBackColor = true;
            this.btn_yltype.Click += new System.EventHandler(this.btn_yltype_Click);
            // 
            // btn_yuanliao
            // 
            this.btn_yuanliao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_yuanliao.Location = new System.Drawing.Point(181, 3);
            this.btn_yuanliao.Name = "btn_yuanliao";
            this.btn_yuanliao.Size = new System.Drawing.Size(179, 54);
            this.btn_yuanliao.TabIndex = 1;
            this.btn_yuanliao.Text = "原料";
            this.btn_yuanliao.UseVisualStyleBackColor = true;
            this.btn_yuanliao.Click += new System.EventHandler(this.btn_yuanliao_Click);
            // 
            // btn_supplier
            // 
            this.btn_supplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_supplier.Location = new System.Drawing.Point(3, 63);
            this.btn_supplier.Name = "btn_supplier";
            this.btn_supplier.Size = new System.Drawing.Size(172, 47);
            this.btn_supplier.TabIndex = 2;
            this.btn_supplier.Text = "供应商";
            this.btn_supplier.UseVisualStyleBackColor = true;
            this.btn_supplier.Click += new System.EventHandler(this.btn_supplier_Click);
            // 
            // btn_caiyao
            // 
            this.btn_caiyao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_caiyao.Location = new System.Drawing.Point(181, 63);
            this.btn_caiyao.Name = "btn_caiyao";
            this.btn_caiyao.Size = new System.Drawing.Size(179, 47);
            this.btn_caiyao.TabIndex = 3;
            this.btn_caiyao.Text = "菜肴产品";
            this.btn_caiyao.UseVisualStyleBackColor = true;
            this.btn_caiyao.Click += new System.EventHandler(this.btn_caiyao_Click);
            // 
            // btn_caiyaobom
            // 
            this.btn_caiyaobom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_caiyaobom.Location = new System.Drawing.Point(3, 116);
            this.btn_caiyaobom.Name = "btn_caiyaobom";
            this.btn_caiyaobom.Size = new System.Drawing.Size(172, 51);
            this.btn_caiyaobom.TabIndex = 4;
            this.btn_caiyaobom.Text = "原料用量明细维护";
            this.btn_caiyaobom.UseVisualStyleBackColor = true;
            this.btn_caiyaobom.Click += new System.EventHandler(this.btn_caiyaobom_Click);
            // 
            // btn_order
            // 
            this.btn_order.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_order.Location = new System.Drawing.Point(181, 116);
            this.btn_order.Name = "btn_order";
            this.btn_order.Size = new System.Drawing.Size(179, 51);
            this.btn_order.TabIndex = 5;
            this.btn_order.Text = "订单量维护";
            this.btn_order.UseVisualStyleBackColor = true;
            this.btn_order.Click += new System.EventHandler(this.btn_order_Click);
            // 
            // btn_kucun
            // 
            this.btn_kucun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_kucun.Location = new System.Drawing.Point(3, 173);
            this.btn_kucun.Name = "btn_kucun";
            this.btn_kucun.Size = new System.Drawing.Size(172, 64);
            this.btn_kucun.TabIndex = 6;
            this.btn_kucun.Text = "库存量维护";
            this.btn_kucun.UseVisualStyleBackColor = true;
            this.btn_kucun.Click += new System.EventHandler(this.btn_kucun_Click);
            // 
            // btn_kucunyugu
            // 
            this.btn_kucunyugu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_kucunyugu.Location = new System.Drawing.Point(3, 243);
            this.btn_kucunyugu.Name = "btn_kucunyugu";
            this.btn_kucunyugu.Size = new System.Drawing.Size(172, 77);
            this.btn_kucunyugu.TabIndex = 7;
            this.btn_kucunyugu.Text = "库存预估报表";
            this.btn_kucunyugu.UseVisualStyleBackColor = true;
            this.btn_kucunyugu.Click += new System.EventHandler(this.btn_kucunyugu_Click);
            // 
            // FC_PRODUCR_INFO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 323);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FC_PRODUCR_INFO";
            this.Text = "方舱生产信息";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_yltype;
        private System.Windows.Forms.Button btn_yuanliao;
        private System.Windows.Forms.Button btn_supplier;
        private System.Windows.Forms.Button btn_caiyao;
        private System.Windows.Forms.Button btn_caiyaobom;
        private System.Windows.Forms.Button btn_order;
        private System.Windows.Forms.Button btn_kucun;
        private System.Windows.Forms.Button btn_kucunyugu;
    }
}