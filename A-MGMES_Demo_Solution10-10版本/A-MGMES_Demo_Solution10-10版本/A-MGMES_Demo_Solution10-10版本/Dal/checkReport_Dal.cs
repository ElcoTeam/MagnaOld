using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbUtility;
using Tools;
namespace Dal
{
   public class checkReport_Dal
    {
       public static DataTable getTable(int PageSize, int pageIndex, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr, out int total)
       {
           if(string.IsNullOrEmpty(SortFlag))
           {
               SortFlag = "ID";
           }
           if(string .IsNullOrEmpty(sortOrder))
           {
               sortOrder = "asc";
           }
           StringBuilder commandText = new StringBuilder();
           commandText.Append("select distinct a.ID,a.StationNO,d.op_name,c.PI_Item,case when a.IsPass = 1 then '合格' when a.IsPass = 0 then '不合格' else '未点检' end as IsPass,a.CreateTime from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE 1=1 ");
           commandText.Append(wherestr);//这里修改条件语句
           commandText.Append(" order by a."+SortFlag+" "+ sortOrder);
           string query_sql = commandText.ToString();
           StringBuilder commandText1 = new StringBuilder();
           commandText1.Append("select distinct a.ID,a.StationNO,d.op_name,c.PI_Item,case when a.IsPass = 1 then '合格' when a.IsPass = 0 then '不合格' else '未点检' end as IsPass,a.CreateTime from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE 1=1 ");
           commandText1.Append(wherestr);//这里修改条件语句
           string count_sql = "select count(*) as total from  ("+ commandText1.ToString() +" ) result ";
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

       public static DataTable getTableExcel(int PageSize, int pageIndex, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr, out int total)
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
           commandText.Append("select distinct a.ID,a.StationNO,d.op_name,c.PI_Item,case when a.IsPass = 1 then '合格' when a.IsPass = 0 then '不合格' else '未点检' end as IsPass,a.CreateTime from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE 1=1 ");
           commandText.Append(wherestr);//这里修改条件语句
           commandText.Append(" order by a."+SortFlag+" "+ sortOrder);
           string query_sql = commandText.ToString();
           StringBuilder commandText1 = new StringBuilder();
           commandText1.Append("select distinct a.ID,a.StationNO,d.op_name,c.PI_Item,case when a.IsPass = 1 then '合格' when a.IsPass = 0 then '不合格' else '未点检' end as IsPass,a.CreateTime from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE 1=1 ");
           commandText1.Append(wherestr);//这里修改条件语句
           string count_sql = "select count(*) as total from  (" + commandText1.ToString() + " ) result ";
          // string count_sql = "select count(*) as total from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.PI_ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE  1=1 " + wherestr;
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
    }
}
