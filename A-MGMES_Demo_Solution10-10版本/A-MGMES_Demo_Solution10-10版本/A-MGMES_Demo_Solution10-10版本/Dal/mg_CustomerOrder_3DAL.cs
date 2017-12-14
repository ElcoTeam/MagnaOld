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
    public  class mg_CustomerOrder_3DAL
    {
        public static int EditDeliveryOrder(mg_CustomerOrder_3 model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_CustomerOrder_3 set ");

            strSql.Append("OrderIsHistory=@OrderIsHistory ");
            strSql.Append(" where OrderID=@OrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.Decimal),
					new SqlParameter("@OrderIsHistory", SqlDbType.Int)};
            parameters[0].Value = model.OrderID1;
            parameters[1].Value = model._OrderIsHistory;
            
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);

            return rows;
        }
    }
}
