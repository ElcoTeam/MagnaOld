<%@ WebHandler Language="C#" Class="BOMHandler" %>

using System;
using System.Web;
using BLL;
using Model;
using Tools;

public class BOMHandler : IHttpHandler
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


            case "saveBOM":
                SaveBOM();
                break;
            case "queryBOMList":
                QueryBOMList();
                break;
            case "deleteBOM":
                DeleteBOM();
                break;
            case "getBomRelation":
                GetBomRelation();
                break;
            case "getAllPartRelation":
                GetAllPartRelation();
                break;
            case "getPartRelation":
                GetPartRelation();
                break;
            case "queryBOMForStepEditing":
                QueryBOMForStepEditing();
                break;
                
                
            //case "queryPartListForBOM":
            //    QueryPartListForBOM();
            //    break;
           
        }
    }

    void QueryBOMForStepEditing()
    {
        string part_id = Request.Params["part_id"];
        string json = mg_BomBLL.QueryBOMForStepEditing(part_id);
        Response.Write(json);
        Response.End();
    }
    //void QueryPartListForBOM()
    //{

    //    string json = mg_PartBLL.QueryPartListForBOM();
    //    Response.Write(json);
    //    Response.End();
    //}


    //void QueryAllPartListForPart()
    //{
    //    string json = mg_allpartBLL.QueryAllPartListForPart();
    //    Response.Write(json);
    //    Response.End();
    //}

    void GetPartRelation()
    {
        string id = Request.Params["id"];
        string key = "p.part_id";
        string json = mg_BomBLL.GetRelation(key, id);
        Response.Write(json);
        Response.End();
    }
    void GetAllPartRelation()
    {
        string id = Request.Params["id"];
        string key = "ap.[all_id]";
        string json = mg_BomBLL.GetRelation(key, id);
        Response.Write(json);
        Response.End();
    }
    void GetBomRelation()
    {
        string id = Request.Params["id"];
        string key = "b.bom_id";
        string json = mg_BomBLL.GetRelation(key, id);
        Response.Write(json);
        Response.End();
    }
    void QueryBOMList()
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

        string json = mg_BomBLL.QueryBOMList(page, pagesize);
        Response.Write(json);
        Response.End();
    }

    void SaveBOM()
    {
        
        string bom_id = Request.Params["bom_id"];
        string bom_PN = Request.Params["bom_PN"];
        string bom_customerPN = Request.Params["bom_customerPN"];
        string bom_isCustomerPN = Request.Params["bom_isCustomerPN"];
        string bom_picture = Request.Params["bom_picture"];
        string bom_weight = Request.Params["bom_weight"];
        string bom_leve = Request.Params["bom_leve"];
        string bom_colorid = Request.Params["bom_colorid"];
        string bom_materialid = Request.Params["bom_materialid"];
        string bom_categoryid = Request.Params["bom_categoryid"];
        string bom_storeid = Request.Params["bom_storeid"];
        string bom_suppllerid = Request.Params["bom_suppllerid"];
        string bom_profile = Request.Params["bom_profile"];
        string bom_descCH = Request.Params["bom_descCH"];
        string bom_desc = Request.Params["bom_desc"];
        string partIDs = Request.Params["partIDs"];
        
        mg_BOMModel model = new mg_BOMModel();
        model.bom_id = NumericParse.StringToInt(bom_id);
        model.bom_PN = bom_PN;
        model.bom_customerPN = bom_customerPN;
        model.bom_isCustomerPN =NumericParse.StringToInt( bom_isCustomerPN);
        model.bom_picture = bom_picture;
        model.bom_weight =NumericParse.StringToInt( bom_weight);
        model.bom_leve =NumericParse.StringToInt( bom_leve);
        model.bom_colorid =NumericParse.StringToInt( bom_colorid);
        model.bom_materialid =NumericParse.StringToInt( bom_materialid);
        model.bom_categoryid = NumericParse.StringToInt(bom_categoryid);
        model.bom_storeid = NumericParse.StringToInt(bom_storeid);
        model.bom_suppllerid =NumericParse.StringToInt( bom_suppllerid);
        model.bom_profile = bom_profile;
        model.bom_descCH = bom_descCH;
        model.bom_desc = bom_desc;
        model.partIDs = partIDs;

        string json = mg_BomBLL.SaveBOM(model);
        Response.Write(json);
        Response.End();
    }

    void DeleteBOM()
    {
        string bom_id = Request.Params["bom_id"];

        string json = mg_BomBLL.DeleteBOM(bom_id);
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