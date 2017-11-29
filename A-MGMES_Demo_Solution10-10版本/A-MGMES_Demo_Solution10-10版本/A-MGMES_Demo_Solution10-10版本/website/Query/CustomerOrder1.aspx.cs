﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace website.Query
{
    public partial class CustomerOrder1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //导出Excel
                ExportByWeb("客户订单报表.xls");
            }
        }
        public static MemoryStream ExcelStream()
        {
            DataTable dtTable = GetDtTable();
            return ExcelHelper.ExportDT(dtTable, "HeaderText");
        }

        private static DataTable GetDtTable()
        {
            string path = HttpContext.Current.Request.MapPath("~/App_Data/客户订单报表.xlsx");
            //调用ZK的ExcelHelper
            DataTable dtTable = ExcelHelper.ImportExceltoDt(path);
            return dtTable;
        }

        public static void ExportByWeb(string strFileName)
        {
            HttpContext curContext = HttpContext.Current;
            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

            curContext.Response.BinaryWrite(ExcelStream().GetBuffer());
            curContext.Response.End();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //导出Excel
            ExportByWeb("ExportDemo.xls");
        }
    }
}