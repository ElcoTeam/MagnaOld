using System;
using System.Web;
using Bll;
using Model;
using Tools;
using System.Collections.Generic;
public class ShowChiClient : IHttpHandler
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


            case "saveShowChiClient":
                saveShowChiClient();
                break;
            case "queryShowChiClientList":
                queryShowChiClientList();
                break;
            case "deleteShowChiClient":
                deleteShowChiClient();
                break;
            
        }
    }



    void saveShowChiClient()
    {
        string SID = Request.Params["SID"];
        string SName = Request.Params["SName"];
        string ClientIP = Request.Params["ClientIP"];
        string SAddTime = Request.Params["SAddTime"];
        string SRole = Request.Params["SRole"];
        string SRamark = Request.Params["SRamark"];


        px_ShowChiClientModel model = new px_ShowChiClientModel();
        model.SID = NumericParse.StringToInt(SID);
        model.SName = SName;
        model.ClientIP = ClientIP;
        model.SAddTime = DateTime.Now.ToString();
        model.SRole = SRole;
        model.SRamark = SRamark;

        string json = px_ShowChiClientBLL.SaveShowChiClient(model);
        Response.Write(json);
        Response.End();
    }
    void queryShowChiClientList()
    {
        string page = Request.Params["page"];
        string pagesize = Request.Params["rows"];
        if (string.IsNullOrEmpty(page))
        {
            page = "1";
        }
        if (string.IsNullOrEmpty(pagesize))
        {
            pagesize = "15";
        }

        string json = px_ShowChiClientBLL.queryShowChiClientList(page, pagesize);
        Response.Write(json);
        Response.End();
    }
    void deleteShowChiClient()
    {
        string SID = Request.Params["SID"];
        string json = px_ShowChiClientBLL.DeleteShowChiClient(SID);
        Response.Write(json);
        Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}