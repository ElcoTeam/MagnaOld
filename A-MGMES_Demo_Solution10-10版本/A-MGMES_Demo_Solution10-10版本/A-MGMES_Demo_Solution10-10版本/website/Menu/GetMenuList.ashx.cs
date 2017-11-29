using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll;
namespace website.Menu
{
    /// <summary>
    /// GetMenuList 的摘要说明
    /// </summary>
    public class GetMenuList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string menuid = RequstString("menuid");
            context.Response.Write(GetUserMenu(menuid));
        }

        public string GetUserMenu(string menuid)
        {
            string json = Sys_MenuBLL.GetMenuList(menuid);
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