using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;


namespace 逅厨系统辅助工具
{
    public partial class OutOrderGDHC : Form
    {
        private DataTable dt, dt2;
        private bool flag = false;
        private string altermsg = "";//警告信息，警告外部订单中哪些物料在金蝶中缺少69码
        private string guid = "";   //随机字符串，用于保证转换数据时绑定用户，不被其他用户影响

        public OutOrderGDHC()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string sql = @"delete from GDHCWEMOrder where guid=@guid";
            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));

            //PDF解析成string数组后，取包含“江苏逅厨供应链管理有限公司”字符串的行号，如果该行号字符串长度大于13，则用旧的转换方法，反之用新的转换方法
            //int rowNo = 0;
            //string txtFromPdf = ""; //PDF转换成的字符串
            string[] fileNames = null;//选取的文件列表
            string fileName = "";//所有文件名称集合
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "PDF文件(*.pdf)|*.pdf";
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
                        //Console.WriteLine(ExcelHelper.PdfToTableGDHC(fileNames));
                        dt = ExcelHelper.PdfToTableGDHC(fileNames);
                    }
                    pdfinfo.Text = fileName;
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


        private void OutOrder_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = System.DateTime.Now;
            guid = Dns.GetHostName();
            lbl_info.Text = "上传或者点击转换之后会有短暂时间的卡顿，为正常情况，请耐心等待";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
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
                    string sql = @"insert into GDHCWEMOrder (guid,id,ordertype,ordernumber,fspcode,fqty,fprice) values (@guid,@id,@ordertype,@ordernumber,@fspcode,@fqty,@fprice)";
                    flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid), SQLHelper.MakeParam("id", dt.Rows[i]["序号"].ToString()),
                        SQLHelper.MakeParam("ordertype", dt.Rows[i]["订单类别"].ToString()),SQLHelper.MakeParam("ordernumber", dt.Rows[i]["订单号"].ToString()),
                        SQLHelper.MakeParam("fspcode", dt.Rows[i]["商品代码"].ToString()), SQLHelper.MakeParam("fqty", dt.Rows[i]["数量"].ToString()), SQLHelper.MakeParam("fprice", dt.Rows[i]["单价"].ToString()));
                }
                if (flag == true)
                {
                    
                    string sql = @"select distinct l.fspcode from GDHCWEMOrder l where not exists (select 1 from AIShlhcsvr.dbo.T_BD_MATERIAL m where m.FMNEMONICCODE=l.fspcode and m.FUSEORGID=441894)
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
                        sql = @"delete from GDHCWEMOrder where guid=@guid";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        MessageBox.Show("沃尔玛商品编码(一便码)" + altermsg + "在金蝶中没有匹配到商品，请检查系统中该物料的一便码");
                    }
                    else
                    {
                        sql = @"select l.id,l.ordernumber,case when l.ordertype='社区' then 'CUST0579' else 'CUST0354' end as cuscode,
                                m.fnumber as kcode,ml.fname as kname,u.fnumber as code,ul.fname as kunit,Convert(decimal(18,0),l.fqty) as num,l.fprice from GDHCWEMOrder l
                                left join AIShlhcsvr.dbo.T_BD_MATERIAL m on m.FMNEMONICCODE=l.fspcode 
                                inner join AIShlhcsvr.dbo.T_BD_MATERIAL_L ml on m.FMATERIALID=ml.FMATERIALID
                                inner join AIShlhcsvr.dbo.T_BD_MATERIALBASE mb on m.FMATERIALID=mb.FMATERIALID
                                inner join AIShlhcsvr.dbo.t_BD_Unit u on u.FUNITID=mb.FBASEUNITID
                                inner join AIShlhcsvr.dbo.t_BD_Unit_L ul on u.FUNITID=ul.FUNITID
                                where l.guid=@guid and m.FUSEORGID=441894
                                order by l.id";
                        ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("guid", guid));
                        dt2 = ds.Tables[0];

                        string fileExt = ".xlsx";

                        //nowDate = DateTime.Now.ToString("D");
                        //取查询日期段作为文件名称
                        MyVar.saleDate = dateTimePicker1.Value.Year + "/" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Day;
                        string strt = dateTimePicker1.Text;


                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog(this) == DialogResult.OK)
                        {
                            //广东逅厨沃尔玛转换
                            ExcelHelper.TableToExcelGDHCWLM(fbd.DirectoryPath + @"\" + "沃尔玛订单" +  strt, dt2 as DataTable, fileExt);
                            
                            
                            MessageBox.Show(fbd.DirectoryPath + @"\" + "沃尔玛订单" + strt + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            sql = @"delete from GDHCWEMOrder where guid=@guid";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        }
                        else if (fbd.ShowDialog(this) == DialogResult.Cancel)
                        {
                            sql = @"delete from GDHCWEMOrder where guid=@guid";
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
