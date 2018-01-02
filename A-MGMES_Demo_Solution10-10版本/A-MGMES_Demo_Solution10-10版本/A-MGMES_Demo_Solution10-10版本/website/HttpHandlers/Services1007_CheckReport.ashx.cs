using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Bll;
namespace website.HttpHandlers
{
    /// <summary>
    /// Services1007_CheckReport 的摘要说明
    /// </summary>
    public class Services1007_CheckReport : IHttpHandler
    {
        public static string sort = "-1";
        public static string order = "-1";
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
                case "Export":
                    Export();
                    break;
            }
        }
        public static string RequstString(string sParam)
        {
            return (HttpContext.Current.Request[sParam] == null ? string.Empty
                  : HttpContext.Current.Request[sParam].ToString().Trim());
        }
        void SelectAssemblyLine()    //查询流水线
        {

            string a = mg_sys_logBll.getfl_idList();
            Response.Write(a);
            Response.End();
        }
        void SelectStation()    //查询工位
        {
            string a = mg_sys_logBll.getst_idList(RequstString("fl_id"));
            Response.Write(a);
            Response.End();
        }
        void SelectCheckReport()    //点检查询结果
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string AssemblyLine = request.Params["fl_name"];
            string Station = request.Params["st_no"];
            string StartTime = request.Params["StartTime"];
            string EndTime = request.Params["EndTime"];
            
            int PageIndex = Convert.ToInt32(request["page"]);
            int PageSize = Convert.ToInt32(request["rows"]); //            string.IsNullOrEmpty(StartTime)
            string SortFlag = request["sort"];
            string sortOrder = request["order"];
            sort = SortFlag;
            order = sortOrder;
            string where="";
            if (!string.IsNullOrEmpty(AssemblyLine))
            {
                where = " and a.StationNO LIKE '" + AssemblyLine + "%'";
            }
            if (!string.IsNullOrEmpty(Station))
            {
                where = " and a.StationNO = '" + Station + "'";
            }

            if(!string.IsNullOrEmpty(StartTime))
            {
                where +=" and cast(a.CreateTime as datetime) > '" + StartTime + "'"; 
            }

            if(!string.IsNullOrEmpty(EndTime))
            {
                where += " and cast(a.CreateTime as datetime) < '" + EndTime + "'";
            }
            int StartIndex = PageSize * (PageIndex - 1) + 1;
            int EndIndex = StartIndex + PageSize - 1;
            int totalcount;
            DataTable ResTable = checkReport_BLL.getTable(PageSize, PageIndex, StartIndex, EndIndex, sort, order, where, out totalcount);
            DataTable NewResTable = GetPagedTable(ResTable, PageIndex, PageSize);
            string json = FunCommon.DataTableToJson2(totalcount, NewResTable);
            Response.Write(json);


           
        }


        void Export()    //点检查询结果导出
        {
            string json = "";
            DataTable ResTable = new DataTable();
            JavaScriptSerializer s = new JavaScriptSerializer();
            HttpRequest request = HttpContext.Current.Request;
            string AssemblyLine = request.Params["fl_name"];
            string Station = request.Params["st_no"];
            string StartTime = request.Params["StartTime"];
            string EndTime = request.Params["EndTime"];
            int PageIndex = Convert.ToInt32(request["page"]);
            int PageSize = Convert.ToInt32(request["rows"]);
            string where = "";
            if (!string.IsNullOrEmpty(AssemblyLine))
            {
                where = " and a.StationNO LIKE '" + AssemblyLine + "%'";
            }
            if (!string.IsNullOrEmpty(Station))
            {
                where = " and a.StationNO = '" + Station + "'";
            }

            if (!string.IsNullOrEmpty(StartTime))
            {
                where += " and cast(a.CreateTime as datetime) > '" + StartTime + "'";
            }

            if (!string.IsNullOrEmpty(EndTime))
            {
                where += " and cast(a.CreateTime as datetime) < '" + EndTime + "'";
            }
            int StartIndex = PageSize * (PageIndex - 1) + 1;
            int EndIndex = StartIndex + PageSize - 1;
            int totalcount;
            ResTable = checkReport_BLL.getTableExcel(PageSize, PageIndex, StartIndex, EndIndex, sort, order, where, out totalcount);
           
               
               
            //}
            try
            {
                 #region 导出代码   
                 //ExcelHelper.ExportDTtoExcel(ResTable, "", HttpContext.Current.Request.MapPath("~/App_Data/点检记录报表.xlsx"));
                string fileName = HttpContext.Current.Request.MapPath("~/App_Data/点检记录报表.xlsx");
                string err = "";
                AsposeExcelTools.DataTableToExcel2(ResTable, fileName, out err);
                string ss = "true";
                if (err.Length < 1)
                {
                    ss = "true";
                }
                else
                {
                    ss = "false";
                }

                json = ss;
                
                 #endregion
            }
            catch
            {
                json = "false";
            }
           
            Response.Write(json);
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