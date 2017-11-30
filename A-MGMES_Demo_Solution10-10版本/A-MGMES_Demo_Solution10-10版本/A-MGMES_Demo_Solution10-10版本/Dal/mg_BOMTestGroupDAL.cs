using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;
using DbUtility;
using Tools;

namespace Dal
{
    public class mg_BOMTestGroupDAL
    {
        public static List<mg_BOMTestGroupModel> queryGroupidForBOMTest()
        {
            List<mg_BOMTestGroupModel> list = null;
            string sql = @"SELECT [ID],[GroupName]  FROM [mg_Test_Group] order by GroupName ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_BOMTestGroupModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_BOMTestGroupModel model = new mg_BOMTestGroupModel();
                    model.group_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                    model.groupname = DataHelper.GetCellDataToStr(row, "groupname");
                    list.Add(model);
                }
            }
            return list;
        }
        public static List<mg_BOMTestGroupModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<mg_BOMTestGroupModel> list = null;

            string sql1 = @"select count(ID) total from [mg_Test_Group];";
            string sql2 = @" SELECT top " + pagesize + @" ID gid
                                      ,GroupName
                                  FROM [mg_Test_Group]                         
                                  order by ID desc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_BOMTestGroupModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_BOMTestGroupModel model = new mg_BOMTestGroupModel();

                    model.group_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "gid"));
                    model.groupname = DataHelper.GetCellDataToStr(row, "GroupName");
                    list.Add(model);
                }
            }
            return list;
        }
        public static List<mg_BOMTestGroupModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_BOMTestGroupModel> list = null;

            string sql1 = @"select count(ID) total from [mg_Test_Group];";
            string sql2 = @" SELECT top " + pagesize + @" ID gid
                                      ,GroupName
                                  FROM [mg_Test_Group] 
                                    where  ID < (
                                                select top 1 ID from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") ID from  [mg_Test_Group] where ID is not null  order by ID desc )t
                                                order by  ID  )
                                  order by ID desc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_BOMTestGroupModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_BOMTestGroupModel model = new mg_BOMTestGroupModel();
                    model.group_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "gid"));
                    model.groupname = DataHelper.GetCellDataToStr(row, "GroupName");
                    list.Add(model);
                }
            }
            return list;
        }
        public static int AddBOMTestGroup(mg_BOMTestGroupModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into mg_Test_Group(");
            strSql.Append("GroupName)");
            strSql.Append(" values (");
            strSql.Append("@GroupName)");
            SqlParameter[] parameters = {
					new SqlParameter("@GroupName", SqlDbType.NVarChar),					
                                        };
            parameters[0].Value = model.groupname;
           
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static int UpdateBOMTestGroup(mg_BOMTestGroupModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_Test_Group set ");
            strSql.Append("GroupName=@GroupName");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@GroupName", SqlDbType.NVarChar),
                                        };
            parameters[0].Value = model.group_id;
            parameters[1].Value = model.groupname;
            
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static int DeleteBOMTestGroup(string group_id)
        {
            string sql = @"delete from [mg_Test_Group] where [ID]=" + group_id + "";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
    }
}
