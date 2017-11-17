using Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace website.HttpHandlers
{
    /// <summary>
    /// TorqueReporterHandler 的摘要说明
    /// </summary>
    public class TorqueReporterHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            switch (context.Request["method"])
            {
                case "get_fl_list":
                    context.Response.Write(mg_sys_logBll.getfl_idList());
                    break;
                case "get_st_list":
                    context.Response.Write(mg_sys_logBll.getst_idList(context.Request["fl_id"]));
                    break;
                case "get_part_list":
                    context.Response.Write(mg_sys_logBll.getpart_idList(context.Request["fl_id"], context.Request["st_id"]));
                    break;
                //获取该订单号
                case "get_order_list":
                    context.Response.Write(mg_sys_logBll.getalldingdanhao(context.Request["StartTime"], context.Request["EndTime"]));
                    break;
                default:
                    context.Response.Write(mg_sys_logBll.getTorqueAndAngleInfo(context.Request["fl_id"], context.Request["st_no"], context.Request["part_no"]));
                    break;
            }
            context.Response.End();
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