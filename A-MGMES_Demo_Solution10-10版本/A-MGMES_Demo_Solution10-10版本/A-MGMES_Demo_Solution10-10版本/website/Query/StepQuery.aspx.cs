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


public partial class Query_StepQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            //导出Excel
            ExportByWeb();
        }
        else
        {
            if (Request.Cookies["admininfo"] != null)
            {
                this.namelit.Text = Request.Cookies["admininfo"]["name"];
                this.tellit.Text = HttpUtility.UrlDecode(Request.Cookies["admininfo"]["user_posiid_name"]);
            }
        }
    }
    
    public static void ExportByWeb()
    {
        HttpContext curContext = HttpContext.Current;

        string outPutZipFile = Path.Combine(curContext.Server.MapPath("~/App_Data"), "步骤日志报表.xlsx");

        FileStream fileStream = new FileStream(outPutZipFile, FileMode.Open);
        long fileSize = fileStream.Length;
        byte[] fileBuffer = new byte[fileSize];
        fileStream.Read(fileBuffer, 0, (int)fileSize);
        fileStream.Close();

        curContext.Response.ContentType = "application/octet-stream";
        curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("步骤日志报表.xlsx", Encoding.UTF8));
        curContext.Response.AddHeader("Content-Length", fileSize.ToString());
        curContext.Response.BinaryWrite(fileBuffer);
        curContext.Response.End();
        curContext.Response.Close();
    }
}