using System;
using System.Collections.Generic;
using System.Text;

namespace 逅厨系统辅助工具
{
     public class RandomStr
    {
        /// <summary>
        /// 生成随机数的种子
        /// </summary>
        /// <returns></returns>
        private static int getNewSeed()
        {
            byte[] rndBytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(rndBytes);
            return BitConverter.ToInt32(rndBytes, 0);
        }     /// <summary>
              /// 生成8位随机数
              /// </summary>
              /// <param name="length"></param>
              /// <returns></returns>

        static public string GetRandomString(int len)
        {
            string s = "123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ";
            string reValue = string.Empty;
            Random rnd = new Random(getNewSeed());
            while (reValue.Length < len)
            {
                string s1 = s[rnd.Next(0, s.Length)].ToString();
                if (reValue.IndexOf(s1) == -1) reValue += s1;
            }
            return reValue;
        }

    }
}
