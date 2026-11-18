using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;


namespace 逅厨系统辅助工具
{
    public partial class OutSongHuoDanGDHCDDD : Form
    {
        private DataTable dt, dt2;
        private bool flag = false;
        private string altermsg = "";//警告信息，警告外部订单中哪些物料在金蝶中缺少69码
        private string guid = "";   //随机字符串，等于主机名称
        public List<string> sheets = new List<string> { };

        

        public static OutSongHuoDanGDHCDDD outSongHuoDanGDHCDDD;//静态实例化对象
        public OutSongHuoDanGDHCDDD()
        {
            InitializeComponent();
            outSongHuoDanGDHCDDD = this;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = "";//文件名称
            string sql = @"delete from WOERMASHDDDD where guid=@guid";
            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && dialog.FileNames.Length != 0)
            {
                try
                {
                    fileName = dialog.FileName;//读取所选取文件
                                               //判断文件是否已打开，需要关闭后操作
                    try
                    {
                        System.IO.FileStream stream = System.IO.File.OpenWrite(fileName);
                        stream.Close();
                    }
                    catch
                    {
                        MessageBox.Show("文件已经被打开，请关闭后再操作.");
                        return;
                    }
                    dt = ExcelHelper.ExcelToTableGDHCSHDDDD(fileName);
                    tb_filename.Text = Path.GetFileName(fileName) +"中页签:"+MyVar.Sheetname;
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


        private void OutSongHuoDanGDHCDDD_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = System.DateTime.Now;
            guid = Dns.GetHostName();
            label3.Text = "文件\n名称";
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            if (tb_dep.Text.ToString().Trim().Equals(""))
            {
                MessageBox.Show("部门不能为空，请检查");
                return;
            }

            dt = dataGridView1.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show("未上传文档，请检查");
                return;
            }
            else
            {
                //guid = RandomStr.GetRandomString(20);
                

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sql = @"insert into WOERMASHDDDD (guid,orderid,shop,spcode,spname,num) values (@guid,@orderid,@shop,@spcode,@spname,@num)";
                    flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid), SQLHelper.MakeParam("orderid", dt.Rows[i]["订单号"].ToString()), SQLHelper.MakeParam("shop", dt.Rows[i]["门店"].ToString()), SQLHelper.MakeParam("spcode", dt.Rows[i]["商品号"].ToString()), SQLHelper.MakeParam("spname", dt.Rows[i]["商品名称"].ToString()),SQLHelper.MakeParam("num", dt.Rows[i]["数量"].ToString()));
                }
                if (flag == true)
                {
                    
                    string sql = @"select distinct l.spcode from WOERMASHDDDD l where not exists (select 1 from AIShlhcsvr.dbo.T_BD_MATERIAL m where m.FMNEMONICCODE=l.spcode and m.FUSEORGID=441894)
                                                             and l.guid=@guid";
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
                        sql = @"delete from WOERMASHDDDD where guid=@guid";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        MessageBox.Show("沃尔玛商品编码(一便码)" + altermsg + "在金蝶中没有匹配到商品，请检查系统中该物料的一便码");
                    }
                    else
                    {
                        sql = @"select l.orderid,l.shop,l.spcode,l.spname,mb.FBARCODE,floor(mb.FNETWEIGHT),l.num  from WOERMASHDDDD l
                                                    left join AIShlhcsvr.dbo.T_BD_MATERIAL m on m.FMNEMONICCODE=l.spcode 
                                                    inner join AIShlhcsvr.dbo.T_BD_MATERIALBASE mb on m.FMATERIALID=mb.FMATERIALID
                                                    where l.guid=@guid and m.FUSEORGID=441894
                                                    order by l.orderid,l.shop,l.spcode";
                        ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("guid", guid));
                        dt2 = ds.Tables[0];


                        string fileExt = ".xlsx";

                        //nowDate = DateTime.Now.ToString("D");
                        //取查询日期段作为文件名称
                        MyVar.saleDate = dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
                        MyVar.Dep = tb_dep.Text;
                        
                        string strt = dateTimePicker1.Text;

                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog(this) == DialogResult.OK)
                        {
                            //广东逅厨沃尔玛送货单转换
                            ExcelHelper.TableToExcelGDHCWEMSHDDDD(fbd.DirectoryPath + @"\" + "沃尔玛送货单" + strt, dt2 as DataTable, fileExt);


                            MessageBox.Show(fbd.DirectoryPath + @"\" + "沃尔玛送货单" + strt + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            sql = @"delete from WOERMASHDDDD where guid=@guid";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        }
                        else if (fbd.ShowDialog(this) == DialogResult.Cancel)
                        {
                            sql = @"delete from WOERMASHDDDD where guid=@guid";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        }
                    }

                }
                else
                {
                    MessageBox.Show("上传文档异常，请检查");
                    return;
                }
            }
        }

    }
}
