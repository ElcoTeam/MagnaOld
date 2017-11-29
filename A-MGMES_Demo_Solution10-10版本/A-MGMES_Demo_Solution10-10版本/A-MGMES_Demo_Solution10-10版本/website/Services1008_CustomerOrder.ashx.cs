using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace website
{
    /// <summary>
    /// Service1008_CustomerOrder 的摘要说明
    /// </summary>
    public class Services1008_CustomerOrder : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string OrderType = request["OrderType"];

            int SortFlag = Convert.ToInt32(request["SortFlag"]);
            int PageSize = Convert.ToInt32(request["rows"]);
            int PageIndex = Convert.ToInt32(request["page"]);
            string method = request["method"];
            if (string.IsNullOrEmpty(OrderType))
            {
                OrderType = " 1=1 ";
            }
            else
            {
                OrderType = " OrderType = " + OrderType + " ";
            }
            string sql = "select a.OrderID,a.CustomerNumber,a.JITCallNumber,a.SerialNumber,a.SerialNumber_MES,a.VinNumber,a.PlanDeliverTime,a.CreateTime,case when a.OrderType = 1 then 'DelJit订单' when a.OrderType = 2 then 'SAP订单' else '紧急插单' end as OrderType,case when a.OrderState = 1 then '未拆分' when a.OrderState = 2 then '未下发' when a.OrderState = 3 then '已下发' when a.OrderState = 4 then '生产中' else '已完成' end as OrderState,LEFT(c.ProductName,CHARINDEX('-',ProductName)-1) as ProductName from mg_CustomerOrder_3 a left join mg_Customer_Product b on b.CustomerOrderID = a.OrderID left join mg_Product c on c.ID = b.ProductID where c.ProductType = 1 and " + OrderType + " order by a.OrderID";
            FunSql.Init();
            DataTable resTable = FunSql.GetTable(sql);
            string JsonStr ="";
            if(string.IsNullOrWhiteSpace(method))
            {
                DataTable resTable1 = GetPagedTable(resTable, PageIndex, PageSize);

                int totalcount = FunSql.GetInt("select count(0) from mg_CustomerOrder_3 a left join mg_Customer_Product b on b.CustomerOrderID = a.OrderID left join mg_Product c on c.ID = b.ProductID where c.ProductType = 1 and  " + OrderType + "");

                JsonStr= FunCommon.DataTableToJson2(totalcount, resTable1);
                //导出
                //ExcelHelper.ExportDTtoExcel(resTable, "", HttpContext.Current.Request.MapPath("~/App_Data/客户订单报表.xlsx"));
                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            if("Export"==method)
            {
               try
               {
                   ExcelHelper.ExportDTtoExcel(resTable, "", HttpContext.Current.Request.MapPath("~/App_Data/客户订单报表.xlsx"));
                   JsonStr="true";
               }
                catch
               {
                    JsonStr="false";
                }
                context.Response.ContentType = "json";
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)//PageIndex表示第几页，PageSize表示每页的记录数
        {
            if (PageIndex == 0)
                return dt;//0页代表每页数据，直接返回

            DataTable newdt = dt.Copy();
            newdt.Clear();//copy dt的框架

            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;//源数据记录数小于等于要显示的记录，直接返回dt

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }
    }
}