using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace 逅厨系统辅助工具
{
    class SQLHelperJD
    {
        private static string ip = "server=" + StrHelper.PasswordDeEncrypt(ConfigurationManager.AppSettings["ip"]) +",10117"+ ";";
        private static string outip = "server=" + StrHelper.PasswordDeEncrypt(ConfigurationManager.AppSettings["outip"]) + ",5998" + ";";
        private static string username = "uid=" + StrHelper.PasswordDeEncrypt(ConfigurationManager.AppSettings["username"]) + ";";
        private static string password = "pwd=" + StrHelper.PasswordDeEncrypt(ConfigurationManager.AppSettings["password"]) + ";";
        private static string database = "database=" + StrHelper.PasswordDeEncrypt(ConfigurationManager.AppSettings["database2"]);

        private static string connectionStr = ip + username + password + database;
        private static string connectionStrout = outip + username + password + database;

        public static SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection(MyVar.IsOutNet ? connectionStrout : connectionStr);
            return conn;
        }

        /// <summary>
        /// 返回DateSet数据集
        /// </summary>
        /// <param name="eMessage">错误信息</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, params SqlParameter[] parameters)
        {
            //using (SqlConnection conn = new SqlConnection(connectionStr))
            using (SqlConnection conn = GetConn())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        da.Fill(ds);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        da.Dispose();
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="eMessage">错误信息</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static int GetCount(string sql, params SqlParameter[] parameters)
        {
            int nCount = 0;
            using (SqlConnection conn = GetConn())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    try
                    {
                        Console.WriteLine(cmd.CommandText);
                        object obj = cmd.ExecuteScalar();
                        if (obj != null)
                        {
                            nCount = int.Parse(obj.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                    return nCount;
                }
            }
        }

        /// <summary>
        /// 执行SQL语句（增加 删除 修改）
        /// </summary>
        /// <param name="eMessage">错误信息</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static bool ExecSql(string sql, params SqlParameter[] parameters)
        {
            bool flag = false;
            using (SqlConnection conn = GetConn())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    try
                    {
                        cmd.ExecuteNonQuery();
                        flag = true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                    return flag;
                }
            }
        }

        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object GetFirstRow(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConn())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    try
                    {
                        return cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        cmd.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 生成SQL参数
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="Value">值</param>
        /// <returns></returns>
        public static SqlParameter MakeParam(string ParamName, string Value)
        {
            return new SqlParameter(ParamName, ToDBValue(Value));

        }

        public static SqlParameter MakeParam(string ParamName, DateTime Value)
        {
            return new SqlParameter(ParamName, ToDBValue(Value));
        }

        public static SqlParameter MakeParam(string ParamName, int Value)
        {
            return new SqlParameter(ParamName, ToDBValue(Value));
        }

        public static SqlParameter MakeParam(string ParamName, float Value)
        {
            return new SqlParameter(ParamName, ToDBValue(Value));
        }

        /// <summary>
        /// 把数据库中的NULL类型转换为null值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object FromDBValue(object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 把要插入的null类型转换成数据库中可识别的NULL类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ToDBValue(object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 通过存储过程读取数据
        /// </summary>

        public static DataSet GetDataSetPROC(string proc, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConn())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        da.Fill(ds);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        da.Dispose();
                    }
                    return ds;
                }
            }
        }
    }
}
