﻿using System;
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
    public class mg_OperatorDAL
    {


        public static int AddOperatorByName(string name, string rfid, int stid, int isoperator, string pic)
        {
            string sql = @"INSERT INTO [mg_Operator] ([op_name],[op_no],[st_id],[op_isoperator],[op_pic]) VALUES ('" + name + "','" + rfid + "'," + stid + "," + isoperator + ",'" + pic + "')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetAllData()
        {
            string sql = @"select o.*,s.st_name,case o.op_isoperator when 1 then 'True' else 'False' end isoperator from [mg_Operator] o left join [mg_station] s on o.st_id = s.st_id order by op_id";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int UpdateOperatorByName(int id, string name, string rfid, int stid, int isoperator, string pic)
        {
            string sql = @"update [mg_Operator] set op_name = '" + name + "',op_no='" + rfid + "',op_pic='" + pic + "',op_isoperator=" + isoperator + ",st_id=" + stid + " where op_id='" + id + "'";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int DelOperatorByName(int user_id)
        {
            string sql = @"delete from [mg_Operator] where op_id=" + user_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int CheckOperatorByName(int a, int userid, string name)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"select * from [mg_Operator] where op_name='" + name + "'";
            }
            if (a == 2)
            {
                sql = @"select * from [mg_Operator] where op_name='" + name + "' and op_id <>" + userid;
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

        public static DataTable GetStName()
        {
            string sql = @"select st_id,st_name from [mg_station] order by [st_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static bool CheckPicName(string name)
        {
            string sql = @"select count(*) from [mg_Operator] where op_pic='" + name + "'";
            DataTable tb;

            tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
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

        public static string GetMaxNO()
        {
            string maxid = @"declare @i int;
                                SELECT @i=max([op_id])  FROM [mg_Operator];
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

        public static int AddOperator(mg_OperatorModel model)
        {
            string maxid = @"declare @i int;
                                SELECT @i=max([op_id])  FROM [mg_Operator];
                                if @i is null
                                    begin
                                        set @i=1
                                    end
                                else
                                    begin
                                        set @i=@i+1
                                    end;
                                ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into mg_Operator(");
            strSql.Append("op_id,st_id,op_name,op_no,op_pic,op_isoperator,op_sex)");
            strSql.Append(" values (");
            strSql.Append("@i,@st_id,@op_name,@op_no,@op_pic,@op_isoperator,@op_sex);");
            SqlParameter[] parameters = {
					new SqlParameter("@st_id", SqlDbType.Int),
					new SqlParameter("@op_name", SqlDbType.NVarChar),
					new SqlParameter("@op_no", SqlDbType.NVarChar),
					new SqlParameter("@op_pic", SqlDbType.VarChar),
					new SqlParameter("@op_isoperator", SqlDbType.Int),
					new SqlParameter("@op_sex", SqlDbType.Int)
                                        };
            parameters[0].Value = model.st_id;
            parameters[1].Value = model.op_name;
            parameters[2].Value = model.op_no;
            parameters[3].Value = model.op_pic;
            parameters[4].Value = model.op_isoperator;
            parameters[5].Value = model.op_sex;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql, parameters);
            return rows;
        }
        public static int UpdateOperator(mg_OperatorModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_Operator set ");
            strSql.Append("st_id=@st_id,");
            strSql.Append("op_name=@op_name,");
            strSql.Append("op_no=@op_no,");
            strSql.Append("op_pic=@op_pic,");
            strSql.Append("op_isoperator=@op_isoperator,");
            strSql.Append("op_sex=@op_sex");
            strSql.Append(" where op_id=@op_id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@st_id", SqlDbType.Int),
					new SqlParameter("@op_name", SqlDbType.NVarChar),
					new SqlParameter("@op_no", SqlDbType.NVarChar),
					new SqlParameter("@op_pic", SqlDbType.VarChar),
					new SqlParameter("@op_isoperator", SqlDbType.Int),
					new SqlParameter("@op_sex", SqlDbType.Int),
					new SqlParameter("@op_id", SqlDbType.Int)
                                        };
            parameters[0].Value = model.st_id;
            parameters[1].Value = model.op_name;
            parameters[2].Value = model.op_no;
            parameters[3].Value = model.op_pic;
            parameters[4].Value = model.op_isoperator;
            parameters[5].Value = model.op_sex;
            parameters[6].Value = model.op_id;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }



        public static List<mg_OperatorModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_OperatorModel> list = null;

            string sql1 = @"select count(op_id) total from [mg_Operator];";
            string sql2 = @" SELECT top " + pagesize + @" [op_id]
                                  ,op.[st_id]
                                  ,st.st_name
                                  ,[op_name]
                                  ,[op_no]
                                  ,[op_pic]
                                  ,[op_isoperator]
	                              ,case [op_isoperator]
		                            when 1 then '是'
		                            else '否'
		                            end op_isoperator_name
                                  ,[op_mac]
                                  ,[op_sex]
	                               ,case [op_sex]
		                            when 1 then '男'
		                            else '女'
		                            end op_sex_name
                              FROM [mg_Operator] op 
                              left join mg_station st on op.st_id=st.st_id
                                where  op.op_id < (
                                                select top 1 op_id from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") op_id from  [mg_Operator] where op_id is not null  order by op_id desc )t
                                                order by  op_id  )
                              order by op_id desc";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_OperatorModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_OperatorModel model = new mg_OperatorModel();

                    model.op_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "op_id"));
                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    model.st_name = DataHelper.GetCellDataToStr(row, "st_name");
                    model.op_name = DataHelper.GetCellDataToStr(row, "op_name");
                    string no = DataHelper.GetCellDataToStr(row, "op_no");
                    model.op_no = no.Substring(no.IndexOf("A") + 1);
                    model.op_pic = DataHelper.GetCellDataToStr(row, "op_pic");
                    model.op_isoperator = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "op_isoperator"));
                    model.op_isoperator_name = DataHelper.GetCellDataToStr(row, "op_isoperator_name");
                    model.op_mac = DataHelper.GetCellDataToStr(row, "op_mac");
                    model.op_sex = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "op_sex"));
                    model.op_sex_name = DataHelper.GetCellDataToStr(row, "op_sex_name");

                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_OperatorModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<mg_OperatorModel> list = null;

            string sql1 = @"select count(op_id) total from [mg_Operator];";
            string sql2 = @" SELECT top " + pagesize + @" [op_id]
                                  ,op.[st_id]
                                  ,st.st_name
                                  ,[op_name]
                                  ,[op_no]
                                  ,[op_pic]
                                  ,[op_isoperator]
	                              ,case [op_isoperator]
		                            when 1 then '是'
		                            else '否'
		                            end op_isoperator_name
                                  ,[op_mac]
                                  ,[op_sex]
	                               ,case [op_sex]
		                            when 1 then '男'
		                            else '女'
		                            end op_sex_name
                              FROM [mg_Operator] op 
                              left join mg_station st on op.st_id=st.st_id
                              order by op_id desc";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_OperatorModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_OperatorModel model = new mg_OperatorModel();

                    model.op_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "op_id"));
                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    model.st_name = DataHelper.GetCellDataToStr(row, "st_name");
                    model.op_name = DataHelper.GetCellDataToStr(row, "op_name");
                    string no = DataHelper.GetCellDataToStr(row, "op_no");
                    model.op_no = no.Substring(no.Length-7);
                    model.op_pic = DataHelper.GetCellDataToStr(row, "op_pic");
                    model.op_isoperator = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "op_isoperator"));
                    model.op_isoperator_name = DataHelper.GetCellDataToStr(row, "op_isoperator_name");
                    model.op_mac = DataHelper.GetCellDataToStr(row, "op_mac");
                    model.op_sex = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "op_sex"));
                    model.op_sex_name = DataHelper.GetCellDataToStr(row, "op_sex_name");

                    list.Add(model);
                }
            }
            return list;
        }
        public static List<mg_OperatorModel> QueryOperatorList()
        {
            return null;
        }

        public static int DeleteOperator(string op_id)
        {

            string sql = "DELETE FROM [mg_Operator] WHERE op_id=" + op_id;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            return rows;
        }


        public static mg_OperatorModel GetOperaterForCardNO(string cardNO)
        {

            string sql = @" SELECT [op_id]
                                  ,[op_name]
                                  ,[op_no]
                                  ,[op_pic]
                                  ,[op_mac]
                                  ,[op_isoperator]
                                  ,op.[st_id]
                                  ,st.st_no
                              FROM [mg_Operator] op
                              left join mg_station st on op.st_id = st.st_id
                            where op_no='" + cardNO.Replace("\0","") + "' ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                foreach (DataRow row in dt.Rows)
                {
                    mg_OperatorModel model = new mg_OperatorModel();
                    model.op_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "op_id"));
                    model.op_isoperator = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "op_isoperator"));
                    model.op_name = DataHelper.GetCellDataToStr(row, "op_name");
                    model.op_no = DataHelper.GetCellDataToStr(row, "op_no");
                    model.op_pic = DataHelper.GetCellDataToStr(row, "op_pic");
                    model.st_no = DataHelper.GetCellDataToStr(row, "st_no");
                    model.op_mac = DataHelper.GetCellDataToStr(row, "op_mac");
                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    return model;
                }
            }
            return null;
        }
    }
}

