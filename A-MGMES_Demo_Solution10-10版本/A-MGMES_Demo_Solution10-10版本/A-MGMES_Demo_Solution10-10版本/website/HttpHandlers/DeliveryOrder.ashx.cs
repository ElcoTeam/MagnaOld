using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Bll;

namespace website.HttpHandlers
{
    
    /// <summary>
    /// DeliveryOrder 的摘要说明
    /// </summary>
    public class DeliveryOrder : IHttpHandler
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

                case "editDeliveryOrder":
                    EditDeliveryOrder();
                    break;

                case "queryOrder":
                    queryOrder();
                    break;
               
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 库存发货 订单信息列表
        /// </summary>
        public void queryOrder()
        {
            string page = Request.Params["page"];
            string pagesize = Request.Params["rows"];
            string orderid = Request.Params["orderid"];
            int totalcount = 0;
            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }
            if (string.IsNullOrEmpty(pagesize))
            {
                pagesize = "20";
            }

            DataTable resTable = DeliveryOrderBLL.getTable(page, pagesize, orderid, out totalcount);
            string json = FunCommon.DataTableToJson2(totalcount, resTable);
            Response.Write(json);
            Response.End();

        }

        /// <summary>
        /// 降低库存操作
        /// </summary>
        void EditDeliveryOrder()
        {
            string PRODN = Request.Params["PRODN"];
            string OrderIsHistory = Request.Params["OrderIsHistory"];

            //mg_CustomerOrder_3 model = new mg_CustomerOrder_3();
            //model.OrderID1 = NumericParse.StringToInt(OrderID);
            ///model._VinNumber = VinNumber;
            //model._OrderIsHistory = NumericParse.StringToInt(OrderIsHistory);

            string json = DeliveryOrderBLL.EditDeliveryOrder(PRODN,OrderIsHistory); 
            Response.Write(json);
            Response.End();
        }
    }
}