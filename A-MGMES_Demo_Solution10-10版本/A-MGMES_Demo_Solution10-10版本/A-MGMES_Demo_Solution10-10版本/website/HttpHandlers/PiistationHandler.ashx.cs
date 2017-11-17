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
    /// PiistationHandler 的摘要说明
    /// </summary>
    public class PiistationHandler : IHttpHandler
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
                case "queryPiistationList":
                    queryPiistationList();
                    break;
                case "savePiiStation":
                    savePiiStation();
                    break;
                case "deletePiistation":
                    DeletePiistation();
                    break;

            }
        }
        void queryPiistationList()
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

            string json = mg_PiistationBLL.queryPiistationList(page, pagesize);
            Response.Write(json);
            Response.End();
        }
        void savePiiStation()
        {
            string ps_id = Request.Params["ps_id"];
            string station_no = Request.Params["station_no"];
            string sorting = Request.Params["sorting"];
            string piIDs = Request.Params["piIDs"];

            mg_PiistationModel model = new mg_PiistationModel();
            model.ps_id = NumericParse.StringToInt(ps_id);
            model.station_no = station_no;
            model.sorting = NumericParse.StringToInt(sorting);
            model.piIDs = piIDs;


            string json = mg_PiistationBLL.SavePiiStation(model);
            Response.Write(json);
            Response.End();
        }
        void DeletePiistation()
        {
            string ps_id = Request.Params["ps_id"];

            string json = mg_PiistationBLL.DeletePiistation(ps_id);
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