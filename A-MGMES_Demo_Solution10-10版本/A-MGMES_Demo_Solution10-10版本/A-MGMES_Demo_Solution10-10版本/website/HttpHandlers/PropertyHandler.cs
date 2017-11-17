using System;
using System.Web;
using Bll;
using Model;

public class PropertyHandler : IHttpHandler
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
            case "queryRateForEditing":
                queryRateForEditing();
                break;

            case "queryColorForEditing":
                queryColorForEditing();
                break;

            case "queryMetaForEditing":
                queryMetaForEditing();
                break;

            case "queryCategoryForPart":
                QueryCategoryForPart();
                break;



            case "queryLeveForBOM":
                QueryLeveForBOM();
                break;
            case "queryColorForBOM":
                QueryColorForBOM();
                break;
            case "queryMaterialForBOM":
                QueryMaterialForBOM();
                break;
            case "queryCategoryForBOM":
                QueryCategoryForBOM();
                break;
            case "querySupplierForBOM":
                QuerySupplierForBOM();
                break;
            case "queryStoreForBOM":
                QueryStoreForBOM();
                break;

            case "queryStationTypeForEditing":
                QueryStationTypeForEditing();
                break;

            case "queryCustomer":
                QueryCustomer();
                break; 
                
                
                
        }
    }
    void QueryCustomer()
    {
        string json = mg_PropertyBLL.queryJSONStringByPropertyType(mg_PropertyEnum.Customer);
        Response.Write(json);
        Response.End();
    }
    

    void QueryStationTypeForEditing()
    {
        string json = mg_PropertyBLL.queryJSONStringByPropertyType(mg_PropertyEnum.StationType);
        Response.Write(json);
        Response.End();
    }
    
    void QueryStoreForBOM()
    {
        string json = mg_PropertyBLL.queryJSONStringByPropertyType(mg_PropertyEnum.BOMStore);
        Response.Write(json);
        Response.End();
    }
    void QuerySupplierForBOM()
    {
        string json = mg_PropertyBLL.queryJSONStringByPropertyType(mg_PropertyEnum.BOMSupplier);
        Response.Write(json);
        Response.End();
    }
    void QueryCategoryForBOM()
    {
        string json = mg_PropertyBLL.queryJSONStringByPropertyType(mg_PropertyEnum.BOMCategory);
        Response.Write(json);
        Response.End();
    }
    void QueryMaterialForBOM()
    {
        string json = mg_PropertyBLL.queryJSONStringByPropertyType(mg_PropertyEnum.BOMMaterial);
        Response.Write(json);
        Response.End();
    }
    void QueryColorForBOM()
    {
        string json = mg_PropertyBLL.queryJSONStringByPropertyType(mg_PropertyEnum.BOMColor);
        Response.Write(json);
        Response.End();
    }
    void QueryLeveForBOM()
    {
        string json = mg_PropertyBLL.queryJSONStringByPropertyType(mg_PropertyEnum.BOMLevel);
        Response.Write(json);
        Response.End();
    }




    void QueryCategoryForPart()
    {
        string json = mg_PropertyBLL.QueryCategoryForPart();
        Response.Write(json);
        Response.End();
    }

    void queryRateForEditing()
    {
        string json = mg_PropertyBLL.queryRateForEditing();
        Response.Write(json);
        Response.End();
    }

    void queryColorForEditing()
    {
        string json = mg_PropertyBLL.queryColorForEditing();
        Response.Write(json);
        Response.End();
    }

    void queryMetaForEditing()
    {
        string json = mg_PropertyBLL.queryMetaForEditing();
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