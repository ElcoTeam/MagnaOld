using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Model;
using System.Reflection;
using DBUtility;

namespace website
{
    public class DataReader
    {
        public static DataTable GetLogs(string Dti1, string Dti2, int PageSize, int PageIndex)
        {
            int TotalPage = PageSize * PageIndex;
            int CurrentPage = PageSize * (PageIndex - 1);
            string SqlStr = "SELECT top " + PageSize + "  * FROM mg_sys_log WHERE (cast(Step_StartTime as datetime) >= '" + Dti1 + "' and cast(step_endTime as datetime) <= '" + Dti2 + "') and sys_id NOT IN(SELECT TOP " + CurrentPage + " sys_id FROM  mg_sys_log where (cast(Step_StartTime as datetime) >='" + Dti1 + "' and cast(step_endTime as datetime) <= '" + Dti2 + "') ORDER BY fl_name,step_startTime,sys_id) ORDER BY fl_name,step_startTime,sys_id";
            FunSql.Init();
            DataTable ResTable = FunSql.GetTable(SqlStr);
            return ResTable;
        }
        public static DataTable GetLogs2(string AssemblyLine, string Station, string Dti1, string Dti2, int PageSize, int PageIndex, string OrderId)
        {   //可多了
            DataTable ResTable;
            int TotalPage = PageSize * PageIndex;
            int CurrentPage = PageSize * (PageIndex - 1);
            if (CurrentPage <= 0)
            {
                CurrentPage = 0;
            }
            if (!string.IsNullOrEmpty(Station) && !string.IsNullOrEmpty(Dti1))  //工位选，时间选
            {
                string SqlStr = "SELECT top " + PageSize + "  * FROM mg_sys_log WHERE (cast(Step_StartTime as datetime) >= '" + Dti1 + "' and cast(step_endTime as datetime) <= '" + Dti2 + "')and st_no='" + Station + "' and sys_id NOT IN(SELECT TOP " + CurrentPage + " sys_id FROM  mg_sys_log where (cast(Step_StartTime as datetime) >='" + Dti1 + "' and cast(step_endTime as datetime) <= '" + Dti2 + "')and st_no='" + Station + "' ORDER BY st_no,step_startTime,sys_id) ORDER BY st_no,step_startTime,sys_id";
                FunSql.Init();
                ResTable = FunSql.GetTable(SqlStr);
            }
            else if (!string.IsNullOrEmpty(Station) && string.IsNullOrEmpty(Dti1)) //工位选，时间不选
            {
                string SqlStr = "SELECT top " + PageSize + "  * FROM mg_sys_log WHERE st_no='" + Station + "' and sys_id NOT IN(SELECT TOP " + CurrentPage + " sys_id FROM  mg_sys_log where st_no='" + Station + "' ORDER BY st_no,sys_id) ORDER BY st_no,sys_id";
                FunSql.Init();
                ResTable = FunSql.GetTable(SqlStr);
            }
            else   //就选流水线
            {
                string SqlStr = "SELECT top " + PageSize + "  * FROM mg_sys_log where st_no like '" + AssemblyLine + "%' and sys_id NOT IN(SELECT TOP " + CurrentPage + " sys_id FROM  mg_sys_log where st_no like '" + AssemblyLine + "%' ORDER BY st_no,sys_id) ORDER BY st_no,sys_id";
                FunSql.Init();
                ResTable = FunSql.GetTable(SqlStr);
            }
            return ResTable;
        }

        public static DataTable GetOrderLogs(string Dti, int PageSize, int PageIndex, string SearchKey = null)
        {
            DataTable ResTable;
            int TotalPage = PageSize * PageIndex;
            int CurrentPage = PageSize * (PageIndex - 1);
            if (CurrentPage <= 0)
            {
                CurrentPage = 0;
            }
            string sql = "SELECT top " + PageSize + "  * FROM mg_sys_log WHERE or_no='" + Dti + "' and sys_id NOT IN(SELECT TOP " + CurrentPage + " sys_id FROM  mg_sys_log where or_no='" + Dti + "' ORDER BY sys_id) ORDER BY sys_id";   //按生成顺序排序
            FunSql.Init();
            ResTable = FunSql.GetTable(sql);
            return ResTable;
        }

