using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
using DbUtility;
using Tools;

namespace Dal
{
   public class mg_BOMTestDAL
    {
       public static List<mg_BOMTestModel> QueryListForFirstPage(string pagesize, out string total)
       {
           total = "0";
           List<mg_BOMTestModel> list = null;

           string sql1 = @"select count(ID) total from [mg_Test];";
           string sql2 = @" SELECT top " + pagesize + @" u.ID tid
                                      ,d.ID Groupid
                                      ,d.GroupName GroupName
                                      ,TestPage
                                      , TestType 
                                      , case u.TestType 
                                                        when 1 then '自动测试'
                                                        when 2 then '手动测试'
                                                        end as TestTypeName
                                      ,TestCalculateType
                                      ,case u.TestCalculateType
                                                                   when 1 then '自动计算'
                                                                   when 2 then '程序逻辑处理'
                                                                   end as TestCalculateTypeName
                                      ,TestCaption 
                                      ,TestValueMin 
                                      ,TestValueMax 
                                      ,TestValueIsContain
	  	                              ,case u.TestValueIsContain
		                                                            when 1 then '包含'
		                                                            when 0 then '不包含'
                                                                    end as TestValueIsContainName   
                                      ,TestValueUnit
                                      ,PLCName
                                      ,PLCValueType
                                      ,PLCOutMultiple 
                                  FROM [mg_Test] u
                                  left join mg_Test_Group d on u.TestGroupID = d.ID
                                  order by u.ID desc
                                ";
           DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
           if (DataHelper.HasData(ds))
           {
               DataTable dt1 = ds.Tables["count"];
               total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
               DataTable dt2 = ds.Tables["data"];
               list = new List<mg_BOMTestModel>();
               foreach (DataRow row in dt2.Rows)
               {
                   mg_BOMTestModel model = new mg_BOMTestModel();

                   model.test_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "tid"));
                   model.testgroupname = DataHelper.GetCellDataToStr(row, "GroupName");
                   model.testpage = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "TestPage"));
                   model.testtypename = DataHelper.GetCellDataToStr(row, "TestTypeName");
                   model.testcalculatetypename = DataHelper.GetCellDataToStr(row, "TestCalculateTypeName");
                   model.testcaption = DataHelper.GetCellDataToStr(row, "TestCaption");
                   string TestValueMin = DataHelper.GetCellDataToStr(row, "TestValueMin") == "" ? "0" : DataHelper.GetCellDataToStr(row, "TestValueMin");
                   string TestValueMax = DataHelper.GetCellDataToStr(row, "TestValueMax") == "" ? "0" : DataHelper.GetCellDataToStr(row, "TestValueMax");

