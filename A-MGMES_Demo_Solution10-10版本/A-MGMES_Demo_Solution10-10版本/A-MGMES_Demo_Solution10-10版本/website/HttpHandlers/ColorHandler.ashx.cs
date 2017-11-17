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
    /// 颜色配置
    /// lx 2017-06-22
    /// </summary>
    public class ColorHandler : IHttpHandler
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
                case "saveColor":
                    SaveColor();
                    break;
                case "queryColorList":
                    QueryColorList();
                    break;
                case "deleteColor":
                    DeleteColor();
                    break;


            }
        }

        /// <summary>
        /// 颜色列表查询
        /// </summary>
        void QueryColorList()
        {
            mg_PropertyBLL bll = new mg_PropertyBLL();
            string json = mg_PropertyBLL.queryColorForEditing();
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 保存颜色
        /// </summary>
        void SaveColor()
        {
            try
            {
                string prop_id = Request.Params["prop_id"];
                string prop_name = Request.Params["prop_name"];
                

                mg_PropertyModel model = new mg_PropertyModel();
                model.prop_name = prop_name;
                if (!string.IsNullOrWhiteSpace(prop_id))
                {
                    model.prop_id = Convert.ToInt32(prop_id);
                }
                else
                {
                    model.prop_id = 0;
                }

                string json = mg_PropertyBLL.SaveColor(model);
                Response.Write(json);
                Response.End();
            }
            catch (Exception ex)
            {
            }

        }

        void DeleteColor()
        {
            string prop_id = Request.Params["prop_id"];

            string json = mg_PropertyBLL.DeleteColor(prop_id);
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