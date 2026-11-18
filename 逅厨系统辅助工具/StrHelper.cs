using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace 逅厨系统辅助工具
{
    public class StrHelper
    {
        /// <summary>
        /// 是否为双字节字符。
        /// </summary>
        private static bool IsTwoBytesChar(char chr)
        {
            string str = chr.ToString();
            // 使用中文支持编码
            Encoding ecode = Encoding.GetEncoding("gb18030");
            if (ecode.GetByteCount(str) == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 传入单个字符，得到字符的ASCII码
        /// </summary>
        /// <param name="chr">汤</param>
        /// <returns>-13120</returns>
        private static int Asc(char character)
        {
            Encoding ecode = Encoding.GetEncoding("GB18030");
            Byte[] codeBytes = ecode.GetBytes(character.ToString());
            if (IsTwoBytesChar(character))
            {
                // 双字节码为高位乘256，再加低位
                // 该为无符号码，再减65536
                return (int)codeBytes[0] * 256 + (int)codeBytes[1] - 65536;
            }
            else
            {
                return (int)codeBytes[0];
            }
        }

        /// <summary>
        /// 传入单个字符的ASCII码，得到ASCII码对应的字符（含双字节）
        /// </summary>
        /// <param name="asc">-13120</param>
        /// <returns>汤</returns>
        private static string Chr(int asc)
        {
            //asc = asc + 65536;
            Encoding asciiEncoding = Encoding.GetEncoding("GB18030");
            Byte[] chrByte = BitConverter.GetBytes((short)asc);
            string strCharacter = string.Empty;
            if (asc < 0 || asc > 255)
            {
                Byte[] chrByteStr = new byte[2];
                chrByteStr[0] = chrByte[1];
                chrByteStr[1] = chrByte[0];
                strCharacter = asciiEncoding.GetString(chrByteStr);
            }
            else
            {
                Byte[] chrByteStr = new byte[1];
                chrByteStr[0] = chrByte[0];
                strCharacter = asciiEncoding.GetString(chrByteStr);
            }
            return (strCharacter);
        }

        private static string Int2Hex(int TheInteger)
        {
            int P1, P2;
            string values = "";
            for (int i = 0; i <= 15; i++)
            {
                for (int j = 0; j <= 15; j++)
                {
                    if (TheInteger == i * 16 + j)
                    {
                        P1 = i + 48;
                        P2 = j + 48;
                        if (P1 > 57)
                        {
                            P1 += 7;
                        }
                        if (P2 > 57)
                        {
                            P2 += 7;
                        }
                        values = Chr(P1) + Chr(P2);
                    }
                }
            }
            return values;
        }

        private static int Hex2Int(string theHex)
        {
            int values;
            int K1, K2, A;
            long W;
            string S;
            values = 0;
            K1 = theHex.Length;
            K2 = 1;
            W = 1;
            while (K1 > 0)
            {
                S = theHex.Substring(K1 - 1, 1);
                A = Asc(Convert.ToChar(S));
                if (A >= 48 && A <= 57)
                {
                    K2 = A - 48;
                }
                else if (A >= 65 && A <= 70)
                {
                    K2 = A - 55;
                }
                else
                {
                    K2 = 0;
                }
                values = values + K2 * Convert.ToInt32(W);
                K1 = K1 - 1;
                W = W * 16;
            }
            return values;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="origPassword"></param>
        /// <returns></returns>
        public static string PasswordEncrypt(string origPassword)
        {
            string values = "", RKey, HalfPassword = "";
            int L, K;
            int[] P = new int[32];
            int[] F = new int[32];
            RKey = "0231042620191022290301120823283206301118152524132717071605210914";
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            L = origPassword.Length;
            if (L > 16 || L == 0)
            {
                return origPassword;
            }
            else
            {
                for (int i = 1; i <= L; i++)
                {
                    K = Asc(Convert.ToChar(origPassword.Substring(i - 1, 1)));
                    HalfPassword += Int2Hex(K);
                }
                L = HalfPassword.Length;
                values = Chr(88 + L);
                for (int i = 1; i <= 32; i++)
                {
                    F[i - 1] = 0;
                }
                for (int i = 1; i <= L; i++)
                {
                    P[i - 1] = Convert.ToInt32(RKey.Substring(i * 2 - 2, 2));
                    F[P[i - 1] - 1] = 126 - Asc(Convert.ToChar(HalfPassword.Substring(i - 1, 1))) + 40;
                }
                for (int i = 31; i >= 0; i--)
                {
                    if (F[i] == 0)
                    {
                        while (F[i] < 40 || F[i] > 126)
                        {
                            F[i] = Convert.ToInt32(Math.Floor(126 * ran.NextDouble()));
                        }
                    }
                    values = Chr(F[i]) + values;
                }
                return values;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="XPassword"></param>
        /// <returns></returns>
        public static string PasswordDeEncrypt(string XPassword)
        {
            string RKey, RC = "", RC2 = "", S;
            int J, K, L, M;
            RKey = "0231042620191022290301120823283206301118152524132717071605210914";
            if (XPassword.Length != 33)
            {
                RC = XPassword;
                return RC;
            }
            else
            {
                L = Asc(Convert.ToChar(XPassword.Substring(XPassword.Length - 1, 1))) - 88;
                for (int i = 1; i <= L; i++)
                {
                    S = RKey.Substring(i * 2 - 2, 2);
                    J = Convert.ToInt32(S);
                    S = XPassword.Substring(J - 1, 1);
                    M = Asc(Convert.ToChar(S));
                    K = 126 - M + 40;
                    //RC += Convert.ToString(Chr(K));
                    RC += Chr(K);
                }
                for (int i = 1; i <= RC.Length; i += 2)
                {
                    S = RC.Substring(i - 1, 2);
                    K = Hex2Int(S);
                    //RC2 += Convert.ToString(Chr(K));
                    RC2 += Chr(K);
                }
                return RC2;
            }
        }

        //把datatable转换为二维数组
        public static string[,] ConvertDataTableTo2DArray(DataTable table)
        {
            int rowCount = table.Rows.Count;
            int columnCount = table.Columns.Count;
            string[,] array = new string[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    array[i, j] = table.Rows[i][j].ToString();
                }
            }
            return array;
        }

        //取指定字符串前的值
        public static string beforeStr(string str, string comma)
        {
            string beforecomma = "";
            int index = str.IndexOf(comma);
            if (index != -1)
            {
                beforecomma = str.Substring(0, index);
            }
            return beforecomma;

        }

        //取指定字符串后的值
        public static string afterStr(string str, string comma)
        {
            string aftercomma = "";
            int index = str.IndexOf(comma);
            if (index != -1)
            {
                aftercomma = str.Substring(index + 1);
            }
            return aftercomma;

        }
    }
}
