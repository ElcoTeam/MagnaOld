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
    public class px_SendToMobileDevDAL
    {
      
        public static int AddInternetPrinter(Model.px_InternetPrinterModel model)
        {
            string maxid = @"set identity_insert px_InternetPrinter ON;
                               declare @i int;
                                SELECT @i=max([IID])  FROM [px_InternetPrinter];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into px_InternetPrinter(");
            strSql.Append("IID,IName,PrintIP,IAddTime,IRole,IRamark)");
            strSql.Append(" values (");
            strSql.Append("@i,@IName,@PrintIP,@IAddTime,@IRole,@IRamark)");
            SqlParameter[] parameters = {
					new SqlParameter("@IName", SqlDbType.NVarChar),
					new SqlParameter("@PrintIP", SqlDbType.NVarChar),
                    new SqlParameter("@IAddTime", SqlDbType.DateTime),
                     new SqlParameter("@IRole", SqlDbType.NVarChar),
                    new SqlParameter("@IRamark", SqlDbType.NVarChar)};
            parameters[0].Value = model.IName;
            parameters[1].Value = model.PrintIP;
            parameters[2].Value = Convert.ToDateTime(model.IAddTime);
            parameters[3].Value = model.IRole;
            parameters[4].Value = model.IRamark;
            string end = @"set identity_insert px_InternetPrinter OFF";

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql + end, parameters);
            return rows;
        }

        public static int UpdateInternetPrinter(Model.px_InternetPrinterModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update px_InternetPrinter set ");
            strSql.Append("IName=@IName,");
            strSql.Append("PrintIP=@PrintIP,");
            strSql.Append("IAddTime=@IAddTime,");
            strSql.Append("IRole=@IRole,");
            strSql.Append("IRamark=@IRamark");
            strSql.Append(" where IID=@IID");
            SqlParameter[] parameters = {
					new SqlParameter("@IName", SqlDbType.NVarChar),
					new SqlParameter("@PrintIP", SqlDbType.NVarChar),
					new SqlParameter("@IAddTime", SqlDbType.DateTime),
					new SqlParameter("@IRole", SqlDbType.NVarChar),
                    new SqlParameter("@IRamark", SqlDbType.NVarChar),
                    new SqlParameter("@IID", SqlDbType.Int)};
            parameters[0].Value = model.IName;
            parameters[1].Value = model.PrintIP;
            parameters[2].Value = Convert.ToDateTime(model.IAddTime);
            parameters[3].Value = model.IRole;
            parameters[4].Value = model.IRamark;
            parameters[5].Value = Convert.ToInt32(model.IID);

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static int update_mg_CustomerOrder_3_OrderIsHistory(string caridStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_CustomerOrder_3 set OrderIsHistory=1 where SerialNumber=@SerialNumber ");         
            SqlParameter[] parameters = {
					new SqlParameter("@SerialNumber", SqlDbType.NVarChar)};
            parameters[0].Value = caridStr;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static List<px_InternetPrinterModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<px_InternetPrinterModel> list = null;

            string sql1 = @"select count(IID) total from [px_InternetPrinter];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [IID]
                                  ,[IName],[PrintIP]
                                  ,[IAddTime],[IRole],[IRamark]
                                 
                              FROM  [px_InternetPrinter] 
	                            order by IID asc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<px_InternetPrinterModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    px_InternetPrinterModel model = new px_InternetPrinterModel();
                    model.IID = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "IID"));
                    model.IName = DataHelper.GetCellDataToStr(row, "IName");
                    model.PrintIP = DataHelper.GetCellDataToStr(row, "PrintIP");
                    model.IAddTime = DataHelper.GetCellDataToStr(row, "IAddTime");
                    model.IRole = DataHelper.GetCellDataToStr(row, "IRole");
                    model.IRamark = DataHelper.GetCellDataToStr(row, "IRamark");

                    list.Add(model);
                }
            }
            return list;
        }

        public static List<px_InternetPrinterModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<px_InternetPrinterModel> list = null;

            string sql1 = @"select count(IID) total from [px_InternetPrinter];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [IID]
                                  ,[IName]
                                  ,[PrintIP] ,   [IAddTime],   [IRole],     [IRamark]                     
                              FROM  [px_InternetPrinter] 
                                where  IID > (
                                                select top 1 IID from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") IID from  [px_InternetPrinter] where IID is not null  order by IID asc )t
                                                order by  IID desc)
                                
	                            order by IID asc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<px_InternetPrinterModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    px_InternetPrinterModel model = new px_InternetPrinterModel();

                    model.IID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "IID"));
                    model.IName = DataHelper.GetCellDataToStr(row, "IName");
                    model.PrintIP = DataHelper.GetCellDataToStr(row, "PrintIP");
                    model.IAddTime = DataHelper.GetCellDataToStr(row, "IAddTime");
                    model.IRole = DataHelper.GetCellDataToStr(row, "IRole");
                    model.IRamark = DataHelper.GetCellDataToStr(row, "IRamark");
                    list.Add(model);
                }
            }
            return list;
        }

        public static int DeleteInternetPrinter(string IID)
        {
            string sql = @"delete from [px_InternetPrinter] where [IID]=" + IID;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static List<px_InternetPrinterModel> QueryInternetPrinterListForPart()
        {
            List<px_InternetPrinterModel> list = null;

            string sql2 = @" 
                            SELECT [IID]
                                  ,[IName]
                                  ,[PrintIP]    ,[IAddTime],[IRole],[IRamark]                             
                              FROM  [px_InternetPrinter]             
	                          order by IID asc 
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text,  sql2,  null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<px_InternetPrinterModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    px_InternetPrinterModel model = new px_InternetPrinterModel();

                    model.IID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "IID"));
                    model.IName = DataHelper.GetCellDataToStr(row, "IName");
                    model.PrintIP = DataHelper.GetCellDataToStr(row, "PrintIP");
                    model.IAddTime = DataHelper.GetCellDataToStr(row, "IAddTime");
                    model.IRole = DataHelper.GetCellDataToStr(row, "IRole");
                    model.IRamark = DataHelper.GetCellDataToStr(row, "IRamark");
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<View_px_AllList_DoHis> QueryView_px_AllList_DoHisList()
        {
            List<View_px_AllList_DoHis> list = null;
            //扫描数据【没有置·1的】          
            string sql = @"SELECT [车身号]
                          ,[主副驾]
                          ,[零件号描述]
                          ,[IsPrint]
                          ,[OrderIsHistory]
                          ,[abb]
                          ,[resultljh]
                      FROM [MagnaDB].[dbo].[AllList_DoHis]
                        where (zjppp = '靠背面套主驾' or 
                        zjppp = '靠背面套副驾' or 
                        zjppp = '坐垫面套主驾' or 
                        zjppp = '坐垫面套副驾' or 
                        zjppp = '坐垫骨架主驾' or 
                        zjppp = '坐垫骨架副驾' or 
                        zjppp = '靠背骨架主驾' or 
                        zjppp = '靠背骨架副驾' or 
                        zjppp = '线束主驾' or 
                        zjppp = '线束副驾' or 
                        zjppp = '大背板主驾' or 
                        zjppp = '大背板副驾' or 
                        zjppp = '靠背面套后40' or 
                        zjppp = '靠背面套后60' or 
                        zjppp = '坐垫面套后坐垫' or 
                        zjppp = '扶手后60' or 
                        zjppp = '中头枕后60' or 
                        zjppp = '侧头枕后40' or 
                        zjppp = '侧头枕后60' 
                        ) ORDER BY abb,resultljh desc";//DESC
                            
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<View_px_AllList_DoHis>();
                foreach (DataRow row in dt2.Rows)
                {
                    View_px_AllList_DoHis model = new View_px_AllList_DoHis();

                    model.车身号 = DataHelper.GetCellDataToStr(row, "车身号");
                    model.主副驾 = DataHelper.GetCellDataToStr(row, "主副驾");
                    model.零件号描述 = DataHelper.GetCellDataToStr(row, "零件号描述");
                    model.IsPrint = DataHelper.GetCellDataToStr(row, "IsPrint");
                    model.OrderIsHistory =Convert.ToInt32(DataHelper.GetCellDataToStr(row, "OrderIsHistory"));
                    model.abb = DataHelper.GetCellDataToStr(row, "abb");
                    model.resultljh = DataHelper.GetCellDataToStr(row, "resultljh");
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_CustomerOrder_3_CreateTime> AutoPrintSent_autoSendPrintedList()
        {
            List<mg_CustomerOrder_3_CreateTime> list = null;
            //离现在最远 未打印OrderIsPrint = 0 整个订单未打印 
            string sql = @"SELECT     TOP (1) CONVERT(int, CustomerOrder.OrderID) AS ID, CONVERT(int, p.ID) AS PartOrderID, CONVERT(nvarchar, CustomerOrder.OrderID) AS 订单号, 
                      CustomerOrder.SerialNumber AS 车身号, CONVERT(nvarchar, p.OrderIsPrint) AS IsPrint, CONVERT(nvarchar, CustomerOrder.CreateTime) AS CreateTime
FROM         dbo.mg_CustomerOrder_3 AS CustomerOrder INNER JOIN
                      dbo.mg_Customer_Product AS Customer_Product ON CustomerOrder.OrderID = Customer_Product.CustomerOrderID INNER JOIN
                      dbo.mg_PartOrder AS p ON Customer_Product.ID = p.CustomerProductID
WHERE     (CustomerOrder.OrderType = 1 OR
                      CustomerOrder.OrderType = 2) AND (p.OrderIsPrint = '0' or p.OrderIsPrint is null)
ORDER BY CustomerOrder.CreateTime ";//DESC

            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<mg_CustomerOrder_3_CreateTime>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_CustomerOrder_3_CreateTime model = new mg_CustomerOrder_3_CreateTime();

                    model.id = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "id"));
                    model.PartOrderID = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "PartOrderID"));
                    model.订单号 = DataHelper.GetCellDataToStr(row, "订单号");
                    model.车身号 = DataHelper.GetCellDataToStr(row, "车身号");
                    model.IsPrint = DataHelper.GetCellDataToStr(row, "IsPrint");
                    model.CreateTime = DataHelper.GetCellDataToStr(row, "CreateTime");                  
                    list.Add(model);
                }
            }
            return list;
        }
    }
}
