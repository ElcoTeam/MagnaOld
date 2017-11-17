<%@ WebHandler Language="C#" Class="AllPartHandler" %>

using System;
using System.Web;
using BLL;
using Model;
using Tools;

public class AllPartHandler : IHttpHandler
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
            

            case "saveAllPart":
                SaveAllPart();
                break;
            case "queryAllPartList":
                QueryAllPartList();
                break;
            case "deleteAllPart":
                DeleteAllPart();
                break;

            case "queryAllPartListForPart":
                QueryAllPartListForPart();
                break;
                
                
        }
    }


    void QueryAllPartListForPart()
    {
        string json = mg_allpartBLL.QueryAllPartListForPart();
        Response.Write(json);
        Response.End();
    }
    

    void QueryAllPartList()
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
        
        string json = mg_allpartBLL.QueryAllPartList(page, pagesize);
        Response.Write(json);
        Response.End();
    }

    void SaveAllPart()
    {
        string all_id = Request.Params["all_id"];
        string all_rateid = Request.Params["all_rateid"];
        string all_colorid = Request.Params["all_colorid"];
        string all_metaid = Request.Params["all_metaid"];
        string all_no = Request.Params["all_no"];
        string all_desc = Request.Params["all_desc"];

        mg_allpartModel model = new mg_allpartModel();
        model.all_id = NumericParse.StringToInt(all_id);
        model.all_rateid = NumericParse.StringToInt(all_rateid);
        model.all_colorid = NumericParse.StringToInt(all_colorid);
        model.all_metaid = NumericParse.StringToInt(all_metaid);
        model.all_no = all_no;
        model.all_desc = all_desc;

        string json = mg_allpartBLL.SaveAllPart(model);
        Response.Write(json);
        Response.End();
    }

    void DeleteAllPart()
    {
        string all_id = Request.Params["all_id"];

        string json = mg_allpartBLL.DeleteAllPart(all_id);
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