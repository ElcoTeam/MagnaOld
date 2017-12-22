using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using DbUtility;
using Tools;
namespace Dal
{
    public class Production_ModelDAL
    {
        public static DataTable getTable(int PageSize, int StartIndex, int EndIndex, string sort, string order,string wherestr,  out int total)
       {
            string SortFlag="";
            string sortOrder="";
            if (string.IsNullOrEmpty(sort))
            {
                SortFlag = "id";
            }
            if (string.IsNullOrEmpty(order))
            {
                sortOrder = "asc";
            }
            string query_sql="";
            if(EndIndex == -1)
            {
                query_sql = " select * from(select row_number() over(order by " + SortFlag + " " + sortOrder + " ) as rowid,report.* from Sheet report  where 1 = 1 " + wherestr + ") as Results where rowid >=" + StartIndex + " ";
            }

            string count_sql = "select  count(*) as total from Sheet where 1 = 1 " + wherestr;
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
        public static DataListModel<Production_Model> GetList(int page, int pagesize, string sidx, string sord, string Where)
        {
            List<Production_Model> modelList = new List<Production_Model>();
            List<Production_Model> footerList = new List<Production_Model>();
            DataListModel<Production_Model> modeldata = new DataListModel<Production_Model>();     
                int returnValue = 0;
                int total = 0;
                int StartIndex = (page - 1) * pagesize + 1;
                int EndIndex = page * pagesize;
                string SortFlag = sidx;
                string sortOrder = sord;
            string wherestr = Where;
                string sql = @"SELECT  [id] 
      ,[product_date]
      ,[cl_name]
      ,[hourid]
      ,[customer_num]
      ,[real_num]
	  ,([real_num] - [customer_num]) as real_customer
      ,[plan_pro_num]
      ,[real_pro_num]
	  ,([real_pro_num] - [plan_pro_num]) as real_plan
	  ,case when ([plan_pro_num]>0)
	  then round([real_pro_num]*100.00/[plan_pro_num],1)
	  else 0.0 end as real_biplan
      ,[line1_finish_num]
      ,[line1_repair_num]
	   ,case when ([line1_finish_num]>0)
	  then round([line1_repair_num]*100.00/[line1_finish_num],1)
	  else 0.0 end as line1_FTT
      ,[line2_finish_num]
      ,[line2_repair_num]
	   ,case when ([line2_finish_num]>0)
	  then round([line2_repair_num]*100.00/[line2_finish_num],1)
	  else 0.0 end as line2_FTT
      ,[line3_finish_num]
      ,[line3_repair_num]
	  ,case when ([line3_finish_num]>0)
	  then round([line3_repair_num]*100.00/[line3_finish_num],1)
	  else 0.0 end as line3_FTT
      ,[line4_finish_num]
      ,[line4_repair_num]
      ,case when ([line4_finish_num]>0)
	  then round([line4_repair_num]*100.00/[line4_finish_num],1)
	  else 0.0 end as line4_FTT
      ,[line5_finish_num]
      ,[line5_repair_num]
      ,case when ([line5_finish_num]>0)
	  then round([line5_repair_num]*100.00/[line5_finish_num],1)
	  else 0.0 end as line5_FTT
     ,[line6_finish_num]
      ,[line6_repair_num]
      ,case when ([line6_finish_num]>0)
	  then round([line6_repair_num]*100.00/[line6_finish_num],1)
	  else 0.0 end as line6_FTT
  FROM [MagnaDB].[dbo].[Sheet] where 1=1 " + Where;
              
                if (string.IsNullOrEmpty(SortFlag))
            {
                SortFlag = "ID";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }
            StringBuilder commandText = new StringBuilder();
            commandText.Append(" SELECT TOP " + pagesize + " * FROM (SELECT ROW_NUMBER() OVER (ORDER BY "+SortFlag+" "+sortOrder +") AS RowNumber,* FROM Sheet  where 1=1 ");
            commandText.Append(wherestr);//这里修改条件语句
            commandText.Append(" ) AS T  WHERE RowNumber >= " + StartIndex + " and RowNumber <= " + EndIndex);
            string query_sql = commandText.ToString();
            string count_sql = " select  count(*) as total from Sheet where 1 = 1 " + wherestr;
            string sum_sql= @" select '汇总' as id 
,null as product_date
,null as cl_name
,null as hourid
     ,sum(customer_num) as customer_num
     ,sum([real_num])   as real_num
     ,sum([real_customer]) as [real_customer]
      , sum([plan_pro_num]) as [plan_pro_num]
      ,sum([real_pro_num]) as [real_pro_num]
      ,sum([real_plan]) as [real_plan]
,null as [real_biplan]
      ,sum([line1_finish_num]) as [line1_finish_num]
      ,sum ([line1_repair_num]) as [line1_repair_num]
,null as [line1_FTT]
        ,sum([line2_finish_num]) as [line2_finish_num]
      ,sum ([line2_repair_num]) as [line2_repair_num]
,null as [line2_FTT]
  ,sum([line3_finish_num]) as [line3_finish_num]
      ,sum ([line3_repair_num]) as [line3_repair_num]
,null as [line3_FTT]
  ,sum([line4_finish_num]) as [line4_finish_num]
      ,sum ([line4_repair_num]) as [line4_repair_num]
,null as [line4_FTT]
  ,sum([line5_finish_num]) as [line5_finish_num]
      ,sum ([line5_repair_num]) as [line5_repair_num]
,null as [line5_FTT]
  ,sum([line6_finish_num]) as [line6_finish_num]
      ,sum ([line6_repair_num]) as [line6_repair_num]
,null as [line6_FTT]
  from ";
            sum_sql += " (" + query_sql + ")a ";
           sum_sql = sum_sql.Replace("\n", string.Empty).Replace("\r", string.Empty);
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, count_sql + query_sql+sum_sql, new string[] { "count", "data","footer" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt1.Rows[0], "total"));
                DataTable dt2 = ds.Tables["data"];
                DataTable footer = ds.Tables["footer"];
                foreach (DataRow row in dt2.Rows)
                {
                    Production_Model model = new Production_Model();
                    model.id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "id"));
                    model.product_date = DataHelper.GetCellDataToStr(row, "product_date").Substring(0,10);
                    model.cl_name = DataHelper.GetCellDataToStr(row, "cl_name");
                    model.hourid = DataHelper.GetCellDataToStr(row, "hourid");
                    model.customer_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "customer_num"));
                    model.real_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "real_num"));
                    model.real_customer = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "real_customer"));
                    model.plan_pro_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "plan_pro_num"));
                    model.real_pro_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "real_pro_num"));
                    model.real_plan = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "real_plan"));
                    model.real_biplan = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "real_biplan"));
                    model.real_biplan = NumericParse.CutDecimalWithN(model.real_biplan, 3);
                    model.line1_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line1_finish_num"));
                    model.line1_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line1_repair_num"));
                    model.line1_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line1_FTT"));
                    model.line1_FTT = NumericParse.CutDecimalWithN(model.line1_FTT, 3);
                    model.line2_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line2_finish_num"));
                    model.line2_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line2_repair_num"));
                    model.line2_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line2_FTT"));
                    model.line2_FTT = NumericParse.CutDecimalWithN(model.line2_FTT, 3);
                    model.line3_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line3_finish_num"));
                    model.line3_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line3_repair_num"));
                    model.line3_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line3_FTT"));
                    model.line3_FTT = NumericParse.CutDecimalWithN(model.line3_FTT, 3);
                    model.line4_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line4_finish_num"));
                    model.line4_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line4_repair_num"));
                    model.line4_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line4_FTT"));
                    model.line4_FTT = NumericParse.CutDecimalWithN(model.line4_FTT, 3);
                    model.line5_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line5_finish_num"));
                    model.line5_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line5_repair_num"));
                    model.line5_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line5_FTT"));
                    model.line5_FTT = NumericParse.CutDecimalWithN(model.line5_FTT, 3);
                    model.line6_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line6_finish_num"));
                    model.line6_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line6_repair_num"));
                    model.line6_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line6_FTT"));
                    model.line6_FTT = NumericParse.CutDecimalWithN(model.line6_FTT, 3);
                    


                    modelList.Add(model);
                }
                foreach (DataRow row in footer.Rows)
                {
                    Production_Model model = new Production_Model();
                    model.id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "id"));
                    model.product_date = DataHelper.GetCellDataToStr(row, "product_date");
                    model.cl_name = DataHelper.GetCellDataToStr(row, "cl_name");
                    model.hourid = DataHelper.GetCellDataToStr(row, "hourid");
                    model.customer_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "customer_num"));
                    model.real_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "real_num"));
                    model.real_customer = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "real_customer"));
                    model.plan_pro_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "plan_pro_num"));
                    model.real_pro_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "real_pro_num"));
                    model.real_plan = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "real_plan"));
                    model.real_biplan = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "real_biplan"));
                    model.line1_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line1_finish_num"));
                    model.line1_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line1_repair_num"));
                    model.line1_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line1_FTT"));
                    model.line2_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line2_finish_num"));
                    model.line2_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line2_repair_num"));
                    model.line2_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line2_FTT"));
                    model.line3_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line3_finish_num"));
                    model.line3_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line3_repair_num"));
                    model.line3_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line3_FTT"));
                    model.line4_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line4_finish_num"));
                    model.line4_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line4_repair_num"));
                    model.line4_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line4_FTT"));
                    model.line5_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line5_finish_num"));
                    model.line5_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line5_repair_num"));
                    model.line5_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line5_FTT"));
                    model.line6_finish_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line6_finish_num"));
                    model.line6_repair_num = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line6_repair_num"));
                    model.line6_FTT = NumericParse.StringToDecimal(DataHelper.GetCellDataToStr(row, "line6_FTT"));



                    footerList.Add(model);
                }
                DataListModel<Production_Model> allmodel = new DataListModel<Production_Model>();
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
        public static DataTable GetClassInfo()
        {
            DataTable tb = new DataTable();
            string sql = @"SELECT  distinct [cl_id], [cl_name] FROM [MagnaDB].[dbo].[mg_classes] where 1=1 ";
            tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);         
            return tb;
        }
    }
}
