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
    /// RPT_ALARM_DLY 的摘要说明
    /// </summary>
    public class RPT_ALARM_DLY : IHttpHandler
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
                case "GetList":
                    context.Response.Write(GetList(context));
                    break;
                case "Export":
                    Export(context);
                    break;
                default:
                    GetList(context);
                    break;

            }
        }
        public void Export(HttpContext context)
        {
            string date_time = context.Request["date_time"];
            
            int PageSize = Convert.ToInt32(context.Request["rows"]);
            int PageIndex = Convert.ToInt32(context.Request["page"]);

            StringBuilder commandText = new StringBuilder();
            string where = "";

            if(string.IsNullOrEmpty(date_time))
            {
                date_time = new DateTime().ToShortDateString();
            }
            string StartTime = date_time.Substring(0, 10) + "00:00:00";
            string EndTime = date_time.Substring(0, 10) + "23:59:59";
            where += " and product_date>='" + StartTime + "'";
            where += " and product_date<='" + EndTime + "'";
                
               

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
            string fileName = HttpContext.Current.Request.MapPath("~/App_Data/生产线报警报表.xlsx");
            try
            {
                int StartIndex = 1;
                int EndIndex = -1;
                int totalcount = 0;
                DataTable resTable = Production_AlarmDlyReport_BLL.getTable(PageSize, StartIndex, EndIndex, sort, order, where, out totalcount);
                ExcelHelper.ExportDTtoExcel(resTable, "生产线报警报表", fileName);
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
            string date_time = context.Request["date_time"];

            int PageSize = Convert.ToInt32(context.Request["rows"]);
            int PageIndex = Convert.ToInt32(context.Request["page"]);

            StringBuilder commandText = new StringBuilder();
            string where = "";

            if (string.IsNullOrEmpty(date_time))
            {
                DateTime t = DateTime.Now;
                date_time = t.ToString("yyyy-MM-dd hh:mm:ss");
            }
            string StartTime = date_time.Substring(0, 10) + " 00:00:00";
            string EndTime = date_time.Substring(0, 10) + " 23:59:59";
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
            DataListModel<Production_AlarmModel> userList = Production_AlarmDlyReport_BLL.GetList(PageIndex, PageSize, sort, order, where);
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