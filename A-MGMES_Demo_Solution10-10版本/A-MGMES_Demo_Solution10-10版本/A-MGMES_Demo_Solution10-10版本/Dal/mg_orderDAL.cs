using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Tools;
using DBUtility;


namespace DAL
{
    public class mg_orderDAL
    {
        public static int Addor(string cono,string or_no,string allno, int orcount)
        {
//            string maxidSql = @"declare @i int;
//                                SELECT @i=max([or_no])  FROM [mg_Order];
//                                if @i is null
//                                    begin
//                                        set @i=1
//                                    end
//                                else
//                                    begin
//                                        set @i=@i+1

            string sql = @"INSERT INTO [mg_Order] ([co_no],[or_no],[all_no],[or_count]) VALUES ('" + cono + "','"+or_no+"','" + allno + "','" + orcount + @"')";

            //string sql = @"INSERT INTO [mg_Order] ([co_no],[all_no],[or_count]) VALUES ('" + cono + "','" + allno + "','" + orcount + @"')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetAllData()
        {
            string sql = @"SELECT or_id,co_no,or_no,all_no,or_count from [mg_Order] order by [or_id] asc";
            
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int UpDateor(int coid)
        {
            string sql = @"update [mg_CustomerOrder] set [co_state]=1 where [co_id]=" + coid;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
    }
}
