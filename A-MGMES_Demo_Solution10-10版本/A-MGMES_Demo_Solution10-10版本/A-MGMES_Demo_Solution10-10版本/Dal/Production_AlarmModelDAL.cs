using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using Tools;
using DbUtility;
namespace Dal
{
    public class Production_AlarmModelDAL
    {
        public static DataTable getTable(int PageSize, int StartIndex, int EndIndex, string sort, string order, string wherestr, out int total)
        {
            string SortFlag = "";
            string sortOrder = "";
            if (string.IsNullOrEmpty(sort))
            {
                SortFlag = "id";
            }
            if (string.IsNullOrEmpty(order))
            {
                sortOrder = "asc";
            }
            string query_sql = "";
            if (EndIndex == -1)
            {
                query_sql = " select * from(select row_number() over(order by " + SortFlag + " " + sortOrder + " ) as rowid,report.* from Sheet report  where 1 = 1 " + wherestr + ") as Results where rowid >=" + StartIndex + " ";
            }

            string count_sql = "select  count(*) as total from View_mg_sys_log where 1 = 1 " + wherestr;
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
        public static DataListModel<Production_AlarmModel> GetList(int page, int pagesize, string sidx, string sord, string Where)
        {
            List<Production_AlarmModel> modelList = new List<Production_AlarmModel>();
            List<Production_AlarmModel> footerList = new List<Production_AlarmModel>();
            DataListModel<Production_AlarmModel> modeldata = new DataListModel<Production_AlarmModel>();
            int returnValue = 0;
            int total = 0;
            int StartIndex = (page - 1) * pagesize + 1;
            int EndIndex = page * pagesize;
            string SortFlag = sidx;
            string sortOrder = sord;
            string wherestr = Where;
            string sql = @"select distinct AlarmStation as stationNo
  ,sum(case when AlarmType=1 then 1 else 0 end ) as material_num
  ,sum(case when AlarmType=2 then 1 else 0 end) as quality_num
  ,sum(case when AlarmType = 3 then 1 else 0 end) as maintenance_num
  ,sum(case when AlarmType =4 then 1 else 0 end) as overcycle_num
  ,sum(case when AlarmType = 5 then 1 else 0 end) as production_num
  ,sum(case when AlarmType<6 then 1 else 0 end )as total_num
  from mg_alarm 
  where 1=1  " + Where;
              
            if (string.IsNullOrEmpty(SortFlag))
            {
                SortFlag = "stationNo";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }
            string group_sql = "  group by AlarmStation";
            string order_sql = "  order by " + SortFlag + " " + sortOrder;
            string query_sql = sql + group_sql;
            string sum_sql = @" select 'summary' as stationNO
  , sum(material_num) as material_num
  , sum(quality_num) as quality_num
  ,sum(maintenance_num) as maintenance_num
  ,sum(overcycle_num) as overcycle_num
  ,sum(production_num) as production_num
,sum(total_num) as total_num
  from  ";
            sum_sql += " (" + query_sql + ")a ";
            sum_sql = sum_sql.Replace("\n", string.Empty).Replace("\r", string.Empty);
            query_sql += order_sql;
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text,  query_sql + sum_sql, new string[] {  "data", "footer" }, null);
            if (DataHelper.HasData(ds))
            {
               
                DataTable dt2 = ds.Tables["data"];
                total = dt2.Rows.Count;
                DataTable footer = ds.Tables["footer"];
                foreach (DataRow row in dt2.Rows)
                {
                    Production_AlarmModel model = new Production_AlarmModel();
                   // model.id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "id"));
                   // model.product_date = DataHelper.GetCellDataToStr(row, "product_date").Substring(0, 10);
                    model.stationNo = DataHelper.GetCellDataToStr(row, "stationNo");
                  
                    model.material_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "material_num"));
                    model.production_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "production_num"));
                    model.maintenance_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "maintenance_num"));
                    model.quality_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "quality_num"));
                    model.overcycle_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "overcycle_num"));
                    model.total_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "total_num"));
                   
                    modelList.Add(model);
                }
                foreach (DataRow row in footer.Rows)
                {
                    Production_AlarmModel model = new Production_AlarmModel();
                    //model.id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "id"));
                    //model.product_date = DataHelper.GetCellDataToStr(row, "product_date");
                    model.stationNo = DataHelper.GetCellDataToStr(row, "stationNo");
                    model.material_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "material_num"));
                    model.production_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "production_num"));
                    model.maintenance_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "maintenance_num"));
                    model.quality_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "quality_num"));
                    model.overcycle_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "overcycle_num"));
                    model.total_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "total_num"));
                    footerList.Add(model);
                }
                DataListModel<Production_AlarmModel> allmodel = new DataListModel<Production_AlarmModel>();
                allmodel.total = total.ToString();
                allmodel.rows = modelList;
                allmodel.footer = footerList;
                return allmodel;

            }
            else
            {
                total = 0;
                return null;
            }

        }
    }
}
