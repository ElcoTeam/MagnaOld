using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Tools;
using DBUtility;
using Model;


namespace DAL
{
    public class mg_StationDAL
    {
        public static int AddStByName(string st_no, string st_name, int fl_id, int st_ispre)
        {
            //            string maxidSql = @"declare @i int;
            //                                SELECT @i=max([st_id])  FROM [mg_station];
            //                                if @i is null
            //                                    begin
            //                                        set @i=1
            //                                    end
            //                                else
            //                                    begin
            //                                        set @i=@i+1
            //                                    end;";
            string maxid = @"declare @i int;
                                SELECT @i=max([st_id])  FROM [mg_station];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;";
            string sql = @"INSERT INTO [mg_station] ([st_id],[st_no],[fl_id],[st_ispre],[st_name]) VALUES ( @i,'" + st_no + "'," + fl_id + "," + st_ispre + ",'" + st_name + @"')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, maxid + sql, null);
        }

        public static DataTable GetAllData()
        {
            string sql = @"SELECT   dbo.mg_station.st_id, dbo.mg_station.st_no, dbo.mg_station.st_name, dbo.mg_station.fl_id, dbo.mg_FlowLine.fl_name, 
                CASE dbo.mg_station.st_ispre WHEN 1 THEN '是' WHEN 0 THEN '否' END AS st_pre
FROM      dbo.mg_FlowLine RIGHT OUTER JOIN
                dbo.mg_station ON dbo.mg_FlowLine.fl_id = dbo.mg_station.fl_id order by dbo.mg_station.st_id";
            //string sql = @"select * from  View_Station order by st_id";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int UpDateStByName(int st_id, string st_no, string st_name, int fl_id, int st_ispre)
        {
            string sql = @"update [mg_station] set [st_no]='" + st_no + "',[st_name]='" + st_name + "',[fl_id]=" + fl_id + ",[st_ispre]= " + st_ispre + " where [st_id]=" + st_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int DelStByName(int st_id)
        {
            string sql = @"delete from [mg_station] where [st_id]=" + st_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int CheckStByName(int a, int st_id, string st_no, string st_name, int fl_id)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"select * from [mg_station] where (st_name='" + st_name + "' or st_no='" + st_no + "')  and fl_id=" + fl_id;
            }
            if (a == 2)
            {
                sql = @"select * from [mg_station] where (st_name='" + st_name + "'or st_no='" + st_no + "') and fl_id=" + fl_id + " and st_id <> " + st_id;
            }
            tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            if (tb.Rows.Count != 0)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
            tb.Dispose();
            return i;
        }

        public static int flid(string flname)
        {
            int i;
            string sql;
            DataTable tb;

            sql = @"select * from [mg_FlowLine] where fl_name='" + flname + "'";
            tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            if (tb.Rows.Count != 0)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
            tb.Dispose();
            return i;
        }

        public static List<mg_stationModel> QueryStationForOperatorEditing()
        {
            List<mg_stationModel> list = null;
            string sql = @"SELECT [st_id]
                                  ,[st_no]
                                  ,[st_name]
                              FROM [mg_station] order by st_no";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_stationModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_stationModel model = new mg_stationModel();
                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    model.st_name = DataHelper.GetCellDataToStr(row, "st_no") + " --- " + DataHelper.GetCellDataToStr(row, "st_name");
                    list.Add(model);
                }
            }
            return list;
        }

        public static string GetMaxNO()
        {
            string maxid = @"declare @i int;
                                SELECT @i=max([st_id])  FROM [mg_station];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                select @i;
                                ";
            return SqlHelper.ExecuteScalar(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid, null).ToString();
        }

