using DbUtility;
using DbUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Tools;
namespace Dal
{
    public class mg_sys_LogDal
    {
        public static int InsertLog(mg_sys_log model)
        {
            string sql2 = "INSERT INTO View_mg_sys_log (AngleResult,TorqueResult,scanCode,MenderName,ReviseTime,op_id,op_name,fl_id,fl_name,st_id,st_no,PartOrderID,or_no,part_no,step_order,step_startTime,step_endTime,step_duringtime,scanResult,sys_desc,ScrewCount) VALUES ('" + model.AngleResult + "', '" + model.TorqueResult + "', '" + model.scanCode + "', '" + model.MenderName + "', '" + model.ReviseTime + "', '" + model.op_id + "', '" + model.op_name + "', '" + model.fl_id + "', '" + model.fl_name + "', '" + model.st_id + "', '" + model.st_no + "', '" + model.PartOrderID + "', '" + model.or_no + "', '" + model.part_no + "', '" + model.step_order + "', '" + model.step_startTime + "', '" + model.step_endTime + "', '" + model.step_duringtime + "', '" + model.scanResult + "', '" + model.sys_desc + "', '" + model.ScrewCount + "')";
            int a = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql2, null);
            return a;
        }
        public static DataTable GetTableByID(string sys_id)
        {
            string sql1 = "select * from View_mg_sys_log where sys_id = '" + sys_id + "'";

            DataTable ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql1, null);
            return ResTable;
        }
        private static readonly Regex numMatcher = new Regex("[0-9]+(\\.[0-9]+)?");
        public static string getList(int PageSize,int StartIndex, int EndIndex,string SortFlag, string sortOrder,string wherestr)
        {
            if (string.IsNullOrEmpty(SortFlag))
            {
                SortFlag = " st_no,step_startTime";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }
            List<mg_sys_log> list = new List<mg_sys_log>();
            int total = 0 ;
            string query_sql = " select * from(select row_number() over(order by " + SortFlag + " " + sortOrder + " ) as rowid,report.* from View_mg_sys_log report  where 1 = 1 " + wherestr + ") as Results where rowid >=" + StartIndex + " and rowid <=" + EndIndex + " ";
            string count_sql = "select  count(*) as total from View_mg_sys_log where 1 = 1 " + wherestr;
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, count_sql + query_sql, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt1.Rows[0], "total"));
                DataTable dt2 = ds.Tables["data"];
                foreach (DataRow row in dt2.Rows)
                {
                      mg_sys_log model = new mg_sys_log();
                      model.sys_id = DataHelper.GetCellDataToStr(row, "sys_id");
                      model.op_id=DataHelper.GetCellDataToStr(row, "op_id");
                      model.op_name=DataHelper.GetCellDataToStr(row, "op_name");
                      model.fl_id= DataHelper.GetCellDataToStr(row, "fl_id");
                      model.fl_name= DataHelper.GetCellDataToStr(row, "fl_name");
                      model.st_id= DataHelper.GetCellDataToStr(row, "st_id");
                      model.st_no= DataHelper.GetCellDataToStr(row, "st_no");
                      model.PartOrderID= DataHelper.GetCellDataToStr(row, "PartOrderID");
                      model.or_no= DataHelper.GetCellDataToStr(row, "or_no");
                      model.part_no= DataHelper.GetCellDataToStr(row, "part_no");
                      model.step_order = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "step_order"));
                      model.step_startTime= DataHelper.GetCellDataToStr(row, "step_startTime");
                      model.step_endTime= DataHelper.GetCellDataToStr(row, "step_endTime");
                      model.step_duringtime= DataHelper.GetCellDataToStr(row, "step_duringtime");
                      model.AngleResult= DataHelper.GetCellDataToStr(row, "AngleResult");
                      model.TorqueResult = DataHelper.GetCellDataToStr(row, "TorqueResult");
                      model.scanCode= DataHelper.GetCellDataToStr(row, "scanCode");
                      model.scanResult= DataHelper.GetCellDataToStr(row, "scanResult");
                      model.sys_desc= DataHelper.GetCellDataToStr(row, "sys_desc");
                      model.ScrewCount= DataHelper.GetCellDataToStr(row, "ScrewCount");
                      model.MenderName= DataHelper.GetCellDataToStr(row, "MenderName");
                      model.ReviseTime= DataHelper.GetCellDataToStr(row, "ReviseTime");
                

                    list.Add(model);
                }
            }
            DataListModel<mg_sys_log> allmodel = new DataListModel<mg_sys_log>();
            allmodel.total = total.ToString();
            allmodel.rows = list;
            string jsonStr = JSONTools.ScriptSerialize<DataListModel<mg_sys_log>>(allmodel);
            return jsonStr;
           
        }

        public static DataTable getList(int PageSize, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr,out int total)
        {
            if (string.IsNullOrEmpty(SortFlag))
            {
                SortFlag = "st_no,step_startTime";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }
            List<mg_sys_log> list = new List<mg_sys_log>();
            string query_sql = " select * from(select row_number() over(order by " + SortFlag + " " + sortOrder + " ) as rowid,report.* from View_mg_sys_log report  where 1 = 1 " + wherestr + ") as Results where rowid >=" + StartIndex + " and rowid <=" + EndIndex + " ";
            if(EndIndex == -1)
            {
                query_sql = " select * from(select row_number() over(order by " + SortFlag + " " + sortOrder + " ) as rowid,report.* from View_mg_sys_log report  where 1 = 1 " + wherestr + ") as Results where rowid >=" + StartIndex + " ";
            }
            
            string count_sql = "select  count(*) as total from View_mg_sys_log where 1 = 1 " + wherestr;
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, count_sql + query_sql, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt1.Rows[0], "total"));
                DataTable dt2 = ds.Tables["data"];
                return dt2;
            }
            else
            {
                total = 0;
                return null;
            }

        }
        #region 获取
        public static List<mg_sys_log> getTorqueAndAngleInfo(string fl_id, string st_no, string part_no, string starttime, string endtime)
        {
            List<mg_sys_log> result = new List<mg_sys_log>();

            StringBuilder sql = new StringBuilder(@"SELECT fl_id,fl_name,st_no,part_no,step_order as step, AngleResult as angle, TorqueResult as torque FROM  dbo.View_mg_sys_log where 1=1 and AngleResult!='' ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            //System.Diagnostics.Debug.Write();
            if (string.IsNullOrEmpty(fl_id) == false)
            {
                sql.Append(" and fl_id=@fl_id");
                parameters.Add(new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id });
            }
            if (string.IsNullOrEmpty(st_no) == false)
            {
                sql.Append(" and st_no=@st_no");
                parameters.Add(new SqlParameter("@st_no", SqlDbType.NVarChar) { Value = st_no });
            }
            if (string.IsNullOrEmpty(part_no) == false)
            {
                sql.Append(" and part_no=@part_no");
                parameters.Add(new SqlParameter("@part_no", SqlDbType.NVarChar) { Value = part_no });
            }
            //时间条件查询
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                sql.Append(" and step_startTime >=@starttime and step_endTime<=@endtime");
                parameters.Add(new SqlParameter("@starttime", SqlDbType.NVarChar) { Value = starttime });
                parameters.Add(new SqlParameter("@endtime", SqlDbType.NVarChar) { Value = endtime });
            }
            sql.Append(" order by step_startTime,or_no,fl_name, st_no, part_no, step");
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql.ToString(), parameters.ToArray());
            foreach (DataRow row in table.Rows)
            {
                decimal TorqueResult = 0;
                string str = Convert.ToString(row["torque"].ToString().Replace("Nm", ""));
                if (!String.IsNullOrEmpty(str))
                {
                    TorqueResult = Convert.ToDecimal(str);
                }
                decimal AngleResult = 0;
                str = Convert.ToString(row["angle"].ToString().Replace("°", ""));
                if (!String.IsNullOrEmpty(str))
                {
                    AngleResult = Convert.ToDecimal(str);
                }


                result.Add(new mg_sys_log()
                {
                    fl_name = Tools.DataHelper.GetCellDataToStr(row, "fl_name"),
                    st_no = Tools.DataHelper.GetCellDataToStr(row, "st_no"),
                    part_no = Tools.DataHelper.GetCellDataToStr(row, "part_no"),
                    step_order = Tools.NumericParse.StringToInt(row["step"] as string),
                    TorqueResult = TorqueResult.ToString(),
                    AngleResult = AngleResult.ToString()
                });
            }


            return result;
        }
        #endregion

        #region 获取流水线
        public static List<object> getfl_idList()
        {
            List<object> result = new List<object>();
            string sql = "select * from dbo.mg_FlowLine order by fl_name";      //有fl_id，fl_name两个字段
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    fl_id = row["fl_id"],
                    fl_name = row["fl_name"]
                });
            }
            
            return result;
        }

        public static List<object> getfl_idListforTor()
        {
            List<object> result = new List<object>();
            string sql = @" select distinct f.fl_id ,f.fl_name  from mg_FlowLine f
    LEFT JOIN dbo.View_mg_sys_log b on f.fl_id = b.fl_id 
	where b.AngleResult!='' order by f.fl_id ";      //有fl_id，fl_name两个字段
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            result.Add(new
            {
                fl_id = "",
                fl_name = "请选择"
            });

            foreach (DataRow row in table.Rows)
            {
                
                result.Add(new
                {
                    fl_id = row["fl_id"],
                    fl_name = row["fl_name"]
                });
            }

            return result;
        }
        #endregion

        #region 获取工位号
        public static List<object> getst_idList(string fl_id)
        {
           
            List<object> result = new List<object>();
            string sql = "";
            SqlParameter[] parameters = null;
            //string sql = "select distinct st_id, st_no from dbo.mg_sys_log where fl_id=@fl_id and len(AngleResult) > 0";
            if (!string.IsNullOrEmpty(fl_id))
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no    where dbo.mg_station.fl_id=@fl_id  and len(b.AngleResult) > 0 order by dbo.mg_station.st_no";
                 parameters = new SqlParameter[] { new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id } };
            }
            else
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no where 1=1 and len(b.AngleResult) > 0 order by dbo.mg_station.st_no";
                 parameters = null;
            }
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, parameters);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    //st_id = row["st_id"],
                    st_no = row["st_no"]
                });
            }
           
            return result;
        }
        #endregion
        #region 获取扭矩角度中的 工位号
        public static List<object> getst_idListForTorque(string fl_id)
        {

            List<object> result = new List<object>();
            string sql = "";
            SqlParameter[] parameters = null;
            //string sql = "select distinct st_id, st_no from dbo.mg_sys_log where fl_id=@fl_id and len(AngleResult) > 0";
            if (!string.IsNullOrEmpty(fl_id))
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no    where dbo.mg_station.fl_id=@fl_id  and b.AngleResult!='' order by dbo.mg_station.st_no";
                //sql = "select distinct b.st_no from  dbo.View_mg_sys_log b  where b.fl_id=@fl_id  order by b.st_no";
                parameters = new SqlParameter[] { new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id } };
            }
            else
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no where 1=1 and b.AngleResult!='' order by dbo.mg_station.st_no";
               // sql = "select distinct b.st_no from  dbo.View_mg_sys_log b   order by b.st_no";
                parameters = null;
            }
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, parameters);
            result.Add(new
            {
               
                st_no = "请选择"
            });
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    // st_id = row["st_id"],
                    st_no = row["st_no"]
                });
            }

            return result;
        }
        #endregion
        #region 获取点检记录表中的 工位号
        public static List<object> getst_idListForCheck(string fl_id)
        {

            List<object> result = new List<object>();
            string sql = "";
            SqlParameter[] parameters = null;
            //string sql = "select distinct st_id, st_no from dbo.mg_sys_log where fl_id=@fl_id and len(AngleResult) > 0";
            if (!string.IsNullOrEmpty(fl_id))
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no    where dbo.mg_station.fl_id=@fl_id  order by dbo.mg_station.st_no";
                parameters = new SqlParameter[] { new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id } };
            }
            else
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no where 1=1 order by dbo.mg_station.st_no";
                parameters = null;
            }
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, parameters);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    // st_id = row["st_id"],
                    st_no = row["st_no"]
                });
            }

            return result;
        }
        #endregion
        #region 获取工序步骤日志查询中的 工位号
        public static List<object> getst_idListForStep(string fl_id)
        {

            List<object> result = new List<object>();
            string sql = "";
            SqlParameter[] parameters = null;
            //string sql = "select distinct st_id, st_no from dbo.mg_sys_log where fl_id=@fl_id and len(AngleResult) > 0";
            if (!string.IsNullOrEmpty(fl_id))
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no    where dbo.mg_station.fl_id=@fl_id  order by dbo.mg_station.st_no";
                parameters = new SqlParameter[] { new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id } };
            }
            else
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no where 1=1 order by dbo.mg_station.st_no";
                parameters = null;
            }
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, parameters);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                   // st_id = row["st_id"],
                    st_no = row["st_no"]
                });
            }

            return result;
        }
        #endregion

        #region 获取产量报表查询中的 工位号
        public static List<object> getst_idListForVolume(string fl_id)
        {

            List<object> result = new List<object>();
            string sql = "";
            SqlParameter[] parameters = null;
            //string sql = "select distinct st_id, st_no from dbo.mg_sys_log where fl_id=@fl_id and len(AngleResult) > 0";
            if (!string.IsNullOrEmpty(fl_id))
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no    where dbo.mg_station.fl_id=@fl_id  order by dbo.mg_station.st_no";
                parameters = new SqlParameter[] { new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id } };
            }
            else
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no where 1=1 order by dbo.mg_station.st_no";
                parameters = null;
            }
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, parameters);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    //st_id = row["st_id"],
                    st_no = row["st_no"]
                });
            }

            return result;
        }
        #endregion
        #region 获取时间报表查询中的 工位号
        public static List<object> getst_idListForTime(string fl_id)
        {

            List<object> result = new List<object>();
            string sql = "";
            SqlParameter[] parameters = null;
            //string sql = "select distinct st_id, st_no from dbo.mg_sys_log where fl_id=@fl_id and len(AngleResult) > 0";
            if (!string.IsNullOrEmpty(fl_id))
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no    where dbo.mg_station.fl_id=@fl_id  order by dbo.mg_station.st_no";
                parameters = new SqlParameter[] { new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id } };
            }
            else
            {
                sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.View_mg_sys_log b ON dbo.mg_station.st_no=b.st_no where 1=1 order by dbo.mg_station.st_no";
                parameters = null;
            }
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, parameters);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    //st_id = row["st_id"],
                    st_no = row["st_no"]
                });
            }

            return result;
        }
        #endregion

        #region 获取部件号
        public static List<object> getpart_idList(string fl_id, string st_no)
        {
            List<object> result = new List<object>();
            StringBuilder sql = new StringBuilder(@"select distinct part_no from dbo.View_mg_sys_log where 1=1 and AngleResult!='' ");
            List<SqlParameter> parameters = new List<SqlParameter>();

                if (string.IsNullOrEmpty(fl_id) == false)
                {
                    sql.Append(" and fl_id=@fl_id");
                    parameters.Add(new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id });
                }
                if (string.IsNullOrEmpty(st_no) == false)
                {
                    sql.Append(" and st_no=@st_no");
                    parameters.Add(new SqlParameter("@st_no", SqlDbType.NVarChar) { Value = st_no });
                }
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql.ToString(), parameters.ToArray());
            result.Add(new
            {
                part_no = "请选择"
            });
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    part_no = row["part_no"]
                });
            }
           
            return result;
        }
        #endregion

        #region 检测返修中，选了日期，可选订单号的函数
        public static List<object> getalldingdanhaohao(string StartTime,string EndTime)
        {
            List<object> result = new List<object>();
            string sql = @"select distinct a.OrderNo from mg_Test_Part_Record a 
left join mg_Test_Part b on a.Test_PartID = b.ID
left join mg_test c on b.TestID = c.id
left join mg_Operator d on a.operatorid = d.op_id
left join View_mg_station_log e on a.OrderNo = e.or_no
left join mg_Test_Repair_Item_Record f on a.OrderNo = f.OrderNo
left join mg_Test_Repair_Item g on f.Repair_ItemID = g.ID
where e.station_startTime > '" + StartTime + "' and e.station_endTime < '" + EndTime + "'";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@StartTime", SqlDbType.NVarChar) { Value = StartTime },
            new SqlParameter("@EndTime", SqlDbType.NVarChar) { Value = StartTime }};
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, parameters);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    OrderNo = row["OrderNo"]
                });
            }
            //result.Insert(0, new
            //{
            //    OrderNo = "-- 订单号(全部) --"
            //});
            return result;
        }
        #endregion

    }
}
