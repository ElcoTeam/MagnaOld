using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Tools;
using DbUtility;
using Model;
namespace Dal
{
    public class Production_AlarmTrendDAL
    {
        public  static DataListModel<Production_AlarmModel> GetListNew(string StartTime,string EndTime,int StartIndex,int EndIndex)
        {
            List<Production_AlarmModel> modelList = new List<Production_AlarmModel>();
            List<Production_AlarmModel> footerList = new List<Production_AlarmModel>();
            DataListModel<Production_AlarmModel> modeldata = new DataListModel<Production_AlarmModel>();
            int total=0;
            SqlParameter[] sqlPara = new SqlParameter[4];
                sqlPara[0] = new SqlParameter("@start_time", StartTime);
                sqlPara[1] = new SqlParameter("@end_time", EndTime);
                sqlPara[2] = new SqlParameter("@start_index", StartIndex);
                sqlPara[3] = new SqlParameter("@end_index", EndIndex);
            DataSet ds = SqlHelper.RunProcedureTables(SqlHelper.SqlConnString, "Proc_Rpt_AlarmTrend", sqlPara, new string[] { "data", "footer" ,"count"});
            if (DataHelper.HasData(ds))
            {

                DataTable dt2 = ds.Tables["data"];
                DataTable footer = ds.Tables["footer"];
                DataTable dt1 = ds.Tables["count"];
                total = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt1.Rows[0], "total"));
                foreach (DataRow row in dt2.Rows)
                {
                    Production_AlarmModel model = new Production_AlarmModel();
                    // model.id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "id"));
                    // model.product_date = DataHelper.GetCellDataToStr(row, "product_date").Substring(0, 10);
                    string strtest = DataHelper.GetCellDataToStr(row, "product_date");
                    string str = DataHelper.GetCellDataToStr(row, "product_date").Split(' ')[0];
                    if (str.Length > 0)
                    {
                        model.product_date = str;
                    }
                    else
                    {
                        model.product_date = DataHelper.GetCellDataToStr(row, "product_date");
                    }
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
                    model.product_date = DataHelper.GetCellDataToStr(row, "product_date");
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
        public static DataTable getTable(string StartTime, string EndTime, int PageSize, int StartIndex, int EndIndex, string sort, string order, string wherestr, out int total)
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
            
            SqlParameter[] sqlPara = new SqlParameter[4];
            sqlPara[0] = new SqlParameter("@start_time", StartTime);
            sqlPara[1] = new SqlParameter("@end_time", EndTime);
            sqlPara[2] = new SqlParameter("@start_index", StartIndex);
            sqlPara[3] = new SqlParameter("@end_index", EndIndex);
            DataSet ds = SqlHelper.RunProcedureTables(SqlHelper.SqlConnString, "Proc_Rpt_AlarmTrend", sqlPara, new string[] { "data", "footer","count" });
            if (DataHelper.HasData(ds))
            {
                
                DataTable dt1 = ds.Tables["data"];
                DataTable dt2 = ds.Tables["footer"];
                total = dt1.Rows.Count;
                int tableNum = 2;
                DataTable dt = ds.Tables[1].Clone();
                if (dt != null)
                {
                    for (int i = 0; i < tableNum; i++)
                    {
                        dt.Merge(ds.Tables[i]);
                    }
                }
                return dt;

               
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
            string sql = @" select distinct AlarmStartTime as product_date
  ,sum(case when AlarmType=1 then 1 else 0 end ) as material_num
  ,sum(case when AlarmType=2 then 1 else 0 end) as quality_num
  ,sum(case when AlarmType = 3 then 1 else 0 end) as maintenance_num
  ,sum(case when AlarmType =4 then 1 else 0 end) as overcycle_num
  ,sum(case when AlarmType = 5 then 1 else 0 end) as production_num
  ,sum(case when AlarmType<6 then 1 else 0 end )as total_num
  from  
 (select AlarmType,AlarmText, convert(varchar(10),AlarmStartTime,120) AlarmStartTime,convert(varchar(10),AlarmEndTime,120) AlarmEndTime,StartOrderNo,EndOrderNo,IsSolve,OperatorID from mg_alarm
 where 1=1 " + wherestr+") report";


            if (string.IsNullOrEmpty(SortFlag))
            {
                SortFlag = "AlarmStartTime";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }
            string group_sql = "  group by AlarmStartTime";
            string order_sql = "  order by " + SortFlag + " " + sortOrder;
            string query_sql = sql + group_sql;
            string sum_sql = @" select 'summary' as product_date
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
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, query_sql + sum_sql, new string[] { "data", "footer" }, null);
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
                    model.product_date = DataHelper.GetCellDataToStr(row, "product_date");
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
                    model.product_date = DataHelper.GetCellDataToStr(row, "product_date");
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
