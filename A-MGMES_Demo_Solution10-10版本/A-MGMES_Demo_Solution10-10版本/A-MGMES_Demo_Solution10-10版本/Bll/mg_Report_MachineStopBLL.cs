using Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Bll
{
    /// <summary>
    /// 设备停机记录
    /// </summary>
    public class mg_Report_MachineStopBLL
    {
        /// <summary>
        /// 查询所有记录
        /// lx 2017-07-07
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllData()
        {
            return mg_Report_MachineStopDAL.GetAllData();
        }

        /// <summary>
        /// 根据条件查询记录
        /// lx 2017-07-07
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataByCondition(string st_no, string starttime, string endtime, string reason, string memo)
        {
            return mg_Report_MachineStopDAL.GetDataByCondition(st_no, starttime, endtime, reason, memo);
        }

        /// <summary>
        /// 新增
        /// lx 2017-07-07
        /// </summary>
        /// <param name="st_no"></param>
        /// <param name="machineStop_reason"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public static bool AddMachineStop(string st_no, string machineStop_reason, string starttime, string endtime, string memo)
        {
            return mg_Report_MachineStopDAL.AddMachineStop(st_no, machineStop_reason, starttime, endtime, memo) > 0 ? true : false;
        }

        /// <summary>
        /// 删除
        /// lx 2017-07-07
        /// </summary>
        /// <param name="posi_id"></param>
        /// <returns></returns>
        public static bool DelByID(string id)
        {
            return mg_Report_MachineStopDAL.DelByID(id) > 0 ? true : false;
        }

        /// <summary>
        /// 编辑
        /// lx 2017-07-07
        /// </summary>
        /// <param name="id"></param>
        /// <param name="st_no"></param>
        /// <param name="machineStop_reason"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public static bool UpdateByID(string id, string st_no, string machineStop_reason, string starttime, string endtime, string memo)
        {
            return mg_Report_MachineStopDAL.UpdateByID(id, st_no, machineStop_reason, starttime, endtime, memo) > 0 ? true : false;
        }
    }
}
