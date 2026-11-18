namespace 逅厨系统辅助工具
{
    partial class ChooseType
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
            this.btn20 = new System.Windows.Forms.Button();
            this.btn69 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn20
            // 
            this.btn20.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn20.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn20.Location = new System.Drawing.Point(76, 22);
            this.btn20.Name = "btn20";
            this.btn20.Size = new System.Drawing.Size(164, 31);
            this.btn20.TabIndex = 0;
            this.btn20.Text = "新格式(20开头)";
            this.btn20.UseVisualStyleBackColor = false;
            this.btn20.Click += new System.EventHandler(this.btn20_Click);
            // 
            // btn69
            // 
            this.btn69.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn69.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn69.Location = new System.Drawing.Point(76, 78);
            this.btn69.Name = "btn69";
            this.btn69.Size = new System.Drawing.Size(164, 31);
            this.btn69.TabIndex = 1;
            this.btn69.Text = "通用格式(条码反查)";
            this.btn69.UseVisualStyleBackColor = false;
            this.btn69.Click += new System.EventHandler(this.btn69_Click);
            // 
            // ChooseType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 138);
            this.Controls.Add(this.btn69);
            this.Controls.Add(this.btn20);
            this.Name = "ChooseType";
            this.Text = "选择条形码类别";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn20;
        private System.Windows.Forms.Button btn69;
    }
}