using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DbUtility;
using System.Data;
using System.Data.SqlClient;
namespace Dal
{
    public class mg_alarmDAL
    {
        public static List<mg_alarm> getfsabydateNew(string dtb, string dta)
        {
            List<mg_alarm> allal = new List<mg_alarm>();
            string sql1 = @"select distinct AlarmStation, 
case when AlarmType = 1 then 5 when AlarmType = 2 then 4
when AlarmType = 3 then 3  when AlarmType = 4 then 1
when AlarmType = 5 then 2 when AlarmType = 6 then 6 else 0 end as AlarmType
from mg_Alarm where [AlarmStartTime]>='" + dtb + "' and [AlarmEndTime]<='" + dta + "'  and AlarmType != 4 ";
            string sql = @"select a.AlarmStation as AlarmStation,max(a.AlarmType)as AlarmType  from ( " + sql1 + " ) a group by a.AlarmStation";
            SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            while (dr.Read())
            {
                mg_alarm alarm = new mg_alarm();
                alarm.AlarmStation = dr["AlarmStation"].ToString();
                alarm.AlarmType = (int)dr["AlarmType"];
                allal.Add(alarm);
            }
            dr.Close();
            return allal;
        }
        public static DataTable getfsabydateexcel(string dtb, string dta)
        {
            string sql = "select distinct case when AlarmType = 1 then '物料' when AlarmType = 2 then '质量'  when AlarmType = 3 then '维修'  when AlarmType = 4 then '超时' when AlarmType = 5 then '生产' when AlarmType = 6 then '急停' else '正常' end as Status,AlarmStation,AlarmStartTime,AlarmEndTime,StartOrderNo,EndOrderNo,case when IsSolve = 1 then '已解决' else '未解决' end as IsPass from mg_Alarm where (([AlarmStartTime]>='" + dtb + "' and [AlarmEndTime]<='" + dta + "') and (StartOrderNo is not null and StartOrderNo <> '') and EndOrderNo is not null and EndOrderNo <> '' )order by AlarmStation ,AlarmStartTime";
            DataTable t = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            return t;
        }
    }
}
