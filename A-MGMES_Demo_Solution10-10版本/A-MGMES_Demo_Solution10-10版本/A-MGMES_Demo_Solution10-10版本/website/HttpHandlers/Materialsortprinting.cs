using System;
using System.Web;
using Bll;
using Model;
using Tools;
using System.Collections.Generic;
public class Materialsortprinting : IHttpHandler
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
            case "saveMaterialsortprinting":
                SaveMaterialsortprinting();
                break;
            case "queryMaterialsortprintingList":
                queryMaterialsortprintingList();
                break;
            case "deleteMaterialsortprinting":
                DeleteMaterialsortprinting();
                break;
        }
    }


    void SaveMaterialsortprinting()
    {
        string IID = Request.Params["IID"];
        string IName = Request.Params["IName"];
        string PrintIP = Request.Params["PrintIP"];
        string IAddTime = Request.Params["IAddTime"];
        string IRole = Request.Params["IRole"];
        string IRamark = Request.Params["IRamark"];


        px_InternetPrinterModel model = new px_InternetPrinterModel();
        model.IID = NumericParse.StringToInt(IID);
        model.IName = IName;
        model.PrintIP = PrintIP;
        model.IAddTime = DateTime.Now.ToString();
        model.IRole = IRole;
        model.IRamark = IRamark;

        string json = px_InternetPrinterBLL.SaveInternetPrinter(model);
        Response.Write(json);
        Response.End();
    }
    void queryMaterialsortprintingList()
    {
        string page = Request.Params["page"];
        string pagesize = Request.Params["rows"];
        DateTime? starttime = NumericParse.StringToDateTime(Request.Params["starttime"]);
        DateTime? endtime = NumericParse.StringToDateTime(Request.Params["endtime"]);
        string csh = Request.Params["csh"];
        if (string.IsNullOrEmpty(page))
        {
            page = "1";
        }
        if (string.IsNullOrEmpty(pagesize))
        {
            pagesize = "15";
        }

        string json = px_MaterialsortprintingBLL.queryMaterialsortprintingList(page, pagesize,starttime,endtime,csh);
        Response.Write(json);
        Response.End();
    }
    void DeleteMaterialsortprinting()
    {
        //string IID = Request.Params["IID"];
        //string json = mg_allpartBLL.DeleteAllPart(IID);
        //Response.Write(json);
        //Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}