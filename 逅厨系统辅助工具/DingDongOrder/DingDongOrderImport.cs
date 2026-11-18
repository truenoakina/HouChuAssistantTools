using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace 逅厨系统辅助工具
{
    public partial class DingDongOrderImport : Form
    {
        private DataTable dt, dt2;
        private bool flag = false;
        private string altermsg = "";//警告信息，警告外部订单中哪些物料在金蝶中缺少69码
        public DingDongOrderImport()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string[] fileNames = null;//选取的文件列表
            string fileName = "";//所有文件名称集合
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && dialog.FileNames.Length != 0)
            {
                try
                {
                    fileNames = dialog.FileNames;//读取所选取文件的列表
                    foreach (string file in fileNames)
                    {
                        //判断文件是否已打开，需要关闭后操作
                        try
                        {
                            System.IO.FileStream stream = System.IO.File.OpenWrite(file);
                            stream.Close();
                        }
                        catch
                        {
                            MessageBox.Show("文件已经被打开，请关闭后再操作.");
                            return;
                        }
                        fileName = fileName + System.IO.Path.GetFileNameWithoutExtension(file) + ";";
                        dt = ExcelHelper.ExcelToTableDingDong(fileNames);



                    }
                    excelinfo.Text = fileName;
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return;
            }

        }



        private void customer_Click(object sender, EventArgs e)
        {
            //HCcustomer hccustomer = new HCcustomer();
            //hccustomer.ShowDialog();
            //清理掉异常导致的DingDongOrder中的数据
            string sql = @"delete from DingDongOrder where 1=@num";
            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("num", 1));
            MessageBox.Show("清理结束");
        }

        private void DingDongOrderImport_Load(object sender, EventArgs e)
        {
            lbl_info.Text = "点击确认转换后，后台需要一段时间生成，会暂时出现卡顿现象，请勿点击，耐心等待转换完成";
        }

        private void btn_stockCusInfo_Click(object sender, EventArgs e)
        {
            DingDongCusInfo dingDongCusInfo = new DingDongCusInfo();
            dingDongCusInfo.ShowDialog();
        }


        /// <summary>
        /// 提示文字改变
        /// </summary>
        

        private void btn_change_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.Rows.Count == 0)
            {
                lbl_info.Text = "";
                return;
            }
            else
            {
                //string guid = System.Guid.NewGuid().ToString();
                string guid = Dns.GetHostName();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sql = @"insert into DingDongOrder (guid,id,fstock,fdate,fspcode,fspname,fprice,fqty) values (@guid,@id,@fstock,@fdate,@fspcode,@fspname,@fprice,@fqty)";
                    flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid), SQLHelper.MakeParam("id", dt.Rows[i]["序号"].ToString()), SQLHelper.MakeParam("fstock", dt.Rows[i]["入库仓"].ToString()),
                                             SQLHelper.MakeParam("fdate", dt.Rows[i]["到货日期"].ToString()), SQLHelper.MakeParam("fspcode", dt.Rows[i]["商品ID"].ToString()),
                                             SQLHelper.MakeParam("fspname", dt.Rows[i]["商品名称"].ToString()), SQLHelper.MakeParam("fprice", dt.Rows[i]["采购单价"].ToString()),
                                             SQLHelper.MakeParam("fqty", dt.Rows[i]["采购数量"].ToString()));
                }
                if (flag == true)
                {
                    string sql = @"select distinct dd.fspcode from DingDongOrder dd where not exists (select 1 from AIShlhcsvr.dbo.T_BD_MATERIAL m 
                                        inner join  AIShlhcsvr.dbo.T_BD_MATERIALBASE mb on m.FMATERIALID=mb.FMATERIALID
                                        where m.FMNEMONICCODE=dd.fspcode and m.FUSEORGID in (1,441894) and mb.FCATEGORYID=241) and dd.guid=@guid";
                    DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("guid", guid));
                    dt2 = ds.Tables[0];
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (i < dt2.Rows.Count - 1)
                            {
                                altermsg = altermsg + "'" + dt2.Rows[i][0].ToString() + "'" + ",";
                            }
                            else
                            {
                                altermsg = altermsg + "'" + dt2.Rows[i][0].ToString() + "'";
                            }
                        }
                        sql = @"delete from DingDongOrder where guid=@guid";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        MessageBox.Show("商品ID" + altermsg + "在金蝶中没有匹配到商品，请检查系统中该物料的一便码");
                    }
                    else
                    {
                        sql = @"select o.id,dc.jdCusID as cuscode,dc.jdCusName as cusname,m.fnumber as kcode,ml.fname as kname,u.fnumber as code,ul.fname as unit,Convert(decimal(18,0),o.fqty) as qty,o.fprice,o.fdate  
                                       from (select guid,id,fstock,fdate,fspcode,fspname,fprice,sum(fqty) as fqty from DingDongOrder
                                    group by guid,id,fstock,fdate,fspcode,fspname,fprice) o 
                                       left join AIShlhcsvr.dbo.T_BD_MATERIAL m on  o.fspcode=m.FMNEMONICCODE
                                       inner join AIShlhcsvr.dbo.T_BD_MATERIAL_L ml on m.FMATERIALID=ml.FMATERIALID
                                       inner join AIShlhcsvr.dbo.T_BD_MATERIALBASE mb on m.FMATERIALID=mb.FMATERIALID
                                       inner join AIShlhcsvr.dbo.t_BD_Unit u on u.FUNITID=mb.FBASEUNITID
                                       inner join AIShlhcsvr.dbo.t_BD_Unit_L ul on u.FUNITID=ul.FUNITID
                                       inner join DingDongStockCus dc on dc.dingdongStock=o.fstock
                                        where m.FUSEORGID=1  and guid=@guid order by o.id";
                        ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("guid", guid));
                        dt2 = ds.Tables[0];

                        string fileExt = ".xlsx";

                        string strt = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;

                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog(this) == DialogResult.OK)
                        {
                            ExcelHelper.TableToExcelDingDong(fbd.DirectoryPath + @"\" + "叮咚订单" + strt, dt2 as DataTable, fileExt);
                            MessageBox.Show(fbd.DirectoryPath + @"\" + "叮咚订单" + strt + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            sql = @"delete from DingDongOrder where guid=@guid";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                            lbl_info.Text = "";
                        }
                        else if (fbd.ShowDialog(this) == DialogResult.Cancel)
                        {
                            sql = @"delete from DingDongOrder where guid=@guid";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                            lbl_info.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("数据转换失败，请检查导入数据");
                    return;
                }
            }
        }
    }

}
