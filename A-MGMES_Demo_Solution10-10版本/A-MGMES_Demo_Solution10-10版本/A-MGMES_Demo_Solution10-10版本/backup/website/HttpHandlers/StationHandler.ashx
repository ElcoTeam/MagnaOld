<%@ WebHandler Language="C#" Class="StationHandler" %>

using System;
using System.Web;
using BLL;
using Model;
using Tools;

public class StationHandler : IHttpHandler
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

            case "queryStationForOperatorEditing":
                QueryStationForOperatorEditing();
                break;
            case "generateNO":
                GenerateNO();
                break;

            case "saveStation":
                saveStation();
                break;

            case "queryStationList":
                QueryStationList();
                break;


            case "deleteStation":
                DeleteStation();
                break;

            case "sorting":
                Sorting();
                break;


            case "queryStationForStepEditing":
                QueryStationForStepEditing();
                break;

        }
    }

    void QueryStationForStepEditing()
    {
        string fl_id = Request.Params["fl_id"];
        string json = mg_StationBLL.QueryStationForStepEditing(fl_id);
        Response.Write(json);
        Response.End();
    }


    void Sorting()
    {
        string st_id = Request.Params["st_id"];
        string st_order = Request.Params["st_order"];
        string point = Request.Params["point"];

        string json = mg_StationBLL.Sorting(st_id, st_order, point);
        Response.Write(json);
        Response.End();
    }


    void DeleteStation()
    {
        string st_id = Request.Params["st_id"];

        string json = mg_StationBLL.DeleteStation(st_id);
        Response.Write(json);
        Response.End();
    }

    void QueryStationList()
    {
        string fl_id = Request.Params["fl_id"];
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

        string json = mg_StationBLL.QueryStationList(page, pagesize, fl_id);
        Response.Write(json);
        Response.End();
    }


    void saveStation()
    {
        string st_id = Request.Params["st_id"];
        string fl_id = Request.Params["fl_id"];
        string st_typeid = Request.Params["st_typeid"];
        string st_no = Request.Params["st_no"];
        string st_name = Request.Params["st_name"];
        string st_mac = Request.Params["st_mac"];
        string st_mushifile = Request.Params["st_mushifile"];
        string st_odsfile = Request.Params["st_odsfile"];
        string st_isfirst = Request.Params["st_isfirst"];
        string st_isend = Request.Params["st_isend"];


        mg_stationModel model = new mg_stationModel();
        model.st_id = NumericParse.StringToInt(st_id);
        model.fl_id = NumericParse.StringToInt(fl_id);
        model.st_typeid = NumericParse.StringToInt(st_typeid);
        model.st_no = st_no;
        model.st_name = st_name;
        model.st_mac = st_mac;
        model.st_mushifile = st_mushifile;
        model.st_odsfile = st_odsfile;
        model.st_isfirst = NumericParse.StringToInt(st_isfirst);
        model.st_isend = NumericParse.StringToInt(st_isend);

        string json = mg_StationBLL.saveStation(model);
        Response.Write(json);
        Response.End();
    }



    void GenerateNO()
    {
        string json = mg_StationBLL.GenerateNO();
        Response.Write(json);
        Response.End();
    }



    void QueryStationForOperatorEditing()
    {
        string json = mg_StationBLL.QueryStationForOperatorEditing();
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