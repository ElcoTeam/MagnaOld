using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Tools;
using DBUtility;
using Model;

namespace DAL
{
    public class mg_BomDAL
    {
        public static int AddBomByName(string name, string no, int level, string desc, string pic, string material, string profile, int weight, string supplier, string category, string comments)
        {
            string sql = @"INSERT INTO [mg_BOM] ([bom_name],[bom_no],[bom_leve],[bom_desc],[bom_picture],[bom_material],[bom_profile],[bom_weight],[bom_suppller],[bom_category],[bom_comments]) VALUES ('" + name + "','" + no + "'," + level + ",'" + desc + "','" + pic + "','" + material + "','" + profile + "'," + weight + ",'" + supplier + "','" + category + "','" + comments + "')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetAllData()
        {
            string sql = @"select * from [mg_BOM] order by [bom_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int UpdateBomByName(int id, string name, string no, int level, string desc, string material, string profile, int weight, string supplier, string category, string comments, string pic)
        {
            string sql = @"update [mg_Bom] set bom_name = '" + name + "',bom_no='" + no + "',bom_leve=" + level + ",bom_desc='" + desc + "',bom_picture='" + pic + "',bom_material='" + material + "',bom_profile='" + profile + "',bom_weight=" + weight + ",bom_suppller='" + supplier + "',bom_category='" + category + "',bom_comments='" + comments + "' where bom_id='" + id + "'";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int DelBomByName(int bom_id)
        {
            string sql = @"delete from [mg_bom] where bom_id=" + bom_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int CheckBomByName(int a, int bomid, string name)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"select * from [mg_bom] where bom_name='" + name + "'";
            }
            if (a == 2)
            {
                sql = @"select * from [mg_bom] where bom_name='" + name + "' and bom_id <>" + bomid;
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

        public static DataTable GetStationID()
        {
            string sql = @"select st_id,st_name from [mg_station] order by [st_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetBomName()
        {
            string sql = @"select bom_id,bom_name from [mg_bom] order by [bom_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static bool CheckPicName(string name)
        {
            string sql = @"select count(*) from [mg_bom] where bom_picture='" + name + "'";
            DataTable tb;

            tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            if (tb.Rows[0][0].ToString() == "0")
            {
                tb.Dispose();
                return false;
            }
            else
            {
                tb.Dispose();
                return true;
            }
        }



        /*
       * 
     *      姜任鹏
     * 
     */


        public static bool UpdateBOM(mg_BOMModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_BOM set ");
            strSql.Append("bom_PN=@bom_PN,");
            strSql.Append("bom_customerPN=@bom_customerPN,");
            strSql.Append("bom_isCustomerPN=@bom_isCustomerPN,");
            strSql.Append("bom_leve=@bom_leve,");
            strSql.Append("bom_materialid=@bom_materialid,");
            strSql.Append("bom_suppllerid=@bom_suppllerid,");
            strSql.Append("bom_categoryid=@bom_categoryid,");
            strSql.Append("bom_colorid=@bom_colorid,");
            strSql.Append("bom_profile=@bom_profile,");
            strSql.Append("bom_weight=@bom_weight,");
            strSql.Append("bom_desc=@bom_desc,");
            strSql.Append("bom_descCH=@bom_descCH,");
            strSql.Append("bom_picture=@bom_picture,");
            strSql.Append("bom_storeid=@bom_storeid");
            strSql.Append(" where bom_id=@bom_id; ");


            if (!string.IsNullOrEmpty(model.partIDs))
            {
                strSql.Append("delete from [mg_part_bom_rel]  where bom_id=@bom_id ;");
                string[] idArr = model.partIDs.Split(',');
                foreach (string id in idArr)
                {
                    strSql.Append("INSERT INTO [mg_part_bom_rel] ([part_id],[bom_id]) VALUES  (" + id + ",@bom_id);");
                }
            }

            SqlParameter[] parameters = {
					new SqlParameter("@bom_id", SqlDbType.Int),
					new SqlParameter("@bom_PN", SqlDbType.NVarChar),
					new SqlParameter("@bom_customerPN", SqlDbType.NVarChar),
					new SqlParameter("@bom_isCustomerPN", SqlDbType.Int),
					new SqlParameter("@bom_leve", SqlDbType.Int),
					new SqlParameter("@bom_materialid", SqlDbType.Int),
					new SqlParameter("@bom_suppllerid", SqlDbType.Int),
					new SqlParameter("@bom_categoryid", SqlDbType.Int),
					new SqlParameter("@bom_colorid", SqlDbType.Int),
					new SqlParameter("@bom_profile", SqlDbType.NVarChar),
					new SqlParameter("@bom_weight", SqlDbType.Int),
					new SqlParameter("@bom_desc", SqlDbType.NVarChar),
					new SqlParameter("@bom_descCH", SqlDbType.VarChar),
					new SqlParameter("@bom_picture", SqlDbType.NVarChar),
					new SqlParameter("@bom_storeid", SqlDbType.Int)
                                        };
            parameters[0].Value = model.bom_id;
            parameters[1].Value = model.bom_PN;
            parameters[2].Value = model.bom_customerPN;
            parameters[3].Value = model.bom_isCustomerPN;
            parameters[4].Value = model.bom_leve;
            parameters[5].Value = model.bom_materialid;
            parameters[6].Value = model.bom_suppllerid;
            parameters[7].Value = model.bom_categoryid;
            parameters[8].Value = model.bom_colorid;
            parameters[9].Value = model.bom_profile;
            parameters[10].Value = model.bom_weight;
            parameters[11].Value = model.bom_desc;
            parameters[12].Value = model.bom_descCH;
            parameters[13].Value = model.bom_picture;
            parameters[14].Value = model.bom_storeid;


            List<SqlParameter> list = new List<SqlParameter>();
            list.AddRange(parameters);
            return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(), list);
        }

        public static bool AddBOM(mg_BOMModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"declare @i int;
                                SELECT @i=max([bom_id])  FROM [mg_BOM];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                ");
            strSql.Append("insert into mg_BOM(");
            strSql.Append("bom_id,bom_PN,bom_customerPN,bom_isCustomerPN,bom_leve,bom_materialid,bom_suppllerid,bom_categoryid,bom_colorid,bom_profile,bom_weight,bom_desc,bom_descCH,bom_picture,bom_storeid)");
            strSql.Append(" values (");
            strSql.Append("@i,@bom_PN,@bom_customerPN,@bom_isCustomerPN,@bom_leve,@bom_materialid,@bom_suppllerid,@bom_categoryid,@bom_colorid,@bom_profile,@bom_weight,@bom_desc,@bom_descCH,@bom_picture,@bom_storeid)");
            SqlParameter[] parameters = {
					new SqlParameter("@bom_PN", SqlDbType.NVarChar),
					new SqlParameter("@bom_customerPN", SqlDbType.NVarChar),
					new SqlParameter("@bom_isCustomerPN", SqlDbType.Int),
					new SqlParameter("@bom_leve", SqlDbType.Int),
					new SqlParameter("@bom_materialid", SqlDbType.Int),
					new SqlParameter("@bom_suppllerid", SqlDbType.Int),
					new SqlParameter("@bom_categoryid", SqlDbType.Int),
					new SqlParameter("@bom_colorid", SqlDbType.Int),
					new SqlParameter("@bom_profile", SqlDbType.NVarChar),
					new SqlParameter("@bom_weight", SqlDbType.Int),
					new SqlParameter("@bom_desc", SqlDbType.NVarChar),
					new SqlParameter("@bom_descCH", SqlDbType.VarChar),
					new SqlParameter("@bom_picture", SqlDbType.NVarChar),
					new SqlParameter("@bom_storeid", SqlDbType.Int)
                                        };
            parameters[0].Value = model.bom_PN;
            parameters[1].Value = model.bom_customerPN;
            parameters[2].Value = model.bom_isCustomerPN;
            parameters[3].Value = model.bom_leve;
            parameters[4].Value = model.bom_materialid;
            parameters[5].Value = model.bom_suppllerid;
            parameters[6].Value = model.bom_categoryid;
            parameters[7].Value = model.bom_colorid;
            parameters[8].Value = model.bom_profile;
            parameters[9].Value = model.bom_weight;
            parameters[10].Value = model.bom_desc;
            parameters[11].Value = model.bom_descCH;
            parameters[12].Value = model.bom_picture;
            parameters[13].Value = model.bom_storeid;

            if (!string.IsNullOrEmpty(model.partIDs))
            {
                string[] idArr = model.partIDs.Split(',');
                foreach (string id in idArr)
                {
                    strSql.Append("INSERT INTO [mg_part_bom_rel] ([part_id],[bom_id]) VALUES  (" + id + ",@i);");
                }
            }

            List<SqlParameter> list = new List<SqlParameter>();
            list.AddRange(parameters);

            return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(), list);
        }

        public static List<mg_BOMModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_BOMModel> list = null;

            string sql1 = @"select count([bom_id]) total from [mg_BOM];";
            string sql2 = @" 
                              with data as 
                                  (
	                                 select p.part_id,p.part_no,pbrel.bom_id from mg_part_bom_rel pbrel left join mg_part p on pbrel.part_id=p.part_id
	                                 )
                                SELECT top " + pagesize + @" [bom_id]
                                      ,[bom_PN]
                                      ,[bom_customerPN]
                                      ,[bom_isCustomerPN]
	                                   ,case [bom_isCustomerPN]
		                                                            when 1 then '是'
		                                                            else '否'
		                                                            end bom_isCustomerPNName
                                      ,[bom_leve]
 ,l.prop_name [bom_leveName]
                                      ,[bom_materialid]
                                      ,m.prop_name [bom_material]
                                      ,[bom_suppllerid]
                                      ,s.prop_name [bom_suppller]
                                      ,[bom_categoryid]
                                      ,ca.prop_name [bom_category]
                                      ,[bom_storeid]
                                      ,st.prop_name [bom_storeName]
                                      ,[bom_colorid]
                                      ,co.prop_name [bom_colorname]
                                      ,[bom_profile]
                                      ,[bom_weight]
                                      ,[bom_desc]
                                      ,[bom_descCH]
                                      ,[bom_picture]
	                                   ,STUFF((SELECT ','+cast (part_id as varchar) from data p where p.bom_id=b.bom_id  for xml path('')),1,1,'')partIDs
	                                    ,STUFF((SELECT ','+cast (part_no as varchar) from data p where p.bom_id=b.bom_id  for xml path('')),1,1,'')partNOs
                                  FROM [mg_BOM] b
                                  left join mg_Property m on b.bom_materialid=m.prop_id
                                  left join mg_Property s on b.bom_suppllerid=s.prop_id
                                  left join mg_Property ca on b.bom_categoryid=ca.prop_id
                                  left join mg_Property co on b.bom_colorid=co.prop_id
  left join mg_Property l on b.bom_leve=l.prop_id
  left join mg_Property st on b.bom_storeid=st.prop_id
                                        where  b.bom_id < (
                                                select top 1 bom_id from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") bom_id from  [mg_BOM] where bom_id is not null  order by bom_id desc )t
                                                order by  bom_id  )

                                    order by b.bom_id desc;
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_BOMModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_BOMModel model = new mg_BOMModel();

                    model.bom_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_id"));
                    model.bom_isCustomerPN = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_isCustomerPN"));
                    model.bom_colorid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_colorid"));
                    model.bom_materialid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_materialid"));
                    model.bom_categoryid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_categoryid"));
                    model.bom_suppllerid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_suppllerid"));
                    model.bom_leve = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_leve"));
                    model.bom_weight = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_weight"));
                    model.partIDs = DataHelper.GetCellDataToStr(row, "partIDs");
                    model.bom_picture = DataHelper.GetCellDataToStr(row, "bom_picture");
                    model.bom_PN = DataHelper.GetCellDataToStr(row, "bom_PN");
                    model.bom_customerPN = DataHelper.GetCellDataToStr(row, "bom_customerPN");
                    model.bom_isCustomerPNName = DataHelper.GetCellDataToStr(row, "bom_isCustomerPNName");
                    model.bom_colorname = DataHelper.GetCellDataToStr(row, "bom_colorname");
                    model.bom_material = DataHelper.GetCellDataToStr(row, "bom_material");
                    model.bom_category = DataHelper.GetCellDataToStr(row, "bom_category");
                    model.bom_suppller = DataHelper.GetCellDataToStr(row, "bom_suppller");
                    model.bom_profile = DataHelper.GetCellDataToStr(row, "bom_profile");
                    model.bom_desc = DataHelper.GetCellDataToStr(row, "bom_desc");
                    model.bom_descCH = DataHelper.GetCellDataToStr(row, "bom_descCH");
                    model.partNOs = DataHelper.GetCellDataToStr(row, "partNOs");
                    model.bom_leveName = DataHelper.GetCellDataToStr(row, "bom_leveName");
                    model.bom_storeid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_storeid"));
                    model.bom_storeName = DataHelper.GetCellDataToStr(row, "bom_storeName");
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_BOMModel> QueryListForFirstPage(string pagesize, out string total)
        {

            total = "0";
            List<mg_BOMModel> list = null;

            string sql1 = @"select count([bom_id]) total from [mg_BOM];";
            string sql2 = @" 
                              with data as 
                                  (
	                                 select p.part_id,p.part_no,pbrel.bom_id from mg_part_bom_rel pbrel left join mg_part p on pbrel.part_id=p.part_id
	                                 )
                                SELECT top " + pagesize + @"  [bom_id]
                                      ,[bom_PN]
                                      ,[bom_customerPN]
                                      ,[bom_isCustomerPN]
	                                   ,case [bom_isCustomerPN]
		                                                            when 1 then '是'
		                                                            else '否'
		                                                            end bom_isCustomerPNName
                                      ,[bom_leve]
 ,l.prop_name [bom_leveName]
                                      ,[bom_materialid]
                                      ,m.prop_name [bom_material]
                                      ,[bom_suppllerid]
                                      ,s.prop_name [bom_suppller]
                                      ,[bom_categoryid]
                                      ,ca.prop_name [bom_category]
                                      ,[bom_storeid]
                                      ,st.prop_name [bom_storeName]
                                      ,[bom_colorid]
                                      ,co.prop_name [bom_colorname]
                                      ,[bom_profile]
                                      ,[bom_weight]
                                      ,[bom_desc]
                                      ,[bom_descCH]
                                      ,[bom_picture]
	                                   ,STUFF((SELECT ','+cast (part_id as varchar) from data p where p.bom_id=b.bom_id  for xml path('')),1,1,'')partIDs
	                                    ,STUFF((SELECT ','+cast (part_no as varchar) from data p where p.bom_id=b.bom_id  for xml path('')),1,1,'')partNOs
                                  FROM [mg_BOM] b
                                  left join mg_Property m on b.bom_materialid=m.prop_id
                                  left join mg_Property s on b.bom_suppllerid=s.prop_id
                                  left join mg_Property ca on b.bom_categoryid=ca.prop_id
                                  left join mg_Property co on b.bom_colorid=co.prop_id
  left join mg_Property l on b.bom_leve=l.prop_id
  left join mg_Property st on b.bom_storeid=st.prop_id
                                    order by b.bom_id desc;
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_BOMModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_BOMModel model = new mg_BOMModel();

                    model.bom_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_id"));
                    model.bom_isCustomerPN = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_isCustomerPN"));
                    model.bom_colorid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_colorid"));
                    model.bom_materialid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_materialid"));
                    model.bom_categoryid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_categoryid"));
                    model.bom_suppllerid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_suppllerid"));
                    model.bom_leve = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_leve"));
                    model.bom_weight = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_weight"));
                    model.partIDs = DataHelper.GetCellDataToStr(row, "partIDs");
                    model.bom_picture = DataHelper.GetCellDataToStr(row, "bom_picture");
                    model.bom_PN = DataHelper.GetCellDataToStr(row, "bom_PN");
                    model.bom_customerPN = DataHelper.GetCellDataToStr(row, "bom_customerPN");
                    model.bom_isCustomerPNName = DataHelper.GetCellDataToStr(row, "bom_isCustomerPNName");
                    model.bom_colorname = DataHelper.GetCellDataToStr(row, "bom_colorname");
                    model.bom_material = DataHelper.GetCellDataToStr(row, "bom_material");
                    model.bom_category = DataHelper.GetCellDataToStr(row, "bom_category");
                    model.bom_suppller = DataHelper.GetCellDataToStr(row, "bom_suppller");
                    model.bom_profile = DataHelper.GetCellDataToStr(row, "bom_profile");
                    model.bom_desc = DataHelper.GetCellDataToStr(row, "bom_desc");
                    model.bom_descCH = DataHelper.GetCellDataToStr(row, "bom_descCH");
                    model.partNOs = DataHelper.GetCellDataToStr(row, "partNOs");
                    model.bom_leveName = DataHelper.GetCellDataToStr(row, "bom_leveName");
                    model.bom_storeid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_storeid"));
                    model.bom_storeName = DataHelper.GetCellDataToStr(row, "bom_storeName");

                    list.Add(model);
                }
            }
            return list;
        }

        public static int DeleteBOM(string bom_id)
        {
            string sql = @"delete from [mg_BOM] where [bom_id]=" + bom_id + ";delete from [mg_part_bom_rel]  where bom_id=" + bom_id + @" ;";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetRelationTable(string key, string id)
        {
            string sql = @"SELECT ap.[all_id]
                              ,ap.[all_no]
                             ,p.part_id
	                         ,p.part_no
	                         ,b.bom_id
	                         ,b.bom_PN
	                         ,b.bom_customerPN
                          FROM [mg_allpart] ap
                          inner join [mg_allpart_part_rel] aprel on ap.all_id = aprel.all_id
                          inner join [mg_part] p on aprel.partid_id=p.part_id
                          inner join [mg_part_bom_rel] pbrel on p.part_id=pbrel.part_id
                          inner join [mg_BOM] b on pbrel.bom_id=b.bom_id

                          where " + key + @"=" + id + @"
";

            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);

        }
        
        public static List<mg_BOMModel> QueryBOMForStepEditing(string part_id)
        {
            List<mg_BOMModel> list = null;
            string joinSre = (!string.IsNullOrEmpty(part_id) && part_id != "0") ? @" inner join [mg_part_bom_rel] pbrel on pbrel.bom_id=bom.bom_id and pbrel.part_id=" + part_id : "";
            string sql = @"
                            SELECT bom.[bom_id]
                                  ,[bom_PN]
                                  ,bom_desc
                              FROM [mg_BOM] bom
                                " + joinSre + @"
                              order by bom_PN,bom_desc";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_BOMModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_BOMModel model = new mg_BOMModel();
                    model.bom_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_id"));
                    model.bom_PN = DataHelper.GetCellDataToStr(row, "bom_PN") + " | " + DataHelper.GetCellDataToStr(row, "bom_desc");
                    list.Add(model);
                }
            }

            return list;
        }
    }
}
