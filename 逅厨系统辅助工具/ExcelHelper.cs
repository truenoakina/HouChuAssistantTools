using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using System.Windows.Forms;
using Spire.Pdf;
using Spire.Pdf.Texts;
using NPOI.SS.Util;
using ZXing;
using System.Drawing.Imaging;
using System.Linq;

namespace 逅厨系统辅助工具
{
    class ExcelHelper
    {


        /// <summary>
        /// Datable导出成Excel罗森用
        /// </summary>
        /// <param name="path">文件绝对路径</param>
        /// <param name="dt"></param>
        /// <param name="fileExt">文件后缀</param>
        public static void TableToExcel(string path, DataTable dt, string fileExt)
        {
            IWorkbook workbook = null;
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                //return null;
                return;
            }
            ISheet sheet = workbook.CreateSheet("销售订单#基本信息(FBillHead)");

            //定义单元格背景颜色类型
            ICellStyle s = workbook.CreateCellStyle();
            s.FillForegroundColor = HSSFColor.Red.Index;
            s.FillPattern = FillPattern.SolidForeground;

            //表头第一行 
            IRow row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("FBillHead(SAL_SaleOrder)");
            row.CreateCell(1).SetCellValue("FBillTypeID");
            row.CreateCell(2).SetCellValue("FBillTypeID#Name");
            row.CreateCell(3).SetCellValue("FDate");
            row.CreateCell(4).SetCellValue("FSaleOrgId");
            row.CreateCell(5).SetCellValue("FSaleOrgId#Name");
            row.CreateCell(6).SetCellValue("FCustId");
            row.CreateCell(7).SetCellValue("FCustId#Name");
            row.CreateCell(8).SetCellValue("FSalerId");
            row.CreateCell(9).SetCellValue("FSalerId#Name");
            row.CreateCell(10).SetCellValue("FSaleOrderEntry");
            row.CreateCell(11).SetCellValue("FMaterialId");
            row.CreateCell(12).SetCellValue("FMaterialId#Name");
            row.CreateCell(13).SetCellValue("FUnitID");
            row.CreateCell(14).SetCellValue("FUnitID#Name");
            row.CreateCell(15).SetCellValue("FQty");
            row.CreateCell(16).SetCellValue("FTaxPrice");
            row.CreateCell(17).SetCellValue("FEntryTaxRate");
            row.CreateCell(18).SetCellValue("FDeliveryDate");
            row.CreateCell(19).SetCellValue("FSettleOrgIds");
            row.CreateCell(20).SetCellValue("FSettleOrgIds#Name");
            row.CreateCell(21).SetCellValue("FReserveType");
            row.CreateCell(22).SetCellValue("FOUTLMTUNIT");
            row.CreateCell(23).SetCellValue("*Split*3");
            row.CreateCell(24).SetCellValue("FTaxDetailSubEntity");

            //表头第二行 
            IRow row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("*基本信息(序号)");
            row1.CreateCell(1).SetCellValue("*(基本信息)单据类型#编码");
            row1.CreateCell(2).SetCellValue("(基本信息)单据类型#名称");
            row1.CreateCell(3).SetCellValue("*(基本信息)日期");
            row1.CreateCell(4).SetCellValue("*(基本信息)销售组织#编码");
            row1.CreateCell(5).SetCellValue("(基本信息)销售组织#名称#Name");
            row1.CreateCell(6).SetCellValue("*(基本信息)客户#编码");
            row1.CreateCell(7).SetCellValue("(基本信息)客户#名称#Name");
            row1.CreateCell(8).SetCellValue("*(基本信息)销售员#编码");
            row1.CreateCell(9).SetCellValue("(基本信息)销售员#名称");
            row1.CreateCell(10).SetCellValue("*订单明细(序号)");
            row1.CreateCell(11).SetCellValue("*(订单明细)物料编码#编码");
            row1.CreateCell(12).SetCellValue("(订单明细)物料编码#名称");
            row1.CreateCell(13).SetCellValue("*(订单明细)销售单位#编码");
            row1.CreateCell(14).SetCellValue("(订单明细)销售单位#名称");
            row1.CreateCell(15).SetCellValue("(订单明细)销售数量");
            row1.CreateCell(16).SetCellValue("(订单明细)含税单价");
            row1.CreateCell(17).SetCellValue("(订单明细)税率%");
            row1.CreateCell(18).SetCellValue("*(订单明细)要货日期");
            row1.CreateCell(19).SetCellValue("*(订单明细)结算组织#编码");
            row1.CreateCell(20).SetCellValue("(订单明细)结算组织#名称");
            row1.CreateCell(21).SetCellValue("*(订单明细)预留类型");
            row1.CreateCell(22).SetCellValue("*(订单明细)超发控制单位类型");
            row1.CreateCell(23).SetCellValue("间隔列");
            row1.CreateCell(24).SetCellValue("*税务明细(序号)");


