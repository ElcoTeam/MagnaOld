using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbUtility;
using Tools;
namespace Dal
{
   public  class TransportHistory_Dal
    {
       public static DataTable getTable(int Pagesize, int pageIndex, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr, out int total)
       {
           if (string.IsNullOrEmpty(SortFlag))
           {
               SortFlag = "ID";
           }
           if (string.IsNullOrEmpty(sortOrder))
           {
               sortOrder = "asc";
           }
           StringBuilder commandText = new StringBuilder();
           commandText.Append("SELECT TOP " + Pagesize + " ID,订单类型,订单编码,下单时间,车型ID,车型,订单状态,前排故障历史,前排是否修复,主驾,主驾气囊,副驾,副驾气囊,[40%] 百分之40,[60%] 百分之60,[100%] 百分之100,卷收器,MainOrderID,VINNumber,回写SAP FROM (SELECT ROW_NUMBER() OVER (ORDER BY " + SortFlag + "  " + sortOrder + " ) AS RowNumber,* FROM ViewOrders where 1=1  ");
           commandText.Append(wherestr);//这里修改条件语句
           commandText.Append(" ) AS T  WHERE RowNumber >= "+StartIndex +" and RowNumber <= "+EndIndex);
           string query_sql = commandText.ToString();
           string count_sql = "select  count(*) as total from ViewOrders where 1 = 1 " + wherestr;
           DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, count_sql + query_sql, new string[] { "count", "data" }, null);
           if (DataHelper.HasData(ds))
           {
               DataTable dt1 = ds.Tables["count"];
               total = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt1.Rows[0], "total"));
               DataTable dt2 = ds.Tables["data"];
               return dt2;
           }
           else
           {
               total = 0;
               return null;
           }
       }


       public static DataTable getTableExcel( string SortFlag, string sortOrder, string wherestr, out int total)
       {
           if(string.IsNullOrEmpty(SortFlag))
           {
               SortFlag = "ID";
           }
           if(string.IsNullOrEmpty(sortOrder))
           {
               sortOrder = "asc";
           }
           string query_sql = "SELECT ID,订单类型,订单编码,下单时间,车型ID,车型,订单状态,前排故障历史,前排是否修复,主驾,主驾气囊,副驾,副驾气囊,[40%] 百分之40,[60%] 百分之60,[100%] 百分之100,卷收器,MainOrderID,VINNumber,回写SAP FROM ViewOrders WHERE 1=1  " + wherestr + "order by " + SortFlag + " " + sortOrder;
           string count_sql = "select  count(*) as total from ViewOrders where 1 = 1 " + wherestr;
           DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, count_sql + query_sql, new string[] { "count", "data" }, null);
           if (DataHelper.HasData(ds))
           {
               DataTable dt1 = ds.Tables["count"];
               total = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt1.Rows[0], "total"));
               DataTable resTable2 = ds.Tables["data"];
               for (int i = 0; i < resTable2.Rows.Count; i++)
               {
                   resTable2.Rows[i]["主驾"] = "\t" + resTable2.Rows[i]["主驾"].ToString();
                   resTable2.Rows[i]["副驾"] = "\t" + resTable2.Rows[i]["副驾"].ToString();
                   resTable2.Rows[i]["百分之40"] = "\t" + resTable2.Rows[i]["百分之40"].ToString();
                   resTable2.Rows[i]["百分之60"] = "\t" + resTable2.Rows[i]["百分之60"].ToString();
                   resTable2.Rows[i]["百分之100"] = "\t" + resTable2.Rows[i]["百分之100"].ToString();
               }
               return resTable2;
           }
           else
           {
               total = 0;
               return null;
           }
       }
    }
}
