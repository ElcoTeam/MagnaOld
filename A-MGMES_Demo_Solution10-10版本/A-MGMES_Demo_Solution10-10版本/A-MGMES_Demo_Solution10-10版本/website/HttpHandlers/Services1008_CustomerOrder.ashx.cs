using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Bll;
namespace website.HttpHandlers
{
    /// <summary>
    /// Service1008_CustomerOrder 的摘要说明
    /// </summary>
    public class Services1008_CustomerOrder : IHttpHandler
    {
        public static string sort = "-1";
        public static string order = "-1";
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string OrderType = request["OrderType"];
            string SerialNumber = request["SerialNumber"];

            int SortFlag = Convert.ToInt32(request["SortFlag"]);
            int PageSize = Convert.ToInt32(request["rows"]);
            int PageIndex = Convert.ToInt32(request["page"]);
            string method = request["method"];
            sort = request["sort"];
            order = request["order"];
            string where = "";
            if (!string.IsNullOrEmpty(OrderType))
            {
                where += " and a.OrderType = " + OrderType + " ";
            }
           if (!string.IsNullOrEmpty(SerialNumber))
            {
                where += "  and a.SerialNumber like '%" + SerialNumber + "%' ";
            }
            
            string JsonStr ="";
            if(string.IsNullOrWhiteSpace(method))
            {
                int StartIndex = PageSize * (PageIndex - 1) + 1;
                int EndIndex = StartIndex + PageSize - 1;
                int totalcount;
                DataTable resTable = mg_CustomerOrderBLL.getTable(PageSize, PageIndex, StartIndex, EndIndex, sort, order, where, out totalcount);
                DataTable resTable1 = GetPagedTable(resTable, PageIndex, PageSize);

                JsonStr= FunCommon.DataTableToJson2(totalcount, resTable1);
                
                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            if("Export"==method)
            {
               try
               {
                   int totalcount;
                   DataTable resTable = mg_CustomerOrderBLL.getTableExcel(sort, order, where, out totalcount);
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