                   model.testvaluemin = float.Parse(TestValueMin);
                   model.testvaluemax = float.Parse(TestValueMax);
                   model.testvalueiscontainname = DataHelper.GetCellDataToStr(row, "TestValueIsContainName");
                   model.testvalueunit = DataHelper.GetCellDataToStr(row, "TestValueUnit");
                   model.plcname = DataHelper.GetCellDataToStr(row, "PLCName");
                   model.plcvaluetype = DataHelper.GetCellDataToStr(row, "PLCValueType");
                   model.plcoutmultiple = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "PLCOutMultiple"));
                   model.testtype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "TestType"));
                   model.testcalculatetype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "TestCalculateType"));
                   model.testvalueiscontain = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "TestValueIsContain"));
                   model.testgroupid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Groupid"));
                   list.Add(model);
               }
           }
           return list;
       }
       public static List<mg_BOMTestModel> QueryListForPaging(string page, string pagesize, out string total)
       {
           total = "0";
           List<mg_BOMTestModel> list = null;

           string sql1 = @"select count(ID) total from [mg_Test];";
           string sql2 = @"SELECT top " + pagesize + @" u.ID tid
                                      ,d.ID Groupid
                                      ,d.GroupName GroupName
                                      ,TestPage
                                      , TestType 
                                      , case u.TestType 
                                                        when 1 then '自动测试'
                                                        when 2 then '手动测试'
                                                        end as TestTypeName
                                      ,TestCalculateType
                                      ,case u.TestCalculateType
                                                                   when 1 then '自动计算'
                                                                   when 2 then '程序逻辑处理'
                                                                   end as TestCalculateTypeName
                                      ,TestCaption 
                                      ,TestValueMin 
                                      ,TestValueMax 
                                      ,TestValueIsContain
	  	                              ,case u.TestValueIsContain
		                                                            when 1 then '包含'
		                                                            when 0 then '不包含'
                                                                    end as TestValueIsContainName   
                                      ,TestValueUnit
                                      ,PLCName
                                      ,PLCValueType
                                      ,PLCOutMultiple 
                                  FROM [mg_Test] u
                                  left join mg_Test_Group d on u.TestGroupID = d.ID
                                    where  u.ID < (
                                                select top 1 ID from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") ID from  [mg_Test] where ID is not null  order by ID desc )t
                                                order by  ID  )
                                  order by u.ID desc
                                ";
           DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
           if (DataHelper.HasData(ds))
           {
               DataTable dt1 = ds.Tables["count"];
               total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
               DataTable dt2 = ds.Tables["data"];
               list = new List<mg_BOMTestModel>();
               foreach (DataRow row in dt2.Rows)
               {
                   mg_BOMTestModel model = new mg_BOMTestModel();

                   model.test_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "tid"));
                   model.testgroupname = DataHelper.GetCellDataToStr(row, "GroupName");
                   model.testpage = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "TestPage"));
                   model.testtypename = DataHelper.GetCellDataToStr(row, "TestTypeName");
                   model.testcalculatetypename = DataHelper.GetCellDataToStr(row, "TestCalculateTypeName");
                   model.testcaption = DataHelper.GetCellDataToStr(row, "TestCaption");
                   string TestValueMin = DataHelper.GetCellDataToStr(row, "TestValueMin") == "" ? "0" : DataHelper.GetCellDataToStr(row, "TestValueMin");
                   string TestValueMax = DataHelper.GetCellDataToStr(row, "TestValueMax") == "" ? "0" : DataHelper.GetCellDataToStr(row, "TestValueMax");

                   model.testvaluemin = float.Parse(TestValueMin);
                   model.testvaluemax = float.Parse(TestValueMax);
                   model.testvalueiscontainname = DataHelper.GetCellDataToStr(row, "TestValueIsContainName");
                   model.testvalueunit = DataHelper.GetCellDataToStr(row, "TestValueUnit");
                   model.plcname = DataHelper.GetCellDataToStr(row, "PLCName");
                   model.plcvaluetype = DataHelper.GetCellDataToStr(row, "PLCValueType");
                   model.plcoutmultiple = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "PLCOutMultiple"));
                   model.testtype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "TestType"));
                   model.testcalculatetype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "TestCalculateType"));
                   model.testvalueiscontain = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "TestValueIsContain"));
                   model.testgroupid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Groupid"));
                   list.Add(model);
               }
           }
           return list;
       }
       public static int AddBOMTest(mg_BOMTestModel model)
       {
           StringBuilder strSql = new StringBuilder();
           
           strSql.Append("insert into mg_Test(");
           strSql.Append("TestGroupID,TestPage,TestType,TestCalculateType,TestCaption,TestValueMin,TestValueMax,TestValueIsContain,TestValueUnit,PLCName,PLCValueType,PLCOutMultiple)");
           strSql.Append(" values (");
           strSql.Append("@TestGroupID,@TestPage,@TestType,@TestCalculateType,@TestCaption,Convert(decimal(18,2),@TestValueMin),Convert(decimal(18,2),@TestValueMax),@TestValueIsContain,@TestValueUnit,@PLCName,@PLCValueType,@PLCOutMultiple)");
           SqlParameter[] parameters = {
					new SqlParameter("@TestGroupID", SqlDbType.Int),
					new SqlParameter("@TestPage", SqlDbType.Int),
					new SqlParameter("@TestType", SqlDbType.Int),
					new SqlParameter("@TestCalculateType", SqlDbType.Int),
					new SqlParameter("@TestCaption", SqlDbType.NVarChar),
					new SqlParameter("@TestValueMin", SqlDbType.Float),
					new SqlParameter("@TestValueMax", SqlDbType.Float),
					new SqlParameter("@TestValueIsContain", SqlDbType.Int),
					new SqlParameter("@TestValueUnit", SqlDbType.NVarChar),
                    new SqlParameter("@PLCName", SqlDbType.NVarChar),
                    new SqlParameter("@PLCValueType", SqlDbType.NVarChar),
                    new SqlParameter("@PLCOutMultiple", SqlDbType.Float)
                                        };
           parameters[0].Value = model.testgroupid;
           parameters[1].Value = model.testpage;
           parameters[2].Value = model.testtype;
           parameters[3].Value = model.testcalculatetype;
           parameters[4].Value = model.testcaption;
           parameters[5].Value = model.testvaluemin;
           parameters[6].Value = model.testvaluemax;
           parameters[7].Value = model.testvalueiscontain;
           parameters[8].Value = model.testvalueunit;
           parameters[9].Value = model.plcname;
           parameters[10].Value = model.plcvaluetype;
           parameters[11].Value = model.plcoutmultiple;

           int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
           return rows;
       }
       public static int UpdateBOMTest(mg_BOMTestModel model)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("update mg_Test set ");
           strSql.Append("TestGroupID=@TestGroupID,");
           strSql.Append("TestPage=@TestPage,");
           strSql.Append("TestType=@TestType,");
           strSql.Append("TestCalculateType=@TestCalculateType,");
           strSql.Append("TestCaption=@TestCaption,");
           strSql.Append("TestValueMin=Convert(decimal(18,2),@TestValueMin),");
           strSql.Append("TestValueMax=Convert(decimal(18,2),@TestValueMax),");
           strSql.Append("TestValueIsContain=@TestValueIsContain,");
           strSql.Append("TestValueUnit=@TestValueUnit,");
           strSql.Append("PLCName=@PLCName,");
           strSql.Append("PLCValueType=@PLCValueType,");
           strSql.Append("PLCOutMultiple=@PLCOutMultiple");
           strSql.Append(" where ID=@ID");
           SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@TestGroupID", SqlDbType.Int),
					new SqlParameter("@TestPage", SqlDbType.Int),
					new SqlParameter("@TestType", SqlDbType.Int),
					new SqlParameter("@TestCalculateType", SqlDbType.Int),
					new SqlParameter("@TestCaption", SqlDbType.NVarChar),
					new SqlParameter("@TestValueMin", SqlDbType.Float),
					new SqlParameter("@TestValueMax", SqlDbType.Float),
					new SqlParameter("@TestValueIsContain", SqlDbType.Int),
					new SqlParameter("@TestValueUnit", SqlDbType.NVarChar),
                    new SqlParameter("@PLCName", SqlDbType.NVarChar),
                    new SqlParameter("@PLCValueType", SqlDbType.NVarChar),
                    new SqlParameter("@PLCOutMultiple", SqlDbType.Float)
                                        };
           parameters[0].Value = model.test_id;
           parameters[1].Value = model.testgroupid;
           parameters[2].Value = model.testpage;
           parameters[3].Value = model.testtype;
           parameters[4].Value = model.testcalculatetype;
           parameters[5].Value = model.testcaption;
           parameters[6].Value = model.testvaluemin;
           parameters[7].Value = model.testvaluemax;
           parameters[8].Value = model.testvalueiscontain;
           parameters[9].Value = model.testvalueunit;
           parameters[10].Value = model.plcname;
           parameters[11].Value = model.plcvaluetype;
           parameters[12].Value = model.plcoutmultiple;


           int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
           return rows;
       }
       public static int DeleteBOMTest(string test_id)
       {
           string sql = @"delete from [mg_Test] where [ID]=" + test_id + "";
           return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
       }
       
    }
}
