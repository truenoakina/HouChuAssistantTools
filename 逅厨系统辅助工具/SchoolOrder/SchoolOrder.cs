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
    public partial class SchoolOrder : Form
    {
        private DataTable dt, dt2;
        private bool flag = false;
       
        private string altermsg = "";//警告信息，
        public SchoolOrder()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = "";//文件名称
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
                    dt = ExcelHelper.ExcelToTableSchool(fileName);

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

        private void good_Click(object sender, EventArgs e)
        {
            HCMaterial hcmaterial = new HCMaterial();
            hcmaterial.ShowDialog();
        }

        private void customer_Click(object sender, EventArgs e)
        {
            HCcustomer hccustomer = new HCcustomer();
            hccustomer.ShowDialog();
        }

        private void OutOrder_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            else
            {
                //string guid = System.Guid.NewGuid().ToString();
                string guid = Dns.GetHostName();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sql = @"insert into SchoolOrder (guid,id,fdate,cusname,ftype,fnumber,qty,funit) values (@guid,@id,@fdate,@cusname,@ftype,@fnumber,@qty,@funit)";
                    flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid), SQLHelper.MakeParam("id", dt.Rows[i]["序号"].ToString()),
                                        SQLHelper.MakeParam("fdate", dt.Rows[i]["日期"].ToString()), SQLHelper.MakeParam("cusname", dt.Rows[i]["客户"].ToString()),
                                        SQLHelper.MakeParam("ftype", dt.Rows[i]["类别"].ToString()), SQLHelper.MakeParam("fnumber", dt.Rows[i]["编码"].ToString()), SQLHelper.MakeParam("qty", dt.Rows[i]["数量"].ToString()),
                                        SQLHelper.MakeParam("funit", dt.Rows[i]["单位"].ToString()));
                }
                if (flag == true)
                {
                    string sql = @"select distinct o.fnumber from SchoolOrder o where not exists (select 1 from AIShlhcsvr.dbo.t_bd_material m where m.fnumber=o.fnumber and m.FUSEORGID=516100)
                                                             and o.guid=@guid";
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
                        sql = @"delete from SchoolOrder where guid=@guid";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        MessageBox.Show("编码" + altermsg + "在金蝶中没有匹配到物料，请检查系统中是否存在该编码");
                    }
                    else
                    {
                        sql = @"select o.id,o.fdate,c.fnumber as cuscode,cl.fname as cusname,m.fnumber as kcode,ml.fname as kname,u.fnumber as code,o.funit as unit,Convert(decimal(18,0),o.qty) as qty,ftype.fnumber,o.ftype,sl.FTAXRATE
                                    from SchoolOrder o inner join AIShlhcsvr.dbo.T_BD_MATERIAL m on m.FNUMBER=o.fnumber
									inner join AIShlhcsvr.dbo.T_BD_MATERIALBASE mb on m.FMATERIALID=mb.FMATERIALID
                                    inner join AIShlhcsvr.dbo.T_BD_MATERIAL_L ml on m.FMATERIALID=ml.FMATERIALID
                                    inner join AIShlhcsvr.dbo.t_BD_Unit_L ul on ul.fname=o.funit
									inner join AIShlhcsvr.dbo.t_BD_Unit u on u.FUNITID=ul.FUNITID
                                    inner join AIShlhcsvr.dbo.T_BD_CUSTOMER_L cl on cl.FNAME=o.cusname
                                    inner join AIShlhcsvr.dbo.T_BD_CUSTOMER c on c.fcustid=cl.FCUSTID
                                    inner join AIShlhcsvr.dbo.T_BD_TAXRATE sl on sl.fid=mb.FTAXRATEID
									inner join (select fz.FNUMBER,fzl.FDATAVALUE from AIShlhcsvr.dbo.T_BAS_ASSISTANTDATAENTRY fz inner join AIShlhcsvr.dbo.T_BAS_ASSISTANTDATAENTRY_L fzl on  fzl.FENTRYID=fz.FMASTERID where FID='62ce74cf6fff79') ftype on ftype.FDATAVALUE=o.ftype
                                     where m.FUSEORGID=516100 and c.FUSEORGID=516100 and 1=@num order by o.fdate,cl.fname,ftype.fnumber";
                        ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("num", 1));
                        dt2 = ds.Tables[0];

                        string fileExt = ".xlsx";

                        //nowDate = DateTime.Now.ToString("D");
                        //取查询日期段作为文件名称
                        string strt = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog(this) == DialogResult.OK)
                        {
                            ExcelHelper.TableToExcelSchool(fbd.DirectoryPath + @"\" + "学校订单" + strt, dt2 as DataTable, fileExt);
                            MessageBox.Show(fbd.DirectoryPath + @"\" + "学校订单" + strt + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            sql = @"delete from SchoolOrder where guid=@guid";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        }
                        else if (fbd.ShowDialog(this) == DialogResult.Cancel)
                        {
                            sql = @"delete from SchoolOrder where guid=@guid";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
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
