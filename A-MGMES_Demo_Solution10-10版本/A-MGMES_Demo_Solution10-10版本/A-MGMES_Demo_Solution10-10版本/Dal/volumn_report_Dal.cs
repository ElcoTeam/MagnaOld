using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbUtility;
namespace Dal
{
   public class volumn_report_Dal
    {
       public static DataTable GetAddupProducts(string method, string dti1, string dti2, int flag, string st_no, out DataTable outtable)//产量报表
       {
           
               string SqlStr = "";
               if (string.IsNullOrEmpty(method))
               {
                   if (flag == 1)
                   {
                       if (st_no == "" || st_no == null)
                       {
                           SqlStr = "select CONVERT(varchar(13),cast(endTime as datetime),120) as dayTime,count(*) as c from(select  right(or_no,10) as orderCode,COUNT(right(or_no,10)) as counts,Max(station_endTime) as endTime from dbo.View_mg_station_log  where or_no<>'' and (st_no='FSA200' or  st_no='RSC040' or st_no='RSB070')  and station_startTime>='" + dti1 + "' and station_endTime <='" + dti2 + "' group by right(or_no,10))a where counts >= 5 group by CONVERT(varchar(13),cast(endTime as datetime),120) order by CONVERT(varchar(13),cast(endTime as datetime),120)";
                           outtable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);

                       }
                       else
                       {
                           SqlStr = "select dayTime,COUNT(*) as c  from (select CONVERT(varchar(13),cast(station_endTime as datetime),120) as dayTime from dbo.View_mg_station_log where station_startTime >= '" + dti1 + "' and station_endTime <= '" + dti2 + "' and st_no = '" + st_no + "') a group by dayTime order by dayTime";
                           outtable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);

                       }
                   }
                   else
                   {
                       if (st_no == "" || st_no == null)
                       {
                           SqlStr = "select CONVERT(varchar(10),cast(endTime as datetime),120) as dayTime,count(*) as c from(select  right(or_no,10) as orderCode,COUNT(right(or_no,10)) as counts,Max(station_endTime) as endTime from dbo.View_mg_station_log  where or_no<>'' and (st_no='FSA200' or  st_no='RSC040' or st_no='RSB070')  and station_startTime>='" + dti1 + "' and station_endTime <='" + dti2 + "' group by right(or_no,10))a where counts >= 5 group by CONVERT(varchar(10),cast(endTime as datetime),120) order by CONVERT(varchar(10),cast(endTime as datetime),120)";
                           outtable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);

                       }
                       else
                       {   //全选
                           SqlStr = "select dayTime,COUNT(*) as c  from (select CONVERT(varchar(10),cast(station_endTime as datetime),120) as dayTime from dbo.View_mg_station_log where station_startTime >= '" + dti1 + "' and station_endTime <= '" + dti2 + "' and st_no = '" + st_no + "') a group by dayTime order by dayTime";
                           outtable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);

                       }
                   }


               }
               else
               {
                   if (flag == 1)
                   {
                       if (st_no == "" || st_no == null)
                       {

                           SqlStr = "select[sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],CONVERT(varchar(13),cast(endTime as datetime),120) as dayTime,count(*) as c from View_mg_station_log b,(select  right(or_no,10) as orderCode,COUNT(right(or_no,10)) as counts,Max(station_endTime) as endTime from dbo.View_mg_station_log  where or_no<>'' and (st_no='FSA200' or  st_no='RSC040'or st_no='RSB070')  and station_startTime>='" + dti1 + "' and station_endTime <='" + dti2 + "' group by right(or_no,10))a where counts >=  5 and right(b.or_no,10) = a.orderCode and CONVERT(varchar(13),cast(a.endTime as datetime),120) = CONVERT(varchar(13),cast(b.station_endTime as datetime),120)group by CONVERT(varchar(13),cast(endTime as datetime),120),[sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan] order by CONVERT(varchar(13),cast(endTime as datetime),120)";
                           outtable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                       }
                       else
                       {

                           SqlStr = "select [sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],dayTime,COUNT(*) as c  from (select CONVERT(varchar(13),cast(station_endTime as datetime),120) as dayTime , [sys_id][sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan]  from dbo.View_mg_station_log where station_startTime >= '" + dti1 + "' and station_endTime <= '" + dti2 + "' and st_no = '" + st_no + "') a group by  [sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],dayTime order by dayTime";
                           outtable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                       }
                   }
                   else
                   {
                       if (st_no == "" || st_no == null)
                       {

                           SqlStr = "select[sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],CONVERT(varchar(10),cast(endTime as datetime),120) as dayTime,count(*) as c from View_mg_station_log b,(select  right(or_no,10) as orderCode,COUNT(right(or_no,10)) as counts,Max(station_endTime) as endTime from dbo.View_mg_station_log  where or_no<>'' and (st_no='FSA200' or  st_no='RSC040'or st_no='RSB070')  and station_startTime>='" + dti1 + "' and station_endTime <='" + dti2 + "' group by right(or_no,10))a where counts >=  5 and right(b.or_no,10) = a.orderCode and CONVERT(varchar(10),cast(a.endTime as datetime),120) = CONVERT(varchar(10),cast(b.station_endTime as datetime),120)group by CONVERT(varchar(10),cast(endTime as datetime),120),[sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan] order by CONVERT(varchar(10),cast(endTime as datetime),120)";
                           outtable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                       }
                       else
                       {   //全选

                           SqlStr = "select [sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],dayTime,COUNT(*) as c  from (select CONVERT(varchar(10),cast(station_endTime as datetime),120) as dayTime , [sys_id][sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan]  from dbo.View_mg_station_log where station_startTime >= '" + dti1 + "' and station_endTime <= '" + dti2 + "' and st_no = '" + st_no + "') a group by  [sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],dayTime order by dayTime";
                           outtable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                       }
                   }


               }
               return outtable;
           
       }
    }
}
