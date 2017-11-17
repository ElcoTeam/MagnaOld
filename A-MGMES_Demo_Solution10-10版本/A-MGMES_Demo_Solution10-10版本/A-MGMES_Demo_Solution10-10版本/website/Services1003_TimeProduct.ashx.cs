using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace website
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
            DataTable resTable = DataReader.TimeProducts(StartTime, EndTime, Flag,st_no);
            string JsonStr = "[]";
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