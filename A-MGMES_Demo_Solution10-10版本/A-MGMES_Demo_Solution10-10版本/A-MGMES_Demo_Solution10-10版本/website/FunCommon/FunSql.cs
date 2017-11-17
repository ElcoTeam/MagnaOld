using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DBUtility;

namespace website
{
    class FunSql
    {
        public static SqlConnection Conn;

        public static bool Init()
        {
            try
            {
                //string SqlStr = "server=.;database=MagnaDB;uid=sa;pwd=123456";
                string SqlStr = SqlHelper.SqlConnString;
                Conn = new SqlConnection(SqlStr);
                Conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int Exec(string str)
        {
            //sql固有变量
            string sqlstr = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = Conn;
            //在这里写sql语句
            sqlstr = str;
            cmd.CommandText = sqlstr;

            //执行 简单写法
            try { reader = cmd.ExecuteReader(); }
            catch
            {
                return -1;
            }
            reader.Close();

            return 0;
        }

        public static void Delete(string Table, int ID)
        {
            FunSql.Exec("delete from " + Table + " where id = '" + ID + "'");
        }

        public static void Insert(string Table, string Column, object[] Values)
        {
            string SqlStr = "insert into " + Table + "(" + Column + ") values (";

            for (int i = 0; i < Values.Length; i++)
            {
                SqlStr += "'" + Values[i] + "',";
            }
            SqlStr = SqlStr.Remove(SqlStr.Length - 1);
            SqlStr += ")";

            Exec(SqlStr);
        }

        public static void Update(string Table, int ID, string Column, object[] Values)
        {
            string SqlStr = "update " + Table + " set ";
            string[] columns = FunCommon.CutToArry(Column, ',');
            for (int i = 0; i < columns.Length; i++)
            {
                SqlStr += columns[i] + " = '" + Values[i] + "',";
            }
            SqlStr = SqlStr.Remove(SqlStr.Length - 1);
            SqlStr += " Where ID = '" + ID + "'";

            Exec(SqlStr);
        }

        public static object[] GetValues(string str)
        {
            //sql固有变量
            string sqlstr = null;
            object[] sqlres = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = Conn;

            //在这里写sql语句
            sqlstr = str;
            cmd.CommandText = sqlstr;

            //执行 简单写法
            try { reader = cmd.ExecuteReader(); }
            catch { return null; }
            //取执行结果
            if (reader.HasRows)
            {
                reader.Read();
                try
                {
                    sqlres = new object[reader.FieldCount];
                    reader.GetValues(sqlres);

                    for (int i = 0; i < sqlres.Length; i++)
                    {
                        if (sqlres[i] == DBNull.Value)
                        {
                            sqlres[i] = "";
                        }
                    }
                }
                catch
                {
                    reader.Close();
                    return null;
                }
            }
            reader.Close();

            return sqlres;
        }

        public static object GetValue(string str)
        {
            //sql固有变量
            string sqlstr = null;
            object sqlres = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = Conn;

            //在这里写sql语句
            sqlstr = str;
            cmd.CommandText = sqlstr;

            //执行 简单写法
            try { reader = cmd.ExecuteReader(); }
            catch { return null; }
            //取执行结果
            if (reader.HasRows)
            {
                reader.Read();
                try
                {
                    sqlres = reader.GetValue(0);
                }
                catch
                {
                    reader.Close();
                    return null;
                }
            }
            reader.Close();

            return sqlres;
        }

        public static int GetInt(string str)
        {
            try
            {
                return (int)GetValue(str);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal GetDecimal(string str)
        {
            try
            {
                return (decimal)GetValue(str);
            }
            catch
            {
                return 0;
            }
        }

        public static string GetString(string str)
        {
            try
            {
                return (string)GetValue(str);
            }
            catch
            {
                return "";
            }
        }
        public static DateTime GetDateTime(string str)
        {
            try
            {
                return (DateTime)GetValue(str);
            }
            catch
            {
                return DateTime.Now;
            }
        }


        public static DataTable GetTable(string str)
        {
            try
            {
                SqlDataAdapter sda;
                string sqlstr = str;
                DataSet ds = new DataSet();

                sda = new SqlDataAdapter(sqlstr, Conn);
                sda.Fill(ds, "table1");

                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static int GetNewID(string Table)
        {
            try
            {
                return GetInt("select max(ID) from " + Table + "");
            }
            catch
            {
                return -1;
            }
        }
    }
}
