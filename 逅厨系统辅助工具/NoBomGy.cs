using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class NoBomGy : Form
    {
        public NoBomGy()
        {
            InitializeComponent();
        }

        private void NoBomGy_Load(object sender, EventArgs e)
        {

            string sql= @"select distinct '物料清单' as 类别,ml.FNAME from T_PRD_INSTOCKENTRY rks inner join T_PRD_INSTOCK rk on rk.FID=rks.FID
                            inner join T_BD_MATERIAL m on m.FMATERIALID=rks.FMATERIALID
                            inner join T_BD_MATERIAL_L ml on m.FMATERIALID=ml.FMATERIALID 
                            where rk.FDATE>=dateadd(dd,-4,getdate()) and rk.FPRDORGID=100007 and rks.FMATERIALID not in (select FMATERIALID from T_ENG_BOM where FUSEORGID=100007)
                            union all
                            select distinct '工艺路线',ml.FNAME from T_PRD_INSTOCKENTRY rks inner join T_PRD_INSTOCK rk on rk.FID=rks.FID
                            inner join T_BD_MATERIAL m on m.FMATERIALID=rks.FMATERIALID
                            inner join T_BD_MATERIAL_L ml on m.FMATERIALID=ml.FMATERIALID 
                            where rk.FDATE>=dateadd(dd,-4,getdate()) and rk.FPRDORGID=100007 and rks.FMATERIALID not in (select FMATERIALID from T_ENG_ROUTE where FUSEORGID=100007) and 1=@data";
            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("data", 1));
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[1].Width = 300;
        }
    }
}
