using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll;
using Model;
using Tools;


    /// <summary>
    /// 配置
    ///
    /// </summary>
    public class Px_TargetHandler : IHttpHandler
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
                case "savetarget":
                    Savetarget();
                    break;
                case "querytargetList":
                    QuerytargetList();
                    break;



            }
        }

        /// <summary>
        /// 列表查询
        /// </summary>
        void QuerytargetList()
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
            px_TargetBLL bll = new px_TargetBLL();
            string json = px_TargetBLL.Querypx_TargetList(page, pagesize);
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 保存
        /// </summary>
        void Savetarget()
        {
            try
            {
                string pxtarget_id = Request.Params["pxtarget_id"];
                string pxtarget_time = Request.Params["pxtarget_time"];
                string pxtarget_target = Request.Params["pxtarget_target"];

                px_TargetModel model = new px_TargetModel();
                model.pxtarget_id = NumericParse.StringToInt(pxtarget_id);
                model.pxtarget_target = NumericParse.StringToInt(pxtarget_target);
                model.pxtarget_time = pxtarget_time;
                if (!string.IsNullOrWhiteSpace(pxtarget_id))
                {
                    model.pxtarget_id = NumericParse.StringToInt(pxtarget_id);
                }
                else
                {
                    model.pxtarget_id = 0;
                }

                string json = px_TargetBLL.Savepx_Target(model);
                Response.Write(json);
                Response.End();
            }
            catch (Exception ex)
            {
            }

        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
