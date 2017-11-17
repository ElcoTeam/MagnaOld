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
    /// TestPartHandler 的摘要说明
    /// </summary>
    public class TestPartHandler : IHttpHandler
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
                case "queryTestPartList":
                    queryTestPartList();
                    break;
                case "saveTestPart":
                    saveTestPart();
                    break;
                case "deleteTestPart":
                    DeleteTestPart();
                    break;

            }
        }
        void queryTestPartList()
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

            string json = mg_TestPartBLL.queryTestPartList(page, pagesize);
            Response.Write(json);
            Response.End();
        }
        void saveTestPart()
        {
            string p_id = Request.Params["p_id"];
            string stationno = Request.Params["stationno"];
            string partid = Request.Params["partid"];
            //string sorting = Request.Params["sorting"];
            string tIDS = Request.Params["tIDS"];

            mg_TestPartModel model = new mg_TestPartModel();
            model.p_id = NumericParse.StringToInt(p_id);
            model.partid = NumericParse.StringToInt(partid);
            model.stationno = stationno;
            //model.sorting = NumericParse.StringToInt(sorting);
            model.tIDS = tIDS;


            string json = mg_TestPartBLL.SaveTestPart(model);
            Response.Write(json);
            Response.End();
        }
        void DeleteTestPart()
        {
            string p_id = Request.Params["p_id"];

            string json = mg_TestPartBLL.DeleteTestPart(p_id);
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