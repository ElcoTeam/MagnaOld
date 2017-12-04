using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

using System.Windows.Forms;
using Bll;


namespace website
{
    /// <summary>
    /// Services1004_Checks 的摘要说明
    /// </summary>
    public class Services1004_Checks : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string StartTime = request["StartTime"];
            string EndTime = request["EndTime"];
            string OrderCode = request["OrderCode"];
            string StationNo = request["StationNo"];
            string method = request["method"];
           
            if (OrderCode == "请选择")
            {
                OrderCode = "";
            }
            if (StationNo == "请选择")
            {
                StationNo = "";
            }
            
            int totalcount;
            string JsonStr = "";
            DateTime date = DateTime.Now;
            string JsonStr3 = "";
            string neirong = "";
            string yee = "";
            int PageIndex = Convert.ToInt32(request["page"]);
            if (string.IsNullOrWhiteSpace(method))
            {
               JsonStr3 =  checkRepair_BLL.getTableString(StartTime, EndTime, OrderCode, StationNo,PageIndex, out totalcount);
               
                context.Response.ContentType = "json";
                context.Response.Write(JsonStr3);
            }
           
            #region 导出代码  
            else if (method == "Export")
            {
                
                DataTable ResTable4 = checkRepair_BLL.getTableExcel(StartTime, EndTime, OrderCode, StationNo,PageIndex, out totalcount);
               
                
                try
                {


                    ExcelHelper.ExportDTtoExcel(ResTable4, "", HttpContext.Current.Request.MapPath("~/App_Data/检测返修报表.xlsx"));
                    JsonStr3 = "true";
                }
                catch
                {
                    JsonStr3 = "false";
                }
                context.Response.ContentType = "json";
                context.Response.Write(JsonStr3);
            }
            #endregion
               
            
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