        public static int UpdateStation(mg_stationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_station set ");
            strSql.Append("fl_id=@fl_id,");
            strSql.Append("st_no=@st_no,");
            strSql.Append("st_name=@st_name,");
            strSql.Append("st_mac=@st_mac,");
            strSql.Append("st_typeid=@st_typeid,");
            strSql.Append("st_mushifile=@st_mushifile,");
            strSql.Append("st_isfirst=@st_isfirst,");
            strSql.Append("st_isend=@st_isend,");
            strSql.Append("st_odsfile=@st_odsfile");
            strSql.Append(" where st_id=@st_id;");
            SqlParameter[] parameters = {
					new SqlParameter("@st_id", SqlDbType.Int),
					new SqlParameter("@fl_id", SqlDbType.Int),
					new SqlParameter("@st_no", SqlDbType.NVarChar),
					new SqlParameter("@st_name", SqlDbType.NVarChar),
					new SqlParameter("@st_mac", SqlDbType.VarChar),
					new SqlParameter("@st_typeid", SqlDbType.Int),
					new SqlParameter("@st_mushifile", SqlDbType.VarChar),
					new SqlParameter("@st_odsfile", SqlDbType.VarChar),
					new SqlParameter("@st_isfirst", SqlDbType.Int),
					new SqlParameter("@st_isend", SqlDbType.Int)
                                        };
            parameters[0].Value = model.st_id;
            parameters[1].Value = model.fl_id;
            parameters[2].Value = model.st_no;
            parameters[3].Value = model.st_name;
            parameters[4].Value = model.st_mac;
            parameters[5].Value = model.st_typeid;
            parameters[6].Value = model.st_mushifile;
            parameters[7].Value = model.st_odsfile;
            parameters[8].Value = model.st_isfirst;
            parameters[9].Value = model.st_isend;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;

        }


        public static int AddStation(mg_stationModel model)
        {
            string maxid = @"declare @i int;declare @o int;
                                SELECT @i=max([st_id])  FROM [mg_station];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                SELECT @o=max([st_order])  FROM [mg_station];
                                if @o is null
                                    begin
                                        set @o=1
                                    end
                                else
                                    begin
                                        set @o=@o+1
                                    end;
                                ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into mg_station(");
            strSql.Append("st_id,fl_id,st_no,st_name,st_mac,st_typeid,st_order,st_mushifile,st_odsfile,st_isfirst,st_isend)");
            strSql.Append(" values (");
            strSql.Append("@i,@fl_id,@st_no,@st_name,@st_mac,@st_typeid,@o,@st_mushifile,@st_odsfile,@st_isfirst,@st_isend)");
            SqlParameter[] parameters = {
					new SqlParameter("@fl_id", SqlDbType.Int),
					new SqlParameter("@st_no", SqlDbType.NVarChar),
					new SqlParameter("@st_name", SqlDbType.NVarChar),
					new SqlParameter("@st_mac", SqlDbType.VarChar),
					new SqlParameter("@st_typeid", SqlDbType.Int),
					new SqlParameter("@st_mushifile", SqlDbType.VarChar),
					new SqlParameter("@st_odsfile", SqlDbType.VarChar),
					new SqlParameter("@st_isfirst", SqlDbType.Int),
					new SqlParameter("@st_isend", SqlDbType.Int)
                                        };
            parameters[0].Value = model.fl_id;
            parameters[1].Value = model.st_no;
            parameters[2].Value = model.st_name;
            parameters[3].Value = model.st_mac;
            parameters[4].Value = model.st_typeid;
            parameters[5].Value = model.st_mushifile;
            parameters[6].Value = model.st_odsfile;
            parameters[7].Value = model.st_isfirst;
            parameters[8].Value = model.st_isend;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql, parameters);
            return rows;
        }

