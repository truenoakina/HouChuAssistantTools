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
    public partial class OutOrder : Form
    {
        private DataTable dt, dt2;
        private bool flag = false;
        private string altermsg = "";//警告信息，警告外部订单中哪些物料在金蝶中缺少69码
        public OutOrder()
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
                        dt = ExcelHelper.ExcelToTableWB(fileNames);



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

        private void good_Click(object sender, EventArgs e)
        {
            HCMaterial hcmaterial = new HCMaterial();
            hcmaterial.ShowDialog();
        }

        private void customer_Click(object sender, EventArgs e)
        {
            //HCcustomer hccustomer = new HCcustomer();
            //hccustomer.ShowDialog();
            //清理掉异常导致的outorder中的数据
            string sql = @"delete from OutOrder where 1=@num";
            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("num", 1));
            MessageBox.Show("清理结束");
        }

        private void OutOrder_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = System.DateTime.Now.AddDays(1);
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
                    string sql = @"insert into OutOrder (guid,id,cusname,goodcode,qty) values (@guid,@id,@cusname,@goodcode,@qty)";
                    flag = SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid), SQLHelper.MakeParam("id", dt.Rows[i]["序号"].ToString()), SQLHelper.MakeParam("cusname", dt.Rows[i]["客户"].ToString()),
                                             SQLHelper.MakeParam("goodcode", dt.Rows[i]["货号"].ToString()), SQLHelper.MakeParam("qty", dt.Rows[i]["数量"].ToString()));
                }
                if (flag == true)
                {
                    string sql = @"select distinct o.goodcode from OutOrder o where not exists (select 1 from AIShlhcsvr.dbo.T_BD_MATERIALBASE mb where mb.FBARCODE=o.goodcode and mb.FUSEORGID=1)
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
                        sql = @"delete from OutOrder where guid=@guid";
                        SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        MessageBox.Show("货号" + altermsg + "在金蝶中没有匹配到商品，请检查系统中该物料的69码(条码)");
                    }
                    else
                    {
                        sql = @"select o.id,c.fnumber as cuscode,cl.fname as cusname,m.fnumber as kcode,ml.fname as kname,u.fnumber as code,ul.fname as unit,Convert(decimal(18,0),o.qty) as qty 
                                    from outorder o left join AIShlhcsvr.dbo.T_BD_MATERIALBASE mb on o.goodcode=mb.FBARCODE
                                    inner join AIShlhcsvr.dbo.T_BD_MATERIAL m on m.FMATERIALID=mb.FMATERIALID 
                                    inner join AIShlhcsvr.dbo.T_BD_MATERIAL_L ml on m.FMATERIALID=ml.FMATERIALID
                                    inner join AIShlhcsvr.dbo.t_BD_Unit u on u.FUNITID=mb.FBASEUNITID
                                    inner join AIShlhcsvr.dbo.t_BD_Unit_L ul on u.FUNITID=ul.FUNITID
                                    inner join AIShlhcsvr.dbo.T_BD_CUSTOMER_L cl on cl.FNAME=o.cusname
                                    inner join AIShlhcsvr.dbo.T_BD_CUSTOMER c on c.fcustid=cl.FCUSTID
                                     where m.FUSEORGID=1 and c.FUSEORGID=1 and 1=@num order by o.id";
                        ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("num", 1));
                        dt2 = ds.Tables[0];

                        string fileExt = ".xlsx";

                        //nowDate = DateTime.Now.ToString("D");
                        //取查询日期段作为文件名称
                        MyVar.saleDate = dateTimePicker1.Value.Year + "/" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Day;
                        MyVar.wantDate = dateTimePicker2.Value.Year + "/" + dateTimePicker2.Value.Month + "/" + dateTimePicker2.Value.Day;
                        string strt = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;

                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog(this) == DialogResult.OK)
                        {
                            ExcelHelper.TableToExcelWB(fbd.DirectoryPath + @"\" + "外部订单" + strt, dt2 as DataTable, fileExt);
                            MessageBox.Show(fbd.DirectoryPath + @"\" + "外部订单" + strt + fileExt + " 导出成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            sql = @"delete from outorder where guid=@guid";
                            SQLHelper.ExecSql(sql, SQLHelper.MakeParam("guid", guid));
                        }
                        else if (fbd.ShowDialog(this) == DialogResult.Cancel)
                        {
                            sql = @"delete from outorder where guid=@guid";
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
