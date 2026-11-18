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
    public partial class CGDJQST_WL : Form
    {
        public static CGDJQST_WL cGDJQST_WL;

        public string orgName,wlName;
        public int year;

        private void CGDJQST_WL_Load(object sender, EventArgs e)
        {
            string sql = "";
            if (orgName == "江苏逅厨供应链管理有限公司")///如果选择江苏逅厨，取江苏逅厨和上海逅厨的数据
            { 
            sql = @"select distinct m.FNUMBER as 物料编码,ml.fname as 物料名称,ml.FSPECIFICATION as 规格型号,case when ul.fname='g' then 'kg'when ul.fname='ml' then 'L' else ul.fname end as 计价单位
                        from T_BD_MATERIAL_L ml inner join T_STK_INSTOCKENTRY rkd on rkd.FMATERIALID=ml.FMATERIALID
                        inner join T_BD_MATERIAL m on m.FMATERIALID=ml.FMATERIALID
                        inner join T_STK_INSTOCK rk on rk.fid = rkd.fid inner join T_ORG_Organizations_L zz on rk.FSTOCKORGID = zz.FORGID
                        inner join t_BD_Unit_L ul on ul.FUNITID=rkd.FBASEUNITID
                        inner join T_BD_MATERIALBASE mb on rkd.FMATERIALID = mb.FMATERIALID where(year(rk.FDATE) =" + year + " or year(rk.FDATE) =" + (year - 1) + ") and mb.FCATEGORYID in (237,238) and zz.FNAME in ('江苏逅厨供应链管理有限公司','逅厨(上海)食品科技有限公司') and ml.fname like '%" + wlName + "%' and 1=@data";
            }else
            {
             sql = @"select distinct m.FNUMBER as 物料编码,ml.fname as 物料名称,ml.FSPECIFICATION as 规格型号,case when ul.fname='g' then 'kg'when ul.fname='ml' then 'L' else ul.fname end as 计价单位
                        from T_BD_MATERIAL_L ml inner join T_STK_INSTOCKENTRY rkd on rkd.FMATERIALID=ml.FMATERIALID
                        inner join T_BD_MATERIAL m on m.FMATERIALID=ml.FMATERIALID
                        inner join T_STK_INSTOCK rk on rk.fid = rkd.fid inner join T_ORG_Organizations_L zz on rk.FSTOCKORGID = zz.FORGID
                        inner join t_BD_Unit_L ul on ul.FUNITID=rkd.FBASEUNITID
                        inner join T_BD_MATERIALBASE mb on rkd.FMATERIALID = mb.FMATERIALID where(year(rk.FDATE) =" + year + " or year(rk.FDATE) =" + (year - 1) + ") and mb.FCATEGORYID in (237,238) and zz.FNAME ='" + orgName + "' and ml.fname like '%" + wlName + "%' and 1=@data";
            }


            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("data", 1));
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.ReadOnly = true;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CGDJQST cGDJQST = (CGDJQST)this.Owner;  //将本窗体拥有者强制设为CGDJQST类的实例
            if (e.RowIndex >= 0 & e.ColumnIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                cGDJQST.fnumber= dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                cGDJQST.fname = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                cGDJQST.guige = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                cGDJQST.uname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
            else if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
        }

        public CGDJQST_WL()
        {
            InitializeComponent();
            cGDJQST_WL = this;
        }


    }
}
