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

    public class GetModel
    {
       

        public int GetSPModels_Count(string csh = "", params DateTime?[] time)
        {
            int sPModels_Count = 0;
            string sql = "select * from View_px_AllList where 1=1 ";
            if ((csh != null && csh.Length > 1) || (time != null && time.Length > 0))
                sql = "select * from View_px_AllListALLLLL where 1=1 ";

            if (time != null && time.Length > 0)
            {
                sql += " and co_starttime>'" + time[0] + "' and co_endtime < '" + time[1] + "'";
            }
            if (csh != null && csh.Length > 1)
                sql += " and 车身号='" + csh + "'";
            List<GetIndex> list = null;

            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<GetIndex>();
                foreach (DataRow row in dt2.Rows)
                {
                    GetIndex model = new GetIndex();

                    model.co_endtime =Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "co_endtime"));
                    model.co_starttime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "co_starttime"));
                    model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                    model.IsPrint = DataHelper.GetCellDataToStr(row, "IsPrint");
                    model.mg_partorder_ordertype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "mg_partorder_ordertype"));
                    model.PartOrderID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "PartOrderID"));
                    model.车身号 = DataHelper.GetCellDataToStr(row, "车身号");
                    model.大部件号 = DataHelper.GetCellDataToStr(row, "大部件号");
                    model.等级 = DataHelper.GetCellDataToStr(row, "等级");
                    model.订单号 = DataHelper.GetCellDataToStr(row, "订单号");
                    model.零件号 = DataHelper.GetCellDataToStr(row, "零件号");
                    model.零件号描述 = DataHelper.GetCellDataToStr(row, "零件号描述");
                    model.主副驾 = DataHelper.GetCellDataToStr(row, "主副驾");                 
                    list.Add(model);
                }
            }
            var AllModelList = list;
            sPModels_Count = AllModelList.DistinctsBy(s => s.订单号).ToList().Count;
            return sPModels_Count;
        }


       

  
        public List<GetSP> GetSPModels(int Skip, int Take, string csh = "", params DateTime?[] time)
        {
            string sqlupdate = "update mg_PartOrder set OrderIsPrint='0' where OrderIsPrint is null";
            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sqlupdate, null);
           // db.Database.ExecuteSqlCommand("update mg_PartOrder set OrderIsPrint='0' where OrderIsPrint is null ");
           ////db.Database.ExecuteSqlCommand("update mg_PartOrder set OrderIsPrintSYS='0' where OrderIsPrintSYS is null ");
            StringBuilder strSql = new StringBuilder();
          
            string sql="select * from View_px_AllList where 1=1 ";
            if ((csh != null && csh.Length > 1) || (time != null && time.Length > 0))
              sql="select * from View_px_AllListALLLLL where 1=1 ";
            strSql.Append(sql);
            if (time != null && time.Length > 0)
            {
                strSql.Append(" and co_starttime>@time0 and co_endtime < @time1 ");
            }
            if (csh != null && csh.Length > 1)
                strSql.Append(" and 车身号=@csh");
            strSql.Append(" order by ID");


            List<GetIndex> listindex = null;        
            DataTable dt;
            if (time == null)
            {
                if (csh == null) 
                {
                    SqlParameter[] parameters = {};                    
                    dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
                }
                else 
                { 
            SqlParameter[] parameters = {
					
					new SqlParameter("@csh", SqlDbType.NVarChar)};   
                    parameters[0].Value = csh;
                    dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
                }
            }
            else
            {
                if (csh == null)
                {
                    SqlParameter[] parameters = {
                    new SqlParameter("@time0", SqlDbType.DateTime),
					new SqlParameter("@time1", SqlDbType.DateTime)};		
                    parameters[0].Value = time[0];
                    parameters[1].Value = time[1];                  
                    dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
                }
                else
                {
                    SqlParameter[] parameters = {
                    new SqlParameter("@time0", SqlDbType.DateTime),
					new SqlParameter("@time1", SqlDbType.DateTime),
                    new SqlParameter("@csh", SqlDbType.NVarChar)};  
                    parameters[0].Value = time[0];
                    parameters[1].Value = time[1];
                    parameters[2].Value = csh;
                    dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
                }
            }

            
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                listindex = new List<GetIndex>();
                foreach (DataRow row in dt2.Rows)
                {
                    GetIndex model = new GetIndex();
                    model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                    model.co_endtime = NumericParse.StringToDateTime(DataHelper.GetCellDataToStr(row, "co_endtime"));
                    model.co_starttime = NumericParse.StringToDateTime(DataHelper.GetCellDataToStr(row, "co_starttime"));
                  
                    model.IsPrint = DataHelper.GetCellDataToStr(row, "IsPrint");
                    model.mg_partorder_ordertype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "mg_partorder_ordertype"));
                    model.PartOrderID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "PartOrderID"));
                    model.车身号 = DataHelper.GetCellDataToStr(row, "车身号");
                    model.大部件号 = DataHelper.GetCellDataToStr(row, "大部件号");
                    model.等级 = DataHelper.GetCellDataToStr(row, "等级");
                    model.订单号 = DataHelper.GetCellDataToStr(row, "订单号");
                    model.零件号 = DataHelper.GetCellDataToStr(row, "零件号");
                    model.零件号描述 = DataHelper.GetCellDataToStr(row, "零件号描述");
                    model.主副驾 = DataHelper.GetCellDataToStr(row, "主副驾");               
                    listindex.Add(model);
                }
            }

            List<GetSP> sp = new List<GetSP>();
            if (listindex == null)
            {
              
            }
            else
            {
              
                List<GetIndex> list = new List<GetIndex>();
                var AllModelList = listindex;
                string sql_Print = @"SELECT [id]
                                          ,[orderid]
                                          ,[XF]
                                          ,[ordername]
                                          ,[IsSendOk]
                                FROM [dbo].[px_Print] ";

                List<GetpxPrintAll> listpxPrintAll = null;

                DataTable dtpxPrintAll = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql_Print, null);
                if (DataHelper.HasData(dtpxPrintAll))
                {
                    DataTable dt2 = dtpxPrintAll;
                    listpxPrintAll = new List<GetpxPrintAll>();
                    foreach (DataRow row in dt2.Rows)
                    {
                        GetpxPrintAll model = new GetpxPrintAll();
                        model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                        model.IsSendOk = DataHelper.GetCellDataToStr(row, "IsSendOk");
                        model.XF = DataHelper.GetCellDataToStr(row, "XF");
                        model.orderid = DataHelper.GetCellDataToStr(row, "orderid");
                        model.ordername = DataHelper.GetCellDataToStr(row, "ordername");
                        listpxPrintAll.Add(model);
                    }
                }

                List<GetpxPrintAll> AllModelList_Print = listpxPrintAll;
                var zfjlist = AllModelList.Where(s => s.主副驾.Equals("主架") || s.主副驾.Equals("副驾")).Select(s => s.订单号).ToList();
                var Listcount = AllModelList.DistinctsBy(s => s.订单号).ToList();


                var AllPrint = px_PrintDAL.Querypx_PrintList();            //所有订单号
                var ListOrderID = AllModelList.Select(s => s.订单号).Distinct().ToList();
                int num = 0;
                for (int i = 0; i < Listcount.Count; i++)
                {
                    num = num + 1;
                    if (num <= Skip)
                        continue;
                    if (num > (Skip + Take))
                        continue;

                    GetSP gsp = new GetSP();
                    gsp.序号 = (i + 1).ToString();
                    string orderid = Listcount[i].订单号;
                    string carid = Listcount[i].车身号;
                    gsp.订单号 = carid;
                    gsp.等级 = AllModelList.FirstOrDefault(s => s.订单号.Equals(orderid)).等级;
                    gsp.靠背面套主驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背面套") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背面套") != -1 && s.主副驾 == "主驾").零件号;
                    gsp.靠背面套副驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背面套") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背面套") != -1 && s.主副驾 == "副驾").零件号;
                    gsp.坐垫面套主驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫面套") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫面套") != -1 && s.主副驾 == "主驾").零件号;
                    gsp.坐垫面套副驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫面套") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫面套") != -1 && s.主副驾 == "副驾").零件号;
                    gsp.坐垫骨架主驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫骨架") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫骨架") != -1 && s.主副驾 == "主驾").零件号;
                    gsp.坐垫骨架副驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫骨架") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫骨架") != -1 && s.主副驾 == "副驾").零件号;
                    gsp.靠背骨架主驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背骨架") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背骨架") != -1 && s.主副驾 == "主驾").零件号;
                    gsp.靠背骨架副驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背骨架") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背骨架") != -1 && s.主副驾 == "副驾").零件号;

                    //var aa = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40" && s.零件号描述.IndexOf("靠背面套") != -1);
                    //var aaaa = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40" && s.零件号描述 == "靠背面套");
                    //var aaaa1 = AllModelList.FirstOrDefault(s => s.订单号 == orderid &&  s.零件号描述 == "靠背面套");
                    //var aaaa2 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40");
                    //var aaaa23 = AllModelList.FirstOrDefault(s =>  s.主副驾 == "后40");
                    //var aaaa21 = AllModelList.FindAll(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背面套") != -1);

                    gsp.线束主驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("线束") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("线束") != -1 && s.主副驾 == "主驾").零件号;
                    gsp.线束副驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("线束") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("线束") != -1 && s.主副驾 == "副驾").零件号;
                    gsp.大背板主驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("大背板") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("大背板") != -1 && s.主副驾 == "主驾").零件号;
                    gsp.大背板副驾 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("大背板") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("大背板") != -1 && s.主副驾 == "副驾").零件号;
                    gsp.靠背40 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40" && s.零件号描述.IndexOf("靠背面套") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40" && s.零件号描述.IndexOf("靠背面套") != -1).零件号;
                    gsp.靠背60 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("靠背面套") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("靠背面套") != -1).零件号;
                    gsp.后坐垫 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后坐垫" && s.零件号描述.IndexOf("坐垫面套") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后坐垫" && s.零件号描述.IndexOf("坐垫面套") != -1).零件号;
                    gsp.后排中央扶手 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("扶手") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("扶手") != -1).零件号;
                    gsp.后排中央头枕 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("中头枕") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("中头枕") != -1).零件号;
                    gsp.侧头枕40 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40" && s.零件号描述.IndexOf("侧头枕") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40" && s.零件号描述.IndexOf("侧头枕") != -1).零件号;
                    gsp.侧头枕60 = AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("侧头枕") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("侧头枕") != -1).零件号;
                    //gsp.靠背面套主驾 = "靠背面套主驾";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背面套") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背面套") != -1 && s.主副驾 == "主驾").零件号;
                    //gsp.靠背面套副驾 = "靠背面套副驾";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背面套") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背面套") != -1 && s.主副驾 == "副驾").零件号;
                    //gsp.坐垫面套主驾 = "坐垫面套主驾";//AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫面套") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫面套") != -1 && s.主副驾 == "主驾").零件号;
                    //gsp.坐垫面套副驾 = "坐垫面套副驾";//AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫面套") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫面套") != -1 && s.主副驾 == "副驾").零件号;
                    //gsp.坐垫骨架主驾 = "坐垫骨架主驾";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫骨架") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫骨架") != -1 && s.主副驾 == "主驾").零件号;
                    //gsp.坐垫骨架副驾 = "坐垫骨架副驾";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫骨架") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("坐垫骨架") != -1 && s.主副驾 == "副驾").零件号;
                    //gsp.靠背骨架主驾 = "靠背骨架主驾";//AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背骨架") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背骨架") != -1 && s.主副驾 == "主驾").零件号;
                    //gsp.靠背骨架副驾 = "靠背骨架副驾";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背骨架") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("靠背骨架") != -1 && s.主副驾 == "副驾").零件号;
                    //gsp.线束主驾 = "线束主驾";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("线束") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("线束") != -1 && s.主副驾 == "主驾").零件号;
                    //gsp.线束副驾 = "线束副驾";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("线束") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("线束") != -1 && s.主副驾 == "副驾").零件号;
                    //gsp.大背板主驾 = "大背板主驾";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("大背板") != -1 && s.主副驾 == "主驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("大背板") != -1 && s.主副驾 == "主驾").零件号;
                    //gsp.大背板副驾 = "大背板副驾";//AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("大背板") != -1 && s.主副驾 == "副驾") == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.零件号描述.IndexOf("大背板") != -1 && s.主副驾 == "副驾").零件号;
                    //gsp.靠背40 = "靠背40";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40" && s.零件号描述.IndexOf("靠背面套") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40" && s.零件号描述.IndexOf("靠背面套") != -1).零件号;
                    //gsp.后坐垫 = "后坐垫";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后坐垫" && s.零件号描述.IndexOf("坐垫面套") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后坐垫" && s.零件号描述.IndexOf("坐垫面套") != -1).零件号;
                    //gsp.后排中央扶手 = "后排中央扶手";//AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("扶手") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("扶手") != -1).零件号;
                    //gsp.后排中央头枕 = "后排中央头枕";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("中头枕") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("中头枕") != -1).零件号;
                    //gsp.侧头枕40 = "侧头枕40";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40" && s.零件号描述.IndexOf("侧头枕") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后40" && s.零件号描述.IndexOf("侧头枕") != -1).零件号;
                    //gsp.侧头枕60 = "侧头枕60";// AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("侧头枕") != -1) == null ? "无" : AllModelList.FirstOrDefault(s => s.订单号 == orderid && s.主副驾 == "后60" && s.零件号描述.IndexOf("侧头枕") != -1).零件号;


                    gsp.靠背面套主驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("靠背面套") != -1 && s.XF == "主驾") == null ? 0 : 1;
                    gsp.靠背面套副驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("靠背面套") != -1 && s.XF == "副驾") == null ? 0 : 1;
                    gsp.坐垫面套主驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("坐垫面套") != -1 && s.XF == "主驾") == null ? 0 : 1;
                    gsp.坐垫面套副驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("坐垫面套") != -1 && s.XF == "副驾") == null ? 0 : 1;
                    gsp.坐垫骨架主驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("坐垫骨架") != -1 && s.XF == "主驾") == null ? 0 : 1;
                    gsp.坐垫骨架副驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("坐垫骨架") != -1 && s.XF == "副驾") == null ? 0 : 1;
                    gsp.靠背骨架主驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("靠背骨架") != -1 && s.XF == "主驾") == null ? 0 : 1;
                    gsp.靠背骨架副驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("靠背骨架") != -1 && s.XF == "副驾") == null ? 0 : 1;
                    gsp.线束主驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("线束") != -1 && s.XF == "主驾") == null ? 0 : 1;
                    gsp.线束副驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("线束") != -1 && s.XF == "副驾") == null ? 0 : 1;
                    gsp.大背板主驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("大背板") != -1 && s.XF == "主驾") == null ? 0 : 1;
                    gsp.大背板副驾打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("大背板") != -1 && s.XF == "副驾") == null ? 0 : 1;

                    gsp.靠背40打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("靠背面套") != -1 && s.XF == "后40") == null ? 0 : 1;
                    gsp.靠背60打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("靠背面套") != -1 && s.XF == "后60") == null ? 0 : 1;
                    gsp.后坐垫打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("坐垫面套") != -1 && s.XF == "后坐垫") == null ? 0 : 1;
                    gsp.后排中央扶手打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("扶手") != -1 && s.XF == "后60") == null ? 0 : 1;
                    gsp.后排中央头枕打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("中头枕") != -1 && s.XF == "后60") == null ? 0 : 1;
                    gsp.侧头枕40打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("侧头枕") != -1 && s.XF == "后40") == null ? 0 : 1;
                    gsp.侧头枕60打印 = AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf("侧头枕") != -1 && s.XF == "后60") == null ? 0 : 1;

                    gsp.靠背面套主驾下发 = this.GetSend_Status(AllPrint, carid, "靠背面套", "主驾", AllModelList_Print);//AllModelList_Print
                    gsp.靠背面套副驾下发 = this.GetSend_Status(AllPrint, carid, "靠背面套", "副驾", AllModelList_Print);
                    gsp.坐垫面套主驾下发 = this.GetSend_Status(AllPrint, carid, "坐垫面套", "主驾", AllModelList_Print);
                    gsp.坐垫面套副驾下发 = this.GetSend_Status(AllPrint, carid, "坐垫面套", "副驾", AllModelList_Print);
                    gsp.坐垫骨架主驾下发 = this.GetSend_Status(AllPrint, carid, "坐垫骨架", "主驾", AllModelList_Print);
                    gsp.坐垫骨架副驾下发 = this.GetSend_Status(AllPrint, carid, "坐垫骨架", "副驾", AllModelList_Print);
                    gsp.靠背骨架主驾下发 = this.GetSend_Status(AllPrint, carid, "靠背骨架", "主驾", AllModelList_Print);
                    gsp.靠背骨架副驾下发 = this.GetSend_Status(AllPrint, carid, "靠背骨架", "副驾", AllModelList_Print);
                    gsp.线束主驾下发 = this.GetSend_Status(AllPrint, carid, "线束", "主驾", AllModelList_Print);
                    gsp.线束副驾下发 = this.GetSend_Status(AllPrint, carid, "线束", "副驾", AllModelList_Print);
                    gsp.大背板主驾下发 = this.GetSend_Status(AllPrint, carid, "大背板", "主驾", AllModelList_Print);
                    gsp.大背板副驾下发 = this.GetSend_Status(AllPrint, carid, "大背板", "副驾", AllModelList_Print);

                    gsp.靠背40下发 = this.GetSend_Status(AllPrint, carid, "靠背面套", "后40", AllModelList_Print);
                    gsp.靠背60下发 = this.GetSend_Status(AllPrint, carid, "靠背面套", "后60", AllModelList_Print);
                    gsp.后坐垫下发 = this.GetSend_Status(AllPrint, carid, "坐垫面套", "后坐垫", AllModelList_Print);
                    gsp.后排中央扶手下发 = this.GetSend_Status(AllPrint, carid, "扶手", "后60", AllModelList_Print);
                    gsp.后排中央头枕下发 = this.GetSend_Status(AllPrint, carid, "中头枕", "后60", AllModelList_Print);
                    gsp.侧头枕40下发 = this.GetSend_Status(AllPrint, carid, "侧头枕", "后40", AllModelList_Print);
                    gsp.侧头枕60下发 = this.GetSend_Status(AllPrint, carid, "侧头枕", "后60", AllModelList_Print);

                    sp.Add(gsp);

                }
            }
            return sp;
        }
        private string GetSend_Status(List<px_PrintModel> AllPrint, string carid, string ordername, string xf, List<GetpxPrintAll> AllModelList_Print)
        {
            string strnull = null;
            string sendStatus = "";
            //            string sql_print = @"SELECT [id]
            //                                          ,[PXID]
            //                                          ,[orderid]
            //                                          ,[cartype]
            //                                          ,[XF]
            //                                          ,[LingjianHao]
            //                                          ,[sum]
            //                                          ,[ordername]
            //                                          ,[dayintime]
            //                                          ,[printpxid]
            //                                          ,[resultljh]
            //                                          ,[isauto]
            //                                          ,[SFlag]
            //                                          ,[IsSendOk]
            //                                FROM [dbo].[px_Print]  where orderid='"
            //                + carid + "' and ordername='" + ordername + "' and xf='" + xf + "'";
            //             var AllModelList = db.Database.SqlQuery<GetpxPrint>(sql_print).ToList();
            //是否saomiao
            if (AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf(ordername) != -1 && s.XF == xf) != null)
            {
                // string IsSendOk = AllModelList.Select(s => s.IsSendOk).Distinct().ToList()[0];
                string IsSendOk = AllModelList_Print.FirstOrDefault(s => s.orderid == carid && s.ordername == ordername && s.XF == xf).IsSendOk;
                if (AllPrint.FirstOrDefault(s => s.orderid == carid && s.ordername.IndexOf(ordername) != -1 && s.XF == xf && s.resultljh != strnull && s.resultljh.Length > 5) != null)
                { 
                    sendStatus = "background-color:green";
                }
                   
                else if (IsSendOk != null && IsSendOk.Trim().Equals("1"))
                {
                    sendStatus = "background-color:blue";
                }

                else
                {
                    sendStatus = "background-color:yellow";
                }
                  
            }
            return sendStatus;
        }
        public List<GetIndex> GetListModels_ByWhere(string id, string er)
        {
            string isprint_ToStr = "";
            if (er == "前排")
                isprint_ToStr = id;
            else
                isprint_ToStr = er + id;

            string searchlistWhere = "select * from View_px_AllList ";
            if (er == "前排")
            {
                searchlistWhere += "where (主副驾 = '主驾' or 主副驾 = '副驾') and  零件号描述 like '%" + id + "%'  and IsPrint not like '%" + isprint_ToStr + "%' Order By ID, 主副驾 desc";
            }
            else if (er == "后排RSB40" || er == "后40")
            {
                searchlistWhere += "where (主副驾 = '后40') and  零件号描述 like '%" + id + "%'  and IsPrint  not like '%" + isprint_ToStr + "%'  Order By ID, 主副驾 desc";
            }
            else if (er == "后排RSB60" || er == "后60")
            {
                searchlistWhere += "where (主副驾 = '后60') and  零件号描述 like '%" + id + "%'  and IsPrint  not like '%" + isprint_ToStr + "%'  Order By ID, 主副驾 desc";
            }
            else if (er == "后排RSC" || er == "后坐垫")
            {
                searchlistWhere += "where (主副驾 = '后坐垫') and  零件号描述 like '%" + id + "%'  and IsPrint  not like '%" + isprint_ToStr + "%'  Order By ID, 主副驾 desc";
            }


           string sql1="update mg_PartOrder set OrderIsPrint='0' where OrderIsPrint is null";
            string sql2="update mg_PartOrder set OrderIsPrintSYS='0' where OrderIsPrintSYS is null";
            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql1+sql2, null);           
            List<GetSP> sp = new List<GetSP>();
            List<GetIndex> list = new List<GetIndex>();

            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, searchlistWhere, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<GetIndex>();
                foreach (DataRow row in dt2.Rows)
                {
                    GetIndex model = new GetIndex();

                    model.co_endtime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "co_endtime"));
                    model.co_starttime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "co_starttime"));
                    model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                    model.IsPrint = DataHelper.GetCellDataToStr(row, "IsPrint");
                    list.Add(model);
                }
            }
            var AllModelList = list;
            var AllPrint = px_PrintDAL.Querypx_PrintList();
            foreach (var item in AllModelList)
            {
                item.IsPrint = AllPrint.FirstOrDefault(s => s.orderid == item.车身号 && item.零件号描述.IndexOf(s.ordername) != -1 && s.XF == item.主副驾) == null ? "0" : "1";
            }

            return AllModelList;
        }


        public List<GetIndex> GetListModels()
        {
            string sql1="update mg_PartOrder set OrderIsPrint='0' where OrderIsPrint is null ";
            string sql2="update mg_PartOrder set OrderIsPrintSYS='0' where OrderIsPrintSYS is null ";
            SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql1 + sql2, null);           
            List<GetSP> sp = new List<GetSP>();
            List<GetIndex> list = new List<GetIndex>();
            string searchlistWhere = "select * from View_px_AllList order by ID";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, searchlistWhere, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<GetIndex>();
                foreach (DataRow row in dt2.Rows)
                {
                    GetIndex model = new GetIndex();

                    model.co_endtime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "co_endtime"));
                    model.co_starttime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "co_starttime"));
                    model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                    model.IsPrint = DataHelper.GetCellDataToStr(row, "IsPrint");
                    model.mg_partorder_ordertype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "mg_partorder_ordertype"));
                    model.PartOrderID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "PartOrderID"));
                    model.车身号 = DataHelper.GetCellDataToStr(row, "车身号");
                    model.大部件号 = DataHelper.GetCellDataToStr(row, "大部件号");
                    model.等级 = DataHelper.GetCellDataToStr(row, "等级");
                    model.订单号 = DataHelper.GetCellDataToStr(row, "订单号");
                    model.零件号 = DataHelper.GetCellDataToStr(row, "零件号");
                    model.零件号描述 = DataHelper.GetCellDataToStr(row, "零件号描述");
                    model.主副驾 = DataHelper.GetCellDataToStr(row, "主副驾");               
                    list.Add(model);
                }
            }
            var AllModelList = list;
            var AllPrint = px_PrintDAL.Querypx_PrintList();
            foreach (var item in AllModelList)
            {
                item.IsPrint = AllPrint.FirstOrDefault(s => s.orderid == item.车身号 && item.零件号描述.IndexOf(s.ordername) != -1 && s.XF == item.主副驾) == null ? "0" : "1";
            }

            return AllModelList;
        }


        public List<GetIndex> GetModels()
        {
           
            List<GetIndex> list = new List<GetIndex>();          
            string searchlistWhere = "select 订单号,等级,零件号,零件号描述,大部件号,'主驾' as 主副驾 ,co_starttime,co_endtime  from zhujia  union all   select 订单号,等级,零件号,零件号描述,大部件号,'副驾' as 主副驾 ,co_starttime,co_endtime   from zhujia  union all  select 订单号,等级,零件号,'40靠背' as 零件号描述,大部件号,'40靠背'as 主副驾 ,co_starttime,co_endtime  from hou40kaobei  union all   select 订单号,等级,零件号,'60靠背'as 零件号描述,大部件号,'60靠背'as 主副驾 ,co_starttime,co_endtime   from hou60kaobei  union all  select 订单号,等级,零件号,'后坐垫' as 零件号描述,大部件号,'后坐垫' as 主副驾 ,co_starttime,co_endtime   from Houzuodian order by co_starttime ,主副驾 desc , 订单号";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, searchlistWhere, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<GetIndex>();
                foreach (DataRow row in dt2.Rows)
                {
                    GetIndex model = new GetIndex();
                    model.co_endtime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "co_endtime"));
                    model.co_starttime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "co_starttime"));
                    model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                    model.IsPrint = DataHelper.GetCellDataToStr(row, "IsPrint");
                    model.mg_partorder_ordertype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "mg_partorder_ordertype"));
                    model.PartOrderID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "PartOrderID"));
                    model.车身号 = DataHelper.GetCellDataToStr(row, "车身号");
                    model.大部件号 = DataHelper.GetCellDataToStr(row, "大部件号");
                    model.等级 = DataHelper.GetCellDataToStr(row, "等级");
                    model.订单号 = DataHelper.GetCellDataToStr(row, "订单号");
                    model.零件号 = DataHelper.GetCellDataToStr(row, "零件号");
                    model.零件号描述 = DataHelper.GetCellDataToStr(row, "零件号描述");
                    model.主副驾 = DataHelper.GetCellDataToStr(row, "主副驾");               
                    list.Add(model);
                }
            }

            var AllModelList = list.DistinctsBy(x => new { x.订单号, x.等级, x.零件号, x.主副驾, x.零件号描述, x.大部件号 }).ToList();
            var zfjlist = AllModelList.Where(s => s.主副驾.Equals("主驾") || s.主副驾.Equals("副驾")).Select(s => s.订单号).ToList();
            // var zfjlist = AllModelList.Select(s => s.订单号).Distinct().ToList();
            var PrintList = px_PrintDAL.Querypx_PrintList();
            //所有订单号
            var ListOrderID = AllModelList.Select(s => s.订单号).Distinct().ToList();
            foreach (var item in ListOrderID)
            {
                bool iszfj = true;
                foreach (var item2 in zfjlist)
                {
                    iszfj = iszfj & (item2.Equals(item));
                }
                var templist = AllModelList.Where(s => s.订单号.Equals(item)).ToList();
                for (int i = 0; i < templist.Count; i++)
                {
                    for (int j = 0; j < PrintList.Count; j++)
                    {
                        if (templist[i].零件号描述.ToString().Equals("") && !templist[i].主副驾.ToString().Equals(""))
                        {
                            templist[i].零件号描述 = templist[i].主副驾;
                        }
                        if (iszfj)
                        {
                            if (PrintList[j].orderid.Equals(templist[i].订单号) && PrintList[j].ordername.Equals(templist[i].零件号描述) && PrintList[j].XF.Equals(templist[i].主副驾))
                            {
                                templist[i].IsPrint = "1";
                            }
                        }
                        else
                        {
                            if (PrintList[j].orderid.Equals(templist[i].订单号) && PrintList[j].ordername.Equals(templist[i].零件号描述))
                            {
                                templist[i].IsPrint = "1";
                            }
                        }
                    }
                }

                for (int i = 0; i < templist.Count; i++)
                {

                    if (templist[i].主副驾.Equals("主驾"))
                    {
                        list.Add(templist[i]);
                    }
                    else if (templist[i].主副驾.Equals("副驾"))
                    {
                        list.Add(templist[i]);
                    }

                }
                if (templist.FirstOrDefault(s => s.主副驾.Equals("40靠背")) != null)
                {
                    list.Add(templist.FirstOrDefault(s => s.主副驾.Equals("40靠背")));

                }
                if (templist.FirstOrDefault(s => s.主副驾.Equals("60靠背")) != null)
                {
                    list.Add(templist.FirstOrDefault(s => s.主副驾.Equals("60靠背")));

                }
                if (templist.FirstOrDefault(s => s.主副驾.Equals("后坐垫")) != null)
                {
                    list.Add(templist.FirstOrDefault(s => s.主副驾.Equals("后坐垫")));
                }

            }
            list = list.Where(s => s.IsPrint == "0").ToList();
            return list;
        }
        public List<GetIndex> GetAllModels()
        {
            List<GetIndex> list = new List<GetIndex>();   
            string sql = "select 订单号,等级,零件号,零件号描述,大部件号,'主驾' as 主副驾 ,co_starttime,co_endtime  from zhujia  union all   select 订单号,等级,零件号,零件号描述,大部件号,'副驾' as 主副驾 ,co_starttime,co_endtime   from zhujia  union all  select 订单号,等级,零件号,零件号描述,大部件号,'40靠背'as 主副驾 ,co_starttime,co_endtime  from hou40kaobei  union all   select 订单号,等级,零件号,零件号描述,大部件号,'60靠背'as 主副驾 ,co_starttime,co_endtime   from hou60kaobei  union all  select 订单号,等级,零件号,零件号描述,大部件号,'后坐垫' as 主副驾 ,co_starttime,co_endtime   from Houzuodian ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<GetIndex>();
                foreach (DataRow row in dt2.Rows)
                {
                    GetIndex model = new GetIndex();

                    model.co_endtime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "co_endtime"));
                    model.co_starttime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "co_starttime"));
                    model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                    model.IsPrint = DataHelper.GetCellDataToStr(row, "IsPrint");
                    model.mg_partorder_ordertype = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "mg_partorder_ordertype"));
                    model.PartOrderID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "PartOrderID"));
                    model.车身号 = DataHelper.GetCellDataToStr(row, "车身号");
                    model.大部件号 = DataHelper.GetCellDataToStr(row, "大部件号");
                    model.等级 = DataHelper.GetCellDataToStr(row, "等级");
                    model.订单号 = DataHelper.GetCellDataToStr(row, "订单号");
                    model.零件号 = DataHelper.GetCellDataToStr(row, "零件号");
                    model.零件号描述 = DataHelper.GetCellDataToStr(row, "零件号描述");
                    model.主副驾 = DataHelper.GetCellDataToStr(row, "主副驾");               
                    list.Add(model);
                }
            }
            return list;
        }




        /// <summary>
        /// 指定物料查询
        /// </summary>
        /// <param name="id">零件号描述</param>
        /// <param name="er">前排 后40  后60 后坐垫</param>
        /// <returns></returns>
        public List<GetIndex> GetIndexWJ(string id, string er)
        {

            //var List = GetListModels();
            var List = GetListModels_ByWhere(id, er);
            List<GetIndex> searchlist = new List<GetIndex>();
            searchlist = List.ToList();
            //if (er=="前排")
            //{
            //    searchlist = List.Where(s => s.主副驾 == "主驾" || s.主副驾 == "副驾").Where(s => s.零件号描述.IndexOf(id) != -1).OrderByDescending(s => s.主副驾).OrderBy(s => s.ID).ToList();
            //}
            //else if (er == "后排RSB40" || er == "后40")
            //{
            //    searchlist = List.Where(s => s.主副驾 == "后40").Where(s => s.零件号描述.IndexOf(id) != -1).OrderByDescending(s => s.主副驾).OrderBy(s => s.ID).ToList();

            //}
            //else if (er == "后排RSB60" || er == "后60")
            //{
            //    searchlist = List.Where(s => s.主副驾 == "后60").Where(s => s.零件号描述.IndexOf(id) != -1).OrderByDescending(s => s.主副驾).OrderBy(s => s.ID).ToList();

            //}
            //else if (er == "后排RSC" || er == "后坐垫")
            //{
            //    searchlist = List.Where(s => s.主副驾 == "后坐垫").Where(s => s.零件号描述.IndexOf(id) != -1).OrderByDescending(s => s.主副驾).OrderBy(s => s.ID).ToList();

            //}
            //else
            //{
            //    return null;
            //}
            for (int i = 0; i < searchlist.Count; i++)
            {
                searchlist[i].ID = i + 1;
            }
            //searchlist = searchlist.Where(s => s.IsPrint == 0).ToList();
            return searchlist;

        }

        //指定订单车型等级查询
        public List<GetIndex> GetIndexOrder(string id)
        {
            var List = GetListModels().Where(s => (s.IsPrint == "0") && s.订单号 == id).OrderByDescending(s => s.主副驾).OrderBy(s => s.ID).ToList();
            return List;

        }
    }
    public class px_MaterialsortprintingDAL
    {
        public static List<classess> Querymg_classes()
        {
            List<classess> list = null;

            string sql = @" 
                            SELECT *
                              FROM  [mg_classes]             
	                          order by cl_id asc 
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<classess>();
                foreach (DataRow row in dt2.Rows)
                {
                    classess model = new classess();

                    model.cl_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "cl_id"));
                    model.cl_name = DataHelper.GetCellDataToStr(row, "cl_name");
                    model.cl_starttime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "cl_starttime"));
                    model.cl_endtime = Convert.ToDateTime(DataHelper.GetCellDataToStr(row, "cl_endtime"));
                    model.st_id = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "st_id"));
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<mg_PartOrde> mg_PartOrde()
        {
            List<mg_PartOrde> list = null;

            string sql = @"select CONVERT(nvarchar,id) as id,OrderIsPrintSYS from mg_PartOrder";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<mg_PartOrde>();
                foreach (DataRow row in dt2.Rows)
                {
                    mg_PartOrde model = new mg_PartOrde();

                    model.id = DataHelper.GetCellDataToStr(row, "id");
                    model.OrderIsPrintSYS = DataHelper.GetCellDataToStr(row, "OrderIsPrintSYS");                  
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<GetProductPrintID> GetProductPrintID()
        {
            List<GetProductPrintID> list = null;

            string sql = @"SELECT LEFT(ProductName, CHARINDEX('-', ProductName) - 1) as ProductName
                                          ,[ProductType]
                                          ,[ProductPrintID]
                                FROM [dbo].[mg_Product]  where ProductPrintID is not null";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<GetProductPrintID>();
                foreach (DataRow row in dt2.Rows)
                {
                    GetProductPrintID model = new GetProductPrintID();
                    model.ProductName = DataHelper.GetCellDataToStr(row, "ProductName");
                    model.ProductType = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ProductType"));
                    model.ProductPrintID = DataHelper.GetCellDataToStr(row, "ProductPrintID");
                    list.Add(model);
                }
            }
            return list;
        }

        public static List<px_IPAddress_Port> Getpx_IPAddress_Port()
        {
            List<px_IPAddress_Port> list = null;

            string sql = @"SELECT TOP(1) ID ,[IP] ,[Port]   
                                FROM [dbo].[px_IPAddress_Port]  order by ID asc";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List<px_IPAddress_Port>();
                foreach (DataRow row in dt2.Rows)
                {
                    px_IPAddress_Port model = new px_IPAddress_Port();
                    model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                    model.IP = DataHelper.GetCellDataToStr(row, "IP");
                    model.Port = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "Port"));
                    list.Add(model);
                }
            }
            return list;
        }

        public static int Updatepx_IPAddress_Port(px_IPAddress_Port model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update px_IPAddress_Port set ");          
            strSql.Append("IP=@IP,");
            strSql.Append("Port=@Port,");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@IP", SqlDbType.NVarChar),
					new SqlParameter("@Port", SqlDbType.Int),
					new SqlParameter("@ID", SqlDbType.Int)};

            parameters[0].Value = model.IP;
            parameters[1].Value = Convert.ToInt32(model.Port);
            parameters[2].Value = Convert.ToInt32(model.ID);
           

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }
        public static DataTable GetAllData()
        {
            string sql = @"SELECT [SID],[SRole] from [px_ShowChiClient] order by [SID] asc";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }
      
        public static int AddShowChiClient(Model.px_ShowChiClientModel model)
        {
            string maxid = @"set identity_insert px_ShowChiClient ON;
                               declare @i int;
                                SELECT @i=max([SID])  FROM [px_ShowChiClient];
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
            strSql.Append("insert into px_ShowChiClient(");
            strSql.Append("SID,SName,ClientIP,SAddTime,SRole)");
            strSql.Append(" values (");
            strSql.Append("@i,@SName,@ClientIP,@SAddTime,@SRole)");
            SqlParameter[] parameters = {
					new SqlParameter("@SName", SqlDbType.NVarChar),
					new SqlParameter("@ClientIP", SqlDbType.NVarChar),
                    new SqlParameter("@SAddTime", SqlDbType.DateTime),
                    new SqlParameter("@SRole", SqlDbType.NVarChar)};
            parameters[0].Value = model.SName;
            parameters[1].Value = model.ClientIP;
            parameters[2].Value =Convert.ToDateTime(model.SAddTime);
            parameters[3].Value = model.SRole;
            string end = @"set identity_insert px_ShowChiClient OFF";

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, maxid + strSql + end, parameters);
            return rows;
        }

        public static int UpdateShowChiClient(px_ShowChiClientModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update px_ShowChiClient set ");
            strSql.Append("SName=@SName,");
            strSql.Append("ClientIP=@ClientIP,");
            strSql.Append("SAddTime=@SAddTime,");
            strSql.Append("SRole=@SRole");
            strSql.Append(" where SID=@SID");  
            SqlParameter[] parameters = {
					new SqlParameter("@SName", SqlDbType.NVarChar),
					new SqlParameter("@ClientIP", SqlDbType.NVarChar),
					new SqlParameter("@SAddTime", SqlDbType.DateTime),
					new SqlParameter("@SRole", SqlDbType.NVarChar),
                    new SqlParameter("@SID", SqlDbType.Int)};
            parameters[0].Value = model.SName;
            parameters[1].Value = model.ClientIP;
            parameters[2].Value = Convert.ToDateTime(model.SAddTime);
            parameters[3].Value = model.SRole;
            parameters[4].Value = Convert.ToInt32(model.SID);

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, strSql.ToString(), parameters);
            return rows;
        }

        public static List<GetSP> QueryListForFirstPage(string pagesize, out string total, DateTime? starttime, DateTime? endtime, string csh)
        {
//            total = "0";
//            List<GetSP> list = null;

//            string sql1 = @"select count(id) total from [mg_Product];";
//            string sql2 = @" 
//                            SELECT top " + pagesize + @" [id]
//                                  ,[SName],[ClientIP]
//                                  ,[SAddTime],[SRole],[SRamark]
//                                 
//                              FROM  [mg_Product] 
//	                            order by id asc
//
//                                ";
//            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
//            if (DataHelper.HasData(ds))
//            {
//                DataTable dt1 = ds.Tables["count"];
//                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
//                DataTable dt2 = ds.Tables["data"];
//                list = new List<GetSP>();
//                foreach (DataRow row in dt2.Rows)
//                {
//                    GetSP model = new GetSP();
//                    //model.id = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "SID"));
//                    //model.ProductName = DataHelper.GetCellDataToStr(row, "SName");
//                    //model.ProductPrintID = DataHelper.GetCellDataToStr(row, "ClientIP");
//                    //model.ProductType = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "SAddTime"));
//                    //model.ProductDesc = DataHelper.GetCellDataToStr(row, "SRole");

//                    list.Add(model);
//                }
//            }
            total = "0";
             DateTime?[] time = null;
             int PageSize =NumericParse.StringToInt(pagesize);            
            int Page=1;
          
            if (starttime != null && endtime != null)
            {
                time = new DateTime?[] { starttime, endtime };
            }
               GetModel GetSPModels  = new GetModel();
              
            //var modellist = (List<GetSP>)getmodel.GetSPModels(time).Skip((Page - 1) * PageSize).Take(PageSize).ToList();
               var modellist = (List<GetSP>)GetSPModels.GetSPModels(((Page - 1) * PageSize), (PageSize), csh, time).ToList();
            if (csh != null && !csh.Equals(""))
            {
                 modellist = modellist.Where(s => s.订单号.Equals(csh)).ToList();
            }          


              return modellist;
        }
        private static string getyear(DateTime? ttime)
        {
            string result = "";
            try
            {
                if (ttime != null)
                {
                    string Month = ttime.Value.Month.ToString();
                    string Day = ttime.Value.Day.ToString();
                    if (Month.Length == 1)
                        Month = "0" + Month;
                    if (Day.Length == 1)
                        Day = "0" + Day;
                    result = ttime.Value.Year + "-" + Month + "-" + Day;
                }
            }
            catch { }
            return result;
        }
        public static List<GetSP> QueryListForPaging(string page, string pagesize, out string total, DateTime? starttime, DateTime? endtime, string csh)
        {
            total = "0";
            List<GetSP> list = null;

            string sql1 = @"select count(id) total from [mg_Product];";
            string sql2 = @" 
                            SELECT top " + pagesize + @" [id]
                                  ,[ProductName]
                                  ,[ProductDesc],[ProductType],[ProductPrintID]                            
                              FROM  [mg_Product] 
                                where  id > (
                                                select top 1 id from 
                                                        (select top ((" + page + @"-1)*" + pagesize + @") id from [mg_Product] where id is not null  order by id asc )t
                                                order by  id desc)
                                
	                            order by id asc

                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
                DataTable dt2 = ds.Tables["data"];
                list = new List<GetSP>();
                foreach (DataRow row in dt2.Rows)
                {
                    GetSP model = new GetSP();

                     //model.id = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "SID"));
                     //model.ProductName = DataHelper.GetCellDataToStr(row, "SName");
                     //model.ProductPrintID = DataHelper.GetCellDataToStr(row, "ClientIP");
                     //model.ProductType = Convert.ToInt32(DataHelper.GetCellDataToStr(row, "SAddTime"));
                     //model.ProductDesc = DataHelper.GetCellDataToStr(row, "SRole");
                    
                    list.Add(model);
                }
            }
            return list;
        }

        public static int DeleteShowChiClient(string SID)
        {
            string sql = @"delete from [px_ShowChiClient] where [SID]=" + SID;
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        }

        public static List< px_ShowChiClientModel> QueryMaterialSortingListForPart()
        {
            List< px_ShowChiClientModel> list = null;

            string sql2 = @" 
                            SELECT [SID]
                                  ,[SName]
                                  ,[ClientIP]
                                  ,[SAddTime]
                                  ,[SRole]
                                  ,[SRamark]
                              FROM  [px_ShowChiClient]             
	                          order by SID asc 
                                ";
            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text,  sql2,  null);
            if (DataHelper.HasData(dt))
            {
                DataTable dt2 = dt;
                list = new List< px_ShowChiClientModel>();
                foreach (DataRow row in dt2.Rows)
                {
                     px_ShowChiClientModel model = new  px_ShowChiClientModel();

                     model.SID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "SID"));
                     model.SName = DataHelper.GetCellDataToStr(row, "SName");
                     model.ClientIP = DataHelper.GetCellDataToStr(row, "ClientIP");

                    list.Add(model);
                }
            }
            return list;
        }
    }

}
