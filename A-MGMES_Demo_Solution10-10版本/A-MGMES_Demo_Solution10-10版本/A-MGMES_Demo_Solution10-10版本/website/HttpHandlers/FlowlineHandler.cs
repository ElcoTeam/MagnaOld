using System;
using System.Web;
using Bll;
using Model;

public class FlowlineHandler : IHttpHandler
{
    //工序步骤查询
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
            case "queryFlowlingForEditing":
                QueryFlowlingForEditing();
                break;
            case "queryFlowlingForStepEditing":
                QueryFlowlingForStepEditing();
                break;
                
        }
    }

    void QueryFlowlingForStepEditing()
    {
        string json = mg_FlowLineBLL.QueryFlowlingForStepEditing();
        Response.Write(json);
        Response.End();
    }
    
    void QueryFlowlingForEditing()
    {
        string json = mg_FlowLineBLL.QueryFlowlingForEditing();
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