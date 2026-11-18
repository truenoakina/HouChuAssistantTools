
namespace 逅厨系统辅助工具
{
    partial class SchoolMenu
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
            this.btn_change = new System.Windows.Forms.Button();
            this.btn_caigouhz = new System.Windows.Forms.Button();
            this.btn_print = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_change
            // 
            this.btn_change.BackColor = System.Drawing.Color.LightGreen;
            this.btn_change.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_change.Location = new System.Drawing.Point(11, 11);
            this.btn_change.Margin = new System.Windows.Forms.Padding(2);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(195, 72);
            this.btn_change.TabIndex = 0;
            this.btn_change.Text = "订单导入表格转换";
            this.btn_change.UseVisualStyleBackColor = false;
            this.btn_change.Visible = false;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // btn_caigouhz
            // 
            this.btn_caigouhz.BackColor = System.Drawing.Color.LightGreen;
            this.btn_caigouhz.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_caigouhz.Location = new System.Drawing.Point(95, 169);
            this.btn_caigouhz.Margin = new System.Windows.Forms.Padding(2);
            this.btn_caigouhz.Name = "btn_caigouhz";
            this.btn_caigouhz.Size = new System.Drawing.Size(195, 72);
            this.btn_caigouhz.TabIndex = 1;
            this.btn_caigouhz.Text = "采购需求汇总";
            this.btn_caigouhz.UseVisualStyleBackColor = false;
            this.btn_caigouhz.Click += new System.EventHandler(this.btn_caigouhz_Click);
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.LightGreen;
            this.btn_print.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_print.Location = new System.Drawing.Point(95, 273);
            this.btn_print.Margin = new System.Windows.Forms.Padding(2);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(195, 72);
            this.btn_print.TabIndex = 2;
            this.btn_print.Text = "装框标签打印";
            this.btn_print.UseVisualStyleBackColor = false;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGreen;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(95, 63);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 72);
            this.button1.TabIndex = 3;
            this.button1.Text = "销售数据";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SchoolMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 402);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.btn_caigouhz);
            this.Controls.Add(this.btn_change);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SchoolMenu";
            this.Text = "学校订单功能菜单";
            this.Load += new System.EventHandler(this.SchoolMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_change;
        private System.Windows.Forms.Button btn_caigouhz;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button button1;
    }
}