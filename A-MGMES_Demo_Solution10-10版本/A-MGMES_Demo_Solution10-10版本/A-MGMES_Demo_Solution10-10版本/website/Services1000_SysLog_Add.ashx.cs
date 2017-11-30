using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using DbUtility;
using Bll;
using Model;
using Tools;
namespace website
{
    /// <summary>
    /// Services1000_SysLog_Add 的摘要说明
    /// </summary>
    public class Services1000_SysLog_Add : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string sys_id = request["step_id"];
            string AngleResult = request["AngleResult"];
            string TorqueResult = request["TorqueResult"];
            string scanCode = request["scanCode"];
            string name = request["edit_name"].Trim();
            DateTime dt = DateTime.Now;
            string t = dt.ToString("yyyy-MM-dd hh:mm:ss");

            DataTable ResTable = mg_sys_logBll.GetTableByID(sys_id);     //根据sys_id拿到选中的该记录 ;     //根据sys_id拿到选中的该记录
            string op_id = ResTable.Rows[0][1].ToString();  //拿到该记录的op_id的值   [op_name]，[fl_id]，[fl_name]，[st_id]，[st_no]，[PartOrderID]，[or_no]，[part_no]，[step_order]，[step_startTime]，[step_endTime]，[step_duringtime]，[sys_desc]，[ScrewCount]
            string op_name = ResTable.Rows[0][2].ToString();
            string fl_id = ResTable.Rows[0][3].ToString();
            string fl_name = ResTable.Rows[0][4].ToString();
            string st_id = ResTable.Rows[0][5].ToString();
            string st_no = ResTable.Rows[0][6].ToString();
            decimal PartOrderID = default(decimal);
            if (!string.IsNullOrEmpty(ResTable.Rows[0][7].ToString()))
            {
                PartOrderID = decimal.Parse(ResTable.Rows[0][7].ToString());
            }
            string or_no = ResTable.Rows[0][8].ToString();
            string part_no = ResTable.Rows[0][9].ToString();
            string step_order = ResTable.Rows[0][10].ToString();
            string step_startTime = ResTable.Rows[0][11].ToString();
            string step_endTime = ResTable.Rows[0][12].ToString();
            string step_duringtime = ResTable.Rows[0][13].ToString();
            string scanResult = ResTable.Rows[0][17].ToString();
            string sys_desc = ResTable.Rows[0][18].ToString();
            int ScrewCount = default(int);
            if (!string.IsNullOrEmpty(ResTable.Rows[0][19].ToString()))
            {
                ScrewCount = int.Parse(ResTable.Rows[0][19].ToString());
            }
            mg_sys_log model = new mg_sys_log();
            model.sys_id = sys_id;
            model.op_id = op_id;
            model.op_name = op_name;
            model.fl_id = fl_id;
            model.fl_name = fl_name;
            model.st_id = st_id;
            model.st_no = st_no;
            model.PartOrderID = PartOrderID.ToString();
            model.or_no = or_no;
            model.part_no = part_no;
            model.step_order = NumericParse.StringToInt(step_order);
            model.step_startTime = step_startTime;
            model.step_endTime = step_endTime;
            model.step_duringtime = step_duringtime;
            model.AngleResult = AngleResult;
            model.TorqueResult = TorqueResult;
            model.scanCode = scanCode;
            model.scanResult = scanResult;
            model.sys_desc = sys_desc;
            model.ScrewCount = ScrewCount.ToString();
            model.MenderName = name;
            model.ReviseTime = t;
            int a = mg_sys_logBll.InsertLog(model);
           
            string ss;
            if (a > 0)
            {
                ss = "true";
            }
            else
            {
                ss = "false";
            }
            string josn = "{\"Result\":\"" + ss + "\"}";
            context.Response.ContentType = "json";
            context.Response.Write(josn);
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