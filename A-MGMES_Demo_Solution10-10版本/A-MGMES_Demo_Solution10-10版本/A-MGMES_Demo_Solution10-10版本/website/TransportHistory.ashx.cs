using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace website
{
    /// <summary>
    /// TransportHistory 的摘要说明
    /// </summary>
    public class TransportHistory : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string OrderCode = request["OrderCode"];
            string CarType = request["CarType"];
            string Worker = request["Worker"];
            string TransportType = request["TransportType"];

            int SortFlag = Convert.ToInt32(request["SortFlag"]);
            int PageSize = Convert.ToInt32(request["rows"]);
            int PageIndex = Convert.ToInt32(request["page"]);
            string method = request["method"];

            if (string.IsNullOrEmpty(OrderCode))
            {
                OrderCode = " 1=1 ";
            }
            else
            {
                OrderCode = " 订单编码 = '" + OrderCode + "' ";
            }

            if (string.IsNullOrEmpty(CarType))
            {
                CarType = " 1=1 ";
            }
            else
            {
                //CarType = "( part1 = '" + CarType + "' or part2 = '" + CarType + "' or part3 = '" + CarType + "' or part4 = '" + CarType + "' or part5 = '" + CarType + "')";
                CarType = " 车型ID = '" + CarType + "' ";
            }

            if (string.IsNullOrEmpty(Worker))
            {
                Worker = " 1=1 ";
            }
            else
            {
                Worker = "( op_name = '" + Worker + "' or Expr1 = '" + Worker + "' or Expr2 = '" + Worker + "'or Expr3 = '" + Worker + "' or Expr4 = '" + Worker + "')";
            }

            if (string.IsNullOrEmpty(TransportType))
            {
                TransportType = " 1=1 ";
            }
            else
            {
                TransportType = " 订单类型 = '" + TransportType + "' ";
            }
            if (string.IsNullOrWhiteSpace(method))
            {
                StringBuilder commandText = new StringBuilder();
                commandText.Append("SELECT TOP " + PageSize + " ID,订单类型,订单编码,下单时间,车型ID,车型,订单状态,前排故障历史,前排是否修复,主驾,主驾气囊,副驾,副驾气囊,[40%] 百分之40,[60%] 百分之60,[100%] 百分之100,卷收器,MainOrderID,VINNumber,回写SAP FROM (SELECT ROW_NUMBER() OVER (ORDER BY ID) AS RowNumber,* FROM ViewOrders ");
                commandText.Append(" WHERE " + OrderCode + " and " + CarType + " and " + Worker + " and " + TransportType);//这里修改条件语句
                commandText.Append(" ) AS T  WHERE RowNumber > (" + PageSize + "*(" + PageIndex + "-1))");
                FunSql.Init();
                DataTable resTable = FunSql.GetTable(commandText.ToString());
                int totalcount = FunSql.GetInt("select count(0) from ViewOrders WHERE " + OrderCode + " and " + CarType + " and " + Worker + " and " + TransportType);
                string JsonStr = FunCommon.DataTableToJson2(totalcount, resTable);
                context.Response.ContentType = "text/plain";
               
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            else
            {
                //导出excel
                string sql = "SELECT ID,订单类型,订单编码,下单时间,车型ID,车型,订单状态,前排故障历史,前排是否修复,主驾,主驾气囊,副驾,副驾气囊,[40%] 百分之40,[60%] 百分之60,[100%] 百分之100,卷收器,MainOrderID,VINNumber,回写SAP FROM ViewOrders WHERE " + OrderCode + "";
                FunSql.Init();
                DataTable resTable2 = FunSql.GetTable(sql);
                for (int i = 0; i < resTable2.Rows.Count; i++)
                {
                    resTable2.Rows[i]["主驾"] = "\t" + resTable2.Rows[i]["主驾"].ToString();
                    resTable2.Rows[i]["副驾"] = "\t" + resTable2.Rows[i]["副驾"].ToString();
                    resTable2.Rows[i]["百分之40"] = "\t" + resTable2.Rows[i]["百分之40"].ToString();
                    resTable2.Rows[i]["百分之60"] = "\t" + resTable2.Rows[i]["百分之60"].ToString();
                    resTable2.Rows[i]["百分之100"] = "\t" + resTable2.Rows[i]["百分之100"].ToString();
                }
                string JsonStr = "[]";
                try
                {
                    ExcelHelper.ExportDTtoExcel(resTable2, "HeaderText", HttpContext.Current.Request.MapPath("~/App_Data/发运历史报表.xlsx"));
                    JsonStr = "true";
                }
                catch
                {
                    JsonStr = "false";
                }
                context.Response.ContentType = "jsons";

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