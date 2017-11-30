using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;

using System.Runtime.Serialization.Formatters;

using System.Runtime.Serialization.Formatters.Binary;
using Bll;
using Model;
namespace website
{
    /// <summary>
    /// Services1000_SysLog 的摘要说明
    /// </summary>
    public class Services1000_SysLog : IHttpHandler
    {
        HttpRequest Request = null;
        HttpResponse Response = null;
        public static string sort = "-1";
        public static string order = "-1";
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string method = request.Params["method"];
            string AssemblyLine = request.Params["fl_name"];    //流水线
            string Station = request.Params["st_no"];   //工位
            string StartTime = request["StartTime"];
            string EndTime = request["EndTime"];
            string OrderId = request["OrderId"];

            string SortFlag = request["sort"];
            string sortOrder = request["order"];
            int PageSize = Convert.ToInt32(request["rows"]);
            int PageIndex = Convert.ToInt32(request["page"]);
            //测试数据
            //StartTime = "2017-08-01 16:51";
            //EndTime = "2017-08-03 16:51";
            int totalcount;
            DataTable resTable = new DataTable();
            //ygy 
            string wherestr = " ";
            string JsonStr="";
           
            if (!string.IsNullOrWhiteSpace(AssemblyLine))
            {
                wherestr += "  and fl_name ='" + AssemblyLine + "'";
            }
            if (!string.IsNullOrWhiteSpace(Station))
            {
                wherestr += "  and st_no='" + Station + "'";
            }
            if (!string.IsNullOrWhiteSpace(OrderId))
            {
                wherestr += " and or_no = '" + OrderId + @"'";
            }

            if (!string.IsNullOrWhiteSpace(StartTime))
            {
                wherestr += " and cast(step_startTime as datetime) >= '" + StartTime + "' ";
            }
            if (!string.IsNullOrWhiteSpace(EndTime))
            {
                wherestr += " and cast(step_endTime as datetime) <= '" + EndTime + @"'";
            }
            //查询
            #region
            if (string.IsNullOrWhiteSpace(method))
            {
                sort = SortFlag;
                order = sortOrder;
                int StartIndex = PageSize * (PageIndex - 1) + 1;
                int EndIndex = StartIndex + PageSize - 1;
                //string   
                JsonStr = mg_sys_logBll.getList(PageSize, StartIndex, EndIndex, sort, order, wherestr);

                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);

            }
            #endregion
            //导出
            #region
            if (method == "Export")
            {
                if("-1"==sort)
                {
                    sort = "sys_id";
                }
                if("-1"==order)
                {
                    order = "asc";
                }
                string json = "";
                string fileName = HttpContext.Current.Request.MapPath("~/App_Data/步骤日志报表.xlsx");
                try
                {
                    int StartIndex = 1;
                    int EndIndex = -1;
                    resTable = mg_sys_logBll.getList(PageSize, StartIndex, EndIndex, sort, order, wherestr,out totalcount );
                    ExcelHelper.ExportDTtoExcel(resTable, "步骤日志报表", fileName);
                    string ss = "true";
                    json = "{\"Result\":\"" + ss + "\"}";
                   
                }
                catch(Exception e)
                {
                    string ss1 = "false";
                    json = "{\"Result\":\"" + ss1 + "\"}";
                 
                    
                   
                }


                context.Response.ContentType = "json";
                context.Response.Write(json);
            }
            #endregion


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