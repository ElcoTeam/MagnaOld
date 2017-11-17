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
    }
}
