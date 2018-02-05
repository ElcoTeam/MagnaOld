using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Tools;
using DbUtility;
namespace Dal
{
    public class FTT_Dal
    {
        public static DataTable getTable(int PageSize, int pageIndex, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr, out int total)
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
            commandText.Append("SELECT TOP " + PageSize + " * FROM (SELECT ROW_NUMBER() OVER (ORDER BY "+SortFlag+" "+sortOrder +") AS RowNumber,* FROM mg_Report_FTT  where 1=1 ");
            commandText.Append(wherestr);//这里修改条件语句
            commandText.Append(" ) AS T  WHERE RowNumber >= " + StartIndex + " and RowNumber <= " + EndIndex);
            string query_sql = commandText.ToString();
            string count_sql = "select  count(*) as total from mg_Report_FTT where 1 = 1 " + wherestr;
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
            commandText.Append("SELECT * FROM mg_Report_FTT  where 1=1 ");
            commandText.Append(wherestr);//这里修改条件语句
    
            string query_sql = commandText.ToString();
            string count_sql = "select  count(*) as total from mg_Report_FTT where 1 = 1 " + wherestr;
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
