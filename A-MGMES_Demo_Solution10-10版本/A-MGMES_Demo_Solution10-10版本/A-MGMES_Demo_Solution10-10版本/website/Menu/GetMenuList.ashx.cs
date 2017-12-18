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
            string action = RequstString("ACTION");
            string currentuser = RequstString("currentuser");

            if (action == "usermenulist")
            {
                context.Response.Write(GetUserMenu(menuid,currentuser));
            }
            else if (action == "menutree")
            {
                context.Response.Write(GetMenuTree());
            }
           
        }

        /// <summary>
        /// 用户菜单
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public string GetUserMenu(string menuid,string currentuser)
        {
            string json = Sys_MenuBLL.GetMenuList(menuid,currentuser);
            return json;
        }

        /// <summary>
        /// 菜单树
        /// </summary>
        /// <returns></returns>
        public string GetMenuTree()
        {
            string json = Sys_MenuBLL.GetMenuTree();
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