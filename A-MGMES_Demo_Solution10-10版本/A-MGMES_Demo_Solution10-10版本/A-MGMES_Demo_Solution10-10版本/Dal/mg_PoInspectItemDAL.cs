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
    public class mg_PoInspectItemDAL
    {
       
        public static List<mg_PoInspectItemModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<mg_PoInspectItemModel> list = null;

            string sql1 = @"select count(ID) total from [mg_PointInspection_Item];";
            string sql2 = @" SELECT top " + pagesize + @" ID pid
                                      ,PI_Item
                                      ,PI_ItemDescribe
                                  FROM [mg_PointInspection_Item]                         
                                  order by ID asc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_PoInspectItemModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_PoInspectItemModel model = new mg_PoInspectItemModel();

                    model.pi_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pid"));
                    model.piitem = DataHelper.GetCellDataToStr(row, "PI_Item");
                    model.piitemdescribe = DataHelper.GetCellDataToStr(row, "PI_ItemDescribe");
                    list.Add(model);
                }
            }
            return list;
        }
        public static List<mg_PoInspectItemModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_PoInspectItemModel> list = null;

            string sql1 = @"select count(ID) total from [mg_PointInspection_Item];";
            string sql2 = @" SELECT top " + pagesize + @" ID pid
                                      ,PI_Item
                                      ,PI_ItemDescribe
                                  FROM [mg_PointInspection_Item]
                                    where  ID not in (
                                               select top ((" + page + @"-1)*" + pagesize + @") ID from  [mg_PointInspection_Item]
                                                            order by ID)
                                  order by ID
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_PoInspectItemModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_PoInspectItemModel model = new mg_PoInspectItemModel();
                    model.pi_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pid"));
                    model.piitem = DataHelper.GetCellDataToStr(row, "PI_Item");
                    model.piitemdescribe = DataHelper.GetCellDataToStr(row, "PI_ItemDescribe");
                    list.Add(model);
                }
            }
            return list;
        }
        public static int AddPoInstpectItem(mg_PoInspectItemModel model)
        {
            StringBuilder strSql = new StringBuilder();
            
            strSql.Append("insert into mg_PointInspection_Item(");
            strSql.Append("PI_Item,PI_ItemDescribe)");
            strSql.Append(" values (");
            strSql.Append("@PI_Item,@PI_ItemDescribe)");
            SqlParameter[] parameters = {
					new SqlParameter("@PI_Item", SqlDbType.NVarChar),	
				    new SqlParameter("@PI_ItemDescribe", SqlDbType.NVarChar),
                                        };
            parameters[0].Value = model.piitem;
            parameters[1].Value = model.piitemdescribe;
           
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static int UpdatePoInstpectItem(mg_PoInspectItemModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_PointInspection_Item set ");
            strSql.Append("PI_Item=@PI_Item,");
            strSql.Append("PI_ItemDescribe=@PI_ItemDescribe");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@PI_Item", SqlDbType.NVarChar),
                    new SqlParameter("@PI_ItemDescribe", SqlDbType.NVarChar),
                                        };
            parameters[0].Value = model.pi_id;
            parameters[1].Value = model.piitem;
            parameters[2].Value = model.piitemdescribe;
            
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static int DeletePoInspectItem(string pi_id)
        {
            string sql = @"delete from [mg_PointInspection_Item] where [ID]=" + pi_id + "";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        
    }
}
