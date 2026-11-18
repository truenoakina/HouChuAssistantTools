using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class EmployeeInfo : Form
    {
        public static EmployeeInfo employeeInfo;

        private string[] jzjdep;
        private string[] gztdep;
        private int maxSortNum;//新增时获取该工厂该部门的最大排序号，新增员工的排序号为最大排序号+1
        private int curid, cursortNum, swapid, swapsortNum;//交换排序号使用
        private int selectRowNum;//交换时选中的行号
        public EmployeeInfo()
        {
            InitializeComponent();
            employeeInfo = this;
        }

        private void EmployeeInfo_Load(object sender, EventArgs e)
        {
            //把佳之家和谷之田的部门提前写入到数组中，方便取数
            string sql = @"select depname from FDD_depinfo where factory=@factory";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", "佳之家"));
            if (ds.Tables[0].Rows.Count > 0)
            {
                jzjdep = ds.Tables[0].AsEnumerable().Select(r => r["depname"].ToString()).ToArray(); ;
            }
            else
            {
                return;
            }
            ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", "谷之田"));
            if (ds.Tables[0].Rows.Count > 0)
            {
                gztdep = ds.Tables[0].AsEnumerable().Select(r => r["depname"].ToString()).ToArray(); ;
            }
            else
            {
                return;
            }

            cb_factory.Text = "佳之家";
            if (jzjdep.Count() > 0)
            {
                cb_dep.DataSource = jzjdep;
                cb_dep.SelectedIndex = 0;
            }
            cb_type.SelectedIndex = 0;
            cb_onjob.SelectedIndex = 0;
            
            refresh();
            
        }

        void refresh()
        {

            string sql = @"select id,empcode,empname,factory,depname,ftype,case when onjob=1 then '在职' else '离职' end as onjob,sortnum from FDD_Employee 
                    where factory=@factory and depname=@depname and ftype=@ftype and onjob=@onjob order by sortnum asc";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", cb_factory.Text),
                SQLHelper.MakeParam("depname", cb_dep.Text), SQLHelper.MakeParam("ftype", cb_type.Text),
                SQLHelper.MakeParam("onjob", cb_onjob.Text=="是"?1:0));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                dataGridView1.DataSource = null;
                btn_up.Enabled = false;
                btn_down.Enabled = false;
                return;
            }
            //dataGridView1.Columns[0].Visible = false;  ///隐藏ID
            dataGridView1.Columns[1].Visible = false;  ///隐藏empcode
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 30;

            dataGridView1.Columns[0].Width = 300;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[2].HeaderText = "姓名";
            dataGridView1.Columns[3].Width = 300;
            dataGridView1.Columns[3].HeaderText = "工厂";
            dataGridView1.Columns[4].Width = 300;
            dataGridView1.Columns[4].HeaderText = "部室";
            dataGridView1.Columns[5].Width = 300;
            dataGridView1.Columns[5].HeaderText = "类别";
            dataGridView1.Columns[6].Width = 300;
            dataGridView1.Columns[6].HeaderText = "状态";
            dataGridView1.Columns[7].Width = 300;
            dataGridView1.Columns[7].HeaderText = "排序";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Rows[0].Selected = false; //取消选中
            dataGridView1.Columns["id"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["empname"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["factory"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["depname"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["ftype"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["onjob"].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns["sortnum"].SortMode = DataGridViewColumnSortMode.NotSortable;
            btn_up.Enabled = false;
            btn_down.Enabled = false;
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            MyVar.Showmode = "add";
            EmployeeDetail employeeDetail = new EmployeeDetail();
            if (employeeDetail.ShowDialog() == DialogResult.OK)
            {
                String empname = EmployeeDetail.employeeDetail.tb_empname.Text.Trim();
                String factory = EmployeeDetail.employeeDetail.cb_factory.Text.Trim();
                String depname = EmployeeDetail.employeeDetail.cb_dep.Text.Trim();
                String ftype= EmployeeDetail.employeeDetail.cb_type.Text.Trim();
                string sql = @"select max(sortnum) from FDD_Employee where 1=@data";
                //DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("factory", factory), SQLHelper.MakeParam("depname", depname),
                //             SQLHelper.MakeParam("ftype", ftype));
                DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
                if (ds.Tables[0].Rows[0][0]!= DBNull.Value)
                {
                    maxSortNum= (int)ds.Tables[0].Rows[0][0];
                }
                else
                {
                    maxSortNum = 0;
                    
                }

                sql = @"insert into FDD_Employee values (@empcode,@empname,@factory,@depname,@ftype,@sortnum,@onjob)";
                SQLHelper.ExecSql(sql, SQLHelper.MakeParam("empcode", ""), SQLHelper.MakeParam("empname", empname), 
                     SQLHelper.MakeParam("factory", factory), SQLHelper.MakeParam("depname", depname),
                     SQLHelper.MakeParam("ftype", ftype), SQLHelper.MakeParam("sortnum", maxSortNum+1), 
                     SQLHelper.MakeParam("onjob", EmployeeDetail.employeeDetail.cb_onjob.SelectedIndex==0?1:0));
                MsgBox.TipMsgShow(btn_add.Location.X + btn_add.Width + 20, btn_add.Location.Y + btn_add.Height - 100, this.Text, "添加成功！", this);
                refresh();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    MyVar.Showmode = "update";
                    EmployeeDetail employeeDetail = new EmployeeDetail();
                    if (employeeDetail.ShowDialog() == DialogResult.OK)
                    {
                        string empname = EmployeeDetail.employeeDetail.tb_empname.Text.Trim();
                        string factory = EmployeeDetail.employeeDetail.cb_factory.Text.Trim();
                        string depname = EmployeeDetail.employeeDetail.cb_dep.Text.Trim();
                        string ftype = EmployeeDetail.employeeDetail.cb_type.Text;

                        string sql = @"update FDD_Employee set empname=@empname,factory=@factory,depname=@depname,
                                       ftype=@ftype,onjob=@onjob where id=@id";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("empname", empname), SQLHelper.MakeParam("factory", factory),
                            SQLHelper.MakeParam("depname", depname), SQLHelper.MakeParam("ftype", ftype),
                            SQLHelper.MakeParam("onjob", EmployeeDetail.employeeDetail.cb_onjob.SelectedIndex == 0 ? 1 : 0), 
                            SQLHelper.MakeParam("id", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                        MsgBox.TipMsgShow(btn_update.Location.X + btn_update.Width + 20, btn_update.Location.Y + btn_update.Height - 100, this.Text, "修改成功！", this);
                        refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("选中要修改的员工");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    string sql = @"select * from FDD_AttendanceDetail where empid=@empid";
                    DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("empid", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("该员工存在考勤记录，无法删除，可修改为离职状态");
                        return;
                    }
                    else 
                    {
                        DialogResult dr = MessageBox.Show("确认删除员工“" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value.ToString() + "”", "提示", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            sql = @"delete from FDD_Employee where id=@id";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("id", dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                            MsgBox.TipMsgShow(btn_delete.Location.X + btn_delete.Width + 20, btn_delete.Location.Y + btn_delete.Height / 2 - 100, this.Text, "删除成功！", this);
                            refresh();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选中要删除的员工");
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_factory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_factory.Text == "佳之家")
            {
                cb_dep.DataSource = jzjdep;
            }
            else if (cb_factory.Text == "谷之田")
            {
                cb_dep.DataSource = gztdep;
            }
            refresh();
        }

        private void cb_dep_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void cb_onjob_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                btn_up.Enabled = false;
                btn_down.Enabled = true;
            }
            else if (e.RowIndex == dataGridView1.Rows.Count - 1)
            {
                btn_up.Enabled = true;
                btn_down.Enabled = false;
            }
            else 
            {
                btn_up.Enabled = true;
                btn_down.Enabled = true;
            }
            selectRowNum = e.RowIndex;
        }

       

        /// <summary>
        /// 交换排序号sortnum
        /// </summary>
        /// <param name="curid">当前id</param>
        /// <param name="cursortNum">当前排序号</param>
        /// <param name="swapid">要交换的id</param>
        /// <param name="swapsortNum">要交换的排序号</param>
        /// <returns></returns>
        private bool SwapSortNum(int curid,int cursortNum,int swapid,int swapsortNum)
        {
            string sql = @"UPDATE FDD_Employee SET sortNum=@sortNum WHERE Id=@Id";

            // 同时更新两条
            bool swap1 = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("sortNum", swapsortNum), SQLHelper.MakeParam("id", curid));
            bool swap2 = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("sortNum", cursortNum), SQLHelper.MakeParam("id", swapid));

            return swap1 && swap2;
        }
        private void btn_up_Click(object sender, EventArgs e)
        {
            curid = (int)dataGridView1.Rows[selectRowNum].Cells[0].Value;
            cursortNum = (int)dataGridView1.Rows[selectRowNum].Cells[7].Value;
            swapid= (int)dataGridView1.Rows[selectRowNum-1].Cells[0].Value;
            swapsortNum= (int)dataGridView1.Rows[selectRowNum - 1].Cells[7].Value;
            SwapSortNum(curid, cursortNum, swapid, swapsortNum);
            refresh();
            selectRowNum--;
            dataGridView1.Rows[selectRowNum].Selected = true;
            if (selectRowNum == 0)
            {
                btn_up.Enabled = false;
                btn_down.Enabled = true;
            }
            else
            {
                btn_up.Enabled = true;
                btn_down.Enabled = true;
            }
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            curid = (int)dataGridView1.Rows[selectRowNum].Cells[0].Value;
            cursortNum = (int)dataGridView1.Rows[selectRowNum].Cells[7].Value;
            swapid = (int)dataGridView1.Rows[selectRowNum +1].Cells[0].Value;
            swapsortNum = (int)dataGridView1.Rows[selectRowNum +1].Cells[7].Value;
            SwapSortNum(curid, cursortNum, swapid, swapsortNum);
            refresh();
            selectRowNum++;
            dataGridView1.Rows[selectRowNum].Selected = true;
            if (selectRowNum == dataGridView1.Rows.Count - 1)
            {
                btn_down.Enabled = false;
                btn_up.Enabled = true;
            }
            else
            {
                btn_up.Enabled = true;
                btn_down.Enabled = true;
            }
        }



    }
}
