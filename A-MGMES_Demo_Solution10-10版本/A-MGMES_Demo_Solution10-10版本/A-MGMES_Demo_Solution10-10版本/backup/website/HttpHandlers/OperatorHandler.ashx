<%@ WebHandler Language="C#" Class="OperatorHandler" %>

using System;
using System.Web;
using BLL;
using Model;
using Tools;

public class OperatorHandler : IHttpHandler
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
            case "generateNO":
                GenerateNO();
                break;

            case "saveOperator":
                SaveOperator();
                break;
            case "queryOperatorList":
                QueryOperatorList();
                break;
            case "deleteOperator":
                DeleteOperator();
                break;


        }
    }

    void QueryOperatorList()
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
        mg_OperatorBLL bll = new mg_OperatorBLL();
        string json = bll.QueryOperatorList(page, pagesize);
        Response.Write(json);
        Response.End();
    }

    void SaveOperator()
    {
        string op_id = Request.Params["op_id"];
        string st_id = Request.Params["st_id"];
        string op_name = Request.Params["op_name"];
        string op_no = Request.Params["op_no"];
        string op_sex = Request.Params["op_sex"];
        string op_isoperator = Request.Params["op_isoperator"];
        string op_pic = Request.Params["op_pic"];

        mg_OperatorModel model = new mg_OperatorModel();
        model.op_id = NumericParse.StringToInt(op_id);
        model.st_id = NumericParse.StringToInt(st_id);
        model.op_name = op_name;
        model.op_no = "00000000000000000000000000" + op_no;
        model.op_sex = NumericParse.StringToInt(op_sex);
        model.op_isoperator = NumericParse.StringToInt(op_isoperator);
        model.op_pic = op_pic;
        string json = mg_OperatorBLL.SaveOperator(model);
        Response.Write(json);
        Response.End();
    }

    void DeleteOperator()
    {
        string op_id = Request.Params["op_id"];

        string json = mg_OperatorBLL.DeleteOperator(op_id);
        Response.Write(json);
        Response.End();
    }


    void GenerateNO()
    {
        string json = mg_OperatorBLL.GenerateNO();
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