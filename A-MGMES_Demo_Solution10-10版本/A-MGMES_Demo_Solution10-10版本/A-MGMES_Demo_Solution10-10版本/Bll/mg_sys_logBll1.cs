using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

namespace Bll
{
    public class mg_sys_logBll1
    {
        public static string getTorqueAndAngleInfo1(string fl_id, string st_no, string part_no)
        {
            List<mg_sys_log> result = mg_sys_LogDal1.getTorqueAndAngleInfo1(fl_id, st_no, part_no);

            return JSONTools.ScriptSerialize<List<mg_sys_log>>(result);

        }

        public static string getfl_idList1()
        {
            List<object> result = mg_sys_LogDal1.getfl_idList1();
            return JSONTools.ScriptSerialize(result);
        }

        public static string getst_idList1(string fl_id)
        {
            List<object> result = mg_sys_LogDal1.getst_idList1(fl_id);
            return JSONTools.ScriptSerialize(result);
        }

        public static string getpart_idList1(string fl_id, string st_no)
        {
            List<object> result = mg_sys_LogDal1.getpart_idList1(fl_id, st_no);
            return JSONTools.ScriptSerialize(result);
        }
    }
}
