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
    public class Services1006_FTT : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string StartTime = request["start_time"];
            string EndTime = request["end_time"];
            string clnameid = request["clnameid"];
            int PageSize = Convert.ToInt32(request["rows"]);
            int PageIndex = Convert.ToInt32(request["page"]);

            StringBuilder commandText = new StringBuilder();

            if (string.IsNullOrEmpty(clnameid) && string.IsNullOrEmpty(StartTime) && string.IsNullOrEmpty(EndTime))
            {

                commandText.Append("SELECT top " + PageSize + "  * FROM mg_Report_FTT WHERE id NOT IN(SELECT TOP (" + PageSize + "*(" + PageIndex + "-1)) id FROM  mg_Report_FTT ORDER BY id DESC) ORDER BY id DESC");
                FunSql.Init();
                DataTable resTable = FunSql.GetTable(commandText.ToString());
                int totalcount = FunSql.GetInt("select count(id) from mg_Report_FTT");
                string JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);

                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            else if (clnameid != "" && clnameid != null && StartTime != "" && EndTime != "")
            {
                commandText.Append("SELECT TOP " + PageSize + " * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ID) AS RowNumber,* FROM mg_Report_FTT ");
                commandText.Append(" WHERE cl_name like'%" + clnameid + "%' and cl_starttime>='" + StartTime + "' and cl_endtime<='" + EndTime + "'");//这里修改条件语句
                commandText.Append(" ) AS T  WHERE RowNumber > (" + PageSize + "*(" + PageIndex + "-1))");

                FunSql.Init();
                DataTable resTable = FunSql.GetTable(commandText.ToString());

                int totalcount = FunSql.GetInt("select count(id) from mg_Report_FTT  WHERE cl_name like'%" + clnameid + "%' and cl_starttime>='" + StartTime + "' and cl_endtime<='" + EndTime + "'");

                string JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);

                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            else if (string.IsNullOrEmpty(clnameid) && StartTime != "" && EndTime != "") 
            {
                commandText.Append("SELECT TOP " + PageSize + " * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ID) AS RowNumber,* FROM mg_Report_FTT ");
                commandText.Append(" WHERE cl_starttime>='" + StartTime + "' and cl_endtime<='" + EndTime + "'");//这里修改条件语句
                commandText.Append(" ) AS T  WHERE RowNumber > (" + PageSize + "*(" + PageIndex + "-1))");

                FunSql.Init();
                DataTable resTable = FunSql.GetTable(commandText.ToString());

                int totalcount = FunSql.GetInt("select count(id) from mg_Report_FTT  WHERE cl_starttime>='" + StartTime + "' and cl_endtime<='" + EndTime + "'");

                string JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);

                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            else if (clnameid != "" && StartTime == "" && EndTime == "")
            {
                commandText.Append("SELECT TOP " + PageSize + " * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ID) AS RowNumber,* FROM mg_Report_FTT ");
                commandText.Append(" WHERE cl_name like'%" + clnameid + "%'");//这里修改条件语句
                commandText.Append(" ) AS T  WHERE RowNumber > (" + PageSize + "*(" + PageIndex + "-1))");

                FunSql.Init();
                DataTable resTable = FunSql.GetTable(commandText.ToString());

                int totalcount = FunSql.GetInt("select count(id) from mg_Report_FTT  WHERE cl_name like'%" + clnameid + "%'");

                string JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);

                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            else
            {
                commandText.Append("SELECT top " + PageSize + "  * FROM mg_Report_FTT WHERE id NOT IN(SELECT TOP (" + PageSize + "*(" + PageIndex + "-1)) id FROM  mg_Report_FTT ORDER BY id DESC) ORDER BY id DESC");
                FunSql.Init();
                DataTable resTable = FunSql.GetTable(commandText.ToString());
                int totalcount = FunSql.GetInt("select count(id) from mg_Report_FTT");
                string JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);

                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);
                context.Response.End();
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
}