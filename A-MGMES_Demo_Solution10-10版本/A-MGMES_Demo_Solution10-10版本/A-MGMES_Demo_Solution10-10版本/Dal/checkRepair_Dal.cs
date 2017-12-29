using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbUtility;
using Tools;
using Model;
namespace Dal
{
   public class checkRepair_Dal
    {
       public static String GetListNew(string StartTime, string EndTime, string OrderCode, string StationNo, int PageIndex, int PageSize, out int totalcount)
       {
           int StartIndex = (PageIndex - 1) * PageSize + 1;
           int EndIndex = PageIndex * PageSize;
           totalcount = 0;
           string where = " where 1=1 ";
           if(StationNo == "FSA210")
           {
               where += "   and a.StartTime > '" + StartTime + "' and a.EndTime < '" + EndTime + "'  ";
               where += "   and a.stationno='" + StationNo + "'";
               if (!string.IsNullOrEmpty(OrderCode))
               {
                   where += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
               }
               string orderStr = " order by a.StartTime , a.OrderNo ";
               string SqlStr = @"select  ROW_NUMBER() over(order by  a.StartTime , a.OrderNo ) as rowid
 , a.stationno as StationNo
 ,d.op_name
 ,g.ItemCaption
 ,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified
 ,a.starttime as StartTime
 ,a.endtime as EndTime
 ,a.OrderNo from mg_Test_Repair_Item_Record a 
left join mg_Operator d on a.operatorid = d.op_id 
left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID ";
             SqlStr += where;
            List<mg_Repair> list = new List<mg_Repair>();
            int total = 0 ;
            string query_sql = " select * from ( "+SqlStr+" ) as Results where rowid >=" + StartIndex + " and rowid <=" + EndIndex + " order by rowid ";
            string count_sql = "select  count(*) as total from ( " + SqlStr + " ) AS T";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, count_sql + query_sql, new string[] { "count", "data" }, null);
            if (DataHelper.HasData(ds))
            {
                DataTable dt1 = ds.Tables["count"];
                total = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt1.Rows[0], "total"));
                DataTable dt2 = ds.Tables["data"];
                foreach (DataRow row in dt2.Rows)
                {
                      mg_Repair model = new mg_Repair();
                      model.rowid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "rowid"));
                      model.StationNo = DataHelper.GetCellDataToStr(row, "StationNo");
                      model.op_name=DataHelper.GetCellDataToStr(row, "op_name");
                      model.ItemCaption = DataHelper.GetCellDataToStr(row, "ItemCaption");
                      model.IsQualifiedstr = DataHelper.GetCellDataToStr(row, "IsQualified");
                      model.CreateTime = (DataHelper.GetCellDataToStr(row, "StartTime"));
                      model.StartTime = (DataHelper.GetCellDataToStr(row, "StartTime"));
                      model.EndTime = (DataHelper.GetCellDataToStr(row, "EndTime"));
                      model.OrderNo = DataHelper.GetCellDataToStr(row, "OrderNo");
                    list.Add(model);
                }
            }
            DataListModel<mg_Repair> allmodel = new DataListModel<mg_Repair>();
            allmodel.total = total.ToString();
            allmodel.rows = list;
            string jsonStr = JSONTools.ScriptSerialize<DataListModel<mg_Repair>>(allmodel);
            return jsonStr;   
              
           }
           else
           {
               where += "   and a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "'  ";
               where += "   and a.stationno='" + StationNo + "'";
               if (!string.IsNullOrEmpty(OrderCode))
               {
                   where += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
               }
               string orderStr = " order by a.CreateTime , a.OrderNo ";
               string SqlStr = @"select  ROW_NUMBER() over(order by  a.CreateTime , a.OrderNo ) as rowid
 , a.stationno as StationNo
,d.op_name
,c.TestCaption as ItemCaption
,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified
,a.CreateTime
,a.OrderNo
,a.TestValue
,c.TestValueMin
,c.TestValueMax 
from mg_Test_Part_Record a 
left join mg_Test_Part b on  a.Test_PartID = b.ID 
left join mg_test c on b.TestID = c.id 
left join mg_Operator d on a.operatorid = d.op_id  ";
               SqlStr += where;
               List<mg_Repair> list = new List<mg_Repair>();
               int total = 0;
               string query_sql = " select * from ( " + SqlStr + " ) as Results where rowid >=" + StartIndex + " and rowid <=" + EndIndex + " order by rowid ";
               string count_sql = "select  count(*) as total from ( " + SqlStr + " ) AS T ";
               DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, count_sql + query_sql, new string[] { "count", "data" }, null);
               if (DataHelper.HasData(ds))
               {
                   DataTable dt1 = ds.Tables["count"];
                   total = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt1.Rows[0], "total"));
                   DataTable dt2 = ds.Tables["data"];
                   foreach (DataRow row in dt2.Rows)
                   {
                       mg_Repair model = new mg_Repair();
                       model.rowid = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "rowid"));
                       model.StationNo = DataHelper.GetCellDataToStr(row, "StationNo");
                       model.op_name = DataHelper.GetCellDataToStr(row, "op_name");
                       model.TestCaption = DataHelper.GetCellDataToStr(row, "ItemCaption");
                       model.TestValue = (DataHelper.GetCellDataToStr(row, "TestValue"));
                       model.TestValueMin = (DataHelper.GetCellDataToStr(row, "TestValueMin"));
                       model.TestValueMax= (DataHelper.GetCellDataToStr(row, "TestValueMax"));
                       string value = model.TestCaption;
                       if (value != "安全气囊电阻" && value != "SBR电阻" && value != "安全带插入电流" && value != "安全带拔出电流" && value != "SBR断路电阻" && value != "SBR下压前断路电阻")
                       {
                          model.TestValue = " ";
                       }
                      
                       model.IsQualifiedstr = DataHelper.GetCellDataToStr(row, "IsQualified");
                       model.CreateTime = (DataHelper.GetCellDataToStr(row, "CreateTime"));
                       model.OrderNo = DataHelper.GetCellDataToStr(row, "OrderNo");
                       list.Add(model);
                   }
               }
               DataListModel<mg_Repair> allmodel = new DataListModel<mg_Repair>();
               allmodel.total = total.ToString();
               allmodel.rows = list;
               string jsonStr = JSONTools.ScriptSerialize<DataListModel<mg_Repair>>(allmodel);
               return jsonStr;   


           }
           return "";
       }
       public static string getTableString(string StartTime, string EndTime, string OrderCode, string StationNo, int PageIndex, out int totalcount)
       {
           string JsonStr3 = "";
           string JsonStr = "";
           string neirong = "";
           string yee = "";
            DateTime date = DateTime.Now;
            DataTable ResTable = new DataTable();
           if (OrderCode != "" && StationNo != "")    //输入订单号，输入工位的时候
           {
               if (StationNo == "FSA210")  //FSA210
               {
                   string SqlStr = @"select a.stationno as StationNo1,d.op_name,g.ItemCaption,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified,a.starttime,a.endtime,a.OrderNo from mg_Test_Repair_Item_Record a 
left join mg_Operator d on a.operatorid = d.op_id 
left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID 
where (a.StartTime > '" + StartTime + "' and a.EndTime < '" + EndTime + "'  ";
                   SqlStr += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
                   SqlStr += " and a.stationno='FSA210') order by g.ItemCaption, a.OrderNo,a.StartTime  ";
                    ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                   if (ResTable.Rows.Count < 1)
                   {
                       JsonStr = "{}";
                   }
                   else
                   {
                       JsonStr = JSONTools.DataTableToJson(ResTable);
                   }
                   int number = 1;
                   neirong = "返修";
                   yee = "1";
                   JsonStr3 = "{\"number\":\"" + number + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
               }
               else  //FSA160和FSA170 
               {
                   string SqlStr = @"select a.stationno,d.op_name,c.TestCaption,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified,a.CreateTime,a.OrderNo,a.TestValue from mg_Test_Part_Record a 
left join mg_Test_Part b on  a.Test_PartID = b.ID 
left join mg_test c on b.TestID = c.id 
left join mg_Operator d on a.operatorid = d.op_id 
where (a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "' ";
                   SqlStr += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";

                   if (StationNo == "FSA160")   //160内容
                   {
                       SqlStr += " and a.stationno='FSA160') order by c.TestCaption, a.OrderNo,a.CreateTime ";
                   }
                   else  //=1  170内容
                   {
                       SqlStr += " and a.stationno='FSA170') order by c.TestCaption,a.OrderNo,a.CreateTime ";
                   }
                   ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                   if (ResTable.Rows.Count < 1)
                   {
                       JsonStr = "{}";
                   }
                   else
                   {
                       JsonStr = JSONTools.DataTableToJson(ResTable);
                   }
                   int number = 1;
                   neirong = "检测";
                   yee = "1";
                   JsonStr3 = "{\"number\":\"" + number + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
               }
           }
           else if (OrderCode != "" && StationNo == "") //输入订单号的情况，工位不输
           {
               if ((PageIndex - 1) % 3 == 2)   //FSA210
               {
                   string SqlStr = @"select a.stationno as StationNo1,d.op_name,g.ItemCaption,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified,a.starttime,a.endtime,a.OrderNo from mg_Test_Repair_Item_Record a 
left join mg_Operator d on a.operatorid = d.op_id 
left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID 
where (a.StartTime > '" + StartTime + "' and a.EndTime < '" + EndTime + "'  ";
                   SqlStr += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
                   SqlStr += " and a.stationno='FSA210') order by g.ItemCaption,a.OrderNo,a.StartTime  ";
                    ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                   if (ResTable.Rows.Count < 1)
                   {
                       JsonStr = "{}";
                   }
                   else
                   {
                       JsonStr = JSONTools.DataTableToJson(ResTable);
                   }
                   int number = 3;
                   neirong = "返修";
                   yee = "3/3";
                   StationNo = "FSA210";
                   JsonStr3 = "{\"number\":\"" + number + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
               }
               else    //FSA160 FSA170
               {
                   string SqlStr = @"select a.stationno,d.op_name,c.TestCaption,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified,a.CreateTime,a.OrderNo,a.TestValue from mg_Test_Part_Record a 
left join mg_Test_Part b on  a.Test_PartID = b.ID 
left join mg_test c on b.TestID = c.id 
left join mg_Operator d on a.operatorid = d.op_id 
where( a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "' ";
                   SqlStr += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
                   if ((PageIndex - 1) % 3 == 0)  //FSA160
                   {
                       SqlStr += " and a.stationno='FSA160') order by c.TestCaption,  a.OrderNo,a.CreateTime ";
                       ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                       if (ResTable.Rows.Count < 1)
                       {
                           JsonStr = "{}";
                       }
                       else
                       {
                           JsonStr = JSONTools.DataTableToJson(ResTable);
                       }
                       int number = 3;
                       neirong = "检测";
                       yee = "1/3";
                       StationNo = "FSA160";
                       JsonStr3 = "{\"number\":\"" + number + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
                   }
                   else  //FSA170
                   {
                       SqlStr += " and a.stationno='FSA170') order by c.TestCaption,a.OrderNo,a.CreateTime ";
                       ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                       if (ResTable.Rows.Count < 1)
                       {
                           JsonStr = "{}";
                       }
                       else
                       {
                           JsonStr = JSONTools.DataTableToJson(ResTable);
                       }
                       int number = 3;
                       neirong = "检测";
                       yee = "2/3";
                       StationNo = "FSA170";
                       JsonStr3 = "{\"number\":\"" + number + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
                   }

               }
           }
           else     //只输日期
           {
               string SqlStr1 = @"select distinct top 1 a.OrderNo from mg_Test_Part_Record a 
where a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + @"' and a.OrderNo 
not in(select distinct top(1*" + (PageIndex - 1) / 3 + @") a.OrderNo from mg_Test_Part_Record a 
where (a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "')order by a.OrderNo ) order by a.OrderNo";
               string SqlStr2 = "";
               object ob = SqlHelper.ExecuteScalar(SqlHelper.SqlConnString, System.Data.CommandType.Text, SqlStr1, null);  //获取所需的一个订单号
               OrderCode = (ob == null) ? "" : ob.ToString();
               if ((PageIndex - 1) % 3 == 2)  //FSA210
               {
                   string SqlStr = @"select a.stationno as StationNo1,d.op_name,g.ItemCaption,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified,a.starttime,a.endtime,a.OrderNo from mg_Test_Repair_Item_Record a 
left join mg_Operator d on a.operatorid = d.op_id 
left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID 
where (a.StartTime > '" + StartTime + "' and a.EndTime < '" + EndTime + "'  ";
                   if(!string.IsNullOrEmpty(OrderCode))
                   {
                       SqlStr += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
                   }

                   SqlStr += " and a.stationno='FSA210') order by g.ItemCaption,a.OrderNo,a.StartTime  ";
                   ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                   if (ResTable.Rows.Count < 1)
                   {
                       JsonStr = "{}";
                   }
                   else
                   {
                       JsonStr = JSONTools.DataTableToJson(ResTable);
                   }
                   if (JsonStr == "]")
                   {
                       JsonStr = "";
                   }
                   SqlStr2 = "select count(distinct OrderNo)  from mg_Test_Repair_Item_Record where (StartTime > '" + StartTime + "' and EndTime < '" + EndTime + "' ) ";
                   int number = 0;
                   ob = SqlHelper.ExecuteScalar(SqlHelper.SqlConnString, System.Data.CommandType.Text, SqlStr2, null); 
                   if(ob==null)
                   {
                       number = 0;
                   }
                   else
                   {
                       number = Convert.ToInt32(ob.ToString());
                   }
                   neirong = "返修";
                   yee = "3/3";
                   StationNo = "FSA210";
                   JsonStr3 = "{\"number\":\"" + number * 3 + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
               }
               else
               {
                   string SqlStr = @"select a.stationno,d.op_name,c.TestCaption,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified,a.CreateTime,a.OrderNo,a.TestValue from mg_Test_Part_Record a 
left join mg_Test_Part b on  a.Test_PartID = b.ID 
left join mg_test c on b.TestID = c.id 
left join mg_Operator d on a.operatorid = d.op_id 
where( a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "' ";
                   SqlStr += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
                   if ((PageIndex - 1) % 3 == 0)  //FSA160
                   {
                       SqlStr += " and a.stationno='FSA160') order by c.TestCaption, a.OrderNo,a.CreateTime ";
                       ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                       if (ResTable.Rows.Count < 1)
                       {
                           JsonStr = "{}";
                       }
                       else
                       {
                           JsonStr = JSONTools.DataTableToJson(ResTable);
                       }

                       //if (JsonStr == "]")
                       //{
                       //    JsonStr = "";
                       //}
                       SqlStr2 = "select count(distinct OrderNo)  from mg_Test_Part_Record where (CreateTime > '" + StartTime + "' and CreateTime < '" + EndTime + "' ) ";
                       int number = 0;
                       ob = SqlHelper.ExecuteScalar(SqlHelper.SqlConnString, System.Data.CommandType.Text, SqlStr2, null);
                       if (ob == null)
                       {
                           number = 0;
                       }
                       else
                       {
                           number = Convert.ToInt32(ob.ToString());
                       }
                       neirong = "检测";
                       yee = "1/3";
                       StationNo = "FSA160";
                       JsonStr3 = "{\"number\":\"" + number * 3 + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
                   }
                   else  //FSA170
                   {
                       SqlStr += " and a.stationno='FSA170') order by c.TestCaption, a.OrderNo,a.CreateTime ";
                       ResTable = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr, null);
                       if (ResTable.Rows.Count < 1)
                       {
                           JsonStr = "{}";
                       }
                       else
                       {
                           JsonStr = JSONTools.DataTableToJson(ResTable);
                       }
                       //if (JsonStr == "]")
                       //{
                       //    JsonStr = "\"\"";
                       //}
                       SqlStr2 = "select count(distinct OrderNo)  from mg_Test_Part_Record where (CreateTime > '" + StartTime + "' and CreateTime < '" + EndTime + "' ) ";
                       int number = 0;
                       ob = SqlHelper.ExecuteScalar(SqlHelper.SqlConnString, System.Data.CommandType.Text, SqlStr2, null);
                       if (ob == null)
                       {
                           number = 0;
                       }
                       else
                       {
                           number = Convert.ToInt32(ob.ToString());
                       }
                       neirong = "检测";
                       yee = "2/3";
                       StationNo = "FSA170";
                       JsonStr3 = "{\"number\":\"" + number * 3 + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "";
                   }
               }
           }
           totalcount = ResTable.Rows.Count;
           return JsonStr3;
       }
       public static DataTable getTableExcel(string StartTime, string EndTime, string OrderCode, string StationNo, int PageIndex, out int total)
       {

           total = 0;
           string where = " where 1=1 ";
           if (StationNo == "FSA210")
           {
               where += "   and a.StartTime > '" + StartTime + "' and a.EndTime < '" + EndTime + "'  ";
               where += "   and a.stationno='" + StationNo + "'";
               if (!string.IsNullOrEmpty(OrderCode))
               {
                   where += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
               }
               string orderStr = " order by a.StartTime , a.OrderNo ";
               string SqlStr = @"select  ROW_NUMBER() over(order by  a.StartTime , a.OrderNo ) as 序号
 , a.stationno as 工位 
 ,d.op_name as 操作员
 ,g.ItemCaption 返修内容
 ,case when a.IsQualified = 1 then '合格'   else '不合格' end as 是否合格
 ,a.starttime as 返修开始时间
 ,a.endtime as 返修结束时间
 ,a.OrderNo  as 订单号 from mg_Test_Repair_Item_Record a 
left join mg_Operator d on a.operatorid = d.op_id 
left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID ";
               SqlStr += where;

               string query_sql = SqlStr + "  order by 序号 asc ";
               string count_sql = "select  count(*) as total from ( " + SqlStr + " ) AS T    ";
               DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, count_sql + query_sql, new string[] { "count", "data" }, null);
               if (DataHelper.HasData(ds))
               {
                   DataTable dt1 = ds.Tables["count"];
                   total = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt1.Rows[0], "total"));
                   DataTable dt2 = ds.Tables["data"];
                   return dt2;
               }
           }
           else
           {
               where += "   and a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "'  ";
               where += "   and a.stationno='" + StationNo + "'";
               if (!string.IsNullOrEmpty(OrderCode))
               {
                   where += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
               }
               string orderStr = " order by a.CreateTime , a.OrderNo ";
               string SqlStr = @" select  ROW_NUMBER() over(order by  a.CreateTime , a.OrderNo ) as 序号
 , a.stationno as 工位
,d.op_name as 操作员
,c.TestCaption as 检测内容
,a.TestValue as 检测值
,c.TestValueMin as 最小值
,c.TestValueMax as 最大值
,case when a.IsQualified = 1 then '合格'   else '不合格' end as 是否合格
,a.CreateTime as 检测时间
,a.OrderNo as 订单号
 from mg_Test_Part_Record a 
left join mg_Test_Part b on  a.Test_PartID = b.ID 
left join mg_test c on b.TestID = c.id 
left join mg_Operator d on a.operatorid = d.op_id  ";
               SqlStr += where;
               List<mg_Repair> list = new List<mg_Repair>();
               total = 0;
               string query_sql = SqlStr + "  order by 序号 asc ";
               string count_sql = " select  count(*) as total from ( " + SqlStr + " ) AS T  ";
               DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, count_sql + query_sql, new string[] { "count", "data" }, null);
               if (DataHelper.HasData(ds))
               {
                   DataTable dt1 = ds.Tables["count"];
                   total = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt1.Rows[0], "total"));
                   DataTable dt2 = ds.Tables["data"];
                   for (int i = 0; i < dt2.Rows.Count; i++)
                   {
                       string value = dt2.Rows[i]["检测内容"].ToString();
                       if (value != "安全气囊电阻" && value != "SBR电阻" && value != "安全带插入电流" && value != "安全带拔出电流" && value != "SBR断路电阻" && value != "SBR下压前断路电阻")
                       {
                           //string aa ;
                           //aa = ResTable4.Rows[i]["真实值"].ToString();
                           //aa = "";
                           DataRow drEmployee = dt2.Rows[i];
                           drEmployee.BeginEdit();
                           drEmployee["检测值"] = DBNull.Value;
                           drEmployee.EndEdit();
                       }
                   }
                   return dt2;
               }

           }
           return null;
       }
//        public static DataTable getTableExcel(string StartTime, string EndTime, string OrderCode1, string StationNo1, int PageIndex, out int totalcount)
//        {
//            DataTable ResTable4 = new DataTable();
//            string SqlStr4="";
//            #region
//            if (!String.IsNullOrEmpty(StationNo1))
//                {
//                    if (StationNo1 == "FSA210")
//                    {
//                        SqlStr4 = @"select a.stationno as 工位,d.op_name 操作员,a.OrderNo 订单号,g.ItemCaption 返修内容,case when a.IsQualified = 1 then '合格'   else '不合格' end as 是否合格,a.starttime 返修开始时间,a.endtime 返修结束时间 from mg_Test_Repair_Item_Record a 
//left join mg_Operator d on a.operatorid = d.op_id 
//left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID
//where (a.starttime > '" + StartTime + "' and a.endtime < '" + EndTime + "'  ";
//                        SqlStr4 += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode1 + "'";
//                        SqlStr4 += " and a.stationno='FSA210' )order by g.ItemCaption, a.OrderNo  ";
//                    }
//                    else
//                    {
//                        SqlStr4 = @"select a.stationno 工位,d.op_name 操作员,a.OrderNo 订单号,c.TestCaption 检测内容,a.TestValue 真实值,case when a.IsQualified = 1 then '合格'   else '不合格' end as 是否合格,a.CreateTime 检测时间 from mg_Test_Part_Record a 
//left join mg_Test_Part b on  a.Test_PartID = b.ID 
//left join mg_test c on b.TestID = c.id 
//left join mg_Operator d on a.operatorid = d.op_id 
//where (a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "' ";
//                        SqlStr4 += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode1 + "'";
//                        if (StationNo1 == "FSA160")   //160内容
//                        {
//                            SqlStr4 += " and a.stationno='FSA160' )order by c.TestCaption, a.OrderNo ";
//                        }
//                        else  //=1  170内容
//                        {
//                            SqlStr4 += " and a.stationno='FSA170' )order by c.TestCaption,a.OrderNo ";
//                        }
//                    }
//                    ResTable4 = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr4, null);

//                    if (StationNo1 != "FSA210")
//                    {
//                        for (int i = 0; i < ResTable4.Rows.Count; i++)
//                        {
//                            string value = ResTable4.Rows[i]["检测内容"].ToString();
//                            if (value != "安全气囊电阻" && value != "SBR电阻" && value != "安全带插入电流" && value != "安全带拔出电流" && value != "SBR断路电阻" && value != "SBR下压前断路电阻")
//                            {
//                                //string aa ;
//                                //aa = ResTable4.Rows[i]["真实值"].ToString();
//                                //aa = "";
//                                DataRow drEmployee = ResTable4.Rows[i];
//                                drEmployee.BeginEdit();
//                                drEmployee["真实值"] = DBNull.Value;
//                                drEmployee.EndEdit();
//                            }
//                        }
//                    }

//                }
//                #endregion
//                else
//                {   //不选工位，只选前3个
//                    SqlStr4 = @"(select a.stationno 工位,d.op_name 操作员,a.OrderNo 订单号,null 检测内容,null 真实值, null 是否合格,null 检测时间,g.ItemCaption as 返修内容, a.starttime 返修开始时间,a.endtime 返修结束时间 from mg_Test_Repair_Item_Record a 
//left join mg_Operator d on a.operatorid = d.op_id 
//left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID 
//where (a.starttime > '" + StartTime + "' and a.endtime < '" + EndTime + "'   and a.stationno='FSA210' ";
//                    if (!String.IsNullOrEmpty(OrderCode1))
//                    {
//                        SqlStr4 += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode1 + "'";
//                    }
//                    SqlStr4 += " )) union all ";
//                    SqlStr4 += @"(select a.stationno 工位,d.op_name 操作员,a.OrderNo 订单号,c.TestCaption as 检测内容,a.TestValue 真实值,case when a.IsQualified = 1 then '合格'   else '不合格' end as 是否合格,a.CreateTime 检测时间,null 返修内容,null 返修开始时间,null 返修结束时间 from mg_Test_Part_Record a 
//left join mg_Test_Part b on a.Test_PartID = b.ID
//left join mg_test c on b.TestID = c.id
//left join mg_Operator d on a.operatorid = d.op_id
//where (a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "'  and(a.stationno = 'FSA160' or a.stationno = 'FSA170')";
//                    if (!String.IsNullOrEmpty(OrderCode1))
//                    {
//                        SqlStr4 += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode1 + "'";
//                    }
//                    SqlStr4 += "  )) order by a.OrderNo,a.stationno";
//                    ResTable4 = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, SqlStr4, null);
//                    for (int i = 0; i < ResTable4.Rows.Count; i++)
//                    {
//                        string value = ResTable4.Rows[i]["检测内容"].ToString();
//                        if (value != "安全气囊电阻" && value != "SBR电阻" && value != "安全带插入电流" && value != "安全带拔出电流" && value != "SBR断路电阻" && value != "SBR下压前断路电阻")
//                        {
//                            //string aa ;
//                            //aa = ResTable4.Rows[i]["真实值"].ToString();
//                            //aa = "";

//                            DataRow drEmployee = ResTable4.Rows[i];
//                            drEmployee.BeginEdit();
//                            drEmployee["真实值"] = DBNull.Value;
//                            drEmployee.EndEdit();
//                        }
//                    }
//             }
//            totalcount = ResTable4.Rows.Count;
//            return ResTable4;
//        }
    }
}
