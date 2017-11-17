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
    public class mg_allpart_part_relDAL
    {
        public static int AddAllPartByName(int allid, int partid)
        {
            string sql = @"INSERT INTO [mg_allpart_part_rel] ([all_id],[partid_id]) VALUES (" + allid + "," + partid + @")";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static DataTable GetAllData()
        {
            string sql = @"SELECT dbo.mg_allpart.all_no, dbo.mg_part.part_name FROM  dbo.mg_allpart CROSS JOIN  dbo.mg_part";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
    }
}
