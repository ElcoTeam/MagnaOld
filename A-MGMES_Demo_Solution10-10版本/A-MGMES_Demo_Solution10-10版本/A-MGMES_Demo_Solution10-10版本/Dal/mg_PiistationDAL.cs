using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Tools;
using DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{
    public class mg_PiistationDAL
    {
        public static List<mg_PiistationModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_PiistationModel> list = null;

            string sql1 = @"select count(ID) total from [mg_PointInspection_Item_StationNo];";
            string sql2 = @" 
                            with data as 
                                  (
	                                 select t1.ID,t1.PI_Item,t1.PI_ItemDescribe from mg_PointInspection_Item t1
	                                 )
                                SELECT top " + pagesize + @" p.ID pid
                                      ,p.PI_ID
                                      ,StationNO
                                      ,[Sorting]
                                      ,p2.PI_Item PI_Item
	                                  ,STUFF((SELECT ','+cast (t1.ID as varchar) from data t1 where t1.ID=p.PI_ID for xml path('')),1,1,'') piIDs
                                  FROM [mg_PointInspection_Item_StationNo] p
                                  left join mg_PointInspection_Item p2 on p.PI_ID = p2.ID
                                     where  p.ID not in (
                                                        select top ((" + page + @"-1)*" + pagesize + @") ID from  [mg_PointInspection_Item_StationNo] order by ID desc)
                                         order by p.ID desc ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_PiistationModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_PiistationModel model = new mg_PiistationModel();

                    model.ps_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pid"));
                    model.station_no = DataHelper.GetCellDataToStr(row, "StationNO");
                    model.sorting = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Sorting"));
                    model.piitem = DataHelper.GetCellDataToStr(row, "PI_Item");
                    model.piIDs = DataHelper.GetCellDataToStr(row, "piIDs");
                    //model.allpartNOs = DataHelper.GetCellDataToStr(row, "allpartNOs");

                    list.Add(model);
                }
            }
            return list;

        }
        public static List<mg_PiistationModel> QueryListForFirstPage(string pagesize, out string total)
        {

            total = "0";
            List<mg_PiistationModel> list = null;

            string sql1 = @"select count(ID) total from [mg_PointInspection_Item_StationNo];";
            string sql2 = @" 
                               with data as 
                                  (
	                                 select t1.ID,t1.PI_Item,t1.PI_ItemDescribe from mg_PointInspection_Item t1
	                                 )
                                SELECT top " + pagesize + @" p.ID pid
                                      ,p.PI_ID
                                      ,StationNO
                                      ,[Sorting]
                                      ,p2.PI_Item PI_Item
	                                  ,STUFF((SELECT ','+cast (t1.ID as varchar) from data t1 where t1.ID=p.PI_ID for xml path('')),1,1,'') piIDs
                                  FROM [mg_PointInspection_Item_StationNo] p
                                  left join mg_PointInspection_Item p2 on p.PI_ID = p2.ID
                                  order by p.ID desc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_PiistationModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_PiistationModel model = new mg_PiistationModel();

                    model.ps_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pid"));
                    model.station_no = DataHelper.GetCellDataToStr(row, "StationNO");
                    model.sorting = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Sorting"));
                    model.piitem = DataHelper.GetCellDataToStr(row, "PI_Item");
                    model.piIDs = DataHelper.GetCellDataToStr(row, "piIDs");
                    //model.allpartNOs = DataHelper.GetCellDataToStr(row, "allpartNOs");

                    list.Add(model);
                }
            }
            return list;

        }
        public static bool AddPiiStation(mg_PiistationModel model)
        {
            
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(model.piIDs))
            {
                string[] idArr = model.piIDs.Split(',');
                foreach (string id in idArr)
                {
                    strSql.Append("INSERT INTO [mg_PointInspection_Item_StationNo](");
                    strSql.Append("PI_ID,StationNO,Sorting)");
                    strSql.Append(" values (");
                    strSql.Append("convert(int, (" + id + ")),"+model.station_no+",convert(int,("+model.sorting+")));");
                    
                }
                
            }
          
            return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(),null );
            
                  
        }
        public static bool UpdatePiiStation(Model.mg_PiistationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("update mg_PointInspection_Item_StationNo set ");
            //strSql.Append("StationNO=@StationNO,");
            //strSql.Append("Sorting=@Sorting");
            //strSql.Append(" where ID=@ID ;");

            if (!string.IsNullOrEmpty(model.piIDs))
            {
                strSql.Append("delete from [mg_PointInspection_Item_StationNo]  where ID=@ID ;");
                string[] idArr = model.piIDs.Split(',');
                foreach (string id in idArr)
                {
                    strSql.Append("INSERT INTO [mg_PointInspection_Item_StationNo] ([PI_ID],StationNO,Sorting) VALUES ( convert(int, (" + id + ")),@StationNO,@Sorting);");
                }
            }

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@StationNO", SqlDbType.NVarChar),
					new SqlParameter("@Sorting", SqlDbType.Int)};
            parameters[0].Value = model.ps_id;
            parameters[1].Value = model.station_no;
            parameters[2].Value = model.sorting;


            List<SqlParameter> list = new List<SqlParameter>();
            list.AddRange(parameters);
            return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(), list);

        }
        public static int DeletePiistation(string ps_id)
        {
            string sql = @"delete from [mg_PointInspection_Item_StationNo] where [ID]=" + ps_id + ";";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

    }
}
