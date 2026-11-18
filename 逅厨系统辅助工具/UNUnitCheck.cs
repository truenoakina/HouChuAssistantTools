using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class UNUnitCheck : Form
    {
        public UNUnitCheck()
        {
            InitializeComponent();
        }

        private void UNUnitCheck_Load(object sender, EventArgs e)
        {
            string sql = @"select gyl.fname as 工艺名称,m.FNUMBER as 物料编码,ml.FNAME as 物料名称,mp.F_PAEZ_integer as 组成量,mp.f_paez_text as 组成单位 from T_PRD_INSTOCKENTRY rks inner join T_PRD_INSTOCK rk on rk.FID=rks.FID
                            inner join T_BD_MATERIAL m on m.FMATERIALID=rks.FMATERIALID
                            inner join T_BD_MATERIAL_L ml on m.FMATERIALID=ml.FMATERIALID 
                            inner join T_BD_MATERIALPRODUCE mp on mp.FMATERIALID=rks.FMATERIALID
                            left join T_ENG_ROUTE gy on gy.FMATERIALID=rks.FMATERIALID
                            left join T_ENG_ROUTE_L gyl on gyl.FID=gy.FID
                            where rk.FDATE>=dateadd(dd,-3,getdate()) and rk.FPRDORGID=100007 and 1=@data";
            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("data", 1));
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 200;
        }
    }
}
