using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Tools;
using DbUtility;
using Model;

namespace DAL
{
    public class mg_FlowLineDAL
    {
        public static int AddFlowlineByName(string fl_name)
        {
            string maxidSql = @"declare @i int;
                                SELECT @i=max([fl_id])  FROM [mg_FlowLine];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;";
            string sql = maxidSql + @"INSERT INTO [mg_FlowLine] ([fl_id],[fl_name]) VALUES (@i,'" + fl_name + @"')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetAllData()
        {
            string sql = @"select * from [mg_FlowLine] order by fl_id";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int UpdateFlowlineByName(int fl_id, string fl_name)
        {
            string sql = @"update [mg_FlowLine] set fl_name='" + fl_name + "' where fl_id=" + fl_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int DelFlowlineByID(int fl_id)
        {
            string sql = @"delete from [mg_FlowLine] where fl_id=" + fl_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int CheckFlowlineByName(int a, int fl_id, string fl_name)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"select * from [mg_FlowLine] where fl_name='" + fl_name + "'";
            }
            if (a == 2)
            {
                sql = @"select * from [mg_FlowLine] where fl_name='" + fl_name + "' and fl_id <>" + fl_id;
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



        public static List<mg_FlowlingModel> QueryFlowlingForEditing()
        {
            List<mg_FlowlingModel> list = null;
            string sql = @"SELECT [fl_id]
                                  ,[fl_name]
                              FROM [mg_FlowLine]
                              order by fl_name";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_FlowlingModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_FlowlingModel model = new mg_FlowlingModel();
                    model.fl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fl_id"));
                    model.fl_name = DataHelper.GetCellDataToStr(row, "fl_name");
                    list.Add(model);
                }
                mg_FlowlingModel firstmodel = new mg_FlowlingModel();
                firstmodel.fl_id = 0;
                firstmodel.fl_name = "-- 流水线(全部) --";
                list.Insert(0, firstmodel);
            }

            return list;
        }

        public static List<mg_FlowlingModel> QueryFlowlingForStepEditing()
        {
            List<mg_FlowlingModel> list = null;
            string sql = @"SELECT [fl_id]
                                  ,[fl_name]
                              FROM [mg_FlowLine]
                              order by fl_name";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_FlowlingModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_FlowlingModel model = new mg_FlowlingModel();
                    model.fl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fl_id"));
                    model.fl_name = DataHelper.GetCellDataToStr(row, "fl_name");
                    list.Add(model);
                }
            }

            return list;
        }
    }
}
