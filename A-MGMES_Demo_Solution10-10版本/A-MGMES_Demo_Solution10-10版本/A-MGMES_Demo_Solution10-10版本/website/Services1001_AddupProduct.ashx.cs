using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace website
{
    /// <summary>
    /// Service1001_AddupProduct 的摘要说明
    /// </summary>
    public class Service1001_AddupProduct : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string StartTime = request["StartTime"];
            string EndTime = request["EndTime"];
            int Flag = Convert.ToInt32(request["Flag"]);    //按天，flag=2,按小时，flag=1
            string st_no = request["st_no"];
            DataTable outtable;
            DataTable resTable = DataReader.GetAddupProducts(StartTime, EndTime, Flag,st_no,out outtable);
            string JsonStr = "[]";
            ExcelHelper.ExportDTtoExcel(outtable, "", HttpContext.Current.Request.MapPath("~/App_Data/excel2009.xlsx"));
            if(resTable != null)
                JsonStr = FunCommon.DataTableToJson(resTable);

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