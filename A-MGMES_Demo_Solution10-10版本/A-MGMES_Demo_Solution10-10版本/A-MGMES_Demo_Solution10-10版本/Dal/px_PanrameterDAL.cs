using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Tools;
using DbUtility;
using Model;
using SortManagent.SortDao;

namespace DAL
{
    public class px_PanrameterDAL
    {
       
        public static int AddAllByName(string all_no, string all_ratename, string all_colorname,string all_metaname,string all_desc)
        {
            string sql = @"INSERT INTO [mg_allpart] ([all_no],[all_ratename],[all_colorname],[all_metaname],[all_desc]) VALUES ('" + all_no + "','" + all_ratename + "','" + all_colorname + "','" + all_metaname + "','" + all_desc + @"')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static int UpPanrameterId(int id,string name)
        {
            string sql1;
            string sql2;
            string sql3;
            int nowid = id;
            int newid = id - 1;
            sql1 = @"update [px_Panrameter] set SerialID=0 where SerialID=" + newid;
            sql2 = @"update [px_Panrameter] set SerialID=" + newid + " where [Name]='" + name + "' and SerialID=" + nowid;
            sql3 = @"update [px_Panrameter] set SerialID=" + nowid + " where SerialID=0";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql1 + sql2+ sql3, null);         
           
        }     
        public static int DownPanrameterId(int id, string name)
        {
            string sql1;
            string sql2;
            string sql3;
            int nowid = id;
            int newid = id + 1;
            sql1 = @"update [px_Panrameter] set SerialID=0 where SerialID=" + newid;
            sql2 = @"update [px_Panrameter] set SerialID=" + newid + " where [Name]='" + name + "' and SerialID=" + nowid;
            sql3 = @"update [px_Panrameter] set SerialID=" + nowid + " where SerialID=0";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql1 + sql2 + sql3, null);

        }
        public static int sendPanrameter(int id, string name)
        {

            var ss = GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == name);
            ss.IsAutoSend = !ss.IsAutoSend;          
            return 1;

        }
        public static int PrintPanrameter(int id, string name)
        {

            var ss = GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == name);
            ss.IsAutoPrint = !ss.IsAutoPrint;
            return 1;

        }
        public static int AscordescPanrameter(int id, string name)
        {

            var ss = GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == name);
            ss.Ascordesc = !ss.Ascordesc;
            return 1;

        }
        public static DataTable GetAllData()
        {
            string sql = @"SELECT [all_id],[all_no],[all_ratename],[all_colorname],[all_metaname],[all_desc] from [mg_allpart] order by [all_id] asc";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static int UpDateAllByName(int allid, string allno, string allrate, string allcolor,string allmeta,string alldesc)
        {
            string sql = @"update [mg_allpart] set [all_no]='" + allno + "',[all_ratename]='" + allrate + "',[all_colorname]= '" + allcolor + "',[all_metaname]='" + allmeta + "',[all_desc]='" + alldesc + "' where [all_id]=" + allid;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static int DelAllByName(int allid)
        {
            string sql = @"delete from [mg_allpart] where [all_id]=" + allid;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
        public static int CheckAllByName(int a, int allid, string allno)
        {
            string sql = "";
            DataTable tb;
            int i;
            if (a == 1)
            {
                sql = @"select [all_id],[all_no],[all_ratename],[all_colorname],[all_metaname],[all_desc] from [mg_allpart] where all_no='" + allno + "'";
            }
            if (a == 2)
            {
                sql = @"select [all_id],[all_no],[all_ratename],[all_colorname],[all_metaname],[all_desc] from [mg_allpart] where all_no='" + allno + "' and all_id <> " + allid;
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

      
        public static int AddPanrameter(Model.px_PanrameterModel model)
        {
            string maxid = @"declare @i int;
                                SELECT @i=max([SerialID])  FROM [px_Panrameter];
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
            strSql.Append("insert into px_Panrameter(");
            strSql.Append("SerialID,Name,Number)");
            strSql.Append(" values (");
            strSql.Append("@i,@Name,@Number)");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar),
					new SqlParameter("@Number", SqlDbType.Int)};           
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Number;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql, parameters);
            return rows;
        }

        public static int UpdatePanrameter(Model.px_PanrameterModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update px_Panrameter set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Number=@Number");
            strSql.Append(" where SerialID=@SerialID");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar),
					new SqlParameter("@Number", SqlDbType.Int),
					new SqlParameter("@SerialID", SqlDbType.Int)};          
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Number;
            parameters[2].Value = model.SerialID;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static List<px_PanrameterModel> QueryListForFirstPage(string pagesize, out string total)
        {
            total = "0";
            List<px_PanrameterModel> list = null;

            string sql1 = @"select count(SerialID) total from [px_Panrameter];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [SerialID]
                                  ,[Name]
                                  ,[Number]
                                 
                              FROM  [px_Panrameter] 
	                            order by SerialID asc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<px_PanrameterModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    px_PanrameterModel model = new px_PanrameterModel();

                    model.SerialID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "SerialID"));
                    model.Name = DataHelper.GetCellDataToStr(row, "Name");
                    model.Number = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Number"));
                    var sendbtnvalue = "";
                    var printbtnvalue = "";
                    var ascdescbtnvalue = "";
                    if (GetBtnClass.BtnClassList == null) 
                    {
                        GetBtnClass gbc = new GetBtnClass();
                       // GetBtnClass.BtnClassList = new List<OrderList>(GetBtnClass.BtnClassList); 
                    }
                    else 
                    { 
                        GetBtnClass.BtnClassList = new List<OrderList>(GetBtnClass.BtnClassList); 
                    }

                    foreach (var item in GetBtnClass.BtnClassList)
                    {
                        if (item.SortName.Equals(model.Name))
                        {
                            sendbtnvalue = item.IsAutoSend ? "开启" : "关闭";
                            printbtnvalue = item.IsAutoPrint ? "开启" : "关闭";
                            ascdescbtnvalue = item.Ascordesc ? "开启" : "关闭";
                            model.IsAutoPrint = item.IsAutoPrint;
                            model.IsAutoSend = item.IsAutoSend;
                            model.Ascordesc = item.Ascordesc;
                        }
                    }
                    model.IsAutoPrint1 = printbtnvalue;
                    model.IsAutoSend1 = sendbtnvalue;
                    model.Ascordesc1 = ascdescbtnvalue;
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<px_PanrameterModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<px_PanrameterModel> list = null;

            string sql1 = @"select count(SerialID) total from [px_Panrameter];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [SerialID]
                                  ,[Name]
                                  ,[Number]                                 
                              FROM  [px_Panrameter] 
                                where  SerialID > (
                                                select top 1 SerialID from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") SerialID from  [px_Panrameter] where SerialID is not null  order by SerialID asc )t
                                                order by  SerialID desc)
                                
	                            order by SerialID asc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<px_PanrameterModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    px_PanrameterModel model = new px_PanrameterModel();

                    model.SerialID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "SerialID"));
                    model.Name = DataHelper.GetCellDataToStr(row, "Name");
                    model.Number = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Number"));
                  

                    list.Add(model);
                }
            }
            return list;
        }

        public static int DeletePanrameter(string all_id)
        {
            string sql = @"delete from [mg_allpart] where [all_id]=" + all_id;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static List<px_PanrameterModel> QueryPanrameterListForPart()
        {
            List<px_PanrameterModel> list = null;

            string sql2 = @" 
                            SELECT [SerialID]
                                  ,[Name]
                                  ,[Number]                                
                              FROM  [px_Panrameter]             
	                          order by SerialID asc 
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text,  sql2,  null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<px_PanrameterModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    px_PanrameterModel model = new px_PanrameterModel();

                    model.SerialID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "SerialID"));
                    model.Name = DataHelper.GetCellDataToStr(row, "Name");
                    model.Number = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Number"));
                    list.Add(model);
                }
            }
            return list;
     
        
        }

    }
    
}
