using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using Bll;
using System.Windows.Forms;

namespace website
{
    /// <summary>
    /// Services1004_Checks 的摘要说明
    /// </summary>
    public class Services1004_Checks : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string StartTime = request["StartTime"];
            string EndTime = request["EndTime"];
            string OrderCode = request["OrderCode"];
            string StationNo = request["StationNo"];
            if (OrderCode == "请选择")
            {
                OrderCode = "";
            }
            if (StationNo == "请选择")
            {
                StationNo = "";
            }
            int PageIndex = Convert.ToInt32(request["page"]);
            string JsonStr = "";
            DateTime date = DateTime.Now;
            string JsonStr3 = "";
            string neirong = "";
            string yee = "";
//            string JsonStr5 = null;
//            string SqlStr5 = null;
//            SqlStr5 = @"select distinct a.OrderNo from mg_Test_Part_Record a 
//where (a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "') order by a.OrderNo";
//            FunSql.Init();
//            DataTable ResTable5 = FunSql.GetTable(SqlStr5);
//            JsonStr5 = FunCommon.DataTableToJson(ResTable5);    //获取全部订单号
            if (OrderCode!="" && StationNo!="")    //输入订单号，输入工位的时候
            {
                if (StationNo == "FSA210")  //FSA210
                {
                    string SqlStr = @"select a.stationno as StationNo1,d.op_name,g.ItemCaption,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified,a.starttime,a.endtime,a.OrderNo from mg_Test_Repair_Item_Record a 
left join mg_Operator d on a.operatorid = d.op_id 
left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID 
where (a.StartTime > '" + StartTime + "' and a.EndTime < '" + EndTime + "'  ";
                    SqlStr += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
                    SqlStr += " and a.stationno='FSA210') order by a.OrderNo,a.StartTime  ";
                    DataTable ResTable = FunSql.GetTable(SqlStr);
                    if (ResTable.Rows.Count < 1)
                    {
                        JsonStr = "{}";
                    }
                    else
                    {
                        JsonStr = FunCommon.DataTableToJson(ResTable);
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
                        SqlStr += " and a.stationno='FSA160') order by a.OrderNo,a.CreateTime ";
                    }
                    else  //=1  170内容
                    {
                        SqlStr += " and a.stationno='FSA170') order by a.OrderNo,a.CreateTime ";
                    }
                    DataTable ResTable = FunSql.GetTable(SqlStr);
                    if (ResTable.Rows.Count < 1)
                    {
                        JsonStr = "{}";
                    }
                    else
                    {
                        JsonStr = FunCommon.DataTableToJson(ResTable);
                    }
                    int number = 1;
                    neirong = "检测";
                    yee = "1";
                    JsonStr3 = "{\"number\":\"" + number + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
                }
            }
            else if(OrderCode != "" && StationNo == "") //输入订单号的情况，工位不输
            {
                if ((PageIndex - 1) % 3 == 2)   //FSA210
                {
                    string SqlStr = @"select a.stationno as StationNo1,d.op_name,g.ItemCaption,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified,a.starttime,a.endtime,a.OrderNo from mg_Test_Repair_Item_Record a 
left join mg_Operator d on a.operatorid = d.op_id 
left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID 
where (a.StartTime > '" + StartTime + "' and a.EndTime < '" + EndTime + "'  ";
                    SqlStr += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
                    SqlStr += " and a.stationno='FSA210') order by a.OrderNo,a.StartTime  ";
                    DataTable ResTable = FunSql.GetTable(SqlStr);
                    if (ResTable.Rows.Count < 1)
                    {
                        JsonStr = "{}";
                    }
                    else
                    {
                        JsonStr = FunCommon.DataTableToJson(ResTable);
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
                        SqlStr += " and a.stationno='FSA160') order by a.OrderNo,a.CreateTime ";
                        DataTable ResTable = FunSql.GetTable(SqlStr);
                        if (ResTable.Rows.Count < 1)
                        {
                            JsonStr = "{}";
                        }
                        else
                        {
                            JsonStr = FunCommon.DataTableToJson(ResTable);
                        }
                        int number = 3;
                        neirong = "检测";
                        yee = "1/3";
                        StationNo = "FSA160";
                        JsonStr3 = "{\"number\":\"" + number + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
                    }
                    else  //FSA170
                    {
                        SqlStr += " and a.stationno='FSA170') order by a.OrderNo,a.CreateTime ";
                        DataTable ResTable = FunSql.GetTable(SqlStr);
                        if (ResTable.Rows.Count < 1)
                        {
                            JsonStr = "{}";
                        }
                        else
                        {
                            JsonStr = FunCommon.DataTableToJson(ResTable);
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
                OrderCode = FunSql.GetString(SqlStr1);  //获取所需的一个订单号
                if ((PageIndex - 1) % 3 == 2)  //FSA210
                {
                    string SqlStr = @"select a.stationno as StationNo1,d.op_name,g.ItemCaption,case when a.IsQualified = 1 then '合格'   else '不合格' end as IsQualified,a.starttime,a.endtime,a.OrderNo from mg_Test_Repair_Item_Record a 
left join mg_Operator d on a.operatorid = d.op_id 
left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID 
where (a.StartTime > '" + StartTime + "' and a.EndTime < '" + EndTime + "'  ";
                    SqlStr += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode + "'";
                    SqlStr += " and a.stationno='FSA210') order by a.OrderNo,a.StartTime  ";
                    DataTable ResTable = FunSql.GetTable(SqlStr);
                    if (ResTable.Rows.Count < 1)
                    {
                        JsonStr = "{}";
                    }
                    else
                    {
                        JsonStr = FunCommon.DataTableToJson(ResTable);
                    }
                    if (JsonStr=="]")
                    {
                        JsonStr = "";
                    }
                    SqlStr2 = "select count(distinct OrderNo)  from mg_Test_Repair_Item_Record where (StartTime > '" + StartTime + "' and EndTime < '" + EndTime + "' ) ";
                    int number = FunSql.GetInt(SqlStr2);
                    neirong = "返修";
                    yee = "3/3";
                    StationNo = "FSA210";
                    JsonStr3 = "{\"number\":\"" + number*3 + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
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
                        SqlStr += " and a.stationno='FSA160') order by a.OrderNo,a.CreateTime ";
                        DataTable ResTable = FunSql.GetTable(SqlStr);
                        if (ResTable.Rows.Count<1)
                        {
                            JsonStr = "{}";
                        }
                        else
                        {
                            JsonStr = FunCommon.DataTableToJson(ResTable);
                        }
                       
                        //if (JsonStr == "]")
                        //{
                        //    JsonStr = "";
                        //}
                        SqlStr2 = "select count(distinct OrderNo)  from mg_Test_Part_Record where (CreateTime > '" + StartTime + "' and CreateTime < '" + EndTime + "' ) ";
                        int number = FunSql.GetInt(SqlStr2);
                        neirong = "检测";
                        yee = "1/3";
                        StationNo = "FSA160";
                        JsonStr3 = "{\"number\":\"" + number*3 + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "}";
                    }
                    else  //FSA170
                    {
                        SqlStr += " and a.stationno='FSA170') order by a.OrderNo,a.CreateTime ";
                        DataTable ResTable = FunSql.GetTable(SqlStr);
                        if (ResTable.Rows.Count < 1)
                        {
                            JsonStr = "{}";
                        }
                        else
                        {
                            JsonStr = FunCommon.DataTableToJson(ResTable);
                        }
                        //if (JsonStr == "]")
                        //{
                        //    JsonStr = "\"\"";
                        //}
                        SqlStr2 = "select count(distinct OrderNo)  from mg_Test_Part_Record where (CreateTime > '" + StartTime + "' and CreateTime < '" + EndTime + "' ) ";
                        int number = FunSql.GetInt(SqlStr2);
                        neirong = "检测";
                        yee = "2/3";
                        StationNo = "FSA170";
                        JsonStr3 = "{\"number\":\"" + number*3 + "\",\"neirong\":\"" + neirong + "\",\"date\":\"" + date + "\",\"yee\":\"" + yee + "\",\"OrderNo\":\"" + OrderCode + "\",\"StationNo\":\"" + StationNo + "\",\"data\":" + JsonStr + "";
                    }
                }
            }
            #region 导出代码   
            string SqlStr4 = null;
            DataTable ResTable4 = default(DataTable);
            #region 选工位的情况，也就是全选
            string OrderCode1 = request["OrderCode"];
            string StationNo1 = request["StationNo"];
            if (OrderCode1 == "请选择")
            {
                OrderCode1 = "";
            }
            if (StationNo1 == "请选择")
            {
                StationNo1 = "";
            }
            if (!String.IsNullOrEmpty(StationNo1))
            {
                if (StationNo1 == "FSA210")
                {
                    SqlStr4 = @"select a.stationno as 工位,d.op_name 操作员,a.OrderNo 订单号,g.ItemCaption 返修内容,case when a.IsQualified = 1 then '合格'   else '不合格' end as 是否合格,a.starttime 返修开始时间,a.endtime 返修结束时间 from mg_Test_Repair_Item_Record a 
left join mg_Operator d on a.operatorid = d.op_id 
left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID
where (a.starttime > '" + StartTime + "' and a.endtime < '" + EndTime + "'  ";
                    SqlStr4 += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode1 + "'";
                    SqlStr4 += " and a.stationno='FSA210' )order by a.OrderNo  ";
                }
                else
                {
                    SqlStr4 = @"select a.stationno 工位,d.op_name 操作员,a.OrderNo 订单号,c.TestCaption 检测内容,a.TestValue 真实值,case when a.IsQualified = 1 then '合格'   else '不合格' end as 是否合格,a.CreateTime 检测时间 from mg_Test_Part_Record a 
left join mg_Test_Part b on  a.Test_PartID = b.ID 
left join mg_test c on b.TestID = c.id 
left join mg_Operator d on a.operatorid = d.op_id 
where (a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "' ";
                    SqlStr4 += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode1 + "'";
                    if (StationNo1 == "FSA160")   //160内容
                    {
                        SqlStr4 += " and a.stationno='FSA160' )order by a.OrderNo ";
                    }
                    else  //=1  170内容
                    {
                        SqlStr4 += " and a.stationno='FSA170' )order by a.OrderNo ";
                    }
                }
                ResTable4 = FunSql.GetTable(SqlStr4);

                if (StationNo1!= "FSA210")
                {
                    for (int i = 0; i < ResTable4.Rows.Count; i++)
                    {
                        string value = ResTable4.Rows[i]["检测内容"].ToString();
                        if (value != "安全气囊电阻" && value != "SBR电阻" && value != "安全带插入电流" && value != "安全带拔出电流")
                        {
                            //string aa ;
                            //aa = ResTable4.Rows[i]["真实值"].ToString();
                            //aa = "";
                            DataRow drEmployee = ResTable4.Rows[i];
                            drEmployee.BeginEdit();
                            drEmployee["真实值"] = DBNull.Value;
                            drEmployee.EndEdit();
                        }
                    }
                }

            }
            #endregion
            else
            {   //不选工位，只选前3个
                SqlStr4 = @"(select a.stationno 工位,d.op_name 操作员,a.OrderNo 订单号,null 检测内容,null 真实值, null 是否合格,null 检测时间,g.ItemCaption as 返修内容, a.starttime 返修开始时间,a.endtime 返修结束时间 from mg_Test_Repair_Item_Record a 
left join mg_Operator d on a.operatorid = d.op_id 
left join mg_Test_Repair_Item g on a.Repair_ItemID = g.ID 
where (a.starttime > '" + StartTime + "' and a.endtime < '" + EndTime + "'   and a.stationno='FSA210' ";
                if (!String.IsNullOrEmpty(OrderCode1))
                {
                    SqlStr4 += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode1 + "'";
                }
                SqlStr4 += " )) union all ";
                SqlStr4 += @"(select a.stationno 工位,d.op_name 操作员,a.OrderNo 订单号,c.TestCaption as 检测内容,a.TestValue 真实值,case when a.IsQualified = 1 then '合格'   else '不合格' end as 是否合格,a.CreateTime 检测时间,null 返修内容,null 返修开始时间,null 返修结束时间 from mg_Test_Part_Record a 
left join mg_Test_Part b on a.Test_PartID = b.ID
left join mg_test c on b.TestID = c.id
left join mg_Operator d on a.operatorid = d.op_id
where (a.CreateTime > '" + StartTime + "' and a.CreateTime < '" + EndTime + "'  and(a.stationno = 'FSA160' or a.stationno = 'FSA170')";
                if (!String.IsNullOrEmpty(OrderCode1))
                {
                    SqlStr4 += " and REPLACE(a.OrderNo, CHAR(13) + CHAR(10), '') = '" + OrderCode1 + "'";
                }
                SqlStr4 += "  )) order by a.OrderNo,a.stationno";
                 ResTable4 = FunSql.GetTable(SqlStr4);
                for (int i = 0; i < ResTable4.Rows.Count; i++)
                {
                    string value = ResTable4.Rows[i]["检测内容"].ToString();
                    if (value != "安全气囊电阻" && value != "SBR电阻" && value != "安全带插入电流" && value != "安全带拔出电流")
                    {
                        //string aa ;
                        //aa = ResTable4.Rows[i]["真实值"].ToString();
                        //aa = "";

                        DataRow drEmployee = ResTable4.Rows[i];
                        drEmployee.BeginEdit();
                        drEmployee["真实值"] = DBNull.Value;
                        drEmployee.EndEdit();
                    }
                }
            }
            ExcelHelper.ExportDTtoExcel(ResTable4, "", HttpContext.Current.Request.MapPath("~/App_Data/excel2017.xlsx"));
            #endregion
            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonStr3);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}