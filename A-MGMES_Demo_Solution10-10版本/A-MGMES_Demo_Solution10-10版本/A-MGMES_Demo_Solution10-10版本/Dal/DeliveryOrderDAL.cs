using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Tools;
using DbUtility;
using Model;
using System.Configuration;

namespace Dal
{
    public class DeliveryOrderDAL
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderno"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getTable(string currentpage, string pagesize, string orderno, out int total)
        {
            
            StringBuilder commandText = new StringBuilder();
            string str = @"select  top " + pagesize + " t.* from (select ROW_NUMBER()over(order by CreateTime) as rowid ,* from (select distinct PARTN,PRODN,right(PDATUM,4)+'-'+Substring(PDATUM,4,2)+'-'+left(PDATUM,2) PDATUM,CreateTime,OrderIsHistory from mg_InteractionData_LineUpOrder where PRODN not in (select JITCallNumber from  mg_CustomerOrder_3) and CreateTime BETWEEN dateadd(month,-1,getdate()) and getdate()";
            
            commandText.Append(str);

            StringBuilder count_sql = new StringBuilder();
            count_sql.Append(" select count(*) as total from  (select distinct PARTN,PRODN,PDATUM,CreateTime from mg_InteractionData_LineUpOrder where PRODN not in (select JITCallNumber from  mg_CustomerOrder_3) and CreateTime BETWEEN dateadd(month,-1,getdate()) and getdate()");

            if(!string.IsNullOrEmpty(orderno))
            {
                commandText.Append(" and PRODN like '%"+orderno+"%'");
                count_sql.Append(" and PRODN like '%" + orderno + "%'");
            }

            commandText.Append("  )as A) t  where rowid> (" + pagesize + ")*((" + currentpage + ")-1)");
            count_sql.Append(") result");

           

            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, commandText + count_sql.ToString(), new string[] { "data", "count" }, null);
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

        /// <summary>
        /// 降低库存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int EditDeliveryOrder(string PRODN, string OrderIsHistory)
        {
            
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ToString()))
            {
                SqlCommand cmd = new SqlCommand();
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = conn;
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter[] parameters = 
                    {
					    new SqlParameter("@JITCallNumber", PRODN),
					    new SqlParameter("@OrderIsHistory", OrderIsHistory)
                    };
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select * from  mg_CustomerOrder_3");
                    strSql.Append(" where JITCallNumber='"+PRODN+"' ");

                    int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString());

                    if(rows>0)
                    {
                        return 0;
                    }
                    else
                    {
                        StringBuilder updateSql = new StringBuilder();
                        updateSql.Append(" update mg_InteractionData_LineUpOrder set ");
                        updateSql.Append(" OrderIsHistory='"+OrderIsHistory+"'");
                        updateSql.Append(" where PRODN='" + PRODN + "'");

                        int result = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, updateSql.ToString());

                        return result;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return -1;
                }
            }
            
        }
    }
}
