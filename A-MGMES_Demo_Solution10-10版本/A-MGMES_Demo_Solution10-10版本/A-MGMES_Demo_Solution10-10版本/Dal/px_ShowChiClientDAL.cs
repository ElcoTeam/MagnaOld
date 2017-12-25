using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Tools;
using DbUtility;
using Model;


namespace DAL
{
    public class px_ShowChiClientDAL
    {
       
       
        public static DataTable GetAllData()
        {
            string sql = @"SELECT [SID],[SRole] from [px_ShowChiClient] order by [SID] asc";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
      
        public static int AddShowChiClient(Model.px_ShowChiClientModel model)
        {
            string maxid = @"set identity_insert px_ShowChiClient ON;
                               declare @i int;
                                SELECT @i=max([SID])  FROM [px_ShowChiClient];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into px_ShowChiClient(");
            strSql.Append("SID,SName,ClientIP,SAddTime,SRole)");
            strSql.Append(" values (");
            strSql.Append("@i,@SName,@ClientIP,@SAddTime,@SRole)");
            SqlParameter[] parameters = {
					new SqlParameter("@SName", SqlDbType.NVarChar),
					new SqlParameter("@ClientIP", SqlDbType.NVarChar),
                    new SqlParameter("@SAddTime", SqlDbType.DateTime),
                    new SqlParameter("@SRole", SqlDbType.NVarChar)};
            parameters[0].Value = model.SName;
            parameters[1].Value = model.ClientIP;
            parameters[2].Value =Convert.ToDateTime(model.SAddTime);
            parameters[3].Value = model.SRole;
            string end = @"set identity_insert px_ShowChiClient OFF";

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql + end, parameters);
            return rows;
        }

        public static int UpdateShowChiClient(Model.px_ShowChiClientModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update px_ShowChiClient set ");
            strSql.Append("SName=@SName,");
            strSql.Append("ClientIP=@ClientIP,");
            strSql.Append("SAddTime=@SAddTime,");
            strSql.Append("SRole=@SRole");
            strSql.Append(" where SID=@SID");  
            SqlParameter[] parameters = {
					new SqlParameter("@SName", SqlDbType.NVarChar),
					new SqlParameter("@ClientIP", SqlDbType.NVarChar),
					new SqlParameter("@SAddTime", SqlDbType.DateTime),
					new SqlParameter("@SRole", SqlDbType.NVarChar),
                    new SqlParameter("@SID", SqlDbType.Int)};
            parameters[0].Value = model.SName;
            parameters[1].Value = model.ClientIP;
            parameters[2].Value = Convert.ToDateTime(model.SAddTime);
            parameters[3].Value = model.SRole;
            parameters[4].Value = Convert.ToInt32(model.SID);

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static List< px_ShowChiClientModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List< px_ShowChiClientModel> list = null;

            string sql1 = @"select count(SID) total from [px_ShowChiClient];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [SID]
                                  ,[SName],[ClientIP]
                                  ,[SAddTime],[SRole],[SRamark]
                                 
                              FROM  [px_ShowChiClient] 
	                            order by SID asc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List< px_ShowChiClientModel>();
                foreach (DataRow row in dt2.Rows)
                {
                     px_ShowChiClientModel model = new  px_ShowChiClientModel();
                     model.SID =Convert.ToInt32(DataHelper.GetCellDataToStr(row, "SID"));
                     model.SName = DataHelper.GetCellDataToStr(row, "SName");
                     model.ClientIP = DataHelper.GetCellDataToStr(row, "ClientIP");
                     model.SAddTime = DataHelper.GetCellDataToStr(row, "SAddTime");
                     model.SRole = DataHelper.GetCellDataToStr(row, "SRole");
                     model.SRamark = DataHelper.GetCellDataToStr(row, "SRamark");

                    list.Add(model);
                }
            }
            return list;
        }

        public static List< px_ShowChiClientModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List< px_ShowChiClientModel> list = null;

            string sql1 = @"select count(SID) total from [px_ShowChiClient];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [SID]
                                  ,[SName]
                                  ,[ClientIP],[SAddTime],[SRole],[SRamark]                            
                              FROM  [px_ShowChiClient] 
                                where  SID > (
                                                select top 1 SID from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") SID from [px_ShowChiClient] where SID is not null  order by SID asc )t
                                                order by  SID desc)
                                
	                            order by SID asc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List< px_ShowChiClientModel>();
                foreach (DataRow row in dt2.Rows)
                {
                     px_ShowChiClientModel model = new  px_ShowChiClientModel();

                     model.SID = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "SID"));
                     model.SName = DataHelper.GetCellDataToStr(row, "SName");
                     model.ClientIP = DataHelper.GetCellDataToStr(row, "ClientIP");
                     model.SAddTime = DataHelper.GetCellDataToStr(row, "SAddTime");
                     model.SRole = DataHelper.GetCellDataToStr(row, "SRole");
                     model.SRamark = DataHelper.GetCellDataToStr(row, "SRamark");
                    list.Add(model);
                }
            }
            return list;
        }

        public static int DeleteShowChiClient(string SID)
        {
            string sql = @"delete from [px_ShowChiClient] where [SID]=" + SID;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static List<px_ShowChiClientModel> Querypx_ShowChiClientListForPart()
        {
            List< px_ShowChiClientModel> list = null;

            string sql2 = @" 
                            SELECT [SID]
                                  ,[SName]
                                  ,[ClientIP]
                                  ,[SAddTime]
                                  ,[SRole]
                                  ,[SRamark]
                              FROM  [px_ShowChiClient]             
	                          order by SID asc 
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text,  sql2,  null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List< px_ShowChiClientModel>();
                foreach (DataRow row in dt2.Rows)
                {
                     px_ShowChiClientModel model = new  px_ShowChiClientModel();

                     model.SID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "SID"));
                     model.SName = DataHelper.GetCellDataToStr(row, "SName");
                     model.ClientIP = DataHelper.GetCellDataToStr(row, "ClientIP");
                     model.SAddTime = DataHelper.GetCellDataToStr(row, "SAddTime");
                     model.SRole = DataHelper.GetCellDataToStr(row, "SRole");
                     model.SRamark = DataHelper.GetCellDataToStr(row, "SRamark");
                     list.Add(model);
                }
            }
            return list;
        }
    }
}
