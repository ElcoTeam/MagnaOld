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
    public class mg_ProductDAL
    {
       
        public static List<mg_ProductModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<mg_ProductModel> list = null;

            string sql1 = @"select count(ID) total from [mg_Product];";
            string sql2 = @" SELECT top " + pagesize + @" ID pid
                                      ,ProductNo
                                      ,ProductName
                                      ,ProductDesc
                                      ,ProductType
                                      ,case ProductType
                                           when 1 then'前排主驾'
                                           when 2 then'前排副驾'
                                           when 3 then'后排' end as ProductTypeName
                                      ,IsUseing
                                      ,case IsUseing
                                           when 0 then'停用'
                                           when 1 then'在用' end as IsUseingName
                                  FROM [mg_Product]                         
                                  order by ID desc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_ProductModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_ProductModel model = new mg_ProductModel();
                    model.p_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pid"));
                    model.ProductNo = DataHelper.GetCellDataToStr(row, "ProductNo");
                    model.ProductName = DataHelper.GetCellDataToStr(row, "ProductName");
                    model.ProductDesc = DataHelper.GetCellDataToStr(row, "ProductDesc");
                    model.ProductType = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ProductType"));
                    model.ProductTypeName = DataHelper.GetCellDataToStr(row, "ProductTypeName");
                    model.IsUseingName = DataHelper.GetCellDataToStr(row, "IsUseingName");
                    model.IsUseing = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "IsUseing"));
                    list.Add(model);
                }
            }
            return list;
        }
        public static List<mg_ProductModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_ProductModel> list = null;

            string sql1 = @"select count(ID) total from [mg_Product];";
            string sql2 = @" SELECT top " + pagesize + @" ID pid
                                      ,ProductNo
                                      ,ProductName
                                      ,ProductDesc
                                      ,ProductType
                                      ,case ProductType
                                           when 1 then'前排主驾'
                                           when 2 then'前排副驾'
                                           when 3 then'后排' end as ProductTypeName
                                      ,IsUseing
                                      ,case IsUseing
                                           when 0 then'停用'
                                           when 1 then'在用' end as IsUseingName
                                  FROM [mg_Product]    
                                    where  ID not in
                                                        (select top ((" + page + @"-1)*" + pagesize + @") ID from  [mg_Product] order by ID desc)
                                  order by ID desc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_ProductModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_ProductModel model = new mg_ProductModel();
                    model.p_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pid"));
                    model.ProductNo = DataHelper.GetCellDataToStr(row, "ProductNo");
                    model.ProductName = DataHelper.GetCellDataToStr(row, "ProductName");
                    model.ProductDesc = DataHelper.GetCellDataToStr(row, "ProductDesc");
                    model.ProductType = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ProductType"));
                    model.ProductTypeName = DataHelper.GetCellDataToStr(row, "ProductTypeName");
                    model.IsUseingName = DataHelper.GetCellDataToStr(row, "IsUseingName");
                    model.IsUseing = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "IsUseing"));
                    list.Add(model);
                }
            }
            return list;
        }
        public static List<mg_ProductModel> queryProductidForPart()
        {
            List<mg_ProductModel> list = null;
            string sql = @"SELECT [ID],[ProductName]  FROM [mg_Product] order by ProductName ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_ProductModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_ProductModel model = new mg_ProductModel();
                    model.p_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                    model.ProductName = DataHelper.GetCellDataToStr(row, "ProductName");
                    list.Add(model);
                }
            }
            return list;
        }
        public static int AddProduct(mg_ProductModel model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into mg_Product(");
            strSql.Append("ProductNo,");
            strSql.Append("ProductName,");
            strSql.Append("ProductDesc,");
            strSql.Append("ProductType,");
            strSql.Append("IsUseing)");
            strSql.Append(" values (");
            strSql.Append("@ProductNo,");
            strSql.Append("@ProductName,");
            strSql.Append("@ProductDesc,");
            strSql.Append("@ProductType,");
            strSql.Append("@IsUseing)");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductNo", SqlDbType.NVarChar),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar),
                    new SqlParameter("@ProductDesc", SqlDbType.NVarChar),
					new SqlParameter("@ProductType", SqlDbType.Int),
                    new SqlParameter("@IsUseing", SqlDbType.Int),
                                        };
            parameters[0].Value = model.ProductNo;
            parameters[1].Value = model.ProductName;
            parameters[2].Value = model.ProductDesc;
            parameters[3].Value = model.ProductType;
            parameters[4].Value = model.IsUseing;
           
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static int UpdateProduct(mg_ProductModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_Product set ");
            strSql.Append("ProductNo=@ProductNo,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("ProductDesc=@ProductDesc,");
            strSql.Append("ProductType=@ProductType,");
            strSql.Append("IsUseing=@IsUseing");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@ProductNo", SqlDbType.NVarChar),
                    new SqlParameter("@ProductName", SqlDbType.NVarChar),
                    new SqlParameter("@ProductDesc", SqlDbType.NVarChar),
                    new SqlParameter("@ProductType",SqlDbType.Int),
                    new SqlParameter("@IsUseing",SqlDbType.Int),
                                        };
            parameters[0].Value = model.p_id;
            parameters[1].Value = model.ProductNo;
            parameters[2].Value = model.ProductName;
            parameters[3].Value = model.ProductDesc;
            parameters[4].Value = model.ProductType;
            parameters[5].Value = model.IsUseing;
            
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static int DeleteProduct(string p_id)
        {
            string sql = @"delete from [mg_Product] where [ID]=" + p_id + "";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
    }
}
