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

using System.Web.Services;
namespace website.Query
{
    public partial class RPT_HOURLY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                //导出Excel
                //ExportByWeb();
               
            }
           
        }

        public static void ExportByWeb()
        {
            HttpContext curContext = HttpContext.Current;

            string outPutZipFile = Path.Combine(curContext.Server.MapPath("~/App_Data"), "生产报表.xlsx");

            FileStream fileStream = new FileStream(outPutZipFile, FileMode.Open);
            long fileSize = fileStream.Length;
            byte[] fileBuffer = new byte[fileSize];
            fileStream.Read(fileBuffer, 0, (int)fileSize);
            fileStream.Close();

            curContext.Response.ContentType = "application/octet-stream";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("生产报表.xlsx", Encoding.UTF8));
            curContext.Response.AddHeader("Content-Length", fileSize.ToString());
            curContext.Response.BinaryWrite(fileBuffer);
            curContext.Response.End();
            curContext.Response.Close();
        }
    }
}