using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll;

namespace website.HttpHandlers
{
    /// <summary>
    /// RepairHandler 的摘要说明
    /// </summary>
    public class RepairHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            switch (context.Request["method"])
            {
                //case "get_fl_list":
                //    context.Response.Write(getrepair_ornoList(StartTime, EndTime));
                //    break;
            }
            context.Response.End();
        }

        //public static List<object> getrepair_ornoList(DateTime StartTime, DateTime EndTime)
        //{
            
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}