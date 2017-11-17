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
    public class mg_UserDAL
    {

        public static Model.mg_userModel GetUserForUID(string uid)
        {

            string sql = @" select * from mg_User where user_id='" + uid + "' ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                foreach (DataRow row in dt.Rows)
                {
                    mg_userModel model = new mg_userModel();
                    model.user_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_id"));
                    model.user_pwd = DataHelper.GetCellDataToStr(row, "user_pwd");
                    model.user_name = DataHelper.GetCellDataToStr(row, "user_name");
                    model.user_no = DataHelper.GetCellDataToStr(row, "user_no");
                    model.user_pic = DataHelper.GetCellDataToStr(row, "user_pic");
                    model.user_email = DataHelper.GetCellDataToStr(row, "user_email");
                    model.user_depid = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "user_depid"));
                    model.user_posiid = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "user_posiid"));
                    model.user_menuids = DataHelper.GetCellDataToStr(row, "user_menuids");
                    return model;
                }
            }
            return null;
        }

        /// <summary>
        /// 读取用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static Model.mg_userModel GetUserForUName(string uname)
        {
            //string sql = @" select * from mg_User where user_name='" + uname + "' ";
            //DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            //if (DataHelper.HasData(dt))
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        mg_userModel model = new mg_userModel();
            //        model.user_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_id"));
            //        model.user_pwd = DataHelper.GetCellDataToStr(row, "user_pwd");
            //        model.user_name = uname;
            //        model.user_no = DataHelper.GetCellDataToStr(row, "user_no");
            //        model.user_pic = DataHelper.GetCellDataToStr(row, "user_pic");
            //        model.user_email = DataHelper.GetCellDataToStr(row, "user_email");
            //        model.user_depid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_depid"));
            //        model.user_posiid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_posiid"));
            //        model.user_menuids = DataHelper.GetCellDataToStr(row, "user_menuids");
            //        return model;
            //    }
            //}
            //return null;

            string sql = @" SELECT  [user_id]
                                      ,user_pwd
                                      ,[user_name]
                                      ,[user_email]
                                      ,[user_depid]
                                      ,[user_posiid]
                                     ,d.dep_name user_depid_name
                                      ,p.posi_name user_posiid_name
                                      ,[user_menuids]
	  	                                  ,case [user_sex]
		                                                            when 1 then '男'
		                                                            else '女'
		                                                            end user_sex_name
                                      ,[user_sex]
	                                  ,case [user_isAdmin]
		                                                            when 1 then '是'
		                                                            else '否'
		                                                            end user_isAdmin_name
                                      ,[user_isAdmin]
                                  FROM [mg_User] u
                                  left join mg_Department d on u.user_depid = d.dep_id
                                  left join mg_Position p on u.user_posiid = p.posi_id 
                                    where user_name='" + uname +@"';
                                    ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                foreach (DataRow row in dt.Rows)
                {
                    mg_userModel model = new mg_userModel();
                    model.user_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_id"));
                    model.user_name = DataHelper.GetCellDataToStr(row, "user_name");
                    model.user_pwd = DataHelper.GetCellDataToStr(row, "user_pwd");
                    model.user_email = DataHelper.GetCellDataToStr(row, "user_email");
                    model.user_depid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_depid"));
                    model.user_posiid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_posiid"));
                    model.user_posiid_name = DataHelper.GetCellDataToStr(row, "user_posiid_name");
                    model.user_depid_name = DataHelper.GetCellDataToStr(row, "user_depid_name");
                    model.user_menuids = DataHelper.GetCellDataToStr(row, "user_menuids");
                    model.user_sex_name = DataHelper.GetCellDataToStr(row, "user_sex_name");
                    model.user_sex = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_sex"));
                    model.user_isAdmin_name = DataHelper.GetCellDataToStr(row, "user_isAdmin_name");
                    model.user_isAdmin = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_isAdmin"));
                    return model;
                }
            }
            return null;

        }
        public static int AddUserByName(string name, string pwd, string rfid, string email, int depid, int posiid, string pic, string menuids)
        {
            string sql = @"INSERT INTO [mg_User] ([user_name],[user_pwd],[user_no],[user_pic],[user_email],[user_depid],[user_posiid],[user_menuids]) VALUES ('" + name + "','" + pwd + "','" + rfid + "','" + pic + "','" + email + "'," + depid + "," + posiid + ",'" + menuids + "')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetAllData()
        {
            string sql = @"select u.*,d.dep_name,p.posi_name from [mg_User] u left join mg_Department d on u.user_depid=d.dep_id left join mg_Position p on u.user_posiid=p.posi_id order by [user_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int UpdateUserByName(int id, string name, string pwd, string rfid, string email, string menuids, int depid, int posiid, string pic)
        {
            string sql = @"update [mg_user] set user_name = '" + name + "',user_pwd='" + pwd + "',user_no='" + rfid + "',user_pic='" + pic + "',user_email='" + email + "',user_depid=" + depid + ",user_posiid=" + posiid + ",user_menuids='" + menuids + "' where user_id='" + id + "'";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int DelUserByName(int user_id)
        {
            string sql = @"delete from [mg_user] where user_id=" + user_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int CheckUserByName(int a, int userid, string name)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"select * from [mg_User] where user_name='" + name + "'";
            }
            if (a == 2)
            {
                sql = @"select * from [mg_User] where user_name='" + name + "' and user_id <>" + userid;
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

        public static DataTable GetPosiName()
        {
            string sql = @"select posi_id,posi_name from [mg_Position] order by [posi_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetDepName()
        {
            string sql = @"select dep_id,dep_name from [mg_Department] order by [dep_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static DataTable GetUserName()
        {
            string sql = @"select bom_id,bom_name from [mg_bom] order by [bom_id]";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static bool CheckPicName(string name)
        {
            string sql = @"select count(*) from [mg_user] where user_pic='" + name + "'";
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

        public static int AddUser(mg_userModel model)
        {
            string maxid = @"declare @i int;
                                SELECT @i=max([user_id])  FROM [mg_User];
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
            strSql.Append("insert into mg_User(");
            strSql.Append("user_id,user_name,user_email,user_depid,user_posiid,user_menuids,user_sex,user_isAdmin,user_pwd)");
            strSql.Append(" values (");
            strSql.Append("@i,@user_name,@user_email,@user_depid,@user_posiid,@user_menuids,@user_sex,@user_isAdmin,@user_pwd)");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.VarChar),
					new SqlParameter("@user_email", SqlDbType.VarChar),
					new SqlParameter("@user_depid", SqlDbType.Int),
					new SqlParameter("@user_posiid", SqlDbType.Int),
					new SqlParameter("@user_menuids", SqlDbType.VarChar),
					new SqlParameter("@user_sex", SqlDbType.Int),
					new SqlParameter("@user_isAdmin", SqlDbType.Int),
					new SqlParameter("@user_pwd", SqlDbType.VarChar)
                                        };
            parameters[0].Value = model.user_name;
            parameters[1].Value = model.user_email;
            parameters[2].Value = model.user_depid;
            parameters[3].Value = model.user_posiid;
            parameters[4].Value = model.user_menuids;
            parameters[5].Value = model.user_sex;
            parameters[6].Value = model.user_isAdmin;
            parameters[7].Value = model.user_pwd;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql.ToString(), parameters);
            return rows;
        }


        public static List<mg_userModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<mg_userModel> list = null;

            string sql1 = @"select count(user_id) total from [mg_User];";
            string sql2 = @" SELECT top " + pagesize + @" [user_id]
                                      ,[user_name]
                                      ,[user_pwd]
                                      ,[user_email]
                                      ,[user_depid]
                                      ,[user_posiid]
                                     ,d.dep_name user_depid_name
                                      ,p.posi_name user_posiid_name
                                      ,[user_menuids]
	  	                                  ,case [user_sex]
		                                                            when 1 then '男'
		                                                            else '女'
		                                                            end user_sex_name
                                      ,[user_sex]
	                                  ,case [user_isAdmin]
		                                                            when 1 then '是'
		                                                            else '否'
		                                                            end user_isAdmin_name
                                      ,[user_isAdmin]
                                  FROM [mg_User] u
                                  left join mg_Department d on u.user_depid = d.dep_id
                                  left join mg_Position p on u.user_posiid = p.posi_id

                                  order by [user_id] desc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_userModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_userModel model = new mg_userModel();

                    model.user_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_id"));
                    model.user_name = DataHelper.GetCellDataToStr(row, "user_name");
                    model.user_pwd = DataHelper.GetCellDataToStr(row, "user_pwd");
                    model.user_email = DataHelper.GetCellDataToStr(row, "user_email");
                    model.user_depid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_depid"));
                    model.user_posiid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_posiid"));
                    model.user_posiid_name = DataHelper.GetCellDataToStr(row, "user_posiid_name");
                    model.user_depid_name = DataHelper.GetCellDataToStr(row, "user_depid_name");
                    model.user_menuids = DataHelper.GetCellDataToStr(row, "user_menuids");
                    model.user_sex_name = DataHelper.GetCellDataToStr(row, "user_sex_name");
                    model.user_sex = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_sex"));
                    model.user_isAdmin_name = DataHelper.GetCellDataToStr(row, "user_isAdmin_name");
                    model.user_isAdmin = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_isAdmin"));

                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_userModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_userModel> list = null;

            string sql1 = @"select count(user_id) total from [mg_User];";
            string sql2 = @" SELECT top " + pagesize + @" [user_id]
                                      ,[user_name]
                                      ,[user_pwd]
                                      ,[user_email]
                                      ,[user_depid]
                                      ,[user_posiid]
                                      ,d.dep_name user_depid_name
                                      ,p.posi_name user_posiid_name
                                      ,[user_menuids]
	  	                                  ,case [user_sex]
		                                                            when 1 then '男'
		                                                            else '女'
		                                                            end user_sex_name
                                      ,[user_sex]
	                                  ,case [user_isAdmin]
		                                                            when 1 then '是'
		                                                            else '否'
		                                                            end user_isAdmin_name
                                      ,[user_isAdmin]
                                  FROM [mg_User] u
                                  left join mg_Department d on u.user_depid = d.dep_id
                                  left join mg_Position p on u.user_posiid = p.posi_id
                                    where  u.user_id < (
                                                select top 1 user_id from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") user_id from  [mg_User] where user_id is not null  order by user_id desc )t
                                                order by  user_id  )
                                  order by [user_id] desc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_userModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_userModel model = new mg_userModel();

                    model.user_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_id"));
                    model.user_name = DataHelper.GetCellDataToStr(row, "user_name");
                    model.user_pwd = DataHelper.GetCellDataToStr(row, "user_pwd");
                    model.user_email = DataHelper.GetCellDataToStr(row, "user_email");
                    model.user_depid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_depid"));
                    model.user_posiid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_posiid"));
                    model.user_posiid_name = DataHelper.GetCellDataToStr(row, "user_posiid_name");
                    model.user_depid_name = DataHelper.GetCellDataToStr(row, "user_depid_name");
                    model.user_menuids = DataHelper.GetCellDataToStr(row, "user_menuids");
                    model.user_sex_name = DataHelper.GetCellDataToStr(row, "user_sex_name");
                    model.user_sex = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_sex"));
                    model.user_isAdmin_name = DataHelper.GetCellDataToStr(row, "user_isAdmin_name");
                    model.user_isAdmin = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "user_isAdmin"));

                    list.Add(model);
                }
            }
            return list;
        }

        public static int UpdateUser(mg_userModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update mg_User set ");
            strSql.Append("user_name=@user_name,");
            strSql.Append("user_pwd=@user_pwd,");
            strSql.Append("user_email=@user_email,");
            strSql.Append("user_depid=@user_depid,");
            strSql.Append("user_posiid=@user_posiid,");
            strSql.Append("user_menuids=@user_menuids,");
            strSql.Append("user_sex=@user_sex,");
            strSql.Append("user_isAdmin=@user_isAdmin");
            strSql.Append(" where user_id=@user_id ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.VarChar),
                    new SqlParameter("@user_pwd", SqlDbType.VarChar),
					new SqlParameter("@user_email", SqlDbType.VarChar),
					new SqlParameter("@user_depid", SqlDbType.Int),
					new SqlParameter("@user_posiid", SqlDbType.Int),
					new SqlParameter("@user_menuids", SqlDbType.VarChar),
					new SqlParameter("@user_sex", SqlDbType.Int),
					new SqlParameter("@user_isAdmin", SqlDbType.Int),
					new SqlParameter("@user_id", SqlDbType.Int)
                                        };
            parameters[0].Value = model.user_name;
            parameters[1].Value = model.user_pwd;
            parameters[2].Value = model.user_email;
            parameters[3].Value = model.user_depid;
            parameters[4].Value = model.user_posiid;
            parameters[5].Value = model.user_menuids;
            parameters[6].Value = model.user_sex;
            parameters[7].Value = model.user_isAdmin;
            parameters[8].Value = model.user_id;
            string a = strSql.ToString();
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text,  strSql.ToString(), parameters);
            return rows;
        }

        public static int DeleteUser(string user_id)
        {
            string sql = "DELETE FROM [mg_User] WHERE user_id=" + user_id;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            return rows;
        }
    }
}