        public static List<mg_stationModel> QueryListForPaging(string page, string pagesize, out string total, string fl_id)
        {
            total = "0";
            List<mg_stationModel> list = null;
            string queryStr = (!string.IsNullOrEmpty(fl_id) && fl_id != "0") ? " where st.fl_id=" + fl_id : " where st.st_id is not null ";
            string sql1 = @"select count(st_id) total from [mg_station] st " + queryStr + @";";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [st_id]
                                      ,st.[fl_id]
                                      ,fl.fl_name
                                      ,[st_no]
                                      ,[st_name]
                                      ,[st_mac]
                                      ,[st_typeid]
                                      ,p.prop_name [st_typename]
                                      ,[st_order]
                                      ,[st_odsfile]
                                      ,[st_mushifile]
,st_isfirst
 ,CASE st_isfirst WHEN 1 THEN '是' WHEN 0 THEN '否' END AS st_isfirstname
,st_isend
 ,CASE st_isend WHEN 1 THEN '是' WHEN 0 THEN '否' END AS st_isendname
                                  FROM [mg_station] st
                                  left join mg_FlowLine fl on st.fl_id = fl.fl_id
                                  left join mg_Property p on st.st_typeid=p.prop_id
                                " + queryStr + @"
                                    and  st.st_order > (
                                                select top 1 st_order from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") st_order from  [mg_station] where st_order is not null  order by st_order  )t
                                                order by  st_order desc )
                                  order by st_order

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_stationModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_stationModel model = new mg_stationModel();

                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    model.fl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fl_id"));
                    model.fl_name = DataHelper.GetCellDataToStr(row, "fl_name");
                    model.st_no = DataHelper.GetCellDataToStr(row, "st_no");
                    model.st_name = DataHelper.GetCellDataToStr(row, "st_name");
                    model.st_mac = DataHelper.GetCellDataToStr(row, "st_mac");
                    model.st_typeid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_typeid"));
                    model.st_typename = DataHelper.GetCellDataToStr(row, "st_typename");
                    model.st_order = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_order"));
                    model.st_odsfile = DataHelper.GetCellDataToStr(row, "st_odsfile");
                    model.st_mushifile = DataHelper.GetCellDataToStr(row, "st_mushifile");
                    string pdf = DataHelper.GetCellDataToStr(row, "st_odsfile");
                    model.st_odsfilename = (!string.IsNullOrEmpty(pdf)) ? pdf.Substring(pdf.LastIndexOf('/') + 1) : "";
                    model.st_isfirst = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_isfirst"));
                    model.st_isend = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_isend"));
                    model.st_isendname = DataHelper.GetCellDataToStr(row, "st_isendname");
                    model.st_isfirstname = DataHelper.GetCellDataToStr(row, "st_isfirstname");
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_stationModel> QueryListForFirstPage(string pagesize, out string total, string fl_id)
        {
            total = "0";
            List<mg_stationModel> list = null;
            string queryStr = (!string.IsNullOrEmpty(fl_id) && fl_id != "0") ? " where st.fl_id=" + fl_id : "";
            string sql1 = @"select count(st_id) total from [mg_station] st " + queryStr + @" ;";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [st_id]
                                      ,st.[fl_id]
                                      ,fl.fl_name
                                      ,[st_no]
                                      ,[st_name]
                                      ,[st_mac]
                                      ,[st_typeid]
                                      ,p.prop_name [st_typename]
                                      ,[st_order]
                                        ,[st_odsfile]
                                      ,[st_mushifile]
,st_isfirst
 ,CASE st_isfirst WHEN 1 THEN '是' WHEN 0 THEN '否' END AS st_isfirstname
,st_isend
 ,CASE st_isend WHEN 1 THEN '是' WHEN 0 THEN '否' END AS st_isendname
                                  FROM [mg_station] st
                                  left join mg_FlowLine fl on st.fl_id = fl.fl_id
                                  left join mg_Property p on st.st_typeid=p.prop_id
                                    " + queryStr + @"
                                  order by st_order

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_stationModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_stationModel model = new mg_stationModel();

                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    model.fl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fl_id"));
                    model.fl_name = DataHelper.GetCellDataToStr(row, "fl_name");
                    model.st_no = DataHelper.GetCellDataToStr(row, "st_no");
                    model.st_name = DataHelper.GetCellDataToStr(row, "st_name");
                    model.st_mac = DataHelper.GetCellDataToStr(row, "st_mac");
                    model.st_typeid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_typeid"));
                    model.st_typename = DataHelper.GetCellDataToStr(row, "st_typename");
                    model.st_order = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_order"));
                    model.st_odsfile = DataHelper.GetCellDataToStr(row, "st_odsfile");
                    model.st_mushifile = DataHelper.GetCellDataToStr(row, "st_mushifile");
                    string pdf = DataHelper.GetCellDataToStr(row, "st_odsfile");
                    model.st_odsfilename = (!string.IsNullOrEmpty(pdf)) ? pdf.Substring(pdf.LastIndexOf('/') + 1) : "";
                    model.st_isfirst = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_isfirst"));
                    model.st_isend = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_isend"));
                    model.st_isendname = DataHelper.GetCellDataToStr(row, "st_isendname");
                    model.st_isfirstname = DataHelper.GetCellDataToStr(row, "st_isfirstname");
                    list.Add(model);
                }
            }
            return list;
        }

        public static int DeleteStation(string st_id)
        {
            string sql = @"delete from [mg_station] where [st_id]=" + st_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int SortingTop(string st_id, string st_order)
        {
            string sql = @"update mg_station set st_order=st_order+1 where st_order>=" + st_order + ";update mg_station set st_order=" + st_order + " where st_id=" + st_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int SortingBottom(string st_id, string st_order)
        {
            string sql = @"update mg_station set st_order=st_order+1 where st_order>" + st_order + ";update mg_station set st_order=" + st_order + "+1 where st_id=" + st_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static List<mg_stationModel> QueryStationForStepEditing(string fl_id)
        {
            List<mg_stationModel> list = null;
            string qsql = (!string.IsNullOrEmpty(fl_id) && fl_id != "0") ? @"  where fl_id=" + fl_id : "";
            string sql = @"SELECT [st_id]
                                      ,[st_no]
                                      ,[st_name]
                                  FROM [mg_station]
                                 " + qsql + @"
                                   order by st_order";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_stationModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_stationModel model = new mg_stationModel();
                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    model.st_no = DataHelper.GetCellDataToStr(row, "st_no") + " | " + DataHelper.GetCellDataToStr(row, "st_name");
                    list.Add(model);
                }
                mg_stationModel firstmodel = new mg_stationModel();
                firstmodel.st_id = 0;
                firstmodel.st_no = "-- 工位(全部) --";
                list.Insert(0, firstmodel);
            }

            return list;
        }

        public static mg_stationModel GetStationByMac(string mac)
        {
            string sql2 = @" 
                            SELECT top 1 [st_id]
                                      ,st.[fl_id]
                                      ,fl.fl_name
                                      ,[st_no]
                                      ,[st_name]
                                      ,[st_mac]
                                      ,[st_typeid]
                                      ,p.prop_name [st_typename]
                                      ,[st_order]
                                  FROM [mg_station] st
                                  left join mg_FlowLine fl on st.fl_id = fl.fl_id
                                  left join mg_Property p on st.st_typeid=p.prop_id
                                    where st.st_mac='" + mac + @"'

                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql2, null);
            if (DataHelper.HasData(dt))
            {
                mg_stationModel model = new mg_stationModel();

                foreach (DataRow row in dt.Rows)
                {

                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    model.fl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fl_id"));
                    model.fl_name = DataHelper.GetCellDataToStr(row, "fl_name");
                    model.st_no = DataHelper.GetCellDataToStr(row, "st_no");
                    model.st_name = DataHelper.GetCellDataToStr(row, "st_name");
                    model.st_mac = DataHelper.GetCellDataToStr(row, "st_mac");
                    model.st_typeid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_typeid"));
                    model.st_typename = DataHelper.GetCellDataToStr(row, "st_typename");
                    model.st_order = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_order"));
                    break;
                }
                return model;
            }
            return null;
        }
    }
}