        public static DataTable GetFTT(string cl_name, string starttime, string endtime, int PageSize, int PageIndex)
        {
            DataTable ResTable;

            int TotalPage = PageSize * PageIndex;
            int CurrentPage = PageSize * (PageIndex - 1);
            string sql = "";
            if (cl_name != "" && cl_name != null && starttime != "" && endtime != "")
            {
                sql = "SELECT top " + PageSize + "  * FROM mg_Report_FTT WHERE cl_name like '%" + cl_name + "%' and cl_starttime>= '" + starttime + "'   and cl_endtime<='" + endtime + "' and id NOT IN(SELECT TOP " + CurrentPage + " id FROM  mg_Report_FTT  ORDER BY id DESC) ORDER BY id DESC";
            }
            else if (starttime == "" && endtime == "" && cl_name != "")
            {
                sql = "SELECT top " + PageSize + "  * FROM mg_Report_FTT WHERE cl_name='" + cl_name + "'  and id NOT IN(SELECT TOP " + CurrentPage + " id FROM  mg_Report_FTT  ORDER BY id DESC) ORDER BY id DESC";
            }
            else if (starttime == "" && endtime == "" && cl_name == "")
            {

                sql = "SELECT top " + PageSize + "  * FROM mg_Report_FTT WHERE id NOT IN(SELECT TOP " + CurrentPage + " id FROM  mg_Report_FTT ORDER BY id DESC) ORDER BY id DESC";
            }
            else
            {
                sql = "SELECT top " + PageSize + "  * FROM mg_Report_FTT WHERE id NOT IN(SELECT TOP " + CurrentPage + " id FROM  mg_Report_FTT ORDER BY id DESC) ORDER BY id DESC";
            }
            FunSql.Init();
            ResTable = FunSql.GetTable(sql);
            return ResTable;
        }



