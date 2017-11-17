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
    public class mg_TestRepairItemDAL
    {
       
        public static List<mg_TestRepairItemModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<mg_TestRepairItemModel> list = null;

            string sql1 = @"select count(ID) total from [mg_Test_Repair_Item];";
            string sql2 = @" SELECT top " + pagesize + @" ID trid
                                      ,ItemCaption
                                      ,ItemType
                                      ,case ItemType
                                                      when 1 then '前排'
                                                      when 2 then'后排靠背'
                                                      when 3 then'后排坐垫'
                                                      end as ItemTypeName
                                      ,Sorting
                                      ,IsUseing                                                        
                                      ,case IsUseing
                                                        when 1 then '在用'
                                                        when 0 then '停用'
                                                        end as IsUseingName
                                  FROM [mg_Test_Repair_Item]                         
                                  order by ID asc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_TestRepairItemModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_TestRepairItemModel model = new mg_TestRepairItemModel();
                    model.tr_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "trid"));
                    model.ItemCaption = DataHelper.GetCellDataToStr(row, "ItemCaption");
                    model.Sorting = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Sorting"));
                    model.IsUseingName = DataHelper.GetCellDataToStr(row, "IsUseingName");
                    model.IsUseing = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "IsUseing"));
                    model.ItemType = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ItemType"));
                    model.ItemTypeName = DataHelper.GetCellDataToStr(row, "ItemTypeName");
                    list.Add(model);
                }
            }
            return list;
        }
        public static List<mg_TestRepairItemModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_TestRepairItemModel> list = null;

            string sql1 = @"select count(ID) total from [mg_Test_Repair_Item];";
            string sql2 = @" SELECT top " + pagesize + @" ID trid
                                      ,ItemCaption
                                      ,ItemType
                                      ,case ItemType
                                                      when 1 then '前排'
                                                      when 2 then'后排靠背'
                                                      when 3 then'后排坐垫'
                                                      end as ItemTypeName
                                      ,Sorting
                                      ,IsUseing
                                      ,case IsUseing
                                                        when 1 then '在用'
                                                        when 0 then '停用'
                                                        end as IsUseingName
                                  FROM [mg_Test_Repair_Item]
                                    where  ID not in (
                                               select top ((" + page + @"-1)*" + pagesize + @") ID from  [mg_Test_Repair_Item]
                                                            order by ID)
                                  order by ID
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_TestRepairItemModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_TestRepairItemModel model = new mg_TestRepairItemModel();
                    model.tr_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "trid"));
                    model.ItemCaption = DataHelper.GetCellDataToStr(row, "ItemCaption");
                    model.Sorting = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Sorting"));
                    model.IsUseingName = DataHelper.GetCellDataToStr(row, "IsUseingName");
                    model.IsUseing = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "IsUseing"));
                    model.ItemType = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ItemType"));
                    model.ItemTypeName = DataHelper.GetCellDataToStr(row, "ItemTypeName");
                    list.Add(model);
                }
            }
            return list;
        }
        public static int AddTestRepairItem(mg_TestRepairItemModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into mg_Test_Repair_Item(");
            strSql.Append("ItemCaption,ItemType,Sorting,IsUseing)");
            strSql.Append(" values (");
            strSql.Append("@ItemCaption,@ItemType,@Sorting,@IsUseing)");
            SqlParameter[] parameters = {
					new SqlParameter("@ItemCaption", SqlDbType.NVarChar),
	                new SqlParameter("@ItemType", SqlDbType.Int),
				    new SqlParameter("@Sorting", SqlDbType.Int),
                    new SqlParameter("@IsUseing", SqlDbType.Int)
                                        };
            parameters[0].Value = model.ItemCaption;
            parameters[1].Value = model.ItemType;
            parameters[2].Value = model.Sorting;
            parameters[3].Value = model.IsUseing;
           
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static int UpdateTestRepairItem(mg_TestRepairItemModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_Test_Repair_Item set ");
            strSql.Append("ItemCaption=@ItemCaption,");
            strSql.Append("ItemType=@ItemType,");
            strSql.Append("Sorting=@Sorting,");
            strSql.Append("IsUseing=@IsUseing");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@ItemCaption", SqlDbType.NVarChar),
                    new SqlParameter("@ItemType", SqlDbType.Int),
                    new SqlParameter("@Sorting", SqlDbType.Int),
                     new SqlParameter("@IsUseing", SqlDbType.Int),
                                        };
            parameters[0].Value = model.tr_id;
            parameters[1].Value = model.ItemCaption;
            parameters[2].Value = model.ItemType;
            parameters[3].Value = model.Sorting;
            parameters[4].Value = model.IsUseing;
            
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static int DeleteTestRepairItem(string tr_id)
        {
            string sql = @"delete from [mg_Test_Repair_Item] where [ID]=" + tr_id + "";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        
    }
}
