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
    /// Part1 的摘要说明
    /// </summary>

    public class Part1 : IHttpHandler
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
                case "savePart1":
                    SavePart1();
                    break;
                case "QueryPart1List":
                    QueryPart1List();
                    break;
                case "DeletePart1":
                    DeletePart1();
                    break;
            }
        }
        void SavePart1()
        {

            string part_id = Request.Params["part_id"];
            string part_no = Request.Params["part_no"];
            string part_name = Request.Params["part_name"];
            string part_categoryid = Request.Params["part_categoryid"];
            string part_desc = Request.Params["part_desc"];
            string part_type = Request.Params["part_type"];
            string FlowLine = Request.Params["FlowLine"];
            string Product = Request.Params["Product"];
            


            mg_partModel model = new mg_partModel();
            model.part_id = NumericParse.StringToInt(part_id);
            model.part_no = part_no;
            model.part_name = part_name;
            model.part_categoryid = NumericParse.StringToInt(part_categoryid);
            model.part_desc = part_desc;
            model.part_type = NumericParse.StringToInt(part_type);
            model.pflowlineid = NumericParse.StringToInt(FlowLine);
            model.pproductid = NumericParse.StringToInt(Product);
           
            
            string json = mg_Part1BLL.SavePart1(model);
            Response.Write(json);
            Response.End();
        }

        void QueryPart1List()
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
            string json = mg_Part1BLL.QueryPart1List(page, pagesize);
            Response.Write(json);
            Response.End();
        }
        void DeletePart1()
        {
            string part_id = Request.Params["part_id"];

            string json = mg_Part1BLL.DeletePart1(part_id);
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