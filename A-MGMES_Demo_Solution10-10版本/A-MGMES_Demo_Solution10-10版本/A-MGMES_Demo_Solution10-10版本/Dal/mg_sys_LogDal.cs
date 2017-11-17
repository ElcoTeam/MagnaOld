using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dal
{
    public class mg_sys_LogDal
    {
        private static readonly Regex numMatcher = new Regex("[0-9]+(\\.[0-9]+)?");

        #region 获取
        public static List<mg_sys_log> getTorqueAndAngleInfo(string fl_id, string st_no, string part_no)
        {
            List<mg_sys_log> result = new List<mg_sys_log>();
            StringBuilder sql = new StringBuilder(@"SELECT fl_id,fl_name,st_no,part_no,step_order as step, AngleResult as angle, TorqueResult as torque FROM  dbo.mg_sys_log where 1=1 and Len(AngleResult) > 0");
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (string.IsNullOrEmpty(fl_id) == true || string.IsNullOrEmpty(st_no) == true || string.IsNullOrEmpty(part_no) == true)
            {
                List<mg_sys_log> list = new List<mg_sys_log>();
                return list;
            }
            else
            {

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
                sql.Append(" order by fl_name, st_no, part_no, step");
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
                        TorqueResult = TorqueResult,
                        AngleResult = AngleResult
                    });
                }
                return result;
            }



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
            result.Insert(0, new
            {
                fl_id = 0,
                //fl_name = "-- 流水线(全部) --"
            });
            return result;
        }
        #endregion

        #region 获取工位号
        public static List<object> getst_idList(string fl_id)
        {
            List<object> result = new List<object>();
            //string sql = "select distinct st_id, st_no from dbo.mg_sys_log where fl_id=@fl_id and len(AngleResult) > 0";
            string sql = "select distinct dbo.mg_station.st_no from dbo.mg_station LEFT JOIN dbo.mg_sys_log ON dbo.mg_station.st_no=dbo.mg_sys_log.st_no  where  dbo.mg_station.fl_id=@fl_id order by dbo.mg_station.st_no";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id } };
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, parameters);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    //st_id = row["st_id"],
                    st_no = row["st_no"]
                });
            }
            result.Insert(0, new
            {
                st_id = 0,
                //st_no = "-- 工位(全部) --"
            });
            return result;
        }
        #endregion

        #region 获取部件号
        public static List<object> getpart_idList(string fl_id, string st_no)
        {
            List<object> result = new List<object>();
            string sql = "select distinct part_no from dbo.mg_sys_log where fl_id=@fl_id and st_no=@st_no";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@fl_id", SqlDbType.NVarChar) { Value = fl_id },
            new SqlParameter("@st_no", SqlDbType.NVarChar) { Value = st_no }};
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, parameters);
            foreach (DataRow row in table.Rows)
            {
                result.Add(new
                {
                    part_no = row["part_no"]
                });
            }
            result.Insert(0, new
            {
                part_no = "-- 部件(全部) --"
            });
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
left join mg_station_log e on a.OrderNo = e.or_no
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
