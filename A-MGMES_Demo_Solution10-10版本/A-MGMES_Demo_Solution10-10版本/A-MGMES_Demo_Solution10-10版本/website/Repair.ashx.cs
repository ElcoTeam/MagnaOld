using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using DbUtility;
namespace website
{
    /// <summary>
    /// Repair 的摘要说明
    /// </summary>
    public class Repair : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string StartTime = request["StartTime"];    //开始时间
            string EndTime = request["EndTime"];    //结束时间
            string Or_no = request["Or_no"];    //订单号
            string St_no = request["St_no"];    //工位号

            int SortFlag = Convert.ToInt32(request["SortFlag"]);
            int PageSize = Convert.ToInt32(request["rows"]);
            int PageIndex = Convert.ToInt32(request["page"]);

            if (string.IsNullOrEmpty(Or_no))    //订单
            {
                Or_no = " 1=1 ";
            }
            else
            {
                Or_no = " OrderNo = '" + Or_no + "' ";
            }
            if (string.IsNullOrEmpty(St_no))    //工位
            {
                St_no = " 1=1 ";
            }
            else
            {
                St_no = " StationNo = '" + St_no + "' ";
            }

            StringBuilder commandText = new StringBuilder();
            commandText.Append("SELECT TOP " + PageSize + " * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ID) AS RowNumber,* FROM ViewBigScreen3 ");
            commandText.Append(" WHERE " + Or_no + " and " + St_no );//这里修改条件语句
            commandText.Append(" ) AS T  WHERE RowNumber > (" + PageSize + "*(" + PageIndex + "-1))");

            DataTable resTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, commandText.ToString(), null);
            string sql = " select count(0) from ViewBigScreen3 WHERE " + Or_no + " and " + St_no + " ";
            int totalcount = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);

            string JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);
            //string JsonStr = "[]";
            //if (resTable != null)
            //JsonStr = FunCommon.DataTableToJson(resTable);

            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonStr);
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