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
    public class mg_PropertyDAL
    {

        public static List<mg_PropertyModel> queryJSONStringByPropertyType(string type)
        {
            List<mg_PropertyModel> list = null;
            string sql = @"SELECT [prop_id]
                                      ,[Prop_type]
                                      ,[prop_name]
                                  FROM [mg_Property] where Prop_type=" + type + @" order by prop_name";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_PropertyModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_PropertyModel model = new mg_PropertyModel();
                    model.prop_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "prop_id"));
                    model.prop_name = DataHelper.GetCellDataToStr(row, "prop_name");
                    list.Add(model);
                }
            }
            return list;
        }


        public static List<mg_PropertyModel> queryJSONStringByPropertyType(mg_PropertyEnum propEnum)
        {
            List<mg_PropertyModel> list = null;
            string sql = @"SELECT [prop_id]
                                      ,[Prop_type]
                                      ,[prop_name]
                                  FROM [mg_Property] where Prop_type=" + (int)propEnum + @" order by prop_name";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_PropertyModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_PropertyModel model = new mg_PropertyModel();
                    model.prop_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "prop_id"));
                    model.prop_name = DataHelper.GetCellDataToStr(row, "prop_name");
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 新增颜色
        /// lx 2017-06-22
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddColor(mg_PropertyModel model)
        {
            string maxid = @"declare @i int;
                                SELECT @i=max([prop_id])  FROM [mg_Property];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into mg_Property(");
            strSql.Append("prop_id,Prop_type,prop_name)");
            strSql.Append(" values (");
            strSql.Append("@i,@Prop_type,@prop_name);");
            SqlParameter[] parameters = {
					new SqlParameter("@Prop_type", SqlDbType.Int),
					new SqlParameter("@prop_name", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = 2;
            parameters[1].Value = model.prop_name;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql, parameters);
            return rows;
        }

        /// <summary>
        /// 更新颜色
        /// lx 2017-06-22
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateColor(mg_PropertyModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_Property set ");
            strSql.Append("prop_name=@prop_name");
            strSql.Append(" where prop_id=@prop_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@prop_id", SqlDbType.Int),
					new SqlParameter("@prop_name", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = model.prop_id;
            parameters[1].Value = model.prop_name;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 删除颜色
        /// lx 2016-06-22
        /// </summary>
        /// <param name="op_id"></param>
        /// <returns></returns>
        public static int DeleteColor(string prop_id)
        {

            string sql = "DELETE FROM [mg_Property] WHERE prop_id=" + prop_id;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            return rows;
        }
    }
}
