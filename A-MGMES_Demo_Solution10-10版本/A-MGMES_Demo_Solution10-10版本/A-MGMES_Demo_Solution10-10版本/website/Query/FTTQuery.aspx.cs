using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bll;
using Tools;
using System.IO;
using website;
using System.Text;



public partial class Query_FTTQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            //导出Excel
            //ExportByWeb("ExportDemo.xls");
        }
        //if (!IsPostBack)
        //{
        //    BindData();
        //}
    }
    //public static MemoryStream ExcelStream()
    //{
    //    DataTable dtTable = GetDtTable();
    //    return ExcelHelper.ExportDT(dtTable, "HeaderText");

    //}

    //private static DataTable GetDtTable()
    //{
    //    string path = HttpContext.Current.Request.MapPath("~/App_Data/excel2006.xlsx");
    //    //调用ZK的ExcelHelper
    //    DataTable dtTable = ExcelHelper.ImportExceltoDt(path);
    //    //   ExcelHelper.ExportDTtoExcel(dtTable, "", HttpContext.Current.Request.MapPath("~/App_Data/excel2006.xlsx"));
    //    return dtTable;
    //}

    //public static void ExportByWeb(string strFileName)
    //{
    //    HttpContext curContext = HttpContext.Current;

    //    // 设置编码和附件格式
    //    curContext.Response.ContentType = "application/vnd.ms-excel";
    //    curContext.Response.ContentEncoding = Encoding.UTF8;
    //    curContext.Response.Charset = "";
    //    curContext.Response.AppendHeader("Content-Disposition",
    //        "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

    //    curContext.Response.BinaryWrite(ExcelStream().GetBuffer());
    //    curContext.Response.End();
    //}

    

}