using System;
using System.Collections.Generic;
using System.Text;

namespace 逅厨系统辅助工具
{
    class MyVar
    {
        public static string zzcode ;
        /// <summary>
        /// 组织代码
        /// </summary>
        public static string Zzcode
        {
            get { return zzcode; }
            set { zzcode = value; }
        }
        public static string zzname ;

        /// <summary>
        /// 组织名称
        /// </summary>
        public static string Zzname
        {
            get { return zzname; }
            set { zzname = value; }
        }

        /// <summary>
        /// 销售员代码
        /// </summary>
        public static string salerCode;

        public static string SalerCode
        {
            get { return salerCode; }
            set { salerCode = value; }
        }

        /// <summary>
        /// 销售员名称
        /// </summary>
        public static string salerName;

        public static string SalerName
        {
            get { return salerName; }
            set { salerName = value; }
        }
        /// <summary>
        /// 订单日期
        /// </summary>
        public static string saleDate;

        public static string SaleDate
        {
            get { return saleDate; }
            set { saleDate = value; }
        }


        /// <summary>
        /// 要货日期
        /// </summary>
        public static string wantDate;

        public static string WantDate
        {
            get { return wantDate; }
            set { wantDate = value; }
        }
        /// <summary>
        /// 金蝶编码
        /// </summary>
        public static string kCode;

        public static string KCode
        {
            get { return kCode; }
            set { kCode = value; }
        }


        /// <summary>
        /// 外部客户excel中客户名称
        /// </summary>
        public static string cusCode;

        public static string CusCode
        {
            get { return cusCode; }
            set { cusCode = value; }
        }

        /// <summary>
        /// 资质提醒的有效天数
        /// </summary>
        private static int usefuldays;

        public static int Usefuldays
        {
            get
            {
                return usefuldays;
            }

            set
            {
                usefuldays = value;
            }
        }

        /// <summary>
        /// 交货地id
        /// </summary>
        public static string jhdId;

        public static string JhdId
        {
            get { return jhdId; }
            set { jhdId = value; }
        }

        /// <summary>
        /// 交货地名称
        /// </summary>
        public static string jhd;

        public static string Jhd
        {
            get { return jhd; }
            set { jhd = value; }
        }

       



        /// <summary>
        /// 不用提醒的采购订单日期
        /// </summary>
        private static DateTime notneedWarnDate;

        public static DateTime NotneedWarnDate { get => notneedWarnDate; set => notneedWarnDate = value; }
        

        /// <summary>
        /// 订单之后几天内未入库需要提醒
        /// </summary>
        private static int wrkdays;

        public static int Wrkdays { get => wrkdays; set => wrkdays = value; }

        /// <summary>
        /// 广东逅厨沃尔玛送货单转换时的门店数量
        /// </summary>
        private static List<string> shops;
        public static List<string> Shops { get => shops; set => shops = value; }

        /// <summary>
        /// 广东逅厨沃尔玛多订单转送货单时的订单列表
        /// </summary>
        private static List<string> orders;
        public static List<string> Orders { get => orders; set => orders = value; }

        /// <summary>
        /// excel转换时选中的页签名称
        /// </summary>
        private static string sheetname;
        public static string Sheetname { get => sheetname; set => sheetname = value; }
        

        /// <summary>
        /// 广东逅厨沃尔玛送货单转换时的部门
        /// </summary>
        private static string dep;
        public static string Dep { get => dep; set => dep = value; }
        

        /// <summary>
        /// 广东逅厨沃尔玛送货单转换时的订单号
        /// </summary>
        private static string orderNo;
        public static string OrderNo { get => orderNo; set => orderNo = value; }

        /// <summary>
        /// 广东逅厨沃尔玛送货单转换时的类别，single单订单，multi多订单
        /// </summary>
        private static int wemshdType;
        public static int WemshdType { get => wemshdType; set => wemshdType = value; }

        
        /// <summary>
        /// 是否外网使用
        /// </summary>
        private static bool isOutNet;
        public static bool IsOutNet { get => isOutNet; set => isOutNet = value; }
        

        /// <summary>
        /// 信息明细页面打开的模式，add新增，update更新
        /// </summary>
        private static string showmode;
        public static string Showmode { get => showmode; set => showmode = value; }
        
        /// <summary>
        /// 每日标准时长，用于计算考勤天数
        /// </summary>
        private static decimal dayHours;
        public static decimal DayHours { get => dayHours; set => dayHours = value; }
       
        /// <summary>
        /// 每日常规工作时长，用于计算加班时长
        /// </summary>
        private static decimal regularHours;
        public static decimal RegularHours { get => regularHours; set => regularHours = value; }

        /// <summary>
        /// 用餐时间
        /// </summary>
        private static decimal diningTime;
        public static decimal DiningTime { get => diningTime; set => diningTime = value; }
        /// <summary>
        /// 是否扣减午餐
        /// </summary>
        private static bool isWuCanKJ;
        public static bool IsWuCanKJ { get => isWuCanKJ; set => isWuCanKJ = value; }
        /// <summary>
        /// 正式工工时成本
        /// </summary>
        private static int regEmpPrice;
        public static int RegEmpPrice { get => regEmpPrice; set => regEmpPrice = value; }
        /// <summary>
        /// 临时工工时成本
        /// </summary>
        private static int tempEmpPrice;
        public static int TempEmpPrice { get => tempEmpPrice; set => tempEmpPrice = value; }
        /// <summary>
        /// 直接人工固定增加值
        /// </summary>
        private static int manualFixValue;
        public static int ManualFixValue { get => manualFixValue; set => manualFixValue = value; }

        /// <summary>
        /// 是否扣减晚餐
        /// </summary>
        private static bool isWanCanKJ;
        public static bool IsWanCanKJ { get => isWanCanKJ; set => isWanCanKJ = value; }
       
        /// <summary>
        /// 水价佳之家
        /// </summary>
        private static decimal waterPriceJZJ;
        public static decimal WaterPriceJZJ { get => waterPriceJZJ; set => waterPriceJZJ = value; }
        
        /// <summary>
        /// 水价谷之田
        /// </summary>
        private static decimal waterPriceGZT;
        public static decimal WaterPriceGZT { get => waterPriceGZT; set => waterPriceGZT = value; }

        /// <summary>
        /// 电价
        /// </summary>
        private static decimal elecPrice;
        public static decimal ElecPrice { get => elecPrice; set => elecPrice = value; }
        
        /// <summary>
        /// 天然气价
        /// </summary>
        private static decimal ngPrice;
        public static decimal NgPrice { get => ngPrice; set => ngPrice = value; }
        
        /// <summary>
        /// 蒸汽价
        /// </summary>
        private static decimal steamPrice;
        public static decimal SteamPrice { get => steamPrice; set => steamPrice = value; }
       

        /// <summary>
        /// 生产数据录入时弹出参数配置页面时的日期
        /// </summary>
        private static DateTime fDDconfigDate;
        public static DateTime FDDconfigDate { get => fDDconfigDate; set => fDDconfigDate = value; }
        
        /// <summary>
        /// 登录姓名
        /// </summary>
        private static string loginName;
        public static string LoginName { get => loginName; set => loginName = value; }

        /// <summary>
        /// 登录计算机名
        /// </summary>
        private static string loginComputer;
        public static string LoginComputer { get => loginComputer; set => loginComputer = value; }
    }
}
