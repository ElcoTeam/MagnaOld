using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace website
{
    /// <summary>
    /// Services1005_CauseOfFailure 的摘要说明
    /// </summary>

    public class Services1005_CauseOfFailure : IHttpHandler
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

                case "AddCauseOfFailure":
                    AddCauseOfFailure();
                    break;
                case "SelectCauseOfFailure":
                    SelectCauseOfFailure();
                    break;
                //case "EditCauseOfFailure":
                //    EditCauseOfFailure();
                //    break;
                case "DelectCauseOfFailure":
                    DelectCauseOfFailure();
                    break;
            }
        }
        void AddCauseOfFailure()    //增加故障原因
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string ID = request.Params["ID"];
            string Name = request.Params["Name"];
            string Code = request.Params["Code"];
            if (ID == "") //添加
            {
                string sql = "INSERT INTO BadReason (Name, Code) VALUES ('" + Name + "', '" + Code + "')";
                int a = FunSql.Exec(sql);
                string ss;
                if (a == 0)
                {
                    ss = "True";
                }
                else
                {
                    ss = "false";
                }
                string sql1 = "select * from BadReason";
                FunSql.Init();
                DataTable ResTable1 = FunSql.GetTable(sql1);
                string json = FunCommon.DataTableToJson(ResTable1);
                string josn1 = "{\"Result\":\"" + ss + "\",\"Data\":" + json + "}";
                Response.Write(josn1);
            }
            else  //编辑
            {
                string sql = "UPDATE BadReason SET Name = '" + Name + "', Code = '" + Code + "' WHERE ID = '" + ID + "'";
                int a = FunSql.Exec(sql);
                string ss;
                if (a == 0)
                {
                    ss = "True";
                }
                else
                {
                    ss = "false";
                }
                string sql1 = "select * from BadReason";
                FunSql.Init();
                DataTable ResTable1 = FunSql.GetTable(sql1);
                string json = FunCommon.DataTableToJson(ResTable1);
                string josn1 = "{\"Result\":\"" + ss + "\",\"Data\":" + json + "}";
                Response.Write(josn1);
            }

            Response.End();
        }
        void SelectCauseOfFailure()    //查询故障原因     
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string sql = "select * from BadReason";
            FunSql.Init();
            DataTable ResTable = FunSql.GetTable(sql);
            string json = FunCommon.DataTableToJson(ResTable);
            Response.Write(json);
            Response.End();
        }
        void EditCauseOfFailure()    //修改故障原因
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string ID = request.Params["ID"];
            string Name = request.Params["Name"];
            string Code = request.Params["Code"];
            string sql = "UPDATE BadReason SET Name = '" + Name + "', Code = '" + Code + "' WHERE ID = '" + ID + "'";
            int a = FunSql.Exec(sql);
            string ss;
            if (a == 0)
            {
                ss = "True";
            }
            else
            {
                ss = "false";
            }
            string sql1 = "select * from BadReason";
            DataTable ResTable1 = FunSql.GetTable(sql1);
            string json = FunCommon.DataTableToJson(ResTable1);
            string josn1 = "{\"Result\":\"" + ss + "\",\"Data\":" + json + "}";
            Response.Write(josn1);
            Response.End();
        }
        void DelectCauseOfFailure()    //删除故障原因
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string ID = request["ID"];
            string sql = "DELETE FROM BadReason WHERE ID = '" + ID + "' ";
            int a = FunSql.Exec(sql);
            string ss;
            if (a == 0)
            {
                ss = "True";
            }
            else
            {
                ss = "false";
            }
            string sql1 = "select * from BadReason";
            FunSql.Init();
            DataTable ResTable1 = FunSql.GetTable(sql1);
            string json = FunCommon.DataTableToJson(ResTable1);
            if (json == "]")
            {
                json = "{}";
            }
            string josn1 = "{\"Result\":\"" + ss + "\",\"Data\":" + json + "}";
            Response.Write(josn1);
            Response.End();
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