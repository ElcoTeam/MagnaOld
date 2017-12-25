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
    public class px_PrintDAL
    {

        public static List<px_PrintModel> Querypx_PrintList()
        {
            List<px_PrintModel> list = null;

            string sql2 = @" 
                            SELECT [id]
                                  ,[PXID]
                                  ,[orderid]
                                  ,[cartype]
                                  ,[XF]
                                  ,[LingjianHao]
                                  ,[sum]
                                  ,[ordername]
                                  ,[dayintime]
                                  ,[printpxid]
                                  ,[resultljh]
                                  ,[isauto]
                                  ,[SFlag]
                              FROM  [px_Print]             
	                          order by id asc 
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text,  sql2,  null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<px_PrintModel>();
                foreach (DataRow row in dt2.Rows)
                {
                    px_PrintModel model = new px_PrintModel();

                    model.id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "id"));
                    model.isauto = DataHelper.GetCellDataToStr(row, "isauto");
                    model.LingjianHao = DataHelper.GetCellDataToStr(row, "LingjianHao");

                    list.Add(model);
                }
            }
            return list;
        }

        public static int Updatepx_Print(Model.px_PrintModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update px_Print set ");
            strSql.Append("PXID=@PXID,");
            strSql.Append("orderid=@orderid,");
            strSql.Append("cartype=@cartype,");
            strSql.Append("XF=@XF,");
            strSql.Append("sum=@sum,");
            strSql.Append("ordername=@ordername,");
            strSql.Append("dayintime=@dayintime,");          
            strSql.Append("printpxid=@printpxid");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@PXID", SqlDbType.NVarChar),
					new SqlParameter("@orderid", SqlDbType.NVarChar),
					new SqlParameter("@cartype", SqlDbType.NVarChar),
					new SqlParameter("@XF", SqlDbType.NVarChar),
                    new SqlParameter("@sum", SqlDbType.NVarChar),
                    new SqlParameter("@ordername", SqlDbType.NVarChar),
                    new SqlParameter("@dayintime", SqlDbType.DateTime),
                    new SqlParameter("@printpxid", SqlDbType.Int),
                    new SqlParameter("@SID", SqlDbType.Int)};
            parameters[0].Value = model.PXID;
            parameters[1].Value = model.orderid;
            parameters[2].Value = model.cartype;
            parameters[3].Value = model.XF;
            parameters[4].Value = model.sum;
            parameters[5].Value = model.ordername;
            parameters[6].Value = Convert.ToDateTime(model.dayintime);
            parameters[7].Value =Convert.ToInt32(model.printpxid);
            parameters[8].Value = Convert.ToInt32(model.id);

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static int Insertpx_Print(Model.px_PrintModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert ( ");
            strSql.Append("PXID,");
            strSql.Append("orderid,");
            strSql.Append("cartype,");
            strSql.Append("XF,");
            strSql.Append("LingjianHao,");
            strSql.Append("sum,");
            strSql.Append("ordername,");
            strSql.Append("dayintime,");
            strSql.Append("printpxid)");
            strSql.Append(" values(@PXID,");
            strSql.Append("@orderid,");
            strSql.Append("@cartype,");
            strSql.Append("@XF,");
            strSql.Append("@sum,");
            strSql.Append("@ordername,");
            strSql.Append("@dayintime,");
            strSql.Append("@printpxid)");
            SqlParameter[] parameters = {
					new SqlParameter("@PXID", SqlDbType.NVarChar),
					new SqlParameter("@orderid", SqlDbType.NVarChar),
					new SqlParameter("@cartype", SqlDbType.NVarChar),
					new SqlParameter("@XF", SqlDbType.NVarChar),
                    new SqlParameter("@LingjianHao", SqlDbType.NVarChar),
                    new SqlParameter("@sum", SqlDbType.NVarChar),
                    new SqlParameter("@ordername", SqlDbType.NVarChar),
                    new SqlParameter("@dayintime", SqlDbType.DateTime),
                    new SqlParameter("@printpxid", SqlDbType.Int),
                    new SqlParameter("@SID", SqlDbType.Int)};
            parameters[0].Value = model.PXID;
            parameters[1].Value = model.orderid;
            parameters[2].Value = model.cartype;
            parameters[3].Value = model.XF;
            parameters[4].Value = model.LingjianHao;
            parameters[5].Value = model.sum;
            parameters[6].Value = model.ordername;
            parameters[7].Value = Convert.ToDateTime(model.dayintime);
            parameters[8].Value = Convert.ToInt32(model.printpxid);           

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static int updateIsSendOkbyPXID(string IsSendOk,string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update px_Print set IsSendOk=@IsSendOk where PXID = @PXID");
            SqlParameter[] parameters = {
                    new SqlParameter("@IsSendOk", SqlDbType.NChar),
					new SqlParameter("@PXID", SqlDbType.Int)};
            parameters[0].Value = IsSendOk;
            parameters[1].Value =Convert.ToInt32(id);
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
    }
}
