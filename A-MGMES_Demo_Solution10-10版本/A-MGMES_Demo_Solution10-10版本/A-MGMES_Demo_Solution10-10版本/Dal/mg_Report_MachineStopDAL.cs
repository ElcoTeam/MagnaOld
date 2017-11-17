using DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
    /// <summary>
    /// 设备停机记录
    /// lx 2017-07-07
    /// </summary>
    public class mg_Report_MachineStopDAL
    {
        public static DataTable GetAllData()
        {
            string sql = @"SELECT ROW_NUMBER() OVER (ORDER BY start_time desc) AS orderNO
                                  ,[machineStop_id]
                                  ,[st_no]
                                  ,[machineStop_reason]
                                  ,[start_time]
                                  ,[end_time]
                                  ,[memo]
                              FROM [mg_Report_MachineStop]
                              order by start_time desc";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 根据条件查询记录
        /// lx 2017-07-07
        /// </summary>
        /// <param name="st_no"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public static DataTable GetDataByCondition(string st_no,string starttime,string endtime, string reason, string memo)
        {
            SqlParameter[] param = new SqlParameter[3];

            string sql = @"SELECT ROW_NUMBER() OVER (ORDER BY start_time desc) AS orderNO
                                  ,[machineStop_id]
                                  ,[st_no]
                                  ,[machineStop_reason]
                                  ,[start_time]
                                  ,[end_time]
                                  ,[memo]
                              FROM [mg_Report_MachineStop]
                              where 1=1
                              ";

            if (!string.IsNullOrEmpty(st_no))
            {
                sql += @" and st_no = @st_no ";
                param[0] = new SqlParameter("@st_no", st_no);
            }
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                sql += @" and start_time >= @start_time and end_time <= @end_time ";
                param[1] = new SqlParameter("@start_time", starttime);
                param[2] = new SqlParameter("@end_time", endtime);
            }

            if (!string.IsNullOrEmpty(reason))
            {
                sql += @" and machineStop_reason like '%" + reason + @"%' ";
            }

            if (!string.IsNullOrEmpty(memo))
            {
                sql += @" and memo like '%"+memo+"%' ";
            }

            sql += @" order by start_time desc";

            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, param);
        }

        public static int AddMachineStop(string st_no, string machineStop_reason, string starttime, string endtime, string memo)
        {
            string sql = @"INSERT INTO [mg_Report_MachineStop]
                                           ([machineStop_id]
                                           ,[st_no]
                                           ,[machineStop_reason]
                                           ,[start_time]
                                           ,[end_time]
                                           ,[memo])
                                     VALUES
                                           (newid()
                                           ,@st_no
                                           ,@machineStop_reason
                                           ,@start_time
                                           ,@end_time
                                           ,@memo)";

            SqlParameter[] param = { new SqlParameter("@st_no", st_no), new SqlParameter("@machineStop_reason", machineStop_reason),
                                   new SqlParameter("@start_time", starttime),new SqlParameter("@end_time", endtime),
                                   new SqlParameter("@memo", memo)
                                   };

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, param);
        }

        public static int DelByID(string id)
        {
            string sql = @"delete from [mg_Report_MachineStop] where machineStop_id = @machineStop_id";

            SqlParameter[] param = { new SqlParameter("@machineStop_id", new Guid(id))
                                   };

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, param);
        }

        public static int UpdateByID(string id, string st_no, string machineStop_reason, string starttime, string endtime, string memo)
        {
            string sql = @"update [mg_Report_MachineStop] set 
                                           [st_no] = @st_no
                                           ,[machineStop_reason] = @machineStop_reason
                                           ,[start_time] =@start_time
                                           ,[end_time] =@end_time
                                           ,[memo] =@memo
                                     where machineStop_id = @machineStop_id
                            ";

            SqlParameter[] param = { new SqlParameter("@st_no", st_no), new SqlParameter("@machineStop_reason", machineStop_reason),
                                   new SqlParameter("@start_time", starttime),new SqlParameter("@end_time", endtime),
                                   new SqlParameter("@memo", memo),new SqlParameter("@machineStop_id", id)
                                   };

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, param);
        }
    }
}
