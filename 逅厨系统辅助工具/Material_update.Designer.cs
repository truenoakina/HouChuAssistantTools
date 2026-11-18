namespace 逅厨系统辅助工具
{
    partial class Material_update
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
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_unit = new System.Windows.Forms.TextBox();
            this.text_kname = new System.Windows.Forms.TextBox();
            this.text_lcode = new System.Windows.Forms.TextBox();
            this.text_kcode = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_2BM = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "金蝶编码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "单位名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "金蝶名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "一便码";
            // 
            // text_unit
            // 
            this.text_unit.Location = new System.Drawing.Point(133, 228);
            this.text_unit.Name = "text_unit";
            this.text_unit.Size = new System.Drawing.Size(142, 21);
            this.text_unit.TabIndex = 11;
            // 
            // text_kname
            // 
            this.text_kname.Location = new System.Drawing.Point(133, 188);
            this.text_kname.Name = "text_kname";
            this.text_kname.Size = new System.Drawing.Size(142, 21);
            this.text_kname.TabIndex = 10;
            // 
            // text_lcode
            // 
            this.text_lcode.Location = new System.Drawing.Point(133, 100);
            this.text_lcode.Name = "text_lcode";
            this.text_lcode.Size = new System.Drawing.Size(142, 21);
            this.text_lcode.TabIndex = 9;
            // 
            // text_kcode
            // 
            this.text_kcode.Location = new System.Drawing.Point(133, 57);
            this.text_kcode.Name = "text_kcode";
            this.text_kcode.Size = new System.Drawing.Size(142, 21);
            this.text_kcode.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(200, 290);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "取  消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(70, 290);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "确  认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_2BM
            // 
            this.txt_2BM.Location = new System.Drawing.Point(133, 143);
            this.txt_2BM.Name = "txt_2BM";
            this.txt_2BM.Size = new System.Drawing.Size(142, 21);
            this.txt_2BM.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "二便码";
            // 
            // Material_update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 377);
            this.Controls.Add(this.txt_2BM);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.text_unit);
            this.Controls.Add(this.text_kname);
            this.Controls.Add(this.text_lcode);
            this.Controls.Add(this.text_kcode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Material_update";
            this.Text = "修改";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Material_update_FormClosed);
            this.Load += new System.EventHandler(this.Material_update_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_unit;
        private System.Windows.Forms.TextBox text_kname;
        private System.Windows.Forms.TextBox text_lcode;
        private System.Windows.Forms.TextBox text_kcode;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_2BM;
        private System.Windows.Forms.Label label5;
    }
}