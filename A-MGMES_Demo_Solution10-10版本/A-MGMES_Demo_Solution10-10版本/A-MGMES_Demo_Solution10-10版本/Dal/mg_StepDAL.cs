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
    public class mg_StepDAL
    {
        public static int AddStepByName(string name, int clock, string desc, string pic, int stid, int bomid, int bomcount)
        {
            string sql = @"INSERT INTO [mg_step] ([step_name],[step_clock],[step_desc],[step_pic],[st_id],[bom_id],[bom_count]) VALUES ('" + name + "'," + clock + ",'" + desc + "','" + pic + "'," + stid + "," + bomid + "," + bomcount + ")";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetAllData()
        {
            string sql = @"select s1.*,s2.st_name from [mg_step] s1 left join [mg_station] s2 on s1.st_id = s2.st_id left join [mg_BOM] b on s1.bom_id = b.bom_id order by [step_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int UpdateStepByName(int step_id, string step_name, int step_clock, string step_desc, string step_pic, int stid, int bomid, int count)
        {
            string sql = @"update [mg_step] set step_name='" + step_name + "',step_clock=" + step_clock + ",step_desc='" + step_desc + "',Step_pic='" + step_pic + "',st_id=" + stid + ",bom_id=" + bomid + ",bom_count=" + count + " where step_id=" + step_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int DelStepByName(int step_id)
        {
            string sql = @"delete from [mg_step] where step_id=" + step_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int CheckStepByName(int a, int stepid, string name)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"select * from [mg_step] where step_name='" + name + "'";
            }
            if (a == 2)
            {
                sql = @"select * from [mg_step] where step_name='" + name + "' and step_id <>" + stepid;
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

        public static DataTable GetStationID()
        {
            string sql = @"select st_id,st_name from [mg_station] order by [st_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetBomName()
        {
            string sql = @"select bom_id from [mg_bom] order by [bom_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static bool CheckPicName(string name)
        {
            string sql = @"select count(*) from [mg_step] where Step_pic='" + name + "'";
            DataTable tb;

            tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            string zz = tb.Rows[0][0].ToString();
            if (tb.Rows[0][0].ToString() == "0")
            {
                tb.Dispose();
                return false;
            }
            else
            {
                tb.Dispose();
                return true;
            }
        }


        /*
     * 
     *      姜任鹏
     * 
     */


        public static int AddStepAndODS(mg_StepModel model)
        {
            string maxid = @"declare @i int;declare @o int;
                                SELECT @i=max([step_id])  FROM [mg_step];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                SELECT @o=max([step_order])  FROM [mg_step];
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
            strSql.Append("insert into mg_step(");
            strSql.Append("step_id,step_name,fl_id,st_id,bom_id,bom_count,step_clock,step_desc,step_pic,step_plccode,step_order,part_id)");
            strSql.Append(" values (");
            strSql.Append("@i,@step_name,@fl_id,@st_id,@bom_id,@bom_count,@step_clock,@step_desc,@step_pic,@step_plccode,@o,@part_id);");
            SqlParameter[] parameters = {
					new SqlParameter("@step_name", SqlDbType.NVarChar),
					new SqlParameter("@fl_id", SqlDbType.Int),
					new SqlParameter("@st_id", SqlDbType.Int),
					new SqlParameter("@bom_id", SqlDbType.Int),
					new SqlParameter("@bom_count", SqlDbType.Int),
					new SqlParameter("@step_clock", SqlDbType.Int),
					new SqlParameter("@step_desc", SqlDbType.NVarChar),
					new SqlParameter("@step_pic", SqlDbType.NVarChar),
					new SqlParameter("@step_plccode", SqlDbType.Int),
					new SqlParameter("@part_id", SqlDbType.Int)
                                        };
            parameters[0].Value = model.step_name;
            parameters[1].Value = model.fl_id;
            parameters[2].Value = model.st_id;
            parameters[3].Value = model.bom_id;
            parameters[4].Value = model.bom_count;
            parameters[5].Value = model.step_clock;
            parameters[6].Value = model.step_desc;
            parameters[7].Value = model.step_pic;
            parameters[8].Value = model.step_plccode;
            parameters[9].Value = model.part_id;

            if (!string.IsNullOrEmpty(model.odsName))
            {
                string[] odsnameArr = model.odsName.Split('|');
                string[] odskeyArr = model.odsKeyword.Split('|');
                string odsStr = @"INSERT INTO [mg_ODS]
                                               ([ods_id]
                                               ,[step_id]
                                               ,[ods_name]
                                               ,[ods_keywords])
                                         VALUES
                                               ($ods_id$
                                               ,$step_id$
                                               ,'$ods_name$'
                                               ,'$ods_keywords$');";
                for (int i = 0; i < odsnameArr.Length; i++)
                {
                    string key = "";
                    if ((i + 1) <= odskeyArr.Length)
                    {
                        key = odskeyArr[i];
                    }
                    strSql.Append(odsStr.Replace("$ods_id$", (i + 1).ToString()).Replace("$step_id$", "@i").Replace("$ods_name$", odsnameArr[i]).Replace("$ods_keywords$", key));
                }
            }

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql, parameters);

            return rows;
        }

        public static int AddStep(mg_StepModel model)
        {
            string maxid = @"declare @i int;declare @o int;
                                SELECT @i=max([step_id])  FROM [mg_step];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                SELECT @o=max([step_order])  FROM [mg_step];
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
            strSql.Append("insert into mg_step(");
            strSql.Append("step_id,step_name,fl_id,st_id,bom_id,bom_count,step_clock,step_desc,step_pic,step_plccode,step_order,part_id,barcode_start,barcode_number)");
            strSql.Append(" values (");
            strSql.Append("@i,@step_name,@fl_id,@st_id,@bom_id,@bom_count,@step_clock,@step_desc,@step_pic,@step_plccode,@o,@part_id,@barcode_start,@barcode_number)");
            SqlParameter[] parameters = {
					new SqlParameter("@step_name", SqlDbType.NVarChar),
					new SqlParameter("@fl_id", SqlDbType.Int),
					new SqlParameter("@st_id", SqlDbType.Int),
					new SqlParameter("@bom_id", SqlDbType.Int),
					new SqlParameter("@bom_count", SqlDbType.Int),
					new SqlParameter("@step_clock", SqlDbType.Int),
					new SqlParameter("@step_desc", SqlDbType.NVarChar),
					new SqlParameter("@step_pic", SqlDbType.NVarChar),
					new SqlParameter("@step_plccode", SqlDbType.Int),
					new SqlParameter("@barcode_start", SqlDbType.Int),
					new SqlParameter("@barcode_number", SqlDbType.Int),
					new SqlParameter("@part_id", SqlDbType.Int)
                                        };
            parameters[0].Value = model.step_name;
            parameters[1].Value = model.fl_id;
            parameters[2].Value = model.st_id;
            parameters[3].Value = model.bom_id;
            parameters[4].Value = model.bom_count;
            parameters[5].Value = model.step_clock;
            parameters[6].Value = model.step_desc;
            parameters[7].Value = model.step_pic;
            parameters[8].Value = model.step_plccode;
            parameters[9].Value = model.barcode_start;
            parameters[10].Value = model.barcode_number;
            parameters[11].Value = model.part_id;


            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql, parameters);
            return rows;
        }

        public static int UpdateStepAndODS(Model.mg_StepModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_step set ");
            strSql.Append("step_name=@step_name,");
            strSql.Append("fl_id=@fl_id,");
            strSql.Append("st_id=@st_id,");
            strSql.Append("bom_id=@bom_id,");
            strSql.Append("part_id=@part_id,");
            strSql.Append("bom_count=@bom_count,");
            strSql.Append("step_clock=@step_clock,");
            strSql.Append("step_desc=@step_desc,");
            strSql.Append("step_pic=@step_pic,");
            strSql.Append("step_plccode=@step_plccode");
            strSql.Append(" where step_id=@step_id ;");
            SqlParameter[] parameters = {
					new SqlParameter("@step_id", SqlDbType.Int),
					new SqlParameter("@step_name", SqlDbType.NVarChar),
					new SqlParameter("@fl_id", SqlDbType.Int),
					new SqlParameter("@st_id", SqlDbType.Int),
					new SqlParameter("@bom_id", SqlDbType.Int),
					new SqlParameter("@bom_count", SqlDbType.Int),
					new SqlParameter("@step_clock", SqlDbType.Int),
					new SqlParameter("@step_desc", SqlDbType.NVarChar),
					new SqlParameter("@step_pic", SqlDbType.NVarChar),
					new SqlParameter("@step_plccode", SqlDbType.Int),
					new SqlParameter("@part_id", SqlDbType.Int)
                                        };
            parameters[0].Value = model.step_id;
            parameters[1].Value = model.step_name;
            parameters[2].Value = model.fl_id;
            parameters[3].Value = model.st_id;
            parameters[4].Value = model.bom_id;
            parameters[5].Value = model.bom_count;
            parameters[6].Value = model.step_clock;
            parameters[7].Value = model.step_desc;
            parameters[8].Value = model.step_pic;
            parameters[9].Value = model.step_plccode;
            parameters[10].Value = model.part_id;


            if (!string.IsNullOrEmpty(model.odsName))
            {
                strSql.Append(" delete from mg_ODS where step_id=" + model.step_id+";");
                string[] odsnameArr = model.odsName.Split('|');
                string[] odskeyArr = model.odsKeyword.Split('|');
                string odsStr = @"INSERT INTO [mg_ODS]
                                               ([ods_id]
                                               ,[step_id]
                                               ,[ods_name]
                                               ,[ods_keywords])
                                         VALUES
                                               ($ods_id$
                                               ,$step_id$
                                               ,'$ods_name$'
                                               ,'$ods_keywords$');";
                for (int i = 0; i < odsnameArr.Length; i++)
                {
                    string key = "";
                    if ((i + 1) <= odskeyArr.Length)
                    {
                        key = odskeyArr[i];
                    }
                    strSql.Append(odsStr.Replace("$ods_id$", (i+1).ToString()).Replace("$step_id$", model.step_id.ToString()).Replace("$ods_name$", odsnameArr[i]).Replace("$ods_keywords$", key));
                }
            }

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static int UpdateStep(Model.mg_StepModel model) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_step set ");
            strSql.Append("step_name=@step_name,");
            strSql.Append("fl_id=@fl_id,");
            strSql.Append("st_id=@st_id,");
            strSql.Append("bom_id=@bom_id,");
            strSql.Append("part_id=@part_id,");
            strSql.Append("bom_count=@bom_count,");
            strSql.Append("step_clock=@step_clock,");
            strSql.Append("step_desc=@step_desc,");
            strSql.Append("step_pic=@step_pic,");
            strSql.Append("barcode_start=@barcode_start,");
            strSql.Append("barcode_number=@barcode_number,");
            strSql.Append("step_plccode=@step_plccode");
            strSql.Append(" where step_id=@step_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@step_id", SqlDbType.Int),
					new SqlParameter("@step_name", SqlDbType.NVarChar),
					new SqlParameter("@fl_id", SqlDbType.Int),
					new SqlParameter("@st_id", SqlDbType.Int),
					new SqlParameter("@bom_id", SqlDbType.Int),
					new SqlParameter("@bom_count", SqlDbType.Int),
					new SqlParameter("@step_clock", SqlDbType.Int),
					new SqlParameter("@step_desc", SqlDbType.NVarChar),
					new SqlParameter("@step_pic", SqlDbType.NVarChar),
					new SqlParameter("@step_plccode", SqlDbType.Int),
					new SqlParameter("@barcode_start", SqlDbType.Int),
					new SqlParameter("@barcode_number", SqlDbType.Int),
					new SqlParameter("@part_id", SqlDbType.Int)
                                        };
            parameters[0].Value = model.step_id;
            parameters[1].Value = model.step_name;
            parameters[2].Value = model.fl_id;
            parameters[3].Value = model.st_id;
            parameters[4].Value = model.bom_id;
            parameters[5].Value = model.bom_count;
            parameters[6].Value = model.step_clock;
            parameters[7].Value = model.step_desc;
            parameters[8].Value = model.step_pic;
            parameters[9].Value = model.step_plccode;
            parameters[10].Value = model.barcode_start;
            parameters[11].Value = model.barcode_number;
            parameters[12].Value = model.part_id;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static List<mg_StepModel> QueryListForPaging(string page, string pagesize, out string total, string fl_id, string st_id, string part_id)
        {
            total = "0";
            List<mg_StepModel> list = null;
            string queryStr = (!string.IsNullOrEmpty(fl_id) && fl_id != "0") ? " where step.fl_id=" + fl_id : " where step.step_id is not null ";
            queryStr += (!string.IsNullOrEmpty(st_id) && st_id != "0") ? " and step.st_id=" + st_id : " ";
            queryStr += (!string.IsNullOrEmpty(part_id) && part_id != "0") ? " and step.part_id=" + part_id : " ";
            string sql1 = @"select count(step_id) total from [mg_step] step " + queryStr + @" ;";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [step_id]
                                      ,[step_name]
                                      ,step.[fl_id]
	                                  ,fl.fl_name
                                      ,step.[st_id]
	                                  ,st.st_name
                                      ,step.[bom_id]
	                                  ,b.bom_PN
                                      ,step.[part_id]
	                                  ,p.part_no
	                                  ,p.part_name
	                                  ,b.bom_desc
                                      ,[bom_count]
                                      ,[step_clock]
                                      ,[step_desc]
                                      ,[step_pic]
                                      ,[step_plccode]
                                      ,[step_order]
                                      ,step.[barcode_start]
                                      ,step.[barcode_number]
                                  FROM [mg_step] step
                                  left join mg_FlowLine fl on step.fl_id = fl.fl_id
                                  left join mg_station st on step.st_id = st.st_id
                                  left join mg_BOM b on step.bom_id=b.bom_id
                                  left join mg_part p on step.part_id=p.part_id
                                    " + queryStr + @"
                                    and  step.step_order > (
                                                select top 1 step_order from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") step_order from  [mg_step] where step_order is not null and fl_id=" + fl_id + @" order by step_order  )t
                                                order by  step_order desc )

                                  order by step.step_order

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_StepModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_StepModel model = new mg_StepModel();

                    model.step_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "step_id"));
                    model.step_name = DataHelper.GetCellDataToStr(row, "step_name");
                    model.fl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fl_id"));
                    model.fl_name = DataHelper.GetCellDataToStr(row, "fl_name");
                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    model.st_name = DataHelper.GetCellDataToStr(row, "st_name");
                    model.bom_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_id"));
                    model.bom_PN = DataHelper.GetCellDataToStr(row, "bom_PN") + " | " + DataHelper.GetCellDataToStr(row, "bom_desc");
                    model.bom_desc = DataHelper.GetCellDataToStr(row, "bom_desc");
                    model.bom_count = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_count"));
                    model.step_clock = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "step_clock"));
                    model.step_desc = DataHelper.GetCellDataToStr(row, "step_desc");
                    model.step_pic = DataHelper.GetCellDataToStr(row, "step_pic");
                    model.step_plccode = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "step_plccode"));
                    model.step_order = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "step_order"));
                    model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.part_no = DataHelper.GetCellDataToStr(row, "part_no") + " | " + DataHelper.GetCellDataToStr(row, "part_name");
                    model.barcode_start = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "barcode_start"));
                    model.barcode_number = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "barcode_number"));
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_StepModel> QueryListForFirstPage(string pagesize, out string total, string fl_id, string st_id, string part_id)
        {
            total = "0";
            List<mg_StepModel> list = null;
            string queryStr = (!string.IsNullOrEmpty(fl_id) && fl_id != "0") ? " where step.fl_id=" + fl_id : " where step.step_id is not null ";
            queryStr += (!string.IsNullOrEmpty(st_id) && st_id != "0") ? " and step.st_id=" + st_id : " ";
            queryStr += (!string.IsNullOrEmpty(part_id) && part_id != "0") ? " and step.part_id=" + part_id : " ";
            string sql1 = @"select count(step_id) total from [mg_step] step " + queryStr + @" ;";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [step_id]
                                      ,[step_name]
                                      ,step.[fl_id]
	                                  ,fl.fl_name
                                      ,step.[st_id]
	                                  ,st.st_name
                                      ,step.[bom_id]
	                                  ,b.bom_PN
                                      ,step.[part_id]
	                                  ,p.part_no
	                                  ,p.part_name
	                                  ,b.bom_desc
                                      ,[bom_count]
                                      ,[step_clock]
                                      ,[step_desc]
                                      ,[step_pic]
                                      ,[step_plccode]
                                      ,[step_order]
                                      ,step.[barcode_start]
                                      ,step.[barcode_number]
                                  FROM [mg_step] step
                                  left join mg_FlowLine fl on step.fl_id = fl.fl_id
                                  left join mg_station st on step.st_id = st.st_id
                                  left join mg_BOM b on step.bom_id=b.bom_id
                                  left join mg_part p on step.part_id=p.part_id
                                " + queryStr + @"

                                  order by step.step_order

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_StepModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_StepModel model = new mg_StepModel();

                    model.step_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "step_id"));
                    model.step_name = DataHelper.GetCellDataToStr(row, "step_name");
                    model.fl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "fl_id"));
                    model.fl_name = DataHelper.GetCellDataToStr(row, "fl_name");
                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    model.st_name = DataHelper.GetCellDataToStr(row, "st_name");
                    model.bom_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_id"));
                    model.bom_PN = DataHelper.GetCellDataToStr(row, "bom_PN") + " | " + DataHelper.GetCellDataToStr(row, "bom_desc");
                    model.bom_desc = DataHelper.GetCellDataToStr(row, "bom_desc");
                    model.bom_count = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "bom_count"));
                    model.step_clock = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "step_clock"));
                    model.step_desc = DataHelper.GetCellDataToStr(row, "step_desc");
                    model.step_pic = DataHelper.GetCellDataToStr(row, "step_pic");
                    model.step_plccode = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "step_plccode"));
                    model.step_order = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "step_order"));
                    model.barcode_start = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "barcode_start"));
                    model.barcode_number = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "barcode_number"));
                    model.part_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "part_id"));
                    model.part_no = DataHelper.GetCellDataToStr(row, "part_no") + " | " + DataHelper.GetCellDataToStr(row, "part_name");
                    list.Add(model);
                }
            }
            return list;
        }

        public static int DeleteStep(string step_id)
        {
            string sql = @"delete from [mg_step] where [step_id]=" + step_id + "; delete from mg_ODS where step_id=" + step_id + ";";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int SortingTop(string step_id, string step_order)
        {
            string sql = @"update mg_step set step_order=step_order+1 where step_order>=" + step_order + ";update mg_step set step_order=" + step_order + " where step_id=" + step_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int SortingBottom(string step_id, string step_order)
        {
            string sql = @"update mg_step set step_order=step_order+1 where step_order>" + step_order + ";update mg_step set step_order=" + step_order + "+1 where step_id=" + step_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataSet QueryAllData()
        {
            string sql = @"if exists(select * from tempdb..sysobjects where id=object_id('tempdb..#temp')) 
	                            drop table #temp;


                            with data as
                            (
                            SELECT 
                                  step.[fl_id]
	                              ,fl.fl_name

	                             ,prop.Prop_type
	                             ,p.part_categoryid 
	                             ,prop.prop_name
	                             ,step.[part_id] 
	                             ,p.part_no
	                             ,p.part_name 

	                               ,step.[st_id]
	                              ,st.st_no
	                              ,st.st_name
	                              ,st.st_typeid
                                    ,st_order
	                              ,prop1.prop_name st_typename

                                  ,step.[bom_id]
	                              ,bom.bom_PN
                                    ,bom.bom_customerPN
								  ,case bom.bom_isCustomerPN
								  when 0 then bom.bom_PN
								  when 1 then  bom.bom_customerPN
								  end bom_barcode
	                              ,bom.bom_storeid
	                              ,prop2.prop_name bom_storename

	                              ,step.bom_count
	                              ,step.step_clock
	                              ,[step_id]
                                  ,[step_name]
	                              ,[step_plccode]
 ,step_desc
                                ,step_pic

                                  ,[step_order]
                                  ,step.[barcode_start]
                                  ,step.[barcode_number]
                              FROM [mg_step] step
                              left join mg_FlowLine fl on step.fl_id = fl.fl_id
                              left join mg_station st on st.st_id=step.st_id
                              left join mg_part p on p.part_id=step.part_id
                              left join mg_Property prop on prop.prop_id = p.part_categoryid
                              left join mg_Property prop1 on prop1.prop_id = st.st_typeid 
                              left join mg_BOM bom on bom.bom_id = step.bom_id
                              left join mg_Property prop2 on prop2.prop_id = bom.bom_storeid
                              )select * into #temp from data  


                               select distinct part_categoryid,part_id,part_name,part_no from #temp order by part_categoryid,part_id;
                               select distinct part_categoryid,part_id,part_name,st_id,st_no,st_typeid,st_order from #temp order by part_categoryid,part_id,st_order;
                               --select distinct part_categoryid,part_id,part_name,st_id,st_no,st_typeid,st_order,step_id,step_order  from #temp order by part_categoryid,part_id,st_order,step_order;

                               select * from #temp order by part_categoryid,part_id,st_order,step_order;

                              drop table #temp";

            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, CommandType.Text, sql, new string[] { "part", "station", "step" }, null);
            return ds;
        }

        public static DataTable GetAllStepForPartAndStation(int fl_id, int st_id, int part_id)
        {
            string sql = @" with data as
                            (
                            SELECT 
                                  step.[fl_id]
	                              ,fl.fl_name

	                             ,prop.Prop_type
	                             ,p.part_categoryid 
	                             ,prop.prop_name
	                             ,step.[part_id] 
	                             ,p.part_no
	                             ,p.part_name 

	                               ,step.[st_id]
	                              ,st.st_no
	                              ,st.st_name
	                              ,st.st_typeid
                                    ,st_order
	                              ,prop1.prop_name st_typename

                                  ,step.[bom_id]
	                              ,bom.bom_PN
                                    ,bom.bom_customerPN
								  ,case bom.bom_isCustomerPN
								  when 1 then bom.bom_PN
								  when 0 then  bom.bom_customerPN
								  end bom_barcode
	                              ,bom.bom_storeid
	                              ,prop2.prop_name bom_storename
                                    ,bom.bom_desc
                                    ,bom_picture
    
	                              ,step.bom_count
	                              ,[step_id]
                                  ,[step_name]
	                              ,[step_plccode]
                                ,step_desc
                                  ,[step_order]
						  ,'未扫码' step_scanCode
								   ,case Step_matchResult
								  when 1 then '匹配'
								  when 0 then  '不匹配'
								  end Step_matchResult

                              FROM [mg_step] step
                              left join mg_FlowLine fl on step.fl_id = fl.fl_id
                              left join mg_station st on st.st_id=step.st_id
                              left join mg_part p on p.part_id=step.part_id
                              left join mg_Property prop on prop.prop_id = p.part_categoryid
                              left join mg_Property prop1 on prop1.prop_id = st.st_typeid 
                              left join mg_BOM bom on bom.bom_id = step.bom_id
                              left join mg_Property prop2 on prop2.prop_id = bom.bom_storeid
                              )select  ROW_NUMBER() over( order by step_order) stepOrder_1,*  from data  
							 where fl_id = " + fl_id + " and part_id=" + part_id + " and st_id=" + st_id + @"
							 order by step_order";

            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            return dt;
        }

        public static DataTable GetAllStepForPartAndStation(string st_ids, string partno)
        {
            string sql = @" with data as
                            (
                            SELECT 
                                  step.[fl_id]
	                              ,fl.fl_name

	                             ,prop.Prop_type
	                             ,p.part_categoryid 
	                             ,prop.prop_name
	                             ,step.[part_id] 
	                             ,p.part_no
	                             ,p.part_name 

	                               ,step.[st_id]
	                              ,st.st_no
	                              ,st.st_name
	                              ,st.st_typeid
                                    ,st_order
	                              ,prop1.prop_name st_typename

                                  ,step.[bom_id]
	                              ,bom.bom_PN
                                    ,bom.bom_customerPN
								  ,case bom.bom_isCustomerPN
								  when 1 then bom.bom_PN
								  when 0 then  bom.bom_customerPN
								  end bom_barcode
	                              ,bom.bom_storeid
	                              ,prop2.prop_name bom_storename
                                    ,bom.bom_desc
                                    ,bom_picture
    
	                              ,step.bom_count
	                              ,step.[step_id]
                                  ,[step_name]
	                              ,[step_plccode]
                                ,step_desc
  ,step_pic
                                  ,[step_order]
						  ,'未扫码' step_scanCode
								   ,case Step_matchResult
								  when 1 then '匹配'
								  when 0 then  '不匹配'
								  end Step_matchResult
                                   -- ,ods.ods_id
								 -- ,ods.ods_name
								 -- ,ods.ods_keywords
                              FROM [mg_step] step
                              left join mg_FlowLine fl on step.fl_id = fl.fl_id
                              left join mg_station st on st.st_id=step.st_id
                              left join mg_part p on p.part_id=step.part_id
                              left join mg_Property prop on prop.prop_id = p.part_categoryid
                              left join mg_Property prop1 on prop1.prop_id = st.st_typeid 
                              left join mg_BOM bom on bom.bom_id = step.bom_id
                              left join mg_Property prop2 on prop2.prop_id = bom.bom_storeid
							--  left join mg_ODS ods on ods.step_id=step.step_id
                              )select  ROW_NUMBER() over( order by step_order) stepOrder_1,*  from data  
							 where  part_no='" + partno + "' and st_id in (" + st_ids + @")
							 order by step_order";

            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            return dt;
        }

        public static string GetIDsByMac(string mac)
        {
            string sql = @"SELECT stuff((select ',' +CAST([st_id] as varchar(10))  from [mg_station] where st_mac = '" + mac + "' for xml path('')),1,1,'')as ids";
            object str = SqlHelper.ExecuteScalar(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            if (str != null)
            {
                return str.ToString();
            }
            return null;
        }

        public static List<mg_ODSModel> QueryODSByStepid(string step_id)
        {
            List<mg_ODSModel> list = null;
            string sql2 = @" 
                            SELECT [ods_name]
                                      ,[ods_keywords]
     
                                  FROM [mg_ODS]
                                  where step_id=" + step_id + @"
                                  order by ods_id

                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql2, null);
            if (DataHelper.HasData(dt))
            {
                list = new List<mg_ODSModel>();
                foreach (DataRow row in dt.Rows)
                {
                    mg_ODSModel model = new mg_ODSModel();

                    model.ods_name = DataHelper.GetCellDataToStr(row, "ods_name");
                    model.ods_keywords = DataHelper.GetCellDataToStr(row, "ods_keywords");

                    list.Add(model);
                }
            }
            return list;
        }
    }
}
