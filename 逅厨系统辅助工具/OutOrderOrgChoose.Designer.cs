
namespace 逅厨系统辅助工具
{
    partial class OutOrderOrgChoose
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
            this.btn_jiangyin = new System.Windows.Forms.Button();
            this.btn_nanjing = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_jiangyin
            // 
            this.btn_jiangyin.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_jiangyin.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btn_jiangyin.FlatAppearance.BorderSize = 3;
            this.btn_jiangyin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_jiangyin.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_jiangyin.ForeColor = System.Drawing.Color.White;
            this.btn_jiangyin.Location = new System.Drawing.Point(62, 49);
            this.btn_jiangyin.Name = "btn_jiangyin";
            this.btn_jiangyin.Size = new System.Drawing.Size(177, 40);
            this.btn_jiangyin.TabIndex = 2;
            this.btn_jiangyin.Text = "江   阴";
            this.btn_jiangyin.UseVisualStyleBackColor = false;
            this.btn_jiangyin.Click += new System.EventHandler(this.btn_jiangyin_Click);
            // 
            // btn_nanjing
            // 
            this.btn_nanjing.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_nanjing.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btn_nanjing.FlatAppearance.BorderSize = 3;
            this.btn_nanjing.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_nanjing.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_nanjing.ForeColor = System.Drawing.Color.White;
            this.btn_nanjing.Location = new System.Drawing.Point(62, 133);
            this.btn_nanjing.Name = "btn_nanjing";
            this.btn_nanjing.Size = new System.Drawing.Size(177, 40);
            this.btn_nanjing.TabIndex = 3;
            this.btn_nanjing.Text = "南   京";
            this.btn_nanjing.UseVisualStyleBackColor = false;
            this.btn_nanjing.Click += new System.EventHandler(this.btn_nanjing_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.BorderSize = 3;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(62, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "广东逅厨";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OutOrderOrgChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 290);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_nanjing);
            this.Controls.Add(this.btn_jiangyin);
            this.Name = "OutOrderOrgChoose";
            this.Text = "选择销售组织";
            this.Load += new System.EventHandler(this.OutOrderOrgChoose_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_jiangyin;
        private System.Windows.Forms.Button btn_nanjing;
        private System.Windows.Forms.Button button1;
    }
}