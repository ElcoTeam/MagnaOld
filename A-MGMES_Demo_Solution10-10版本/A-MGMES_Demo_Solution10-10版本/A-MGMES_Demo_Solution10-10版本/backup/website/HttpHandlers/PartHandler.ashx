<%@ WebHandler Language="C#" Class="PartHandler" %>

using System;
using System.Web;
using BLL;
using Model;
using Tools;

public class PartHandler : IHttpHandler
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


            case "savePart":
                savePart();
                break;
            case "queryAllPartList":
                QueryAllPartList();
                break;
            case "deletePart":
                DeletePart();
                break;
            case "queryPartListForBOM":
                QueryPartListForBOM();
                break;
            case "queryPartForStepEditing":
                QueryPartForStepEditing();
                break;
            case "queryPartForStepSearching":
                queryPartForStepSearching();
                break;


            //case "queryAllPartListForPart":
            //    QueryAllPartListForPart();
            //    break;


        }
    }

    void queryPartForStepSearching()
    {
        string json = mg_PartBLL.queryPartForStepSearching();
        Response.Write(json);
        Response.End();
    }
    void QueryPartForStepEditing()
    {
        string json = mg_PartBLL.QueryPartForStepEditing();
        Response.Write(json);
        Response.End();
    }

    void QueryPartListForBOM()
    {

        string json = mg_PartBLL.QueryPartListForBOM();
        Response.Write(json);
        Response.End();
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

        string json = mg_PartBLL.QueryAllPartList(page, pagesize);
        Response.Write(json);
        Response.End();
    }

    void savePart()
    {
        string part_id = Request.Params["part_id"];
        string part_categoryid = Request.Params["part_categoryid"];
        string part_no = Request.Params["part_no"];
        string part_desc = Request.Params["part_desc"];
        string part_name = Request.Params["part_name"];
        string allpartIDs = Request.Params["allpartIDs"];

        mg_partModel model = new mg_partModel();
        model.part_id = NumericParse.StringToInt(part_id);
        model.part_categoryid = NumericParse.StringToInt(part_categoryid);
        model.part_no = (!string.IsNullOrEmpty(part_no)) ? part_no.Trim() : "";
        model.part_desc = part_desc;
        model.part_name = part_name;
        model.allpartIDs = allpartIDs;

        string json = mg_PartBLL.SavePart(model);
        Response.Write(json);
        Response.End();
    }

    void DeletePart()
    {
        string part_id = Request.Params["part_id"];

        string json = mg_PartBLL.DeletePart(part_id);
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