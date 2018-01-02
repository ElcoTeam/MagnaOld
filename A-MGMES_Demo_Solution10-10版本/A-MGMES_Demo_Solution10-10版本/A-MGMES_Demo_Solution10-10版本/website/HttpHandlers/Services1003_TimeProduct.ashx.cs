using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Bll;
namespace website.HttpHandlers
{
    /// <summary>
    /// Services1003_TimeProduct 的摘要说明
    /// </summary>
    public class Services1003_TimeProduct : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {//excel
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string StartTime = request["StartTime"];
            string EndTime = request["EndTime"];
            int Flag = Convert.ToInt32(request["Flag"]);
            string st_no = request["st_no"];
            string method = request["method"];
            if (string.IsNullOrWhiteSpace(method))
            {
                DataTable resTable = Time_ReportBLL.TimeProducts(StartTime, EndTime, Flag, st_no);
                string JsonStr = "[]";
                if (resTable != null)
                    JsonStr = FunCommon.DataTableToJson(resTable);

                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            else
            {
                string json = "";
                try
                {


                    DataTable resTable = Time_ReportBLL.TimeProducts(StartTime, EndTime, Flag, st_no);
                    //ExcelHelper.ExportDTtoExcel(resTable, "", HttpContext.Current.Request.MapPath("~/App_Data/时间信息报表.xlsx"));
                    string fileName = HttpContext.Current.Request.MapPath("~/App_Data/时间信息报表.xlsx");
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
                   
                    json = ss;
                }
                catch
                {
                    json = "false";
                }
                context.Response.ContentType = "json";
                context.Response.Write(json);
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