            //数据第一行
            IRow row2 = sheet.CreateRow(2);
            row2.CreateCell(0).SetCellValue("100000");
            row2.CreateCell(1).SetCellValue("XSDD01_SYS");
            row2.CreateCell(2).SetCellValue("标准销售订单");
            row2.CreateCell(3).SetCellValue(MyVar.saleDate);
            row2.CreateCell(4).SetCellValue(MyVar.zzcode);
            row2.CreateCell(5).SetCellValue(MyVar.zzname);
            row2.CreateCell(6).SetCellValue("01");
            row2.CreateCell(7).SetCellValue("罗森便利店");
            row2.CreateCell(8).SetCellValue(MyVar.salerCode);
            row2.CreateCell(9).SetCellValue(MyVar.salerName);
            row2.CreateCell(10).SetCellValue("");
            row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? "NULL" : dt.Rows[0][0].ToString());
            row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][1].ToString()) ? "NULL" : dt.Rows[0][1].ToString());
            row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][2].ToString()) ? "NULL" : dt.Rows[0][2].ToString());
            row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][3].ToString()) ? "NULL" : dt.Rows[0][3].ToString());
            row2.CreateCell(15).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][4].ToString()) ? "NULL" : dt.Rows[0][4].ToString());
            row2.CreateCell(16).SetCellValue(dt.Rows[0][5].ToString());
            row2.CreateCell(17).SetCellValue("13");
            row2.CreateCell(18).SetCellValue(MyVar.saleDate);
            row2.CreateCell(19).SetCellValue(MyVar.zzcode);
            row2.CreateCell(20).SetCellValue(MyVar.zzname);
            row2.CreateCell(21).SetCellValue("");
            row2.CreateCell(22).SetCellValue("");
            row2.CreateCell(23).SetCellValue("");
            row2.CreateCell(24).SetCellValue("");

            //数据二行至结束  
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                IRow row3 = sheet.CreateRow(i + 2);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row3.CreateCell(j + 11).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][j].ToString()) ? "NULL" : dt.Rows[i][j].ToString());
                }
                row3.CreateCell(17).SetCellValue("13");
                row3.CreateCell(18).SetCellValue(MyVar.saleDate);
                row3.CreateCell(19).SetCellValue(MyVar.zzcode);
                row3.CreateCell(20).SetCellValue(MyVar.zzname);
                row3.CreateCell(21).SetCellValue("");
                row3.CreateCell(22).SetCellValue("");
                row3.CreateCell(23).SetCellValue("");
                row3.CreateCell(24).SetCellValue("");
            }

            //设置宽度
            for (int i = 0; i < 23; i++)
            {
                int length = Encoding.Default.GetBytes(row.GetCell(i).ToString()).Length;
                sheet.SetColumnWidth(i, (length + 2) * 256);
            }
            //return workbook;

            //添加单元格背景颜色
            row1.GetCell(3).CellStyle = s;
            row1.GetCell(18).CellStyle = s;



            //生成excel：
            FileStream file = new FileStream(path + fileExt, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }

        /// <summary>
        /// 广东逅厨订单转换
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        /// <param name="fileExt"></param>
        public static void TableToExcelGDHC(string path, DataTable dt, string fileExt)
        {
            IWorkbook workbook = null;
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                //return null;
                return;
            }
            ISheet sheet = workbook.CreateSheet("销售订单#基本信息(FBillHead)");

            //定义单元格背景颜色类型
            ICellStyle s = workbook.CreateCellStyle();
            s.FillForegroundColor = HSSFColor.Red.Index;
            s.FillPattern = FillPattern.SolidForeground;

            //表头第一行 
            IRow row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("FBillHead(SAL_SaleOrder)");
            row.CreateCell(1).SetCellValue("FBillTypeID");
            row.CreateCell(2).SetCellValue("FBillTypeID#Name");
            row.CreateCell(3).SetCellValue("FDate");
            row.CreateCell(4).SetCellValue("FSaleOrgId");
            row.CreateCell(5).SetCellValue("FSaleOrgId#Name");
            row.CreateCell(6).SetCellValue("FCustId");
            row.CreateCell(7).SetCellValue("FCustId#Name");
            row.CreateCell(8).SetCellValue("FHEADLOCID");
            row.CreateCell(9).SetCellValue("FHEADLOCID#Name");
            row.CreateCell(10).SetCellValue("FSaleOrderEntry");
            row.CreateCell(11).SetCellValue("FMaterialId");
            row.CreateCell(12).SetCellValue("FMaterialId#Name");
            row.CreateCell(13).SetCellValue("FUnitID");
            row.CreateCell(14).SetCellValue("FUnitID#Name");
            row.CreateCell(15).SetCellValue("FQty");
            row.CreateCell(16).SetCellValue("FTaxPrice");
            row.CreateCell(17).SetCellValue("FEntryTaxRate");
            row.CreateCell(18).SetCellValue("FDeliveryDate");
            row.CreateCell(19).SetCellValue("FSettleOrgIds");
            row.CreateCell(20).SetCellValue("FSettleOrgIds#Name");
            row.CreateCell(21).SetCellValue("FReserveType");
            row.CreateCell(22).SetCellValue("FOUTLMTUNIT");
            row.CreateCell(23).SetCellValue("*Split*3");
            row.CreateCell(24).SetCellValue("FTaxDetailSubEntity");

            //表头第二行 
            IRow row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("*基本信息(序号)");
            row1.CreateCell(1).SetCellValue("*(基本信息)单据类型#编码");
            row1.CreateCell(2).SetCellValue("(基本信息)单据类型#名称");
            row1.CreateCell(3).SetCellValue("*(基本信息)日期");
            row1.CreateCell(4).SetCellValue("*(基本信息)销售组织#编码");
            row1.CreateCell(5).SetCellValue("(基本信息)销售组织#名称#Name");
            row1.CreateCell(6).SetCellValue("*(基本信息)客户#编码");
            row1.CreateCell(7).SetCellValue("(基本信息)客户#名称#Name");
            row1.CreateCell(8).SetCellValue("(基本信息)交货地点#编码");
            row1.CreateCell(9).SetCellValue("(基本信息)交货地点#名称");
            row1.CreateCell(10).SetCellValue("*订单明细(序号)");
            row1.CreateCell(11).SetCellValue("*(订单明细)物料编码#编码");
            row1.CreateCell(12).SetCellValue("(订单明细)物料编码#名称");
            row1.CreateCell(13).SetCellValue("*(订单明细)销售单位#编码");
            row1.CreateCell(14).SetCellValue("(订单明细)销售单位#名称");
            row1.CreateCell(15).SetCellValue("(订单明细)销售数量");
            row1.CreateCell(16).SetCellValue("(订单明细)含税单价");
            row1.CreateCell(17).SetCellValue("(订单明细)税率%");
            row1.CreateCell(18).SetCellValue("*(订单明细)要货日期");
            row1.CreateCell(19).SetCellValue("*(订单明细)结算组织#编码");
            row1.CreateCell(20).SetCellValue("(订单明细)结算组织#名称");
            row1.CreateCell(21).SetCellValue("*(订单明细)预留类型");
            row1.CreateCell(22).SetCellValue("*(订单明细)超发控制单位类型");
            row1.CreateCell(23).SetCellValue("间隔列");
            row1.CreateCell(24).SetCellValue("*税务明细(序号)");


            //数据第一行
            IRow row2 = sheet.CreateRow(2);
            row2.CreateCell(0).SetCellValue("100000");
            row2.CreateCell(1).SetCellValue("XSDD01_SYS");
            row2.CreateCell(2).SetCellValue("标准销售订单");
            row2.CreateCell(3).SetCellValue(MyVar.saleDate);
            row2.CreateCell(4).SetCellValue(MyVar.zzcode);
            row2.CreateCell(5).SetCellValue(MyVar.zzname);
            row2.CreateCell(6).SetCellValue("CUST0274");
            row2.CreateCell(7).SetCellValue("罗森(广东)便利有限公司");
            row2.CreateCell(8).SetCellValue(MyVar.jhdId);
            row2.CreateCell(9).SetCellValue(MyVar.jhd);
            row2.CreateCell(10).SetCellValue("");
            row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? "NULL" : dt.Rows[0][0].ToString());
            row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][1].ToString()) ? "NULL" : dt.Rows[0][1].ToString());
            row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][2].ToString()) ? "NULL" : dt.Rows[0][2].ToString());
            row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][3].ToString()) ? "NULL" : dt.Rows[0][3].ToString());
            row2.CreateCell(15).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][4].ToString()) ? "NULL" : dt.Rows[0][4].ToString());
            row2.CreateCell(16).SetCellValue(dt.Rows[0][5].ToString());
            row2.CreateCell(17).SetCellValue("13");
            row2.CreateCell(18).SetCellValue(MyVar.saleDate);
            row2.CreateCell(19).SetCellValue(MyVar.zzcode);
            row2.CreateCell(20).SetCellValue(MyVar.zzname);
            row2.CreateCell(21).SetCellValue("");
            row2.CreateCell(22).SetCellValue("");
            row2.CreateCell(23).SetCellValue("");
            row2.CreateCell(24).SetCellValue("");

            //数据二行至结束  
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                IRow row3 = sheet.CreateRow(i + 2);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row3.CreateCell(j + 11).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][j].ToString()) ? "NULL" : dt.Rows[i][j].ToString());
                }
                row3.CreateCell(17).SetCellValue("13");
                row3.CreateCell(18).SetCellValue(MyVar.saleDate);
                row3.CreateCell(19).SetCellValue(MyVar.zzcode);
                row3.CreateCell(20).SetCellValue(MyVar.zzname);
                row3.CreateCell(21).SetCellValue("");
                row3.CreateCell(22).SetCellValue("");
                row3.CreateCell(23).SetCellValue("");
                row3.CreateCell(24).SetCellValue("");
            }

            //设置宽度
            for (int i = 0; i < 23; i++)
            {
                int length = Encoding.Default.GetBytes(row.GetCell(i).ToString()).Length;
                sheet.SetColumnWidth(i, (length + 2) * 256);
            }
            //return workbook;

            //添加单元格背景颜色
            row1.GetCell(3).CellStyle = s;
            row1.GetCell(18).CellStyle = s;



            //生成excel：
            FileStream file = new FileStream(path + fileExt, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }

        /// <summary>
        /// 广东逅厨沃尔玛订单转换
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        /// <param name="fileExt"></param>
        public static void TableToExcelGDHCWLM(string path, DataTable dt, string fileExt)
        {
            IWorkbook workbook = null;
            int id = 99999;  //序号
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                //return null;
                return;
            }
            ISheet sheet = workbook.CreateSheet("销售订单#基本信息(FBillHead)");

            //定义单元格背景颜色类型
            ICellStyle s = workbook.CreateCellStyle();
            s.FillForegroundColor = HSSFColor.Red.Index;
            s.FillPattern = FillPattern.SolidForeground;

            //表头第一行 
            IRow row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("FBillHead(SAL_SaleOrder)");
            row.CreateCell(1).SetCellValue("FBillTypeID");
            row.CreateCell(2).SetCellValue("FBillTypeID#Name");
            row.CreateCell(3).SetCellValue("FDate");
            row.CreateCell(4).SetCellValue("FSaleOrgId");
            row.CreateCell(5).SetCellValue("FSaleOrgId#Name");
            row.CreateCell(6).SetCellValue("FCustId");
            row.CreateCell(7).SetCellValue("FCustId#Name");
            row.CreateCell(8).SetCellValue("FNote");
            row.CreateCell(9).SetCellValue("FHEADLOCID");
            row.CreateCell(10).SetCellValue("FHEADLOCID#Name");
            row.CreateCell(11).SetCellValue("FSaleOrderEntry");
            row.CreateCell(12).SetCellValue("FMaterialId");
            row.CreateCell(13).SetCellValue("FMaterialId#Name");
            row.CreateCell(14).SetCellValue("FUnitID");
            row.CreateCell(15).SetCellValue("FUnitID#Name");
            row.CreateCell(16).SetCellValue("FQty");
            row.CreateCell(17).SetCellValue("FPrice");
            row.CreateCell(18).SetCellValue("FEntryTaxRate");
            row.CreateCell(19).SetCellValue("FDeliveryDate");
            row.CreateCell(20).SetCellValue("FSettleOrgIds");
            row.CreateCell(21).SetCellValue("FSettleOrgIds#Name");
            row.CreateCell(22).SetCellValue("FReserveType");
            row.CreateCell(23).SetCellValue("FOUTLMTUNIT");
            row.CreateCell(24).SetCellValue("*Split*3");
            row.CreateCell(25).SetCellValue("FTaxDetailSubEntity");

            //表头第二行 
            IRow row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("*基本信息(序号)");
            row1.CreateCell(1).SetCellValue("*(基本信息)单据类型#编码");
            row1.CreateCell(2).SetCellValue("(基本信息)单据类型#名称");
            row1.CreateCell(3).SetCellValue("*(基本信息)日期");
            row1.CreateCell(4).SetCellValue("*(基本信息)销售组织#编码");
            row1.CreateCell(5).SetCellValue("(基本信息)销售组织#名称#Name");
            row1.CreateCell(6).SetCellValue("*(基本信息)客户#编码");
            row1.CreateCell(7).SetCellValue("(基本信息)客户#名称#Name");
            row1.CreateCell(8).SetCellValue("(基本信息)备注");
            row1.CreateCell(9).SetCellValue("(基本信息)交货地点#编码");
            row1.CreateCell(10).SetCellValue("(基本信息)交货地点#名称");
            row1.CreateCell(11).SetCellValue("*订单明细(序号)");
            row1.CreateCell(12).SetCellValue("*(订单明细)物料编码#编码");
            row1.CreateCell(13).SetCellValue("(订单明细)物料编码#名称");
            row1.CreateCell(14).SetCellValue("*(订单明细)销售单位#编码");
            row1.CreateCell(15).SetCellValue("(订单明细)销售单位#名称");
            row1.CreateCell(16).SetCellValue("(订单明细)销售数量");
            row1.CreateCell(17).SetCellValue("(订单明细)单价");
            row1.CreateCell(18).SetCellValue("(订单明细)税率%");
            row1.CreateCell(19).SetCellValue("*(订单明细)要货日期");
            row1.CreateCell(20).SetCellValue("*(订单明细)结算组织#编码");
            row1.CreateCell(21).SetCellValue("(订单明细)结算组织#名称");
            row1.CreateCell(22).SetCellValue("*(订单明细)预留类型");
            row1.CreateCell(23).SetCellValue("*(订单明细)超发控制单位类型");
            row1.CreateCell(24).SetCellValue("间隔列");
            row1.CreateCell(25).SetCellValue("*税务明细(序号)");

            //数据二行至结束  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row2 = sheet.CreateRow(i+2);
                if (!dt.Rows[i][0].ToString().Equals(id.ToString()))
                {
                    row2.CreateCell(0).SetCellValue(dt.Rows[i][0].ToString());
                    row2.CreateCell(1).SetCellValue("XSDD01_SYS");
                    row2.CreateCell(2).SetCellValue("标准销售订单");
                    row2.CreateCell(3).SetCellValue(MyVar.saleDate);
                    row2.CreateCell(4).SetCellValue("107");
                    row2.CreateCell(5).SetCellValue("广东逅厨食品有限公司");
                    row2.CreateCell(6).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? "NULL" : dt.Rows[i][2].ToString());
                    row2.CreateCell(7).SetCellValue("");
                    row2.CreateCell(8).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][2].ToString()) ? "NULL" : dt.Rows[i][1].ToString());
                    row2.CreateCell(9).SetCellValue("");
                    row2.CreateCell(10).SetCellValue("");
                    row2.CreateCell(11).SetCellValue("");
                    row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "NULL" : dt.Rows[i][3].ToString());
                    row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "NULL" : dt.Rows[i][4].ToString());
                    row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                    row2.CreateCell(15).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                    row2.CreateCell(16).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][7].ToString()) ? "NULL" : dt.Rows[i][7].ToString());
                    row2.CreateCell(17).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][8].ToString()) ? "NULL" : dt.Rows[i][8].ToString());
                    row2.CreateCell(18).SetCellValue("13");
                    row2.CreateCell(19).SetCellValue(MyVar.saleDate);
                    row2.CreateCell(20).SetCellValue("107");
                    row2.CreateCell(21).SetCellValue("广东逅厨食品有限公司");
                    row2.CreateCell(22).SetCellValue("");
                    row2.CreateCell(23).SetCellValue("");
                    row2.CreateCell(24).SetCellValue("");
                    row2.CreateCell(25).SetCellValue("");
                    id++;
                }
                else
                {
                    row2.CreateCell(0).SetCellValue("");
                    row2.CreateCell(1).SetCellValue("");
                    row2.CreateCell(2).SetCellValue("");
                    row2.CreateCell(3).SetCellValue("");
                    row2.CreateCell(4).SetCellValue("");
                    row2.CreateCell(5).SetCellValue("");
                    row2.CreateCell(6).SetCellValue("");
                    row2.CreateCell(7).SetCellValue("");
                    row2.CreateCell(8).SetCellValue("");
                    row2.CreateCell(9).SetCellValue("");
                    row2.CreateCell(10).SetCellValue("");
                    row2.CreateCell(11).SetCellValue("");
                    row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "NULL" : dt.Rows[i][3].ToString());
                    row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "NULL" : dt.Rows[i][4].ToString());
                    row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                    row2.CreateCell(15).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                    row2.CreateCell(16).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][7].ToString()) ? "NULL" : dt.Rows[i][7].ToString());
                    row2.CreateCell(17).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][8].ToString()) ? "NULL" : dt.Rows[i][8].ToString());
                    row2.CreateCell(18).SetCellValue("13");
                    row2.CreateCell(19).SetCellValue(MyVar.saleDate);
                    row2.CreateCell(20).SetCellValue("107");
                    row2.CreateCell(21).SetCellValue("广东逅厨食品有限公司");
                    row2.CreateCell(22).SetCellValue("");
                    row2.CreateCell(23).SetCellValue("");
                    row2.CreateCell(24).SetCellValue("");
                    row2.CreateCell(25).SetCellValue("");

                }
            }

            //设置宽度
            for (int i = 0; i < 24; i++)
            {
                int length = Encoding.Default.GetBytes(row.GetCell(i).ToString()).Length;
                sheet.SetColumnWidth(i, (length + 2) * 256);
            }
            //return workbook;

            //添加单元格背景颜色
            row1.GetCell(3).CellStyle = s;
            row1.GetCell(18).CellStyle = s;



            //生成excel：
            FileStream file = new FileStream(path + fileExt, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }

        /// <summary>
        /// 广东逅厨沃尔玛送货单转换-不分单打印-基于excel字体的条形码
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        /// <param name="fileExt"></param>
        public static void TableToExcelGDHCWEMSHD(string path, DataTable dt, string fileExt)
        {
            IWorkbook workbook = null;
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                return;
            }

            //条形码字体
            IFont barcodefont = workbook.CreateFont();
            barcodefont.FontHeightInPoints = 18; // 字体大小
            barcodefont.FontName = "3 of 9 Barcode"; // 字体名称
                                                     //正常字体
            IFont normalfont = workbook.CreateFont();
            normalfont.FontHeightInPoints = 11; // 字体大小
            normalfont.FontName = "微软雅黑"; // 字体名称
                                          //加粗字体
            IFont boldfont = workbook.CreateFont();
            boldfont.FontHeightInPoints = 11; // 字体大小
            boldfont.FontName = "微软雅黑"; // 字体名称
            boldfont.IsBold = true;
            //店号大字体
            IFont bigfont = workbook.CreateFont();
            bigfont.FontHeightInPoints = 25; // 字体大小
            bigfont.FontName = "微软雅黑"; // 字体名称
            bigfont.IsBold = true;
            //条形码样式
            ICellStyle barcodeStyle = workbook.CreateCellStyle();
            barcodeStyle.SetFont(barcodefont);
            barcodeStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            barcodeStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            barcodeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式不加粗
            ICellStyle normalNoBackStyle = workbook.CreateCellStyle();
            normalNoBackStyle.SetFont(normalfont);
            normalNoBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式加粗
            ICellStyle normalNoBackBoldStyle = workbook.CreateCellStyle();
            normalNoBackBoldStyle.SetFont(boldfont);
            normalNoBackBoldStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackBoldStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackBoldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格有背景样式加粗
            ICellStyle normalBackStyle = workbook.CreateCellStyle();
            normalBackStyle.SetFont(boldfont);
            normalBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalBackStyle.FillForegroundColor = HSSFColor.Blue.SkyBlue.Index;
            normalBackStyle.FillPattern = FillPattern.SolidForeground;
            normalBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //第一行店号大字体
            ICellStyle bigfontstyle = workbook.CreateCellStyle();
            bigfontstyle.SetFont(bigfont);
            bigfontstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            bigfontstyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            //循环门店数
            for (int i = 0; i < MyVar.Shops.Count; i++)
            {
                int shopqty = 0;//门店订单总数
                ISheet sheet = workbook.CreateSheet(MyVar.Shops[i]);
                //从dt中根据店号再过滤出该店号的订单数据
                DataRow[] shoprows = dt.Select("shop = '" + MyVar.Shops[i] + "'");

                //第一行
                IRow row0 = sheet.CreateRow(0);
                // 合并单元格
                CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3); // 
                sheet.AddMergedRegion(region);
                // 在合并后的单元格中设置值
                ICell cell = sheet.GetRow(0).GetCell(0);
                if (cell == null)
                {
                    cell = sheet.GetRow(0).CreateCell(0);
                }
                cell.SetCellValue(MyVar.Shops[i].Replace("店", ""));
                row0.GetCell(0).CellStyle = bigfontstyle;
                row0.CreateCell(6).SetCellValue("即食");
                row0.GetCell(6).CellStyle = bigfontstyle;

                ///第二行
                IRow row1 = sheet.CreateRow(1);
                row1.CreateCell(0).SetCellValue("部门");
                row1.CreateCell(1).SetCellValue("公司名称");
                row1.CreateCell(2).SetCellValue("订单号");
                row1.CreateCell(3).SetCellValue("店号");
                row1.CreateCell(4).SetCellValue("温区");
                // 合并单元格
                region = new CellRangeAddress(1, 1, 5, 6); // 
                sheet.AddMergedRegion(region);
                // 在合并后的单元格中设置值
                cell = sheet.GetRow(1).GetCell(5);

                if (cell == null)
                {
                    cell = sheet.GetRow(1).CreateCell(5);

                }
                cell.SetCellValue("收货日期");
                row1.CreateCell(6).SetCellValue("");



                ///设置有背景颜色的样式
                for (int j = 0; j <= 6; j++)
                {
                    row1.GetCell(j).CellStyle = normalBackStyle;
                }
                ///第三行
                IRow row2 = sheet.CreateRow(2);
                row2.CreateCell(0).SetCellValue(MyVar.Dep);
                row2.CreateCell(1).SetCellValue("江苏逅厨");
                row2.CreateCell(2).SetCellValue(MyVar.OrderNo);
                row2.CreateCell(3).SetCellValue(MyVar.Shops[i].Replace("店", ""));
                row2.CreateCell(4).SetCellValue("冷藏");
                // 合并单元格
                region = new CellRangeAddress(2, 2, 5, 6); // 
                sheet.AddMergedRegion(region);
                // 在合并后的单元格中设置值
                cell = sheet.GetRow(2).GetCell(5);
                if (cell == null)
                {
                    cell = sheet.GetRow(2).CreateCell(5);

                }
                cell.SetCellValue(MyVar.saleDate);
                row2.CreateCell(6).SetCellValue("");



                ///设置第三行的样式
                row2.GetCell(0).CellStyle = normalNoBackBoldStyle;
                row2.GetCell(1).CellStyle = normalNoBackBoldStyle;
                row2.GetCell(2).CellStyle = normalNoBackStyle;
                row2.GetCell(3).CellStyle = normalNoBackBoldStyle;
                row2.GetCell(4).CellStyle = normalNoBackBoldStyle;
                row2.GetCell(5).CellStyle = normalNoBackStyle;
                row2.GetCell(6).CellStyle = normalNoBackStyle;


                //第五行
                IRow row4 = sheet.CreateRow(4);
                row4.CreateCell(0).SetCellValue("序号");
                row4.CreateCell(1).SetCellValue("商品号");
                row4.CreateCell(2).SetCellValue("商品名称");
                row4.CreateCell(3).SetCellValue("商品条码");
                row4.CreateCell(4).SetCellValue("规格");
                row4.CreateCell(5).SetCellValue("数量");
                row4.CreateCell(6).SetCellValue("商品号/条码");
                ///设置有背景颜色的样式
                for (int j = 0; j <= 6; j++)
                {
                    row4.GetCell(j).CellStyle = normalBackStyle;
                }
                //第六行开始
                for (int m = 0; m < shoprows.Length; m++)
                {
                    IRow row5 = sheet.CreateRow(m + 5);
                    row5.CreateCell(0).SetCellValue(m + 1);
                    row5.CreateCell(1).SetCellValue(shoprows[m][1].ToString());
                    row5.CreateCell(2).SetCellValue(shoprows[m][2].ToString());
                    row5.CreateCell(3).SetCellValue(shoprows[m][3].ToString());
                    row5.CreateCell(4).SetCellValue(shoprows[m][4].ToString() + "g");
                    row5.CreateCell(5).SetCellValue(shoprows[m][5].ToString());
                    row5.CreateCell(6).SetCellValue("*" + shoprows[m][3].ToString() + "*");
                    row5.Height = 700;
                    shopqty = shopqty + int.Parse(shoprows[m][5].ToString());
                    for (int j = 0; j <= 5; j++)
                    {
                        row5.GetCell(j).CellStyle = normalNoBackStyle;
                    }
                    row5.GetCell(6).CellStyle = barcodeStyle; //条形码
                }


                //总数行
                IRow row6 = sheet.CreateRow(shoprows.Length + 5);
                row6.CreateCell(4).SetCellValue("箱总数");
                row6.CreateCell(5).SetCellValue(shopqty);
                row6.GetCell(4).CellStyle = normalNoBackStyle;
                row6.GetCell(5).CellStyle = normalNoBackStyle;



                //设置宽度
                sheet.SetColumnWidth(0, 1100);
                sheet.SetColumnWidth(1, 2400);
                sheet.SetColumnWidth(2, 4800);
                sheet.SetColumnWidth(3, 4000);
                sheet.SetColumnWidth(4, 1600);
                sheet.SetColumnWidth(5, 1000);
                sheet.SetColumnWidth(6, 8000);




                FileStream file = new FileStream(path + fileExt, FileMode.Create);
                workbook.Write(file);
                file.Close();
            }
        }

        /// <summary>
        /// 广东逅厨沃尔玛送货单转换-不分单打印-基于图形的条形码
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        /// <param name="fileExt"></param>
        public static void TableToExcelGDHCWEMSHD_barcode(string path, DataTable dt, string fileExt)
        {
            IWorkbook workbook = null;
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                return;
            }

            //条形码字体
            IFont barcodefont = workbook.CreateFont();
            barcodefont.FontHeightInPoints = 18; // 字体大小
            barcodefont.FontName = "3 of 9 Barcode"; // 字体名称
                                                     //正常字体
            IFont normalfont = workbook.CreateFont();
            normalfont.FontHeightInPoints = 11; // 字体大小
            normalfont.FontName = "微软雅黑"; // 字体名称
                                          //加粗字体
            IFont boldfont = workbook.CreateFont();
            boldfont.FontHeightInPoints = 11; // 字体大小
            boldfont.FontName = "微软雅黑"; // 字体名称
            boldfont.IsBold = true;
            //店号大字体
            IFont bigfont = workbook.CreateFont();
            bigfont.FontHeightInPoints = 25; // 字体大小
            bigfont.FontName = "微软雅黑"; // 字体名称
            bigfont.IsBold = true;
            //条形码样式
            ICellStyle barcodeStyle = workbook.CreateCellStyle();
            barcodeStyle.SetFont(barcodefont);
            barcodeStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            barcodeStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            barcodeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式不加粗
            ICellStyle normalNoBackStyle = workbook.CreateCellStyle();
            normalNoBackStyle.SetFont(normalfont);
            normalNoBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式加粗
            ICellStyle normalNoBackBoldStyle = workbook.CreateCellStyle();
            normalNoBackBoldStyle.SetFont(boldfont);
            normalNoBackBoldStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackBoldStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackBoldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格有背景样式加粗
            ICellStyle normalBackStyle = workbook.CreateCellStyle();
            normalBackStyle.SetFont(boldfont);
            normalBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalBackStyle.FillForegroundColor = HSSFColor.Blue.SkyBlue.Index;
            normalBackStyle.FillPattern = FillPattern.SolidForeground;
            normalBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //第一行店号大字体
            ICellStyle bigfontstyle = workbook.CreateCellStyle();
            bigfontstyle.SetFont(bigfont);
            bigfontstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            bigfontstyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            //循环门店数
            for (int i = 0; i < MyVar.Shops.Count; i++)
            {
                int shopqty = 0;//门店订单总数
                ISheet sheet = workbook.CreateSheet(MyVar.Shops[i]);
                //从dt中根据店号再过滤出该店号的订单数据
                DataRow[] shoprows = dt.Select("shop = '" + MyVar.Shops[i] + "'");

                //第一行
                IRow row0 = sheet.CreateRow(0);
                // 合并单元格
                CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3); // 
                sheet.AddMergedRegion(region);
                // 在合并后的单元格中设置值
                ICell cell = sheet.GetRow(0).GetCell(0);
                if (cell == null)
                {
                    cell = sheet.GetRow(0).CreateCell(0);
                }
                cell.SetCellValue(MyVar.Shops[i].Replace("店", ""));
                row0.GetCell(0).CellStyle = bigfontstyle;
                row0.CreateCell(5).SetCellValue("即食");
                row0.GetCell(5).CellStyle = bigfontstyle;

                ///第二行
                IRow row1 = sheet.CreateRow(1);
                row1.CreateCell(0).SetCellValue("部门");
                row1.CreateCell(1).SetCellValue("公司名称");
                row1.CreateCell(2).SetCellValue("订单号");
                row1.CreateCell(3).SetCellValue("店号");
                row1.CreateCell(4).SetCellValue("温区");
                row1.CreateCell(5).SetCellValue("收货日期");



                ///设置有背景颜色的样式
                for (int j = 0; j <= 5; j++)
                {
                    row1.GetCell(j).CellStyle = normalBackStyle;
                }
                ///第三行
                IRow row2 = sheet.CreateRow(2);
                row2.CreateCell(0).SetCellValue(MyVar.Dep);
                row2.CreateCell(1).SetCellValue("江苏逅厨");
                row2.CreateCell(2).SetCellValue(MyVar.OrderNo);
                row2.CreateCell(3).SetCellValue(MyVar.Shops[i].Replace("店", ""));
                row2.CreateCell(4).SetCellValue("冷藏");
                row2.CreateCell(5).SetCellValue(MyVar.saleDate);



                ///设置第三行的样式
                row2.GetCell(0).CellStyle = normalNoBackBoldStyle;
                row2.GetCell(1).CellStyle = normalNoBackBoldStyle;
                row2.GetCell(2).CellStyle = normalNoBackStyle;
                row2.GetCell(3).CellStyle = normalNoBackBoldStyle;
                row2.GetCell(4).CellStyle = normalNoBackBoldStyle;
                row2.GetCell(5).CellStyle = normalNoBackStyle;
                


                //第五行
                IRow row4 = sheet.CreateRow(4);
                row4.CreateCell(0).SetCellValue("序号");
                row4.CreateCell(1).SetCellValue("商品号");
                row4.CreateCell(2).SetCellValue("商品名称");
                row4.CreateCell(3).SetCellValue("规格");
                row4.CreateCell(4).SetCellValue("数量");
                row4.CreateCell(5).SetCellValue("商品条码");
                ///设置有背景颜色的样式
                for (int j = 0; j <= 5; j++)
                {
                    row4.GetCell(j).CellStyle = normalBackStyle;
                }
                //第六行开始
                for (int m = 0; m < shoprows.Length; m++)
                {
                    IRow row5 = sheet.CreateRow(m + 5);
                    row5.CreateCell(0).SetCellValue(m + 1);
                    row5.CreateCell(1).SetCellValue(shoprows[m][1].ToString());
                    row5.CreateCell(2).SetCellValue(shoprows[m][2].ToString());
                    row5.CreateCell(3).SetCellValue(shoprows[m][4].ToString() + "g");
                    row5.CreateCell(4).SetCellValue(shoprows[m][5].ToString());
                    row5.CreateCell(5).SetCellValue("");
                    BarcodeFormat format = BarcodeFormat.EAN_13;
                    var barcodeImage = BarcodeHelper.GenerateBarcode(shoprows[m][3].ToString(), format);
                    MemoryStream ms = new MemoryStream();
                    barcodeImage.Save(ms, ImageFormat.Png);
                    int pictureIdx = workbook.AddPicture(ms.ToArray(), PictureType.PNG);
                    IDrawing drawing = sheet.CreateDrawingPatriarch();
                    IClientAnchor anchor = drawing.CreateAnchor(2 * 9525, 1 * 9525, 1000 * 9525, 110 * 9525, 5, m + 5, 5, m + 5);
                    //dx1：起始单元格的x偏移量；
                    //dy1：起始单元格的y偏移量；
                    //dx2：终止单元格的x偏移量；
                    //dy2：终止单元格的y偏移量；
                    //col1：起始单元格列序号；
                    //row1：起始单元格行序号；
                    //col2：终止单元格列序号；
                    //row2：终止单元格行序号；
                    IPicture pict = drawing.CreatePicture(anchor, pictureIdx);
                    //pict.Resize(1); // 自动调整图片大小以适应单元格大小
                    row5.Height = 850;
                    shopqty = shopqty + int.Parse(shoprows[m][5].ToString());
                    for (int j = 0; j <= 5; j++)
                    {
                        row5.GetCell(j).CellStyle = normalNoBackStyle;
                    }
                    
                }


                //总数行
                IRow row6 = sheet.CreateRow(shoprows.Length + 5);
                row6.CreateCell(3).SetCellValue("箱总数");
                row6.CreateCell(4).SetCellValue(shopqty);
                row6.GetCell(3).CellStyle = normalNoBackStyle;
                row6.GetCell(4).CellStyle = normalNoBackStyle;



                //设置宽度
                sheet.SetColumnWidth(0, 1100);
                sheet.SetColumnWidth(1, 2400);
                sheet.SetColumnWidth(2, 4800);
                sheet.SetColumnWidth(3, 2000);
                sheet.SetColumnWidth(4, 1600);
                sheet.SetColumnWidth(5, 8000);




                FileStream file = new FileStream(path + fileExt, FileMode.Create);
                workbook.Write(file);
                file.Close();
            }
        }
        /// <summary>
        /// 广东逅厨沃尔玛送货单转换-分单打印-基于excel字体的条形码
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        /// <param name="fileExt"></param>
        public static void TableToExcelGDHCWEMSHD_FD(string path, DataTable dt, string fileExt)
        {
            List<int> shopfdnum = new List<int> { }; //门店的分单集合，分店加分单数0,1.2.3.......N
            int shopfdqty;//门店分单的商品总数
            IWorkbook workbook = null;
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                return;
            }

            //条形码字体
            IFont barcodefont = workbook.CreateFont();
            barcodefont.FontHeightInPoints = 18; // 字体大小
            barcodefont.FontName = "3 of 9 Barcode"; // 字体名称
                                                     //正常字体
            IFont normalfont = workbook.CreateFont();
            normalfont.FontHeightInPoints = 11; // 字体大小
            normalfont.FontName = "微软雅黑"; // 字体名称
                                          //加粗字体
            IFont boldfont = workbook.CreateFont();
            boldfont.FontHeightInPoints = 11; // 字体大小
            boldfont.FontName = "微软雅黑"; // 字体名称
            boldfont.IsBold = true;
            //店号大字体
            IFont bigfont = workbook.CreateFont();
            bigfont.FontHeightInPoints = 25; // 字体大小
            bigfont.FontName = "微软雅黑"; // 字体名称
            bigfont.IsBold = true;
            //条形码样式
            ICellStyle barcodeStyle = workbook.CreateCellStyle();
            barcodeStyle.SetFont(barcodefont);
            barcodeStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            barcodeStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            barcodeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式不加粗
            ICellStyle normalNoBackStyle = workbook.CreateCellStyle();
            normalNoBackStyle.SetFont(normalfont);
            normalNoBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式加粗
            ICellStyle normalNoBackBoldStyle = workbook.CreateCellStyle();
            normalNoBackBoldStyle.SetFont(boldfont);
            normalNoBackBoldStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackBoldStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackBoldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格有背景样式加粗
            ICellStyle normalBackStyle = workbook.CreateCellStyle();
            normalBackStyle.SetFont(boldfont);
            normalBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalBackStyle.FillForegroundColor = HSSFColor.Blue.SkyBlue.Index;
            normalBackStyle.FillPattern = FillPattern.SolidForeground;
            normalBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //第一行店号大字体
            ICellStyle bigfontstyle = workbook.CreateCellStyle();
            bigfontstyle.SetFont(bigfont);
            bigfontstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            bigfontstyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            //1、循环门店数
            for (int i = 0; i < MyVar.Shops.Count; i++)
            {
                shopfdnum.Clear();
                int fdpc = -1;  //分单批次

                //从dt中根据店号再过滤出该店号的订单数据
                DataRow[] shoprows = dt.Select("shop = '" + MyVar.Shops[i] + "'");
                foreach (DataRow row in shoprows)
                {
                    if (int.Parse(row[6].ToString()) != fdpc)
                    {
                        fdpc = int.Parse(row[6].ToString());
                        shopfdnum.Add(fdpc);
                    }
                }
                //Console.WriteLine("门店分单数"+shopfdnum);

                //2、循环分单数生成
                for (int k = 0; k < shopfdnum.Count; k++)
                {
                    //以店名+ 分单号创建sheet
                    ISheet sheet = workbook.CreateSheet(MyVar.Shops[i] + "(" + shopfdnum[k] + ")");
                    shoprows = dt.Select("shop = '" + MyVar.Shops[i] + "' and pc='" + shopfdnum[k] + "'");
                    shopfdqty = 0;//分单的商品总数重置为0
                    //第一行
                    IRow row0 = sheet.CreateRow(0);
                    // 合并单元格
                    CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3); // 
                    sheet.AddMergedRegion(region);
                    // 在合并后的单元格中设置值
                    ICell cell = sheet.GetRow(0).GetCell(0);
                    if (cell == null)
                    {
                        cell = sheet.GetRow(0).CreateCell(0);
                    }
                    cell.SetCellValue(MyVar.Shops[i].Replace("店", ""));
                    row0.GetCell(0).CellStyle = bigfontstyle;
                    row0.CreateCell(6).SetCellValue("即食");
                    row0.GetCell(6).CellStyle = bigfontstyle;

                    ///第二行
                    IRow row1 = sheet.CreateRow(1);
                    row1.CreateCell(0).SetCellValue("部门");
                    row1.CreateCell(1).SetCellValue("公司名称");
                    row1.CreateCell(2).SetCellValue("订单号");
                    row1.CreateCell(3).SetCellValue("店号");
                    row1.CreateCell(4).SetCellValue("温区");
                    // 合并单元格
                    region = new CellRangeAddress(1, 1, 5, 6); // 
                    sheet.AddMergedRegion(region);
                    // 在合并后的单元格中设置值
                    cell = sheet.GetRow(1).GetCell(5);

                    if (cell == null)
                    {
                        cell = sheet.GetRow(1).CreateCell(5);

                    }
                    cell.SetCellValue("收货日期");
                    row1.CreateCell(6).SetCellValue("");



                    ///设置有背景颜色的样式
                    for (int j = 0; j <= 6; j++)
                    {
                        row1.GetCell(j).CellStyle = normalBackStyle;
                    }
                    ///第三行
                    IRow row2 = sheet.CreateRow(2);
                    row2.CreateCell(0).SetCellValue(MyVar.Dep);
                    row2.CreateCell(1).SetCellValue("江苏逅厨");
                    row2.CreateCell(2).SetCellValue(MyVar.OrderNo);
                    row2.CreateCell(3).SetCellValue(MyVar.Shops[i].Replace("店", ""));
                    row2.CreateCell(4).SetCellValue("冷藏");
                    // 合并单元格
                    region = new CellRangeAddress(2, 2, 5, 6); // 
                    sheet.AddMergedRegion(region);
                    // 在合并后的单元格中设置值
                    cell = sheet.GetRow(2).GetCell(5);
                    if (cell == null)
                    {
                        cell = sheet.GetRow(2).CreateCell(5);

                    }
                    cell.SetCellValue(MyVar.saleDate);
                    row2.CreateCell(6).SetCellValue("");



                    ///设置第三行的样式
                    row2.GetCell(0).CellStyle = normalNoBackBoldStyle;
                    row2.GetCell(1).CellStyle = normalNoBackBoldStyle;
                    row2.GetCell(2).CellStyle = normalNoBackStyle;
                    row2.GetCell(3).CellStyle = normalNoBackBoldStyle;
                    row2.GetCell(4).CellStyle = normalNoBackBoldStyle;
                    row2.GetCell(5).CellStyle = normalNoBackStyle;
                    row2.GetCell(6).CellStyle = normalNoBackStyle;


                    //第五行
                    IRow row4 = sheet.CreateRow(4);
                    row4.CreateCell(0).SetCellValue("序号");
                    row4.CreateCell(1).SetCellValue("商品号");
                    row4.CreateCell(2).SetCellValue("商品名称");
                    row4.CreateCell(3).SetCellValue("商品条码");
                    row4.CreateCell(4).SetCellValue("规格");
                    row4.CreateCell(5).SetCellValue("数量");
                    row4.CreateCell(6).SetCellValue("商品号/条码");
                    ///设置有背景颜色的样式
                    for (int j = 0; j <= 6; j++)
                    {
                        row4.GetCell(j).CellStyle = normalBackStyle;
                    }
                    //第六行开始
                    for (int m = 0; m < shoprows.Length; m++)
                    {
                        IRow row5 = sheet.CreateRow(m + 5);
                        row5.CreateCell(0).SetCellValue(m + 1);
                        row5.CreateCell(1).SetCellValue(shoprows[m][1].ToString());
                        row5.CreateCell(2).SetCellValue(shoprows[m][2].ToString());
                        row5.CreateCell(3).SetCellValue(shoprows[m][3].ToString());
                        row5.CreateCell(4).SetCellValue(shoprows[m][4].ToString() + "g");
                        row5.CreateCell(5).SetCellValue(shoprows[m][5].ToString());
                        row5.CreateCell(6).SetCellValue("*" + shoprows[m][3].ToString() + "*");
                        row5.Height = 700;
                        shopfdqty = shopfdqty + int.Parse(shoprows[m][5].ToString());
                        for (int j = 0; j <= 5; j++)
                        {
                            row5.GetCell(j).CellStyle = normalNoBackStyle;
                        }
                        row5.GetCell(6).CellStyle = barcodeStyle; //条形码
                    }


                    //总数行
                    IRow row6 = sheet.CreateRow(shoprows.Length + 5);
                    row6.CreateCell(4).SetCellValue("箱总数");
                    row6.CreateCell(5).SetCellValue(shopfdqty);
                    row6.GetCell(4).CellStyle = normalNoBackStyle;
                    row6.GetCell(5).CellStyle = normalNoBackStyle;



                    //设置宽度
                    sheet.SetColumnWidth(0, 1100);
                    sheet.SetColumnWidth(1, 2400);
                    sheet.SetColumnWidth(2, 4800);
                    sheet.SetColumnWidth(3, 4000);
                    sheet.SetColumnWidth(4, 1600);
                    sheet.SetColumnWidth(5, 1000);
                    sheet.SetColumnWidth(6, 8000);




                    FileStream file = new FileStream(path + fileExt, FileMode.Create);
                    workbook.Write(file);
                    file.Close();
                }
            }
        }

        /// <summary>
        /// 广东逅厨沃尔玛送货单转换-分单打印-基于图形的条形码
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        /// <param name="fileExt"></param>
        public static void TableToExcelGDHCWEMSHD_FD_barcode(string path, DataTable dt, string fileExt)
        { 
            List<int> shopfdnum = new List<int> { }; //门店的分单集合，分店加分单数0,1.2.3.......N
            int shopfdqty;//门店分单的商品总数
            IWorkbook workbook = null;
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                return;
            }

            //条形码字体
            IFont barcodefont = workbook.CreateFont();
            barcodefont.FontHeightInPoints = 18; // 字体大小
            barcodefont.FontName = "3 of 9 Barcode"; // 字体名称
                                                     //正常字体
            IFont normalfont = workbook.CreateFont();
            normalfont.FontHeightInPoints = 11; // 字体大小
            normalfont.FontName = "微软雅黑"; // 字体名称
                                          //加粗字体
            IFont boldfont = workbook.CreateFont();
            boldfont.FontHeightInPoints = 11; // 字体大小
            boldfont.FontName = "微软雅黑"; // 字体名称
            boldfont.IsBold = true;
            //店号大字体
            IFont bigfont = workbook.CreateFont();
            bigfont.FontHeightInPoints = 25; // 字体大小
            bigfont.FontName = "微软雅黑"; // 字体名称
            bigfont.IsBold = true;
            //条形码样式
            ICellStyle barcodeStyle = workbook.CreateCellStyle();
            barcodeStyle.SetFont(barcodefont);
            barcodeStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            barcodeStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            barcodeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式不加粗
            ICellStyle normalNoBackStyle = workbook.CreateCellStyle();
            normalNoBackStyle.SetFont(normalfont);
            normalNoBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式加粗
            ICellStyle normalNoBackBoldStyle = workbook.CreateCellStyle();
            normalNoBackBoldStyle.SetFont(boldfont);
            normalNoBackBoldStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackBoldStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackBoldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格有背景样式加粗
            ICellStyle normalBackStyle = workbook.CreateCellStyle();
            normalBackStyle.SetFont(boldfont);
            normalBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalBackStyle.FillForegroundColor = HSSFColor.Blue.SkyBlue.Index;
            normalBackStyle.FillPattern = FillPattern.SolidForeground;
            normalBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //第一行店号大字体
            ICellStyle bigfontstyle = workbook.CreateCellStyle();
            bigfontstyle.SetFont(bigfont);
            bigfontstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            bigfontstyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            //1、循环门店数
            for (int i = 0; i < MyVar.Shops.Count; i++)
            {
                shopfdnum.Clear();
                int fdpc = -1;  //分单批次

                //从dt中根据店号再过滤出该店号的订单数据
                DataRow[] shoprows = dt.Select("shop = '" + MyVar.Shops[i] + "'");
                foreach (DataRow row in shoprows)
                {
                    if (int.Parse(row[6].ToString()) != fdpc)
                    {
                        fdpc = int.Parse(row[6].ToString());
                        shopfdnum.Add(fdpc);
                    }
                }
                //Console.WriteLine("门店分单数"+shopfdnum);

                //2、循环分单数生成
                for (int k = 0; k < shopfdnum.Count; k++)
                {
                    //以店名+ 分单号创建sheet
                    ISheet sheet = workbook.CreateSheet(MyVar.Shops[i] + "(" + shopfdnum[k] + ")");
                    shoprows = dt.Select("shop = '" + MyVar.Shops[i] + "' and pc='" + shopfdnum[k] + "'");
                    shopfdqty = 0;//分单的商品总数重置为0
                    //第一行
                    IRow row0 = sheet.CreateRow(0);
                    // 合并单元格
                    CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3); // 
                    sheet.AddMergedRegion(region);
                    // 在合并后的单元格中设置值
                    ICell cell = sheet.GetRow(0).GetCell(0);
                    if (cell == null)
                    {
                        cell = sheet.GetRow(0).CreateCell(0);
                    }
                    cell.SetCellValue(MyVar.Shops[i].Replace("店", ""));
                    row0.GetCell(0).CellStyle = bigfontstyle;
                    row0.CreateCell(5).SetCellValue("即食");
                    row0.GetCell(5).CellStyle = bigfontstyle;

                    ///第二行
                    IRow row1 = sheet.CreateRow(1);
                    row1.CreateCell(0).SetCellValue("部门");
                    row1.CreateCell(1).SetCellValue("公司名称");
                    row1.CreateCell(2).SetCellValue("订单号");
                    row1.CreateCell(3).SetCellValue("店号");
                    row1.CreateCell(4).SetCellValue("温区");
                    row1.CreateCell(5).SetCellValue("收货日期");



                    ///设置有背景颜色的样式
                    for (int j = 0; j <= 5; j++)
                    {
                        row1.GetCell(j).CellStyle = normalBackStyle;
                    }
                    ///第三行
                    IRow row2 = sheet.CreateRow(2);
                    row2.CreateCell(0).SetCellValue(MyVar.Dep);
                    row2.CreateCell(1).SetCellValue("江苏逅厨");
                    row2.CreateCell(2).SetCellValue(MyVar.OrderNo);
                    row2.CreateCell(3).SetCellValue(MyVar.Shops[i].Replace("店", ""));
                    row2.CreateCell(4).SetCellValue("冷藏");
                    row2.CreateCell(5).SetCellValue(MyVar.saleDate);


                    ///设置第三行的样式
                    row2.GetCell(0).CellStyle = normalNoBackBoldStyle;
                    row2.GetCell(1).CellStyle = normalNoBackBoldStyle;
                    row2.GetCell(2).CellStyle = normalNoBackStyle;
                    row2.GetCell(3).CellStyle = normalNoBackBoldStyle;
                    row2.GetCell(4).CellStyle = normalNoBackBoldStyle;
                    row2.GetCell(5).CellStyle = normalNoBackStyle;
                    


                    //第五行
                    IRow row4 = sheet.CreateRow(4);
                    row4.CreateCell(0).SetCellValue("序号");
                    row4.CreateCell(1).SetCellValue("商品号");
                    row4.CreateCell(2).SetCellValue("商品名称");
                    row4.CreateCell(3).SetCellValue("规格");
                    row4.CreateCell(4).SetCellValue("数量");
                    row4.CreateCell(5).SetCellValue("商品条码");
                    ///设置有背景颜色的样式
                    for (int j = 0; j <= 5; j++)
                    {
                        row4.GetCell(j).CellStyle = normalBackStyle;
                    }
                    //第六行开始
                    for (int m = 0; m < shoprows.Length; m++)
                    {
                        IRow row5 = sheet.CreateRow(m + 5);
                        row5.CreateCell(0).SetCellValue(m + 1);
                        row5.CreateCell(1).SetCellValue(shoprows[m][1].ToString());
                        row5.CreateCell(2).SetCellValue(shoprows[m][2].ToString());
                        row5.CreateCell(3).SetCellValue(shoprows[m][4].ToString() + "g");
                        row5.CreateCell(4).SetCellValue(shoprows[m][5].ToString());
                        row5.CreateCell(5).SetCellValue("");
                        BarcodeFormat format = BarcodeFormat.EAN_13;
                        var barcodeImage = BarcodeHelper.GenerateBarcode(shoprows[m][3].ToString(), format);
                        MemoryStream ms = new MemoryStream();
                        barcodeImage.Save(ms, ImageFormat.Png);
                        int pictureIdx = workbook.AddPicture(ms.ToArray(), PictureType.PNG);
                        IDrawing drawing = sheet.CreateDrawingPatriarch();
                        IClientAnchor anchor = drawing.CreateAnchor(2 * 9525, 1 * 9525, 1000 * 9525, 110 * 9525, 5, m + 5, 5, m + 5);
                        //dx1：起始单元格的x偏移量；
                        //dy1：起始单元格的y偏移量；
                        //dx2：终止单元格的x偏移量；
                        //dy2：终止单元格的y偏移量；
                        //col1：起始单元格列序号；
                        //row1：起始单元格行序号；
                        //col2：终止单元格列序号；
                        //row2：终止单元格行序号；
                        IPicture pict = drawing.CreatePicture(anchor, pictureIdx);
                        //pict.Resize(1); // 自动调整图片大小以适应单元格大小
                        row5.Height = 850;
                        shopfdqty = shopfdqty + int.Parse(shoprows[m][5].ToString());
                        for (int j = 0; j <= 5; j++)
                        {
                            row5.GetCell(j).CellStyle = normalNoBackStyle;
                        }
                        
                    }


                    //总数行
                    IRow row6 = sheet.CreateRow(shoprows.Length + 5);
                    row6.CreateCell(3).SetCellValue("箱总数");
                    row6.CreateCell(4).SetCellValue(shopfdqty);
                    row6.GetCell(3).CellStyle = normalNoBackStyle;
                    row6.GetCell(4).CellStyle = normalNoBackStyle;



                    //设置宽度
                    sheet.SetColumnWidth(0, 1100);
                    sheet.SetColumnWidth(1, 2400);
                    sheet.SetColumnWidth(2, 4800);
                    sheet.SetColumnWidth(3, 2000);
                    sheet.SetColumnWidth(4, 1600);
                    sheet.SetColumnWidth(5, 8000);




                    FileStream file = new FileStream(path + fileExt, FileMode.Create);
                    workbook.Write(file);
                    file.Close();
                }
            }
        }
        /// <summary>
        /// 广东逅厨沃尔玛送货单转换多订单-字体条形码
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        /// <param name="fileExt"></param>
        public static void TableToExcelGDHCWEMSHDDDD(string path, DataTable dt, string fileExt)
        {
            //需要分单的订单开头字符串FDSTARTNUM
            string[] FDSTARTNUM = Properties.Settings.Default.fdNums.ToString().Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> shopfdnum = new List<int> { }; //门店的分单集合，分店加分单数0,1.2.3.......N
            List<string> sheetlist = new List<string> { };//已经创建的sheet名称集合，如果遇到一个门店多个订单的，重复的需要加门店后缀
            int shopfdqty;//分单的话门店分单的商品总数
            int shopqty;//不分单的话门店商品总数
            ISheet sheet;
            IWorkbook workbook = null;
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                return;
            }
            //条形码字体
            IFont barcodefont = workbook.CreateFont();
            barcodefont.FontHeightInPoints = 18; // 字体大小
            barcodefont.FontName = "3 of 9 Barcode"; // 字体名称
                                                     //正常字体
            IFont normalfont = workbook.CreateFont();
            normalfont.FontHeightInPoints = 11; // 字体大小
            normalfont.FontName = "微软雅黑"; // 字体名称
                                          //加粗字体
            IFont boldfont = workbook.CreateFont();
            boldfont.FontHeightInPoints = 11; // 字体大小
            boldfont.FontName = "微软雅黑"; // 字体名称
            boldfont.IsBold = true;
            //店号大字体
            IFont bigfont = workbook.CreateFont();
            bigfont.FontHeightInPoints = 25; // 字体大小
            bigfont.FontName = "微软雅黑"; // 字体名称
            bigfont.IsBold = true;
            //条形码样式
            ICellStyle barcodeStyle = workbook.CreateCellStyle();
            barcodeStyle.SetFont(barcodefont);
            barcodeStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            barcodeStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            barcodeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式不加粗
            ICellStyle normalNoBackStyle = workbook.CreateCellStyle();
            normalNoBackStyle.SetFont(normalfont);
            normalNoBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式加粗
            ICellStyle normalNoBackBoldStyle = workbook.CreateCellStyle();
            normalNoBackBoldStyle.SetFont(boldfont);
            normalNoBackBoldStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackBoldStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackBoldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格有背景样式加粗
            ICellStyle normalBackStyle = workbook.CreateCellStyle();
            normalBackStyle.SetFont(boldfont);
            normalBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalBackStyle.FillForegroundColor = HSSFColor.Blue.SkyBlue.Index;
            normalBackStyle.FillPattern = FillPattern.SolidForeground;
            normalBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //第一行店号大字体
            ICellStyle bigfontstyle = workbook.CreateCellStyle();
            bigfontstyle.SetFont(bigfont);
            bigfontstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            bigfontstyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            //按订单号遍历
            for (int k = 0; k < MyVar.Orders.Count; k++)
            {
                //如果订单号25开头，需要分单生成
                //if (MyVar.Orders[k].Substring(0, 2) == "25")
                //判断订单号前几个字符是否包含需要分单的订单号
                if (FDSTARTNUM.Any(prefix => MyVar.Orders[k].StartsWith(prefix)))
                {
                    for (int i = 0; i < MyVar.Shops.Count; i++)
                    {
                        int fdpc = -1;  //分单批次
                        //门店分单信息及商品汇总数清零
                        shopfdnum.Clear();
                        shopfdqty = 0;
                        //从dt中根据订单号和店号过滤出该店号的订单数据
                        DataRow[] shoprows = dt.Select("shop = '" + MyVar.Shops[i] + "' and orderid='" + MyVar.Orders[k] + "'");
                        if (shoprows.Length == 0)
                        {
                            continue;
                        }
                        else
                        { 
                            foreach (DataRow row in shoprows)
                            {
                                if (int.Parse(row[7].ToString()) != fdpc)
                                {
                                    fdpc = int.Parse(row[7].ToString());
                                    shopfdnum.Add(fdpc);
                                }
                            }
                            //2、循环分单数生成
                            for (int fd = 0; fd < shopfdnum.Count; fd++)
                            {
                                //以店名+ 分单号创建sheet
                                //判断已经生成的sheet里是否包含将要生成的sheet页名称
                                if (!sheetlist.Contains(MyVar.Shops[i] + "(" + shopfdnum[fd] + ")"))
                                {
                                    sheet = workbook.CreateSheet(MyVar.Shops[i] + "(" + shopfdnum[fd] + ")");
                                    sheetlist.Add(MyVar.Shops[i] + "(" + shopfdnum[fd] + ")");
                                }
                                else
                                {
                                    sheet = workbook.CreateSheet(MyVar.Shops[i] + "(" + shopfdnum[fd] + ")"+"补");
                                }
                                shoprows = dt.Select("orderid = '" + MyVar.Orders[k] + "' and shop = '" + MyVar.Shops[i] + "' and pc='" + shopfdnum[fd] + "'");
                                shopfdqty = 0;//分单的商品总数重置为0
                                              //第一行
                                IRow row0 = sheet.CreateRow(0);
                                // 合并单元格
                                CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3); // 
                                sheet.AddMergedRegion(region);
                                // 在合并后的单元格中设置值
                                ICell cell = sheet.GetRow(0).GetCell(0);
                                if (cell == null)
                                {
                                    cell = sheet.GetRow(0).CreateCell(0);
                                }
                                cell.SetCellValue(MyVar.Shops[i].Replace("店", ""));
                                row0.GetCell(0).CellStyle = bigfontstyle;
                                row0.CreateCell(6).SetCellValue("即食");
                                row0.GetCell(6).CellStyle = bigfontstyle;

                                ///第二行
                                IRow row1 = sheet.CreateRow(1);
                                row1.CreateCell(0).SetCellValue("部门");
                                row1.CreateCell(1).SetCellValue("公司名称");
                                row1.CreateCell(2).SetCellValue("订单号");
                                row1.CreateCell(3).SetCellValue("店号");
                                row1.CreateCell(4).SetCellValue("温区");
                                // 合并单元格
                                region = new CellRangeAddress(1, 1, 5, 6); // 
                                sheet.AddMergedRegion(region);
                                // 在合并后的单元格中设置值
                                cell = sheet.GetRow(1).GetCell(5);

                                if (cell == null)
                                {
                                    cell = sheet.GetRow(1).CreateCell(5);

                                }
                                cell.SetCellValue("收货日期");
                                row1.CreateCell(6).SetCellValue("");



                                ///设置有背景颜色的样式
                                for (int j = 0; j <= 6; j++)
                                {
                                    row1.GetCell(j).CellStyle = normalBackStyle;
                                }
                                ///第三行
                                IRow row2 = sheet.CreateRow(2);
                                row2.CreateCell(0).SetCellValue(MyVar.Dep);
                                row2.CreateCell(1).SetCellValue("江苏逅厨");
                                row2.CreateCell(2).SetCellValue(shoprows[0][0].ToString());
                                row2.CreateCell(3).SetCellValue(MyVar.Shops[i].Replace("店", ""));
                                row2.CreateCell(4).SetCellValue("冷藏");
                                // 合并单元格
                                region = new CellRangeAddress(2, 2, 5, 6); // 
                                sheet.AddMergedRegion(region);
                                // 在合并后的单元格中设置值
                                cell = sheet.GetRow(2).GetCell(5);
                                if (cell == null)
                                {
                                    cell = sheet.GetRow(2).CreateCell(5);

                                }
                                cell.SetCellValue(MyVar.saleDate);
                                row2.CreateCell(6).SetCellValue("");



                                ///设置第三行的样式
                                row2.GetCell(0).CellStyle = normalNoBackBoldStyle;
                                row2.GetCell(1).CellStyle = normalNoBackBoldStyle;
                                row2.GetCell(2).CellStyle = normalNoBackStyle;
                                row2.GetCell(3).CellStyle = normalNoBackBoldStyle;
                                row2.GetCell(4).CellStyle = normalNoBackBoldStyle;
                                row2.GetCell(5).CellStyle = normalNoBackStyle;
                                row2.GetCell(6).CellStyle = normalNoBackStyle;


                                //第五行
                                IRow row4 = sheet.CreateRow(4);
                                row4.CreateCell(0).SetCellValue("序号");
                                row4.CreateCell(1).SetCellValue("商品号");
                                row4.CreateCell(2).SetCellValue("商品名称");
                                row4.CreateCell(3).SetCellValue("商品条码");
                                row4.CreateCell(4).SetCellValue("规格");
                                row4.CreateCell(5).SetCellValue("数量");
                                row4.CreateCell(6).SetCellValue("商品号/条码");
                                ///设置有背景颜色的样式
                                for (int j = 0; j <= 6; j++)
                                {
                                    row4.GetCell(j).CellStyle = normalBackStyle;
                                }
                                //第六行开始
                                for (int m = 0; m < shoprows.Length; m++)
                                {
                                    IRow row5 = sheet.CreateRow(m + 5);
                                    row5.CreateCell(0).SetCellValue(m + 1);
                                    row5.CreateCell(1).SetCellValue(shoprows[m][2].ToString());
                                    row5.CreateCell(2).SetCellValue(shoprows[m][3].ToString());
                                    row5.CreateCell(3).SetCellValue(shoprows[m][4].ToString());
                                    row5.CreateCell(4).SetCellValue(shoprows[m][5].ToString() + "g");
                                    row5.CreateCell(5).SetCellValue(shoprows[m][6].ToString());
                                    row5.CreateCell(6).SetCellValue("*" + shoprows[m][4].ToString() + "*");
                                    row5.Height = 700;
                                    shopfdqty = shopfdqty + int.Parse(shoprows[m][6].ToString());
                                    for (int j = 0; j <= 5; j++)
                                    {
                                        row5.GetCell(j).CellStyle = normalNoBackStyle;
                                    }
                                    row5.GetCell(6).CellStyle = barcodeStyle; //条形码
                                }


                                //总数行
                                IRow row6 = sheet.CreateRow(shoprows.Length + 5);
                                row6.CreateCell(4).SetCellValue("箱总数");
                                row6.CreateCell(5).SetCellValue(shopfdqty);
                                row6.GetCell(4).CellStyle = normalNoBackStyle;
                                row6.GetCell(5).CellStyle = normalNoBackStyle;



                                //设置宽度
                                sheet.SetColumnWidth(0, 1100);
                                sheet.SetColumnWidth(1, 2400);
                                sheet.SetColumnWidth(2, 4800);
                                sheet.SetColumnWidth(3, 4000);
                                sheet.SetColumnWidth(4, 1600);
                                sheet.SetColumnWidth(5, 1000);
                                sheet.SetColumnWidth(6, 8000);

                                FileStream file = new FileStream(path + fileExt, FileMode.Create);
                                workbook.Write(file);
                                file.Close();
                            }
                        }
                    }
                }
                //如果订单号93开头，不需要分单生成
                //else if (MyVar.Orders[k].Substring(0, 2) == "93"|| MyVar.Orders[k].Substring(0, 2) == "65")
                else
                {
                    for (int i = 0; i < MyVar.Shops.Count; i++)
                    {
                        //门店汇总数清零
                        shopqty = 0;
                        //从dt中根据订单号和店号过滤出该店号的订单数据
                        DataRow[] shoprows = dt.Select("shop = '" + MyVar.Shops[i] + "' and orderid='" + MyVar.Orders[k] + "'");
                        if (shoprows.Length == 0)
                        {
                            continue;
                        }
                        else
                        {
                            //以店名和批次创建sheet
                            //先判断已有的sheet页签是否包含将要生成的页签名称
                            if (!sheetlist.Contains(MyVar.Shops[i]))
                            {
                                sheet = workbook.CreateSheet(MyVar.Shops[i]);
                                sheetlist.Add(MyVar.Shops[i]);
                            }
                            else
                            {
                                sheet = workbook.CreateSheet(MyVar.Shops[i]+"补");
                            }
                            //第一行
                            IRow row0 = sheet.CreateRow(0);
                            // 合并单元格
                            CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3); // 
                            sheet.AddMergedRegion(region);
                            // 在合并后的单元格中设置值
                            ICell cell = sheet.GetRow(0).GetCell(0);
                            if (cell == null)
                            {
                                cell = sheet.GetRow(0).CreateCell(0);
                            }
                            cell.SetCellValue(MyVar.Shops[i].Replace("店", ""));
                            row0.GetCell(0).CellStyle = bigfontstyle;
                            row0.CreateCell(6).SetCellValue("即食");
                            row0.GetCell(6).CellStyle = bigfontstyle;

                            ///第二行
                            IRow row1 = sheet.CreateRow(1);
                            row1.CreateCell(0).SetCellValue("部门");
                            row1.CreateCell(1).SetCellValue("公司名称");
                            row1.CreateCell(2).SetCellValue("订单号");
                            row1.CreateCell(3).SetCellValue("店号");
                            row1.CreateCell(4).SetCellValue("温区");
                            // 合并单元格
                            region = new CellRangeAddress(1, 1, 5, 6); // 
                            sheet.AddMergedRegion(region);
                            // 在合并后的单元格中设置值
                            cell = sheet.GetRow(1).GetCell(5);

                            if (cell == null)
                            {
                                cell = sheet.GetRow(1).CreateCell(5);

                            }
                            cell.SetCellValue("收货日期");
                            row1.CreateCell(6).SetCellValue("");



                            ///设置有背景颜色的样式
                            for (int j = 0; j <= 6; j++)
                            {
                                row1.GetCell(j).CellStyle = normalBackStyle;
                            }
                            ///第三行
                            IRow row2 = sheet.CreateRow(2);
                            row2.CreateCell(0).SetCellValue(MyVar.Dep);
                            row2.CreateCell(1).SetCellValue("江苏逅厨");
                            row2.CreateCell(2).SetCellValue(shoprows[0][0].ToString());
                            row2.CreateCell(3).SetCellValue(MyVar.Shops[i].Replace("店", ""));
                            row2.CreateCell(4).SetCellValue("冷藏");
                            // 合并单元格
                            region = new CellRangeAddress(2, 2, 5, 6); // 
                            sheet.AddMergedRegion(region);
                            // 在合并后的单元格中设置值
                            cell = sheet.GetRow(2).GetCell(5);
                            if (cell == null)
                            {
                                cell = sheet.GetRow(2).CreateCell(5);

                            }
                            cell.SetCellValue(MyVar.saleDate);
                            row2.CreateCell(6).SetCellValue("");



                            ///设置第三行的样式
                            row2.GetCell(0).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(1).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(2).CellStyle = normalNoBackStyle;
                            row2.GetCell(3).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(4).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(5).CellStyle = normalNoBackStyle;
                            row2.GetCell(6).CellStyle = normalNoBackStyle;


                            //第五行
                            IRow row4 = sheet.CreateRow(4);
                            row4.CreateCell(0).SetCellValue("序号");
                            row4.CreateCell(1).SetCellValue("商品号");
                            row4.CreateCell(2).SetCellValue("商品名称");
                            row4.CreateCell(3).SetCellValue("商品条码");
                            row4.CreateCell(4).SetCellValue("规格");
                            row4.CreateCell(5).SetCellValue("数量");
                            row4.CreateCell(6).SetCellValue("商品号/条码");
                            ///设置有背景颜色的样式
                            for (int j = 0; j <= 6; j++)
                            {
                                row4.GetCell(j).CellStyle = normalBackStyle;
                            }
                            //第六行开始
                            for (int m = 0; m < shoprows.Length; m++)
                            {
                                IRow row5 = sheet.CreateRow(m + 5);
                                row5.CreateCell(0).SetCellValue(m + 1);
                                row5.CreateCell(1).SetCellValue(shoprows[m][2].ToString());
                                row5.CreateCell(2).SetCellValue(shoprows[m][3].ToString());
                                row5.CreateCell(3).SetCellValue(shoprows[m][4].ToString());
                                row5.CreateCell(4).SetCellValue(shoprows[m][5].ToString() + "g");
                                row5.CreateCell(5).SetCellValue(shoprows[m][6].ToString());
                                row5.CreateCell(6).SetCellValue("*" + shoprows[m][4].ToString() + "*");   //利用excel中的条码字体生成条形码
                                row5.Height = 700;
                                shopqty = shopqty + int.Parse(shoprows[m][6].ToString());
                                for (int j = 0; j <= 5; j++)
                                {
                                    row5.GetCell(j).CellStyle = normalNoBackStyle;
                                }
                                row5.GetCell(6).CellStyle = barcodeStyle; //条形码//利用excel中的条码字体生成条形码
                            }


                            //总数行
                            IRow row6 = sheet.CreateRow(shoprows.Length + 5);
                            row6.CreateCell(4).SetCellValue("箱总数");
                            row6.CreateCell(5).SetCellValue(shopqty);
                            row6.GetCell(4).CellStyle = normalNoBackStyle;
                            row6.GetCell(5).CellStyle = normalNoBackStyle;



                            //设置宽度
                            sheet.SetColumnWidth(0, 1100);
                            sheet.SetColumnWidth(1, 2400);
                            sheet.SetColumnWidth(2, 4800);
                            sheet.SetColumnWidth(3, 4000);
                            sheet.SetColumnWidth(4, 1600);
                            sheet.SetColumnWidth(5, 1000);
                            sheet.SetColumnWidth(6, 8000);

                            FileStream file = new FileStream(path + fileExt, FileMode.Create);
                            workbook.Write(file);
                            file.Close();
                        }
                    }
                }
            }
           
        }

        /// <summary>
        /// 广东逅厨沃尔玛送货单转换多订单-图形条形码
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        /// <param name="fileExt"></param>
        public static void TableToExcelGDHCWEMSHDDDD_barcode(string path, DataTable dt, string fileExt)
        {
            //需要分单的订单开头字符串FDSTARTNUM
            string[] FDSTARTNUM = Properties.Settings.Default.fdNums.ToString().Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> shopfdnum = new List<int> { }; //门店的分单集合，分店加分单数0,1.2.3.......N
            List<string> sheetlist = new List<string> { };//已经创建的sheet名称集合，如果遇到一个门店多个订单的，重复的需要加门店后缀
            int shopfdqty;//分单的话门店分单的商品总数
            int shopqty;//不分单的话门店商品总数
            int xs;//单独装满一个品的箱数
            ISheet sheet;
            IWorkbook workbook = null;
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                return;
            }
            //条形码字体
            IFont barcodefont = workbook.CreateFont();
            barcodefont.FontHeightInPoints = 18; // 字体大小
            barcodefont.FontName = "3 of 9 Barcode"; // 字体名称
                                                     //正常字体
            IFont normalfont = workbook.CreateFont();
            normalfont.FontHeightInPoints = 11; // 字体大小
            normalfont.FontName = "微软雅黑"; // 字体名称
                                          //加粗字体
            IFont boldfont = workbook.CreateFont();
            boldfont.FontHeightInPoints = 11; // 字体大小
            boldfont.FontName = "微软雅黑"; // 字体名称
            boldfont.IsBold = true;
            //店号大字体
            IFont bigfont = workbook.CreateFont();
            bigfont.FontHeightInPoints = 16; // 字体大小
            bigfont.FontName = "微软雅黑"; // 字体名称
            bigfont.IsBold = true;
            //条形码样式
            ICellStyle barcodeStyle = workbook.CreateCellStyle();
            barcodeStyle.SetFont(barcodefont);
            barcodeStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            barcodeStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            barcodeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            barcodeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式不加粗
            ICellStyle normalNoBackStyle = workbook.CreateCellStyle();
            normalNoBackStyle.SetFont(normalfont);
            normalNoBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格无背景样式加粗
            ICellStyle normalNoBackBoldStyle = workbook.CreateCellStyle();
            normalNoBackBoldStyle.SetFont(boldfont);
            normalNoBackBoldStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalNoBackBoldStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalNoBackBoldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalNoBackBoldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //正常单元格有背景样式加粗
            ICellStyle normalBackStyle = workbook.CreateCellStyle();
            normalBackStyle.SetFont(boldfont);
            normalBackStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            normalBackStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            normalBackStyle.FillForegroundColor = HSSFColor.Blue.SkyBlue.Index;
            normalBackStyle.FillPattern = FillPattern.SolidForeground;
            normalBackStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalBackStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //第一行店号大字体
            ICellStyle bigfontstyle = workbook.CreateCellStyle();
            bigfontstyle.SetFont(bigfont);
            bigfontstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            bigfontstyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            //按订单号遍历
            for (int k = 0; k < MyVar.Orders.Count; k++)
            {
                //如果订单号25开头，需要分单生成
                //if (MyVar.Orders[k].Substring(0, 2) == "25")
                //判断订单号前几个字符是否包含需要分单的订单号前缀（界面上自定义）
                if (FDSTARTNUM.Any(prefix => MyVar.Orders[k].StartsWith(prefix)))
                {
                    for (int i = 0; i < MyVar.Shops.Count; i++)
                    {
                        int fdpc = -1;  //分单批次
                        //门店分单信息及商品汇总数清零
                        shopfdnum.Clear();
                        shopfdqty = 0;
                        //从dt中根据订单号和店号过滤出该店号的订单数据
                        DataRow[] shoprows = dt.Select("shop = '" + MyVar.Shops[i] + "' and orderid='" + MyVar.Orders[k] + "'");
                        if (shoprows.Length == 0)
                        {
                            continue;
                        }
                        else
                        {
                            //分单批次的组合
                            foreach (DataRow row in shoprows)
                            {
                                if (int.Parse(row[7].ToString()) != fdpc)
                                {
                                    fdpc = int.Parse(row[7].ToString());
                                    shopfdnum.Add(fdpc);
                                }
                            }
                            //2、循环分单数生成
                            for (int fd = 0; fd < shopfdnum.Count; fd++)
                            {
                                //以店名+ 分单号创建sheet
                                //判断已经生成的sheet里是否包含将要生成的sheet页名称,
                                //如果sheet组合里不包含该店名加批次并且箱数=0则新建
                                if (!sheetlist.Contains(MyVar.Shops[i] + "(" + shopfdnum[fd] + ")"))
                                {
                                    sheet = workbook.CreateSheet(MyVar.Shops[i] + "(" + shopfdnum[fd] + ")");
                                    sheetlist.Add(MyVar.Shops[i] + "(" + shopfdnum[fd] + ")");
                                }
                                else
                                {
                                    sheet = workbook.CreateSheet(MyVar.Shops[i] + "(" + shopfdnum[fd] + ")" + "补");
                                }
                                shoprows = dt.Select("orderid = '" + MyVar.Orders[k] + "' and shop = '" + MyVar.Shops[i] + "' and pc='" + shopfdnum[fd] + "' and ys>0");
                                shopfdqty = 0;//分单的商品总数重置为0
                                              //第一行
                                IRow row0 = sheet.CreateRow(0);
                                // 合并单元格
                                CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3); // 
                                sheet.AddMergedRegion(region);
                                // 在合并后的单元格中设置值
                                ICell cell = sheet.GetRow(0).GetCell(0);
                                if (cell == null)
                                {
                                    cell = sheet.GetRow(0).CreateCell(0);
                                }
                                cell.SetCellValue(MyVar.Shops[i].Replace("店", ""));
                                row0.GetCell(0).CellStyle = bigfontstyle;
                                row0.CreateCell(5).SetCellValue("即食");
                                row0.GetCell(5).CellStyle = bigfontstyle;

                                ///第二行
                                IRow row1 = sheet.CreateRow(1);
                                row1.CreateCell(0).SetCellValue("部门");
                                row1.CreateCell(1).SetCellValue("公司名称");
                                row1.CreateCell(2).SetCellValue("订单号");
                                row1.CreateCell(3).SetCellValue("店号");
                                row1.CreateCell(4).SetCellValue("温区");
                                row1.CreateCell(5).SetCellValue("收货日期");
                                
                                



                                ///设置有背景颜色的样式
                                for (int j = 0; j <= 5; j++)
                                {
                                    row1.GetCell(j).CellStyle = normalBackStyle;
                                }
                                ///第三行
                                IRow row2 = sheet.CreateRow(2);
                                row2.CreateCell(0).SetCellValue(MyVar.Dep);
                                row2.CreateCell(1).SetCellValue("江苏逅厨");
                                row2.CreateCell(2).SetCellValue(shoprows[0][0].ToString());
                                row2.CreateCell(3).SetCellValue(MyVar.Shops[i].Replace("店", ""));
                                row2.CreateCell(4).SetCellValue("冷藏");
                                row2.CreateCell(5).SetCellValue(MyVar.saleDate);



                                ///设置第三行的样式
                                row2.GetCell(0).CellStyle = normalNoBackBoldStyle;
                                row2.GetCell(1).CellStyle = normalNoBackBoldStyle;
                                row2.GetCell(2).CellStyle = normalNoBackStyle;
                                row2.GetCell(3).CellStyle = normalNoBackBoldStyle;
                                row2.GetCell(4).CellStyle = normalNoBackBoldStyle;
                                row2.GetCell(5).CellStyle = normalNoBackStyle;
                                

                                //第五行
                                IRow row4 = sheet.CreateRow(4);
                                row4.CreateCell(0).SetCellValue("序号");
                                row4.CreateCell(1).SetCellValue("商品号");
                                row4.CreateCell(2).SetCellValue("商品名称");
                                row4.CreateCell(3).SetCellValue("规格");
                                row4.CreateCell(4).SetCellValue("数量");
                                row4.CreateCell(5).SetCellValue("商品条码");
                                ///设置有背景颜色的样式
                                for (int j = 0; j <= 5; j++)
                                {
                                    row4.GetCell(j).CellStyle = normalBackStyle;
                                }
                                //第六行开始
                                for (int m = 0; m < shoprows.Length; m++)
                                {
                                    IRow row5 = sheet.CreateRow(m + 5);
                                    row5.CreateCell(0).SetCellValue(m + 1);
                                    row5.CreateCell(1).SetCellValue(shoprows[m][2].ToString());
                                    row5.CreateCell(2).SetCellValue(shoprows[m][3].ToString());
                                    row5.CreateCell(3).SetCellValue(shoprows[m][5].ToString() + "g");
                                    row5.CreateCell(4).SetCellValue(shoprows[m][9].ToString()); //余数
                                    row5.CreateCell(5).SetCellValue("");
                                    BarcodeFormat format = BarcodeFormat.EAN_13;
                                    var barcodeImage = BarcodeHelper.GenerateBarcode(shoprows[m][4].ToString(), format);
                                    MemoryStream ms = new MemoryStream();
                                    barcodeImage.Save(ms, ImageFormat.Png);
                                    int pictureIdx = workbook.AddPicture(ms.ToArray(), PictureType.PNG);
                                    IDrawing drawing = sheet.CreateDrawingPatriarch();
                                    IClientAnchor anchor = drawing.CreateAnchor(2 * 9525, 1 * 9525, 800 * 9525, 80 * 9525, 5, m + 5, 5, m + 5);
                                    //dx1：起始单元格的x偏移量；
                                    //dy1：起始单元格的y偏移量；
                                    //dx2：终止单元格的x偏移量；
                                    //dy2：终止单元格的y偏移量；
                                    //col1：起始单元格列序号；
                                    //row1：起始单元格行序号；
                                    //col2：终止单元格列序号；
                                    //row2：终止单元格行序号；
                                    IPicture pict = drawing.CreatePicture(anchor, pictureIdx);
                                    //pict.Resize(1); // 自动调整图片大小以适应单元格大小
                                    row5.Height = 560;
                                    shopfdqty = shopfdqty + int.Parse(shoprows[m][9].ToString());
                                    for (int j = 0; j <= 5; j++)
                                    {
                                        row5.GetCell(j).CellStyle = normalNoBackStyle;
                                    }

                                }


                                //总数行
                                IRow row6 = sheet.CreateRow(shoprows.Length + 5);
                                row6.CreateCell(3).SetCellValue("箱总数");
                                row6.CreateCell(4).SetCellValue(shopfdqty);
                                row6.GetCell(3).CellStyle = normalNoBackStyle;
                                row6.GetCell(4).CellStyle = normalNoBackStyle;



                                //设置宽度
                                sheet.SetColumnWidth(0, 1100);
                                sheet.SetColumnWidth(1, 2400);
                                sheet.SetColumnWidth(2, 4800);
                                sheet.SetColumnWidth(3, 2000);
                                sheet.SetColumnWidth(4, 1600);
                                sheet.SetColumnWidth(5, 7000);

                                FileStream file = new FileStream(path + fileExt, FileMode.Create);
                                workbook.Write(file);
                                file.Close();
                            }
                        }

                        //取箱数不为0的数据生成sheet
                        //从dt中过滤根据订单号和店号，以及箱数不为0订单数据shoprowsXS
                        DataRow[] shoprowsXS = dt.Select("shop = '" + MyVar.Shops[i] + "' and orderid='" + MyVar.Orders[k] + "' and xs>0");
                        for (int y = 0; y < shoprowsXS.Count(); y++)
                        {
                            sheet = workbook.CreateSheet(MyVar.Shops[i] + shoprowsXS[y][3].ToString()+"(箱数"+ shoprowsXS[y][8].ToString()+")");
                            IRow row0 = sheet.CreateRow(0);
                            // 合并单元格
                            CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3); // 
                            sheet.AddMergedRegion(region);
                            // 在合并后的单元格中设置值
                            ICell cell = sheet.GetRow(0).GetCell(0);
                            if (cell == null)
                            {
                                cell = sheet.GetRow(0).CreateCell(0);
                            }
                            cell.SetCellValue(MyVar.Shops[i].Replace("店", ""));
                            row0.GetCell(0).CellStyle = bigfontstyle;
                            row0.CreateCell(5).SetCellValue("即食");
                            row0.GetCell(5).CellStyle = bigfontstyle;

                            ///第二行
                            IRow row1 = sheet.CreateRow(1);
                            row1.CreateCell(0).SetCellValue("部门");
                            row1.CreateCell(1).SetCellValue("公司名称");
                            row1.CreateCell(2).SetCellValue("订单号");
                            row1.CreateCell(3).SetCellValue("店号");
                            row1.CreateCell(4).SetCellValue("温区");
                            row1.CreateCell(5).SetCellValue("收货日期");





                            ///设置有背景颜色的样式
                            for (int j = 0; j <= 5; j++)
                            {
                                row1.GetCell(j).CellStyle = normalBackStyle;
                            }
                            ///第三行
                            IRow row2 = sheet.CreateRow(2);
                            row2.CreateCell(0).SetCellValue(MyVar.Dep);
                            row2.CreateCell(1).SetCellValue("江苏逅厨");
                            row2.CreateCell(2).SetCellValue(shoprowsXS[0][0].ToString());
                            row2.CreateCell(3).SetCellValue(MyVar.Shops[i].Replace("店", ""));
                            row2.CreateCell(4).SetCellValue("冷藏");
                            row2.CreateCell(5).SetCellValue(MyVar.saleDate);



                            ///设置第三行的样式
                            row2.GetCell(0).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(1).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(2).CellStyle = normalNoBackStyle;
                            row2.GetCell(3).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(4).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(5).CellStyle = normalNoBackStyle;


                            //第五行
                            IRow row4 = sheet.CreateRow(4);
                            row4.CreateCell(0).SetCellValue("序号");
                            row4.CreateCell(1).SetCellValue("商品号");
                            row4.CreateCell(2).SetCellValue("商品名称");
                            row4.CreateCell(3).SetCellValue("规格");
                            row4.CreateCell(4).SetCellValue("数量");
                            row4.CreateCell(5).SetCellValue("商品条码");
                            ///设置有背景颜色的样式
                            for (int j = 0; j <= 5; j++)
                            {
                                row4.GetCell(j).CellStyle = normalBackStyle;
                            }
                            //第六行
                            IRow row5 = sheet.CreateRow(5);
                            row5.CreateCell(0).SetCellValue(1);
                            row5.CreateCell(1).SetCellValue(shoprowsXS[y][2].ToString());
                            row5.CreateCell(2).SetCellValue(shoprowsXS[y][3].ToString());
                            row5.CreateCell(3).SetCellValue(shoprowsXS[y][5].ToString() + "g");
                            row5.CreateCell(4).SetCellValue(shoprowsXS[y][10].ToString()); 
                            row5.CreateCell(5).SetCellValue("");
                            BarcodeFormat format = BarcodeFormat.EAN_13;
                            var barcodeImage = BarcodeHelper.GenerateBarcode(shoprowsXS[y][4].ToString(), format);
                            MemoryStream ms = new MemoryStream();
                            barcodeImage.Save(ms, ImageFormat.Png);
                            int pictureIdx = workbook.AddPicture(ms.ToArray(), PictureType.PNG);
                            IDrawing drawing = sheet.CreateDrawingPatriarch();
                            IClientAnchor anchor = drawing.CreateAnchor(2 * 9525, 1 * 9525, 800 * 9525, 80 * 9525, 5, 5, 5, 5);
                            //dx1：起始单元格的x偏移量；
                            //dy1：起始单元格的y偏移量；
                            //dx2：终止单元格的x偏移量；
                            //dy2：终止单元格的y偏移量；
                            //col1：起始单元格列序号；
                            //row1：起始单元格行序号；
                            //col2：终止单元格列序号；
                            //row2：终止单元格行序号；
                            IPicture pict = drawing.CreatePicture(anchor, pictureIdx);
                            //pict.Resize(1); // 自动调整图片大小以适应单元格大小
                            row5.Height = 560;
                            for (int j = 0; j <= 5; j++)
                            {
                                row5.GetCell(j).CellStyle = normalNoBackStyle;
                            }

                            //总数行
                            IRow row6 = sheet.CreateRow(6);
                            row6.CreateCell(3).SetCellValue("箱总数");
                            row6.CreateCell(4).SetCellValue(shoprowsXS[y][10].ToString());
                            row6.GetCell(3).CellStyle = normalNoBackStyle;
                            row6.GetCell(4).CellStyle = normalNoBackStyle;



                            //设置宽度
                            sheet.SetColumnWidth(0, 1100);
                            sheet.SetColumnWidth(1, 2400);
                            sheet.SetColumnWidth(2, 4800);
                            sheet.SetColumnWidth(3, 2000);
                            sheet.SetColumnWidth(4, 1600);
                            sheet.SetColumnWidth(5, 7000);

                            FileStream file = new FileStream(path + fileExt, FileMode.Create);
                            workbook.Write(file);
                            file.Close();
                        }
                    }
                    
                    
                }
                //如果订单号93开头，不需要分单生成
                //else if (MyVar.Orders[k].Substring(0, 2) == "93"||MyVar.Orders[k].Substring(0, 2) == "65")
                //如果订单号前缀不包含需要分单的前缀，则不分单
                else
                {
                    for (int i = 0; i < MyVar.Shops.Count; i++)
                    {
                        //门店汇总数清零
                        shopqty = 0;
                        //从dt中根据订单号和店号过滤出该店号的订单数据
                        DataRow[] shoprows = dt.Select("shop = '" + MyVar.Shops[i] + "' and orderid='" + MyVar.Orders[k] + "'");
                        if (shoprows.Length == 0)
                        {
                            continue;
                        }
                        else
                        {
                            //以店名和批次创建sheet
                            //先判断已有的sheet页签是否包含将要生成的页签名称
                            if (!sheetlist.Contains(MyVar.Shops[i]))
                            {
                                sheet = workbook.CreateSheet(MyVar.Shops[i]);
                                sheetlist.Add(MyVar.Shops[i]);
                            }
                            else
                            {
                                sheet = workbook.CreateSheet(MyVar.Shops[i] + "补"+ Guid.NewGuid().ToString().Substring(0, 4).ToUpper());
                            }
                            //第一行
                            IRow row0 = sheet.CreateRow(0);
                            // 合并单元格
                            CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3); // 
                            sheet.AddMergedRegion(region);
                            // 在合并后的单元格中设置值
                            ICell cell = sheet.GetRow(0).GetCell(0);
                            if (cell == null)
                            {
                                cell = sheet.GetRow(0).CreateCell(0);
                            }
                            cell.SetCellValue(MyVar.Shops[i].Replace("店", ""));
                            row0.GetCell(0).CellStyle = bigfontstyle;
                            row0.CreateCell(5).SetCellValue("即食");
                            row0.GetCell(5).CellStyle = bigfontstyle;

                            ///第二行
                            IRow row1 = sheet.CreateRow(1);
                            row1.CreateCell(0).SetCellValue("部门");
                            row1.CreateCell(1).SetCellValue("公司名称");
                            row1.CreateCell(2).SetCellValue("订单号");
                            row1.CreateCell(3).SetCellValue("店号");
                            row1.CreateCell(4).SetCellValue("温区");
                            row1.CreateCell(5).SetCellValue("收货日期");


                            ///设置有背景颜色的样式
                            for (int j = 0; j <= 5; j++)
                            {
                                row1.GetCell(j).CellStyle = normalBackStyle;
                            }
                            ///第三行
                            IRow row2 = sheet.CreateRow(2);
                            row2.CreateCell(0).SetCellValue(MyVar.Dep);
                            row2.CreateCell(1).SetCellValue("江苏逅厨");
                            row2.CreateCell(2).SetCellValue(shoprows[0][0].ToString());
                            row2.CreateCell(3).SetCellValue(MyVar.Shops[i].Replace("店", ""));
                            row2.CreateCell(4).SetCellValue("冷藏");
                            row2.CreateCell(5).SetCellValue(MyVar.saleDate);


                            ///设置第三行的样式
                            row2.GetCell(0).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(1).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(2).CellStyle = normalNoBackStyle;
                            row2.GetCell(3).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(4).CellStyle = normalNoBackBoldStyle;
                            row2.GetCell(5).CellStyle = normalNoBackStyle;

                            //第五行
                            IRow row4 = sheet.CreateRow(4);
                            row4.CreateCell(0).SetCellValue("序号");
                            row4.CreateCell(1).SetCellValue("商品号");
                            row4.CreateCell(2).SetCellValue("商品名称");
                            row4.CreateCell(3).SetCellValue("规格");
                            row4.CreateCell(4).SetCellValue("数量");
                            row4.CreateCell(5).SetCellValue("商品条码");
                            ///设置有背景颜色的样式
                            for (int j = 0; j <= 5; j++)
                            {
                                row4.GetCell(j).CellStyle = normalBackStyle;
                            }
                            //第六行开始
                            for (int m = 0; m < shoprows.Length; m++)
                            {
                                IRow row5 = sheet.CreateRow(m + 5);
                                row5.CreateCell(0).SetCellValue(m + 1);
                                row5.CreateCell(1).SetCellValue(shoprows[m][2].ToString());
                                row5.CreateCell(2).SetCellValue(shoprows[m][3].ToString());
                                row5.CreateCell(3).SetCellValue(shoprows[m][5].ToString() + "g");
                                row5.CreateCell(4).SetCellValue(shoprows[m][6].ToString());
                                row5.CreateCell(5).SetCellValue("");
                                BarcodeFormat format = BarcodeFormat.EAN_13;
                                var barcodeImage = BarcodeHelper.GenerateBarcode(shoprows[m][4].ToString(), format);
                                MemoryStream ms = new MemoryStream();
                                barcodeImage.Save(ms, ImageFormat.Png);
                                int pictureIdx = workbook.AddPicture(ms.ToArray(), PictureType.PNG);
                                IDrawing drawing = sheet.CreateDrawingPatriarch();
                                IClientAnchor anchor = drawing.CreateAnchor(2*9525, 1*9525, 800 * 9525, 80 * 9525, 5, m+5, 5, m+5);
                                //dx1：起始单元格的x偏移量；
                                //dy1：起始单元格的y偏移量；
                                //dx2：终止单元格的x偏移量；
                                //dy2：终止单元格的y偏移量；
                                //col1：起始单元格列序号；
                                //row1：起始单元格行序号；
                                //col2：终止单元格列序号；
                                //row2：终止单元格行序号；
                                IPicture pict = drawing.CreatePicture(anchor, pictureIdx);
                                //pict.Resize(1); // 自动调整图片大小以适应单元格大小
                                row5.Height = 560;
                                shopqty = shopqty + int.Parse(shoprows[m][6].ToString());
                                for (int j = 0; j <= 5; j++)
                                {
                                    row5.GetCell(j).CellStyle = normalNoBackStyle;
                                }
                                
                            }


                            //总数行
                            IRow row6 = sheet.CreateRow(shoprows.Length + 5);
                            row6.CreateCell(3).SetCellValue("箱总数");
                            row6.CreateCell(4).SetCellValue(shopqty);
                            row6.GetCell(3).CellStyle = normalNoBackStyle;
                            row6.GetCell(4).CellStyle = normalNoBackStyle;



                            //设置宽度
                            sheet.SetColumnWidth(0, 1100);
                            sheet.SetColumnWidth(1, 2400);
                            sheet.SetColumnWidth(2, 4800);
                            sheet.SetColumnWidth(3, 2000);
                            sheet.SetColumnWidth(4, 1600);
                            sheet.SetColumnWidth(5, 7000);

                            FileStream file = new FileStream(path + fileExt, FileMode.Create);
                            workbook.Write(file);
                            file.Close();
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Datable导出成Excel正常用
        /// </summary>
        /// <param name="path">文件绝对路径</param>
        /// <param name="dt"></param>
        /// <param name="fileExt">文件后缀</param>
        public static void TableToExcelNomal(string path, DataTable dt, string fileExt)
        {
            IWorkbook workbook = null;
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                //return null;
                return;
            }
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);

            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());

                }
            }
            //设置宽度
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                int length = Encoding.Default.GetBytes(row.GetCell(i).ToString()).Length;
                sheet.SetColumnWidth(i, (length + 2) * 256 * 2);
            }
            //return workbook;

            //生成excel：
            FileStream file = new FileStream(path + fileExt, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }

        /// <summary>
        /// Datable导出成Excel外部客户用
        /// </summary>
        /// <param name="path">文件绝对路径</param>
        /// <param name="dt"></param>
        /// <param name="fileExt">文件后缀</param>
        public static void TableToExcelWB(string path, DataTable dt, string fileExt)
        {
            IWorkbook workbook = null;
            int id = 99999;  //序号
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                return;
            }
            ISheet sheet = workbook.CreateSheet("销售订单#基本信息(FBillHead)");

            //定义单元格背景颜色类型
            ICellStyle s = workbook.CreateCellStyle();
            s.FillForegroundColor = HSSFColor.Red.Index;
            s.FillPattern = FillPattern.SolidForeground;

            //表头第一行 
            IRow row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("FBillHead(SAL_SaleOrder)");
            row.CreateCell(1).SetCellValue("FBillTypeID");
            row.CreateCell(2).SetCellValue("FBillTypeID#Name");
            row.CreateCell(3).SetCellValue("FDate");
            row.CreateCell(4).SetCellValue("FSaleOrgId");
            row.CreateCell(5).SetCellValue("FSaleOrgId#Name");
            row.CreateCell(6).SetCellValue("FCustId");
            row.CreateCell(7).SetCellValue("FCustId#Name");
            row.CreateCell(8).SetCellValue("FSalerId");
            row.CreateCell(9).SetCellValue("FSalerId#Name");
            row.CreateCell(10).SetCellValue("FSaleOrderEntry");
            row.CreateCell(11).SetCellValue("FMaterialId");
            row.CreateCell(12).SetCellValue("FMaterialId#Name");
            row.CreateCell(13).SetCellValue("FUnitID");
            row.CreateCell(14).SetCellValue("FUnitID#Name");
            row.CreateCell(15).SetCellValue("FQty");
            row.CreateCell(16).SetCellValue("FEntryTaxRate");
            row.CreateCell(17).SetCellValue("FDeliveryDate");
            row.CreateCell(18).SetCellValue("FSettleOrgIds");
            row.CreateCell(19).SetCellValue("FSettleOrgIds#Name");
            row.CreateCell(20).SetCellValue("FReserveType");
            row.CreateCell(21).SetCellValue("FOUTLMTUNIT");
            row.CreateCell(22).SetCellValue("*Split*3");
            row.CreateCell(23).SetCellValue("FTaxDetailSubEntity");

            //表头第二行 
            IRow row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("*基本信息(序号)");
            row1.CreateCell(1).SetCellValue("*(基本信息)单据类型#编码");
            row1.CreateCell(2).SetCellValue("(基本信息)单据类型#名称");
            row1.CreateCell(3).SetCellValue("*(基本信息)日期");
            row1.CreateCell(4).SetCellValue("*(基本信息)销售组织#编码");
            row1.CreateCell(5).SetCellValue("(基本信息)销售组织#名称#Name");
            row1.CreateCell(6).SetCellValue("*(基本信息)客户#编码");
            row1.CreateCell(7).SetCellValue("(基本信息)客户#名称#Name");
            row1.CreateCell(8).SetCellValue("*(基本信息)销售员#编码");
            row1.CreateCell(9).SetCellValue("(基本信息)销售员#名称");
            row1.CreateCell(10).SetCellValue("*订单明细(序号)");
            row1.CreateCell(11).SetCellValue("*(订单明细)物料编码#编码");
            row1.CreateCell(12).SetCellValue("(订单明细)物料编码#名称");
            row1.CreateCell(13).SetCellValue("*(订单明细)销售单位#编码");
            row1.CreateCell(14).SetCellValue("(订单明细)销售单位#名称");
            row1.CreateCell(15).SetCellValue("(订单明细)销售数量");
            row1.CreateCell(16).SetCellValue("(订单明细)税率%");
            row1.CreateCell(17).SetCellValue("*(订单明细)要货日期");
            row1.CreateCell(18).SetCellValue("*(订单明细)结算组织#编码");
            row1.CreateCell(19).SetCellValue("(订单明细)结算组织#名称");
            row1.CreateCell(20).SetCellValue("*(订单明细)预留类型");
            row1.CreateCell(21).SetCellValue("*(订单明细)超发控制单位类型");
            row1.CreateCell(22).SetCellValue("间隔列");
            row1.CreateCell(23).SetCellValue("*税务明细(序号)");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row2 = sheet.CreateRow(i + 2);
                if (!dt.Rows[i][0].ToString().Equals(id.ToString()))
                {
                    row2.CreateCell(0).SetCellValue(dt.Rows[i][0].ToString());
                    row2.CreateCell(1).SetCellValue("XSDD01_SYS");
                    row2.CreateCell(2).SetCellValue("标准销售订单");
                    row2.CreateCell(3).SetCellValue(MyVar.saleDate);
                    row2.CreateCell(4).SetCellValue("101");
                    row2.CreateCell(5).SetCellValue("江苏逅厨供应链管理有限公司");
                    row2.CreateCell(6).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? "NULL" : dt.Rows[i][1].ToString());
                    row2.CreateCell(7).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][2].ToString()) ? "NULL" : dt.Rows[i][2].ToString());
                    row2.CreateCell(8).SetCellValue("HC0008");
                    row2.CreateCell(9).SetCellValue("张鹏飞");
                    row2.CreateCell(10).SetCellValue("");
                    row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "NULL" : dt.Rows[i][3].ToString());
                    row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "NULL" : dt.Rows[i][4].ToString());
                    row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                    row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                    row2.CreateCell(15).SetCellValue(dt.Rows[i][7].ToString());
                    row2.CreateCell(16).SetCellValue("13");
                    row2.CreateCell(17).SetCellValue(MyVar.wantDate);
                    row2.CreateCell(18).SetCellValue("101");
                    row2.CreateCell(19).SetCellValue("江苏逅厨供应链管理有限公司");
                    row2.CreateCell(20).SetCellValue("");
                    row2.CreateCell(21).SetCellValue("");
                    row2.CreateCell(22).SetCellValue("");
                    row2.CreateCell(23).SetCellValue("");
                    id++;
                }
                else
                {
                    row2.CreateCell(0).SetCellValue("");
                    row2.CreateCell(1).SetCellValue("");
                    row2.CreateCell(2).SetCellValue("");
                    row2.CreateCell(3).SetCellValue("");
                    row2.CreateCell(4).SetCellValue("");
                    row2.CreateCell(5).SetCellValue("");
                    row2.CreateCell(6).SetCellValue("");
                    row2.CreateCell(7).SetCellValue("");
                    row2.CreateCell(8).SetCellValue("");
                    row2.CreateCell(9).SetCellValue("");
                    row2.CreateCell(10).SetCellValue("");
                    row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "NULL" : dt.Rows[i][3].ToString());
                    row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "NULL" : dt.Rows[i][4].ToString());
                    row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                    row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                    row2.CreateCell(15).SetCellValue(dt.Rows[i][7].ToString());
                    row2.CreateCell(16).SetCellValue("13");
                    row2.CreateCell(17).SetCellValue(MyVar.wantDate);
                    row2.CreateCell(18).SetCellValue("101");
                    row2.CreateCell(19).SetCellValue("江苏逅厨供应链管理有限公司");
                    row2.CreateCell(20).SetCellValue("");
                    row2.CreateCell(21).SetCellValue("");
                    row2.CreateCell(22).SetCellValue("");
                    row2.CreateCell(23).SetCellValue("");
                }

            }


            //设置宽度
            for (int i = 0; i < 22; i++)
            {
                int length = Encoding.Default.GetBytes(row.GetCell(i).ToString()).Length;
                sheet.SetColumnWidth(i, (length + 2) * 256);
            }
            //return workbook;

            //添加单元格背景颜色
            row1.GetCell(3).CellStyle = s;
            row1.GetCell(17).CellStyle = s;



            //生成excel：
            FileStream file = new FileStream(path + fileExt, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }

        /// <summary>
        /// Datable导出成Excel叮咚客户用
        /// </summary>
        /// <param name="path">文件绝对路径</param>
        /// <param name="dt"></param>
        /// <param name="fileExt">文件后缀</param>
        public static void TableToExcelDingDong(string path, DataTable dt, string fileExt)
        {
            IWorkbook workbook = null;
            int id = 99999;  //序号
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                return;
            }
            ISheet sheet = workbook.CreateSheet("销售订单#基本信息(FBillHead)");

            //定义单元格背景颜色类型
            ICellStyle s = workbook.CreateCellStyle();
            s.FillForegroundColor = HSSFColor.Red.Index;
            s.FillPattern = FillPattern.SolidForeground;

            //表头第一行 
            IRow row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("FBillHead(SAL_SaleOrder)");
            row.CreateCell(1).SetCellValue("FBillTypeID");
            row.CreateCell(2).SetCellValue("FBillTypeID#Name");
            row.CreateCell(3).SetCellValue("FDate");
            row.CreateCell(4).SetCellValue("FSaleOrgId");
            row.CreateCell(5).SetCellValue("FSaleOrgId#Name");
            row.CreateCell(6).SetCellValue("FCustId");
            row.CreateCell(7).SetCellValue("FCustId#Name");
            row.CreateCell(8).SetCellValue("FSalerId");
            row.CreateCell(9).SetCellValue("FSalerId#Name");
            row.CreateCell(10).SetCellValue("FSaleOrderEntry");
            row.CreateCell(11).SetCellValue("FMaterialId");
            row.CreateCell(12).SetCellValue("FMaterialId#Name");
            row.CreateCell(13).SetCellValue("FUnitID");
            row.CreateCell(14).SetCellValue("FUnitID#Name");
            row.CreateCell(15).SetCellValue("FQty");
            row.CreateCell(16).SetCellValue("FTaxPrice");
            row.CreateCell(17).SetCellValue("FEntryTaxRate");
            row.CreateCell(18).SetCellValue("FDeliveryDate");
            row.CreateCell(19).SetCellValue("FSettleOrgIds");
            row.CreateCell(20).SetCellValue("FSettleOrgIds#Name");
            row.CreateCell(21).SetCellValue("FReserveType");
            row.CreateCell(22).SetCellValue("FOUTLMTUNIT");
            row.CreateCell(23).SetCellValue("*Split*3");
            row.CreateCell(24).SetCellValue("FTaxDetailSubEntity");

            //表头第二行 
            IRow row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("*基本信息(序号)");
            row1.CreateCell(1).SetCellValue("*(基本信息)单据类型#编码");
            row1.CreateCell(2).SetCellValue("(基本信息)单据类型#名称");
            row1.CreateCell(3).SetCellValue("*(基本信息)日期");
            row1.CreateCell(4).SetCellValue("*(基本信息)销售组织#编码");
            row1.CreateCell(5).SetCellValue("(基本信息)销售组织#名称#Name");
            row1.CreateCell(6).SetCellValue("*(基本信息)客户#编码");
            row1.CreateCell(7).SetCellValue("(基本信息)客户#名称#Name");
            row1.CreateCell(8).SetCellValue("*(基本信息)销售员#编码");
            row1.CreateCell(9).SetCellValue("(基本信息)销售员#名称");
            row1.CreateCell(10).SetCellValue("*订单明细(序号)");
            row1.CreateCell(11).SetCellValue("*(订单明细)物料编码#编码");
            row1.CreateCell(12).SetCellValue("(订单明细)物料编码#名称");
            row1.CreateCell(13).SetCellValue("*(订单明细)销售单位#编码");
            row1.CreateCell(14).SetCellValue("(订单明细)销售单位#名称");
            row1.CreateCell(15).SetCellValue("(订单明细)销售数量");
            row1.CreateCell(16).SetCellValue("(订单明细)含税单价");
            row1.CreateCell(17).SetCellValue("(订单明细)税率%");
            row1.CreateCell(18).SetCellValue("*(订单明细)要货日期");
            row1.CreateCell(19).SetCellValue("*(订单明细)结算组织#编码");
            row1.CreateCell(20).SetCellValue("(订单明细)结算组织#名称");
            row1.CreateCell(21).SetCellValue("*(订单明细)预留类型");
            row1.CreateCell(22).SetCellValue("*(订单明细)超发控制单位类型");
            row1.CreateCell(23).SetCellValue("间隔列");
            row1.CreateCell(24).SetCellValue("*税务明细(序号)");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row2 = sheet.CreateRow(i + 2);
                if (!dt.Rows[i][0].ToString().Equals(id.ToString()))
                {
                    row2.CreateCell(0).SetCellValue(dt.Rows[i][0].ToString());
                    row2.CreateCell(1).SetCellValue("XSDD01_SYS");
                    row2.CreateCell(2).SetCellValue("标准销售订单");
                    row2.CreateCell(3).SetCellValue(dt.Rows[i][9].ToString());
                    row2.CreateCell(4).SetCellValue("104");
                    row2.CreateCell(5).SetCellValue("江苏好利森食品科技有限公司");
                    row2.CreateCell(6).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? "NULL" : dt.Rows[i][1].ToString());
                    row2.CreateCell(7).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][2].ToString()) ? "NULL" : dt.Rows[i][2].ToString());
                    row2.CreateCell(8).SetCellValue("");
                    row2.CreateCell(9).SetCellValue("");
                    row2.CreateCell(10).SetCellValue("");
                    row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "NULL" : dt.Rows[i][3].ToString());
                    row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "NULL" : dt.Rows[i][4].ToString());
                    row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                    row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                    row2.CreateCell(15).SetCellValue(dt.Rows[i][7].ToString());
                    row2.CreateCell(16).SetCellValue(dt.Rows[i][8].ToString());
                    row2.CreateCell(17).SetCellValue("13");
                    row2.CreateCell(18).SetCellValue(dt.Rows[i][9].ToString());
                    row2.CreateCell(19).SetCellValue("104");
                    row2.CreateCell(20).SetCellValue("江苏好利森食品科技有限公司");
                    row2.CreateCell(21).SetCellValue("");
                    row2.CreateCell(22).SetCellValue("");
                    row2.CreateCell(23).SetCellValue("");
                    row2.CreateCell(24).SetCellValue("");
                    id++;
                }
                else
                {
                    row2.CreateCell(0).SetCellValue("");
                    row2.CreateCell(1).SetCellValue("");
                    row2.CreateCell(2).SetCellValue("");
                    row2.CreateCell(3).SetCellValue("");
                    row2.CreateCell(4).SetCellValue("");
                    row2.CreateCell(5).SetCellValue("");
                    row2.CreateCell(6).SetCellValue("");
                    row2.CreateCell(7).SetCellValue("");
                    row2.CreateCell(8).SetCellValue("");
                    row2.CreateCell(9).SetCellValue("");
                    row2.CreateCell(10).SetCellValue("");
                    row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "NULL" : dt.Rows[i][3].ToString());
                    row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "NULL" : dt.Rows[i][4].ToString());
                    row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                    row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                    row2.CreateCell(15).SetCellValue(dt.Rows[i][7].ToString());
                    row2.CreateCell(16).SetCellValue(dt.Rows[i][8].ToString());
                    row2.CreateCell(17).SetCellValue("13");
                    row2.CreateCell(18).SetCellValue(dt.Rows[i][9].ToString());
                    row2.CreateCell(19).SetCellValue("104");
                    row2.CreateCell(20).SetCellValue("江苏好利森食品科技有限公司");
                    row2.CreateCell(21).SetCellValue("");
                    row2.CreateCell(22).SetCellValue("");
                    row2.CreateCell(23).SetCellValue("");
                    row2.CreateCell(24).SetCellValue("");
                }

            }


            //设置宽度
            for (int i = 0; i < 22; i++)
            {
                int length = Encoding.Default.GetBytes(row.GetCell(i).ToString()).Length;
                sheet.SetColumnWidth(i, (length + 2) * 256);
            }
            //return workbook;

            //添加单元格背景颜色
            row1.GetCell(3).CellStyle = s;
            row1.GetCell(18).CellStyle = s;



            //生成excel：
            FileStream file = new FileStream(path + fileExt, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }

        /// <summary>
        /// Datable导出成Excel学校客户用
        /// </summary>
        /// <param name="path">文件绝对路径</param>
        /// <param name="dt"></param>
        /// <param name="fileExt">文件后缀</param>
        public static void TableToExcelSchool(string path, DataTable dt, string fileExt)
        {
            string oldcuscode="";//客户编码，用于判断是否需要新的编号
            string olddate = "";//订单日期
            IWorkbook workbook = null;
            int id = 100000;  //序号
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                return;
            }
            ISheet sheet = workbook.CreateSheet("销售订单#基本信息(FBillHead)");

            //定义单元格背景颜色类型
            ICellStyle s = workbook.CreateCellStyle();
            s.FillForegroundColor = HSSFColor.Red.Index;
            s.FillPattern = FillPattern.SolidForeground;

            //表头第一行 
            IRow row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("FBillHead(SAL_SaleOrder)");
            row.CreateCell(1).SetCellValue("FBillTypeID");
            row.CreateCell(2).SetCellValue("FBillTypeID#Name");
            row.CreateCell(3).SetCellValue("FDate");
            row.CreateCell(4).SetCellValue("FSaleOrgId");
            row.CreateCell(5).SetCellValue("FSaleOrgId#Name");
            row.CreateCell(6).SetCellValue("FCustId");
            row.CreateCell(7).SetCellValue("FCustId#Name");
            row.CreateCell(8).SetCellValue("FSaleOrderEntry");
            row.CreateCell(9).SetCellValue("FMaterialId");
            row.CreateCell(10).SetCellValue("FMaterialId#Name");
            row.CreateCell(11).SetCellValue("FUnitID");
            row.CreateCell(12).SetCellValue("FUnitID#Name");
            row.CreateCell(13).SetCellValue("FQty");
            row.CreateCell(14).SetCellValue("FEntryTaxRate");
            row.CreateCell(15).SetCellValue("FDeliveryDate");
            row.CreateCell(16).SetCellValue("FSettleOrgIds");
            row.CreateCell(17).SetCellValue("FReserveType");
            row.CreateCell(18).SetCellValue("FOUTLMTUNIT");
            row.CreateCell(19).SetCellValue("F_PAEZ_ISCWC");
            row.CreateCell(20).SetCellValue("F_PAEZ_ISCWC#Name");
            row.CreateCell(21).SetCellValue("*Split*3");
            row.CreateCell(22).SetCellValue("FTaxDetailSubEntity");

            //表头第二行 
            IRow row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("*基本信息(序号)");
            row1.CreateCell(1).SetCellValue("*(基本信息)单据类型#编码");
            row1.CreateCell(2).SetCellValue("(基本信息)单据类型#名称");
            row1.CreateCell(3).SetCellValue("*(基本信息)日期");
            row1.CreateCell(4).SetCellValue("*(基本信息)销售组织#编码");
            row1.CreateCell(5).SetCellValue("(基本信息)销售组织#名称#Name");
            row1.CreateCell(6).SetCellValue("*(基本信息)客户#编码");
            row1.CreateCell(7).SetCellValue("(基本信息)客户#名称#Name");
            row1.CreateCell(8).SetCellValue("*订单明细(序号)");
            row1.CreateCell(9).SetCellValue("*(订单明细)物料编码#编码");
            row1.CreateCell(10).SetCellValue("(订单明细)物料编码#名称");
            row1.CreateCell(11).SetCellValue("*(订单明细)销售单位#编码");
            row1.CreateCell(12).SetCellValue("(订单明细)销售单位#名称");
            row1.CreateCell(13).SetCellValue("(订单明细)销售数量");
            row1.CreateCell(14).SetCellValue("(订单明细)税率%");
            row1.CreateCell(15).SetCellValue("*(订单明细)要货日期");
            row1.CreateCell(16).SetCellValue("*(订单明细)结算组织#编码");
            row1.CreateCell(17).SetCellValue("*(订单明细)预留类型");
            row1.CreateCell(18).SetCellValue("*(订单明细)超发控制单位类型");
            row1.CreateCell(19).SetCellValue("(订单明细)需求分类#编码");
            row1.CreateCell(20).SetCellValue("(订单明细)需求分类#名称");
            row1.CreateCell(21).SetCellValue("间隔列");
            row1.CreateCell(22).SetCellValue("*税务明细(序号)");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row2 = sheet.CreateRow(i + 2);
                if (!dt.Rows[i][2].ToString().Equals(oldcuscode)||!dt.Rows[i][1].ToString().Equals(olddate))
                {
                    oldcuscode = dt.Rows[i][2].ToString();//客户编码
                    olddate= dt.Rows[i][1].ToString();//订单日期
                    row2.CreateCell(0).SetCellValue(id);
                    row2.CreateCell(1).SetCellValue("XSDD01_SYS");
                    row2.CreateCell(2).SetCellValue("标准销售订单");
                    row2.CreateCell(3).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? "NULL" : dt.Rows[i][1].ToString());
                    row2.CreateCell(4).SetCellValue("109");
                    row2.CreateCell(5).SetCellValue("逅厨加工中心");
                    row2.CreateCell(6).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][2].ToString()) ? "NULL" : dt.Rows[i][2].ToString());
                    row2.CreateCell(7).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "NULL" : dt.Rows[i][3].ToString());
                    row2.CreateCell(8).SetCellValue("");
                    row2.CreateCell(9).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "NULL" : dt.Rows[i][4].ToString());
                    row2.CreateCell(10).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                    row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                    row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][7].ToString()) ? "NULL" : dt.Rows[i][7].ToString());
                    row2.CreateCell(13).SetCellValue(dt.Rows[i][8].ToString());
                    row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][11].ToString()) ? "NULL" : dt.Rows[i][11].ToString());
                    row2.CreateCell(15).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? "NULL" : dt.Rows[i][1].ToString());
                    row2.CreateCell(16).SetCellValue("109");
                    row2.CreateCell(17).SetCellValue("");
                    row2.CreateCell(18).SetCellValue("");
                    row2.CreateCell(19).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][9].ToString()) ? "NULL" : dt.Rows[i][9].ToString());
                    row2.CreateCell(20).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][10].ToString()) ? "NULL" : dt.Rows[i][10].ToString());
                    row2.CreateCell(21).SetCellValue("");
                    row2.CreateCell(22).SetCellValue("");
                    id++;
                }
                else
                {
                    row2.CreateCell(0).SetCellValue("");
                    row2.CreateCell(1).SetCellValue("");
                    row2.CreateCell(2).SetCellValue("");
                    row2.CreateCell(3).SetCellValue("");
                    row2.CreateCell(4).SetCellValue("");
                    row2.CreateCell(5).SetCellValue("");
                    row2.CreateCell(6).SetCellValue("");
                    row2.CreateCell(7).SetCellValue("");
                    row2.CreateCell(8).SetCellValue("");
                    row2.CreateCell(9).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "NULL" : dt.Rows[i][4].ToString());
                    row2.CreateCell(10).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                    row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                    row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][7].ToString()) ? "NULL" : dt.Rows[i][7].ToString());
                    row2.CreateCell(13).SetCellValue(dt.Rows[i][8].ToString());
                    row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][11].ToString()) ? "NULL" : dt.Rows[i][11].ToString());
                    row2.CreateCell(15).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? "NULL" : dt.Rows[i][1].ToString());
                    row2.CreateCell(16).SetCellValue("109");
                    row2.CreateCell(17).SetCellValue("");
                    row2.CreateCell(18).SetCellValue("");
                    row2.CreateCell(19).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][9].ToString()) ? "NULL" : dt.Rows[i][9].ToString());
                    row2.CreateCell(20).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][10].ToString()) ? "NULL" : dt.Rows[i][10].ToString());
                    row2.CreateCell(21).SetCellValue("");
                    row2.CreateCell(22).SetCellValue("");
                }

            }


            //设置宽度
            for (int i = 0; i < 22; i++)
            {
                int length = Encoding.Default.GetBytes(row.GetCell(i).ToString()).Length;
                sheet.SetColumnWidth(i, (length + 2) * 256);
            }
            //return workbook;

            //添加单元格背景颜色
            //row1.GetCell(3).CellStyle = s;
            //row1.GetCell(17).CellStyle = s;



            //生成excel：
            FileStream file = new FileStream(path + fileExt, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }

        /// <summary>
        /// Datable导出成Excel外部客户用好利森
        /// </summary>
        /// <param name="path">文件绝对路径</param>
        /// <param name="dt"></param>
        /// <param name="fileExt">文件后缀</param>
        public static void TableToExcelWBHLS(string path, DataTable dt, string fileExt)
        {
            IWorkbook workbook = null;
            int id = 99999;  //序号
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                return;
            }
            ISheet sheet = workbook.CreateSheet("销售订单#基本信息(FBillHead)");

            //定义单元格背景颜色类型
            ICellStyle s = workbook.CreateCellStyle();
            s.FillForegroundColor = HSSFColor.Red.Index;
            s.FillPattern = FillPattern.SolidForeground;

            //表头第一行 
            IRow row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("FBillHead(SAL_SaleOrder)");
            row.CreateCell(1).SetCellValue("FBillTypeID");
            row.CreateCell(2).SetCellValue("FBillTypeID#Name");
            row.CreateCell(3).SetCellValue("FDate");
            row.CreateCell(4).SetCellValue("FSaleOrgId");
            row.CreateCell(5).SetCellValue("FSaleOrgId#Name");
            row.CreateCell(6).SetCellValue("FCustId");
            row.CreateCell(7).SetCellValue("FCustId#Name");
            row.CreateCell(8).SetCellValue("FSalerId");
            row.CreateCell(9).SetCellValue("FSalerId#Name");
            row.CreateCell(10).SetCellValue("FSaleOrderEntry");
            row.CreateCell(11).SetCellValue("FMaterialId");
            row.CreateCell(12).SetCellValue("FMaterialId#Name");
            row.CreateCell(13).SetCellValue("FUnitID");
            row.CreateCell(14).SetCellValue("FUnitID#Name");
            row.CreateCell(15).SetCellValue("FQty");
            row.CreateCell(16).SetCellValue("FEntryTaxRate");
            row.CreateCell(17).SetCellValue("FDeliveryDate");
            row.CreateCell(18).SetCellValue("FSettleOrgIds");
            row.CreateCell(19).SetCellValue("FSettleOrgIds#Name");
            row.CreateCell(20).SetCellValue("FReserveType");
            row.CreateCell(21).SetCellValue("FOUTLMTUNIT");
            row.CreateCell(22).SetCellValue("*Split*3");
            row.CreateCell(23).SetCellValue("FTaxDetailSubEntity");

            //表头第二行 
            IRow row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("*基本信息(序号)");
            row1.CreateCell(1).SetCellValue("*(基本信息)单据类型#编码");
            row1.CreateCell(2).SetCellValue("(基本信息)单据类型#名称");
            row1.CreateCell(3).SetCellValue("*(基本信息)日期");
            row1.CreateCell(4).SetCellValue("*(基本信息)销售组织#编码");
            row1.CreateCell(5).SetCellValue("(基本信息)销售组织#名称#Name");
            row1.CreateCell(6).SetCellValue("*(基本信息)客户#编码");
            row1.CreateCell(7).SetCellValue("(基本信息)客户#名称#Name");
            row1.CreateCell(8).SetCellValue("*(基本信息)销售员#编码");
            row1.CreateCell(9).SetCellValue("(基本信息)销售员#名称");
            row1.CreateCell(10).SetCellValue("*订单明细(序号)");
            row1.CreateCell(11).SetCellValue("*(订单明细)物料编码#编码");
            row1.CreateCell(12).SetCellValue("(订单明细)物料编码#名称");
            row1.CreateCell(13).SetCellValue("*(订单明细)销售单位#编码");
            row1.CreateCell(14).SetCellValue("(订单明细)销售单位#名称");
            row1.CreateCell(15).SetCellValue("(订单明细)销售数量");
            row1.CreateCell(16).SetCellValue("(订单明细)税率%");
            row1.CreateCell(17).SetCellValue("*(订单明细)要货日期");
            row1.CreateCell(18).SetCellValue("*(订单明细)结算组织#编码");
            row1.CreateCell(19).SetCellValue("(订单明细)结算组织#名称");
            row1.CreateCell(20).SetCellValue("*(订单明细)预留类型");
            row1.CreateCell(21).SetCellValue("*(订单明细)超发控制单位类型");
            row1.CreateCell(22).SetCellValue("间隔列");
            row1.CreateCell(23).SetCellValue("*税务明细(序号)");


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row2 = sheet.CreateRow(i + 2);
                if (!dt.Rows[i][0].ToString().Equals(id.ToString()))
                {
                    row2.CreateCell(0).SetCellValue(dt.Rows[i][0].ToString());
                    row2.CreateCell(1).SetCellValue("XSDD01_SYS");
                    row2.CreateCell(2).SetCellValue("标准销售订单");
                    row2.CreateCell(3).SetCellValue(MyVar.saleDate);
                    row2.CreateCell(4).SetCellValue(MyVar.zzcode);
                    row2.CreateCell(5).SetCellValue(MyVar.zzname);
                    row2.CreateCell(6).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? "NULL" : dt.Rows[i][1].ToString());
                    row2.CreateCell(7).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][2].ToString()) ? "NULL" : dt.Rows[i][2].ToString());
                    row2.CreateCell(8).SetCellValue("");
                    row2.CreateCell(9).SetCellValue("");
                    row2.CreateCell(10).SetCellValue("");
                    row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "NULL" : dt.Rows[i][3].ToString());
                    row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "NULL" : dt.Rows[i][4].ToString());
                    row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                    row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                    row2.CreateCell(15).SetCellValue(dt.Rows[i][7].ToString());
                    row2.CreateCell(16).SetCellValue("13");
                    row2.CreateCell(17).SetCellValue(MyVar.wantDate);
                    row2.CreateCell(18).SetCellValue(MyVar.zzcode);
                    row2.CreateCell(19).SetCellValue(MyVar.zzname);
                    row2.CreateCell(20).SetCellValue("");
                    row2.CreateCell(21).SetCellValue("");
                    row2.CreateCell(22).SetCellValue("");
                    row2.CreateCell(23).SetCellValue("");
                    id++;
                }
                else
                {
                    row2.CreateCell(0).SetCellValue("");
                    row2.CreateCell(1).SetCellValue("");
                    row2.CreateCell(2).SetCellValue("");
                    row2.CreateCell(3).SetCellValue("");
                    row2.CreateCell(4).SetCellValue("");
                    row2.CreateCell(5).SetCellValue("");
                    row2.CreateCell(6).SetCellValue("");
                    row2.CreateCell(7).SetCellValue("");
                    row2.CreateCell(8).SetCellValue("");
                    row2.CreateCell(9).SetCellValue("");
                    row2.CreateCell(10).SetCellValue("");
                    row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "NULL" : dt.Rows[i][3].ToString());
                    row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "NULL" : dt.Rows[i][4].ToString());
                    row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                    row2.CreateCell(14).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                    row2.CreateCell(15).SetCellValue(dt.Rows[i][7].ToString());
                    row2.CreateCell(16).SetCellValue("13");
                    row2.CreateCell(17).SetCellValue(MyVar.wantDate);
                    row2.CreateCell(18).SetCellValue(MyVar.zzcode);
                    row2.CreateCell(19).SetCellValue(MyVar.zzname);
                    row2.CreateCell(20).SetCellValue("");
                    row2.CreateCell(21).SetCellValue("");
                    row2.CreateCell(22).SetCellValue("");
                    row2.CreateCell(23).SetCellValue("");
                }

            }


            //设置宽度
            for (int i = 0; i < 22; i++)
            {
                int length = Encoding.Default.GetBytes(row.GetCell(i).ToString()).Length;
                sheet.SetColumnWidth(i, (length + 2) * 256);
            }
            //return workbook;

            //添加单元格背景颜色
            row1.GetCell(3).CellStyle = s;
            row1.GetCell(17).CellStyle = s;



            //生成excel：
            FileStream file = new FileStream(path + fileExt, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }

        /// <summary>
        /// Datable导出成Excel导出一阶BOM导入模板用
        /// </summary>
        /// <param name="path">文件绝对路径</param>
        /// <param name="dt"></param>
        /// <param name="fileExt">文件后缀</param>
        public static void TableToExcelBOM(string path, DataTable dt, string fileExt)
        {
            IWorkbook workbook = null;
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            if (workbook == null)
            {
                //return null;
                return;
            }
            ISheet sheet = workbook.CreateSheet("物料清单#单据头(FBillHead)");

            //定义单元格背景颜色类型
            ICellStyle s = workbook.CreateCellStyle();
            s.FillForegroundColor = HSSFColor.Red.Index;
            s.FillPattern = FillPattern.SolidForeground;

            //表头第一行 
            IRow row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("FBillHead(ENG_BOM)");
            row.CreateCell(1).SetCellValue("FCreateOrgId");
            row.CreateCell(2).SetCellValue("FCreateOrgId#Name");
            row.CreateCell(3).SetCellValue("FUseOrgId");
            row.CreateCell(4).SetCellValue("FUseOrgId#Name");
            row.CreateCell(5).SetCellValue("FName#2052");
            row.CreateCell(6).SetCellValue("FBILLTYPE");
            row.CreateCell(7).SetCellValue("FBILLTYPE#Name");
            row.CreateCell(8).SetCellValue("FBOMCATEGORY");
            row.CreateCell(9).SetCellValue("FBOMUSE");
            row.CreateCell(10).SetCellValue("FMATERIALID");
            row.CreateCell(11).SetCellValue("FMATERIALID#Name");
            row.CreateCell(12).SetCellValue("FUNITID");
            row.CreateCell(13).SetCellValue("FUNITID#Name");
            row.CreateCell(14).SetCellValue("*Split*1");
            row.CreateCell(15).SetCellValue("FTreeEntity");
            row.CreateCell(16).SetCellValue("FMATERIALIDCHILD");
            row.CreateCell(17).SetCellValue("FMATERIALIDCHILD#Name");
            row.CreateCell(18).SetCellValue("FMATERIALTYPE");
            row.CreateCell(19).SetCellValue("FCHILDUNITID");
            row.CreateCell(20).SetCellValue("FCHILDUNITID#Name");
            row.CreateCell(21).SetCellValue("FDOSAGETYPE");
            row.CreateCell(22).SetCellValue("FNUMERATOR");
            row.CreateCell(23).SetCellValue("FDENOMINATOR");
            row.CreateCell(24).SetCellValue("FOverControlMode");
            row.CreateCell(25).SetCellValue("FEntrySource");
            row.CreateCell(26).SetCellValue("FEFFECTDATE");
            row.CreateCell(27).SetCellValue("FEXPIREDATE");
            row.CreateCell(28).SetCellValue("FISSUETYPE");
            row.CreateCell(29).SetCellValue("FTIMEUNIT");
            row.CreateCell(30).SetCellValue("FOWNERTYPEID");
            //表头第二行 
            IRow row1 = sheet.CreateRow(1);
            row1.CreateCell(0).SetCellValue("*单据头(序号)");
            row1.CreateCell(1).SetCellValue("*(单据头)创建组织#编码");
            row1.CreateCell(2).SetCellValue("(单据头)创建组织#名称");
            row1.CreateCell(3).SetCellValue("*(单据头)使用组织#编码");
            row1.CreateCell(4).SetCellValue("(单据头)使用组织#名称");
            row1.CreateCell(5).SetCellValue("*(单据头)BOM简称#中文(简体/中国)");
            row1.CreateCell(6).SetCellValue("*(单据头)单据类型#编码");
            row1.CreateCell(7).SetCellValue("(单据头)单据类型#名称");
            row1.CreateCell(8).SetCellValue("*(单据头)BOM分类");
            row1.CreateCell(9).SetCellValue("*(单据头)BOM用途");
            row1.CreateCell(10).SetCellValue("*(单据头)父项物料编码#编码");
            row1.CreateCell(11).SetCellValue("(单据头)父项物料编码#名称");
            row1.CreateCell(12).SetCellValue("*(单据头)父项物料单位#编码");
            row1.CreateCell(13).SetCellValue("(单据头)父项物料单位#名称");
            row1.CreateCell(14).SetCellValue("间隔列");
            row1.CreateCell(15).SetCellValue("*子项明细(序号)");
            row1.CreateCell(16).SetCellValue("*(子项明细)子项物料编码#编码");
            row1.CreateCell(17).SetCellValue("(子项明细)子项物料编码#名称");
            row1.CreateCell(18).SetCellValue("*(子项明细)子项类型");
            row1.CreateCell(19).SetCellValue("*(子项明细)子项单位#编码");
            row1.CreateCell(20).SetCellValue("(子项明细)子项单位#名称");
            row1.CreateCell(21).SetCellValue("*(子项明细)用量类型");
            row1.CreateCell(22).SetCellValue("(子项明细)用量:分子");
            row1.CreateCell(23).SetCellValue("(子项明细)用量:分母");
            row1.CreateCell(24).SetCellValue("*(子项明细)超发控制方式");
            row1.CreateCell(25).SetCellValue("*(子项明细)子项来源");
            row1.CreateCell(26).SetCellValue("*(子项明细)生效日期");
            row1.CreateCell(27).SetCellValue("*(子项明细)失效日期");
            row1.CreateCell(28).SetCellValue("*(子项明细)发料方式");
            row1.CreateCell(29).SetCellValue("*(子项明细)时间单位");
            row1.CreateCell(30).SetCellValue("*(子项明细)货主类型");


            //数据第一行
            IRow row2 = sheet.CreateRow(2);
            row2.CreateCell(0).SetCellValue("319290");
            row2.CreateCell(1).SetCellValue("100");
            row2.CreateCell(2).SetCellValue("华联集团");
            row2.CreateCell(3).SetCellValue("100");
            row2.CreateCell(4).SetCellValue("华联集团");
            row2.CreateCell(5).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][2].ToString()) ? "NULL" : dt.Rows[0][2].ToString());
            row2.CreateCell(6).SetCellValue("WLQD01_SYS");
            row2.CreateCell(7).SetCellValue("物料清单");
            row2.CreateCell(8).SetCellValue("标准BOM");
            row2.CreateCell(9).SetCellValue("通用");
            row2.CreateCell(10).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][1].ToString()) ? "NULL" : dt.Rows[0][1].ToString());
            row2.CreateCell(11).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][2].ToString()) ? "NULL" : dt.Rows[0][2].ToString());
            row2.CreateCell(12).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][3].ToString()) ? "NULL" : dt.Rows[0][3].ToString());
            row2.CreateCell(13).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][4].ToString()) ? "NULL" : dt.Rows[0][4].ToString());
            row2.CreateCell(14).SetCellValue("");
            row2.CreateCell(15).SetCellValue("");
            row2.CreateCell(16).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][5].ToString()) ? "NULL" : dt.Rows[0][5].ToString());
            row2.CreateCell(17).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][6].ToString()) ? "NULL" : dt.Rows[0][6].ToString());
            row2.CreateCell(18).SetCellValue("标准件");
            row2.CreateCell(19).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][8].ToString()) ? "NULL" : dt.Rows[0][8].ToString());
            row2.CreateCell(20).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][9].ToString()) ? "NULL" : dt.Rows[0][9].ToString());
            row2.CreateCell(21).SetCellValue("变动");
            row2.CreateCell(22).SetCellValue(string.IsNullOrEmpty(dt.Rows[0][7].ToString()) ? "NULL" : dt.Rows[0][7].ToString());
            row2.CreateCell(23).SetCellValue(1);
            row2.CreateCell(24).SetCellValue("最小发料批量");
            row2.CreateCell(25).SetCellValue("");
            row2.CreateCell(26).SetCellValue("2021-07-01");
            row2.CreateCell(27).SetCellValue("9999-12-31");
            row2.CreateCell(28).SetCellValue("直接领料");
            row2.CreateCell(29).SetCellValue("天");
            row2.CreateCell(30).SetCellValue("业务组织");


            //数据二行至结束  
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                IRow row3 = sheet.CreateRow(i + 2);
                row3.CreateCell(16).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "NULL" : dt.Rows[i][5].ToString());
                row3.CreateCell(17).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? "NULL" : dt.Rows[i][6].ToString());
                row3.CreateCell(18).SetCellValue("标准件");
                row3.CreateCell(19).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][8].ToString()) ? "NULL" : dt.Rows[i][8].ToString());
                row3.CreateCell(20).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][9].ToString()) ? "NULL" : dt.Rows[i][9].ToString());
                row3.CreateCell(21).SetCellValue("变动");
                row3.CreateCell(22).SetCellValue(string.IsNullOrEmpty(dt.Rows[i][7].ToString()) ? "NULL" : dt.Rows[i][7].ToString());
                row3.CreateCell(23).SetCellValue(1);
                row3.CreateCell(24).SetCellValue("最小发料批量");
                row3.CreateCell(25).SetCellValue("");
                row3.CreateCell(26).SetCellValue("2021-07-01");
                row3.CreateCell(27).SetCellValue("9999-12-31");
                row3.CreateCell(28).SetCellValue("直接领料");
                row3.CreateCell(29).SetCellValue("天");
                row3.CreateCell(30).SetCellValue("业务组织");
            }

            //设置宽度
            for (int i = 0; i < 31; i++)
            {
                int length = Encoding.Default.GetBytes(row.GetCell(i).ToString()).Length;
                sheet.SetColumnWidth(i, (length + 2) * 256);
            }
            //return workbook;

            //添加单元格背景颜色
            row1.GetCell(5).CellStyle = s;
            row1.GetCell(10).CellStyle = s;



            //生成excel：
            FileStream file = new FileStream(path + fileExt, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }


        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">文件的绝对路径+文件名</param>
        public static void TableToExcel(DataTable dt, string file)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
            if (workbook == null) { return; }
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
        }


        /// <summary>
        /// Excel导入成Datable
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string file)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);


                //第一行为空，报错
                if (string.IsNullOrEmpty(sheet.GetRow(sheet.FirstRowNum).FirstCellNum.ToString()))
                {
                    MessageBox.Show("文档内容不符合要求，请检查！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return dt;
                }
                //表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据  
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }


        /// <summary>
        /// 外部客户Excel导入成Datable
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableWB(string[] files)
        {
            DataTable dt = new DataTable();
            int id;
            String cusName = "";
            IWorkbook workbook;
            //表头  
            if (dt.Columns["序号"] == null)
            {
                dt.Columns.Add(new DataColumn("序号"));
                dt.Columns.Add(new DataColumn("客户"));
                dt.Columns.Add(new DataColumn("货号"));
                dt.Columns.Add(new DataColumn("名称"));
                dt.Columns.Add(new DataColumn("数量"));
            }
            List<int> columns = new List<int> { 0, 1, 2, 3, 4 };
            id = 100000;
            foreach (string file in files)
            {
                string fileExt = Path.GetExtension(file).ToLower();
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                    if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                    if (workbook == null) { return null; }
                    ISheet sheet = workbook.GetSheetAt(0);
                    cusName = sheet.SheetName;//获取外部客户名称

                    string sql = @"select * from AIShlhcsvr.dbo.T_BD_CUSTOMER_L where fname=@cusname";
                    DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("cusname", cusName));
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        MessageBox.Show("文档'" + System.IO.Path.GetFileNameWithoutExtension(file) + "'客户名称错误，请检查");
                        dt.Rows.Clear();
                        break;
                    }

                    if (string.IsNullOrEmpty(sheet.GetRow(sheet.FirstRowNum).FirstCellNum.ToString()))
                    //第一行为空，报错
                    {
                        MessageBox.Show("文档内容不符合要求，请检查！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dt.Rows.Clear();
                        break;
                    }

                    //数据  

                    for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                    {

                        //判断是否为合计行，合计行不用写入
                        string heji = sheet.GetRow(i).GetCell(0).ToString();
                        if (heji.Contains("合计"))
                        {
                            break;
                        }
                        DataRow dr = dt.NewRow();
                        bool hasValue = false;
                        foreach (int j in columns)
                        {

                            if (j == 2)
                            {
                                dr[j] = GetValueType(sheet.GetRow(i).GetCell(0));
                            }
                            else if (j == 3)
                            {
                                dr[j] = GetValueType(sheet.GetRow(i).GetCell(1));
                            }
                            else if (j == 4)
                            {
                                dr[j] = GetValueType(sheet.GetRow(i).GetCell(2));
                            }
                            else if (j == 0)
                            {
                                dr[j] = id;
                            }
                            else if (j == 1)
                            {
                                dr[j] = cusName;
                            }
                            if (dr[j] != null && dr[j].ToString() != string.Empty)
                            {
                                hasValue = true;
                            }
                        }
                        if (hasValue)
                        {
                            dt.Rows.Add(dr);
                        }
                    }
                }
                id++;
            }
            return dt;
        }

        /// <summary>
        /// 好利森外部客户Excel导入成Datable
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableWBHLS(string[] files)
        {
            DataTable dt = new DataTable();
            int id;
            String cusName = "";
            IWorkbook workbook;
            //表头  
            if (dt.Columns["序号"] == null)
            {
                dt.Columns.Add(new DataColumn("序号"));
                dt.Columns.Add(new DataColumn("客户"));
                dt.Columns.Add(new DataColumn("货号"));
                dt.Columns.Add(new DataColumn("名称"));
                dt.Columns.Add(new DataColumn("数量"));
            }
            List<int> columns = new List<int> { 0, 1, 2, 3, 4 };
            id = 100000;
            foreach (string file in files)
            {
                string fileExt = Path.GetExtension(file).ToLower();
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                    if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                    if (workbook == null) { return null; }
                    ISheet sheet = workbook.GetSheetAt(0);
                    //遍历每个客户的列
                    for (int k = 6; k <= sheet.GetRow(1).LastCellNum; k++)
                    {
                        int cusQty = 0; //每个客户的订单总数
                        if (sheet.GetRow(1).GetCell(k) != null) ///判断客户不会空
                        {
                            cusName = sheet.GetRow(1).GetCell(k).ToString(); ;//获取外部客户名称
                            string sql = @"select * from AIShlhcsvr.dbo.T_BD_CUSTOMER_L where fname=@cusname";
                            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("cusname", cusName));
                            if (ds.Tables[0].Rows.Count <= 0)
                            {
                                MessageBox.Show("文档'" + System.IO.Path.GetFileNameWithoutExtension(file) + "'客户'" + cusName + "'ERP中不存在，请检查");
                                dt.Rows.Clear();
                                break;
                            }

                            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                            {
                                DataRow dr = dt.NewRow();
                                if (sheet.GetRow(i).GetCell(1) != null && sheet.GetRow(i).GetCell(k) != null && sheet.GetRow(i).GetCell(1).ToString().Length > 2)///商品货号不为空，数量不为空
                                {
                                    //判断货号为69码， 并且数量列为数字
                                    if (sheet.GetRow(i).GetCell(1).ToString().Substring(0, 2).Equals("69") && int.TryParse(sheet.GetRow(i).GetCell(k).ToString(), out int result))
                                    {
                                        //数量列>0
                                        if (int.Parse(sheet.GetRow(i).GetCell(k).ToString()) > 0)
                                        {
                                            dr[0] = id;
                                            dr[1] = cusName;
                                            dr[2] = GetValueType(sheet.GetRow(i).GetCell(1));
                                            dr[3] = GetValueType(sheet.GetRow(i).GetCell(2));
                                            dr[4] = GetValueType(sheet.GetRow(i).GetCell(k));
                                            dt.Rows.Add(dr);
                                        }
                                        //每个客户订单总数，只有总数>0,才算有效客户订单。id递增。
                                        cusQty = cusQty + int.Parse(sheet.GetRow(i).GetCell(k).ToString());
                                    }
                                }
                            }
                        }
                        if (cusQty > 0)
                        {
                            id++;
                        }
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 叮咚订单Excel导入成Datable
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableDingDong(string[] files)
        {
            DataTable dt = new DataTable();
            int id;
            String cusName = "";
            IWorkbook workbook;
            //表头  
            if (dt.Columns["序号"] == null)
            {
                dt.Columns.Add(new DataColumn("序号"));
                dt.Columns.Add(new DataColumn("入库仓"));
                dt.Columns.Add(new DataColumn("到货日期"));
                dt.Columns.Add(new DataColumn("商品ID"));
                dt.Columns.Add(new DataColumn("商品名称"));
                dt.Columns.Add(new DataColumn("采购单价"));
                dt.Columns.Add(new DataColumn("采购数量"));
            }
            List<int> columns = new List<int> { 0, 1, 2, 3, 4,5,6 };
            id = 100000;
            foreach (string file in files)
            {
                string fileExt = Path.GetExtension(file).ToLower();
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                    if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                    if (workbook == null) { return null; }
                    ISheet sheet = workbook.GetSheetAt(0);

                    if (string.IsNullOrEmpty(sheet.GetRow(sheet.FirstRowNum).FirstCellNum.ToString()))
                    //第一行为空，报错
                    {
                        MessageBox.Show("文档内容不符合要求，请检查！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dt.Rows.Clear();
                        break;
                    }

                    //数据  

                    for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                    {
                        DataRow dr = dt.NewRow();
                        bool hasValue = false;
                        foreach (int j in columns)
                        {

                            if (j == 2)
                            {
                                dr[j] = GetValueType(sheet.GetRow(i).GetCell(4));
                            }
                            else if (j == 3)
                            {
                                dr[j] = GetValueType(sheet.GetRow(i).GetCell(11));
                            }
                            else if (j == 4)
                            {
                                dr[j] = GetValueType(sheet.GetRow(i).GetCell(13));
                            }
                            else if (j == 5)
                            {
                                dr[j] = GetValueType(sheet.GetRow(i).GetCell(15));
                            }
                            else if (j == 6)
                            {
                                dr[j] = GetValueType(sheet.GetRow(i).GetCell(16));
                            }
                            else if (j == 0)
                            {
                                dr[j] = id;
                            }
                            else if (j == 1)
                            {
                                dr[j] = GetValueType(sheet.GetRow(i).GetCell(3));
                            }
                            if (dr[j] != null && dr[j].ToString() != string.Empty)
                            {
                                hasValue = true;
                            }
                        }
                        if (hasValue)
                        {
                            dt.Rows.Add(dr);
                        }
                    }
                }
                id++;
            }
            return dt;
        }


        /// <summary>
        /// 广东逅厨沃尔玛Excel导入成Datable
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable PdfToTableGDHC(string[] files)
        {
            //PDF解析成string数组后，取包含“江苏逅厨供应链管理有限公司”字符串的行号，如果该行号字符串长度大于13，则用旧的转换方法，反之用新的转换方法
            int rowNo = 0;
            int id;
            string ordertype="";//订单类别，社区/非社区
            string orderNumber="";//订单号
            string txtResult = "";//生成的文字
            DataTable dt = new DataTable();
            if (dt.Columns["序号"] == null)
            {
                dt.Columns.Add(new DataColumn("序号"));
                dt.Columns.Add(new DataColumn("订单类别"));
                dt.Columns.Add(new DataColumn("订单号"));
                dt.Columns.Add(new DataColumn("商品代码"));
                dt.Columns.Add(new DataColumn("数量"));
                dt.Columns.Add(new DataColumn("单价"));
            }
            id = 100000;
            foreach (string file in files)
            {
                txtResult = "";
                PdfDocument pdf = new PdfDocument();
                pdf.LoadFromFile(file);
                PdfTextExtractOptions extractOptions = new PdfTextExtractOptions();
                extractOptions.IsSimpleExtraction = true;

                StringBuilder extractedText = new StringBuilder();
                foreach (PdfPageBase page in pdf.Pages)
                {
                    PdfTextExtractor extractor = new PdfTextExtractor(page);
                    string text = extractor.ExtractText(extractOptions);
                    extractedText.Append(text);
                }

                txtResult = txtResult + extractedText.ToString();
                txtResult= txtResult.Replace("(GTIN-13)", "");
                pdf.Close();
                
                //把pdf转换成的字符串按行写入string[]，通过分析每行的string，写入tb，忽略空行
                //忽略空行
                string[] ContentLines1 = txtResult.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                // ContentLines1中去除内容为空格的行
                string[] ContentLines1NoEmpty = ContentLines1.Where(item => !string.IsNullOrWhiteSpace(item)).ToArray();
                for (int k = 0; k < ContentLines1NoEmpty.Length; k++)
                {
                    if (ContentLines1NoEmpty[k].ToString().Contains("Purchase Order Number"))
                    {
                        string text = ContentLines1NoEmpty[k].ToString().Replace(" ", "");
                        int index = text.IndexOf("Purchase Order Number");
                        orderNumber = index >= 0 ? text.Substring(index + "Purchase Order Number".Length) : string.Empty;
                        if (orderNumber.Substring(0, 3).Equals("VPO") || orderNumber.Substring(0, 2).Equals("91"))
                        {
                            ordertype = "社区";
                        }
                        else
                        {
                            ordertype = "非社区";
                        }
                    }

                    if (ContentLines1NoEmpty[k].ToString().Contains("江苏逅厨供应链管理有限公司"))
                    {
                        rowNo = k;
                        break;
                    }
                }
                //Console.WriteLine(ContentLines1[rowNo].ToString().Length.ToString());
                if (ContentLines1NoEmpty[rowNo].ToString().Length > 13)
                {
                    
                    for (int i = rowNo; i < ContentLines1NoEmpty.Length; i++)
                    {
                        //判断文本的首行字符串，有时候沃尔玛的PDF格式不一定一致，需要多加点判断条件，第一个数字是0
                        if (char.IsDigit(ContentLines1NoEmpty[i][0]) && ContentLines1NoEmpty[i][0] == '0')
                        {
                            // 拆分出一行的所有用空格分割的数据项
                            string[] values = ContentLines1NoEmpty[i].Split(' ');
                            // 存入DataTable
                            DataRow dr = dt.NewRow();
                            dr[0] = id;
                            dr[1]= ordertype;
                            dr[2]= orderNumber;
                            dr[3]= values[1].Length > 8 ? values[1].Substring(1) : values[1]; //大于8位的话去除第一位0
                            dr[4]= values[7];
                            dr[5] = values[12];
                            dt.Rows.Add(dr);
                        }
                        //判断文本的首行字符串，第一个是数字就能取那一行的数据作为订单数据，这个情况是第一个数字不是0
                        else if (char.IsDigit(ContentLines1NoEmpty[i][0]) && int.Parse(ContentLines1NoEmpty[i][0].ToString()) > 0)
                        {
                            // 拆分出一行的所有用空格分割的数据项
                            string[] values = ContentLines1NoEmpty[i].Split(' ');
                            // 存入DataTable
                            DataRow dr = dt.NewRow();
                            dr[0] = id;
                            dr[1] = ordertype;
                            dr[2] = orderNumber;
                            dr[3] = values[1].Length > 8 ? values[1].Substring(1) : values[1]; //大于8位的话去除第一位0
                            dr[4] = values[6];
                            dr[5] = values[9];
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    for (int i = rowNo; i < ContentLines1NoEmpty.Length - 2; i++)
                    {
                        //判断文本的首行字母，有时候沃尔玛的PDF格式不一定一直，需要多加点判断条件
                        if (char.IsDigit(ContentLines1NoEmpty[i][0]) && ContentLines1NoEmpty[i][0] == '0' && ContentLines1NoEmpty[i].ToString().Contains("6977"))
                        {
                            if (ContentLines1NoEmpty[i].ToString().Contains("份") || ContentLines1NoEmpty[i].ToString().Contains("CA"))
                            {
                                Console.WriteLine(i);
                                // 拆分出一行的所有用空格分割的数据项
                                string[] values = ContentLines1NoEmpty[i].Split(' ');
                                // 存入DataTable
                                DataRow dr = dt.NewRow();
                                dr[0] = id;
                                dr[1] = ordertype;
                                dr[2] = orderNumber;
                                dr[3] = values[1].Length > 8 ? values[1].Substring(1) : values[1]; //大于8位的话去除第一位0
                                dr[4] = values[6];
                                dr[5] = values[11];
                                dt.Rows.Add(dr);
                            }
                            else
                            {
                                Console.WriteLine(i);
                                // 拆分出一行的所有用空格分割的数据项
                                string[] values = ContentLines1NoEmpty[i].Split(' ');
                                // 存入DataTable
                                DataRow dr = dt.NewRow();
                                dr[0] = id;
                                dr[1] = ordertype;
                                dr[2] = orderNumber;
                                dr[3] = values[1].Length > 8 ? values[1].Substring(1) : values[1]; //大于8位的话去除第一位0
                                values = ContentLines1NoEmpty[i + 1].Split(' ');
                                dr[4] = values[3];
                                dr[5] = values[8];
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
                id++;
            }
            return dt;
        }

        /// <summary>
        /// 广东逅厨送货单转换（单订单号）
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableGDHCSHD(string file)
        {
            List<string> shops = new List<string> { };
            int cusQty = 0;
            DataTable dt = new DataTable();
            IWorkbook workbook;
            //表头  
            if (dt.Columns["门店"] == null)
            {
                dt.Columns.Add(new DataColumn("门店"));
                dt.Columns.Add(new DataColumn("商品号"));
                dt.Columns.Add(new DataColumn("商品名称"));
                dt.Columns.Add(new DataColumn("数量"));
            }

            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    OutSongHuoDanGDHC.outSongHuoDanGDHC.sheets.Add(workbook.GetSheetAt(i).SheetName);
                }
                SheetChoose sheetChoose = new SheetChoose();//弹出页签选择页面，选择需要的转换的页签
                if (sheetChoose.ShowDialog() == DialogResult.OK)
                {
                    ISheet sheet = workbook.GetSheetAt(SheetChoose.sheetChoose.cb_sheets.SelectedIndex);
                    //Console.WriteLine(sheet.FirstRowNum);
                    //遍历每个门店的列
                    for (int i = 2; i <= sheet.GetRow(1).LastCellNum; i++)
                    {
                        cusQty = 0;
                        if (sheet.GetRow(1).GetCell(i).ToString().Equals("合计") || sheet.GetRow(1).GetCell(i).ToString().Equals("总计"))
                        {
                            break;
                        }

                        for (int j = sheet.FirstRowNum + 2; j <= sheet.LastRowNum; j++)
                        {
                            DataRow dr = dt.NewRow();
                            if (sheet.GetRow(j).GetCell(0) != null && sheet.GetRow(j).GetCell(i) != null)///商品货号不为空，数量不为空
                            {
                                //判断数量列为数字
                                if (int.TryParse(sheet.GetRow(j).GetCell(i).ToString(), out int result))
                                {
                                    //数量列>0
                                    if (int.Parse(sheet.GetRow(j).GetCell(i).ToString()) > 0)
                                    {
                                        dr[0] = GetValueType(sheet.GetRow(1).GetCell(i));
                                        dr[1] = GetValueType(sheet.GetRow(j).GetCell(0));
                                        dr[2] = GetValueType(sheet.GetRow(j).GetCell(1));
                                        dr[3] = GetValueType(sheet.GetRow(j).GetCell(i));
                                        dt.Rows.Add(dr);
                                        //门店总数
                                        cusQty = cusQty + int.Parse(sheet.GetRow(j).GetCell(i).ToString());
                                    }
                                }
                            }
                        }
                        if (cusQty > 0)
                        {
                            shops.Add(GetValueType(sheet.GetRow(1).GetCell(i)).ToString());//门店列表
                        }
                    }
                    OutSongHuoDanGDHC.outSongHuoDanGDHC.sheets.Clear();
                }
                else
                {
                    OutSongHuoDanGDHC.outSongHuoDanGDHC.sheets.Clear();
                }
            }
            MyVar.Shops = shops;
            return dt;
        }

        /// <summary>
        /// 广东逅厨送货单转换（单订单号）分单，三明治一单，剩余的一单
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableGDHCSHD_FD(string file)
        {
            List<string> shops = new List<string> { };
            int cusQty1 = 0;//分单一的总量
            int cusQty2 = 0;//分单二的总量
            DataTable dt = new DataTable();
            IWorkbook workbook;
            //表头  
            if (dt.Columns["门店"] == null)
            {
                dt.Columns.Add(new DataColumn("门店"));
                dt.Columns.Add(new DataColumn("商品号"));
                dt.Columns.Add(new DataColumn("商品名称"));
                dt.Columns.Add(new DataColumn("数量"));
            }

            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    OutSongHuoDanGDHC.outSongHuoDanGDHC.sheets.Add(workbook.GetSheetAt(i).SheetName);
                }
                SheetChoose sheetChoose = new SheetChoose();//弹出页签选择页面，选择需要的转换的页签
                if (sheetChoose.ShowDialog() == DialogResult.OK)
                {
                    ISheet sheet = workbook.GetSheetAt(SheetChoose.sheetChoose.cb_sheets.SelectedIndex);
                    Console.WriteLine(sheet.FirstRowNum);
                    //遍历每个门店的列
                    for (int i = 2; i <= sheet.GetRow(1).LastCellNum; i++)
                    {
                        cusQty1 = 0;
                        cusQty2 = 0;
                        if (sheet.GetRow(1).GetCell(i).ToString().Equals("合计") || sheet.GetRow(1).GetCell(i).ToString().Equals("总计"))
                        {
                            break;
                        }

                        for (int j = sheet.FirstRowNum + 2; j <= sheet.LastRowNum; j++)
                        {
                            DataRow dr = dt.NewRow();
                            if (sheet.GetRow(j).GetCell(0) != null && sheet.GetRow(j).GetCell(i) != null)///商品货号不为空，数量不为空
                            {
                                //判断数量列为数字
                                if (int.TryParse(sheet.GetRow(j).GetCell(i).ToString(), out int result))
                                {
                                    //数量列>0
                                    if (int.Parse(sheet.GetRow(j).GetCell(i).ToString()) > 0)
                                    {
                                        if (GetValueType(sheet.GetRow(j).GetCell(1)).ToString().Contains("三明治"))
                                        {
                                            dr[0] = GetValueType(sheet.GetRow(1).GetCell(i)) + "(2)";
                                            dr[1] = GetValueType(sheet.GetRow(j).GetCell(0));
                                            dr[2] = GetValueType(sheet.GetRow(j).GetCell(1));
                                            dr[3] = GetValueType(sheet.GetRow(j).GetCell(i));
                                            dt.Rows.Add(dr);
                                            //门店总数
                                            cusQty1 = cusQty1 + int.Parse(sheet.GetRow(j).GetCell(i).ToString());
                                        }
                                        else
                                        {
                                            dr[0] = GetValueType(sheet.GetRow(1).GetCell(i));
                                            dr[1] = GetValueType(sheet.GetRow(j).GetCell(0));
                                            dr[2] = GetValueType(sheet.GetRow(j).GetCell(1));
                                            dr[3] = GetValueType(sheet.GetRow(j).GetCell(i));
                                            dt.Rows.Add(dr);
                                            //门店总数
                                            cusQty2 = cusQty2 + int.Parse(sheet.GetRow(j).GetCell(i).ToString());
                                        }
                                    }
                                }
                            }
                        }
                        if (cusQty1 > 0)
                        {
                            shops.Add(GetValueType(sheet.GetRow(1).GetCell(i)).ToString() + "(2)");//门店列表
                        }
                        if (cusQty2 > 0)
                        {
                            shops.Add(GetValueType(sheet.GetRow(1).GetCell(i)).ToString());//门店列表
                        }
                    }
                    OutSongHuoDanGDHC.outSongHuoDanGDHC.sheets.Clear();
                }
                else
                {
                    OutSongHuoDanGDHC.outSongHuoDanGDHC.sheets.Clear();
                }
            }
            MyVar.Shops = shops;
            return dt;
        }

        /// <summary>
        /// 广东逅厨送货单转换（多订单号）
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableGDHCSHDDDD(string file)
        {
            List<string> shops = new List<string> { };
            int cusQty = 0;
            DataTable dt = new DataTable();
            IWorkbook workbook;
            //表头  
            if (dt.Columns["门店"] == null)
            {
                dt.Columns.Add(new DataColumn("订单号"));
                dt.Columns.Add(new DataColumn("门店"));
                dt.Columns.Add(new DataColumn("商品号"));
                dt.Columns.Add(new DataColumn("商品名称"));
                dt.Columns.Add(new DataColumn("数量"));
            }

            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    OutSongHuoDanGDHCDDD.outSongHuoDanGDHCDDD.sheets.Add(workbook.GetSheetAt(i).SheetName);
                }
                SheetChoose sheetChoose = new SheetChoose();//弹出页签选择页面，选择需要的转换的页签
                if (sheetChoose.ShowDialog() == DialogResult.OK)
                {
                    ISheet sheet = workbook.GetSheetAt(SheetChoose.sheetChoose.cb_sheets.SelectedIndex);

                    //遍历每个门店的列
                    for (int i = 3; i <= sheet.GetRow(1).LastCellNum; i++)
                    {
                        cusQty = 0;
                        if (sheet.GetRow(1).GetCell(i).ToString().Equals("合计") || sheet.GetRow(1).GetCell(i).ToString().Equals("总计"))
                        {
                            break;
                        }

                        for (int j = sheet.FirstRowNum + 2; j <= sheet.LastRowNum; j++)
                        {

                            DataRow dr = dt.NewRow();
                            if (sheet.GetRow(j).GetCell(0) != null && sheet.GetRow(j).GetCell(1) != null && sheet.GetRow(j).GetCell(i) != null)///订单号不为空，商品货号不为空，数量不为空
                            {
                                //判断数量列为数字
                                if (int.TryParse(sheet.GetRow(j).GetCell(i).ToString(), out int result))
                                {
                                    //数量列>0
                                    if (int.Parse(sheet.GetRow(j).GetCell(i).ToString()) > 0)
                                    {
                                        dr[0] = GetValueType(sheet.GetRow(j).GetCell(0));
                                        dr[1] = GetValueType(sheet.GetRow(1).GetCell(i));
                                        dr[2] = GetValueType(sheet.GetRow(j).GetCell(1));
                                        dr[3] = GetValueType(sheet.GetRow(j).GetCell(2));
                                        dr[4] = GetValueType(sheet.GetRow(j).GetCell(i));
                                        dt.Rows.Add(dr);
                                        //门店总数
                                        cusQty = cusQty + int.Parse(sheet.GetRow(j).GetCell(i).ToString());
                                    }
                                }
                            }
                        }
                        if (cusQty > 0)
                        {
                            shops.Add(GetValueType(sheet.GetRow(1).GetCell(i)).ToString());//门店列表
                        }
                    }
                    OutSongHuoDanGDHCDDD.outSongHuoDanGDHCDDD.sheets.Clear();
                }
                else
                {
                    OutSongHuoDanGDHCDDD.outSongHuoDanGDHCDDD.sheets.Clear();
                }
            }
            MyVar.Shops = shops;
            return dt;
        }

        /// <summary>
        /// 广东逅厨送货单转换（多订单号二）
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableGDHCSHDDDD2(string file)
        {
            List<string> orders = new List<string> { }; //订单列表
            List<string> shops = new List<string> { };  //门店列表
            DataTable dt = new DataTable();
            IWorkbook workbook;
            //表头  
            if (dt.Columns["门店"] == null)
            {
                dt.Columns.Add(new DataColumn("订单号"));
                dt.Columns.Add(new DataColumn("门店"));
                dt.Columns.Add(new DataColumn("商品号"));
                dt.Columns.Add(new DataColumn("商品名称"));
                dt.Columns.Add(new DataColumn("数量"));
            }

            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    OutSongHuoDanGDHCDDD2.outSongHuoDanGDHCDDD2.sheets.Add(workbook.GetSheetAt(i).SheetName);
                }
                SheetChoose sheetChoose = new SheetChoose();//弹出页签选择页面，选择需要的转换的页签
                if (sheetChoose.ShowDialog() == DialogResult.OK)
                {
                    ISheet sheet = workbook.GetSheetAt(SheetChoose.sheetChoose.cb_sheets.SelectedIndex);

                    //遍历每一行
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        DataRow dr = dt.NewRow();
                        if (sheet.GetRow(i).GetCell(0) != null && sheet.GetRow(i).GetCell(2) != null && sheet.GetRow(i).GetCell(4) != null)///订单号不为空，商品货号不为空，数量不为空
                        {
                            //判断数量列为数字
                            if (int.TryParse(sheet.GetRow(i).GetCell(4).ToString(), out int result))
                            {
                                //数量列>0
                                if (int.Parse(sheet.GetRow(i).GetCell(4).ToString()) > 0)
                                {
                                    dr[0] = GetValueType(sheet.GetRow(i).GetCell(0));
                                    dr[1] = GetValueType(sheet.GetRow(i).GetCell(1));
                                    dr[2] = GetValueType(sheet.GetRow(i).GetCell(2));
                                    dr[3] = GetValueType(sheet.GetRow(i).GetCell(3));
                                    dr[4] = GetValueType(sheet.GetRow(i).GetCell(4));
                                    dt.Rows.Add(dr);
                                    if (!shops.Contains(GetValueType(sheet.GetRow(i).GetCell(1)).ToString()))
                                    {
                                        shops.Add(GetValueType(sheet.GetRow(i).GetCell(1)).ToString());//门店列表
                                    }
                                    if (!orders.Contains(GetValueType(sheet.GetRow(i).GetCell(0)).ToString()))
                                    {
                                        orders.Add(GetValueType(sheet.GetRow(i).GetCell(0)).ToString());//订单列表
                                    }
                                }

                            }
                        }

                    }
                    OutSongHuoDanGDHCDDD2.outSongHuoDanGDHCDDD2.sheets.Clear();
                }
                else
                {
                    OutSongHuoDanGDHCDDD2.outSongHuoDanGDHCDDD2.sheets.Clear();
                }
            }
            MyVar.Shops = shops;
            MyVar.Orders = orders;
            return dt;
        }


        /// <summary>
        ///学校订单Excel导入成Datable
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableSchool(string file)
        {
            List<string> sheets = new List<string> { };
            DataTable dt = new DataTable();
            int id;
            String cusName = "";
            String type = "";//类别01学生，02教师
            IWorkbook workbook;
            //表头  
            if (dt.Columns["序号"] == null)
            {
                dt.Columns.Add(new DataColumn("序号"));
                dt.Columns.Add(new DataColumn("日期"));
                dt.Columns.Add(new DataColumn("客户"));
                dt.Columns.Add(new DataColumn("类别")); 
                dt.Columns.Add(new DataColumn("编码"));
                dt.Columns.Add(new DataColumn("名称"));
                dt.Columns.Add(new DataColumn("数量"));
                dt.Columns.Add(new DataColumn("单位"));
            }
            List<int> columns = new List<int> { 0, 1, 2, 3, 4,5 };
            id = 100000;

            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    cusName = StrHelper.beforeStr(sheet.SheetName,"#");
                    Console.WriteLine(cusName);
                    type= StrHelper.afterStr(sheet.SheetName, "#");
                    string sql = @"select * from AIShlhcsvr.dbo.T_BD_CUSTOMER_L where fname=@cusname";
                    DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("cusname", cusName));
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        MessageBox.Show("文档'" + System.IO.Path.GetFileNameWithoutExtension(file) + "'中客户名称"+ cusName + "错误，请检查");
                        dt.Rows.Clear();
                        return null;
                    }
                    if (string.IsNullOrEmpty(sheet.GetRow(sheet.FirstRowNum).FirstCellNum.ToString()))
                    //第一行为空，报错
                    {
                        MessageBox.Show("文档内容不符合要求，请检查！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dt.Rows.Clear();
                        break;
                    }
                    for (int j = sheet.FirstRowNum + 2; j <= sheet.LastRowNum; j++)
                    {
                        for (int k = 5; k <= 11; k++)
                        {
                            DataRow dr = dt.NewRow();
                            //bool hasValue = false;
                            if (sheet.GetRow(j).GetCell(k)!=null)
                            {
                                if(!GetValueType(sheet.GetRow(j).GetCell(k)).Equals("0"))
                                { 
                                    dr[0] = id;
                                    dr[1] = GetValueType(sheet.GetRow(1).GetCell(k));
                                    dr[2] = cusName;
                                    dr[3] = type;
                                    dr[4] = GetValueType(sheet.GetRow(j).GetCell(1));
                                    dr[5] = GetValueType(sheet.GetRow(j).GetCell(2));
                                    dr[6] = GetValueType(sheet.GetRow(j).GetCell(k));
                                    dr[7] = GetValueType(sheet.GetRow(j).GetCell(4));
                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                        
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:
                    if (DateUtil.IsCellDateFormatted(cell))//检查是否为日期格式的数字
                    {
                        return cell.DateCellValue.Date;
                    }
                    else
                    { 
                        return cell.NumericCellValue;
                    }
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA: 
                    cell.SetCellType(CellType.String);
                    return cell.StringCellValue;
                default:
                    return "=" + cell.CellFormula;
            }
        }


    }
}
