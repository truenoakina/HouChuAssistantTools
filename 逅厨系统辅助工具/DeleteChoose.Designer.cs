namespace 逅厨系统辅助工具
{
    partial class DeleteChoose
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
            this.btn_AllDel = new System.Windows.Forms.Button();
            this.btn_PartDel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_AllDel
            // 
            this.btn_AllDel.Location = new System.Drawing.Point(52, 43);
            this.btn_AllDel.Name = "btn_AllDel";
            this.btn_AllDel.Size = new System.Drawing.Size(96, 36);
            this.btn_AllDel.TabIndex = 0;
            this.btn_AllDel.Text = "全部删除";
            this.btn_AllDel.UseVisualStyleBackColor = true;
            this.btn_AllDel.Click += new System.EventHandler(this.btn_AllDel_Click);
            // 
            // btn_PartDel
            // 
            this.btn_PartDel.Location = new System.Drawing.Point(173, 43);
            this.btn_PartDel.Name = "btn_PartDel";
            this.btn_PartDel.Size = new System.Drawing.Size(96, 36);
            this.btn_PartDel.TabIndex = 1;
            this.btn_PartDel.Text = "部分删除";
            this.btn_PartDel.UseVisualStyleBackColor = true;
            this.btn_PartDel.Click += new System.EventHandler(this.btn_PartDel_Click);
            // 
            // DeleteChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 125);
            this.Controls.Add(this.btn_PartDel);
            this.Controls.Add(this.btn_AllDel);
            this.Name = "DeleteChoose";
            this.Text = "删除确认";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_AllDel;
        private System.Windows.Forms.Button btn_PartDel;
    }
}