        public static DataTable GetAddupProducts(string dti1, string dti2, int flag, string st_no, out DataTable outtable)//产量报表
        {
            DataTable ResTable;
            if (flag == 1)
            {
                if (st_no == "" || st_no == null)
                {
                    string SqlStr = "select CONVERT(varchar(13),cast(endTime as datetime),120) as dayTime,count(*) as c from(select  right(or_no,10) as orderCode,COUNT(right(or_no,10)) as counts,Max(station_endTime) as endTime from dbo.mg_station_log  where or_no<>'' and (st_no='FSA200' or  st_no='RSC040' or st_no='RSB070')  and station_startTime>='" + dti1 + "' and station_endTime <='" + dti2 + "' group by right(or_no,10))a where counts >= 5 group by CONVERT(varchar(13),cast(endTime as datetime),120) order by CONVERT(varchar(13),cast(endTime as datetime),120)";
                    FunSql.Init();
                    ResTable = FunSql.GetTable(SqlStr);
                    SqlStr = "select[sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],CONVERT(varchar(13),cast(endTime as datetime),120) as dayTime,count(*) as c from mg_station_log b,(select  right(or_no,10) as orderCode,COUNT(right(or_no,10)) as counts,Max(station_endTime) as endTime from dbo.mg_station_log  where or_no<>'' and (st_no='FSA200' or  st_no='RSC040'or st_no='RSB070')  and station_startTime>='" + dti1 + "' and station_endTime <='" + dti2 + "' group by right(or_no,10))a where counts >=  5 and right(b.or_no,10) = a.orderCode and CONVERT(varchar(13),cast(a.endTime as datetime),120) = CONVERT(varchar(13),cast(b.station_endTime as datetime),120)group by CONVERT(varchar(13),cast(endTime as datetime),120),[sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan] order by CONVERT(varchar(13),cast(endTime as datetime),120)";
                    FunSql.Init();
                    outtable = FunSql.GetTable(SqlStr);
                }
                else
                {
                    string SqlStr = "select dayTime,COUNT(*) as c  from (select CONVERT(varchar(13),cast(station_endTime as datetime),120) as dayTime from dbo.mg_station_log where station_startTime >= '" + dti1 + "' and station_endTime <= '" + dti2 + "' and st_no = '" + st_no + "') a group by dayTime order by dayTime";
                    ResTable = FunSql.GetTable(SqlStr);
                    SqlStr = "select [sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],dayTime,COUNT(*) as c  from (select CONVERT(varchar(13),cast(station_endTime as datetime),120) as dayTime , [sys_id][sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan]  from dbo.mg_station_log where station_startTime >= '" + dti1 + "' and station_endTime <= '" + dti2 + "' and st_no = '" + st_no + "') a group by  [sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],dayTime order by dayTime";
                    outtable = FunSql.GetTable(SqlStr);
                }
            }
            else
            {
                if (st_no == "" || st_no == null)
                {
                    string SqlStr = "select CONVERT(varchar(10),cast(endTime as datetime),120) as dayTime,count(*) as c from(select  right(or_no,10) as orderCode,COUNT(right(or_no,10)) as counts,Max(station_endTime) as endTime from dbo.mg_station_log  where or_no<>'' and (st_no='FSA200' or  st_no='RSC040' or st_no='RSB070')  and station_startTime>='" + dti1 + "' and station_endTime <='" + dti2 + "' group by right(or_no,10))a where counts >= 5 group by CONVERT(varchar(10),cast(endTime as datetime),120) order by CONVERT(varchar(10),cast(endTime as datetime),120)";
                    ResTable = FunSql.GetTable(SqlStr);
                    SqlStr = "select[sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],CONVERT(varchar(10),cast(endTime as datetime),120) as dayTime,count(*) as c from mg_station_log b,(select  right(or_no,10) as orderCode,COUNT(right(or_no,10)) as counts,Max(station_endTime) as endTime from dbo.mg_station_log  where or_no<>'' and (st_no='FSA200' or  st_no='RSC040'or st_no='RSB070')  and station_startTime>='" + dti1 + "' and station_endTime <='" + dti2 + "' group by right(or_no,10))a where counts >=  5 and right(b.or_no,10) = a.orderCode and CONVERT(varchar(10),cast(a.endTime as datetime),120) = CONVERT(varchar(10),cast(b.station_endTime as datetime),120)group by CONVERT(varchar(10),cast(endTime as datetime),120),[sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan] order by CONVERT(varchar(10),cast(endTime as datetime),120)";
                    FunSql.Init();
                    outtable = FunSql.GetTable(SqlStr);
                }
                else
                {   //全选
                    string SqlStr = "select dayTime,COUNT(*) as c  from (select CONVERT(varchar(10),cast(station_endTime as datetime),120) as dayTime from dbo.mg_station_log where station_startTime >= '" + dti1 + "' and station_endTime <= '" + dti2 + "' and st_no = '" + st_no + "') a group by dayTime order by dayTime";
                    //string SqlStr = "";
                    ResTable = FunSql.GetTable(SqlStr);
                    SqlStr = "select [sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],dayTime,COUNT(*) as c  from (select CONVERT(varchar(10),cast(station_endTime as datetime),120) as dayTime , [sys_id][sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan]  from dbo.mg_station_log where station_startTime >= '" + dti1 + "' and station_endTime <= '" + dti2 + "' and st_no = '" + st_no + "') a group by  [sys_id],[op_id],[op_name] ,[fl_id],[fl_name],[st_id] ,[st_no],[or_no] ,[part_no],[station_startTime] ,[station_endTime],[station_TimeSpan],dayTime order by dayTime";
                    outtable = FunSql.GetTable(SqlStr);
                }
            }

            return ResTable;
        }
        public static DataTable TimeProducts(string dti1, string dti2, int flag, string st_no)  //时间报表的
        {
            DataTable ResTable;
            if (flag == 1 && !string.IsNullOrEmpty(st_no))  //按小时                   前两个是工站不为空的，后两个是工站位空的，也就是只选时间
            {
                string SqlStr = "select or_no,station_TimeSpan from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 and st_no = '" + st_no + "' order by sys_id";
                FunSql.Init();
                ResTable = FunSql.GetTable(SqlStr);
                string SqlStr1 = "select sys_id 序号,or_no 订单号,st_no 工站,station_startTime 开始时间,station_endTime 结束时间,station_TimeSpan 耗时 from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 and st_no = '" + st_no + "' order by sys_id";
                FunSql.Init();
                DataTable ResTable1 = FunSql.GetTable(SqlStr1);
                ExcelHelper.ExportDTtoExcel(ResTable1, "", HttpContext.Current.Request.MapPath("~/App_Data/excel3012.xlsx"));
            }
            else if (!string.IsNullOrEmpty(st_no))   //按天
            {
                string SqlStr = "select or_no,station_TimeSpan from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 and st_no = '" + st_no + "' order by sys_id";
                FunSql.Init();
                ResTable = FunSql.GetTable(SqlStr);
                string SqlStr1 = "select sys_id 序号,or_no 订单号,st_no 工站,station_startTime 开始时间,station_endTime 结束时间,station_TimeSpan 耗时 from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 and st_no = '" + st_no + "' order by sys_id";
                FunSql.Init();
                DataTable ResTable1 = FunSql.GetTable(SqlStr1);
                ExcelHelper.ExportDTtoExcel(ResTable1, "", HttpContext.Current.Request.MapPath("~/App_Data/excel3012.xlsx"));
            }
            else if (string.IsNullOrEmpty(st_no) && flag == 1)
            {
                string SqlStr = "select or_no,station_TimeSpan from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 order by sys_id";
                FunSql.Init();
                ResTable = FunSql.GetTable(SqlStr);
                string SqlStr1 = "select sys_id 序号,or_no 订单号,st_no 工站,station_startTime 开始时间,station_endTime 结束时间,station_TimeSpan 耗时 from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 order by sys_id";
                FunSql.Init();
                DataTable ResTable1 = FunSql.GetTable(SqlStr1);
                ExcelHelper.ExportDTtoExcel(ResTable1, "", HttpContext.Current.Request.MapPath("~/App_Data/excel3012.xlsx"));
            }
            else
            {
                string SqlStr = "select or_no,station_TimeSpan from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 order by sys_id";
                FunSql.Init();
                ResTable = FunSql.GetTable(SqlStr);
                string SqlStr1 = "select sys_id 序号,or_no 订单号,st_no 工站,station_startTime 开始时间,station_endTime 结束时间,station_TimeSpan 耗时 from dbo.mg_station_log where cast(station_startTime as datetime) >= '" + dti1 + "' and cast(station_endTime as datetime) <= '" + dti2 + "' and station_TimeSpan < 500 order by sys_id";
                FunSql.Init();
                DataTable ResTable1 = FunSql.GetTable(SqlStr1);
                ExcelHelper.ExportDTtoExcel(ResTable1, "", HttpContext.Current.Request.MapPath("~/App_Data/excel3012.xlsx"));
            }
            return ResTable;
        }

