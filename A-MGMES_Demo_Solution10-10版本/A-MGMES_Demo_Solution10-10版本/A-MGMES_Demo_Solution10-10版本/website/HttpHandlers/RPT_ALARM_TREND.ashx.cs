using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Tools;
using Bll;
using DbUtility;
using Model;
namespace website.HttpHandlers
{
    /// <summary>
    /// RPT_ALARM_TREND 的摘要说明
    /// </summary>
    public class RPT_ALARM_TREND : IHttpHandler
    {

        JavaScriptSerializer jsc = new JavaScriptSerializer();

        public static string sort = "-1";
        public static string order = "-1";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            string method = context.Request["method"];
            switch (method)
            {
                case "GetListNew":
                    context.Response.Write(GetListNew(context));
                    break;
                case "GetList":
                    context.Response.Write(GetList(context));
                    break;
                case "Export":
                    Export(context);
                    break;
                default:
                    GetListNew(context);
                    break;

            }
        }

       
        public void Export(HttpContext context)
        {
            string start_time = context.Request["start_time"];
            string end_time = context.Request["end_time"];
            int PageSize = Convert.ToInt32(context.Request["rows"]);
            int PageIndex = Convert.ToInt32(context.Request["page"]);

            StringBuilder commandText = new StringBuilder();
            string where = "";

            if (string.IsNullOrEmpty(start_time))
            {
                DateTime t = DateTime.Now.AddMonths(-1);
                start_time = t.ToString("yyyy-MM-dd");
            }
            string StartTime = start_time.Substring(0, 10);
            if (string.IsNullOrEmpty(end_time))
            {
                DateTime t = DateTime.Now;
                end_time = t.ToString("yyyy-MM-dd");

            }
            string EndTime = end_time.Substring(0, 10);
            string sidx = RequstString("sidx");    //排序名称
            string sort = RequstString("sord");    //排序方式
            if ("-1" == sort)
            {
                sort = "id";
            }
            if ("-1" == order)
            {
                order = "asc";
            }
            string json = "";
            string fileName = HttpContext.Current.Request.MapPath("~/App_Data/生产线报警趋势报表.xlsx");
            try
            {
                int StartIndex = 1;
                int EndIndex = -1;
                int totalcount = 0;
                DataTable resTable = Production_AlarmTrendReport_BLL.getTable(StartTime, EndTime,PageSize, StartIndex, EndIndex, sort, order, where, out totalcount);
                ExcelHelper.ExportDTtoExcel(resTable, "生产线报警趋势报表", fileName);
                string ss = "true";
                json = "{\"Result\":\"" + ss + "\"}";

            }
            catch (Exception e)
            {
                string ss1 = "false";
                json = "{\"Result\":\"" + ss1 + "\"}";



            }


            context.Response.ContentType = "json";
            context.Response.Write(json);
        }
       
        public string GetList(HttpContext context)
        {
            string start_time = context.Request["start_time"];
            string end_time = context.Request["end_time"];
            int PageSize = Convert.ToInt32(context.Request["rows"]);
            int PageIndex = Convert.ToInt32(context.Request["page"]);

            StringBuilder commandText = new StringBuilder();
            string where = "";

            if (string.IsNullOrEmpty(start_time))
            {
                DateTime t = DateTime.Now.AddMonths(-1);
                start_time = t.ToString("yyyy-MM-dd hh:mm:ss");
            }
            string StartTime = start_time.Substring(0, 10) + " 00:00:00";
            if(string.IsNullOrEmpty(end_time))
            {
                DateTime t = DateTime.Now;
                end_time = t.ToString("yyyy-MM-dd hh:mm:ss");

            }
            string EndTime = end_time.Substring(0, 10) + " 23:59:59";
            where += " and [AlarmStartTime]>='" + StartTime + "'";
            where += " and [AlarmEndTime]<='" + EndTime + "'";



            string sidx = RequstString("sidx");    //排序名称
            string sort = RequstString("sord");    //排序方式
            if ("-1" == sort)
            {
                sort = "id";
            }
            if ("-1" == order)
            {
                order = "asc";
            }
            DataListModel<Production_AlarmModel> userList = Production_AlarmTrendReport_BLL.GetList(PageIndex, PageSize, sort, order, where);
            string json = JSONTools.ScriptSerialize<DataListModel<Production_AlarmModel>>(userList);
            return json;
        }
        public string GetListNew(HttpContext context)
        {
            string start_time = context.Request["start_time"];
            string end_time = context.Request["end_time"];
            int PageSize = Convert.ToInt32(context.Request["rows"]);
            int PageIndex = Convert.ToInt32(context.Request["page"]);

            StringBuilder commandText = new StringBuilder();
            string where = "";

            if (string.IsNullOrEmpty(start_time))
            {
                DateTime t = DateTime.Now.AddMonths(-1);
                start_time = t.ToString("yyyy-MM-dd");
            }
            string StartTime = start_time.Substring(0, 10) ;
            if (string.IsNullOrEmpty(end_time))
            {
                DateTime t = DateTime.Now;
                end_time = t.ToString("yyyy-MM-dd");

            }
            string EndTime = end_time.Substring(0, 10);
            int StartIndex = (PageIndex - 1) * PageSize + 1;
            int EndIndex = PageIndex * PageSize;
            DataListModel<Production_AlarmModel> userList = Production_AlarmTrendReport_BLL.GetListNew(StartTime, EndTime,StartIndex,EndIndex);
            string json = JSONTools.ScriptSerialize<DataListModel<Production_AlarmModel>>(userList);
            return json;
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public static string RequstString(string sParam)
        {
            return (HttpContext.Current.Request[sParam] == null ? string.Empty
                  : HttpContext.Current.Request[sParam].ToString().Trim());
        }
    }
}