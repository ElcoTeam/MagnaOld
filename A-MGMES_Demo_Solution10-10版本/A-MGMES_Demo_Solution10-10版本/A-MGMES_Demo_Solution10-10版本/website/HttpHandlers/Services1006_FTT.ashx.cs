using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;
using System.Reflection;
using Bll;
namespace website.HttpHandlers
{
    /// <summary>
    /// Services1000_SysLog 的摘要说明
    /// </summary>
    public class Services1006_FTT : IHttpHandler
    {
        public static string sort = "-1";
        public static string order = "-1";

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string StartTime = request["start_time"];
            string EndTime = request["end_time"];
            string clnameid = request["clnameid"];
            int PageSize = Convert.ToInt32(request["rows"]);
            int PageIndex = Convert.ToInt32(request["page"]);
            string SortFlag = request["sort"];
            string sortOrder = request["order"];
            string where = "";
            if(!string.IsNullOrEmpty(StartTime))
            {
                where += " and cl_starttime>='" + StartTime + "'";
            }
            if(!string.IsNullOrEmpty(EndTime))
            {
                where += " and cl_endtime<='" + EndTime + "'";
            }
            if(!string.IsNullOrEmpty(clnameid))
            {
                where += " and cl_name like'%" + clnameid + "%'"; 
            }
            int totalcount;
            DataTable resTable = new DataTable();
            sort = SortFlag;
            order = sortOrder;
            int StartIndex = PageSize * (PageIndex - 1) + 1;
            int EndIndex = StartIndex + PageSize - 1;


            resTable = FTT_BLL.getTable(PageSize,PageIndex, StartIndex, EndIndex, sort, order, where, out totalcount);
                string JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);

                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);
                context.Response.End();
            
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