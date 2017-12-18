using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Model;
using Dal;
using Bll;
using Tools;
namespace website.HttpHandlers
{
    /// <summary>
    /// EditPassword 的摘要说明
    /// </summary>
    public class EditPassword : IHttpHandler
    {

        JavaScriptSerializer jsc = new JavaScriptSerializer();
        string Action = "";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Action = RequstString("Action");

            if (Action.Length == 0) Action = "";


            if (Action == "EditPsw")
            {
                mg_userModel userinfo = new mg_userModel();
                userinfo.user_name = RequstString("UserID");
                userinfo.user_pwd = RequstString("OldPsw");
                userinfo.user_NewPassword = RequstString("NewPsw");
                ResultMsg_User result = new ResultMsg_User();
                result = EditPsw(userinfo, result);
                context.Response.Write(jsc.Serialize(result));
            }
            
        }

        public ResultMsg_User EditPsw(mg_userModel dataEntity, ResultMsg_User result)
        {
            return  EditPassword_BLL.EditPsw(dataEntity, result);

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
    

    //public class UserInfo
    //{

    //    public string UserID { set; get; }

    //    public string UserName { set; get; }
    //    public string OldPassword { set; get; }
    //    public string NewPassword { set; get; }
    //}

    //public class ResultMsg_User
    //{
    //    public string result { set; get; }
    //    public string msg { set; get; }
    //    public UserInfo data { set; get; }
    //}

  
    }
}