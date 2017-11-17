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
    /// TestRepairItemHandler 的摘要说明
    /// </summary>
    public class TestRepairItemHandler : IHttpHandler
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

                case "saveTestRepairItem":
                    saveTestRepairItem();
                    break;
                case "QueryTestRepairItemList":
                    QueryTestRepairItemList();
                    break;
                case "DeleteTestRepairItem":
                    DeleteTestRepairItem();
                    break;
            }
        }

        void saveTestRepairItem()
        {
            string tr_id = Request.Params["tr_id"];
            string ItemCaption = Request.Params["ItemCaption"];
            string ItemType = Request.Params["ItemType"];
            string Sorting = Request.Params["Sorting"];
            string IsUseing = Request.Params["IsUseing"];

            mg_TestRepairItemModel model = new mg_TestRepairItemModel();
            model.tr_id = NumericParse.StringToInt(tr_id);
            model.ItemCaption = ItemCaption;
            model.ItemType = NumericParse.StringToInt(ItemType);
            model.Sorting = NumericParse.StringToInt(Sorting); ;
            model.IsUseing = NumericParse.StringToInt(IsUseing);


            string json = mg_TestRepairItemBLL.saveTestRepairItem(model);
            Response.Write(json);
            Response.End();
        }
        void QueryTestRepairItemList()
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
            string json = mg_TestRepairItemBLL.QueryTestRepairItemList(page, pagesize);
            Response.Write(json);
            Response.End();
        }
        void DeleteTestRepairItem()
        {
            string tr_id = Request.Params["tr_id"];

            string json = mg_TestRepairItemBLL.DeleteTestRepairItem(tr_id);
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