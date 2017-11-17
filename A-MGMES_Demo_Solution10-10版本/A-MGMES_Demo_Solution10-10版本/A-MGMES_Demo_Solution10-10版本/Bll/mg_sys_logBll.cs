using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

namespace Bll
{
    public class mg_sys_logBll
    {
        public static string getTorqueAndAngleInfo(string fl_id, string st_no, string part_no)
        {
            List<mg_sys_log> result = mg_sys_LogDal.getTorqueAndAngleInfo(fl_id, st_no, part_no);

            return JSONTools.ScriptSerialize<List<mg_sys_log>>(result);

        }

        public static string getfl_idList()
        {
            List<object> result = mg_sys_LogDal.getfl_idList();
            return JSONTools.ScriptSerialize(result);
        }

        public static string getst_idList(string fl_id)
        {
            List<object> result = mg_sys_LogDal.getst_idList(fl_id);
            return JSONTools.ScriptSerialize(result);
        }

        public static string getpart_idList(string fl_id, string st_no)
        {
            List<object> result = mg_sys_LogDal.getpart_idList(fl_id, st_no);
            return JSONTools.ScriptSerialize(result);
        }

        //检测返修报表中的，按时间筛选下的 订单号
        public static string getalldingdanhao(string StartTime, string EndTime)
        {
            List<object> result = mg_sys_LogDal.getalldingdanhaohao(StartTime, EndTime);
            return JSONTools.ScriptSerialize(result);
        }
    }
}
