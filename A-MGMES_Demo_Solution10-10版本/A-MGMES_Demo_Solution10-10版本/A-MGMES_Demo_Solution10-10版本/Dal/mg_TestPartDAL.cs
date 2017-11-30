using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Tools;
using DbUtility;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{
    public class mg_TestPartDAL
    {
        public static List<mg_TestPartModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_TestPartModel> list = null;

            string sql1 = @"select count(ID) total from [mg_Test_Part];";
            string sql2 = @" 
                           SELECT top " + pagesize + @" t1.ID pid
                                      ,p.part_id part_id
                                      ,p.part_name part_name
                                      ,t1.StationNO StationNO
                                      ,t2.TestCaption TestCaption
	                                  ,STUFF((SELECT ','+cast (t3.ID as varchar) from (select t4.ID,t4.TestCaption from mg_Test t4) t3
									   where t1.TestID=ID for xml path('')),1,1,'') tIDs
                                  FROM [mg_Test_Part] t1
                                  left join mg_Test t2 on t1.TestID = t2.ID left join mg_part p on p.part_id= t1.partID
                                     where  t1.ID not in (
                                                        select top ((" + page + @"-1)*" + pagesize + @") ID from  [mg_Test_Part] order by ID desc)
                                         order by t1.ID desc ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_TestPartModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_TestPartModel model = new mg_TestPartModel();

                    model.p_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pid"));
                    model.partid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.partname = DataHelper.GetCellDataToStr(row, "part_name");
                    model.stationno = DataHelper.GetCellDataToStr(row, "StationNO");
                    model.testcaption = DataHelper.GetCellDataToStr(row, "TestCaption");
                    model.tIDS = DataHelper.GetCellDataToStr(row, "tIDs");
                    //model.allpartNOs = DataHelper.GetCellDataToStr(row, "allpartNOs");

                    list.Add(model);
                }
            }
            return list;

        }
        public static List<mg_TestPartModel> QueryListForFirstPage(string pagesize, out string total)
        {

            total = "0";
            List<mg_TestPartModel> list = null;

            string sql1 = @"select count(ID) total from [mg_Test_Part];";
            string sql2 = @" 
                              SELECT top " + pagesize + @" t1.ID pid
                                      ,p.part_id part_id
                                      ,p.part_name part_name
                                      ,StationNO
                                      ,t2.TestCaption TestCaption
	                                  ,STUFF((SELECT ','+cast (t3.ID as varchar) from (select t4.ID,t4.TestCaption from mg_Test t4) t3
									   where t1.TestID=ID for xml path('')),1,1,'') tIDs
                                  FROM [mg_Test_Part] t1
                                  left join mg_Test t2 on t1.TestID = t2.ID left join mg_part p on p.part_id= t1.partID order by pid asc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_TestPartModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_TestPartModel model = new mg_TestPartModel();

                    model.p_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pid"));
                    model.partid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.partname = DataHelper.GetCellDataToStr(row, "part_name");
                    model.stationno = DataHelper.GetCellDataToStr(row, "StationNO");
                    //model.sorting = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Sorting"));
                    model.testcaption = DataHelper.GetCellDataToStr(row, "TestCaption");
                    model.tIDS = DataHelper.GetCellDataToStr(row, "tIDs");
                    //model.allpartNOs = DataHelper.GetCellDataToStr(row, "allpartNOs");

                    list.Add(model);
                }
            }
            return list;

        }
        public static bool AddTestPart(mg_TestPartModel model)
        {
            
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(model.tIDS))
            {
                string partid = model.partid.ToString();
                string[] idArr = model.tIDS.Split(',');
                foreach (string id in idArr)
                {
                    strSql.Append("INSERT INTO [mg_Test_Part](");
                    strSql.Append("TestID,StationNO,PartID)");
                    strSql.Append(" values (");
                    strSql.Append("convert(int, (" + id + ")),'" + model.stationno.Trim() + "',convert(int,(" + model.partid + ")));");
                    
                }
                
            }
          
            return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(),null );
            
                  
        }
        public static bool UpdateTestPart(Model.mg_TestPartModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_Test_Part set ");
            strSql.Append("PartID=@PartID,");
            strSql.Append("StationNO=@StationNO");
            strSql.Append(" where ID=@ID;");

            if (!string.IsNullOrEmpty(model.tIDS))
            {
                strSql.Append("delete from [mg_Test_Part]  where ID=@ID ;");
                string[] idArr = model.tIDS.Split(',');
                foreach (string id in idArr)
                {
                    strSql.Append("INSERT INTO [mg_Test_Part] ([TestID]) VALUES ( convert(int, (" + id + ")));");
                }
            }

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@StationNO", SqlDbType.NVarChar),
					new SqlParameter("@PartID", SqlDbType.Int)};
            parameters[0].Value = model.p_id;
            parameters[1].Value = model.stationno;
            parameters[2].Value = model.partid;


            List<SqlParameter> list = new List<SqlParameter>();
            list.AddRange(parameters);
            return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(), list);

        }
        public static int DeleteTestPart(string p_id)
        {
            string sql = @"delete from [mg_Test_Part] where [ID]=" + p_id + ";";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

    }
}
