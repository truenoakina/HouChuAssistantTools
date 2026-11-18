using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class DingDongCusInfo : Form
    {
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        private string connStr = SQLHelper.GetConnString();
        public DingDongCusInfo()
        {
            InitializeComponent();
            LoadData();
            ConfigureDataGridView();
        }

        // 配置DataGridView属性
        private void ConfigureDataGridView()
        {
            dataGridView1.AllowUserToAddRows = true;     // 允许新增行
            dataGridView1.AllowUserToDeleteRows = true;  // 允许删除行
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke; // 按键盘即编辑

            // 禁止自动生成列
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            // 列1
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "id";
            col1.HeaderText = "id";
            col1.DataPropertyName = "id";
            col1.Width = 30;
            // 列1
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "dingdongStock";
            col2.HeaderText = "叮咚入库仓";
            col2.DataPropertyName = "dingdongStock";
            col2.Width = 180;

            // 列2
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "jdCusID";
            col3.HeaderText = "金蝶客户编码";
            col3.DataPropertyName = "jdCusID";
            col3.Width = 120;

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = "jdCusName";
            col4.HeaderText = "金蝶客户名称";
            col4.DataPropertyName = "jdCusName";
            col4.Width = 200;


            // 添加到控件
            dataGridView1.Columns.AddRange(col1, col2, col3, col4);
            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.Columns["id"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["dingdongStock"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["jdCusID"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["jdCusName"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //dataGridView1.Columns[5].Visible = false;  ///隐藏id
        }

       

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // 注意：SELECT 语句必须包含主键列，否则 SqlCommandBuilder 无法生成更新命令
                    string sql = "SELECT id, dingdongStock, jdCusID,jdCusName FROM DingDongStockCus";
                    adapter = new SqlDataAdapter(sql, conn);
                    dataTable = new DataTable();

                    // 自动填充 DataTable
                    adapter.Fill(dataTable);

                    // 关键：设置主键，用于定位更新行
                    if (dataTable.Columns.Contains("Id"))
                    {
                        dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"] };
                    }

                    // 绑定数据源
                    dataGridView1.DataSource = dataTable;

                    // 可选：禁止自动生成列以保留自定义列样式，若需自动列可注释此行
                    // dataGridView1.AutoGenerateColumns = true;                 }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载失败: {ex.Message}");
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                // 结束当前单元格的编辑状态，确保最新输入被写入 DataTable
                dataGridView1.EndEdit();

                // 检查是否有变更
                if (dataTable.GetChanges() == null)
                {
                    MessageBox.Show("没有数据变更。");
                    return;
                }

                // 使用 SqlCommandBuilder 自动生成 Insert/Update/Delete 命令
                // 注意：每次 Update 前最好重新创建 Builder，或确保 Adapter 命令未丢失
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    adapter.SelectCommand.Connection = conn;
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    // 执行更新，返回受影响的行数
                    int rowsAffected = adapter.Update(dataTable);
                    // 接受更改，重置行状态为 Unchanged
                    dataTable.AcceptChanges();
                    MessageBox.Show($"成功同步 {rowsAffected} 条记录。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败: {ex.Message}\n请检查数据类型匹配及主键约束。");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
