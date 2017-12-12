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

        HttpRequest Request = null;
        HttpResponse Response = null;
        public void ProcessRequest(HttpContext context)
        {
            Request = context.Request;
            Response = context.Response;
            context.Response.ContentType = "text/plain";
            switch (context.Request["method"])
            {
                case "get_fl_list":
                    fl();
                   // context.Response.Write(mg_sys_logBll.getfl_idList());
                    break;
                case "get_st_list":
                    st();
                    //context.Response.Write(mg_sys_logBll.getst_idList(context.Request["fl_id"]));
                    break;
                case "get_st_listforcheck":
                    st_for_check();
                    //context.Response.Write(mg_sys_logBll.getst_idList(context.Request["fl_id"]));
                    break;
                case "get_st_listForStep":
                    st_for_step();
                    //context.Response.Write(mg_sys_logBll.getst_idList(context.Request["fl_id"]));
                    break;
                case "get_st_listForVolume":
                    st_for_volume();
                    //context.Response.Write(mg_sys_logBll.getst_idList(context.Request["fl_id"]));
                    break;
                case "get_st_listForTime":
                    st_for_time();
                    //context.Response.Write(mg_sys_logBll.getst_idList(context.Request["fl_id"]));
                    break;
                case "get_part_list":
                    part();
                    //context.Response.Write(mg_sys_logBll.getpart_idList(context.Request["fl_id"], context.Request["st_id"]));
                    break;
                //获取该订单号
                case "get_order_list":
                    order();
                    //context.Response.Write(mg_sys_logBll.getalldingdanhao(context.Request["StartTime"], context.Request["EndTime"]));
                    break;
                default:
                    Select();
                    //context.Response.Write(mg_sys_logBll.getTorqueAndAngleInfo(context.Request["fl_id"], context.Request["st_no"], context.Request["part_no"]));
                    break;
            }
            context.Response.End();
        }
        void fl()
        {
            string a = mg_sys_logBll.getfl_idList();
            Response.Write(a);
            Response.End();
        }
        void st_for_check()
        {
            string a = mg_sys_logBll.getst_idListForCheck(RequstString("fl_id"));
            Response.Write(a);
            Response.End();
        }
        void st_for_step()
        {
            string a = mg_sys_logBll.getst_idListForStep(RequstString("fl_id"));
            Response.Write(a);
            Response.End();
        }
        void st_for_time()
        {
            string a = mg_sys_logBll.getst_idListForTime(RequstString("fl_id"));
            Response.Write(a);
            Response.End();
        }
        void st_for_volume()
        {
            string a = mg_sys_logBll.getst_idListForVolume(RequstString("fl_id"));
            Response.Write(a);
            Response.End();
        }
        void st()
        {
            string a = mg_sys_logBll.getst_idList(RequstString("fl_id"));
            Response.Write(a);
            Response.End();
        }
        void part()
        {
            string a = mg_sys_logBll.getpart_idList(RequstString("fl_id"), RequstString("st_no"));
            Response.Write(a);
            Response.End();
        }
        void order()
        {
            string a = mg_sys_logBll.getalldingdanhao(RequstString("StartTime"), RequstString("EndTime"));
            Response.Write(a);
            Response.End();
        }
        void Select()
        {
            string a = mg_sys_logBll.getTorqueAndAngleInfo(RequstString("fl_id"), RequstString("st_no"), RequstString("part_no"));
            Response.Write(a);
            Response.End();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public static string RequstString(string sParam)
        {
            return (HttpContext.Current.Request[sParam] == null ? string.Empty
                  : HttpContext.Current.Request[sParam].ToString().Trim());
        }
    }
}