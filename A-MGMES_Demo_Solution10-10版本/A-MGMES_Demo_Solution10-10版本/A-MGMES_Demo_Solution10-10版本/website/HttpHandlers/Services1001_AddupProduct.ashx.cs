﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Bll;
namespace website.HttpHandlers
{
    /// <summary>
    /// Service1001_AddupProduct 的摘要说明
    /// </summary>
    public class Service1001_AddupProduct : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string StartTime = request["StartTime"];
            string EndTime = request["EndTime"];
            int Flag = Convert.ToInt32(request["Flag"]);    //按天，flag=2,按小时，flag=1
            string st_no = request["st_no"];
            string method = request["method"];
            string JsonStr = "[]";
            DataTable outtable;

            if (string.IsNullOrWhiteSpace(method))
            {
                
               
                volumn_report_BLL.GetAddupProducts(method, StartTime, EndTime, Flag, st_no, out outtable);
                //ExcelHelper.ExportDTtoExcel(outtable, "产量报表", HttpContext.Current.Request.MapPath("~/App_Data/产量报表.xlsx"));
                if (outtable != null)
                {
                    JsonStr = FunCommon.DataTableToJson(outtable);
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write(JsonStr);
                context.Response.End();
            }
            else
            {
                //DataReader.GetAddupProducts(method, StartTime, EndTime, Flag, st_no, out outtable);
                volumn_report_BLL.GetAddupProducts(method, StartTime, EndTime, Flag, st_no, out outtable);
               
                if (outtable != null)
                {
                    try
                    {
                        string fileName = HttpContext.Current.Request.MapPath("~/App_Data/产量报表.xlsx");

                       // ExcelHelper.ExportDTtoExcel(outtable, "产量报表", HttpContext.Current.Request.MapPath("~/App_Data/产量报表.xlsx"));
                        string err = "";
                        AsposeExcelTools.DataTableToExcel2(outtable, fileName, out err);
                        string ss = "true";
                        if (err.Length < 1)
                        {
                            ss = "true";
                        }
                        else
                        {
                            ss = "false";
                        }
                        JsonStr = ss;
                    }
                    catch
                    {
                        JsonStr = "false";
                    }
                }
                context.Response.ContentType = "json";
                context.Response.Write(JsonStr);
                context.Response.End();
            }
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