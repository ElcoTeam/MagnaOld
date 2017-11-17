using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll;
using Model;
using Tools;


namespace website.HttpHandlers
{
    /// <summary>
    /// TestHandler 的摘要说明
    /// </summary>
    public class TestHandler : IHttpHandler
    {

        HttpRequest Request = null;
        HttpResponse Response = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            Request = context.Request;
            Response = context.Response;

            string method = Request.Params["method"];
            switch (method)
            {
                case "QueryTestList":
                    QueryTestList();
                    break;
               
            }
        }
        public void QueryTestList()
        {
            string json = mg_TestBLL.QueryTestList();
            Response.Write(json);
            Response.End();
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