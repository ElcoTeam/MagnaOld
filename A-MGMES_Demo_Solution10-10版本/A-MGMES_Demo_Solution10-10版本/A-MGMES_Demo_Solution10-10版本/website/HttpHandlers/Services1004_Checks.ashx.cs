using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

using System.Windows.Forms;
using Bll;


namespace website.HttpHandlers
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
            if (string.IsNullOrEmpty(StartTime))
            {
                DateTime t = DateTime.Now.AddMonths(-1);
                StartTime = t.ToString("yyyy-MM-dd HH:mm:ss");
            }
           
            if (string.IsNullOrEmpty(EndTime))
            {
                DateTime t = DateTime.Now;
                EndTime = t.ToString("yyyy-MM-dd HH:mm:ss");

            }
            if (OrderCode == "请选择")
            {
                OrderCode = "";
            }
            if (StationNo == "请选择")
            {
                StationNo = "FSA210";
            }
            
            int totalcount;
            string JsonStr = "";
            DateTime date = DateTime.Now;
            string JsonStr3 = "";
            string neirong = "";
            string yee = "";
            int PageIndex = Convert.ToInt32(request["page"]);
            int PageSize = Convert.ToInt32(request["rows"]);
            if (string.IsNullOrWhiteSpace(method))
            {
               JsonStr3 =  checkRepair_BLL.getTableString(StartTime, EndTime, OrderCode, StationNo,PageIndex, out totalcount);
               
                context.Response.ContentType = "json";
                context.Response.Write(JsonStr3);
            }
            else if (method =="GetListNew")
            {
                JsonStr3 = checkRepair_BLL.GetListNew(StartTime, EndTime, OrderCode, StationNo, PageIndex, PageSize, out totalcount);

                context.Response.ContentType = "json";
                context.Response.Write(JsonStr3);
            }
            #region 导出代码  
            else if (method == "Export")
            {
                
                DataTable ResTable4 = checkRepair_BLL.getTableExcel(StartTime, EndTime, OrderCode, StationNo,PageIndex, out totalcount);

                string str="";
                try
                {


                   // ExcelHelper.ExportDTtoExcel(ResTable4, "", HttpContext.Current.Request.MapPath("~/App_Data/检测返修报表.xlsx"));
                    
                    AsposeExcelTools.DataTableToExcel2(ResTable4, HttpContext.Current.Request.MapPath("~/App_Data/检测返修报表.xlsx"), out str);
                    if(str.Length<1)
                    {
                        JsonStr3 = "true";
                    }
                    else
                    {
                        JsonStr3 = "false:"+str;
                    }
                }
                catch
                {
                    JsonStr3 = "false:"+str;
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