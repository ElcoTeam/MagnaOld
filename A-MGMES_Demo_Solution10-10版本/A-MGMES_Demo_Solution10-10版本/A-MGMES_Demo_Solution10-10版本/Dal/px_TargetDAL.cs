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
    public class px_TargetDAL
    {
      
     
        public static int UpdateTarget(Model.px_TargetModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update px_target set ");           
            strSql.Append("pxtarget_time=@pxtarget_time,");
            strSql.Append("pxtarget_target=@pxtarget_target");
            strSql.Append(" where pxtarget_id=@pxtarget_id");
            SqlParameter[] parameters = {
					new SqlParameter("@pxtarget_time", SqlDbType.NVarChar),
					new SqlParameter("@pxtarget_target", SqlDbType.Int),					
                    new SqlParameter("@pxtarget_id", SqlDbType.Int)};
            parameters[0].Value = model.pxtarget_time;
            parameters[1].Value = model.pxtarget_target;          
            parameters[2].Value = model.pxtarget_id;           

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static List<px_TargetModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<px_TargetModel> list = null;

            string sql1 = @"select count(pxtarget_id) total from [px_target];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [pxtarget_id]
                                  ,[pxtarget_time],[pxtarget_target]                               
                                 
                              FROM  [px_target] 
	                            order by pxtarget_id asc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<px_TargetModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    px_TargetModel model = new px_TargetModel();
                    model.pxtarget_id = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "pxtarget_id"));
                    model.pxtarget_time = DataHelper.GetCellDataToStr(row, "pxtarget_time");
                    model.pxtarget_target =  Convert.ToInt32(DataHelper.GetCellDataToStr(row, "pxtarget_target"));
                  

                    list.Add(model);
                }
            }
            return list;
        }

        public static List<px_TargetModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<px_TargetModel> list = null;

            string sql1 = @"select count(pxtarget_id) total from [px_target];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [pxtarget_id]
                                  ,[pxtarget_time]
                                  ,[pxtarget_target]                  
                              FROM  [px_target] 
                                where  pxtarget_id > (
                                                select top 1 pxtarget_id from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") pxtarget_id from  [px_target] where pxtarget_id is not null  order by pxtarget_id asc )t
                                                order by  pxtarget_id desc)
                                
	                            order by pxtarget_id asc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<px_TargetModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    px_TargetModel model = new px_TargetModel();

                    model.pxtarget_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pxtarget_id"));
                    model.pxtarget_target = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "pxtarget_target"));
                    model.pxtarget_time = DataHelper.GetCellDataToStr(row, "pxtarget_time");
                   
                    list.Add(model);
                }
            }
            return list;
        }

   

     
    }
}
