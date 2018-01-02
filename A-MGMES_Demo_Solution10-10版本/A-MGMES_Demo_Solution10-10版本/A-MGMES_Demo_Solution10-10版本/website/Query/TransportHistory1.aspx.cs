using System;
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
    public partial class TransportHistory1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //导出Excel
                //ExportByWeb();
                ExportByWebNew("发运历史报表.xlsx");
            }
        }

        public static void ExportByWebNew(string fileName)
        {
            string filePath = HttpContext.Current.Request.MapPath("~/App_Data/发运历史报表.xlsx");
            HttpContext curContext = HttpContext.Current;
            FileInfo fileInfo = new FileInfo(filePath);
            curContext.Response.Clear();
            curContext.Response.ClearContent();
            curContext.Response.ClearHeaders();
            curContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            curContext.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            curContext.Response.AddHeader("Content-Transfer-Encoding", "binary");
            curContext.Response.ContentType = "application/octet-stream";
            curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            curContext.Response.WriteFile(fileInfo.FullName);
            curContext.Response.Flush();
            curContext.Response.End();
        }
        public static void ExportByWeb()
        {
            HttpContext curContext = HttpContext.Current;

            string outPutZipFile = Path.Combine(curContext.Server.MapPath("~/App_Data"), "发运历史报表.xlsx");

            FileStream fileStream = new FileStream(outPutZipFile, FileMode.Open);
            long fileSize = fileStream.Length;
            byte[] fileBuffer = new byte[fileSize];
            fileStream.Read(fileBuffer, 0, (int)fileSize);
            fileStream.Close();

            curContext.Response.ContentType = "application/octet-stream";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("发运历史报表.xlsx", Encoding.UTF8));
            curContext.Response.AddHeader("Content-Length", fileSize.ToString());
            curContext.Response.BinaryWrite(fileBuffer);
            curContext.Response.End();
            curContext.Response.Close();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}