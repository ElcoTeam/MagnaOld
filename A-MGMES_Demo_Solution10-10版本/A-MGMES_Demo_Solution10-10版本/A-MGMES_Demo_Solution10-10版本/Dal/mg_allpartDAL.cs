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
    public class mg_allpartDAL
    {
        public static int AddAllByName(string all_no, string all_ratename, string all_colorname,string all_metaname,string all_desc)
        {
            string sql = @"INSERT INTO [mg_allpart] ([all_no],[all_ratename],[all_colorname],[all_metaname],[all_desc]) VALUES ('" + all_no + "','" + all_ratename + "','" + all_colorname + "','" + all_metaname + "','" + all_desc + @"')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static DataTable GetAllData()
        {
            string sql = @"SELECT [all_id],[all_no],[all_ratename],[all_colorname],[all_metaname],[all_desc] from [mg_allpart] order by [all_id] asc";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static int UpDateAllByName(int allid, string allno, string allrate, string allcolor,string allmeta,string alldesc)
        {
            string sql = @"update [mg_allpart] set [all_no]='" + allno + "',[all_ratename]='" + allrate + "',[all_colorname]= '" + allcolor + "',[all_metaname]='" + allmeta + "',[all_desc]='" + alldesc + "' where [all_id]=" + allid;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int DelAllByName(int allid)
        {
            string sql = @"delete from [mg_allpart] where [all_id]=" + allid;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static int CheckAllByName(int a, int allid, string allno)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"select [all_id],[all_no],[all_ratename],[all_colorname],[all_metaname],[all_desc] from [mg_allpart] where all_no='" + allno + "'";
            }
            if (a == 2)
            {
                sql = @"select [all_id],[all_no],[all_ratename],[all_colorname],[all_metaname],[all_desc] from [mg_allpart] where all_no='" + allno + "' and all_id <> " + allid;
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

        /*
         *      姜任鹏
         * 
         * 
         */
        public static int AddAllPart(Model.mg_allpartModel model)
        {
            string maxid = @"declare @i int;
                                SELECT @i=max([all_id])  FROM [mg_allpart];
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
            strSql.Append("insert into mg_allpart(");
            strSql.Append("all_id,all_no,all_rateid,all_colorid,all_metaid,all_desc)");
            strSql.Append(" values (");
            strSql.Append("@i,@all_no,@all_rateid,@all_colorid,@all_metaid,@all_desc)");
            SqlParameter[] parameters = {
					new SqlParameter("@all_no", SqlDbType.NVarChar),
					new SqlParameter("@all_rateid", SqlDbType.Int),
					new SqlParameter("@all_colorid", SqlDbType.Int),
					new SqlParameter("@all_metaid", SqlDbType.Int),
					new SqlParameter("@all_desc", SqlDbType.NVarChar)};
            parameters[0].Value = model.all_no;
            parameters[1].Value = model.all_rateid;
            parameters[2].Value = model.all_colorid;
            parameters[3].Value = model.all_metaid;
            parameters[4].Value = model.all_desc;


            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql, parameters);
            return rows;
        }

        public static int UpdateAllPart(Model.mg_allpartModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_allpart set ");
            strSql.Append("all_no=@all_no,");
            strSql.Append("all_rateid=@all_rateid,");
            strSql.Append("all_colorid=@all_colorid,");
            strSql.Append("all_metaid=@all_metaid,");
            strSql.Append("all_desc=@all_desc");
            strSql.Append(" where all_id=@all_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@all_id", SqlDbType.Int),
					new SqlParameter("@all_no", SqlDbType.NVarChar),
					new SqlParameter("@all_rateid", SqlDbType.Int),
					new SqlParameter("@all_colorid", SqlDbType.Int),
					new SqlParameter("@all_metaid", SqlDbType.Int),
					new SqlParameter("@all_desc", SqlDbType.NVarChar)};
            parameters[0].Value = model.all_id;
            parameters[1].Value = model.all_no;
            parameters[2].Value = model.all_rateid;
            parameters[3].Value = model.all_colorid;
            parameters[4].Value = model.all_metaid;
            parameters[5].Value = model.all_desc;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static List<mg_allpartModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<mg_allpartModel> list = null;

            string sql1 = @"select count(all_id) total from [mg_allpart];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [all_id]
                                  ,[all_no]
                                  ,[all_rateid]
                                  ,r.prop_name [all_ratename]
                                  ,[all_colorid]
                                  ,c.prop_name [all_colorname]
                                  ,[all_metaid]
                                  ,m.prop_name [all_metaname]
                                  ,[all_desc]
                              FROM  [mg_allpart] a
                              left join mg_Property r on a.all_rateid=r.prop_id
                              left join mg_Property c on a.all_colorid=c.prop_id
                              left join mg_Property m on a.all_metaid=m.prop_id
	                            order by a.all_id desc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_allpartModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_allpartModel model = new mg_allpartModel();

                    model.all_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "all_id"));
                    model.all_no = DataHelper.GetCellDataToStr(row, "all_no");
                    model.all_rateid =NumericParse.StringToInt( DataHelper.GetCellDataToStr(row, "all_rateid"));
                    model.all_ratename = DataHelper.GetCellDataToStr(row, "all_ratename");
                    model.all_colorid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "all_colorid"));
                    model.all_colorname = DataHelper.GetCellDataToStr(row, "all_colorname");
                    model.all_metaid =NumericParse.StringToInt( DataHelper.GetCellDataToStr(row, "all_metaid"));
                    model.all_metaname = DataHelper.GetCellDataToStr(row, "all_metaname");
                    model.all_desc = DataHelper.GetCellDataToStr(row, "all_desc");

                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_allpartModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_allpartModel> list = null;

            string sql1 = @"select count(all_id) total from [mg_allpart];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [all_id]
                                  ,[all_no]
                                  ,[all_rateid]
                                  ,r.prop_name [all_ratename]
                                  ,[all_colorid]
                                  ,c.prop_name [all_colorname]
                                  ,[all_metaid]
                                  ,m.prop_name [all_metaname]
                                  ,[all_desc]
                              FROM  [mg_allpart] a
                              left join mg_Property r on a.all_rateid=r.prop_id
                              left join mg_Property c on a.all_colorid=c.prop_id
                              left join mg_Property m on a.all_metaid=m.prop_id
                                where  a.all_id < (
                                                select top 1 all_id from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") all_id from  [mg_allpart] where all_id is not null  order by all_id desc )t
                                                order by  all_id  )
                                
	                            order by a.all_id desc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_allpartModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_allpartModel model = new mg_allpartModel();

                    model.all_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "all_id"));
                    model.all_no = DataHelper.GetCellDataToStr(row, "all_no");
                    model.all_rateid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "all_rateid"));
                    model.all_ratename = DataHelper.GetCellDataToStr(row, "all_ratename");
                    model.all_colorid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "all_colorid"));
                    model.all_colorname = DataHelper.GetCellDataToStr(row, "all_colorname");
                    model.all_metaid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "all_metaid"));
                    model.all_metaname = DataHelper.GetCellDataToStr(row, "all_metaname");
                    model.all_desc = DataHelper.GetCellDataToStr(row, "all_desc");

                    list.Add(model);
                }
            }
            return list;
        }

        public static int DeleteAllPart(string all_id)
        {
            string sql = @"delete from [mg_allpart] where [all_id]=" + all_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static List<mg_allpartModel> QueryAllPartListForPart()
        {
            List<mg_allpartModel> list = null;

            string sql2 = @" 
                            SELECT  [all_id]
                                  ,[all_no]
                                  ,[all_rateid]
                                  ,r.prop_name [all_ratename]
                                  ,[all_colorid]
                                  ,c.prop_name [all_colorname]
                                  ,[all_metaid]
                                  ,m.prop_name [all_metaname]
                                  ,[all_desc]
                              FROM  [mg_allpart] a
                              left join mg_Property r on a.all_rateid=r.prop_id
                              left join mg_Property c on a.all_colorid=c.prop_id
                              left join mg_Property m on a.all_metaid=m.prop_id
	                            order by a.all_no 

                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text,  sql2,  null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<mg_allpartModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_allpartModel model = new mg_allpartModel();

                    model.all_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "all_id"));
                    model.all_no = DataHelper.GetCellDataToStr(row, "all_no");
                    model.all_rateid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "all_rateid"));
                    model.all_ratename = DataHelper.GetCellDataToStr(row, "all_ratename");
                    model.all_colorid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "all_colorid"));
                    model.all_colorname = DataHelper.GetCellDataToStr(row, "all_colorname");
                    model.all_metaid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "all_metaid"));
                    model.all_metaname = DataHelper.GetCellDataToStr(row, "all_metaname");
                    model.all_desc = DataHelper.GetCellDataToStr(row, "all_desc");

                    list.Add(model);
                }
            }
            return list;
        }
    }
}
