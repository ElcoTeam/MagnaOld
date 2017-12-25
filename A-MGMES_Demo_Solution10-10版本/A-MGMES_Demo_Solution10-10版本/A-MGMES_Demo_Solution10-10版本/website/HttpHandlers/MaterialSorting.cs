using System;
using System.Web;
using Bll;
using Model;
using Tools;
using System.Collections.Generic;
public class MaterialSorting : IHttpHandler
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


            case "savePanrameter":
                SavePanrameter();
                break;
            case "queryPanrameterList":
                QueryPanrameterList();
                break;
            case "deletePanrameter":
                DeletePanrameter();
                break;

            case "queryPanrameterListForPart":
                QueryPanrameterListForPart();
                break;
            case "UpPanrameter":
                UpPanrameter();
                break;
            case "DownPanrameter":
                DownPanrameter();
                break;
            case "sendPanrameter":
                sendPanrameter();
                break;
            case "printPanrameter":
                printPanrameter();
                break;
            case "ascdescPanrameter":
                ascdescPanrameter();
                break; 
                
                
        }
    }

    void QueryPanrameterListForPart()
    {
        string json = px_PanrameterBLL.QueryPanrameterListForPart();
        Response.Write(json);
        Response.End();
    }

    /// <summary>
    /// 上调数据
    /// </summary>
    void UpPanrameter()
    {
        string sid = Request.Params["id"];   
        string value = Request.Params["name"];
        if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(value) || sid == "1")
        {
            Response.Write("上调失败！");
        }       
        else
        {
          
            int id = Convert.ToInt32(sid.Trim());
            string name = value.Trim();
            bool json = px_PanrameterBLL.UpPanrameterId(id, name);
            if (json==true)
            {
                Response.Write("上调成功！");
            }
            else
            { 
            Response.Write("上调失败！");
            }          
            Response.End();
        }       
       
    }
    /// <summary>
    /// 下调数据
    /// </summary>
    void DownPanrameter()
    {
        string sid = Request.Params["id"];
        int id = Convert.ToInt32(sid.Trim());
        string value = Request.Params["name"];
        int count = px_PanrameterBLL.QueryPanrameterList().Count;
        if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(value) || id == count)
        {
            Response.Write("下调失败!");
        }       
        else
        {      
           
            string name = value.Trim();
            bool json = px_PanrameterBLL.DownPanrameterId(id, name);
            if (json == true)
            {
                Response.Write("下调成功！");
            }
            else
            {
                Response.Write("下调失败！");
            }
            Response.End();
        }

    }


    /// <summary>
    /// 自动下发
    /// </summary>
    void sendPanrameter()
    {
        string sid = Request.Params["id"];
        string value = Request.Params["name"];
        if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(value))
        {
            Response.Write("操作失败！");
        }
        else
        {
            int id = Convert.ToInt32(sid.Trim());
            string name = value.Trim();
            bool json = px_PanrameterBLL.sendPanrameter(id, name);
            if (json == true)
            {
                Response.Write("操作成功！");
            }
            else
            {
                Response.Write("操作失败！");
            }
            Response.End();
        }

    }


    /// <summary>
    /// 自动打印
    /// </summary>
    void printPanrameter()
    {
        string sid = Request.Params["id"];
        string value = Request.Params["name"];
        if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(value))
        {
            Response.Write("操作失败！");
        }
        else
        {
            int id = Convert.ToInt32(sid.Trim());
            string name = value.Trim();
            bool json = px_PanrameterBLL.PrintPanrameter(id, name);
            if (json == true)
            {
                Response.Write("操作成功！");
            }
            else
            {
                Response.Write("操作失败！");
            }
            Response.End();
        }

    }


    /// <summary>
    /// 是否倒序打印
    /// </summary>
    void ascdescPanrameter()
    {
        string sid = Request.Params["id"];
        string value = Request.Params["name"];
        if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(value))
        {
            Response.Write("操作失败！");
        }
        else
        {
            int id = Convert.ToInt32(sid.Trim());
            string name = value.Trim();
            bool json = px_PanrameterBLL.AscordescPanrameter(id, name);
            if (json == true)
            {
                Response.Write("操作成功！");
            }
            else
            {
                Response.Write("操作失败！");
            }
            Response.End();
        }

    }


    void QueryPanrameterList()
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

        string json = px_PanrameterBLL.QueryPanrameterList(page, pagesize);
        Response.Write(json);
        Response.End();
    }
    void SavePanrameter()
    {
        string SerialID = Request.Params["SerialID"];
        string Name = Request.Params["Name"];
        string Number = Request.Params["Number"];

        px_PanrameterModel model = new px_PanrameterModel();
        model.SerialID = NumericParse.StringToInt(SerialID);
        model.Name = Name;
        model.Number = NumericParse.StringToInt(Number);

        string json = px_PanrameterBLL.SavePanrameter(model);
        Response.Write(json);
        Response.End();
    }

    void DeletePanrameter()
    {
        //string all_id = Request.Params["all_id"];

        //string json = px_PanrameterBLL.DeleteAllPart(all_id);
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