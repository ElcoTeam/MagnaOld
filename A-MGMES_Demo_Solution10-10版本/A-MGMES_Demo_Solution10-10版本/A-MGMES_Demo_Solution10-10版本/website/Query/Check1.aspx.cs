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
using System.Text;

namespace website.Query
{
    public partial class Check1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //导出Excel
                //ExportByWeb("检测返修报表.xls");
                //ExportByWeb("ExportDemo3.xls");
                ExportByWebNew1("检测返修报表.xlsx");
            }
        }
        public static MemoryStream ExcelStream()
        {
            DataTable dtTable = GetDtTable();
            return ExcelHelper.ExportDT(dtTable, "HeaderText");
        }

        private static DataTable GetDtTable()
        {
            string path = HttpContext.Current.Request.MapPath("~/App_Data/检测返修报表.xlsx");
            //调用ZK的ExcelHelper
            //DataTable dtTable = ExcelHelper.ImportExceltoDt(path);
            DataTable dtTable = new DataTable();
            string str;
            AsposeExcelTools.ExcelFileToDataTable(path,out dtTable,out str);
            return dtTable;
        }


        public static void ExportByWebNew1(string fileName)
        {
            HttpResponse Response = HttpContext.Current.Response;
            System.IO.Stream iStream = null;
            // Buffer to read 10K bytes in chunk:
            byte[] buffer = new Byte[10000];

            // Length of the file:

            int length;

            // Total bytes to read.

            long dataToRead;

            // Identify the file to download including its path.

            string filepath = HttpContext.Current.Request.MapPath("~/App_Data/检测返修报表.xlsx");

            // Identify the file name.

            string filename = System.IO.Path.GetFileName(filepath);

            try
            {

                // Open the file.

                iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open,

                            System.IO.FileAccess.Read, System.IO.FileShare.Read);



                // Total bytes to read.

                dataToRead = iStream.Length;



                Response.Clear();

                Response.ClearHeaders();

                Response.ClearContent();

                Response.ContentType = "text/plain"; // Set the file type

                Response.AddHeader("Content-Length", dataToRead.ToString());

                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);



                // Read the bytes.

                while (dataToRead > 0)
                {

                    // Verify that the client is connected.

                    if (Response.IsClientConnected)
                    {

                        // Read the data in buffer.

                        length = iStream.Read(buffer, 0, 10000);



                        // Write the data to the current output stream.

                        Response.OutputStream.Write(buffer, 0, length);



                        // Flush the data to the HTML output.

                        Response.Flush();



                        buffer = new Byte[10000];

                        dataToRead = dataToRead - length;

                    }

                    else
                    {

                        // Prevent infinite loop if user disconnects

                        dataToRead = -1;

                    }

                }

            }

            catch (Exception ex)
            {

                // Trap the error, if any.

                Response.Write("Error : " + ex.Message);

            }

            finally
            {

                if (iStream != null)
                {

                    //Close the file.

                    iStream.Close();

                }



                Response.End();

            }



            //string filePath = HttpContext.Current.Request.MapPath("~/App_Data/检测返修报表.xlsx");
            //HttpContext curContext = HttpContext.Current;
            //FileInfo fileInfo = new FileInfo(filePath);
            //curContext.Response.Clear();
            //curContext.Response.ClearContent();
            //curContext.Response.ClearHeaders();
            //curContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            
            //curContext.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            //curContext.Response.AddHeader("Content-Transfer-Encoding", "binary");
            //curContext.Response.ContentType = "application/octet-stream";
            //curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            //curContext.Response.WriteFile(fileInfo.FullName);
            //curContext.Response.Flush();
            //curContext.Response.End();
        }
        public static void ExportByWebNew(string fileName)
        {
            string filePath = HttpContext.Current.Request.MapPath("~/App_Data/检测返修报表.xlsx");
            HttpContext curContext = HttpContext.Current;
            FileInfo fileInfo = new FileInfo(filePath);
            curContext.Response.Clear();
            curContext.Response.ClearContent();
            curContext.Response.ClearHeaders();
            curContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

            curContext.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            curContext.Response.AddHeader("Content-Transfer-Encoding", "binary");
            curContext.Response.ContentType = "application/octet-stream";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.WriteFile(fileInfo.FullName);
            curContext.Response.Flush();
            curContext.Response.End();
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
            ExportByWeb("ExportDemo2.xls");
            ExportByWeb("ExportDemo3.xls");
        }
    }
}