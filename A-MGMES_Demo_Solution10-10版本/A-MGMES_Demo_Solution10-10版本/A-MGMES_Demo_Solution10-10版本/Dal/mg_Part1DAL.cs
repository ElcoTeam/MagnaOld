using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
using DBUtility;
using Tools;

namespace Dal
{
    public class mg_Part1DAL
    {
       public static List<mg_partModel> QueryListForFirstPage(string pagesize, out string total)
       {
           total = "0";
           List<mg_partModel> list = null;

           string sql1 = @"select count(part_id) total from [mg_part];";
           string sql2 = @" SELECT top " + pagesize + @" a.part_id pid
                                      ,a.part_no part_no
                                      ,a.part_name part_name
                                      ,a.part_desc part_desc
                                      ,a.PartType
                                      ,case a.PartType
                                                   when 1 then '主驾靠背'
                                                   when 2 then '主驾坐垫'
                                                   when 3 then '主驾总'
                                                   when 4 then '副驾靠背'
                                                   when 5 then '副驾坐垫'
                                                   when 6 then '副驾总'
                                                   when 7 then '后排40%'
                                                   when 8 then '后排60%'
                                                   when 9 then '后排100%'
                                                   end as PartTypeName
                                      ,b.prop_id Propid
                                      ,b.prop_name PropName
                                      ,c.fl_id
                                      ,c.fl_name fl_name
                                      ,d.ID ProductID
                                      ,d.ProductName ProductName 
                                  FROM [mg_part] a
                                  left join mg_Property b on a.part_categoryid = b.prop_id left join mg_FlowLine c on a.FlowLineID = c.fl_id
                                  left join mg_Product d on a.ProductID = d.ID where d.IsUseing=1
                                  order by a.part_id desc
                                ";
           DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
           if (DataHelper.HasData(ds))
           {
               DataTable dt1 = ds.Tables["count"];
               total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
               DataTable dt2 = ds.Tables["data"];
               list = new List<mg_partModel>();
               foreach (DataRow row in dt2.Rows)
               {
                   mg_partModel model = new mg_partModel();

                   model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pid"));
                   model.part_no = DataHelper.GetCellDataToStr(row, "part_no");
                   model.part_name = DataHelper.GetCellDataToStr(row, "part_name");
                   model.part_desc = DataHelper.GetCellDataToStr(row, "part_desc");
                   model.parttypename = DataHelper.GetCellDataToStr(row, "PartTypeName");
                   model.propname = DataHelper.GetCellDataToStr(row, "PropName");
                   model.pflowlinename = DataHelper.GetCellDataToStr(row, "fl_name");
                   model.pproductname = DataHelper.GetCellDataToStr(row, "ProductName");
                   model.propid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Propid"));
                   model.part_type = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "PartType"));
                   model.pflowlineid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fl_id"));
                   model.pproductid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ProductID"));
                   list.Add(model);
               }
           }
           return list;
       }
       public static List<mg_partModel> QueryListForPaging(string page, string pagesize, out string total)
       {
           total = "0";
           List<mg_partModel> list = null;

           string sql1 = @"select count(part_id) total from [mg_part];";
           string sql2 = @"SELECT top " + pagesize + @" a.part_id pid
                                      ,a.part_no part_no
                                      ,a.part_name part_name
                                      ,a.part_desc part_desc
                                      ,a.PartType
                                      ,case a.PartType
                                                   when 1 then '主驾靠背'
                                                   when 2 then '主驾坐垫'
                                                   when 3 then '主驾总'
                                                   when 4 then '副驾靠背'
                                                   when 5 then '副驾坐垫'
                                                   when 6 then '副驾总'
                                                   when 7 then '后排40%'
                                                   when 8 then '后排60%'
                                                   when 9 then '后排100%'
                                                   end as PartTypeName
                                      ,b.prop_id Propid
                                      ,b.prop_name PropName
                                      ,c.fl_id
                                      ,c.fl_name fl_name
                                      ,d.ID ProductID
                                      ,d.ProductName ProductName 
                                  FROM [mg_part] a
                                  left join mg_Property b on a.part_categoryid = b.prop_id left join mg_FlowLine c on a.FlowLineID = c.fl_id
                                  left join mg_Product d on a.ProductID = d.ID
                                    where d.IsUseing=1 and a.part_id not in
                                                        (select top ((" + page + @"-1)*" + pagesize + @") part_id from  [mg_part]  order by part_id desc)
                                       order by a.part_id desc
                                ";
           DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
           if (DataHelper.HasData(ds))
           {
               DataTable dt1 = ds.Tables["count"];
               total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
               DataTable dt2 = ds.Tables["data"];
               list = new List<mg_partModel>();
               foreach (DataRow row in dt2.Rows)
               {
                   mg_partModel model = new mg_partModel();

                    model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pid"));
                    model.part_no = DataHelper.GetCellDataToStr(row, "part_no");
                    model.part_name = DataHelper.GetCellDataToStr(row, "part_name");
                    model.part_desc = DataHelper.GetCellDataToStr(row, "part_desc");
                    model.parttypename = DataHelper.GetCellDataToStr(row, "PartTypeName");
                    model.propname = DataHelper.GetCellDataToStr(row, "PropName");
                    model.pflowlinename = DataHelper.GetCellDataToStr(row, "fl_name");
                    model.pproductname = DataHelper.GetCellDataToStr(row, "ProductName");
                    model.propid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Propid"));
                    model.part_type = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "PartType"));
                    model.pflowlineid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fl_id"));
                    model.pproductid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ProductID"));
                    list.Add(model);
               }
           }
           return list;
       }
       public static int AddPart1(mg_partModel model)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append(@"declare @i int;
                                SELECT @i=max([part_id])  FROM [mg_part];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                ");
           strSql.Append("insert into mg_part(");
           strSql.Append("part_id,part_no,part_name,part_desc,part_categoryid,PartType,FlowLineID,ProductID)");
           strSql.Append(" values (");
           strSql.Append("@i,@part_no,@part_name,@part_desc,@part_categoryid,@PartType,@FlowLineID,@ProductID)");
           SqlParameter[] parameters = {
					new SqlParameter("@part_id", SqlDbType.Int),
					new SqlParameter("@part_no", SqlDbType.NVarChar),
					new SqlParameter("@part_name", SqlDbType.NVarChar),
					new SqlParameter("@part_desc", SqlDbType.NVarChar),
					new SqlParameter("@part_categoryid", SqlDbType.Int),
					new SqlParameter("@PartType", SqlDbType.Int),
					new SqlParameter("@FlowLineID", SqlDbType.Int),
					new SqlParameter("@ProductID", SqlDbType.Int)};
           parameters[0].Value = model.part_id;
           parameters[1].Value = model.part_no;
           parameters[2].Value = model.part_name;
           parameters[3].Value = model.part_desc;
           parameters[4].Value = model.part_categoryid;
           parameters[5].Value = model.part_type;
           parameters[6].Value = model.pflowlineid;
           parameters[7].Value = model.pproductid;
           
           int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
           return rows;
       }
       public static int UpdatePart1(mg_partModel model)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("update mg_part set ");
           strSql.Append("part_no=@part_no,");
           strSql.Append("part_name=@part_name,");
           strSql.Append("part_desc=@part_desc,");
           strSql.Append("part_categoryid=@part_categoryid,");
           strSql.Append("PartType=@PartType,");
           strSql.Append("FlowLineID=@FlowLineID,");
           strSql.Append("ProductID=@ProductID");
           strSql.Append(" where part_id=@part_id");
           SqlParameter[] parameters = {
                    new SqlParameter("@part_id", SqlDbType.Int),
					new SqlParameter("@part_no", SqlDbType.NVarChar),
					new SqlParameter("@part_name", SqlDbType.NVarChar),
					new SqlParameter("@part_desc", SqlDbType.NVarChar),
					new SqlParameter("@part_categoryid", SqlDbType.Int),
					new SqlParameter("@PartType", SqlDbType.Int),
					new SqlParameter("@FlowLineID", SqlDbType.Int),
					new SqlParameter("@ProductID", SqlDbType.Int)};
                                        
            parameters[0].Value = model.part_id;
           parameters[1].Value = model.part_no;
           parameters[2].Value = model.part_name;
           parameters[3].Value = model.part_desc;
           parameters[4].Value = model.part_categoryid;
           parameters[5].Value = model.part_type;
           parameters[6].Value = model.pflowlineid;
           parameters[7].Value = model.pproductid;


           int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
           return rows;
       }
       public static int DeletePart1(string part_id)
       {
           string sql = @"delete from [mg_part] where [part_id]=" + part_id + "";
           return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
       }
       
    }
}
