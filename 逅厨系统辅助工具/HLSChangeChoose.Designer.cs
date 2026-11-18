
namespace 逅厨系统辅助工具
{
    partial class HLSChangeChoose
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
            this.btn_DingDongorder = new System.Windows.Forms.Button();
            this.btn_outOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_DingDongorder
            // 
            this.btn_DingDongorder.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_DingDongorder.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btn_DingDongorder.FlatAppearance.BorderSize = 3;
            this.btn_DingDongorder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_DingDongorder.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DingDongorder.ForeColor = System.Drawing.Color.White;
            this.btn_DingDongorder.Location = new System.Drawing.Point(82, 216);
            this.btn_DingDongorder.Name = "btn_DingDongorder";
            this.btn_DingDongorder.Size = new System.Drawing.Size(161, 40);
            this.btn_DingDongorder.TabIndex = 2;
            this.btn_DingDongorder.Text = "叮咚订单转换";
            this.btn_DingDongorder.UseVisualStyleBackColor = false;
            this.btn_DingDongorder.Click += new System.EventHandler(this.btn_DingDongorder_Click);
            // 
            // btn_outOrder
            // 
            this.btn_outOrder.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_outOrder.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btn_outOrder.FlatAppearance.BorderSize = 3;
            this.btn_outOrder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_outOrder.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_outOrder.ForeColor = System.Drawing.Color.White;
            this.btn_outOrder.Location = new System.Drawing.Point(82, 87);
            this.btn_outOrder.Name = "btn_outOrder";
            this.btn_outOrder.Size = new System.Drawing.Size(161, 40);
            this.btn_outOrder.TabIndex = 6;
            this.btn_outOrder.Text = "外部订单转换";
            this.btn_outOrder.UseVisualStyleBackColor = false;
            this.btn_outOrder.Click += new System.EventHandler(this.btn_outOrder_Click);
            // 
            // HLSChangeChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 366);
            this.Controls.Add(this.btn_outOrder);
            this.Controls.Add(this.btn_DingDongorder);
            this.Name = "HLSChangeChoose";
            this.Text = "选择功能";
            this.Load += new System.EventHandler(this.HLSChangeChoose_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DingDongorder;
        private System.Windows.Forms.Button btn_outOrder;
    }
}