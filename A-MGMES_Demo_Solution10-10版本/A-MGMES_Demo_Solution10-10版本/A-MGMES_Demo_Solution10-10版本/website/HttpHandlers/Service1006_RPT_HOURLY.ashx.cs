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
    /// Service1006_RPT_HOURLY 的摘要说明
    /// </summary>
    public class Service1006_RPT_HOURLY : IHttpHandler
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
                case "GetListNew":
                    context.Response.Write(GetListNew(context));
                    break;
                case "GetClassInfo":
                    context.Response.Write(GetClassInfo());
                    break;
                case "Export":
                   Export(context);
                    break;
                case "Print":
                    Print(context);
                    break;
                default:
                    GetListNew(context);
                    break;

            }
        }
        public string GetListNew(HttpContext context)
        {
            string StartTime = context.Request["start_time"];
            string EndTime = context.Request["end_time"];
            if (string.IsNullOrEmpty(StartTime))
            {
                DateTime t = DateTime.Now;
                StartTime = t.AddDays(-1).ToString("yyyy-MM-dd hh:mm:ss");
            }
            if (string.IsNullOrEmpty(EndTime))
            {
                DateTime t = DateTime.Now;
                
                EndTime = t.ToString("yyyy-MM-dd hh:mm:ss");
            }
            string clid = context.Request["clnameid"];
            int clnameid = 0;
            if(string.IsNullOrEmpty(clid))
            {
                clnameid = 0;
            }
            else
            {
              clnameid = Convert.ToInt32(clid);
            }
            
            string clname = context.Request["clname"];
            int PageSize = Convert.ToInt32(context.Request["rows"]);
            int PageIndex = Convert.ToInt32(context.Request["page"]);
            string sort = RequstString("sidx");    //排序名称
            string order = RequstString("sord");    //排序方式

            DataListModel<Production_Model> userList = Production_Report_BLL.GetListNew(StartTime, EndTime, clnameid, clname, PageIndex,PageSize);
            string json = JSONTools.ScriptSerialize<DataListModel<Production_Model>>(userList);
            return json;
        }

        public void Print(HttpContext context)
        {
            string StartTime = context.Request["start_time"];
            string EndTime = context.Request["end_time"];
            if (string.IsNullOrEmpty(StartTime))
            {
                DateTime t = DateTime.Now;
                StartTime = t.AddDays(-1).ToString("yyyy-MM-dd hh:mm:ss");
            }
            if (string.IsNullOrEmpty(EndTime))
            {
                DateTime t = DateTime.Now;

                EndTime = t.ToString("yyyy-MM-dd hh:mm:ss");
            }
            string clid = context.Request["clnameid"];
            int clnameid = 0;
            if (string.IsNullOrEmpty(clid))
            {
                clnameid = 0;
            }
            else
            {
                clnameid = Convert.ToInt32(clid);
            }
            string clname = context.Request["clname"];
            int PageSize = Convert.ToInt32(context.Request["rows"]);
            int PageIndex = Convert.ToInt32(context.Request["page"]);
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
            string fileName = HttpContext.Current.Request.MapPath("~/App_Data/生产报表.xlsx");

            try
            {
                int StartIndex = 1;
                int EndIndex = -1;
                int totalcount = 0;
                DataTable resTable = Production_Report_BLL.getTable(StartTime, EndTime, clnameid, clname, StartIndex, EndIndex, out totalcount);
                string title = " 生产报表 - 班次（小时）";
                string html = DataHelper.ExportDatatableToHtml(resTable, title);
                string ss = "true";
                json = "{\"Result\":\"" + ss + "\"," + "\"Html\":\"" + html + "\"}";


            }
            catch (Exception e)
            {
                string ss1 = "false";
                json = "{\"Result\":\"" + ss1 + "\"}";



            }


            context.Response.ContentType = "json";
            context.Response.Write(json);
        }
        public void Export(HttpContext context)
        {
            string StartTime = context.Request["start_time"];
            string EndTime = context.Request["end_time"];
            if (string.IsNullOrEmpty(StartTime))
            {
                DateTime t = DateTime.Now;
                StartTime = t.AddDays(-1).ToString("yyyy-MM-dd hh:mm:ss");
            }
            if (string.IsNullOrEmpty(EndTime))
            {
                DateTime t = DateTime.Now;

                EndTime = t.ToString("yyyy-MM-dd hh:mm:ss");
            }
            string clid = context.Request["clnameid"];
            int clnameid = 0;
            if (string.IsNullOrEmpty(clid))
            {
                clnameid = 0;
            }
            else
            {
                clnameid = Convert.ToInt32(clid);
            }
            string clname = context.Request["clname"];
            int PageSize = Convert.ToInt32(context.Request["rows"]);
            int PageIndex = Convert.ToInt32(context.Request["page"]);         
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
            string fileName = HttpContext.Current.Request.MapPath("~/App_Data/生产报表.xlsx");

            try
            {
                int StartIndex = 1;
                int EndIndex = -1;
                int totalcount = 0;
                DataTable resTable = Production_Report_BLL.getTable(StartTime, EndTime, clnameid, clname, StartIndex, EndIndex, out totalcount);
                //ExcelHelper.ExportDTtoExcel(resTable, "生产报表", fileName);
                string err = "";
                AsposeExcelTools.DataTableToExcel2(resTable, fileName, out err);
                string ss = "true";
                if (err.Length < 1)
                {
                    ss = "true";
                }
                else
                {
                    ss = "false";
                }
               
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
        public string GetClassInfo()
        {
            DataTable dt = Production_Report_BLL.GetClassInfo();
            string json = JSONTools.DataTableToJson(dt);
            return json;
        }
        public string GetList(HttpContext context)
        {
            string StartTime = context.Request["start_time"];
            string EndTime = context.Request["end_time"];
            string clnameid = context.Request["clnameid"];
            int PageSize = Convert.ToInt32(context.Request["rows"]);
            int PageIndex = Convert.ToInt32(context.Request["page"]);

            StringBuilder commandText = new StringBuilder();
            string where = "";
            if (!string.IsNullOrWhiteSpace(clnameid))
            {
                where += " and cl_name like'%" + clnameid + "%' ";
                if (!string.IsNullOrWhiteSpace(StartTime))
                {
                    StartTime = StartTime.Substring(0, 10);
                    where += " and product_date>='" + StartTime + "'";
                    string endtime = Convert.ToDateTime(StartTime).AddDays(1).ToString();
                    where += " and product_date<='" + endtime.Substring(0, 10) + "'";
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(StartTime))
                {

                    where += " and product_date>='" + StartTime + "'";
                }
                if (!string.IsNullOrWhiteSpace(EndTime))
                {
                    where += " and product_date<='" + EndTime + "'";
                }
            }
           
            string sort = RequstString("sidx");    //排序名称
            string order = RequstString("sord");    //排序方式
            DataListModel<Production_Model> userList = Production_Report_BLL.GetList(PageIndex, PageSize, sort, order, where);
            string json = JSONTools.ScriptSerialize<DataListModel<Production_Model>>(userList);
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