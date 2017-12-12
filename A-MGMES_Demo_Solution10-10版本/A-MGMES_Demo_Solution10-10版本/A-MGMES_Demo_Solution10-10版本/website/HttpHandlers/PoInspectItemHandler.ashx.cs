using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using Bll;
using Tools;

namespace website.HttpHandlers
{
    /// <summary>
    /// PoInspectItemHandler 的摘要说明
    /// </summary>
    public class PoInspectItemHandler : IHttpHandler
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

                case "savePoInstpectItem":
                    savePoInstpectItem();
                    break;
                case "QueryPoInspectItemList":
                    QueryPoInspectItemList();
                    break;
                case "QueryPoInspectItemListALL":
                    QueryPoInspectItemListALL();
                    break;
                case "DeletePoInspectItem":
                    DeletePoInspectItem();
                    break;
            }
        }

        void savePoInstpectItem()
        {
            string pi_id = Request.Params["pi_id"];
            string piitem = Request.Params["piitem"];
            string piitemdescribe = Request.Params["piitemdescribe"];

            mg_PoInspectItemModel model = new mg_PoInspectItemModel();
            model.pi_id = NumericParse.StringToInt(pi_id);
            model.piitem = piitem;
            model.piitemdescribe = piitemdescribe;


            string json = mg_PoInspectItemBLL.savePoInstpectItem(model);
            Response.Write(json);
            Response.End();
        }
        void QueryPoInspectItemList()
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
            string json = mg_PoInspectItemBLL.QueryPoInspectItemList(page, pagesize);
            Response.Write(json);
            Response.End();
        }
        void QueryPoInspectItemListALL()
        {
       
            string json = mg_PoInspectItemBLL.QueryPoInspectItemListALL();
            Response.Write(json);
            Response.End();
        }
        void DeletePoInspectItem()
        {
            string pi_id = Request.Params["pi_id"];

            string json = mg_PoInspectItemBLL.DeletePoInspectItem(pi_id);
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