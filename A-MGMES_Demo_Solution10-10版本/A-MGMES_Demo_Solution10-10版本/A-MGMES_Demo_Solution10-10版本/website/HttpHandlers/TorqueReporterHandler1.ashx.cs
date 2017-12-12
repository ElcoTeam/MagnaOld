using Bll;
using DbUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Tools;

namespace website.HttpHandlers
{
    /// <summary>
    /// TorqueReporterHandler 的摘要说明
    /// </summary>
    public class TorqueReporterHandler1 : IHttpHandler
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
                case "get_fl_list1":
                    //context.Response.Write(mg_sys_logBll1.getfl_idList1());
                    fl();
                    break;
                case "get_st_list1":
                    st();
                    break;
                case "get_part_list1":
                    part();
                    break;
                case "get_order_list":
                    order();
                    break;
                default:
                    Select();
                    break;
            }
            context.Response.End();
        }
        void Select()    //查询扭矩
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string fl_id = request.Params["fl_id"];
            string st_id = request.Params["st_id"];
            string part_no = request.Params["part_no"];

            List<mg_sys_log> result = new List<mg_sys_log>();
      
            StringBuilder sql = new StringBuilder(@"SELECT fl_id,fl_name,st_no,part_no,step_order as step, AngleResult as angle, TorqueResult as torque FROM  dbo.View_mg_sys_log where 1=1 and Len(AngleResult) > 0");
            List<SqlParameter> parameters = new List<SqlParameter>();
            //System.Diagnostics.Debug.Write();
            if (string.IsNullOrEmpty(fl_id) == false)
            {
                sql.Append(" and fl_id=@fl_id");
                parameters.Add(new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id });
            }
            if (string.IsNullOrEmpty(st_id) == false)
            {
                sql.Append(" and st_id=@st_id");
                parameters.Add(new SqlParameter("@st_id", SqlDbType.NVarChar) { Value = st_id });
            }
            if (string.IsNullOrEmpty(part_no) == false)
            {
                sql.Append(" and part_no=@part_no");
                parameters.Add(new SqlParameter("@part_no", SqlDbType.NVarChar) { Value = part_no });
            }
            sql.Append(" order by fl_name, st_no, part_no, step");
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql.ToString(), parameters.ToArray());
            foreach (DataRow row in table.Rows)
            {
                decimal TorqueResult = 0;
                string str = Convert.ToString(row["torque"].ToString().Replace("Nm", ""));
                if (!String.IsNullOrEmpty(str))
                {
                    TorqueResult = Convert.ToDecimal(str);
                }
                decimal AngleResult = 0;
                str = Convert.ToString(row["angle"].ToString().Replace("°", ""));
                if (!String.IsNullOrEmpty(str))
                {
                    AngleResult = Convert.ToDecimal(str);
                }


                result.Add(new mg_sys_log()
                {
                    fl_name = Tools.DataHelper.GetCellDataToStr(row, "fl_name"),
                    st_no = Tools.DataHelper.GetCellDataToStr(row, "st_no"),
                    part_no = Tools.DataHelper.GetCellDataToStr(row, "part_no"),
                    step_order = Tools.NumericParse.StringToInt(row["step"] as string),
                    TorqueResult = TorqueResult.ToString(),
                    AngleResult = AngleResult.ToString()
                });
            }
            //return result;
            List<mg_sys_log> result1 = result;

            //return JSONTools.ScriptSerialize<List<mg_sys_log>>(result);

            string a = JSONTools.ScriptSerialize<List<mg_sys_log>>(result1);
            Response.Write(a);
            Response.End();
        }
        void fl()   //查询流水线
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string sql = "select fl_id, fl_name from dbo.mg_FlowLine order by fl_name";
          
            DataTable ResTable3 = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            //string json = FunCommon.DataTableToJson(ResTable1);
            if (ResTable3 == null)
            {
                string json = JSONTools.ObjectToJSON("");
                Response.Write(json);
                Response.End();
            }
            else
            {
                string json = JSONTools.DataTableToJSON(ResTable3);
                Response.Write(json);
                Response.End();
            }
            
        }
        void order()   //检测返修页面查询全部订单号
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string StartTime = request.Params["StartTime"];
            string EndTime = request.Params["EndTime"];
            string sql = "select distinct  REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') as OrderNo from mg_Test_Part_Record a where(a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "') order by OrderNo";

            DataTable ResTable1 = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            string json = FunCommon.DataTableToJson(ResTable1);
            Response.Write(json);
            Response.End();
        }
        void st()   //查询工位
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string fl_id = request.Params["fl_id"];
            string sql = "select distinct a.st_no,a.st_id from mg_station a LEFT JOIN View_mg_sys_log b ON a.st_no=b.st_no  where  a.fl_id='" + fl_id + "' and len(b.AngleResult) > 0 order by a.st_no";
          
            DataTable ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);

            if (ResTable == null)
            {
                string json = JSONTools.ObjectToJSON("");
                Response.Write(json);
                Response.End();
            }
            else
            {
                string json = JSONTools.DataTableToJSON(ResTable);
                Response.Write(json);
                Response.End();
            }
            
           
        }
        void part() //查询部件号
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string fl_id = request.Params["fl_id"];
            string st_id = request.Params["st_id"];
            string sql = "select distinct part_no from View_mg_sys_log where fl_id='"+ fl_id + "' and st_id='" + st_id + "'";
          
            DataTable ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);

            if (ResTable == null)
            {
                string json = JSONTools.ObjectToJSON("");
                Response.Write(json);
                Response.End();
            }
            else
            {
                string json = JSONTools.DataTableToJSON(ResTable);
                Response.Write(json);
                Response.End();
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