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
    /// FlowLine1Handler 的摘要说明
    /// </summary>
    public class FlowLine1Handler : IHttpHandler
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
                
                case "saveFlowLine1":
                saveFlowLine1();
                break;
                case "QueryFlowLine1List":
                QueryFlowLine1List();
                break;
                case "queryFlowLineidForPart":
                queryFlowLineidForPart();
                break;
                case "DeleteFlowLine1":
                DeleteFlowLine1();
                break;
           }
        }
        
       
        void saveFlowLine1() 
        {
            string fl_id = Request.Params["fl_id"];
            string fl_name = Request.Params["fl_name"];
            string flowlinetype = Request.Params["flowlinetype"];
           
            mg_FlowlingModel model = new mg_FlowlingModel();
            model.fl_id = NumericParse.StringToInt(fl_id);
            model.fl_name = fl_name;
            model.flowlinetype = NumericParse.StringToInt(flowlinetype);
           

            string json = mg_FlowLine1BLL.SaveFlowLine1(model);
            Response.Write(json);
            Response.End();
        }
        void QueryFlowLine1List() 
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
            string json = mg_FlowLine1BLL.QueryFlowLine1List(page, pagesize);
            Response.Write(json);
            Response.End();
        }
        public void queryFlowLineidForPart()
        {
            string json = mg_FlowLine1BLL.queryFlowLineidForPart();
            Response.Write(json);
            Response.End();
        }
        void DeleteFlowLine1()
        {
            string fl_id = Request.Params["fl_id"];

            string json = mg_FlowLine1BLL.DeleteFlowLine1(fl_id);
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