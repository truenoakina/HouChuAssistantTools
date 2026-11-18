using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 逅厨系统辅助工具
{
    public class LoginInfo
    {
        private static string userName;
        /// <summary>
        /// 登录用户名
        /// </summary>
        public static string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        

        private static string passWord;
        /// <summary>
        /// 登录用户密码
        /// </summary>
        public static string PassWord
        {
            get { return LoginInfo.passWord; }
            set { LoginInfo.passWord = value; }
        }


       
        private static string loginComputer;
        /// <summary>
        /// 登录计算机名
        /// </summary>
        public static string LoginComputer
        {
            get { return LoginInfo.loginComputer; }
            set { LoginInfo.loginComputer = value; }
        }

        private static string loginIP;
        /// <summary>
        /// 登录IP
        /// </summary>
        public static string LoginIP
        {
            get { return LoginInfo.loginIP; }
            set { LoginInfo.loginIP = value; }
        }
    }
}
