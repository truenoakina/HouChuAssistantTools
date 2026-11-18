
namespace 逅厨系统辅助工具
{
    partial class HoursAdjust
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpup = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_from = new System.Windows.Forms.Label();
            this.dgv_from = new System.Windows.Forms.DataGridView();
            this.btn_change = new System.Windows.Forms.Button();
            this.tlpdown = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_to = new System.Windows.Forms.Label();
            this.dgv_to = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn_empchoose = new System.Windows.Forms.Button();
            this.lbl_empinfo = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_from)).BeginInit();
            this.tlpdown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_to)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tlpup, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tlpdown, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_change, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePicker1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_empchoose, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_empinfo, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1127, 624);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tlpup
            // 
            this.tlpup.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpup, 5);
            this.tlpup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpup.Controls.Add(this.lbl_from, 0, 0);
            this.tlpup.Controls.Add(this.dgv_from, 0, 1);
            this.tlpup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpup.Location = new System.Drawing.Point(3, 38);
            this.tlpup.Name = "tlpup";
            this.tlpup.RowCount = 2;
            this.tlpup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpup.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpup.Size = new System.Drawing.Size(1121, 288);
            this.tlpup.TabIndex = 0;
            // 
            // lbl_from
            // 
            this.lbl_from.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_from.AutoSize = true;
            this.lbl_from.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_from.ForeColor = System.Drawing.Color.Blue;
            this.lbl_from.Location = new System.Drawing.Point(3, 5);
            this.lbl_from.Name = "lbl_from";
            this.lbl_from.Size = new System.Drawing.Size(17, 20);
            this.lbl_from.TabIndex = 0;
            this.lbl_from.Text = "  ";
            // 
            // dgv_from
            // 
            this.dgv_from.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_from.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_from.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpup.SetColumnSpan(this.dgv_from, 2);
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_from.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgv_from.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_from.Location = new System.Drawing.Point(3, 33);
            this.dgv_from.MultiSelect = false;
            this.dgv_from.Name = "dgv_from";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_from.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgv_from.RowTemplate.Height = 23;
            this.dgv_from.Size = new System.Drawing.Size(1115, 252);
            this.dgv_from.TabIndex = 1;
            this.dgv_from.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_from_CellClick);
            // 
            // btn_change
            // 
            this.btn_change.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_change.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_change.ForeColor = System.Drawing.Color.Red;
            this.btn_change.Location = new System.Drawing.Point(363, 3);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(75, 29);
            this.btn_change.TabIndex = 2;
            this.btn_change.Text = "确认调整";
            this.btn_change.UseVisualStyleBackColor = true;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // tlpdown
            // 
            this.tlpdown.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpdown, 5);
            this.tlpdown.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpdown.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpdown.Controls.Add(this.lbl_to, 0, 0);
            this.tlpdown.Controls.Add(this.dgv_to, 0, 1);
            this.tlpdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpdown.Location = new System.Drawing.Point(3, 332);
            this.tlpdown.Name = "tlpdown";
            this.tlpdown.RowCount = 2;
            this.tlpdown.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpdown.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpdown.Size = new System.Drawing.Size(1121, 289);
            this.tlpdown.TabIndex = 1;
            // 
            // lbl_to
            // 
            this.lbl_to.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_to.AutoSize = true;
            this.lbl_to.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_to.ForeColor = System.Drawing.Color.Blue;
            this.lbl_to.Location = new System.Drawing.Point(3, 5);
            this.lbl_to.Name = "lbl_to";
            this.lbl_to.Size = new System.Drawing.Size(17, 20);
            this.lbl_to.TabIndex = 1;
            this.lbl_to.Text = "  ";
            // 
            // dgv_to
            // 
            this.dgv_to.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_to.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_to.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpdown.SetColumnSpan(this.dgv_to, 2);
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_to.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_to.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_to.Location = new System.Drawing.Point(3, 33);
            this.dgv_to.MultiSelect = false;
            this.dgv_to.Name = "dgv_to";
            this.dgv_to.RowTemplate.Height = 23;
            this.dgv_to.Size = new System.Drawing.Size(1115, 253);
            this.dgv_to.TabIndex = 3;
            this.dgv_to.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_to_CellClick);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(27, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择月份";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1.Location = new System.Drawing.Point(123, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(114, 26);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // btn_empchoose
            // 
            this.btn_empchoose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_empchoose.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_empchoose.Location = new System.Drawing.Point(252, 3);
            this.btn_empchoose.Name = "btn_empchoose";
            this.btn_empchoose.Size = new System.Drawing.Size(95, 29);
            this.btn_empchoose.TabIndex = 4;
            this.btn_empchoose.Text = "选择员工...";
            this.btn_empchoose.UseVisualStyleBackColor = true;
            this.btn_empchoose.Click += new System.EventHandler(this.btn_empchoose_Click);
            // 
            // lbl_empinfo
            // 
            this.lbl_empinfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_empinfo.AutoSize = true;
            this.lbl_empinfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_empinfo.ForeColor = System.Drawing.Color.Blue;
            this.lbl_empinfo.Location = new System.Drawing.Point(483, 8);
            this.lbl_empinfo.Name = "lbl_empinfo";
            this.lbl_empinfo.Size = new System.Drawing.Size(17, 19);
            this.lbl_empinfo.TabIndex = 5;
            this.lbl_empinfo.Text = "  ";
            // 
            // HoursAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 624);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HoursAdjust";
            this.Text = "总工时调整";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HoursAdjust_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpup.ResumeLayout(false);
            this.tlpup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_from)).EndInit();
            this.tlpdown.ResumeLayout(false);
            this.tlpdown.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_to)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tlpup;
        private System.Windows.Forms.TableLayoutPanel tlpdown;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btn_empchoose;
        private System.Windows.Forms.Label lbl_empinfo;
        private System.Windows.Forms.Label lbl_from;
        private System.Windows.Forms.Label lbl_to;
        private System.Windows.Forms.Button btn_change;
        public System.Windows.Forms.DataGridView dgv_from;
        public System.Windows.Forms.DataGridView dgv_to;
    }
}