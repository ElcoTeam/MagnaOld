using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using System.Data;
namespace Bll
{
    public class mg_sys_logBll
    {
        public static int InsertLog(mg_sys_log model)
        {
            return mg_sys_LogDal.InsertLog(model);
        }
        public static DataTable GetTableByID(string ID)
        {
            return mg_sys_LogDal.GetTableByID(ID);
        }
        public static string getList(int Pagesize,int StartIndex, int EndIndex,string SortFlag, string sortOrder,string wherestr)
        {
            return mg_sys_LogDal.getList(Pagesize,StartIndex, EndIndex, SortFlag, sortOrder, wherestr);
        }
        public static DataTable getList(int Pagesize, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr,out int total)
        {
            return mg_sys_LogDal.getList(Pagesize, StartIndex, EndIndex, SortFlag, sortOrder, wherestr,out total);
        }
        public static string getTorqueAndAngleInfo(string fl_id, string st_no, string part_no,string starttime, string endtime)
        {
            List<mg_sys_log> result = mg_sys_LogDal.getTorqueAndAngleInfo(fl_id, st_no, part_no, starttime, endtime);

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
        public static string getst_idListForTorque(string fl_id)
        {
            List<object> result = mg_sys_LogDal.getst_idListForTorque(fl_id);
            return JSONTools.ScriptSerialize(result);
        }
        public static string getst_idListForCheck(string fl_id)
        {
            List<object> result = mg_sys_LogDal.getst_idListForCheck(fl_id);
            return JSONTools.ScriptSerialize(result);
        }
        public static string getst_idListForStep(string fl_id)
        {
            List<object> result = mg_sys_LogDal.getst_idListForStep(fl_id);
            return JSONTools.ScriptSerialize(result);
        }
        public static string getst_idListForTime(string fl_id)
        {
            List<object> result = mg_sys_LogDal.getst_idListForTime(fl_id);
            return JSONTools.ScriptSerialize(result);
        }
        public static string getst_idListForVolume(string fl_id)
        {
            List<object> result = mg_sys_LogDal.getst_idListForVolume(fl_id);
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
