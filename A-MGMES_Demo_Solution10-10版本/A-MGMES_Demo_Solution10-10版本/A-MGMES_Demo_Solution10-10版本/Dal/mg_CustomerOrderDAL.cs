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
    public class mg_CustomerOrderDAL
    {

        public static DataTable getTable(int Pagesize, int pageIndex, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr, out int total)
        {
            if(string.IsNullOrEmpty(SortFlag))
            {
                SortFlag = "OrderID";
            }
            if(string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }
            StringBuilder commandText = new StringBuilder();
            commandText.Append("select a.OrderID,a.CustomerNumber,a.JITCallNumber,a.SerialNumber,a.SerialNumber_MES,a.VinNumber,a.PlanDeliverTime,a.CreateTime,case when a.OrderType = 1 then 'DelJit订单' when a.OrderType = 2 then 'SAP订单' else '紧急插单' end as OrderType,case when a.OrderState = 1 then '未拆分' when a.OrderState = 2 then '未下发' when a.OrderState = 3 then '已下发' when a.OrderState = 4 then '生产中' else '已完成' end as OrderState,LEFT(c.ProductName,CHARINDEX('-',ProductName)-1) as ProductName,a.OrderIsHistory from mg_CustomerOrder_3 a left join mg_Customer_Product b on b.CustomerOrderID = a.OrderID left join mg_Product c on c.ID = b.ProductID where c.ProductType = 1  ");
            commandText.Append(wherestr);//这里修改条件语句
            commandText.Append(" order by a."+SortFlag+" "+ sortOrder);
            string query_sql = commandText.ToString();
            StringBuilder commandText1 = new StringBuilder();
            commandText1.Append("select a.OrderID,a.CustomerNumber,a.JITCallNumber,a.SerialNumber,a.SerialNumber_MES,a.VinNumber,a.PlanDeliverTime,a.CreateTime,case when a.OrderType = 1 then 'DelJit订单' when a.OrderType = 2 then 'SAP订单' else '紧急插单' end as OrderType,case when a.OrderState = 1 then '未拆分' when a.OrderState = 2 then '未下发' when a.OrderState = 3 then '已下发' when a.OrderState = 4 then '生产中' else '已完成' end as OrderState,LEFT(c.ProductName,CHARINDEX('-',ProductName)-1) as ProductName,a.OrderIsHistory from mg_CustomerOrder_3 a left join mg_Customer_Product b on b.CustomerOrderID = a.OrderID left join mg_Product c on c.ID = b.ProductID where c.ProductType = 1  ");
            commandText1.Append(wherestr);//这里修改条件语句
            string count_sql = "select count(*) as total from  (" + commandText1.ToString() + " ) result ";
           
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


        public static DataTable getTableExcel(string SortFlag, string sortOrder, string wherestr, out int total)
        {
            if (string.IsNullOrEmpty(SortFlag))
            {
                SortFlag = "OrderID";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }
            StringBuilder commandText = new StringBuilder();
            commandText.Append("select a.OrderID,a.CustomerNumber,a.JITCallNumber,a.SerialNumber,a.SerialNumber_MES,a.VinNumber,a.PlanDeliverTime,a.CreateTime,case when a.OrderType = 1 then 'DelJit订单' when a.OrderType = 2 then 'SAP订单' else '紧急插单' end as OrderType,case when a.OrderState = 1 then '未拆分' when a.OrderState = 2 then '未下发' when a.OrderState = 3 then '已下发' when a.OrderState = 4 then '生产中' else '已完成' end as OrderState,LEFT(c.ProductName,CHARINDEX('-',ProductName)-1) as ProductName from mg_CustomerOrder_3 a left join mg_Customer_Product b on b.CustomerOrderID = a.OrderID left join mg_Product c on c.ID = b.ProductID where c.ProductType = 1  ");
            commandText.Append(wherestr);//这里修改条件语句
            commandText.Append(" order by a." + SortFlag + " " + sortOrder);
            string query_sql = commandText.ToString();
            StringBuilder commandText1 = new StringBuilder();
            commandText1.Append("select a.OrderID,a.CustomerNumber,a.JITCallNumber,a.SerialNumber,a.SerialNumber_MES,a.VinNumber,a.PlanDeliverTime,a.CreateTime,case when a.OrderType = 1 then 'DelJit订单' when a.OrderType = 2 then 'SAP订单' else '紧急插单' end as OrderType,case when a.OrderState = 1 then '未拆分' when a.OrderState = 2 then '未下发' when a.OrderState = 3 then '已下发' when a.OrderState = 4 then '生产中' else '已完成' end as OrderState,LEFT(c.ProductName,CHARINDEX('-',ProductName)-1) as ProductName from mg_CustomerOrder_3 a left join mg_Customer_Product b on b.CustomerOrderID = a.OrderID left join mg_Product c on c.ID = b.ProductID where c.ProductType = 1  ");
            commandText1.Append(wherestr);//这里修改条件语句
            string count_sql = "select count(*) as total from  (" + commandText1.ToString() + " ) result ";

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

        public static int AddOrder(string co_no, int all_id, int co_count, string co_customer)
        {
            string sql = @"INSERT INTO [mg_CustomerOrder] ([co_no],[all_id],[co_count],[co_customer]) VALUES ('" + co_no + "'," + all_id + "," + co_count + ",'" + co_customer + @"')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetAllData(string strstate)
        {
            string sql = @"SELECT dbo.mg_CustomerOrder.co_id, dbo.mg_CustomerOrder.co_no, dbo.mg_allpart.all_no,  dbo.mg_CustomerOrder.co_count, 
                dbo.mg_CustomerOrder.co_customer,  CASE co_state WHEN 0 THEN '未下达' WHEN 1 THEN '已下达' END AS state FROM  dbo.mg_allpart INNER JOIN
                dbo.mg_CustomerOrder ON dbo.mg_allpart.all_id = dbo.mg_CustomerOrder.all_id" + strstate + " order by [co_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int UpDateOrder(int coid, string co_no, int allid, int co_count, string co_customer)
        {
            string sql = @"update [mg_CustomerOrder] set [co_no]='" + co_no + "',[all_id]=" + allid + ",[co_count]=" + co_count + ",[co_customer]= '" + co_customer + "' where co_id=" + coid;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static int CheckOrder(int a, int co_id, string co_no)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"select [co_no] from [mg_CustomerOrder] where co_no='" + co_no + "'";
            }
            if (a == 2)
            {
                sql = @"select [co_no] from [mg_CustomerOrder] where [co_no]='" + co_no + "' and [co_id] <> " + co_id;
            }
            tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            if (tb.Rows.Count != 0)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
            tb.Dispose();
            return i;
        }

        public static int DelOrder(int coid)
        {
            string sql = @"delete from [mg_CustomerOrder] where [co_id]=" + coid;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int AddCutomerOrderWithoutCutting(mg_CustomerOrderModel model)
        {
            string maxid = @"declare @i int;declare @o int;
                                SELECT @i=max([co_id])  FROM [mg_CustomerOrder];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                SELECT @o=max([co_order])  FROM [mg_CustomerOrder];
                                if @o is null
                                    begin
                                        set @o=1
                                    end
                                else
                                    begin
                                        set @o=@o+1
                                    end;
                                ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into mg_CustomerOrder(");
            strSql.Append("co_id,co_no,co_cutomerid,co_state,co_order,co_isCutted,co_createtime)");
            strSql.Append(" values (");
            strSql.Append("@i,@co_no,@co_cutomerid,0,@o,@co_isCutted,@co_createtime);");
            SqlParameter[] parameters = {
					new SqlParameter("@co_no", SqlDbType.NVarChar),
					new SqlParameter("@co_cutomerid", SqlDbType.Int),
					new SqlParameter("@co_isCutted", SqlDbType.Int),
					new SqlParameter("@co_createtime", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.co_no;
            parameters[1].Value = model.co_cutomerid;
            parameters[2].Value = model.co_isCutted;
            parameters[3].Value = DateTime.Now;

            if (!string.IsNullOrEmpty(model.all_ids))   //订单：点击保存时候触发的
            {
                string[] all_idArr = model.all_ids.Split('|');
                string[] co_countArr = model.co_counts.Split('|');
                string odsStr = @"INSERT INTO [mg_cusOrder_Allpart_rel]
                                                   ([co_id]
                                                   ,[all_id]
                                                   ,[co_count]
                                                    ,orderno)
                                             VALUES
                                                   ($co_id$
                                                   ,$all_id$
                                                   ,$co_count$
                                                    ,$orderno$);";
                for (int i = 0; i < all_idArr.Length; i++)
                {
                    string key = "";
                    if ((i + 1) <= co_countArr.Length)
                    {
                        key = co_countArr[i];
                    }
                    strSql.Append(odsStr.Replace("$all_id$", all_idArr[i]).Replace("$co_id$", "@i").Replace("$co_count$", key)).Replace("$orderno$", (i + 1).ToString());
                }
            }

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql, parameters);

            return rows;
           // return 0;
        }



        public static List<mg_CustomerOrderModel> QueryListForPaging(string page, string pagesize, out string total, string isCutted)
        {
            total = "0";
            List<mg_CustomerOrderModel> list = null;
            string queryStr = " where co_isCutted=" + isCutted;
            string sql1 = @"select count([co_id]) total from [mg_CustomerOrder] cus " + queryStr + @" ;";
            string sql2 = @" 
                                with allPart as 
                                (
	                                SELECT [all_id]
		                                  ,[all_no]
		                                  ,[all_rateid]
		                                  ,r.prop_name [all_ratename]
		                                  ,[all_colorid]
		                                  ,c.prop_name [all_colorname]
		                                  ,[all_metaid]
		                                  ,m.prop_name [all_metaname]
	                                  FROM [mg_allpart] a
	                                  left join mg_Property c on a.all_colorid = c.prop_id
	                                  left join mg_Property r on a.all_rateid = r.prop_id
	                                  left join mg_Property m on a.all_metaid = m.prop_id
                                )
                                SELECT top " + pagesize + @" cus.[co_id]
                                      ,cus.[co_no]
                                    , stuff((select '|' +CAST(all_id as varchar(10))  from mg_cusOrder_Allpart_rel where co_id = cus.co_id order by orderno for xml path('')),1,1,'')as all_ids
                                    , stuff((select '|' +CAST(co_count as varchar(10))  from mg_cusOrder_Allpart_rel where co_id = cus.co_id order by orderno for xml path('')),1,1,'')as co_counts
                                   , stuff((select '、' + (all_no+' ('+ap.all_ratename+'+'+ap.all_metaname+'+'+ap.all_colorname+') '+CAST(co_count as varchar(10))+' 套')  from allPart ap inner join  mg_cusOrder_Allpart_rel ca on ap.all_id=ca.all_id and co_id = cus.co_id order by ca.orderno for xml path('')),1,1,'')as appPartdesc
       , stuff((select '|' +CAST(ap.all_id as varchar(10))+','+CAST(co_count as varchar(10))+','+all_no  from mg_cusOrder_Allpart_rel car left join allPart ap on ap.all_id=car.all_id where co_id = cus.co_id order by car.orderno for xml path('')),1,1,'')as idcounts

                                      ,[co_cutomerid]
                                      ,p.prop_name [co_customer]
                                      ,[co_state]
                                      ,[co_order]
                                      ,[co_isCutted]
                                  FROM [mg_CustomerOrder] cus
                                  left join mg_Property p on cus.co_cutomerid = p.prop_id
                                  " + queryStr + @"

                                  and  cus.co_order < (
                                                select top 1 co_order from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") co_order from  [mg_CustomerOrder] " + queryStr + @"  order by co_order desc )t
                                                order by  co_order  )

                                  order by cus.co_order desc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_CustomerOrderModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_CustomerOrderModel model = new mg_CustomerOrderModel();

                    model.co_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "co_id"));
                    model.co_no = DataHelper.GetCellDataToStr(row, "co_no");
                    model.co_isCutted = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "co_isCutted"));
                    model.co_cutomerid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "co_cutomerid"));
                    model.all_ids = DataHelper.GetCellDataToStr(row, "all_ids");
                    model.co_counts = DataHelper.GetCellDataToStr(row, "co_counts");
                    model.appPartdesc = DataHelper.GetCellDataToStr(row, "appPartdesc");
                    model.idcounts = DataHelper.GetCellDataToStr(row, "idcounts");
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_CustomerOrderModel> QueryListForFirstPage(string pagesize, out string total, string isCutted)
        {
            total = "0";
            List<mg_CustomerOrderModel> list = null;
            string queryStr = " where co_isCutted=" + isCutted;
            string sql1 = @"select count([co_id]) total from [mg_CustomerOrder] cus " + queryStr + @" ;";
            string sql2 = @" 
                                with allPart as 
                                (
	                                SELECT [all_id]
		                                  ,[all_no]
		                                  ,[all_rateid]
		                                  ,r.prop_name [all_ratename]
		                                  ,[all_colorid]
		                                  ,c.prop_name [all_colorname]
		                                  ,[all_metaid]
		                                  ,m.prop_name [all_metaname]
	                                  FROM [mg_allpart] a
	                                  left join mg_Property c on a.all_colorid = c.prop_id
	                                  left join mg_Property r on a.all_rateid = r.prop_id
	                                  left join mg_Property m on a.all_metaid = m.prop_id
                                )
                                SELECT top " + pagesize + @"  cus.[co_id]
                                      ,cus.[co_no]
                                    , stuff((select '|' +CAST(all_id as varchar(10))  from mg_cusOrder_Allpart_rel where co_id = cus.co_id order by orderno for xml path('')),1,1,'')as all_ids
                                    , stuff((select '|' +CAST(co_count as varchar(10))  from mg_cusOrder_Allpart_rel where co_id = cus.co_id order by orderno for xml path('')),1,1,'')as co_counts
                                   , stuff((select '、' + (all_no+' ('+ap.all_ratename+'+'+ap.all_metaname+'+'+ap.all_colorname+') '+CAST(co_count as varchar(10))+' 套')  from allPart ap inner join  mg_cusOrder_Allpart_rel ca on ap.all_id=ca.all_id and co_id = cus.co_id order by ca.orderno for xml path('')),1,1,'')as appPartdesc
       , stuff((select '|' +CAST(ap.all_id as varchar(10))+','+CAST(co_count as varchar(10))+','+all_no  from mg_cusOrder_Allpart_rel car left join allPart ap on ap.all_id=car.all_id where co_id = cus.co_id order by car.orderno for xml path('')),1,1,'')as idcounts

                                      ,[co_cutomerid]
                                      ,p.prop_name [co_customer]
                                      ,[co_state]
                                      ,[co_order]
                                      ,[co_isCutted]
                                  FROM [mg_CustomerOrder] cus
                                  left join mg_Property p on cus.co_cutomerid = p.prop_id
                                  " + queryStr + @"
                                  order by cus.co_order desc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_CustomerOrderModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_CustomerOrderModel model = new mg_CustomerOrderModel();

                    model.co_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "co_id"));
                    model.co_no = DataHelper.GetCellDataToStr(row, "co_no");
                    model.co_isCutted = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "co_isCutted"));
                    model.co_cutomerid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "co_cutomerid"));
                    model.all_ids = DataHelper.GetCellDataToStr(row, "all_ids");
                    model.co_counts = DataHelper.GetCellDataToStr(row, "co_counts");
                    model.appPartdesc = DataHelper.GetCellDataToStr(row, "appPartdesc");
                    model.idcounts = DataHelper.GetCellDataToStr(row, "idcounts");
                    list.Add(model);
                }
            }
            return list;
        }

        public static int UpdateCutomerOrderWithoutCutting(mg_CustomerOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_CustomerOrder set ");
            strSql.Append("co_no=@co_no,");
            strSql.Append("co_cutomerid=@co_cutomerid ");
            strSql.Append(" where co_id=@co_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@co_id", SqlDbType.Decimal),
					new SqlParameter("@co_no", SqlDbType.NVarChar),
					new SqlParameter("@co_cutomerid", SqlDbType.Int)};
            parameters[0].Value = model.co_id;
            parameters[1].Value = model.co_no;
            parameters[2].Value = model.co_cutomerid;

            strSql.Append(" delete from mg_cusOrder_Allpart_rel where co_id=" + model.co_id + "; ");

            if (!string.IsNullOrEmpty(model.all_ids))
            {
                string[] all_idArr = model.all_ids.Split('|');
                string[] co_countArr = model.co_counts.Split('|');
                string odsStr = @"INSERT INTO [mg_cusOrder_Allpart_rel]
                                                   ([co_id]
                                                   ,[all_id]
                                                   ,[co_count],orderno)
                                             VALUES
                                                   ($co_id$
                                                   ,$all_id$
                                                   ,$co_count$,$orderno$);";
                for (int i = 0; i < all_idArr.Length; i++)
                {
                    string key = "";
                    if ((i + 1) <= co_countArr.Length)
                    {
                        key = co_countArr[i];
                    }
                    strSql.Append(odsStr.Replace("$all_id$", all_idArr[i]).Replace("$co_id$", model.co_id.ToString()).Replace("$co_count$", key).Replace("$orderno$", (i + 1).ToString()));
                }
            }

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);

            return rows;
        }

        public static int DeleteCustomerOrder(string co_id)
        {
            string sql = @"delete from [mg_CustomerOrder] where [co_id]=" + co_id + "; delete from mg_cusOrder_Allpart_rel where co_id=" + co_id + ";";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int AddCutomerOrderWithCutting(mg_CustomerOrderModel model)
        {
            throw new NotImplementedException();
        }

        public static DataTable GetCustomerOrderDetail(string co_id)
        {
            string sql = @"SELECT co.[co_id]
                                  ,[co_no]
	                              ,corel.all_id
	                              ,corel.co_count
	                              ,p.part_no
	                              ,p.part_name
	                              ,p.part_categoryid
                              FROM [mg_CustomerOrder] co
                              left join mg_cusOrder_Allpart_rel corel on co.co_id = corel.co_id
                              left join mg_allpart_part_rel aprel on corel.all_id = aprel.all_id
                              left join mg_part p  on aprel.partid_id = p.part_id
                              where co_isCutted = 0 and co.co_id=" + co_id + @"
                            order by co_order,corel.orderno;
                            ";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static bool CuttingOrder(DataTable orderDT, DataTable dt2)   //点击拆单时候触发的
        {

            StringBuilder sbStr = new StringBuilder();
            string co_id = DataHelper.GetCellDataToStr(orderDT.Rows[0], "co_id");
            string orderSql = @" UPDATE [mg_CustomerOrder]   SET [co_isCutted] =1  WHERE co_id =" + co_id + ";";
            sbStr.Append(orderSql);

            sbStr.Append(@"DELETE FROM mg_Order_FSD  WHERE co_id =" + co_id + ";");
            sbStr.Append(@"DELETE FROM mg_Order_FSDB  WHERE co_id =" + co_id + ";");
            sbStr.Append(@"DELETE FROM mg_Order_FSDC  WHERE co_id =" + co_id + ";");
            sbStr.Append(@"DELETE FROM mg_Order_FSP  WHERE co_id =" + co_id + ";");
            sbStr.Append(@"DELETE FROM mg_Order_FSPB  WHERE co_id =" + co_id + ";");
            sbStr.Append(@"DELETE FROM mg_Order_FSPC  WHERE co_id =" + co_id + ";");
            sbStr.Append(@"DELETE FROM mg_Order_RSB40  WHERE co_id =" + co_id + ";");
            sbStr.Append(@"DELETE FROM mg_Order_RSB60  WHERE co_id =" + co_id + ";");
            sbStr.Append(@"DELETE FROM mg_Order_RSC  WHERE co_id =" + co_id + ";");

            //sbStr.Append(@"insert into a(A,B,C) select '','',c from  B;");

            sbStr.Append(AssemblingOrderSqlStr(dt2, orderDT, (int)mg_XLSEnum.FS_Drive, "mg_Order_FSD", "ofsd_", "FS"));
            sbStr.Append(AssemblingOrderSqlStr(dt2, orderDT, (int)mg_XLSEnum.FSB_Drive, "mg_Order_FSDB", "ofsdb_", "FB"));
            sbStr.Append(AssemblingOrderSqlStr(dt2, orderDT, (int)mg_XLSEnum.FSC_Drive, "mg_Order_FSDC", "ofsdc_", "FC"));
            sbStr.Append(AssemblingOrderSqlStr(dt2, orderDT, (int)mg_XLSEnum.FS_Passenger, "mg_Order_FSP", "ofsp_", "FS"));
            sbStr.Append(AssemblingOrderSqlStr(dt2, orderDT, (int)mg_XLSEnum.FSB_Passenger, "mg_Order_FSPB", "ofspb_", "FB"));
            sbStr.Append(AssemblingOrderSqlStr(dt2, orderDT, (int)mg_XLSEnum.FSC_Passenger, "mg_Order_FSPC", "ofspc_", "FC"));
            sbStr.Append(AssemblingOrderSqlStr(dt2, orderDT, (int)mg_XLSEnum.RSB40, "mg_Order_RSB40", "orsb40_", "B4"));
            sbStr.Append(AssemblingOrderSqlStr(dt2, orderDT, (int)mg_XLSEnum.RSB60, "mg_Order_RSB60", "orsb60_", "B6"));
            sbStr.Append(AssemblingOrderSqlStr(dt2, orderDT, (int)mg_XLSEnum.RSC, "mg_Order_RSC", "orsc_", "RC"));

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sbStr.ToString(), null) != -1 ? true : false;




        }

        /// <summary>
        /// 根据工位查询该工位每天的生产情况、根据订单号查询该订单每个工位的生产信息
        /// </summary>
        /// <param name="co_id"></param>
        public static void GenerateTempInfo(string co_id)
        {
            string commandText = @"
--FSD FSP
insert into mg_Order_FS_Temp (TypeIdentifier,TempOfspb_pre,Order_FS_ID,TempCo_id,TempPart_no,TempOfspb_cdstr,TempOfspb_createdate,SortNumber)
select 1 as type_identify,ofsd_pre,ofsd_id,co_id,part_no,ofsd_cdstr,ofsd_createdate,row_number() over (order by ofsd_id,co_id,ofsd_cdstr) from mg_Order_FSD where co_id=@co_id
union all
select 2 as type_identify,ofsp_pre,ofsp_id,co_id,part_no,ofsp_cdstr,ofsp_createdate,row_number() over (order by ofsp_id,co_id,ofsp_cdstr) from mg_Order_FSP where co_id=@co_id
order by ofsd_id,co_id,ofsd_cdstr,type_identify;
-- FSDB FSPB
insert into mg_Order_FSB_Temp (TypeIdentifier,TempOfspb_pre,Order_FS_ID,TempCo_id,TempPart_no,TempOfspb_cdstr,TempOfspb_createdate,SortNumber)
select 1 as type_identify,ofsdb_pre,ofsdb_id,co_id,part_no,ofsdb_cdstr,ofsdb_createdate,row_number() over (order by ofsdb_id,co_id,ofsdb_cdstr) from mg_Order_FSDB where co_id=@co_id
union all
select 2 as type_identify,ofspb_pre,ofspb_id,co_id,part_no,ofspb_cdstr,ofspb_createdate,row_number() over (order by ofspb_id,co_id,ofspb_cdstr) from mg_Order_FSPB where co_id=@co_id
order by ofsdb_id,co_id,ofsdb_cdstr,type_identify;
-- FSDC FSPC
insert into mg_Order_FSC_Temp (TypeIdentifier,TempOfspb_pre,Order_FS_ID,TempCo_id,TempPart_no,TempOfspb_cdstr,TempOfspb_createdate,SortNumber)
select 1 as type_identify,ofsdc_pre,ofsdc_id,co_id,part_no,ofsdc_cdstr,ofsdc_createdate,row_number() over (order by ofsdc_id,co_id,ofsdc_cdstr) from mg_Order_FSDC where co_id=@co_id
union all
select 2 as type_identify,ofspc_pre,ofspc_id,co_id,part_no,ofspc_cdstr,ofspc_createdate,row_number() over (order by ofspc_id,co_id,ofspc_cdstr) from mg_Order_FSPC where co_id=@co_id
order by ofsdc_id,co_id,ofsdc_cdstr,type_identify;
";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@co_id", co_id)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, commandText, parameters);
        }

        public static string AssemblingOrderSqlStr(DataTable dt2, DataTable orderDT, int XLSType, string tableName, string columnIndexStr, string pre)
        {
            //string fsdSql1 = @"ISNULL((select max($columnIndexStr$id) from [$tableName$] where co_id=$co_id$ and $columnIndexStr$createdate between '$date$ 00:00:00' and '$date$ 23:59:59'),0)+$index$";
            string fsdSql1 = @"ISNULL((select max($columnIndexStr$id) from [$tableName$] where  $columnIndexStr$createdate between '$date$ 00:00:00' and '$date$ 23:59:59'),0)+$index$";
            string fsdSql2 = @"insert into $tableName$($columnIndexStr$id,co_id,part_no,$columnIndexStr$createdate,$columnIndexStr$cdstr,$columnIndexStr$unproducing,$columnIndexStr$producing,$columnIndexStr$finish,$columnIndexStr$pre)values";
            string fsdSql3 = @"($ofsd_id$,$co_id$,'$part_no$','$ofsd_createdate$','$ofsd_cdstr$',1,0,0,'$pre$');";

            StringBuilder sqlStr = new StringBuilder();

            //几种车型配置
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                DataRow dr = dt2.Rows[i];
                string all_id = DataHelper.GetCellDataToStr(dr, "all_id");
                int co_count = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dr, "co_count"));

                //FSD
                DataRow rowsFSD = orderDT.Select(" all_id=" + all_id + " and co_count=" + co_count + " and part_categoryid=" + XLSType)[0];
                for (int j = 0; j < co_count; j++)
                {
                    string co_id = DataHelper.GetCellDataToStr(rowsFSD, "co_id");
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string fsdSql1_t = fsdSql1.Replace("$date$", currentDate).Replace("$index$", "1").Replace("$co_id$", co_id).Replace("$tableName$", tableName).Replace("$columnIndexStr$", columnIndexStr);

                    string fsdSql2_t = fsdSql2.Replace("$tableName$", tableName).Replace("$columnIndexStr$", columnIndexStr);

                    string part_no = DataHelper.GetCellDataToStr(rowsFSD, "part_no").Trim();
                    string currentDateTime = DateTime.Now.ToString();
                    string currentDateShort = DateTime.Now.ToString("yyyyMMdd").Substring(2);
                    string fsdSql3_t = fsdSql3.Replace("$ofsd_id$", fsdSql1_t).Replace("$co_id$", co_id.ToString()).Replace("$part_no$", part_no).Replace("$ofsd_createdate$", currentDateTime).Replace("$ofsd_cdstr$", currentDateShort).Replace("$pre$", pre);

                    sqlStr.Append(fsdSql2_t);
                    sqlStr.Append(fsdSql3_t);
                }

            }



            string s = sqlStr.ToString();

            return s;
        }




    }
}
