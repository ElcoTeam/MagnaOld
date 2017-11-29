using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

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
            string sql1 = "select * from mg_sys_log where sys_id = '" + sys_id + "'";
            FunSql.Init();
            DataTable ResTable = FunSql.GetTable(sql1);     //根据sys_id拿到选中的该记录
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
            string sql2 = "INSERT INTO mg_sys_log (AngleResult,TorqueResult,scanCode,MenderName,ReviseTime,op_id,op_name,fl_id,fl_name,st_id,st_no,PartOrderID,or_no,part_no,step_order,step_startTime,step_endTime,step_duringtime,scanResult,sys_desc,ScrewCount) VALUES ('" + AngleResult + "', '" + TorqueResult + "', '" + scanCode + "', '" + name + "', '" + t + "', '" + op_id + "', '" + op_name + "', '" + fl_id + "', '" + fl_name + "', '" + st_id + "', '" + st_no + "', '" + PartOrderID + "', '" + or_no + "', '" + part_no + "', '" + step_order + "', '" + step_startTime + "', '" + step_endTime + "', '" + step_duringtime + "', '" + scanResult + "', '" + sys_desc + "', '" + ScrewCount + "')";
            int a = FunSql.Exec(sql2);
            string ss;
            if (a == 0)
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