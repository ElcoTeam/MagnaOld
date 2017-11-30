using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbUtility;
using Tools;
using System.Data;
namespace Dal
{
    public class Failure_DAL
    {
        public static int AddFailure(string Name, string Code)
        {
            string sql = "INSERT INTO BadReason (Name, Code) VALUES ('" + Name + "', '" + Code + "')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static int EditFailure(string Name, string Code, string ID)
        {
            string sql = "UPDATE BadReason SET Name = '" + Name + "', Code = '" + Code + "' WHERE ID = '" + ID + "'";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static int DeleteFailure( string ID)
        {
            string sql = "DELETE FROM BadReason WHERE ID = '" + ID + "' ";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static DataTable GetTable()
        {
            string sql = "select * from BadReason ";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
    }
}
