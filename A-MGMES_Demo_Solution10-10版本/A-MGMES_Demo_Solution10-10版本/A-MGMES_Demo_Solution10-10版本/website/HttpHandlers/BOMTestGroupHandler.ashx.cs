using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll;
using Model;
using Tools;
using Bll;

namespace website.HttpHandlers
{
    /// <summary>
    /// BOMTestGroupHandler 的摘要说明
    /// </summary>
    public class BOMTestGroupHandler : IHttpHandler
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
                case "queryGroupidForBOMTest":
                queryGroupidForBOMTest();
                break;
                case "saveBOMTestGroup":
                saveBOMTestGroup();
                break;
                case "QueryBOMTestGroupList":
                QueryBOMTestGroupList();
                break;
                case "DeleteBOMTestGroup":
                DeleteBOMTestGroup();
                break;
           }
        }
        
        public void queryGroupidForBOMTest()
        {
            string json = mg_BOMTestGroupBLL.queryGroupidForBOMTest();
            Response.Write(json);
            Response.End();
        }
        void saveBOMTestGroup() 
        {
            string group_id = Request.Params["group_id"];
            string groupname = Request.Params["groupname"];
           
            mg_BOMTestGroupModel model = new mg_BOMTestGroupModel();
            model.group_id = NumericParse.StringToInt(group_id);
            model.groupname = groupname;
           

            string json = mg_BOMTestGroupBLL.SaveBOMTestGroup(model);
            Response.Write(json);
            Response.End();
        }
        void QueryBOMTestGroupList() 
        {
            string page = Request.Params["page"];
            string pagesize = Request.Params["rows"];
            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }
            if (string.IsNullOrEmpty(pagesize))
            {
                pagesize = "15";
            }
            string json = mg_BOMTestGroupBLL.QueryBOMTestGroupList(page, pagesize);
            Response.Write(json);
            Response.End();
        }
        void DeleteBOMTestGroup()
        {
            string group_id = Request.Params["group_id"];

            string json = mg_BOMTestGroupBLL.DeleteBOMTestGroup(group_id);
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