using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;

using System.Runtime.Serialization.Formatters;

using System.Runtime.Serialization.Formatters.Binary;

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
            string method = request.Params["method"];
            string AssemblyLine = request.Params["fl_name"];    //流水线
            string Station = request.Params["st_no"];   //工位
            string StartTime = request["StartTime"];
            string EndTime = request["EndTime"];
            string OrderId = request["OrderId"];

            string SortFlag = request["sort"];
            string sortOrder = request["order"];
            int PageSize = Convert.ToInt32(request["rows"]);
            int PageIndex = Convert.ToInt32(request["page"]);
            //测试数据
            //StartTime = "2017-08-01 16:51";
            //EndTime = "2017-08-03 16:51";
            int totalcount;
            DataTable resTable = new DataTable();
            //ygy 
            string wherestr = " ";
            string JsonStr="";
            string sort = "-1";
            string order = "-1";
            if (!string.IsNullOrWhiteSpace(AssemblyLine))
            {
                wherestr += "  and fl_name ='" + AssemblyLine + "'";
            }
            if (!string.IsNullOrWhiteSpace(Station))
            {
                wherestr += "  and st_no='" + Station + "'";
            }
            if (!string.IsNullOrWhiteSpace(OrderId))
            {
                wherestr += " and or_no = '" + OrderId + @"'";
            }

            if (!string.IsNullOrWhiteSpace(StartTime))
            {
                wherestr += " and cast(step_startTime as datetime) >= '" + StartTime + "' ";
            }
            if (!string.IsNullOrWhiteSpace(EndTime))
            {
                wherestr += " and cast(step_endTime as datetime) <= '" + EndTime + @"'";
            }
            //查询
            #region
            if (string.IsNullOrWhiteSpace(method))
            {
                sort = SortFlag;
                order = sortOrder;
                int StartIndex = PageSize * (PageIndex - 1) + 1;
                int EndIndex = StartIndex + PageSize - 1;
                string query_sql = " select * from(select row_number() over(order by " + SortFlag + " " + sortOrder + " ) as rowid,report.* from mg_sys_log report  where 1 = 1 " + wherestr + ") as Results where rowid >=" + StartIndex + " and rowid <=" + EndIndex + " ";
                string count_sql = "select  count(*) from mg_sys_log where 1 = 1" + wherestr;
                FunSql.Init();
                resTable = FunSql.GetTable(query_sql);
                FunSql.Init();
                totalcount = FunSql.GetInt(count_sql);
                JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);
                //
                string sql = "select  * from mg_sys_log where 1 = 1" + wherestr;
                resTable = FunSql.GetTable(sql);
                
                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);

            }
            #endregion
            //导出
            #region
            if (method == "Export")
            {
                if("-1"==sort)
                {
                    sort = "sys_id";
                }
                if("-1"==order)
                {
                    order = "asc";
                }
                string json = "";
                string fileName = HttpContext.Current.Request.MapPath("~/App_Data/步骤日志报表.xlsx");
                try
                {
                    string count_sql = "select  count(*) from mg_sys_log where 1 = 1" + wherestr;
                    FunSql.Init();
                    totalcount = FunSql.GetInt(count_sql);
                    int start = 0;
                    int end = 0;
                    string query_sql = "";
                    DataTable dt = new DataTable();
                    int page = totalcount / ExcelHelper.EXCEL03_MaxRow;

                    string sheetName = "sheet";
                    if (page * ExcelHelper.EXCEL03_MaxRow < totalcount)//当总行数不被sheetRows整除时，经过四舍五入可能页数不准
                    {
                        page = page + 1;
                    }
                    for (int i = 0; i < page; i++)
                    {
                        sheetName ="sheet"+i.ToString();
                        start = i * ExcelHelper.EXCEL03_MaxRow + 1;
                        end = (i * ExcelHelper.EXCEL03_MaxRow) + ExcelHelper.EXCEL03_MaxRow;
                        if (end > totalcount)
                        {
                            end = totalcount;
                        }
                        query_sql = " select * from(select row_number() over(order by " + sort + "  " + order + " ) as rowid,report.* from mg_sys_log report  where 1 = 1 " + wherestr + ") as Results where rowid >=" + start + " and rowid <=" + end + " ";
                        dt = FunSql.GetTable(query_sql);
                        if (i == 0)
                        {
                            byte[] data = ExcelHelper.ExportDTtoExcelTest(dt, "HeaderText", fileName, i);
                            FileStream fs = new FileStream(fileName, FileMode.Create);
                            fs.Write(data, 0, data.Length);
                            fs.Close();
                        }

                        else
                        {
                            ExcelHelper.InsertSheet(fileName, sheetName, dt);
                        }
                    }
                    string ss = "true";
                    json = "{\"Result\":\"" + ss + "\"}";
                   
                }
                catch(Exception e)
                {
                    string ss1 = "false";
                    json = "{\"Result\":\"" + ss1 + "\"}";
                 
                    
                   
                }


                context.Response.ContentType = "json";
                context.Response.Write(json);
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