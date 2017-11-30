using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbUtility;
namespace Dal
{
  public  class Time_ReportDal
    {
      public static DataTable TimeProducts(string dti1, string dti2, int flag, string st_no)  //时间报表的
      {
          DataTable ResTable;
          if (flag == 1 && !string.IsNullOrEmpty(st_no))  //按小时                   前两个是工站不为空的，后两个是工站位空的，也就是只选时间
          {
             
              string SqlStr1 = "select sys_id 序号,or_no 订单号,st_no 工站,station_startTime 开始时间,station_endTime 结束时间,station_TimeSpan 耗时 from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 and st_no = '" + st_no + "' order by sys_id";
             
              
              ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr1, null);
            
          }
          else if (flag == 2 && !string.IsNullOrEmpty(st_no))   //按天
          {
             
              string SqlStr1 = "select sys_id 序号,or_no 订单号,st_no 工站,station_startTime 开始时间,station_endTime 结束时间,station_TimeSpan 耗时 from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 and st_no = '" + st_no + "' order by sys_id";
              
             
              ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr1, null);
            

          }
          else if (string.IsNullOrEmpty(st_no) && flag == 1)
          {
              
              string SqlStr1 = "select sys_id 序号,or_no 订单号,st_no 工站,station_startTime 开始时间,station_endTime 结束时间,station_TimeSpan 耗时 from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 order by sys_id";
             
              ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr1, null);
            
          }
          else
          {
           
              string SqlStr1 = "select sys_id 序号,or_no 订单号,st_no 工站,station_startTime 开始时间,station_endTime 结束时间,station_TimeSpan 耗时 from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 order by sys_id";

              ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr1, null);
             
          }
          return ResTable;
      }
    }
}
