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
    /// ProductHandler 的摘要说明
    /// </summary>
    public class ProductHandler : IHttpHandler
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
                
                case "saveProduct":
                saveProduct();
                break;
                case "QueryProductList":
                QueryProductList();
                break;
                case "queryProductidForPart":
                queryProductidForPart();
                break;
                case "DeleteProduct":
                DeleteProduct();
                break;
           }
        }
        
       
        void saveProduct() 
        {
            string p_id = Request.Params["p_id"];
            string ProductNo = Request.Params["ProductNo"];
            string ProductName = Request.Params["ProductName"];
            string ProductDesc = Request.Params["ProductDesc"];
            string ProductType = Request.Params["ProductType"];
            string IsUseing = Request.Params["IsUseing"];
           
            mg_ProductModel model = new mg_ProductModel();
            model.p_id = NumericParse.StringToInt(p_id);
            model.ProductNo = ProductNo;
            model.ProductName = ProductName;
            model.ProductDesc = ProductDesc;
            model.ProductType = NumericParse.StringToInt(ProductType);
            model.IsUseing = NumericParse.StringToInt(IsUseing);
           

            string json = mg_ProductBLL.SaveProduct(model);
            Response.Write(json);
            Response.End();
        }
        void QueryProductList() 
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
            string json = mg_ProductBLL.QueryProductList(page, pagesize);
            Response.Write(json);
            Response.End();
        }
        public void queryProductidForPart()
        {
            string json = mg_ProductBLL.queryProductidForPart();
            Response.Write(json);
            Response.End();
        }
        void DeleteProduct()
        {
            string p_id = Request.Params["p_id"];

            string json = mg_ProductBLL.DeleteProduct(p_id);
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