using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;
using System.Reflection;

namespace website
{
    /// <summary>
    /// Services1000_SysLog 的摘要说明
    /// </summary>
    public class Services1000_SysLog : IHttpHandler
    {
        HttpRequest Request = null;
        HttpResponse Response = null;
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string ran = request.QueryString["ran"];
            string AssemblyLine = request.Params["fl_name"];    //流水线
            string Station = request.Params["st_no"];   //工位
            string StartTime = request["StartTime"];
            string EndTime = request["EndTime"];
            string OrderId = request["OrderId"];
            int Num = Convert.ToInt32(request["Num"]);
            int SortFlag = Convert.ToInt32(request["SortFlag"]);
            int PageSize = Convert.ToInt32(request["rows"]);
            int PageIndex = Convert.ToInt32(request["page"]);
            //测试数据
            //StartTime = "2017-08-01 16:51";
            //EndTime = "2017-08-03 16:51";
            string SqlStr;
            int totalcount;
            DataTable resTable;
            DataTable dt1;
            if (string.IsNullOrEmpty(Station) & !string.IsNullOrEmpty(StartTime))   //只有时间，工位不选
            {
                resTable = DataReader.GetLogs(StartTime, EndTime, PageSize, PageIndex);
                SqlStr = @"select count(*) from mg_sys_log where cast(Step_StartTime as datetime) >= '" + StartTime + "' and cast(Step_StartTime as datetime) <= '" + EndTime + @"'";
                totalcount = FunSql.GetInt(SqlStr);
                dt1 = DataReader.GetLogs(StartTime, EndTime, totalcount, 1);
            }
            else if (!string.IsNullOrEmpty(OrderId))    //只要订单号不为空就进到这里处理
            {
                if (string.IsNullOrEmpty(Station))
                {
                    resTable = DataReader.GetOrderLogs(OrderId, PageSize, PageIndex);
                    string SqlStr1 = @" select count(*) from mg_sys_log where or_no = '" + OrderId + @"'";
                    totalcount = FunSql.GetInt(SqlStr1);
                    dt1 = DataReader.GetOrderLogs(OrderId, totalcount, 1);
                }
                else
                {
                    string SqlStr1 = "SELECT top " + PageSize + "  * FROM mg_sys_log WHERE st_no='" + Station + "' and or_no='" + OrderId + "' and sys_id NOT IN(SELECT TOP " + PageSize * (PageIndex - 1) + " sys_id FROM  mg_sys_log where st_no='" + Station + "' and or_no='" + OrderId + "' ORDER BY st_no,sys_id) ORDER BY st_no,sys_id";
                    FunSql.Init();
                    resTable = FunSql.GetTable(SqlStr1);
                    string SqlStr2 = @" select count(*) from mg_sys_log where or_no = '" + OrderId + @"' and st_no='" + Station + "'";
                    totalcount = FunSql.GetInt(SqlStr2);
                    dt1 = FunSql.GetTable("select * from mg_sys_log where st_no='" + Station + "' and and or_no='" + OrderId + "'");
                }
            }
            else    //其他
            {
                resTable = DataReader.GetLogs2(AssemblyLine, Station, StartTime, EndTime, PageSize, PageIndex, OrderId);
                //计算总行数 Excel文件有最多65535行数据的限制
                DataTable resTable1 = DataReader.GetLogs2(AssemblyLine, Station, StartTime, EndTime, 1000*10000, 1, OrderId);
                totalcount = resTable1.Rows.Count;
                //dt1 = DataReader.GetLogs(StartTime, EndTime, totalcount, 1);
                if (totalcount < 65500)
                {
                    dt1 = resTable1;
                }
                else
                {
                    dt1 = null;
                }

            }
            string JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);
            ExcelHelper.ExportDTtoExcel(dt1, "HeaderText", HttpContext.Current.Request.MapPath("~/App_Data/excel2006.xlsx"));
            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonStr);
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