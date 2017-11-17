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
   public class mg_TestDAL
    {
       public static List<mg_TestModel> QueryTestList()
       {
           List<mg_TestModel> list = null;
           string sql = @"SELECT [ID],[TestCaption]  FROM [mg_Test] ";
           DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
           if (DataHelper.HasData(dt))
           {
               list = new List<mg_TestModel>();
               foreach (DataRow row in dt.Rows)
               {
                   mg_TestModel model = new mg_TestModel();
                   model.t_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                   model.testcaption = DataHelper.GetCellDataToStr(row, "TestCaption");
                   list.Add(model);
               }
           }
           return list;
       }
    }
}
