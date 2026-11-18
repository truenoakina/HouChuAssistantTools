using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace 逅厨系统辅助工具
{
    class WrkDdInfo
    {  
        //逅厨的采购订单未入库数量
        private static int houchuwrk;

        public static int Houchuwrk { get => houchuwrk; set => houchuwrk = value; }
        

        //佳之家的采购订单未入库数量
        private static int jzjwrk;

        public static int Jzjwrk { get => jzjwrk; set => jzjwrk = value; }
        

        //谷之田的采购订单未入库数量
        private static int gztwrk;

        public static int Gztwrk { get => gztwrk; set => gztwrk = value; }
        

        //好利森的采购订单未入库数量
        private static int hlswrk;

        public static int Hlswrk { get => hlswrk; set => hlswrk = value; }
        

        //弹出页面点击的采购组织1.逅厨，2，佳之家，3，谷之田，4.好利森
        private static int cgzz;

        public static int Cgzz { get => cgzz; set => cgzz = value; }
        

        private static DataTable dthouchuwrk;
        /// <summary>
        /// 存放逅厨未入库订单的datatable
        /// </summary>
        public static DataTable Dthouchuwrk { get => dthouchuwrk; set => dthouchuwrk = value; }
        

        private static DataTable dtjzjwrk;
        /// <summary>
        /// 存放佳之家未入库订单的datatable
        /// </summary>
        public static DataTable Dtjzjwrk { get => dtjzjwrk; set => dtjzjwrk = value; }
        

        private static DataTable dtgztwrk;
        /// <summary>
        /// 存放谷之田未入库订单的datatable
        /// </summary>
        public static DataTable Dtgztwrk { get => dtgztwrk; set => dtgztwrk = value; }
        

        private static DataTable dthlswrk;
        /// <summary>
        /// 存放好利森未入库订单的datatable
        /// </summary>
        public static DataTable Dthlswrk { get => dthlswrk; set => dthlswrk = value; }

    }
}
