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
    public class mg_ClassesDAL
    {
        public static int AddflByName(string classesname, string starttime, string endtime)
        {
            string maxidSql = @"declare @i int;
                                SELECT @i=max([cl_id])  FROM [mg_classes];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;";
            string sql = @"INSERT INTO [mg_classes] ([cl_id],[cl_name],[cl_starttime],[cl_endtime]) VALUES (@i,'" + classesname + "','" + starttime + "','" + endtime + "')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, maxidSql+sql, null);
        }

        public static DataTable GetAllData()
        {
            string sql = @"select * from [mg_classes] order by [cl_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int UpDateflByName(int cl_id, string cl_name, string stime, string etime)
        {
            string sql = @"update [mg_classes] set cl_name='" + cl_name + "',cl_starttime='" + stime + "',cl_endtime='" + etime + "' where cl_id=" + cl_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int DelflByName(int fl_id)
        {
            string sql = @"delete from [mg_classes] where cl_id=" + fl_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int CheckflByName(int a, int classesid, string classesname)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"select * from [mg_classes] where cl_name='" + classesname + "'";
            }
            if (a == 2)
            {
                sql = @"select * from [mg_classes] where cl_name='" + classesname + "' and cl_id <>" + classesid;
            }
            tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            if (tb.Rows.Count != 0)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
            tb.Dispose();
            return i;
        }
    }
}
