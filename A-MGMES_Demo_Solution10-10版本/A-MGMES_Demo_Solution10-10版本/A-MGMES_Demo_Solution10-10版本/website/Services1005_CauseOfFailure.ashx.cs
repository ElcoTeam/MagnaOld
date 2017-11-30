using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Bll;
namespace website
{
    /// <summary>
    /// Services1005_CauseOfFailure 的摘要说明
    /// </summary>

    public class Services1005_CauseOfFailure : IHttpHandler
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

                case "AddCauseOfFailure":
                    AddCauseOfFailure();
                    break;
                case "SelectCauseOfFailure":
                    SelectCauseOfFailure();
                    break;
                
                case "DelectCauseOfFailure":
                    DelectCauseOfFailure();
                    break;
            }
        }
        void AddCauseOfFailure()    //增加故障原因
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string ID = request.Params["ID"];
            string Name = request.Params["Name"];
            string Code = request.Params["Code"];
            if (ID == "") //添加
            {
                
                //
                int result = Failure_BLL.AddFailure(Name, Code);
                string ss;
                if (result > 0)
                {
                    ss = "True";
                }
                else
                {
                    ss = "false";
                }
                string josn1 = "{\"Result\":\"" + ss + "\"}";
                Response.Write(josn1);
            }
            else  //编辑
            {

                int result = Failure_BLL.EditFailure(Name, Code,ID);
                
                string ss;
                if (result > 0)
                {
                    ss = "True";
                }
                else
                {
                    ss = "false";
                }
         
                string josn1 = "{\"Result\":\"" + ss + "\"}";
                Response.Write(josn1);
            }

            Response.End();
        }
        void SelectCauseOfFailure()    //查询故障原因     
        {
            DataTable ResTable = Failure_BLL.GetTable();
            string json = FunCommon.DataTableToJson(ResTable);
            Response.Write(json);
            Response.End();
        }
        
        void DelectCauseOfFailure()    //删除故障原因
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string ID = request["ID"];
            int a = Failure_BLL.DeleteFailure(ID);
           
            string ss;
            if (a > 0)
            {
                ss = "True";
            }
            else
            {
                ss = "false";
            }
           
            string josn1 = "{\"Result\":\"" + ss + "\"}";
            Response.Write(josn1);
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