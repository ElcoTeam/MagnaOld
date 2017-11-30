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
    public class mg_PartDAL
    {
        public static int AddPartByName(string part_no, string part_name, string part_desc)
        {
            string sql = @"INSERT INTO [mg_part] ([part_no],[part_name],[part_desc]) VALUES ('" + part_no + "','" + part_name + "','" + part_desc + @"')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static int Addrel(int allid, int partid)
        {
            string sql = @"INSERT INTO [mg_allpart_part_rel] ([all_id],[partid_id]) VALUES (" + allid + "," + partid + @")";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetAllData()
        {
            //string sql = @"SELECT * from [mg_part] order by [part_id] asc";
            string sql = @"SELECT dbo.mg_allpart.all_id, dbo.mg_allpart.all_no, dbo.mg_part.part_id, dbo.mg_part.part_no, dbo.mg_part.part_name, 
                    dbo.mg_part.part_desc FROM dbo.mg_allpart_part_rel INNER JOIN
                    dbo.mg_allpart ON dbo.mg_allpart_part_rel.all_id = dbo.mg_allpart.all_id INNER JOIN
                    dbo.mg_part ON dbo.mg_allpart_part_rel.partid_id = dbo.mg_part.part_id
                    GROUP BY dbo.mg_allpart.all_id, dbo.mg_allpart.all_no, dbo.mg_part.part_id, dbo.mg_part.part_no, dbo.mg_part.part_name, 
                    dbo.mg_part.part_desc";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int UpDatePartByName(int p_id, string p_no, string p_name, string p_desc)
        {
            string sql = @"update [mg_part] set [part_no]='" + p_no + "',[part_name]='" + p_name + "',[part_desc]= '" + p_desc + "' where [part_id]=" + p_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int Updaterel(int newallid, int newpartid)
        {
            string sql = @"INSERT INTO [mg_allpart_part_rel] ([all_id],[partid_id]) VALUES (" + newallid + "," + newpartid + @")";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int Delrel(int allid, int partid)
        {
            string sql = @"delete from [mg_allpart_part_rel] where [all_id]=" + allid + " and [partid_id]=" + partid + @"";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int DelPartByName(int p_id)
        {
            string sql = @"delete from [mg_part] where [part_id]=" + p_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int CheckPartByName(int a, int part_id, string part_no, string part_name, string part_desc, string allno)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"SELECT dbo.mg_allpart.all_id, dbo.mg_allpart.all_no, dbo.mg_part.part_id, dbo.mg_part.part_no, dbo.mg_part.part_name, 
                    dbo.mg_part.part_desc FROM dbo.mg_allpart_part_rel INNER JOIN
                    dbo.mg_allpart ON dbo.mg_allpart_part_rel.all_id = dbo.mg_allpart.all_id INNER JOIN
                    dbo.mg_part ON dbo.mg_allpart_part_rel.partid_id = dbo.mg_part.part_id where (dbo.mg_part.part_name='" + part_name + "' or dbo.mg_part.part_no='" + part_no + "') and dbo.mg_allpart.all_no='" + allno + "'";
            }
            if (a == 2)
            {
                sql = @"SELECT dbo.mg_allpart.all_id, dbo.mg_allpart.all_no, dbo.mg_part.part_id, dbo.mg_part.part_no, dbo.mg_part.part_name, 
                    dbo.mg_part.part_desc FROM dbo.mg_allpart_part_rel INNER JOIN
                    dbo.mg_allpart ON dbo.mg_allpart_part_rel.all_id = dbo.mg_allpart.all_id INNER JOIN
                    dbo.mg_part ON dbo.mg_allpart_part_rel.partid_id = dbo.mg_part.part_id  where (dbo.mg_part.part_name='" + part_name + "'or dbo.mg_part.part_no='" + part_no + "')  and dbo.mg_allpart.all_no='" + allno + "'  and dbo.mg_part.part_id <> " + part_id;
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
         * 
       *      姜任鹏
       * 
       */


        public static bool UpdatePart(Model.mg_partModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_part set ");
            strSql.Append("part_no=@part_no,");
            strSql.Append("part_name=@part_name,");
            strSql.Append("part_desc=@part_desc,");
            strSql.Append("part_categoryid=@part_categoryid");
            strSql.Append(" where part_id=@part_id ;");

            if (!string.IsNullOrEmpty(model.allpartIDs))
            {
                strSql.Append("delete from [mg_allpart_part_rel]  where partid_id=@part_id ;");
                string[] idArr = model.allpartIDs.Split(',');
                foreach (string id in idArr)
                {
                    strSql.Append("INSERT INTO [mg_allpart_part_rel] ([all_id],[partid_id])     VALUES  (" + id + ",@part_id);");
                }
            }

            SqlParameter[] parameters = {
                    new SqlParameter("@part_id", SqlDbType.Int,4),
                    new SqlParameter("@part_no", SqlDbType.NVarChar,50),
                    new SqlParameter("@part_name", SqlDbType.NVarChar,50),
                    new SqlParameter("@part_desc", SqlDbType.NVarChar,500),
                    new SqlParameter("@part_categoryid", SqlDbType.Int,4)};
            parameters[0].Value = model.part_id;
            parameters[1].Value = model.part_no;
            parameters[2].Value = model.part_name;
            parameters[3].Value = model.part_desc;
            parameters[4].Value = model.part_categoryid;


            List<SqlParameter> list = new List<SqlParameter>();
            list.AddRange(parameters);
            return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(), list);

        }

        public static bool AddPart(mg_partModel model)
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
            strSql.Append("part_id,part_no,part_name,part_desc,part_categoryid)");
            strSql.Append(" values (");
            strSql.Append("@i,@part_no,@part_name,@part_desc,@part_categoryid);");
            SqlParameter[] parameters = {
                    new SqlParameter("@part_id", SqlDbType.Int),
                    new SqlParameter("@part_no", SqlDbType.NVarChar),
                    new SqlParameter("@part_name", SqlDbType.NVarChar),
                    new SqlParameter("@part_desc", SqlDbType.NVarChar),
                    new SqlParameter("@part_categoryid", SqlDbType.Int)};
            parameters[0].Value = model.part_id;
            parameters[1].Value = model.part_no;
            parameters[2].Value = model.part_name;
            parameters[3].Value = model.part_desc;
            parameters[4].Value = model.part_categoryid;

            if (!string.IsNullOrEmpty(model.allpartIDs))
            {
                string[] idArr = model.allpartIDs.Split(',');
                foreach (string id in idArr)
                {
                    strSql.Append("INSERT INTO [mg_allpart_part_rel] ([all_id],[partid_id])     VALUES  (" + id + ",@i);");
                }
            }

            List<SqlParameter> list = new List<SqlParameter>();
            list.AddRange(parameters);


            return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(), list);
        }

        public static List<mg_partModel> QueryListForFirstPage(string pagesize, out string total)
        {

            total = "0";
            List<mg_partModel> list = null;

            string sql1 = @"select count(part_id) total from [mg_part];";
            string sql2 = @" 
                              with data as 
                                  (
	                                 select allp.all_id,allp.all_no,aprel.partid_id from [mg_allpart_part_rel] aprel left join [mg_allpart] allp on aprel.all_id=allp.all_id
	                                 )
                                SELECT top " + pagesize + @" [part_id]
                                      ,[part_no]
                                      ,[part_name]
                                      ,[part_desc]
                                      ,[part_categoryid]
                                      ,prop.prop_name [part_Category]
	                                  ,STUFF((SELECT ','+cast (all_id as varchar) from data allp where allp.partid_id=p.part_id  for xml path('')),1,1,'')allpartIDs
	                                  ,STUFF((SELECT ','+cast (all_no as varchar) from data allp where allp.partid_id=p.part_id  for xml path('')),1,1,'')allpartNOs
                                  FROM [mg_part] p
                                  left join mg_Property prop on p.part_categoryid = prop.prop_id
                                  order by part_id desc
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

                    model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.part_no = DataHelper.GetCellDataToStr(row, "part_no");
                    model.part_name = DataHelper.GetCellDataToStr(row, "part_name");
                    model.part_desc = DataHelper.GetCellDataToStr(row, "part_desc");
                    model.part_categoryid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_categoryid"));
                    model.part_Category = DataHelper.GetCellDataToStr(row, "part_Category");
                    model.allpartIDs = DataHelper.GetCellDataToStr(row, "allpartIDs");
                    model.allpartNOs = DataHelper.GetCellDataToStr(row, "allpartNOs");

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
            string sql2 = @" 
                            with data as 
                                  (
	                                 select allp.all_id,allp.all_no,aprel.partid_id from [mg_allpart_part_rel] aprel left join [mg_allpart] allp on aprel.all_id=allp.all_id
	                                 )
                                SELECT top " + pagesize + @" [part_id]
                                      ,[part_no]
                                      ,[part_name]
                                      ,[part_desc]
                                      ,[part_categoryid]
                                      ,prop.prop_name [part_Category]
	                                  ,STUFF((SELECT ','+cast (all_id as varchar) from data allp where allp.partid_id=p.part_id  for xml path('')),1,1,'')allpartIDs
	                                  ,STUFF((SELECT ','+cast (all_no as varchar) from data allp where allp.partid_id=p.part_id  for xml path('')),1,1,'')allpartNOs
                                  FROM [mg_part] p
                                  left join mg_Property prop on p.part_categoryid = prop.prop_id
                                     where  p.part_id < (
                                                select top 1 part_id from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") part_id from  [mg_part] where part_id is not null  order by part_id desc )t
                                                order by  part_id  )
                                
                                order by part_id desc
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

                    model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.part_no = DataHelper.GetCellDataToStr(row, "part_no");
                    model.part_name = DataHelper.GetCellDataToStr(row, "part_name");
                    model.part_desc = DataHelper.GetCellDataToStr(row, "part_desc");
                    model.part_categoryid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_categoryid"));
                    model.part_Category = DataHelper.GetCellDataToStr(row, "part_Category");
                    model.allpartIDs = DataHelper.GetCellDataToStr(row, "allpartIDs");
                    model.allpartNOs = DataHelper.GetCellDataToStr(row, "allpartNOs");

                    list.Add(model);
                }
            }
            return list;

        }

        public static int DeletePart(string part_id)
        {
            string sql = @"delete from [mg_part] where [part_id]=" + part_id + ";delete from [mg_allpart_part_rel]  where partid_id=" + part_id + @" ;";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static List<mg_partModel> QueryPartListForBOM()
        {
            List<mg_partModel> list = null;

            string sql2 = @" 
                              with data as 
                                  (
	                                 select allp.all_id,allp.all_no,aprel.partid_id from [mg_allpart_part_rel] aprel left join [mg_allpart] allp on aprel.all_id=allp.all_id
	                                 )
                                SELECT  [part_id]
                                      ,[part_no]
                                      ,[part_name]
                                      ,[part_desc]
                                      ,[part_categoryid]
                                      ,prop.prop_name [part_Category]
	                                  ,STUFF((SELECT ','+cast (all_id as varchar) from data allp where allp.partid_id=p.part_id  for xml path('')),1,1,'')allpartIDs
	                                  ,STUFF((SELECT ','+cast (all_no as varchar) from data allp where allp.partid_id=p.part_id  for xml path('')),1,1,'')allpartNOs
                                  FROM [mg_part] p
                                  left join mg_Property prop on p.part_categoryid = prop.prop_id
                                  order by part_no 
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql2, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_partModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_partModel model = new mg_partModel();

                    model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.part_no = DataHelper.GetCellDataToStr(row, "part_no");
                    model.part_name = DataHelper.GetCellDataToStr(row, "part_name");
                    model.part_desc = DataHelper.GetCellDataToStr(row, "part_desc");
                    model.part_categoryid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_categoryid"));
                    model.part_Category = DataHelper.GetCellDataToStr(row, "part_Category");
                    model.allpartIDs = DataHelper.GetCellDataToStr(row, "allpartIDs");
                    model.allpartNOs = DataHelper.GetCellDataToStr(row, "allpartNOs");

                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_partModel> QueryPartForStepEditing()
        {
            List<mg_partModel> list = null;

            string sql2 = @" 
                             
                                SELECT  [part_id]
                                      ,[part_no]
                                      ,[part_name]
                                      ,[part_desc]
                                      ,[part_categoryid]
                                     
                                  FROM [mg_part] p
                          
                                  order by part_no 
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql2, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_partModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_partModel model = new mg_partModel();

                    model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.part_no = DataHelper.GetCellDataToStr(row, "part_no") + " | " + DataHelper.GetCellDataToStr(row, "part_name");
                    model.part_name = DataHelper.GetCellDataToStr(row, "part_name");
                    model.part_desc = DataHelper.GetCellDataToStr(row, "part_desc");

                    list.Add(model);
                }
            }
            return list;
        }
        public static List<mg_partModel> QueryPartForPartidList()
        {
            List<mg_partModel> list = null;

            string sql2 = @" 
                             
                                SELECT  [part_id]           
                                      ,[part_name]                                    
                                  FROM [mg_part]
                          
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql2, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_partModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_partModel model = new mg_partModel();

                    model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.part_name = DataHelper.GetCellDataToStr(row, "part_name");
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_partModel> queryPartForStepSearching()
        {
            List<mg_partModel> list = null;

            string sql2 = @" 
                             
                                SELECT  [part_id]
                                      ,[part_no]
                                      ,[part_name]
                                      ,[part_desc]
                                      ,[part_categoryid]
                                     
                                  FROM [mg_part] p
                          
                                  order by part_no 
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql2, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_partModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_partModel model = new mg_partModel();

                    model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.part_no = DataHelper.GetCellDataToStr(row, "part_no") + " | " + DataHelper.GetCellDataToStr(row, "part_name");
                    model.part_name = DataHelper.GetCellDataToStr(row, "part_name");
                    model.part_desc = DataHelper.GetCellDataToStr(row, "part_desc");
                    list.Add(model);
                }
                mg_partModel firstmodel = new mg_partModel();
                firstmodel.part_id = 0;
                firstmodel.part_no = "-- 部件(全部) --";
                list.Insert(0, firstmodel);
            }
            return list;
        }

        public static mg_partModel GetPartModelByPartNO(string partNO)
        {
            string sql2 = @" 
                           with data as 
                                  (
	                                 select allp.all_id,allp.all_no,aprel.partid_id from [mg_allpart_part_rel] aprel left join [mg_allpart] allp on aprel.all_id=allp.all_id
	                                 )
                                SELECT top 1 [part_id]
                                      ,[part_no]
                                      ,[part_name]
                                      ,[part_desc]
                                      ,[part_categoryid]
                                      ,prop.prop_name [part_Category]
	                                  ,STUFF((SELECT ','+cast (all_id as varchar) from data allp where allp.partid_id=p.part_id  for xml path('')),1,1,'')allpartIDs
	                                  ,STUFF((SELECT ','+cast (all_no as varchar) from data allp where allp.partid_id=p.part_id  for xml path('')),1,1,'')allpartNOs
                                  FROM [mg_part] p
                                  left join mg_Property prop on p.part_categoryid = prop.prop_id
                                where p.part_no='" + partNO + @"'
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql2, null);
            if (DataHelper.HasData(dt))
            {
                mg_partModel model = new mg_partModel();

                foreach (DataRow row in dt.Rows)
                {

                    model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.part_no = DataHelper.GetCellDataToStr(row, "part_no");
                    model.part_name = DataHelper.GetCellDataToStr(row, "part_name");
                    model.part_desc = DataHelper.GetCellDataToStr(row, "part_desc");
                    model.part_categoryid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_categoryid"));
                    model.part_Category = DataHelper.GetCellDataToStr(row, "part_Category");
                    model.allpartIDs = DataHelper.GetCellDataToStr(row, "allpartIDs");
                    model.allpartNOs = DataHelper.GetCellDataToStr(row, "allpartNOs");
                    break;
                }

                return model;

            }
            return null;
        }
    }
}
