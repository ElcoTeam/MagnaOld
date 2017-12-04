using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll;
namespace website
{
    /// <summary>
    /// DelJetSEQNR 的摘要说明
    /// </summary>
    public class DelJetSEQNR : IHttpHandler
    {
        HttpRequest Request = null;
        HttpResponse Response = null;

        public void ProcessRequest(HttpContext context)
        {
            Request = HttpContext.Current.Request;
            Response = HttpContext.Current.Response;

            context.Response.ContentType = "text/plain";
            string method = Request["method"];
            string new_num = Request["seqnrnum"];    //流水线
            switch(method)
            {
                case "select":
                    select();
                    break;
                case "edit":
                    edit(new_num);
                    break;
                default:
                    select();
                    break;
            }

        }

        public static void select()
        {
            string result =  DelJetSEQNR_BLL.select();
           HttpContext.Current.Response.Write(result);
           HttpContext.Current.Response.End();

        }
        public static void edit(string seqnr)
        {
            string result = DelJetSEQNR_BLL.edit(seqnr);
            select();
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