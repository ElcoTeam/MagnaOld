using System;
using System.Web;
using Bll;
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
    /// 修改时间：2017年6月26日
    /// 修改人：冉守旭
    void SaveOperator()
    {
        try
        {
            string op_id = Request.Params["op_id"];
            string st_id = Request.Params["st_id[]"];  /// 修改时间：2017年5月9日
            string op_name = Request.Params["op_name"];
            string op_no = Request.Params["op_no"];
            string op_sex = Request.Params["op_sex"];
            string op_isoperator = Request.Params["op_isoperator"];
            string op_pic = Request.Params["op_pic"];

            mg_OperatorModel model = new mg_OperatorModel();
            model.op_id = NumericParse.StringToInt(op_id);

            /// 修改开始
            string[] strArray = st_id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); //字符串转数组
            model.st_id = Convert.ToInt32(strArray[0]);
            model.list_st_id = st_id;
            //修改结束
            model.op_name = op_name;
            model.op_no = "0000000000000000000000000" + op_no;//25个0+7为工号A000001共32位
            model.op_sex = NumericParse.StringToInt(op_sex);
            model.op_isoperator = NumericParse.StringToInt(op_isoperator);
            model.op_pic = op_pic;
            string json = mg_OperatorBLL.SaveOperator(model);
            Response.Write(json);
            Response.End();
        }
        catch (Exception ex)
        {
            
           
        }
      
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