using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Tools;
using DBUtility;
using Model;


namespace DAL
{
    public class mg_CuttedOrderDAL
    {
        public static DataTable GetData(string tablename, string preChar)
        {
            string sql = @"
                            SELECT top 1 " + preChar + @".[o" + preChar + @"_pre]+" + preChar + @".[part_no]+" + preChar + @".[o" + preChar + @"_cdstr]+right('0000'+ltrim(" + preChar + @".[o" + preChar + @"_id]),4)orderno
                            ,[o" + preChar + @"_id] id," + preChar + @".co_id coid
     
                              FROM [" + tablename + @"] " + preChar + @" 
                              inner join mg_CustomerOrder co on " + preChar + @".co_id = co.co_id
                              where (o" + preChar + @"_isPLC is null or  o" + preChar + @"_isPLC <>1)
                              order by co.co_order," + preChar + @".o" + preChar + @"_id";

            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static void IsPublished(string tablename, string preChar, string co_id, string id)
        {
            string sql = " UPDATE " + tablename + " set [o" + preChar + @"_isPLC] =1 WHERE [o" + preChar + @"_id] =" + id + "  and  [co_id] =" + co_id;

            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetzhuData(string tablenameZhu,string preCharZhu,string tablename,string preChar)
        {
            string sql = @"SELECT top 1
                            " + preCharZhu + @".o" + preCharZhu + @"_id
                                  ," + preCharZhu + @".[o" + preCharZhu + @"_isPLC] zhuplc
                                  ," + preChar + @".[o" + preChar + @"_isPLC] fuplc
                              FROM [" + tablenameZhu + @"] " + preCharZhu + @"
                              inner join [" + tablename + @"] " + preChar + @" on " + preCharZhu + @".co_id=" + preChar + @".co_id and " + preCharZhu + @".o" + preCharZhu + @"_id=" + preChar + @".o" + preChar + @"_id
                               inner join mg_CustomerOrder co on " + preCharZhu + @".co_id = co.co_id and co.co_isCutted=1 and " + preCharZhu + @".o" + preCharZhu + @"_producing=0
                               where " + preCharZhu + @".o" + preCharZhu + @"_isPLC = 1
                               order by co.co_order desc ," + preCharZhu + @".o" + preCharZhu + @"_id desc";

            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }



        #region 较早代码
        public static DataTable GetFSDData(bool isFSDzhu)
        {
            string sql = "";
            if (isFSDzhu)
            {
                sql = @"
                            SELECT top 1 fsd.[ofsd_pre]+fsd.[part_no]+fsd.[ofsd_cdstr]+right('0000'+ltrim(fsd.[ofsd_id]),4)orderno
                            ,[ofsd_id] id,fsd.co_id coid
     
                              FROM [mg_Order_FSD] fsd 
                              inner join mg_CustomerOrder co on fsd.co_id = co.co_id
                              where (ofsd_isPLC is null or  ofsd_isPLC <>1)
                              order by co.co_order,fsd.ofsd_id";
            }
            else
            {
                sql = @"
                              
                            SELECT top 1 fsp.[ofsp_pre]+fsp.[part_no]+fsp.[ofsp_cdstr]+right('0000'+ltrim(fsp.[ofsp_id]),4)orderno
                            ,[ofsp_id] id,fsp.co_id coid
     
                              FROM mg_Order_FSP fsp 
                              inner join mg_CustomerOrder co on fsp.co_id = co.co_id
                              where (ofsp_isPLC is null or  ofsp_isPLC <>1)
                              order by co.co_order,fsp.ofsp_id";
            }


            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        //public static void IsPublished(string co_id, string id, bool isFSDzhu)
        //{
        //    string sql = "";
        //    if (isFSDzhu)
        //    {
        //        sql = " UPDATE mg_Order_FSD set [ofsd_isPLC] =1 WHERE [ofsd_id] =" + id + "  and  [co_id] =" + co_id;
        //    }
        //    else
        //    {
        //        sql = " UPDATE  mg_Order_FSP set [ofsp_isPLC] =1 WHERE [ofsp_id] =" + id + "  and  [co_id] =" + co_id;
        //    }

        //    SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        //}

        public static DataTable GetFSzhuData()
        {
            string sql = @"SELECT top 1
                            fsd.ofsd_id
                                  ,fsd.[ofsd_isPLC]
                                  ,fsp.[ofsp_isPLC]
                              FROM [mg_Order_FSD] fsd
                              inner join [mg_Order_FSP] fsp on fsd.co_id=fsp.co_id and fsd.ofsd_id=fsp.ofsp_id
                               inner join mg_CustomerOrder co on fsd.co_id = co.co_id and co.co_isCutted=1 and fsd.ofsd_unproducing=1
                               where fsd.ofsd_isPLC = 1
                               order by co.co_order desc ,fsd.ofsd_id desc";

            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        #endregion
    }
}
