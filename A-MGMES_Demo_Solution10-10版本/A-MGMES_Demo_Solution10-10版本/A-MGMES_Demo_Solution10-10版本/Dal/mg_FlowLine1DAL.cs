using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using Tools;

namespace Dal
{
    public class mg_FlowLine1DAL
    {
       
        public static List<mg_FlowlingModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<mg_FlowlingModel> list = null;

            string sql1 = @"select count(fl_id) total from [mg_FlowLine];";
            string sql2 = @" SELECT top " + pagesize + @" fl_id fid
                                      ,fl_name
                                      ,FlowLineType
                                      ,case FlowLineType
                                           when 1 then'前排'
                                           when 2 then '后排' end as FLTName
                                  FROM [mg_FlowLine]                         
                                  order by fl_id desc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_FlowlingModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_FlowlingModel model = new mg_FlowlingModel();
                    model.fl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fid"));
                    model.fl_name = DataHelper.GetCellDataToStr(row, "fl_name");
                    model.fltname = DataHelper.GetCellDataToStr(row, "FLTName");
                    model.flowlinetype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "FlowLineType"));
                    list.Add(model);
                }
            }
            return list;
        }
        public static List<mg_FlowlingModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_FlowlingModel> list = null;

            string sql1 = @"select count(fl_id) total from [mg_FlowLine];";
            string sql2 = @" SELECT top " + pagesize + @" fl_id fid
                                      ,fl_name
                                      ,FlowLineType
                                      ,case FlowLineType
                                           when 1 then'前排'
                                           when 2 then '后排' end as FLTName
                                  FROM [mg_FlowLine] 
                                    where  fl_id not in
                                                        (select top ((" + page + @"-1)*" + pagesize + @") fl_id from  [mg_FlowLine] order by fl_id desc)
                                  order by fl_id desc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_FlowlingModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_FlowlingModel model = new mg_FlowlingModel();
                    model.fl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fid"));
                    model.fl_name = DataHelper.GetCellDataToStr(row, "fl_name");
                    model.fltname = DataHelper.GetCellDataToStr(row, "FLTName");
                    model.flowlinetype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "flowlinetype"));
                    list.Add(model);
                }
            }
            return list;
        }
        public static List<mg_FlowlingModel> queryFlowLineidForPart()
        {
            List<mg_FlowlingModel> list = null;
            string sql = @"SELECT [fl_id],[fl_name]  FROM [mg_FlowLine] order by fl_name ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_FlowlingModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_FlowlingModel model = new mg_FlowlingModel();
                    model.fl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fl_id"));
                    model.fl_name = DataHelper.GetCellDataToStr(row, "fl_name");
                    list.Add(model);
                }
            }
            return list;
        }
        public static int AddFlowLine1(mg_FlowlingModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into mg_FlowLine(");
            strSql.Append("fl_name,");
            strSql.Append("FlowLineType)");
            strSql.Append(" values (");
            strSql.Append("@fl_name,");
            strSql.Append("@FlowLineType)");
            SqlParameter[] parameters = {
					new SqlParameter("@fl_name", SqlDbType.NVarChar),
					new SqlParameter("@FlowLineType", SqlDbType.Int),
                                        };
            parameters[0].Value = model.fl_name;
            parameters[1].Value = model.flowlinetype;
           
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static int UpdateFlowLine1(mg_FlowlingModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_FlowLine set ");
            strSql.Append("fl_name=@fl_name,");
            strSql.Append("FlowLineType=@FlowLineType");
            strSql.Append(" where fl_id=@fl_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@fl_id", SqlDbType.Int),
					new SqlParameter("@fl_name", SqlDbType.NVarChar),
                    new SqlParameter("@FlowLineType",SqlDbType.Int),
                                        };
            parameters[0].Value = model.fl_id;
            parameters[1].Value = model.fl_name;
            parameters[2].Value = model.flowlinetype;
            
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static int DeleteFlowLine1(string fl_id)
        {
            string sql = @"delete from [mg_FlowLine] where [fl_id]=" + fl_id + "";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
    }
}
