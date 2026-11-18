namespace 逅厨系统辅助工具
{
    partial class ChangePassWord
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_OldPassWord = new System.Windows.Forms.TextBox();
            this.txt_NewPassWord = new System.Windows.Forms.TextBox();
            this.txt_NewPassWord1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "旧  密  码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "新  密  码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "确认新密码";
            // 
            // txt_OldPassWord
            // 
            this.txt_OldPassWord.Location = new System.Drawing.Point(111, 19);
            this.txt_OldPassWord.Name = "txt_OldPassWord";
            this.txt_OldPassWord.Size = new System.Drawing.Size(200, 21);
            this.txt_OldPassWord.TabIndex = 3;
            this.txt_OldPassWord.UseSystemPasswordChar = true;
            this.txt_OldPassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_OldPassWord_KeyDown);
            // 
            // txt_NewPassWord
            // 
            this.txt_NewPassWord.Location = new System.Drawing.Point(111, 56);
            this.txt_NewPassWord.Name = "txt_NewPassWord";
            this.txt_NewPassWord.Size = new System.Drawing.Size(200, 21);
            this.txt_NewPassWord.TabIndex = 4;
            this.txt_NewPassWord.UseSystemPasswordChar = true;
            this.txt_NewPassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_NewPassWord_KeyDown);
            // 
            // txt_NewPassWord1
            // 
            this.txt_NewPassWord1.Location = new System.Drawing.Point(111, 93);
            this.txt_NewPassWord1.Name = "txt_NewPassWord1";
            this.txt_NewPassWord1.Size = new System.Drawing.Size(200, 21);
            this.txt_NewPassWord1.TabIndex = 5;
            this.txt_NewPassWord1.UseSystemPasswordChar = true;
            this.txt_NewPassWord1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_NewPassWord1_KeyDown);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(73, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确  认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(214, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "取  消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ChangePassWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 163);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_NewPassWord1);
            this.Controls.Add(this.txt_NewPassWord);
            this.Controls.Add(this.txt_OldPassWord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ChangePassWord";
            this.Text = "修改密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_OldPassWord;
        private System.Windows.Forms.TextBox txt_NewPassWord;
        private System.Windows.Forms.TextBox txt_NewPassWord1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}