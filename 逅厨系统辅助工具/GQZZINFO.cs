using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace 逅厨系统辅助工具
{
    public class GQZZINFO
    {
        private static int gysyyzzNum;

        /// <summary>
        /// 供应商营业执照过期资质数量
        /// </summary>
        public static int GysyyzzNum
        {
            get
            {
                return gysyyzzNum;
            }

            set
            {
                gysyyzzNum = value;
            }
        }

        private static int gysjyxkzNum;
        /// <summary>
        /// 供应商经营许可证过期资质数量
        /// </summary>
        public static int GysjyxkzNum
        {
            get
            {
                return gysjyxkzNum;
            }

            set
            {
                gysjyxkzNum = value;
            }
        }

        private static int scsyyzzNum;
        /// <summary>
        /// 生产商营业执照过期资质数量
        /// </summary>
        public static int ScsyyzzNum
        {
            get
            {
                return scsyyzzNum;
            }

            set
            {
                scsyyzzNum = value;
            }
        }

        private static int scsscxkzNum;
        /// <summary>
        /// S生产商生产许可证过期资质数量
        /// </summary>
        public static int ScsscxkzNum
        {
            get
            {
                return scsscxkzNum;
            }

            set
            {
                scsscxkzNum = value;
            }
        }

        private static int xjbgNum;
        /// <summary>
        /// 型检报告过期资质数量
        /// </summary>
        public static int XjbgNum
        {
            get
            {
                return xjbgNum;
            }

            set
            {
                xjbgNum = value;
            }
        }

        private static int chooseNo;
        /// <summary>
        /// zizhiwarn页面点击查看是对应的页签项
        /// </summary>
        public static int ChooseNo
        {
            get
            {
                return chooseNo;
            }

            set
            {
                chooseNo = value;
            }
        }

        private static DataTable dtgysyyzz;
        /// <summary>
        /// 存放供应商营业执照过期资质的datatable
        /// </summary>
        public static DataTable Dtgysyyzz
        {
            get
            {
                return dtgysyyzz;
            }

            set
            {
                dtgysyyzz = value;
            }
        }

        private static DataTable dtgysjyxkz;
        /// <summary>
        /// 存放供应商经营许可证过期资质的datatable
        /// </summary>
        public static DataTable Dtgysjyxkz
        {
            get
            {
                return dtgysjyxkz;
            }

            set
            {
                dtgysjyxkz = value;
            }
        }


        private static DataTable dtscsyyzz;
        /// <summary>
        /// 存放生产商营业执照过期资质的datatable
        /// </summary>
        public static DataTable Dtscsyyzz
        {
            get
            {
                return dtscsyyzz;
            }

            set
            {
                dtscsyyzz = value;
            }
        }

        private static DataTable dtscsscxkz;
        /// <summary>
        /// 存放生产商生产许可证资质的datatable
        /// </summary>

        public static DataTable Dtscsscxkz
        {
            get
            {
                return dtscsscxkz;
            }

            set
            {
                dtscsscxkz = value;
            }
        }


        private static DataTable dtxjbg;
        /// <summary>
        /// 存放型检报告资质的datatable
        /// </summary>

        public static DataTable Dtxjbg
        {
            get
            {
                return dtxjbg;
            }

            set
            {
                dtxjbg = value;
            }
        }

        



    }
}
