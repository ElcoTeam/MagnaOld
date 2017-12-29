using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Bll;
namespace website
{
    /// <summary>
    /// TransportHistory 的摘要说明
    /// </summary>
    public class TransportHistory : IHttpHandler
    {
        public static string sort = "-1";
        public static string order = "-1";
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string OrderCode = request["OrderCode"];
            string CarType = request["CarType"];
            string Worker = request["Worker"];
            string TransportType = request["TransportType"];

            int SortFlag = Convert.ToInt32(request["SortFlag"]);
            int PageSize = Convert.ToInt32(request["rows"]);
            int PageIndex = Convert.ToInt32(request["page"]);
            string method = request["method"];
            sort = request["sort"];
            order = request["order"];
            string where="";
            if (!string.IsNullOrEmpty(OrderCode))
            {
               
                where += " and  订单编码 like '%" + OrderCode + "%' ";
            }

            if (!string.IsNullOrEmpty(CarType))
            {
               where +=" and 车型ID = '" + CarType + "' ";
            }
       
            if (!string.IsNullOrEmpty(Worker))
            {

               where += " and ( op_name = '" + Worker + "' or Expr1 = '" + Worker + "' or Expr2 = '" + Worker + "'or Expr3 = '" + Worker + "' or Expr4 = '" + Worker + "')";
            }
           
            if (!string.IsNullOrEmpty(TransportType))
            {
               where +=" and 订单类型 = '" + TransportType + "' ";
            }
           
            if (string.IsNullOrWhiteSpace(method))
            {
                sort = request["sort"];
                order = request["order"];
                int totalcount = 0;
                int StartIndex = PageSize * (PageIndex - 1) + 1;
                int EndIndex = StartIndex + PageSize - 1;
                DataTable resTable = null;
                resTable = TransportHistory_BLL.getTable(PageSize, PageIndex, StartIndex, EndIndex, sort, order, where, out totalcount);
                
                string JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);
                context.Response.ContentType = "text/plain";
               
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            else
            {
                //导出excel
               
                int totalcount = 0;
                DataTable resTable2 = TransportHistory_BLL.getTableExcel(sort, order, where, out totalcount);
                string JsonStr = "[]";
                try
                {
                    ExcelHelper.ExportDTtoExcel(resTable2, "HeaderText", HttpContext.Current.Request.MapPath("~/App_Data/发运历史报表.xlsx"));
                    JsonStr = "true";
                }
                catch
                {
                    JsonStr = "false";
                }
                context.Response.ContentType = "jsons";

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
    }
}