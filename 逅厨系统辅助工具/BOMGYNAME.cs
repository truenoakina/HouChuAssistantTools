using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class BOMGYNAME : Form
    {
        public BOMGYNAME()
        {
            InitializeComponent();
        }

        private void BOMGYNAME_Load(object sender, EventArgs e)
        {
            string sql = @"select distinct t.FNAME from (select  bl.FNAME from T_ENG_BOM_L bl inner join T_ENG_BOM b on b.fid=bl.FID 
                            where b.FDOCUMENTSTATUS='C' and b.FFORBIDSTATUS='A' and b.FGROUP in (select g.fid from T_ENG_BOMGROUP g inner join T_ENG_BOMGROUP_L gl on g.FID=gl.FID
                            where gl.FNAME in ('三明治','面包','蛋糕甜点'))
                            union all
                            select  gyl.FNAME from T_ENG_ROUTE gy inner join T_ENG_ROUTE_L gyl on gyl.FID=gy.FID 
                            where gy.FFORBIDSTATUS='A' and gy.FROUTEGROUPID in ('217768','217769','217770')) t where 1=@data order by t.fname";
            DataSet ds = SQLHelperJD.GetDataSet(sql, SQLHelperJD.MakeParam("data", 1));
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 300;
        }
    }
}