        //public static List<mg_alarm> getfsa(string dti1, string dti2, DateTime StartTime)
        public static List<mg_alarm> getfsa()
        {
            List<mg_alarm> allal = new List<mg_alarm>();
            string SqlStr = SqlHelper.SqlConnString;
            SqlConnection conn = new SqlConnection(SqlStr);
            string sql = "";
            sql = "select distinct AlarmStation,AlarmType from mg_Alarm where IsSolve=0 and CONVERT(varchar(12) , AlarmStartTime, 23 ) =  CONVERT(varchar(12) , getdate(), 23 )  and AlarmType != 4 order by AlarmStation";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                mg_alarm alarm = new mg_alarm();
                alarm.AlarmStation = dr["AlarmStation"].ToString();
                alarm.AlarmType = (int)dr["AlarmType"];
                allal.Add(alarm);
            }
            dr.Close();
            conn.Close();
            return allal;
        }
        public static List<mg_alarm> getfsabydate(string dtb, string dta)
        {
            List<mg_alarm> allal = new List<mg_alarm>();
            string SqlStr = SqlHelper.SqlConnString;
            SqlConnection conn = new SqlConnection(SqlStr);
            string sql = "";
            sql = "select distinct AlarmStation,AlarmType from mg_Alarm where [AlarmStartTime]>='" + dtb + "' and [AlarmEndTime]<='" + dta + "'  and AlarmType != 4 order by AlarmStation";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                mg_alarm alarm = new mg_alarm();
                alarm.AlarmStation = dr["AlarmStation"].ToString();
                alarm.AlarmType = (int)dr["AlarmType"];
                allal.Add(alarm);
            }
            dr.Close();
            conn.Close();
            return allal;
        }
        public static List<mg_alarm> getfsaexcel()
        {
            List<mg_alarm> allal = new List<mg_alarm>();
            string SqlStr = SqlHelper.SqlConnString;
            SqlConnection conn = new SqlConnection(SqlStr);
            string sql = "";
            sql = "select  distinct* from mg_Alarm where CONVERT(varchar(12) , AlarmStartTime, 23 ) =  CONVERT(varchar(12) , getdate(), 23 ) order by AlarmStation";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                mg_alarm alarm = new mg_alarm();
                alarm.AlarmType = (int)dr["AlarmType"];
                if (dr["AlarmText"] != DBNull.Value)
                {
                    alarm.AlarmText = dr["AlarmText"].ToString();
                }
                alarm.AlarmStation = dr["AlarmStation"].ToString();
                if (dr["AlarmStartTime"] != DBNull.Value)
                {
                    alarm.AlarmStartTime = Convert.ToDateTime(dr["AlarmStartTime"]);
                }
                if (dr["AlarmEndTime"] != DBNull.Value)
                {
                    alarm.AlarmEndTime = Convert.ToDateTime(dr["AlarmEndTime"]);
                }
                if (dr["StartOrderNo"] != DBNull.Value)
                {
                    alarm.StartOrderNo = dr["StartOrderNo"].ToString();
                }
                if (dr["EndOrderNo"] != DBNull.Value)
                {
                    alarm.EndOrderNo = dr["EndOrderNo"].ToString();
                }
                alarm.IsSolve = (int)dr["AlarmType"];
                allal.Add(alarm);
            }
            dr.Close();
            conn.Close();
            return allal;
        }
        public static DataTable getfsabydateexcel(string dtb, string dta)
        {
            string sql = "select distinct case when AlarmType = 1 then '物料' when AlarmType = 2 then '质量'  when AlarmType = 3 then '维修'  when AlarmType = 4 then '超时' when AlarmType = 5 then '生产' when AlarmType = 6 then '急停' else '正常' end as Status,AlarmStation,AlarmStartTime,AlarmEndTime,StartOrderNo,EndOrderNo,case when IsSolve = 1 then '已解决' else '未解决' end as IsPass from mg_Alarm where (([AlarmStartTime]>='" + dtb + "' and [AlarmEndTime]<='" + dta + "') and (StartOrderNo is not null and StartOrderNo <> '') and EndOrderNo is not null and EndOrderNo <> '' )order by AlarmStation";
            FunSql.Init();
            DataTable t = FunSql.GetTable(sql);
            return t;
        }
        public static void MergeOrderToBuffer()
        {
            #region FS表
            FunSql.Init();
            DataTable FSD = FunSql.GetTable("select * from mg_Order_FSD where MergeFlag = 0");
            DataTable FSP = FunSql.GetTable("select * from mg_Order_FSP where MergeFlag = 0");

            int FSCount = FSD.Rows.Count;
            if (FSP.Rows.Count > FSD.Rows.Count)
            {
                FSCount = FSP.Rows.Count;
            }

            for (int i = 0; i < FSCount; i++)
            {
                if (i < FSD.Rows.Count)
                {
                    FunSql.Insert("mg_Order_FS_Temp", "TypeIdentifier,Order_FS_ID,TempOfspb_pre,TempOfspb_id,TempCo_id,TempPart_no,TempOfspb_cdstr,TempOfspb_createdate,SortNumber", new object[] { 1, FSD.Rows[i]["ID"], FSD.Rows[i]["ofsd_pre"], FSD.Rows[i]["ofsd_id"], FSD.Rows[i]["co_id"], FSD.Rows[i]["part_no"], FSD.Rows[i]["ofsd_cdstr"], FSD.Rows[i]["ofsd_createdate"], i * 2 + 1 });
                }
                if (i < FSP.Rows.Count)
                {
                    FunSql.Insert("mg_Order_FS_Temp", "TypeIdentifier,Order_FS_ID,TempOfspb_pre,TempOfspb_id,TempCo_id,TempPart_no,TempOfspb_cdstr,TempOfspb_createdate,SortNumber", new object[] { 2, FSP.Rows[i]["ID"], FSP.Rows[i]["ofsp_pre"], FSP.Rows[i]["ofsp_id"], FSP.Rows[i]["co_id"], FSP.Rows[i]["part_no"], FSP.Rows[i]["ofsp_cdstr"], FSP.Rows[i]["ofsp_createdate"], i * 2 + 2 });
                }
            }

            foreach (DataRow row in FSD.Rows)
            {
                FunSql.Exec("Update mg_Order_FSD set MergeFlag = 1 where ID = '" + row["ID"] + "'");
            }

            foreach (DataRow row in FSP.Rows)
            {
                FunSql.Exec("Update mg_Order_FSP set MergeFlag = 1 where ID = '" + row["ID"] + "'");
            }
            #endregion

            #region FSB表
            FunSql.Init();
            DataTable FSDB = FunSql.GetTable("select * from mg_Order_FSDB where MergeFlag = 0");
            DataTable FSPB = FunSql.GetTable("select * from mg_Order_FSPB where MergeFlag = 0");

            int FSBCount = FSDB.Rows.Count;
            if (FSPB.Rows.Count > FSDB.Rows.Count)
            {
                FSBCount = FSPB.Rows.Count;
            }

            for (int i = 0; i < FSBCount; i++)
            {
                if (i < FSDB.Rows.Count)
                {
                    FunSql.Insert("mg_Order_FSB_Temp", "TypeIdentifier,Order_FS_ID,TempOfspb_pre,TempOfspb_id,TempCo_id,TempPart_no,TempOfspb_cdstr,TempOfspb_createdate,SortNumber", new object[] { 1, FSDB.Rows[i]["ID"], FSDB.Rows[i]["ofsdb_pre"], FSDB.Rows[i]["ofsdb_id"], FSDB.Rows[i]["co_id"], FSDB.Rows[i]["part_no"], FSDB.Rows[i]["ofsdb_cdstr"], FSDB.Rows[i]["ofsdb_createdate"], i * 2 + 1 });
                }
                if (i < FSPB.Rows.Count)
                {
                    FunSql.Insert("mg_Order_FSB_Temp", "TypeIdentifier,Order_FS_ID,TempOfspb_pre,TempOfspb_id,TempCo_id,TempPart_no,TempOfspb_cdstr,TempOfspb_createdate,SortNumber", new object[] { 2, FSPB.Rows[i]["ID"], FSPB.Rows[i]["ofspb_pre"], FSPB.Rows[i]["ofspb_id"], FSPB.Rows[i]["co_id"], FSPB.Rows[i]["part_no"], FSPB.Rows[i]["ofspb_cdstr"], FSPB.Rows[i]["ofspb_createdate"], i * 2 + 2 });
                }
            }

            foreach (DataRow row in FSDB.Rows)
            {
                FunSql.Exec("Update mg_Order_FSDB set MergeFlag = 1 where ID = '" + row["ID"] + "'");
            }

            foreach (DataRow row in FSPB.Rows)
            {
                FunSql.Exec("Update mg_Order_FSPB set MergeFlag = 1 where ID = '" + row["ID"] + "'");
            }
            #endregion

            #region FSC表
            FunSql.Init();
            DataTable FSDC = FunSql.GetTable("select * from mg_Order_FSDC where MergeFlag = 0");
            DataTable FSPC = FunSql.GetTable("select * from mg_Order_FSPC where MergeFlag = 0");

            int FSCCount = FSDC.Rows.Count;
            if (FSPC.Rows.Count > FSDC.Rows.Count)
            {
                FSCCount = FSPC.Rows.Count;
            }

            for (int i = 0; i < FSCCount; i++)
            {
                if (i < FSDC.Rows.Count)
                {
                    FunSql.Insert("mg_Order_FSC_Temp", "TypeIdentifier,Order_FS_ID,TempOfspb_pre,TempOfspb_id,TempCo_id,TempPart_no,TempOfspb_cdstr,TempOfspb_createdate,SortNumber", new object[] { 1, FSDC.Rows[i]["ID"], FSDC.Rows[i]["ofsdc_pre"], FSDC.Rows[i]["ofsdc_id"], FSDC.Rows[i]["co_id"], FSDC.Rows[i]["part_no"], FSDC.Rows[i]["ofsdc_cdstr"], FSDC.Rows[i]["ofsdc_createdate"], i * 2 + 1 });
                }
                if (i < FSPC.Rows.Count)
                {
                    FunSql.Insert("mg_Order_FSC_Temp", "TypeIdentifier,Order_FS_ID,TempOfspb_pre,TempOfspb_id,TempCo_id,TempPart_no,TempOfspb_cdstr,TempOfspb_createdate,SortNumber", new object[] { 2, FSPC.Rows[i]["ID"], FSPC.Rows[i]["ofspc_pre"], FSPC.Rows[i]["ofspc_id"], FSPC.Rows[i]["co_id"], FSPC.Rows[i]["part_no"], FSPC.Rows[i]["ofspc_cdstr"], FSPC.Rows[i]["ofspc_createdate"], i * 2 + 2 });
                }
            }

            foreach (DataRow row in FSDC.Rows)
            {
                FunSql.Exec("Update mg_Order_FSDC set MergeFlag = 1 where ID = '" + row["ID"] + "'");
            }

            foreach (DataRow row in FSPC.Rows)
            {
                FunSql.Exec("Update mg_Order_FSPC set MergeFlag = 1 where ID = '" + row["ID"] + "'");
            }
            #endregion

        }

    }
    public class alarms
    {
        public string alarmsstation { get; set; }
        public int alarmtype { get; set; }
    }
}