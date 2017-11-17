using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace website
{
    /// <summary>
    /// Services1007_CheckReport 的摘要说明
    /// </summary>
    public class Services1007_CheckReport : IHttpHandler
    {
        HttpRequest Request = null;
        HttpResponse Response = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Request = context.Request;
            Response = context.Response;
            string method = Request.Params["method"];
            switch (method)
            {

                case "SelectAssemblyLine":
                    SelectAssemblyLine();
                    break;
                case "SelectStation":
                    SelectStation();
                    break;
                case "SelectCheckReport":
                    SelectCheckReport();
                    break;
            }
        }
        void SelectAssemblyLine()    //查询流水线
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string sql = "select fl_name from mg_FlowLine order by fl_name";
            FunSql.Init();
            DataTable ResTable = FunSql.GetTable(sql);
            string json = FunCommon.DataTableToJson(ResTable);
            Response.Write(json);
        }
        void SelectStation()    //查询工位
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string AssemblyLine = request.Params["AssemblyLine"];
            string sql = @"select distinct st_no from mg_station a left join mg_FlowLine b on a.fl_id = b.fl_id where b.fl_name = '" + AssemblyLine + "' order by a.st_no";
            FunSql.Init();
            DataTable ResTable = FunSql.GetTable(sql);
            string json = FunCommon.DataTableToJson(ResTable);
            Response.Write(json);
        }
        void SelectCheckReport()    //点检查询结果
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string AssemblyLine = request.Params["fl_name"];
            string Station = request.Params["st_id"];
            string StartTime = request.Params["StartTime"];
            string EndTime = request.Params["EndTime"];
            //string pageNumber = request.Params["PageIndex"];
            //string pageSize = request.Params["PageSize"];
            int PageIndex = Convert.ToInt32(request["page"]);
            int PageSize = Convert.ToInt32(request["rows"]); //            string.IsNullOrEmpty(StartTime)
            if (!string.IsNullOrEmpty(AssemblyLine) && string.IsNullOrEmpty(Station) && string.IsNullOrEmpty(StartTime))    //流水线选择，工位没有选择，时间没有选择的状态
            {
                string sql = "select a.ID,a.StationNO,d.op_name,c.PI_Item,case when a.IsPass = 1 then '合格' when a.IsPass = 0 then '不合格' else '未点检' end as IsPass,a.CreateTime from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE a.StationNO LIKE '" + AssemblyLine + "%' order by a.ID";
                FunSql.Init();
                DataTable ResTable = FunSql.GetTable(sql);
                //string sql2 = "select count(0) from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.PI_ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE a.StationNO LIKE '" + AssemblyLine + "%'";
                //int totalcount = FunSql.GetInt(sql2);
                int totalcount = ResTable.Rows.Count;
                DataTable NewResTable = GetPagedTable(ResTable,PageIndex,PageSize);
                string json = FunCommon.DataTableToJson2(totalcount,NewResTable);
                Response.Write(json);
                #region 导出代码   
                ExcelHelper.ExportDTtoExcel(ResTable, "", HttpContext.Current.Request.MapPath("~/App_Data/excel2027.xlsx"));
                #endregion
            }
            else if (!string.IsNullOrEmpty(AssemblyLine) && !string.IsNullOrEmpty(Station) && string.IsNullOrEmpty(StartTime))   //流水线选择，工位选择，时间没有选择的状态
            {
                string sql = "select distinct a.ID,a.StationNO,d.op_name,c.PI_Item,case when a.IsPass = 1 then '合格' when a.IsPass = 0 then '不合格' else '未点检' end as IsPass,a.CreateTime from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE a.StationNO = '" + Station + "' order by a.ID";
                FunSql.Init();
                DataTable ResTable = FunSql.GetTable(sql);
                //string sql2 = "select count(0) from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.PI_ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE a.StationNO = '" + Station + "'"; 
                //int totalcount = FunSql.GetInt(sql2);
                int totalcount = ResTable.Rows.Count;
                DataTable NewResTable = GetPagedTable(ResTable, PageIndex, PageSize);
                string json = FunCommon.DataTableToJson2(totalcount,NewResTable);
                Response.Write(json);
                #region 导出代码   
                ExcelHelper.ExportDTtoExcel(ResTable, "", HttpContext.Current.Request.MapPath("~/App_Data/excel2027.xlsx"));
                #endregion
            }
            else if (!string.IsNullOrEmpty(AssemblyLine) && !string.IsNullOrEmpty(Station) && !string.IsNullOrEmpty(StartTime))  //流水线选择，工位选择，时间选择的状态
            {
                string sql = "select distinct a.ID,a.StationNO,d.op_name,c.PI_Item,case when a.IsPass = 1 then '合格' when a.IsPass = 0 then '不合格' else '未点检' end as IsPass,a.CreateTime from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE a.StationNO = '" + Station + "' and cast(a.CreateTime as datetime) > '" + StartTime + "' and cast(a.CreateTime as datetime) < '" + EndTime + "' order by a.ID";
                FunSql.Init();
                DataTable ResTable = FunSql.GetTable(sql);
                //string sql2 = "select count(0) from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.PI_ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE a.StationNO = '" + Station + "' and cast(a.CreateTime as datetime) > '" + StartTime + "' and cast(a.CreateTime as datetime) < '" + EndTime + "'";
                //int totalcount = FunSql.GetInt(sql2);
                int totalcount = ResTable.Rows.Count;
                DataTable NewResTable = GetPagedTable(ResTable, PageIndex, PageSize);
                string json = FunCommon.DataTableToJson2(totalcount, NewResTable);
                Response.Write(json);
                #region 导出代码   
                ExcelHelper.ExportDTtoExcel(ResTable, "", HttpContext.Current.Request.MapPath("~/App_Data/excel2027.xlsx"));
                #endregion
            }
            else if (string.IsNullOrEmpty(AssemblyLine) && string.IsNullOrEmpty(Station) && !string.IsNullOrEmpty(StartTime))  //只选时间的状态
            {
                string sql = "select distinct a.ID,a.StationNO,d.op_name,c.PI_Item,case when a.IsPass = 1 then '合格' when a.IsPass = 0 then '不合格' else '未点检' end as IsPass,a.CreateTime from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE cast(a.CreateTime as datetime) > '" + StartTime + "' and cast(a.CreateTime as datetime) < '" + EndTime + "' order by a.ID";
                FunSql.Init();
                DataTable ResTable = FunSql.GetTable(sql);
                //string sql2 = "select count(0) from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.PI_ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id WHERE cast(a.CreateTime as datetime) > '" + StartTime + "' and cast(a.CreateTime as datetime) < '" + EndTime + "'";
                //int totalcount = FunSql.GetInt(sql2);
                int totalcount = ResTable.Rows.Count;
                DataTable NewResTable = GetPagedTable(ResTable, PageIndex, PageSize);
                string json = FunCommon.DataTableToJson2(totalcount,NewResTable);
                Response.Write(json);
                #region 导出代码   
                ExcelHelper.ExportDTtoExcel(ResTable, "", HttpContext.Current.Request.MapPath("~/App_Data/excel2027.xlsx"));
                #endregion
            }
            else    //均没有选择，即为显示全部
            {
                string sql = "select distinct a.ID,a.StationNO,d.op_name,c.PI_Item,case when a.IsPass = 1 then '合格' when a.IsPass = 0 then '不合格' else '未点检' end as IsPass,a.CreateTime from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id  order by a.ID";
                FunSql.Init();
                DataTable ResTable = FunSql.GetTable(sql);
                //string sql2 = "select distinct count(0) from mg_PointInspection_Item_Value a left join mg_PointInspection_Item_StationNo b on a.PIS_ID = b.PI_ID left join mg_PointInspection_Item c on b.PI_ID = c.ID left join mg_Operator d on a.OperatorID = d.op_id";
                //int totalcount = FunSql.GetInt(sql2);
                int totalcount = ResTable.Rows.Count;
                DataTable NewResTable = GetPagedTable(ResTable, PageIndex, PageSize);
                string json = FunCommon.DataTableToJson2(totalcount,NewResTable);
                Response.Write(json);
                #region 导出代码   
                ExcelHelper.ExportDTtoExcel(ResTable, "", HttpContext.Current.Request.MapPath("~/App_Data/excel2027.xlsx"));
                #endregion
            }
        }
        public DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)//PageIndex表示第几页，PageSize表示每页的记录数
        {
            if (PageIndex == 0)
                return dt;//0页代表每页数据，直接返回

            DataTable newdt = dt.Copy();
            newdt.Clear();//copy dt的框架

            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;//源数据记录数小于等于要显示的记录，直接返回dt

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
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