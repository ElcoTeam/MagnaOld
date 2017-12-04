using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Tools;
using DbUtility;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{
    public class mg_MailDAL
    {
        public static List<mg_MailModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            total = "0";
            List<mg_MailModel> list = null;

            string sql1 = @"select count(ID) total from [mg_MailConfig];";
            string sql2 = @" 
                           SELECT top " + pagesize + @" ID mid
                                      ,ReceiptType
                                      ,case ReceiptType
                                             --when 1 then 'LineUp'
                                             --when 2 then 'Delgit'
                                            -- when 3 then '回冲' end as ReceiptTypeName
	                                    when 1 then 'LineUpTxt加载失败'
			                            when 2 then 'LineUp订单的ProductNo在MES系统中不匹配或为空'
			                            when 3 then 'DelJetTxt加载失败'
			                            when 4 then 'DelJet订单的在LineUp订单中不匹配或对应的ProductNo不匹配'
			                            when 5 then 'DelJet订单自动拆单失败'
			                            when 6 then 'SAP手动插单Txt加载失败'
			                            when 7 then 'SAP手动插单订单自动拆单失败'
			                            when 8 then 'SAP手动插单订单的ProductNo在MES系统中不匹配或为空'
			                            when 9 then 'DelJet订单的SEQNR不连续'
			                            when 10 then 'DelJet缓存文件夹写入失败' end as ReceiptTypeName
                                      ,MailRecipient
                                      ,RecipientType
                                      ,case RecipientType
                                            when 1 then '收件人'
                                            when 2 then '抄送人' end as RecipientTypeName
                                  FROM [mg_MailConfig] t1
                                     where  t1.ID not in (
                                                        select top ((" + page + @"-1)*" + pagesize + @") ID from  [mg_MailConfig] order by ID desc)
                                         order by ID desc ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_MailModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_MailModel model = new mg_MailModel();

                    model.mail_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "mid"));
                    model.ReceiptType = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ReceiptType"));
                    model.ReceiptTypeName = DataHelper.GetCellDataToStr(row, "ReceiptTypeName");
                    model.MailRecipient = DataHelper.GetCellDataToStr(row, "MailRecipient");
                    model.RecipientType = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "RecipientType"));
                    model.RecipientTypeName = DataHelper.GetCellDataToStr(row, "RecipientTypeName");
                    


                    list.Add(model);
                }
            }
            return list;

        }
        public static List<mg_MailModel> QueryListForFirstPage(string pagesize, out string total)
        {

            total = "0";
            List<mg_MailModel> list = null;

            string sql1 = @"select count(ID) total from [mg_MailConfig];";
            string sql2 = @" 
                              SELECT top " + pagesize + @" ID mid
                                      ,ReceiptType
                                      ,case ReceiptType
                                            -- when 1 then 'LineUp'
                                            -- when 2 then 'Delgit'
                                            -- when 3 then '回冲' end as ReceiptTypeName
                                            	when 1 then 'LineUpTxt加载失败'
			                                    when 2 then 'LineUp订单的ProductNo在MES系统中不匹配或为空'
			                                    when 3 then 'DelJetTxt加载失败'
			                                    when 4 then 'DelJet订单的在LineUp订单中不匹配或对应的ProductNo不匹配'
			                                    when 5 then 'DelJet订单自动拆单失败'
			                                    when 6 then 'SAP手动插单Txt加载失败'
			                                    when 7 then 'SAP手动插单订单自动拆单失败'
			                                    when 8 then 'SAP手动插单订单的ProductNo在MES系统中不匹配或为空'
			                                    when 9 then 'DelJet订单的SEQNR不连续'
			                                    when 10 then 'DelJet缓存文件夹写入失败' end as ReceiptTypeName
                                      ,MailRecipient
                                      ,RecipientType
                                      ,case RecipientType
                                            when 1 then '收件人'
                                            when 2 then '抄送人' end as RecipientTypeName
                                  FROM [mg_MailConfig] order by ID desc
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<mg_MailModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_MailModel model = new mg_MailModel();

                    model.mail_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "mid"));
                    model.ReceiptType = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ReceiptType"));
                    model.ReceiptTypeName = DataHelper.GetCellDataToStr(row, "ReceiptTypeName");
                    model.MailRecipient = DataHelper.GetCellDataToStr(row, "MailRecipient");
                    model.RecipientType = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "RecipientType"));
                    model.RecipientTypeName = DataHelper.GetCellDataToStr(row, "RecipientTypeName");
                    
                    list.Add(model);
                }
            }
            return list;

        }
        public static bool AddMail(mg_MailModel model)
        {
            
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(model.MailName))
            {
                
                string[] idArr = model.MailName.Split(',');
                foreach (string MailName in idArr)
                {
                    strSql.Append("INSERT INTO [mg_MailConfig](");
                    strSql.Append("ReceiptType,MailRecipient,RecipientType)");
                    strSql.Append(" values (");
                    strSql.Append("convert(int, (" + model.ReceiptType + ")),'" + MailName + "',convert(int,(" + model.RecipientType + ")));");
                    
                }
                
            }
            
            return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(),null );
            
                  
        }
        public static bool UpdateMail(Model.mg_MailModel model)
        {
            string[] idArr = model.MailName.Split(',');

            if (idArr.Length == 1)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update mg_MailConfig set ");
                strSql.Append("ReceiptType=@ReceiptType,");
                strSql.Append("MailRecipient=@MailRecipient,");
                strSql.Append("RecipientType=@RecipientType");
                strSql.Append(" where ID=@ID;");


                SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int),
					new SqlParameter("@ReceiptType", SqlDbType.Int),
					new SqlParameter("@MailRecipient", SqlDbType.NVarChar),
                    new SqlParameter("@RecipientType", SqlDbType.Int)};
                parameters[0].Value = model.mail_id;
                parameters[1].Value = model.ReceiptType;
                parameters[2].Value = model.MailName;
                parameters[3].Value = model.RecipientType;


                List<SqlParameter> list = new List<SqlParameter>();
                list.AddRange(parameters);
                return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(), list);
            }
            else {
                StringBuilder strSql = new StringBuilder();
                //strSql.Append("update mg_MailConfig set ");
                //strSql.Append("ReceiptType=@ReceiptType,");
                //strSql.Append("MailRecipient=@MailRecipient,");
                //strSql.Append("RecipientType=@RecipientType");
                //strSql.Append(" where ID=@ID;");

                if (!string.IsNullOrEmpty(model.MailName))
                {
                    strSql.Append("delete from [mg_MailConfig]  where ID=@ID ;");
                    
                    foreach (string id in idArr)
                    {
                        strSql.Append("INSERT INTO [mg_MailConfig] ([ID],[ReceiptType],[MailRecipient],[RecipientType]) VALUES (convert(int, (" + id + ")),@ReceiptType,@MailRecipient,@RecipientType);");
                    }
                }

                SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int),
                    new SqlParameter("@ReceiptType", SqlDbType.Int),
                    new SqlParameter("@MailRecipient", SqlDbType.NVarChar),
                    new SqlParameter("@RecipientType", SqlDbType.Int)};
                parameters[0].Value = model.mail_id;
                parameters[1].Value = model.ReceiptType;
                parameters[2].Value = model.MailName;
                parameters[3].Value = model.RecipientType;


                List<SqlParameter> list = new List<SqlParameter>();
                list.AddRange(parameters);
                return SqlHelper.ExecuteSqlTran(SqlHelper.SqlConnString, strSql.ToString(), list);
 
            }
        }
        public static int DeleteMail(string mail_id)
        {
            string sql = @"delete from [mg_MailConfig] where [ID]=" + mail_id + ";";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

    }
}
